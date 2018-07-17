using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class CompDetailsModel
    {
        public CompDetailsModel()
        {
            Branches = new List<SelectListItem>();
            BusinessSegments = new List<SelectListItem>();
            Countries = new List<SelectListItem>();
            IsStockExchange = new List<SelectListItem>();
        }

        [DisplayName("Customer NO")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Operating Address")]
        public string BIZ_ADDRESS_REG_OFFICE_1 { get; set; }
        [DisplayName("Company Registration NO")]
        public string CERT_OF_INCORP_REG_NO { get; set; }
        [DisplayName("Expected Annual Turnover")]
        public decimal? EXPECTED_ANNUAL_TURNOVER { get; set; }
        [DisplayName("Operating Business 1")]
        public string OPERATING_BUSINESS_1 { get; set; }
        [DisplayName("Branch name")]
        public string BRANCH_CODE { get; set; }
        public int? QUEUE_STATUS { get; set; }

        public List<SelectListItem> Branches { get; set; }
        public List<SelectListItem> BusinessSegments { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> IsStockExchange { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }
    }
}