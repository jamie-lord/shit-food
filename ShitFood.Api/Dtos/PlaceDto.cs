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

        public string FoodHygieneRatingUri { get; set; }
    }
}
