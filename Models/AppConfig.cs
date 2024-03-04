﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunction_RepRepair.Models
{
    public class AppConfig
    {
        public string serviceRegion { get; set; }
        public string translateAPIEndpoint { get; set; }
        public string postReportFunctionUrl { get; set; }
        public string getLanguagesUrl { get; set; }
        public string getReportTypesUrl { get; set; }
        public string getQRCodeFunctionUrl { get; set; }
    }
}
