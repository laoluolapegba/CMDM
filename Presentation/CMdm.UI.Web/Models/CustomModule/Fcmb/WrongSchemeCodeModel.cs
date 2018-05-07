using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.CustomModule.Fcmb
{
    public class WrongSchemeCodeModel
    {
        public WrongSchemeCodeModel()
        {
            Branches = new List<SelectListItem>();
        }

        [DisplayName("CIF ID")]
        public string CIF_ID { get; set; }
        [DisplayName("Account Number")]
        public string FORACID { get; set; }
        [DisplayName("Scheme Code")]
        public string SCHM_CODE { get; set; }
        [DisplayName("Account Officer Code")]
        public string ACCOUNTOFFICER_CODE { get; set; }
        [DisplayName("Account Officer Name")]
        public string ACCOUNTOFFICER_NAME { get; set; }
        [DisplayName("Scheme Code Classification")]
        public string SCHMECODE_CLASSIFIATION { get; set; }
        [DisplayName("Account Name")]
        public string ACCT_NAME { get; set; }
        [DisplayName("Branch Name")]
        public string SOL_ID { get; set; }
        [DisplayName("Customer Type")]
        public string CUSTOMER_TYPE { get; set; }
        [DisplayName("Run Date")]
        public DateTime? DATE_OF_RUN { get; set; }
        public int Id
        {
            get; set;
        }

        public IList<SelectListItem> Branches { get; set; }
    }
}