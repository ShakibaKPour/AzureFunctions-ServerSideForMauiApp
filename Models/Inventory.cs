namespace AzureFunction_RepRepair.Models;

public class Inventory
{
    public int ObjectId { get; set; }
    public string QRCode { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

}

//public class Inventory
//{
//    // public int ID { get; set; }
//    public Guid QRCode { get; set; }
//    public string Name { get; set; }
//    public string Location { get; set; }

//}
