using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class ReasonMaster
    {
        public string BoatHouseId
        {
            get;
            set;
        }

        public int ReasonId
        {
            get;
            set;
        }
        
        public string ReasonName
        {
            get;
            set;
        }
    }
    public class ReasonMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class ReasonMasterList
    {
        public List<ReasonMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }
}