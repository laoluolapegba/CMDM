using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.CustomModule.Fcmb
{
    public class PhoneValidationModel
    {
        public PhoneValidationModel()
        {
            Branches = new List<SelectListItem>();
        }
        [DisplayName("Customer Number ")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Branch Code ")]
        public string BRANCH_CODE { get; set; }
        [DisplayName("Last Name ")]
        public string CUST_LAST_NAME { get; set; }
        [DisplayName("Middle Name ")]
        public string CUST_MIDDLE_NAME { get; set; }
        [DisplayName("First Name ")]
        public string CUST_FIRST_NAME { get; set; }
        [DisplayName("Run Date ")]
        public DateTime? LAST_RUN_DATE { get; set; }
        [DisplayName("Reason ")]
        public string REASON { get; set; }
        [DisplayName("Phone Number ")]
        public string PHONE_NO { get; set; }
        [DisplayName("Type ")]
        public string TYPE { get; set; }
        [DisplayName("Branch Name ")]
        public string BRANCH_NAME { get; set; }
        [DisplayName("Scheme Code ")]
        public string SCHEME_CODE { get; set; }
        [DisplayName("Account Number ")]
        public string ACCOUNTNO { get; set; }
        public int Id
        {
            get; set;
        }

        public IList<SelectListItem> Branches { get; set; }
    }
}