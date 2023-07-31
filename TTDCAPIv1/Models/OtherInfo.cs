using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class OtherInfo
    {

        public string QueryType { get; set; }
        public int InfoId { get; set; }
        public string InfoImageLink { get; set; }

        public string InfoName { get; set; }

        public string InfoDescription { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
    }
    public class OtherInfoRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class OtherInfoList
    {
        public List<OtherInfo> Response { get; set; }
        public int StatusCode { get; set; }
    }

}