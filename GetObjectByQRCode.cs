using AzureFunction_RepRepair.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;

namespace AzureFunction_RepRepair
{
    public static class GetObjectByQRCode
    {
        [Function("GetObjectByQRCode")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route ="getobjectinfo/qrcode/{QRCode}")]
            HttpRequest req,
            string QrCode,
            [SqlInput(commandText:"Scan.GetInventoryByQRCode",
            commandType: System.Data.CommandType.StoredProcedure,
            parameters:"@QrCode ={QrCode}",
            connectionStringSetting:"ConnectionString")]
            IEnumerable<Inventory> objectInfo)
        {
            var result = objectInfo.FirstOrDefault();
            if (result == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }

    }
}