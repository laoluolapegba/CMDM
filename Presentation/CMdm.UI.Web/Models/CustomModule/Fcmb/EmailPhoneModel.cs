using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.CustomModule.Fcmb
{
    public class EmailPhoneModel
    {
        public EmailPhoneModel()
        {
            Branches = new List<SelectListItem>();
        }

        [DisplayName("ORG Key")]
        public string ORGKEY { get; set; }
        [DisplayName("Duplicate ID")]
        public string DUPLICATE_ID { get; set; }
        [DisplayName("First Name")]
        public string CUST_FIRST_NAME { get; set; }
        [DisplayName("Middle Name")]
        public string CUST_MIDDLE_NAME { get; set; }
        [DisplayName("Last Name")]
        public string CUST_LAST_NAME { get; set; }
        [DisplayName("Date of Birth")]
        public DateTime? CUST_DOB { get; set; }
        [DisplayName("BVN")]
        public string BVN { get; set; }
        [DisplayName("Gender")]
        public string GENDER { get; set; }
        [DisplayName("Customer Minor")]
        public string CUSTOMERMINOR { get; set; }
        [DisplayName("Preferred Phone")]
        public string PREFERREDPHONE { get; set; }
        [DisplayName("Email")]
        public string EMAIL { get; set; }
        [DisplayName("Branch Code")]
        public string BRANCH_CODE { get; set; }
        [DisplayName("Branch Name")]
        public string BRANCH_NAME { get; set; }
        [DisplayName("Account Number")]
        public string ACCOUNT_NO { get; set; }
        [DisplayName("Scheme Code")]
        public string SCHM_CODE { get; set; }

        public int Id
        {
            get; set;
        }
        public IList<SelectListItem> Branches { get; set; }
    }
}