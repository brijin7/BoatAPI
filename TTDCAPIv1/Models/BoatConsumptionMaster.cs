using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BoatConsumptionMaster
    {
        public string QueryType { get; set; }
        public string ConsumptionId { get; set; }
        public string ConsumptionDate { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatType { get; set; }
        public string BoatSeaterId { get; set; }
        public string SeaterType { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string ItemId { get; set; }
        public string ItemQtyPerTrip { get; set; }
        public string ItemDescription { get; set; }
        public string NoofItems { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
    }

    public class BoatConsumptionMasterList
    {
        public List<BoatConsumptionMaster> Response
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
    public class BoatConsumptionMasterString
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