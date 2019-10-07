using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShitFood.Api.Ptos
{
    [Table("Place")]
    public class PlacePto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Point Location { get; set; }

        public FoodHygieneRatingPto FoodHygieneRating { get; set; }
    }
}
