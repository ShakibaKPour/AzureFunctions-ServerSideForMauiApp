using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.AspNetCore.Mvc;
using AzureFunction_RepRepair.Models;
using Newtonsoft.Json;

namespace AzureFunction_RepRepair
{
    public static class GetReportTypes
    {
        
        [Function("GetReportTypes")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetReportTypes")] HttpRequestData req,
            [SqlInput("SELECT * FROM [Scan].[ReportTypes]",
            "ConnectionString")]
            IEnumerable<ReportType> reports)
        {

            var result = JsonConvert.SerializeObject(reports);
            if (result == null || !result.Any())
            {
                return new NoContentResult();
            }
            return new OkObjectResult(result);
        }
    }
}
