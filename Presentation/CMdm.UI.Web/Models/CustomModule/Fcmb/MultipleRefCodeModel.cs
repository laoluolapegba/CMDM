using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.CustomModule.Fcmb
{
    public class MultipleRefCodeModel
    {
        public MultipleRefCodeModel()
        {
            Branches = new List<SelectListItem>();
        }

        [DisplayName("Account Number")]
        public string FORACID { get; set; }
        [DisplayName("Duplication ID")]
        public string DUPLICATION_ID { get; set; }
        [DisplayName("Account Officer")]
        public string ACCOUNTOFFICER_NAME { get; set; }
        [DisplayName("Ref Code")]
        public string REF_CODE { get; set; }
        [DisplayName("Branch Name")]
        public string SOL_ID { get; set; }
        [DisplayName("CIF ID")]
        public string CIF_ID { get; set; }
        [DisplayName("Run Date")]
        public DateTime? RUN_DATE { get; set; }
        [DisplayName("Scheme Code")]
        public string SCHM_CODE { get; set; }

        public int Id
        {
            get; set;
        }

        public IList<SelectListItem> Branches { get; set; }
    }
}