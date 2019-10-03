using System;

namespace ShitFood.Api
{
    public class ShitDto
    {
        public Guid Id { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public string Name { get; set; }

        public string FoodHygieneRating { get; set; }

        public string GooglePlacesId { get; set; }
    }
}
