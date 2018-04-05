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
        }
        [DisplayName("Customer No")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Trusts Client ACC")]
        public string TRUSTS_CLIENT_ACCOUNTS { get; set; }
        [DisplayName("Beneficial Ownner")]
        public string NAME_OF_BENEFICIAL_OWNER { get; set; }
        [DisplayName("Spouse Name")]
        public string SPOUSE_NAME { get; set; }
        [DisplayName("Spouse’s Date of Birth")]
        [UIHint("DateNullable")]
        public DateTime? SPOUSE_DATE_OF_BIRTH { get; set; }
        [DisplayName("Spouse’s Occupation")]
        public string SPOUSE_OCCUPATION { get; set; }
        [DisplayName("Sources Of Fund To The Account")]
        public string SOURCES_OF_FUND_TO_ACCOUNT { get; set; }
        [DisplayName("Expected Annual Income From Other Sources")]
        public string OTHER_SOURCE_EXPECT_ANN_INC { get; set; }
        [DisplayName("Name Of Associated Business(Es)")]
        public string NAME_OF_ASSOCIATED_BUSINESS { get; set; }
        [DisplayName("Frequent International Traveler")]
        public string FREQ_INTERNATIONAL_TRAVELER { get; set; }
        [DisplayName("Insider Relation")]
        public string INSIDER_RELATION { get; set; }
        [DisplayName("Is The Applicant A Politically Exposed Person")]
        public string POLITICALLY_EXPOSED_PERSON { get; set; }
        [DisplayName("Power Of Attorney")]
        public string POWER_OF_ATTORNEY { get; set; }
        [DisplayName("Holder Name")]
        public string HOLDER_NAME { get; set; }
        [DisplayName("Address")]
        public string ADDRESS { get; set; }
        [DisplayName("Country")]
        public decimal? COUNTRY { get; set; }
        [DisplayName("Nationality")]
        public decimal? NATIONALITY { get; set; }
        [DisplayName("Telephone Number")]
        public string TELEPHONE_NUMBER { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }

        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Nationalities { get; set; }
        public List<SelectListItem> CientAcc { get; set; }
        public List<SelectListItem> FreqInt { get; set; }
        public List<SelectListItem> InsidrRel { get; set; }
        public List<SelectListItem> PolExpose { get; set; }
        public List<SelectListItem> PowAnto { get; set; }
    }
}