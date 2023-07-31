using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class OtherServicesDetails
    {
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ShortName { get; set; }
        public string ServiceTotalAmount { get; set; }
        public string AdultCharge { get; set; }
        public string ChargePerItemTax { get; set; }
        public string TaxID { get; set; }
        public string TaxName { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }

        public string Category { get; set; }
        public string CategoryName { get; set; }
        //public string SGST { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }

        public string AvailableQty { get; set; }

        public string StockEntryMaintenance { get; set; }       

            
    }

    public class OtherServicesDetailslist
    {
        public List<OtherServicesDetails> Response { get; set; }
        public int StatusCode { get; set; }
    }
}