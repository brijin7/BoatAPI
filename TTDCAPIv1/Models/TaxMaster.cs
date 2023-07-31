using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class TaxMaster
    {

        public string QueryType { get; set; }
        public string TaxId { get; set; }
        public string UniqueId { get; set; }
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string TaxName { get; set; }
        public string TaxDescription { get; set; }
        public string TaxPercentage { get; set; }
        public string EffectiveFrom { get; set; }
        public string EffectiveTill { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
        public string ValidDate { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }

        public string RefNum { get; set; }
        public string RefDate { get; set; }
        public string RefDocLink { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
    }

    public class TaxMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class TaxMasterList
    {
        public List<TaxMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }
}