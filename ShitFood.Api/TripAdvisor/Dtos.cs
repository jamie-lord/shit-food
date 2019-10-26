using System.Collections.Generic;

namespace ShitFood.Api.TripAdvisor
{
    public class SlotOffer
    {
        public string url { get; set; }
        public string nameInCommerceTool { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
        public object id { get; set; }
        public bool bookable { get; set; }
        public bool lockable { get; set; }
        public bool racable { get; set; }
        public object pickerOptions { get; set; }
        public object disclaimerText { get; set; }
        public string headerText { get; set; }
        public object subText { get; set; }
        public string buttonText { get; set; }
        public string trackingEvent { get; set; }
        public string seeAllRestaurantsUrl { get; set; }
        public object specialOfferText { get; set; }
    }

    public class Offers
    {
        public SlotOffer slot1Offer { get; set; }
        public SlotOffer slot2Offer { get; set; }
    }

    public class ReviewSnippetsList
    {
        public string reviewText { get; set; }
        public string reviewUrl { get; set; }
    }

    public class ReviewSnippets
    {
        public List<ReviewSnippetsList> reviewSnippetsList { get; set; }
    }

    public class AwardInfo
    {
        public string icon { get; set; }
        public string awardText { get; set; }
        public string yearsText { get; set; }
        public bool isTravelersChoice { get; set; }
    }

    public class Restaurant
    {
        public string detailPageUrl { get; set; }
        public string heroImgUrl { get; set; }
        public int heroImgRawHeight { get; set; }
        public int heroImgRawWidth { get; set; }
        public string squareImgUrl { get; set; }
        public int squareImgRawLength { get; set; }
        public int locationId { get; set; }
        public string name { get; set; }
        public double averageRating { get; set; }
        public int userReviewCount { get; set; }
        public string currentOpenStatusCategory { get; set; }
        public string currentOpenStatusText { get; set; }
        public List<string> establishmentTypeAndCuisineTags { get; set; }
        public string priceTag { get; set; }
        public Offers offers { get; set; }
        public bool hasMenu { get; set; }
        public string menuUrl { get; set; }
        public bool isDifferentGeo { get; set; }
        public string parentGeoName { get; set; }
        public string distanceTo { get; set; }
        public ReviewSnippets reviewSnippets { get; set; }
        public AwardInfo awardInfo { get; set; }
        public string warUrl { get; set; }
        public bool isLocalChefItem { get; set; }
    }

    public class Links
    {
        public string warUrl { get; set; }
        public string addPhotoUrl { get; set; }
        public string ownerAddPhotoUrl { get; set; }
    }

    public class Location
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string directionsUrl { get; set; }
        public string landmark { get; set; }
        public object neighborhood { get; set; }
    }

    public class Contact
    {
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
    }

    public class Ranking
    {
        public int rank { get; set; }
        public int totalCount { get; set; }
        public string category { get; set; }
        public string geo { get; set; }
        public string url { get; set; }
    }

    public class RatingQuestion
    {
        public string name { get; set; }
        public int rating { get; set; }
        public string icon { get; set; }
    }

    public class Rating
    {
        public Ranking primaryRanking { get; set; }
        public Ranking secondaryRanking { get; set; }
        public double primaryRating { get; set; }
        public int reviewCount { get; set; }
        public List<RatingQuestion> ratingQuestions { get; set; }
    }

    public class Award
    {
        public string icon { get; set; }
        public string awardText { get; set; }
        public string yearsText { get; set; }
        public bool isTravelersChoice { get; set; }
    }

    public class Tags
    {
        public object reviewSnippetSections { get; set; }
    }

    public class Tag
    {
        public int tagId { get; set; }
        public string tagValue { get; set; }
    }

    public class PriceRange
    {
        public int tagCategoryId { get; set; }
        public List<Tag> tags { get; set; }
    }

    public class Cuisines
    {
        public int tagCategoryId { get; set; }
        public List<Tag> tags { get; set; }
    }

    public class DietaryRestrictions
    {
        public int tagCategoryId { get; set; }
        public List<Tag> tags { get; set; }
    }

    public class Meals
    {
        public int tagCategoryId { get; set; }
        public List<Tag> tags { get; set; }
    }

    public class Features
    {
        public int tagCategoryId { get; set; }
        public List<Tag> tags { get; set; }
    }

    public class EstablishmentType
    {
        public int tagCategoryId { get; set; }
        public List<Tag> tags { get; set; }
    }

    public class TagTexts
    {
        public PriceRange priceRange { get; set; }
        public Cuisines cuisines { get; set; }
        public DietaryRestrictions dietaryRestrictions { get; set; }
        public Meals meals { get; set; }
        public Features features { get; set; }
        public EstablishmentType establishmentType { get; set; }
    }

    public class DetailCard
    {
        public TagTexts tagTexts { get; set; }
        public string numericalPrice { get; set; }
        public string improveListingUrl { get; set; }
        public string updateListingUrl { get; set; }
    }

    public class RestaurantDetail
    {
        public string name { get; set; }
        public int detailId { get; set; }
        public string geo { get; set; }
        public int geoId { get; set; }
        public bool isOwner { get; set; }
        public Links links { get; set; }
        public Location location { get; set; }
        public Contact contact { get; set; }
        public Rating rating { get; set; }
        public Award award { get; set; }
        public Tags tags { get; set; }
        public DetailCard detailCard { get; set; }
    }
}
