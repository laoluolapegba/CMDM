using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.CustomModule.Fcmb
{
    public class DistinctRefCodeModel
    {
        [DisplayName("Account Officer")]
        public string ACCOUNTOFFICER_NAME { get; set; }
        [DisplayName("Ref Code")]
        public string REF_CODE { get; set; }

        public int Id
        {
            get; set;
        }
    }
}