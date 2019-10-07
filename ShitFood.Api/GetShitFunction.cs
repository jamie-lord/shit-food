using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using ShitFood.Api.FoodHygieneApi;
using GoogleApi;
using GoogleApi.Entities.Places.Search.Find.Request;
using GoogleApi.Entities.Places.Search.Find.Response;
using System.Linq;
using System.Collections.Generic;

namespace ShitFood.Api
{
    public class GetShitFunction
    {
        private static string _googleApiKey = Environment.GetEnvironmentVariable("GoogleApiKeyFromKeyVault");
        private readonly ShitFoodContext _context;

        public GetShitFunction(ShitFoodContext context)
        {
            _context = context;
        }

        [FunctionName("GetShit")]
        public async Task<IActionResult> Get([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            string lat = req.Query["lat"];
            string lng = req.Query["lng"];

            //Establishment[] establishments = await GetBadFoodHygieneRatings(lat, lng);

            //var results = new List<ShitDto>();

            //foreach (Establishment establishment in establishments)
            //{
            //    var result = new ShitDto
            //    {
            //        Id = Guid.NewGuid(),
            //        Lat = double.Parse(establishment.geocode.latitude),
            //        Lng = double.Parse(establishment.geocode.longitude),
            //        Name = establishment.BusinessName,
            //        FoodHygieneRating = establishment.RatingValue
            //    };

            //    //var findPlaceRequest = new PlacesFindSearchRequest()
            //    //{
            //    //    Key = _googleApiKey,
            //    //    Input = establishment.BusinessName,
            //    //    Type = GoogleApi.Entities.Places.Search.Find.Request.Enums.InputType.TextQuery,
            //    //    Location = new GoogleApi.Entities.Common.Location
            //    //    {
            //    //        Latitude = double.Parse(establishment.geocode.latitude),
            //    //        Longitude = double.Parse(establishment.geocode.longitude)
            //    //    },
            //    //    Radius = 1
            //    //};
            //    //PlacesFindSearchResponse findPlaceResponse = await GooglePlaces.FindSearch.QueryAsync(findPlaceRequest);
            //    //if (findPlaceResponse.Status == GoogleApi.Entities.Common.Enums.Status.Ok && findPlaceResponse.Candidates.Count() == 1)
            //    //{
            //    //    result.GooglePlacesId = findPlaceResponse.Candidates.First().PlaceId;
            //    //}
            //    //else
            //    //{
            //    //    log.LogDebug(establishment.FHRSID + " " + findPlaceResponse.Status);
            //    //}

            //    results.Add(result);
            //}
            return new OkResult();

            //return establishments != null ? (ActionResult)new OkObjectResult(results) : new BadRequestObjectResult("You must pass lat and lng parameters");
        }

        
    }
}
