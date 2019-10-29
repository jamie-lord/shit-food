using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using ShitFood.Api.TripAdvisor;
using ShitFood.Api.Ptos;
using NetTopologySuite.Geometries;
using Microsoft.Extensions.Caching.Distributed;

namespace ShitFood.Api
{
    public class UpdateTripAdvisorFunction : UpdateSourceFunctionBase
    {
        private HttpClient _httpClient = new HttpClient();
        private const string JsonPattern = @"\{(?:[^\{\}]|(?<o>\{)|(?<-o>\}))+(?(o)(?!))\}";

        public UpdateTripAdvisorFunction(ShitFoodContext context, IDistributedCache cache) : base(context, cache)
        {
        }

        [FunctionName("UpdateTripAdvisor")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            var geoId = 186356;
            var page = 1;
            var lastPage = false;

            do
            {
                var r = await GetRestaurantsOnPage(geoId, page, log);
                if (r.Item1 != null)
                {
                    foreach (Summary summary in r.Item1)
                    {
                        Details details = await GetRestaurantDetails(geoId, summary.locationId, log);

                        try
                        {
                            if (details != null)
                            {
                                TripAdvisorPto tripAdvisorPto = Context.TripAdvisorLocations.Find(summary.locationId);
                                if (tripAdvisorPto != null)
                                {
                                    // update
                                    log.LogInformation($"Updating TripAdvisor location {summary.locationId}");
                                    tripAdvisorPto.Rating = summary.averageRating;
                                    tripAdvisorPto.SummaryObject = summary;
                                    tripAdvisorPto.DetailsObject = details;
                                    tripAdvisorPto.Updated = DateTime.Now;
                                    Context.Update(tripAdvisorPto);
                                }
                                else
                                {
                                    // insert
                                    log.LogInformation($"Inserting new TripAdvisor location {summary.locationId}");
                                    tripAdvisorPto = new TripAdvisorPto
                                    {
                                        LocationId = summary.locationId,
                                        Rating = summary.averageRating,
                                        SummaryObject = summary,
                                        DetailsObject = details,
                                        Updated = DateTime.Now
                                    };
                                    PlacePto placePto = FindExistingPlace(log, details.location.latitude, details.location.longitude, summary.name);
                                    if (placePto == null)
                                    {
                                        placePto = new PlacePto
                                        {
                                            Location = new Point(details.location.latitude, details.location.longitude)
                                            {
                                                SRID = 4326
                                            },
                                            Name = summary.name,
                                            TripAdvisorLocation = tripAdvisorPto
                                        };
                                        Context.Places.Add(placePto);
                                    }
                                    tripAdvisorPto.Place = placePto;
                                    Context.TripAdvisorLocations.Add(tripAdvisorPto);
                                }
                                Context.SaveChanges();
                                await RemoveCachedPlace(log, tripAdvisorPto.PlaceId.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            log.LogError(ex.Message);
                        }
                    }
                    page++;
                }
                lastPage = r.Item2;
            } while (!lastPage);

            Context.SaveChanges();
            return new OkResult();
        }

        private async Task<Details> GetRestaurantDetails(int geoId, int locationId, ILogger log)
        {
            string url = $"https://www.tripadvisor.co.uk/Restaurant_Review-g{geoId}-d{locationId}";
            log.LogInformation($"Getting restaurant {locationId} details");

            try
            {
                string pageHtml = await _httpClient.GetStringAsync(url);
                string input = pageHtml.Substring(pageHtml.IndexOf($"\"/data/1.0/restaurant/{locationId}/overview\":"));
                Match match = Regex.Matches(input, JsonPattern, RegexOptions.Multiline | RegexOptions.IgnoreCase)[0];
                string jsonText = match.Groups[0].Value;
                var jsonObj = JObject.Parse(jsonText);
                var dataObj = jsonObj.Value<JObject>("data");
                return JsonConvert.DeserializeObject<Details>(dataObj.ToString());
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return null;
            }
        }

        private async Task<(Summary[], bool)> GetRestaurantsOnPage(int geoId, int pageNumber, ILogger log)
        {
            string url = $"https://www.tripadvisor.co.uk/Restaurants-g{geoId}";
            if (pageNumber > 1)
            {
                var count = $"oa{30 * (pageNumber - 1)}";
                url = $"{url}-{count}";
            }

            log.LogInformation($"Getting restaurants on {url}");

            try
            {
                string pageHtml = await _httpClient.GetStringAsync(url);
                string input = pageHtml.Substring(pageHtml.IndexOf("{\"data\":{\"restaurants\":"));
                Match match = Regex.Matches(input, JsonPattern, RegexOptions.Multiline | RegexOptions.IgnoreCase)[0];
                string jsonText = match.Groups[0].Value;
                var jsonObj = JObject.Parse(jsonText);
                var jsonArray = jsonObj.Value<JObject>("data").Value<JArray>("restaurants");
                var restaurants = JsonConvert.DeserializeObject<Summary[]>(jsonArray.ToString());
                log.LogInformation($"Deserialised {restaurants.Length} restaurants");
                return (restaurants, pageHtml.Contains("nav next disabled"));
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return (null, true);
            }
        }
    }
}
