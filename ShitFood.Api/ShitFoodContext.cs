using Microsoft.EntityFrameworkCore;
using ShitFood.Api.Ptos;

namespace ShitFood.Api
{
    public class ShitFoodContext : DbContext
    {
        public ShitFoodContext(DbContextOptions<ShitFoodContext> options)
            : base(options)
        { }

        public DbSet<PlacePto> Places { get; set; }

        public DbSet<FoodHygieneRatingPto> FoodHygieneRatings { get; set; }
    }
}
