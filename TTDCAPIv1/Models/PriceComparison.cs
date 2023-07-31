using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class PriceComparison
    {
        public string QueryType { get; set; }
        
        public string CorpId { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }

        public string Total { get; set; }

        public string Input1 { get; set; }
        public string Input2 { get; set; }
        public string Input3 { get; set; }

        public string Normal { get; set; }
        public string Premium { get; set; }
    }

    public class PriceComparisonRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class PriceComparisonList
    {
        public List<PriceComparison> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class PriceComparisonAttributes
    {
        public string BoatType { get; set; }

        public string BoatTypeId { get; set; }

        public string SeaterType { get; set; }

        public string BoatSeaterId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryId { get; set; }

        public string ServiceName { get; set; }

        public string ServiceId { get; set; }

      
    }

    public class PriceComparisonAttributesRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class PriceComparisonAttributesList
    {
        public List<PriceComparisonAttributes> Response { get; set; }
        public int StatusCode { get; set; }
    }
}