using AzureFunction_RepRepair.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;

namespace AzureFunction_RepRepair
{
    public class WriteToReportAndLinkTables
    {

        [Function("InsertToReportAndLinkTables")]
        public static async Task<HttpResponseData> Run(
       [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestData req,
       FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Insert to malfunctionReport and ReportKeywordLink");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ReportInfo? reportData = JsonConvert.DeserializeObject<ReportInfo>(requestBody);
            if (reportData == null)
            {
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }
            reportData.ID = Guid.NewGuid();
            var connectionStringSetting = Environment.GetEnvironmentVariable("ConnectionString");
            try
            {
                using (var connection = new SqlConnection(connectionStringSetting))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand("Scan.InsertReportAndKeywords", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@ReportID", reportData.ID);
                    command.Parameters.AddWithValue("@QrCode", reportData.QrCode);
                    command.Parameters.AddWithValue("@OriginalFaultReport", reportData.OriginalFaultReport);
                    command.Parameters.AddWithValue("@TranslatedFaultReport", reportData.TranslatedFaultReport);
                    command.Parameters.AddWithValue("@SelectedLanguage", reportData.SelectedLanguage);
                    command.Parameters.AddWithValue("@TypeOfReport", reportData.TypeOfReport);
                    await command.ExecuteNonQueryAsync();

                }
                return req.CreateResponse(System.Net.HttpStatusCode.Created);

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception thrown: {ex.Message}");
                return req.CreateResponse(System.Net.HttpStatusCode.InternalServerError);

            }

        }

    }
}
