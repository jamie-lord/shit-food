using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShitFood.Api.Ptos
{
    [Table("GooglePlaces")]
    public class GooglePlacesPto : DataSourceBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public Guid? PlaceId { get; set; }

        public PlacePto Place { get; set; }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Rating { get; set; }

        public int UserRatingsTotal { get; set; }

        public bool PermanentlyClosed { get; set; }

        public PriceLevel? PriceLevel { get; set; }
    }

    public enum PriceLevel
    {
        Free = 0,
        Inexpensive = 1,
        Moderate = 2,
        Expensive = 3,
        VeryExpensive = 4
    }
}
