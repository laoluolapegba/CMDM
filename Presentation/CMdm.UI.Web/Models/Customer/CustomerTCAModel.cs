using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class CustomerTCAModel
    {
        public CustomerTCAModel()
        {
            Countries = new List<SelectListItem>();
            Nationalities = new List<SelectListItem>();
            CientAcc = new List<SelectListItem>();
            FreqInt = new List<SelectListItem>();
            InsidrRel = new List<SelectListItem>();
            PolExpose = new List<SelectListItem>();
            PowAnto = new List<SelectListItem>();
            Branches = new List<SelectListItem>();
        }
        [DisplayName("Customer No")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Customer Business Address")]
        public string CUSTOMER_BUSINESS_ADDRESS { get; set; }
        [DisplayName("Customer Spouse DOB")]
        [UIHint("DateNullable")]
        public DateTime? CUSTOMER_SPOUSE_DOB { get; set; }
        [DisplayName("Expected Annual Income From Other Sources")]
        public string OTHER_SOURCE_EXPECT_ANN_INC { get; set; }
        [DisplayName("Customer Business Name")]
        public string CUSTOMER_BUSINESS_NAME { get; set; }
        [DisplayName("Customer Spouse Name")]
        public string CUSTOMER_SPOUSE_NAME { get; set; }
        [DisplayName("Customer Spouse Occupation")]
        public string CUSTOMER_SPOUSE_OCCUPATION { get; set; }
        [DisplayName("Customer Business Type")]
        public string CUSTOMER_BUSINESS_TYPE { get; set; }
        [DisplayName("Primary Source Of Fund")]
        public string SOURCES_OF_FUND_TO_ACCOUNT { get; set; }
        [DisplayName("Branch")]
        public string BRANCH_CODE { get; set; }
        public int? QUEUE_STATUS { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }

        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Nationalities { get; set; }
        public List<SelectListItem> CientAcc { get; set; }
        public List<SelectListItem> FreqInt { get; set; }
        public List<SelectListItem> InsidrRel { get; set; }
        public List<SelectListItem> PolExpose { get; set; }
        public List<SelectListItem> PowAnto { get; set; }
        public List<SelectListItem> Branches { get; set; }
    }
}