namespace CMdm.UI.Web.Models.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    public class CustomerTrustClientAccount
    {
        public CustomerTrustClientAccount()
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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Spouse’s Date of Birth")]
        public Nullable<System.DateTime> SPOUSE_DATE_OF_BIRTH { get; set; }
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
        public string LAST_MODIFIED_BY { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> LAST_MODIFIED_DATE { get; set; }
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }


        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Nationalities { get; set; }
        public List<SelectListItem> CientAcc { get; set; }
        public List<SelectListItem> FreqInt { get; set; }
        public List<SelectListItem> InsidrRel { get; set; }
        public List<SelectListItem> PolExpose { get; set; }
        public List<SelectListItem> PowAnto { get; set; }
    }
}