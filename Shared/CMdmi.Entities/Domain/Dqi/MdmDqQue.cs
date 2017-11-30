using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Dqi
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MDM_DQ_QUE")]
    public partial class MdmDQQue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [DisplayName("Branch Code")]
        public int BRANCH_CODE { get; set; }
        public decimal PCT_COMPLETION { get; set; }

        public virtual MdmDQImpact MdmDQImpacts { get; set; }

        public virtual MdmDQPriority MdmDQPriorities { get; set; }
        public virtual MdmDQQueStatus MdmDQQueStatuses { get; set; }
        public virtual MdmDqRule MdmDqRules { get; set; }

    }
}
