using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class CompInfoModel
    {
        public CompInfoModel()
        {
            BusinessSegments = new List<SelectListItem>();
            CustomerTypes = new List<SelectListItem>();
            Branches = new List<SelectListItem>();
        }
        [DisplayName("Customer NO")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Source of Asset")]
        public string SOURCE_OF_ASSET { get; set; }
        [DisplayName("Registered Address")]
        public string REGISTERED_ADDRESS { get; set; }
        [DisplayName("Introduced By")]
        public string INTRODUCED_BY { get; set; }
        [DisplayName("Length of Stay Inbus")]
        public int? LENGTH_OF_STAY_INBUS { get; set; }
        [DisplayName("Company Name")]
        public string COMPANY_NAME { get; set; }
        [DisplayName("History of Customer")]
        public string HISTORY_OF_CUSTOMER { get; set; }
        [DisplayName("Company Networth")]
        public int? COMPANY_NETWORTH_SOA { get; set; }
        [DisplayName("Current Line of Business")]
        public string CURRENT_LINE_OF_BUSINESS { get; set; }
        [DisplayName("Brf Investigation Media Report")]
        public string BRF_INVESTIGATION_MEDIA_REPORT { get; set; }
        [DisplayName("Investigation Media Report")]
        public string INVESTIGATION_MEDIA_REPORT { get; set; }
        [DisplayName("Add Verification Status")]
        public string ADD_VERIFICATION_STATUS { get; set; }
        [DisplayName("Anticipated Trans Outflow")]
        public int? ANTICIPATED_TRANS_OUTFLOW { get; set; }
        [DisplayName("Anticipated Trans Inflow")]
        public int? ANTICIPATED_TRANS_INFLOW { get; set; }
        [DisplayName("EOC Risk")]
        public string EOC_RISK { get; set; }
        [DisplayName("Branch name")]
        public string BRANCH_CODE { get; set; }
        public int? QUEUE_STATUS { get; set; }

        public List<SelectListItem> BusinessSegments { get; set; }
        public List<SelectListItem> CustomerTypes { get; set; }
        public List<SelectListItem> Branches { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }
    }
}