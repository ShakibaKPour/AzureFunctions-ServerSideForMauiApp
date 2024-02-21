using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunction_RepRepair.Models
{
    public class ReportType
    {
        public Guid ID { get; set; }
        public string? TypeOfReport{ get; set; }
    }
}
