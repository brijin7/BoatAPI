using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class Enquiry
    {
        public string QueryType { get; set; }
        public string EnquiryId { get; set; }
        public string EnquiryType { get; set; }
        public string EnquiryTypeName { get; set; }
        public string EnquiredBy { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string MailId { get; set; }
        public string QueryDetails { get; set; }
        public string ResponseDetails { get; set; }
        public string ResponseBy { get; set; }
    }
    public class EnquiryRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class EnquiryList
    {
        public List<Enquiry> Response { get; set; }
        public int StatusCode { get; set; }
    }

}