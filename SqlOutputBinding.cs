using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using AzureFunction_RepRepair.Models;
using System.Diagnostics.Eventing.Reader;

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
            //reportData.ReportId= Guid.NewGuid();
            if (reportData == null)
            {
                return new OutputType() { ReportInfo = null, HttpResponse= req.CreateResponse(System.Net.HttpStatusCode.BadRequest)};
            }
                return new OutputType()
                {
                    ReportInfo = reportData,
                    HttpResponse = req.CreateResponse(System.Net.HttpStatusCode.Created)
                };
        }

        public class OutputType
        {
            [SqlOutput("dbo.ReportDetails", connectionStringSetting: "ConnectionString")]
            public ReportInfo ReportInfo { get; set; }
            public HttpResponseData HttpResponse { get; set; }
        }
    }
}
