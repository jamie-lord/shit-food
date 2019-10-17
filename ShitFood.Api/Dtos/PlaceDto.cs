using System;

namespace ShitFood.Api.Dtos
{
    public class PlaceDto
    {
        public Guid Id { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public string Name { get; set; }

        public string FoodHygieneRating { get; set; }

        public int? FoodHygieneRatingId { get; set; }

        public string GooglePlacesId { get; set; }

        public double? GooglePlacesRating { get; set; }

        public int? GooglePlacesRatings { get; set; }
    }
}
