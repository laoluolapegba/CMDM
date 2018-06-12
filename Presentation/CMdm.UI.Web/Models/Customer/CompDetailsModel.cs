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
        [DisplayName("Company Registration NO")]
        public string CERT_OF_INCORP_REG_NO { get; set; }
        [DisplayName("Jurisdiction of Incorporation")]
        public string JURISDICTION_OF_INCORP_REG { get; set; }
        [DisplayName("Scuml No")]
        public string SCUML_NO { get; set; }
        [DisplayName("Gender Controlling 51%")]
        public string GENDER_CONTROLLING_51_PERC { get; set; }
        [DisplayName("Sector or Industry")]
        public string SECTOR_OR_INDUSTRY { get; set; }
        [DisplayName("Operating Business 1")]
        public string OPERATING_BUSINESS_1 { get; set; }
        [DisplayName("City 1")]
        public string CITY_1 { get; set; }
        [DisplayName("Country 1")]
        public string COUNTRY_1 { get; set; }
        [DisplayName("Zip Code 1")]
        public string ZIP_CODE_1 { get; set; }
        [DisplayName("Business Address 1")]
        public string BIZ_ADDRESS_REG_OFFICE_1 { get; set; }
        [DisplayName("Operating Business 2")]
        public string OPERATING_BUSINESS_2 { get; set; }
        [DisplayName("City 2")]
        public string CITY_2 { get; set; }
        [DisplayName("Country 2")]
        public string COUNTRY_2 { get; set; }
        [DisplayName("Zip Code 2")]
        public string ZIP_CODE_2 { get; set; }
        [DisplayName("Business Address 2")]
        public string BIZ_ADDRESS_REG_OFFICE_2 { get; set; }
        [DisplayName("Company Email Address")]
        public string COMPANY_EMAIL_ADDRESS { get; set; }
        [DisplayName("Website")]
        public string WEBSITE { get; set; }
        [DisplayName("Office Number")]
        public string OFFICE_NUMBER { get; set; }
        [DisplayName("Mobile Number")]
        public string MOBILE_NUMBER { get; set; }
        [DisplayName("TIN")]
        public string TIN { get; set; }
        [DisplayName("No Borrower Code")]
        public string CRMB_NO_BORROWER_CODE { get; set; }
        [DisplayName("Expected Annual Turnover")]
        public int? EXPECTED_ANNUAL_TURNOVER { get; set; }
        [DisplayName("Is company on stock exchange?")]
        public string IS_COMPANY_ON_STOCK_EXCH { get; set; }
        [DisplayName("Stock exchange name")]
        public string STOCK_EXCHANGE_NAME { get; set; }
        [DisplayName("Branch name")]
        public string BRANCH_CODE { get; set; }

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