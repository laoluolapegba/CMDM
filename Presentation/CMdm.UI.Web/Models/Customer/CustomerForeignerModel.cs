using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class CustomerForeignerModel
    {
        public CustomerForeignerModel()
        {
            Countries = new List<SelectListItem>();
            Foreigners = new List<SelectListItem>();
            MultipleCitezenships = new List<SelectListItem>();
        }
        [DisplayName("Customer NO")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Passport / Residence Permit No")]
        public string PASSPORT_RESIDENCE_PERMIT { get; set; }
        [DisplayName("Passport / Permit Issue Date")]
        [UIHint("DateNullable")]
        public DateTime? PERMIT_ISSUE_DATE { get; set; }
        [DisplayName("Passport / Permit Expiry Date")]
        [UIHint("DateNullable")]
        public DateTime? PERMIT_EXPIRY_DATE { get; set; }
        [DisplayName("Foreign Address")]
        public string FOREIGN_ADDRESS { get; set; }
        [DisplayName("City")]
        public string CITY { get; set; }
        [DisplayName("Foreign Phone No")]
        public string FOREIGN_TEL_NUMBER { get; set; }
        [DisplayName("Zip Postal Code")]
        public string ZIP_POSTAL_CODE { get; set; }
        [DisplayName("Purpose of Account")]
        public string PURPOSE_OF_ACCOUNT { get; set; }
        [DisplayName("Country")]
        public int? COUNTRY { get; set; }
        [DisplayName("Foreigner")]
        public string FOREIGNER { get; set; }
        [DisplayName("Multiple Citezenship")]
        public string MULTIPLE_CITEZENSHIP { get; set; }

        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Foreigners { get; set; }
        public List<SelectListItem> MultipleCitezenships { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }
    }
}