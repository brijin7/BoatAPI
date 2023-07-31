using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BranchDesgMap
    {
        public string QueryType
        {
            get;
            set;
        }
        public string UniqueId
        {
            get;
            set;
        }

        public string BranchId
        {
            get;
            set;
        }
        public string DesignationId
        {
            get;
            set;
        }
        public string Department
        {
            get;
            set;
        }

        public string Designation
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public string ActiveStatus
        {
            get;
            set;
        }

    }

    public class DeptDesgStr
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

    public class DeptDesgList
    {
        public List<BranchDesgMap> Response
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