using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.CustomModule.Fcmb
{
    public class AccountOfficerModel
    {
        public AccountOfficerModel()
        {
            Branches = new List<SelectListItem>();
            AccountOfficers = new List<SelectListItem>();
        }

        [DisplayName("Search ")]
        public string SearchName { get; set; }
        [DisplayName("Account Number ")]
        public string ACCOUNT_NUMBER { get; set; }
        [DisplayName("Account Name")]
        public string ACCOUNT_NAME { get; set; }
        [DisplayName("Branch Code ")]
        public string SOL_ID { get; set; }
        [DisplayName("Branch Name ")]
        public string BRANCH { get; set; }
        [DisplayName("Scheme Code ")]
        public string SCHM_CODE { get; set; }
        [DisplayName("Account Opened Date ")]
        [UIHint("Nullable")]
        public DateTime? ACCT_OPN_DATE { get; set; }
        [DisplayName("AO Code ")]
        public string AO_CODE { get; set; }
        [DisplayName("AO Name ")]
        public string AO_NAME { get; set; }
        [DisplayName("SBU Code ")]
        public string SBU_CODE { get; set; }
        [DisplayName("SBU Name")]
        public string SBU_NAME { get; set; }
        [DisplayName("Broker Code")]
        public string BROKER_CODE { get; set; }
        [DisplayName("Broker Name")]
        public string BROKER_NAME { get; set; }
        [DisplayName("Run Date")]
        public DateTime? RUN_DATE { get; set; }
        public int Id
        {
            get; set;
        }

        public IList<SelectListItem> Branches { get; set; }
        public IList<SelectListItem> AccountOfficers { get; set; }
    }
}