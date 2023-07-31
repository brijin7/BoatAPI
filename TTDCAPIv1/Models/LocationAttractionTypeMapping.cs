using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class LocationAttractionTypeMapping
    {
        public string QueryType { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int AttractionId { get; set; }

        public string AttractionTypeName { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
    }

    public class LocationAttractionTypeMappingRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class LocationAttractionTypeMappingList
    {
        public List<LocationAttractionTypeMapping> Response { get; set; }
        public int StatusCode { get; set; }
    }
}