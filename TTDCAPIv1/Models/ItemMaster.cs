using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class ItemMaster
    {
        public string QueryType { get; set; }
        public string ItemId { get; set; }
        public string ItemDescription { get; set; }
        public string ItemType { get; set; }

        public string EntityFlag { get; set; }
        public string EntityId { get; set; }

        public string EntityName { get; set; }
        public string UOM { get; set; }
        public string ItemRate { get; set; }
        public string OpeningQty { get; set; }
        public string CreatedBy { get; set; }

        public string UOMName { get; set; }
        public string ItemName { get; set; }
        public string ActiveStatus { get; set; }




    }
    public class ItemMasterList
    {
        public List<ItemMaster> Response
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
    public class ItemMasterString
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