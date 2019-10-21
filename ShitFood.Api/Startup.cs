using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

[assembly: FunctionsStartup(typeof(ShitFood.Api.Startup))]
namespace ShitFood.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IDistributedCache>(new AzureTableStorageCache(Environment.GetEnvironmentVariable("TableStorageCacheConnectionString"), "PlaceDtos", "Places"));
            string SqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
            builder.Services.AddDbContext<ShitFoodContext>(options => options.UseSqlServer(SqlConnection, x => x.UseNetTopologySuite()));
        }
    }
}
