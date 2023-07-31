using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class ImageUpload
    {
        public string BoatHouseId
        {
            get;
            set;
        }
        public string QueryType
        {
            get;
            set;
        }
        public string ImageLink
        {
            get;
            set;
        }

        public string PrevImageLink
        {
            get;
            set;
        }
        public string FormName
        {
            get;
            set;
        }
    }
    public class ImageUploadRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class ImageUploadList
    {
        public List<ImageUpload> Response { get; set; }
        public int StatusCode { get; set; }
    }
}