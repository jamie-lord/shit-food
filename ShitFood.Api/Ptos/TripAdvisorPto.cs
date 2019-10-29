using Newtonsoft.Json;
using ShitFood.Api.TripAdvisor;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShitFood.Api.Ptos
{
    [Table("TripAdvisor")]
    public class TripAdvisorPto : DataSourceBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LocationId { get; set; }

        public Guid? PlaceId { get; set; }

        public PlacePto Place { get; set; }

        public double Rating { get; set; }

        // Object from search results page
        public string Summary { get; set; }

        // Object from single page
        public string Details { get; set; }

        [NotMapped]
        public Summary SummaryObject
        {
            get
            {
                return JsonConvert.DeserializeObject<Summary>(Summary);
            }

            set
            {
                Summary = JsonConvert.SerializeObject(value);
            }
        }

        [NotMapped]
        public Details DetailsObject
        {
            get
            {
                return JsonConvert.DeserializeObject<Details>(Details);
            }

            set
            {
                Details = JsonConvert.SerializeObject(value);
            }
        }
    }
}
