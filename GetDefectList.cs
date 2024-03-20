using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.AspNetCore.Mvc;
using AzureFunction_RepRepair.Models;
using Newtonsoft.Json;

namespace AzureFunction_RepRepair
{
    public static class GetDefectList
    {
        [Function("GetDefectList")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetDefectList")] HttpRequestData req,
            [SqlInput("SELECT * FROM [Scan].[DefectList]",
            "ConnectionString")] IEnumerable<DefectList> defects)
        {
            var result = JsonConvert.SerializeObject(defects);
            if (result == null || !result.Any())
            {
                return new NoContentResult();
            }
            return new OkObjectResult(result);
        }
    }
}
