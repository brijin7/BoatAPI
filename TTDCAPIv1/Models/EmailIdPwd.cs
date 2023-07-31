using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class EmailIdPwd
    {
        public string QueryType { get; set; }
        public int UniqueId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }

        public string ServiceType { get; set; }
    }
}