using AzureFunction_RepRepair.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;

namespace AzureFunction_RepRepair
{
    public static class SqlInputBinding
    {
        [Function("GetObjectInfoByQRCode")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route ="getobjectinfo/qrcode/{QRCode}")]
            HttpRequest req,
            string QRCode,
            [SqlInput(commandText:"GetObjectInfoByQRCode",
            commandType: System.Data.CommandType.StoredProcedure,
            parameters:"@QRCode ={QRCode}",
            connectionStringSetting:"ConnectionString")]
            IEnumerable<Inventory> objectInfo)
        {
            var result = objectInfo.FirstOrDefault();
            if (objectInfo == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }
    }
}
