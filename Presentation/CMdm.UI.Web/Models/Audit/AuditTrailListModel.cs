using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Audit
{
    public class AuditTrailListModel
    {
        [DisplayName("Search")]
        [AllowHtml]
        public string SearchName { get; set; }
        public AuditTrailListModel()
        {

        }
        public decimal LogId { get; set; }
        public string CustNo { get; set; }
        public string AffectedCat { get; set; }
        public string Loggedby { get; set; }
        public DateTime? LoggedDate { get; set; }
        public string Comments { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}