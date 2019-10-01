using System;
using System.Collections.Generic;

namespace ShitFood.Api.FoodHygieneApi
{
    public class Scores
    {
        public int? Hygiene { get; set; }
        public int? Structural { get; set; }
        public int? ConfidenceInManagement { get; set; }
    }

    public class Geocode
    {
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class Establishment
    {
        public int FHRSID { get; set; }
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
        public Scores scores { get; set; }
        public string SchemeType { get; set; }
        public Geocode geocode { get; set; }
        public string RightToReply { get; set; }
        public double Distance { get; set; }
        public bool NewRatingPending { get; set; }
        public List<Link> links { get; set; }
    }

    public class EstablishmentsArray
    {
        public Establishment[] Establishments { get; set; }
    }
}
