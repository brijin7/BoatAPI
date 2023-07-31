using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BoatSeatMaster
    {
        public string QueryType
        {
            get;
            set;
        }
        public string BoatTypeId
        {
            get;
            set;
        }
        public string BoatSeaterId
        {
            get;
            set;
        }
        public string SeaterType
        {
            get;
            set;
        }
        public string BoatHouseId
        {
            get;
            set;
        }
        public string BoatHouseName
        {
            get;
            set;
        }
        public string NoOfSeats
        {
            get;
            set;
        }
        public string ActiveStatus
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public string AllowedNoOfSeats
        {
            get;
            set;
        }
        public string RestrictionReason
        {
            get;
            set;
        }
        public string BoatStatusId
        {
            get;
            set;
        }
    }
    public class BoatSeatMasterList
    {
        public List<BoatSeatMaster> Response
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
    public class BoatSeatMasterString
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
