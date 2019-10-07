using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShitFood.Api.Ptos
{
    [Table("Place")]
    public class PlacePto
    {
        public Guid Id { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public string Name { get; set; }

        public FoodHygieneRatingPto FoodHygieneRating { get; set; }
    }
}
