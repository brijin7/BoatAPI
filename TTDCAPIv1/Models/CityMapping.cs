using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class CityMapping
    {
        public string QueryType { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityDescription { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
    }

    public class CityMappingRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class CityMappingList
    {
        public List<CityMapping> Response { get; set; }
        public int StatusCode { get; set; }
    }
}