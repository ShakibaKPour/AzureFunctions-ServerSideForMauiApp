using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunction_RepRepair.Models
{
    public class Inventory
    {
        public int ID { get; set; }
        public string QRCode { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

    }
}
