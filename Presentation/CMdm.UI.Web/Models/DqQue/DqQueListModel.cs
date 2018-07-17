using CMdm.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.DqQue
{
    public partial class DqQueListModel : BaseModel
    {
        

        [DisplayName("Search")]
        [AllowHtml]
        public string SearchName { get; set; }
        public DqQueListModel()
        {
            MdmList = new List<SelectListItem>();
        }
        public int RECORD_ID { get; set; }
        [DisplayName("Datasource Name")]
        public string DATA_SOURCE { get; set; }
        [DisplayName("Table Name")]

        public string CATALOG_NAME { get; set; }

        public string ERROR_CODE { get; set; }
        [DisplayName("Issue Description")]
        public string ERROR_DESC { get; set; }
        [DisplayName("DQ Process Name")]
        public string DQ_PROCESS_NAME { get; set; }
        [DisplayName("Impact")]
        public int IMPACT_LEVEL { get; set; }
        [DisplayName("Priority")]
        public int PRIORITY { get; set; }
        [DisplayName("Status")]
        public int QUE_STATUS { get; set; }

        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        [DisplayName("Violated Quality Rule")]
        public int RULE_ID { get; set; }
        [DisplayName("Branch Name")]
        public string BRANCH_CODE { get; set; }
        public decimal PCT_COMPLETION { get; set; }
        public string DaysonQue { get; set; }
        public int CATALOG_ID { get; set; }
        public int? MDM_ID { get; set; }
        public string ISSUE_PRIORITY_DESC;

        public IList<SelectListItem> MdmList;
    }
}