using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CMdm.Entities.ViewModels
{
    public class CustExceptionsModel
    {
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
        [DisplayName("Table Name")]
        public string CATALOG_TABLE_NAME { get; set; }
        [DisplayName("Catalog")]
        public int CATALOG_ID { get; set; }
        public string SURNAME { get; set; }
        public string FIRST_NAME { get; set; }
        public string OTHERNAME { get; set; }
        [DisplayName("Status")]
        public int ISSUE_STATUS { get; set; }
        [DisplayName("Priority")]
        public int ISSUE_PRIORITY { get; set; }

    }
}