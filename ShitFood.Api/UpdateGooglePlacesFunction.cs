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
                .Ignore(x => x.PlaceId)
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

            List<NearByResult> results = new List<NearByResult>();

            try
            {
                foreach (GeoCoordinate pos in surroundingArea)
                {
                    List<NearByResult> places = await GetNearByPlaces(pos.Latitude, pos.Longitude);
                    foreach (NearByResult place in places)
                    {
                        if (!results.Any(x => x.PlaceId == place.PlaceId))
                        {
                            results.Add(place);
                        }
                    }
                }

                foreach (NearByResult nearByResult in results)
                {
                    GooglePlacesPto googlePlacesPto = _context.GooglePlaces.Find(nearByResult.PlaceId);
                    if (googlePlacesPto != null)
                    {
                        // update
                        log.LogInformation($"Updating Google Places {googlePlacesPto.Id}");
                        nearByResult.Adapt(googlePlacesPto);
                        _context.GooglePlaces.Update(googlePlacesPto);
                    }
                    else
                    {
                        // insert
                        log.LogInformation($"Inserting new Google Places {nearByResult.PlaceId}");
                        googlePlacesPto = new GooglePlacesPto();
                        nearByResult.Adapt(googlePlacesPto);
                        PlacePto placePto = await FindExistingPlace(log, lat, lng, googlePlacesPto.Name);
                        if (placePto == null)
                        {
                            placePto = new PlacePto
                            {
                                Location = new Point(googlePlacesPto.Longitude, googlePlacesPto.Latitude)
                                {
                                    SRID = 4326
                                },
                                Name = googlePlacesPto.Name,
                                GooglePlaces = googlePlacesPto
                            };
                            _context.Places.Add(placePto);
                        }
                        googlePlacesPto.Place = placePto;
                        _context.GooglePlaces.Add(googlePlacesPto);
                    }
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return new OkResult();
        }

        private async Task<PlacePto> FindExistingPlace(ILogger log, double lat, double lng, string name)
        {
            double locationMargin = 0.0001;
            var results = _context.Places.Where(x => x.Location.X > lng - locationMargin && x.Location.X < lng + locationMargin && x.Location.Y > lat - locationMargin && x.Location.Y < lat + locationMargin && x.Name.ToLower() == name.ToLower());
            if (results.Count() == 1)
            {
                log.LogInformation($"Found one existing place match for {name} at {lat},{lng}");
                return results.First();
            }
            else if (results.Count() > 1)
            {
                log.LogWarning($"Found {results.Count()} place matches for {name} at {lat},{lng}");
                return results.First();
            }
            log.LogInformation($"Found no existing places for {name} at {lat},{lng}");
            return null;
        }

        private async Task<List<NearByResult>> GetNearByPlaces(double lat, double lng, string pageToken = null)
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

                List<NearByResult> results = nearByResponse.Results.ToList();

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
