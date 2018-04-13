using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class AuthFIModel
    {
        public AuthFIModel()
        {
            SocialOrFin = new List<SelectListItem>();
            KycReq = new List<SelectListItem>();
            YesKyc = new List<SelectListItem>();
        }

        [DisplayName("Customer No")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Is The Customer Socially Or Financially Disadvantaged?")]
        public string SOCIAL_FINANCIAL_DISADVTAGE { get; set; }
        [DisplayName("If Answer To The (I)Above Is Yes, State Other Documents Obtained In Line With The Bank’S Policy On Socially/ Financially Disadvantaged Customer Incompliance With Regulation 77 (4) Of Aml/Cft Regulation, 2013")]
        public string SOCIAL_FINANCIAL_DOCUMENTS { get; set; }
        [DisplayName("Does The Customer Enjoy Tiered KYC Requirements?")]
        public string ENJOYED_TIERED_KYC { get; set; }
        [DisplayName("If Answer To Question Above Is Yes, Identify The Customer Risk Category:")]
        public string RISK_CATEGORY { get; set; }
        [DisplayName("Mandate Authorization/Combination Rule ")]
        public string MANDATE_AUTH_COMBINE_RULE { get; set; }
        [DisplayName("Account Held With Other Banks")]
        public string ACCOUNT_WITH_OTHER_BANKS { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }

        public List<SelectListItem> SocialOrFin { get; set; }
        public List<SelectListItem> KycReq { get; set; }
        public List<SelectListItem> YesKyc { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }
    }
}