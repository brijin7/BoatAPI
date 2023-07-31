using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class MaterialIssue
    {
        public string QueryType { get; set; }
        public string IssueId { get; set; }
        public string ItemId { get; set; }
        public string IssueDate { get; set; }
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public string IssueRef { get; set; }
        public string IssuedQty { get; set; }
        public string IssueRate { get; set; }
        public string Createdby { get; set; }
        public string NoOfItems { get; set; }

        public string BoatHouseId { get; set; }

        public string ItemDescription { get; set; }
        public string ActiveStatus { get; set; }

    }

    public class MaterialIssueList
    {
        public List<MaterialIssue> Response
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
    public class MaterialIssueString
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