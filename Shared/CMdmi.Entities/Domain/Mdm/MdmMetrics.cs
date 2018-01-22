namespace CMdm.Entities.Domain.Mdm
{
    using Dqi;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MDM_METRICS")]
    public partial class MdmMetrics
    {
        public MdmMetrics()
        {
            MDM_DQI_AGGR_TRANSACTIONS = new HashSet<DqiAggrTransactions>();
           
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int METRIC_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string METRIC_NAME { get; set; }

        [StringLength(50)]
        public string METRIC_DESC { get; set; }

        public decimal METRIC_SCORE { get; set; }

        [StringLength(20)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string LAST_MODIFIED_BY { get; set; }

        public DateTime? LAST_MODIFIED_DATE { get; set; }

        [StringLength(1)]
        public string RECORD_STATUS { get; set; }

        public virtual ICollection<DqiAggrTransactions> MDM_DQI_AGGR_TRANSACTIONS { get; set; }
        
    }
}
