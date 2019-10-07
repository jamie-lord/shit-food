using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

[assembly: FunctionsStartup(typeof(ShitFood.Api.Startup))]
namespace ShitFood.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string SqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
            builder.Services.AddDbContext<ShitFoodContext>(options => options.UseSqlServer(SqlConnection, x => x.UseNetTopologySuite()));
        }
    }
}
