using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShitFood.Api.FoodHygieneApi;
using System.Net.Http;
using ShitFood.Api.Ptos;
using System.Linq;
using Mapster;
using System.Collections.Generic;

namespace ShitFood.Api
{
    public class UpdateFoodHygieneRatingsFunction
    {
        private readonly ShitFoodContext _context;

        public UpdateFoodHygieneRatingsFunction(ShitFoodContext context)
        {
            _context = context;

            TypeAdapterConfig<Establishment, FoodHygieneRatingPto>
                .NewConfig()
                .Map(dest => dest.Hygiene, src => src.scores.Hygiene)
                .Map(dest => dest.Structural, src => src.scores.Structural)
                .Map(dest => dest.ConfidenceInManagement, src => src.scores.ConfidenceInManagement)
                .Map(dest => dest.Latitude, src => double.Parse(src.geocode.latitude))
                .Map(dest => dest.Longitude, src => double.Parse(src.geocode.longitude))
                .Map(dest => dest.Link, src => src.links.FirstOrDefault().href);
        }

        [FunctionName("UpdateFoodHygieneRatings")]
        public async Task<IActionResult> Update([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            string lat = req.Query["lat"];
            string lng = req.Query["lng"];

            if (string.IsNullOrWhiteSpace(lat) || string.IsNullOrWhiteSpace(lng))
            {
                return new NotFoundResult();
            }

            try
            {
                int page = 1;
                log.LogInformation($"Getting page {page} for {lat}, {lng}");
                List<Establishment> establishments = await GetBadFoodHygieneRatings(lat, lng, page);
                while (establishments != null && establishments.Count() > 0)
                {
                    foreach (Establishment establishment in establishments)
                    {
                        FoodHygieneRatingPto foodHygieneRatingPto = _context.FoodHygieneRatings.Find(establishment.FHRSID);
                        if (foodHygieneRatingPto != null)
                        {
                            // update
                            log.LogInformation($"Updating {establishment.FHRSID}");
                            establishment.Adapt(foodHygieneRatingPto);
                            _context.FoodHygieneRatings.Update(foodHygieneRatingPto);
                        }
                        else
                        {
                            // insert
                            log.LogInformation($"Inserting {establishment.FHRSID}");
                            foodHygieneRatingPto = new FoodHygieneRatingPto();
                            establishment.Adapt(foodHygieneRatingPto);
                            // find generic place
                            var place = _context.Places.FirstOrDefault(x => x.Lat == foodHygieneRatingPto.Latitude && x.Lng == foodHygieneRatingPto.Longitude && x.Name == foodHygieneRatingPto.BusinessName);
                            if (place == null)
                            {
                                place = new PlacePto
                                {
                                    Lat = foodHygieneRatingPto.Latitude,
                                    Lng = foodHygieneRatingPto.Longitude,
                                    Name = foodHygieneRatingPto.BusinessName,
                                    FoodHygieneRating = foodHygieneRatingPto
                                };
                                _context.Places.Add(place);
                            }
                            foodHygieneRatingPto.Place = place;
                            _context.FoodHygieneRatings.Add(foodHygieneRatingPto);
                        }
                    }
                    page++;
                    establishments = await GetBadFoodHygieneRatings(lat, lng, page);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return new OkResult();
        }

        private async Task<List<Establishment>> GetBadFoodHygieneRatings(string lat, string lng, int page = 1)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.ratings.food.gov.uk/Establishments?latitude={lat}&longitude={lng}&ratingKey=2&ratingOperatorKey=LessThanOrEqual&maxDistanceLimit=25&pageNumber={page}&pageSize=100");
            request.Headers.Add("x-api-version", "2");
            try
            {
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return (JsonConvert.DeserializeObject<EstablishmentsArray>(await response.Content.ReadAsStringAsync())?.Establishments).ToList();
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}
