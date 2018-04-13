
namespace CMdm.UI.Web.Models.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;

    public class IndividualContactDetails
    {
   //     [DisplayName("Customer Number")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Mobile Number")]
        public string MOBILE_NO { get; set; }
        [DisplayName("Email Address")]
        public string EMAIL_ADDRESS { get; set; }
        [DisplayName("Mailing Address")]
        public string MAILING_ADDRESS { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public DateTime? AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }
    }
}