using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunction_RepRepair.Models;
using Microsoft.AspNetCore.Mvc;

namespace AzureFunction_RepRepair
{
    public static class SqlOutputBindingFunction
    {

        [Function("InsertReportInfo")]
        public static async Task<OutputType> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("PostFunction");
            logger.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ReportInfo? reportData = JsonConvert.DeserializeObject<ReportInfo>(requestBody);
            if (reportData == null)
            {
                return new OutputType() { ReportInfo = null, HttpResponse = req.CreateResponse(System.Net.HttpStatusCode.BadRequest) };
            }
            reportData.ID = Guid.NewGuid();
            return new OutputType()
            {
                ReportInfo = reportData,
                HttpResponse = req.CreateResponse(System.Net.HttpStatusCode.Created)
            };
        }

        public class OutputType
        {
            [SqlOutput("Scan.InventoryMalfunctionReports", connectionStringSetting: "ConnectionString")]
            public ReportInfo ReportInfo { get; set; }
            public HttpResponseData HttpResponse { get; set; }
        }
    }
}
