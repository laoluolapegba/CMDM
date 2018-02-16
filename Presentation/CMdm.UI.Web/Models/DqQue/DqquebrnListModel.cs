using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.DqQue
{
    
    public partial class DqquebrnListModel
    {
        public DqquebrnListModel()
        {
            Priorities = new List<SelectListItem>();
            Statuses = new List<SelectListItem>();
            Branches = new List<SelectListItem>();
        }
        [DisplayName("Search")]
        [AllowHtml]
        public string SearchName { get; set; }
        [DisplayName("FromDate")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnFrom { get; set; }

        [DisplayName("ToDate")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnTo { get; set; }
        [DisplayName("Exception Id")]
        public int EXCEPTION_ID { get; set; }
        [DisplayName("Rule Id")]
        public int RULE_ID { get; set; }
        [DisplayName("Rule name")]
        public string RULE_NAME { get; set; }
        [DisplayName("Customer Id")]
        public string CUST_ID { get; set; }
        [DisplayName("Branch Code")]
        public string BRANCH_CODE { get; set; }
        [DisplayName("Branch Name")]
        public string BRANCH_NAME { get; set; }
        [DisplayName("Last Run Date")]
        public DateTime? RUN_DATE { get; set; }
        public string RUN_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }
        [DisplayName("Status")]
        public string ISSUE_STATUS_DESC { get; set; }
        [DisplayName("Priority")]
        public string ISSUE_PRIORITY_DESC { get; set; }

        public int PRIORITY_CODE { get; set; }
        public int STATUS_CODE { get; set; }
        [DisplayName("Exception reason")]
        public string REASON { get; set; }
        public IList<SelectListItem> Priorities { get; set; }
        public IList<SelectListItem> Statuses { get; set; }
        public IList<SelectListItem> Branches { get; set; }
    }
}