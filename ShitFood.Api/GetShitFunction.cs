using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using NetTopologySuite.Geometries;
using ShitFood.Api.Ptos;
using System.Collections.Generic;
using ShitFood.Api.Dtos;

namespace ShitFood.Api
{
    public class GetShitFunction
    {
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
            int.TryParse(req.Query["distance"], out int distance);
            if (distance == 0)
            {
                distance = 5000;
            }

            var location = new Point(double.Parse(lng), double.Parse(lat))
            {
                SRID = 4326
            };

            var requestPto = new GetShitRequestPto
            {
                Requested = DateTime.Now,
                Location = location,
                Distance = distance,
                ClientIpAddress = req.HttpContext.Connection.RemoteIpAddress.ToString()
            };

            _context.GetShitRequests.Add(requestPto);
            _context.SaveChanges();

            IQueryable<PlacePto> ptos = _context.Places.Where(x => x.Location.Distance(location) <= distance &&
            (x.FoodHygieneRating.RatingValue == "0" ||
            x.FoodHygieneRating.RatingValue == "1" ||
            x.FoodHygieneRating.RatingValue == "2" ||
            (x.GooglePlaces.Rating > 0 &&
            x.GooglePlaces.Rating < 3.5)))
                .OrderBy(x => x.Location.Distance(location))
                .Take(50);

            if (ptos.Count() == 0)
            {
                return new NotFoundResult();
            }

            var places = new List<PlaceDto>();

            foreach (PlacePto pto in ptos)
            {
                var place = new PlaceDto
                {
                    Id = pto.Id,
                    Lat = pto.Location.Y,
                    Lng = pto.Location.X,
                    Name = pto.Name
                };

                FoodHygieneRatingPto foodHygieneRatingPto = _context.FoodHygieneRatings.Where(x => x.PlaceId == pto.Id).SingleOrDefault();

                if (foodHygieneRatingPto != null)
                {
                    place.FoodHygieneRating = foodHygieneRatingPto.RatingValue;
                    place.FoodHygieneRatingId = foodHygieneRatingPto.FHRSID;
                }

                GooglePlacesPto googlePlacesPto = _context.GooglePlaces.Where(x => x.PlaceId == pto.Id).SingleOrDefault();

                if (googlePlacesPto != null)
                {
                    place.GooglePlacesId = googlePlacesPto.Id;
                    place.GooglePlacesRating = googlePlacesPto.Rating;
                    place.GooglePlacesRatings = googlePlacesPto.UserRatingsTotal;
                }

                if (place.FoodHygieneRatingId != null || place.GooglePlacesId != null)
                {
                    places.Add(place);
                }
            }

            return new OkObjectResult(places);
        }
    }
}
