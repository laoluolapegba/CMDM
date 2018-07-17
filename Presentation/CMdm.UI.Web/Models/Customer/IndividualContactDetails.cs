
namespace CMdm.UI.Web.Models.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class IndividualContactDetails
    {
        public IndividualContactDetails()
        {
            Branches = new List<SelectListItem>();
            States = new List<SelectListItem>();
            LGAs = new List<SelectListItem>();
        }

        [DisplayName("Customer NO")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Phone NO")]
        public string MOBILE_NO { get; set; }
        [DisplayName("Email")]
        public string EMAIL_ADDRESS { get; set; }
        [DisplayName("Address")]
        public string MAILING_ADDRESS { get; set; }
        [DisplayName("Alternate Phone NO")]
        public string ALTERNATEPHONENO { get; set; }
        [DisplayName("Residence LGA")]
        public string RESIDENCE_LGA { get; set; }
        [DisplayName("Nearest Bustop Landmark")]
        public string NEAREST_BUSTOP_LANDMARK { get; set; }
        [DisplayName("Branch")]
        public string BRANCH_CODE { get; set; }
        [DisplayName("State")]
        public string STATE { get; set; }
        [DisplayName("City")]
        public string CITY { get; set; }
        [DisplayName("House No")]
        public string ADDRESS_NUMBER { get; set; }
        [DisplayName("Residential Street Name")]
        public string MAILING_ADDRESS_STREET { get; set; }
        public int? TIER { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public DateTime? AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }
        public int? QUEUE_STATUS { get; set; }

        public List<SelectListItem> Branches { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> LGAs { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }
    }
}