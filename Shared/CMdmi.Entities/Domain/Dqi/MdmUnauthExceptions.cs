using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CMdm.Entities.Domain.Dqi
{
    

    [Table("VW_UNAUTHORISED_CHANGES")] 
    public partial class MdmUnauthException
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public int ISSUE_STATUS { get; set; }
        [DisplayName("Priority")]
        public int ISSUE_PRIORITY { get; set; }
        public int CATALOG_ID { get; set; }
        public int? RELATED_ENTITY_ID { get; set; }
        public string CATALOG_TAB_COL { get; set; }
        public virtual MdmDQPriority MdmDQPriorities { get; set; }
        public virtual MdmDQQueStatus MdmDQQueStatuses { get; set; }
        //public virtual MdmDqRule MdmDqRule { get; set; }
        //public virtual Mdm MdmDqRule { get; set; }
        [DisplayName("Table Name")]
        public string CATALOG_TABLE_NAME { get; set; }
        [DisplayName("Exception reason")]
        public string REASON { get; set; }
        public string AUTH_REJECT_REASON { get; set; }

        public string FIRST_NAME { get; set; }

        public string SURNAME { get; set; }
        public string OTHER_NAME { get; set; }

    }
}