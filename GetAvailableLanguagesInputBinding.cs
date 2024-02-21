using System;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using AzureFunction_RepRepair.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AzureFunction_RepRepair
{
    public class GetAvailableLanguagesInputBinding
    {
        [Function("GetAvailableLanguagesInputBinding")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetAvailableLanguagesInputBinding")] HttpRequestData req,
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
