using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class OfferMaster
    {
        public string OfferId
        {
            get;
            set;
        }

        public string OfferType
        {
            get;
            set;
        }

        public string OfferCategory
        {
            get;
            set;
        }
        public string OfferCategoryName
        {
            get;
            set;
        }
        public string OfferName
        {
            get;
            set;
        }

        public string AmountType
        {
            get;
            set;
        }
        public string Offer
        {
            get;
            set;
        }
        public string MinNoOfTickets
        {
            get;
            set;
        }
        public string EffectiveFrom
        {
            get;
            set;
        }
        public string EffectiveTill
        {
            get;
            set;
        }

        public string ActiveStatus
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public string CreatedDate
        {
            get;
            set;
        }
        public string UpdatedBy
        {
            get;
            set;
        }
        public string UpdatedDate
        {
            get;
            set;
        }

        public string Input1
        {
            get;
            set;
        }

        public string QueryType { get; set; }
        public string CorpId { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string OfferAmount { get; set; }
        public string MinBillAmount { get; set; }
        public string Createdby { get; set; }

    }

    public class OfferMasterList
    {
        public List<OfferMaster> Response
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
    public class OfferMasterString
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