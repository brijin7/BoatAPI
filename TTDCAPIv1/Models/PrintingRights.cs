using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class PrintingRights
    {
        public string QueryType { get; set; }
        public string UniqueId { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string OtherService { get; set; }
        public string Restaurant { get; set; }
        public string AdditionalTicket { get; set; }
        public string CreatedBy { get; set; }
    }

    public class PrintingRightsList
    {
        public List<PrintingRights> Response
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
    public class PrintingRightsString
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