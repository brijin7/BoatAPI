using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FA_ConfigurationType
    {
        public string QueryType { get; set; }
        public string TypeId { get; set; }
        public string ConfigurationName { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
    }
    public class FA_ConfigurationTypeRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class FA_ConfigurationTypeList
    {
        public List<FA_ConfigurationType> Response { get; set; }
        public int StatusCode { get; set; }
    }
}