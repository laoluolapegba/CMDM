using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.Services.Report
{
    public class ReportRequestModel
    {
        public ReportRequestModel()
        {
            Branches = new List<SelectListItem>();
            ReportList = new List<SelectListItem>();
        }
        [Required]
        public int REPORT_ID { get; set; }
        public string REPORT_NAME { get; set; }
        public string REPORT_DESCRIPTION { get; set; }
        public string REPORT_URL { get; set; }
        [Required]
        public string CURRENCY_ID { get; set; }
        public string BRANCH_ID { get; set; }
        [UIHint("DateNullable")]
        [DisplayName("From Date")]
        public DateTime FROM_DATE { get; set; }
        [UIHint("DateNullable")]
        [DisplayName("To Date")]
        public DateTime TO_DATE { get; set; }
        public string DATASETNAME { get; set; }
        public int SearchMode { get; set; }
        public string StatusCode { get; set; }
        public string CUSTOMER_NO { get; set; }
        public List<SelectListItem> ReportList { get; set; }
        public List<SelectListItem> Branches { get; set; }
    }
}