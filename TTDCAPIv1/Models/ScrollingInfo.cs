using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{

    public class ScrollingInfo
    {

        public string QueryType { get; set; }
        public string InfoId { get; set; }
        public string Information { get; set; }
        public string InfoLinkURL { get; set; }
        public string InfoType { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
        public string InformationType { get; set; }
        


    }

    public class ScrollingInfoRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class ScrollingInfoList
    {
        public List<ScrollingInfo> Response { get; set; }
        public int StatusCode { get; set; }
    }

}