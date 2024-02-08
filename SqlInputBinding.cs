using AzureFunction_RepRepair.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;

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

//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.Functions.Worker;
//using Microsoft.Azure.Functions.Worker.Http;
//using Microsoft.Extensions.Logging;
//using System.Threading.Tasks;
//using AzureFunction_RepRepair.Models;
//using Microsoft.Data.SqlClient;

//public static class SqlInputBinding
//{
//    [Function("GetObjectByQRCode")]
//    public static async Task<IActionResult> Run(
//        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getobjectinfo/qrcode/{QRCode}")] HttpRequestData req,
//        string QRCode,
//        FunctionContext executionContext)
//    {
//        var logger = executionContext.GetLogger("GetObjectByQRCode");

//        if (!Guid.TryParse(QRCode, out Guid qrCodeGuid))
//        {
//            logger.LogInformation("Invalid QR Code format.");
//            return new BadRequestResult();
//        }

//        string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
//        Inventory inventoryItem = null;

//        using (var connection = new SqlConnection(connectionString))
//        {
//            var command = new SqlCommand("GetObjectByQRCode", connection)
//            {
//                CommandType = CommandType.StoredProcedure
//            };
//            command.Parameters.Add(new SqlParameter("@QRCode", qrCodeGuid));

//            await connection.OpenAsync();
//            using (var reader = await command.ExecuteReaderAsync())
//            {
//                if (reader.Read())
//                {
//                    inventoryItem = new Inventory
//                    {
//                        //I get casting error here! If deciding to use guid, work out this part
//                        QRCode = reader.GetGuid(0), 
//                        Name = reader.IsDBNull(1) ? null : reader.GetString(1),
//                        Location = reader.IsDBNull(2) ? null : reader.GetString(2)
//                    };
//                }
//            }
//        }

//        if (inventoryItem == null)
//        {
//            return new NotFoundResult();
//        }

//        return new OkObjectResult(inventoryItem);
//    }
//}