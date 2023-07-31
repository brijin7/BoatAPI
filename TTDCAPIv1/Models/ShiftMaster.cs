using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class ShiftMaster
    {

        public string QueryType { get; set; }
        public string ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string BreakStartTime { get; set; }
        public string BreakEndTime { get; set; }
        public string GracePeriod { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
    }

    public class ShiftMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class ShiftMasterList
    {
        public List<ShiftMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }
}