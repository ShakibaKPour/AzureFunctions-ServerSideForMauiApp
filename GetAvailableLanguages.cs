using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using AzureFunction_RepRepair.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AzureFunction_RepRepair
{
    public class GetAvailableLanguages
    {
        [Function("GetAvailableLanguages")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAvailableLanguages")] HttpRequestData req,
            [SqlInput("SELECT * FROM [Scan].[Languages]",
            "ConnectionString")] IEnumerable<Languages> languages)
        {
            var result = JsonConvert.SerializeObject(languages);
            if (result == null || !result.Any())
            {
                return new NoContentResult();
            }
            return new OkObjectResult(result);

        }
    }
}
