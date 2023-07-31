using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class LocationMaster
    {
        public string QueryType { get; set; }
        public int LocationId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string LocationName { get; set; }
        public string LocationImageLink { get; set; }
        public string LocationDescription { get; set; }
        public string HomePageDisplay { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
    }

    public class LocationMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class LocationMasterList
    {
        public List<LocationMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class LocationGallery
    {
        public string QueryType { get; set; }
        public int LocationId { get; set; }
        public int GalleryId { get; set; }
        public string LocationName { get; set; }
        public string LocationImageLink { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
    }

    public class LocationGalleryRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class LocationGalleryList
    {
        public List<LocationGallery> Response { get; set; }
        public int StatusCode { get; set; }
    }
}