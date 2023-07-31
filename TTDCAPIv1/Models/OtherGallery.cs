using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class OtherGallery
    {
        public string QueryType { get; set; }
        public string GalleryId { get; set; }
        public string Type { get; set; }
        public string ImageVideoLink { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
    }
    public class OtherGalleryrRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class OtherGalleryList
    {
        public List<OtherGallery> Response { get; set; }
        public int StatusCode { get; set; }
    }
}