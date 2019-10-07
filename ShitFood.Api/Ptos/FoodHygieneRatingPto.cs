using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShitFood.Api.Ptos
{
    [Table("FoodHygieneRating")]
    public class FoodHygieneRatingPto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FHRSID { get; set; }
        public Guid? PlaceId { get; set; }
        public PlacePto Place { get; set; }
        public string LocalAuthorityBusinessID { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public int BusinessTypeID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public string RatingValue { get; set; }
        public string RatingKey { get; set; }
        public DateTime RatingDate { get; set; }
        public string LocalAuthorityCode { get; set; }
        public string LocalAuthorityName { get; set; }
        public string LocalAuthorityWebSite { get; set; }
        public string LocalAuthorityEmailAddress { get; set; }
        public int? Hygiene { get; set; }
        public int? Structural { get; set; }
        public int? ConfidenceInManagement { get; set; }
        public string SchemeType { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string RightToReply { get; set; }
        public double Distance { get; set; }
        public bool NewRatingPending { get; set; }
        public string Link { get; set; }
    }
}
