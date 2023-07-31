using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class Feedback
    {
        public string QueryType { get; set; }
        public string FeedbackId { get; set; }
        public string GivenBy { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string MailId { get; set; }
        public string FeedbackDet { get; set; }
        public string HomePageDisplay { get; set; }
        public string ActionDetails { get; set; }
        public string Status { get; set; }
        public string ActionBy { get; set; }

        public string ActionDate { get; set; }

    }
    public class FeedbackRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class FeedbackList
    {
        public List<Feedback> Response { get; set; }
        public int StatusCode { get; set; }
    }

}