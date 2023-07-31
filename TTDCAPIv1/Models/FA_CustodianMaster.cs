using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FA_CustodianMaster
    {
        public string QueryType { get; set; }
        public string CustodianId { get; set; }
        public string CustodianName { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
        public string CorpId { get; set; }
    }
    public class FA_CustodianMasterList
    {
        public List<FA_CustodianMaster> Response
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
    public class FA_CustodianMasterRes
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