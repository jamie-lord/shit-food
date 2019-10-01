using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using ShitFood.Api.FoodHygieneApi;

namespace ShitFood.Api
{
    public static class GetShit
    {
        [FunctionName("GetShit")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            string lat = req.Query["lat"];
            string lon = req.Query["lon"];

            var establishments = await GetBadFoodHygieneRatings(lat, lon);

            return establishments != null ? (ActionResult)new OkObjectResult(establishments) : new BadRequestObjectResult("You must pass lat and lon parameters");
        }

        private static async Task<Establishment[]> GetBadFoodHygieneRatings(string lat, string lon)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.ratings.food.gov.uk/Establishments?latitude={lat}&longitude={lon}&maxDistanceLimit=2&ratingKey=3&ratingOperatorKey=LessThanOrEqual&pageNumber=1&pageSize=250");
            Console.WriteLine(request.RequestUri);
            request.Headers.Add("x-api-version", "2");
            try
            {
                var response = await client.SendAsync(request);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<EstablishmentsArray>(await response.Content.ReadAsStringAsync())?.Establishments;
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}
