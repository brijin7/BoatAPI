using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class PaymentUPIDetails
    {
        public string QueryType { get; set; }
        public string UniqueId { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string UPIId { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }


        public string GatewayName { get; set; }
        public string MerchantId { get; set; }
        public string AccessCode { get; set; }
        public string WorkingKey { get; set; }
        public string MerchantCode { get; set; }        
    }

    public class PaymentRightsDetails
    {
        public string QueryType { get; set; }
        public string UniqueId { get; set; }
        public string ApplicationType { get; set; }
        public string BranchType { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string BlockType { get; set; }
        public string CreatedBy { get; set; }

        public string ActiveStatus { get; set; }
        public string UnBlockReason { get; set; }
        public string BlockReason { get; set; }

    }

    public class PaymentAccDetails
    {
        public string QueryType { get; set; }
        public int UniqueId { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string BankIFSCCode { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string MICRCode { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string EntityType { get; set; }
        public string CreatedBy { get; set; }


    }
}