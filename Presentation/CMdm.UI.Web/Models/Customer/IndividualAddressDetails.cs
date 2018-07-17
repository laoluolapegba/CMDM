namespace CMdm.UI.Web.Models.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    public class IndividualAddressDetails
    {
        public IndividualAddressDetails()
        {
            CountryofResidence = new List<SelectListItem>();
            StateofResidence = new List<SelectListItem>();
            LGAofResidence = new List<SelectListItem>();
            ResidenceStatus = new List<SelectListItem>();
        }
        [DisplayName("Customer Number")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Residential Address ")]
        public string RESIDENTIAL_ADDRESS { get; set; }
        [DisplayName("Town of Residence")]
        public string CITY_TOWN_OF_RESIDENCE { get; set; }
        [DisplayName("LGA of Residence")]
        public string LGA_OF_RESIDENCE { get; set; }
        [DisplayName("Nearest Busstop/LandMark")]
        public string NEAREST_BUS_STOP_LANDMARK { get; set; }
        [DisplayName("State of Residence")]
        public string STATE_OF_RESIDENCE { get; set; }
        [DisplayName("Country of Residence")]
        public string COUNTRY_OF_RESIDENCE { get; set; }
        [DisplayName("Residence Owned or Rented")]
        public string RESIDENCE_OWNED_OR_RENT { get; set; }
        [DisplayName("Postal Code")]
        public string ZIP_POSTAL_CODE { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        [DisplayName("Branch")]
        public string CREATED_BY { get; set; }
        [DisplayName("Branch")]
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public DateTime? AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }
        public int? QUEUE_STATUS { get; set; }

        public List<SelectListItem> CountryofResidence { get; set; }
        public List<SelectListItem> StateofResidence { get; set; }
        public List<SelectListItem> LGAofResidence { get; set; }
        public List<SelectListItem> ResidenceStatus { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }
    }
}