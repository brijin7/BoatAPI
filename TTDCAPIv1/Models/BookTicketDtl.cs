using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BoatTicketDtl
    {
        public string QueryType
        {
            get;
            set;
        }

        public string BookingMedia
        {
            get;
            set;
        }
        public string BookingId
        {
            get;
            set;
        }

        public string BookingDate
        {
            get;
            set;
        }

        public string CustomerMobile
        {
            get;
            set;
        }

        public string CustomerName
        {
            get;
            set;
        }

        public string BookingStatus
        {
            get;
            set;
        }

        public string InitBillAmount
        {
            get;
            set;
        }

        public string OtherService
        {
            get;
            set;
        }

        public string BoatHouseId
        {
            get;
            set;
        }

        public string BookingType
        {
            get;
            set;
        }

        public string FromDate
        {
            get;
            set;
        }

        public string ToDate
        {
            get;
            set;
        }

        public string BoatTypeId
        {
            get;
            set;
        }

        public string BoatType
        {
            get;
            set;
        }

        public string RemainTrips
        {
            get;
            set;
        }

        public string BookedTrips
        {
            get;
            set;
        }

        public string CustomerGSTNo
        {
            get;
            set;
        }
        public string CustomerEmailId
        {
            get;
            set;
        }
        public string UserId
        {
            get;
            set;
        }
        public string ActualBillAmount
        {
            get;
            set;
        }
        public string OtherServiceAmount
        {
            get;
            set;
        }
        public string SeaterType
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }
        public string BoatHouseName
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public string Reason
        {
            get;
            set;
        }
        public string ServiceType
        {
            get;
            set;
        }
        public string BookingPin
        {
            get;
            set;
        }

        public string RePaymentType
        {
            get;
            set;
        }

        public string CountStart { get; set; }

        public string RowNumber  { get; set; }

        public string Search { get; set; }        


    }

    public class BoatTicketDtllist
    {
        public List<BoatTicketDtl> Response
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
    public class BoatTicketDtlStr
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

    public class PublicBookedHistory
    {
        public string UserId { get; set; }
        public string MobileNo { get; set; }
        public string TransactionNo { get; set; }
        public string BookingId { get; set; }
        public string BookingDate { get; set; }
        public string Amount { get; set; }
        public string BookingType { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string BoardingPassStatus { get; set; }
        public string TripStartStatus { get; set; }
        public string BookingStatus { get; set; }
    }

    public class PublicBookedHistorylist
    {
        public List<PublicBookedHistory> Response
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
    public class PublicBookedHistoryStr
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