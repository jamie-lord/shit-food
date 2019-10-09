using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GoogleApi.Entities.Places.Search.Find.Request;
using GoogleApi.Entities.Places.Search.Find.Response;
using GoogleApi;
using GoogleApi.Entities.Places.Search.NearBy.Request;
using GoogleApi.Entities.Places.Search.Common.Enums;
using GoogleApi.Entities.Places.Search.NearBy.Request.Enums;
using GoogleApi.Entities.Places.Search.NearBy.Response;
using Mapster;
using ShitFood.Api.Ptos;
using System.Collections.Generic;

namespace ShitFood.Api
{
    public class UpdateGooglePlacesFunction
    {
        private static string _googleApiKey = Environment.GetEnvironmentVariable("GoogleApiKeyFromKeyVault");
        private readonly ShitFoodContext _context;

        public UpdateGooglePlacesFunction(ShitFoodContext context)
        {
            _context = context;
            TypeAdapterConfig<NearByResult, GooglePlacesPto>
                .NewConfig()
                .Map(dest => dest.Id, src => src.PlaceId)
                .Map(dest => dest.Latitude, src => src.Geometry.Location.Latitude)
                .Map(dest => dest.Longitude, src => src.Geometry.Location.Longitude);
        }

        [FunctionName("UpdateGooglePlaces")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            string lat = req.Query["lat"];
            string lng = req.Query["lng"];

            if (string.IsNullOrWhiteSpace(lat) || string.IsNullOrWhiteSpace(lng))
            {
                return new NotFoundResult();
            }

            var results = await GetNearByPlaces(double.Parse(lat), double.Parse(lng));

            return new OkResult();
        }

        private async Task<List<GooglePlacesPto>> GetNearByPlaces(double lat, double lng, string pageToken = null)
        {
            var nearByRequest = new PlacesNearBySearchRequest()
            {
                Key = _googleApiKey,
                Location = new Location
                {
                    Latitude = lat,
                    Longitude = lng
                },
                Type = SearchPlaceType.Restaurant,
                Rankby = Ranking.Distance
            };

            if (!string.IsNullOrWhiteSpace(pageToken))
            {
                nearByRequest.PageToken = pageToken;
            }

            try
            {
                PlacesNearbySearchResponse nearByResponse = await GooglePlaces.NearBySearch.QueryAsync(nearByRequest);

                var results = nearByResponse.Results.Adapt<List<GooglePlacesPto>>();

                if (!string.IsNullOrWhiteSpace(nearByResponse.NextPageToken))
                {
                    await Task.Delay(5000);
                    results.AddRange(await GetNearByPlaces(lat, lng, nearByResponse.NextPageToken));
                }

                results.RemoveAll(x => x.Rating >= 3 || x.Rating == 0);

                return results;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
