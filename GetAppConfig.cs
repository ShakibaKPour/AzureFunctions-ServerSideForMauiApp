using AzureFunction_RepRepair.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunction_RepRepair
{
    public class GetAppConfig
    {
        private readonly ILogger<GetAppConfig> _logger;

        public GetAppConfig(ILogger<GetAppConfig> logger)
        {
            _logger = logger;
        }

        [Function("GetAppConfig")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var region = Environment.GetEnvironmentVariable("Region");
            var translateEndpoint = Environment.GetEnvironmentVariable("TranslateEndpoint");
            var getQRCodeFunction = Environment.GetEnvironmentVariable("GetQRCodeUrl");
            var postReportFunction = Environment.GetEnvironmentVariable("PostReportUrl");
            var getLanguages = Environment.GetEnvironmentVariable("GetLanguages");
            var getReportTypes = Environment.GetEnvironmentVariable("GetReportTypeUrl");



            var config = new AppConfig {serviceRegion = region, 
                translateAPIEndpoint = translateEndpoint,
                postReportFunctionUrl = postReportFunction,
                getLanguagesUrl = getLanguages,
                getReportTypesUrl = getReportTypes,
                getQRCodeFunctionUrl = getQRCodeFunction,
                };
            return new OkObjectResult(config);
        }
    }
}
