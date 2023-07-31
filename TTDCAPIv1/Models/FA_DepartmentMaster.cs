using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FA_DepartmentMaster
    {
        public string QueryType { get; set; }
        public string DeptId { get; set; }
        public string DeptName { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
        public string ServiceType { get; set; }
        public string CorpId { get; set; }
        public string BranchCode { get; set; }
    }
    public class FA_DepartmentMasterList
    {
        public List<FA_DepartmentMaster> Response
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
    public class FA_DepartmentMasterRes
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