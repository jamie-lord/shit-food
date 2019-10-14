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
using NetTopologySuite.Geometries;
using GeoCoordinatePortable;
using System.Linq;

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
            double lat = double.Parse(req.Query["lat"]);
            double lng = double.Parse(req.Query["lng"]);

            var origin = new GeoCoordinate(lat, lng);
            var jump = 0.0025;

            // +1,-1 +1,0 +1,+1
            //  0,-1  oo   0,+1
            // -1,-1 -1,0 -1,+1

            var surroundingArea = new List<GeoCoordinate>
            {
                new GeoCoordinate(origin.Latitude + jump, origin.Longitude - jump),
                new GeoCoordinate(origin.Latitude + jump, origin.Longitude),
                new GeoCoordinate(origin.Latitude + jump, origin.Longitude + jump),
                new GeoCoordinate(origin.Latitude, origin.Longitude - jump),
                origin,
                new GeoCoordinate(origin.Latitude, origin.Longitude + jump),
                new GeoCoordinate(origin.Latitude - jump, origin.Longitude - jump),
                new GeoCoordinate(origin.Latitude - jump, origin.Longitude),
                new GeoCoordinate(origin.Latitude - jump, origin.Longitude + jump)
            };

            List<GooglePlacesPto> results = new List<GooglePlacesPto>();

            foreach (GeoCoordinate pos in surroundingArea)
            {
                List<GooglePlacesPto> places = await GetNearByPlaces(pos.Latitude, pos.Longitude);
                foreach (GooglePlacesPto place in places)
                {
                    if (!results.Any(x => x.Id == place.Id))
                    {
                        results.Add(place);
                    }
                }
            }

            var shit = results.Where(x => x.Rating < 3.5);

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
                    await Task.Delay(2000);
                    results.AddRange(await GetNearByPlaces(lat, lng, nearByResponse.NextPageToken));
                }

                return results;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
