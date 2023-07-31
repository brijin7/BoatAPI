using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class UserRegistration
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string QueryType { get; set; }
    }

    public class UserRegistrationRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class UserRegistrationList
    {
        public List<UserRegistration> Response { get; set; }
        public int StatusCode { get; set; }
    }
}