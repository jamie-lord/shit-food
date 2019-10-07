using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace ShitFood.Api
{
    public class ShitFoodContextFactory : IDesignTimeDbContextFactory<ShitFoodContext>
    {
        public ShitFoodContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShitFoodContext>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString"), x => x.UseNetTopologySuite());

            return new ShitFoodContext(optionsBuilder.Options);
        }
    }
}
