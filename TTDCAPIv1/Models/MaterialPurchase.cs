using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class MaterialPurchase
    {
        public string QueryType { get; set; }
        public string PurchaseId { get; set; }
        public string ItemId { get; set; }
        public string PurchaseDate { get; set; }
        public string BoatHouseId { get; set; }
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public string VendorRef { get; set; }
        public string ReceivedQty { get; set; }
        public string AcceptedQty { get; set; }
        public string RejectedQty { get; set; }
        public string RejectionReason { get; set; }
        public string PurchaseRate { get; set; }
        public string Createdby { get; set; }
        public string ItemDescription { get; set; }
        public string NoOfItems { get; set; }
        public string ActiveStatus { get; set; }

    }
    public class MaterialPurchaseList
    {
        public List<MaterialPurchase> Response
        {

            get;
            set;
        }
        public int StatusCode
        {
            get;
            set;
        }
    }
    public class MaterialPurchaseString
    {
        public string Response
        {
            get;
            set;
        }
        public int StatusCode
        {
            get;
            set;
        }
    }
}