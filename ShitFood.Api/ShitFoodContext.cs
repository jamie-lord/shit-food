using Microsoft.EntityFrameworkCore;
using ShitFood.Api.Ptos;

namespace ShitFood.Api
{
    public class ShitFoodContext : DbContext
    {
        public ShitFoodContext(DbContextOptions<ShitFoodContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodHygieneRatingPto>().HasIndex(x => x.PlaceId);
            modelBuilder.Entity<FoodHygieneRatingPto>().HasIndex(x => x.RatingValue);

            modelBuilder.Entity<GooglePlacesPto>().HasIndex(x => x.PlaceId);
            modelBuilder.Entity<GooglePlacesPto>().HasIndex(x => x.Rating);

            modelBuilder.Entity<TripAdvisorPto>().HasIndex(x => x.PlaceId);
            modelBuilder.Entity<TripAdvisorPto>().HasIndex(x => x.Rating);
        }

        public DbSet<PlacePto> Places { get; set; }

        public DbSet<FoodHygieneRatingPto> FoodHygieneRatings { get; set; }

        public DbSet<GooglePlacesPto> GooglePlaces { get; set; }

        public DbSet<TripAdvisorPto> TripAdvisorLocations { get; set; }

        public DbSet<GetShitRequestPto> GetShitRequests { get; set; }
    }
}
