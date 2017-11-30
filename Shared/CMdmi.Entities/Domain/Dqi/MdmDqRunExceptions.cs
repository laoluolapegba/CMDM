using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CMdm.Entities.Domain.Dqi
{
    

    [Table("MDM_DQ_RULE_RUN_EXCPTIONS")]
    public partial class MdmDqRunException
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
        public int BRANCH_CODE { get; set; }
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
        public virtual MdmDQPriority MdmDQPriorities { get; set; }
        public virtual MdmDQQueStatus MdmDQQueStatuses { get; set; }
        //public virtual MdmDqRule MdmDqRule { get; set; }
        //public virtual Mdm MdmDqRule { get; set; }
    }
}