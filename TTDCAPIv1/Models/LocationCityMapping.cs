using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class LocationCityMapping
    {
        public string QueryType { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int Distance { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
    }

    public class LocationCityMappingRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class LocationCityMappingList
    {
        public List<LocationCityMapping> Response { get; set; }
        public int StatusCode { get; set; }
    }
}