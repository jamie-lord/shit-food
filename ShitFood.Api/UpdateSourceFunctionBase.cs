using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ShitFood.Api.Ptos;
using System.Linq;
using System.Threading.Tasks;

namespace ShitFood.Api
{
    public abstract class UpdateSourceFunctionBase
    {
        private readonly IDistributedCache _cache;

        protected readonly ShitFoodContext Context;

        public UpdateSourceFunctionBase(ShitFoodContext context, IDistributedCache cache)
        {
            Context = context;
            _cache = cache;
        }

        protected PlacePto FindExistingPlace(ILogger log, double lat, double lng, string name)
        {
            double locationMargin = 0.0001;
            var results = Context.Places.Where(x => x.Location.X > lng - locationMargin && x.Location.X < lng + locationMargin && x.Location.Y > lat - locationMargin && x.Location.Y < lat + locationMargin && x.Name.ToLower() == name.ToLower());
            if (results.Count() == 1)
            {
                log.LogInformation($"Found one existing place match for {name} at {lat},{lng}");
                return results.First();
            }
            else if (results.Count() > 1)
            {
                log.LogWarning($"Found {results.Count()} place matches for {name} at {lat},{lng}");
                return results.First();
            }
            log.LogInformation($"Found no existing places for {name} at {lat},{lng}");
            return null;
        }

        protected async Task RemoveCachedPlace(ILogger log, string id)
        {
            log.LogInformation($"Removing {id} from cache");
            await _cache.RemoveAsync(id);
        }
    }
}
