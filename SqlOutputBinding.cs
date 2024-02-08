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
                return null;
            }
            return new OutputType()
            {
                ReportInfo = reportData,
                HttpResponse = req.CreateResponse(System.Net.HttpStatusCode.Created)
            };
        }
    }

    public class OutputType
    {
        [SqlOutput("dbo.ReportDetails", connectionStringSetting: "ConnectionString")]
        public ReportInfo ReportInfo { get; set; }
        public HttpResponseData HttpResponse { get; set; }
    }

    public class ReportInfo
    {
        public int ReportDetailsId { get; set; }
        public int ObjectId { get; set; }
        public string? OriginalFaultReport { get; set; }
        public string? TranslatedFaultReport { get; set; }
        //public DateTime ReportDate { get; set; }
        public string? SelectedLanguage { get; set; }
        public string? TypeOfReport { get; set; } //or public list<TypeOfReport> typeOfReport {get; set} = new();
    }

}
