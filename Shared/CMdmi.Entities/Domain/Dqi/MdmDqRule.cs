using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Dqi
{
    using Mdm;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MDM_DQ_RULE")]
    public partial class MdmDqRule
    {
        public  MdmDqRule ()
        {
            MdmDQQues = new HashSet<MdmDQQue>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Record Id")]
        public int RECORD_ID { get; set; }

        [DisplayName("Datasource Id")]
        public int DATA_SOURCE_ID { get; set; }
        [DisplayName("Catalog")] 

        public int CATALOG_ID { get; set; }
        [DisplayName("Rule Name")]
        [Required]

        public string RULE_NAME { get; set; }
        [DisplayName("Population Query")]
        public string POP_QUERY { get; set; }
        [DisplayName("Exception Query")]
        //[AllowHtml]
        [Required]
        public string EXCEPTION_QUERY { get; set; }
        [DisplayName("Description and Resolution")]
        public string DESCRIPTION_RESOLUTION { get; set; }

        [DisplayName("Schedule")]
        public int RUN_SCHEDULE { get; set; }
        [DisplayName("Dimension")]
        public int DIMENSION { get; set; }
        [DisplayName("Severity")]
        public int SEVERITY { get; set; }
        [DisplayName("Last Run Date")]
        public DateTime? LAST_RUN { get; set; }
        public bool RECORD_STATUS { get; set; }
        public virtual MdmDQDataSource MdmDQDataSources { get; set; }

        public virtual MdmDqRunSchedule MdmDqRunSchedules { get; set; }
        public virtual MdmAggrDimensions MdmAggrDimensions { get; set; }
        public virtual MdmDQPriority MdmDQPriorities { get; set; }
        public virtual MdmCatalog MdmCatalogs { get; set; }
        public HashSet<MdmDQQue> MdmDQQues { get; private set; }
        
    }
}
