namespace CMdm.Entities.Domain.Dqi
{
    using Mdm;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MDM_DQI_AGGR_TRANSACTIONS")]
    public partial class DqiAggrTransactions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RECORD_ID { get; set; }

        [StringLength(50)]
        public string TXN_REF_NO { get; set; }

        public short METRIC_ID { get; set; }

        public short? DIM_ID { get; set; }

        public virtual MdmAggrDimensions MDM_AGGR_DIMENSION { get; set; }

        public virtual MdmMetrics MDM_METRICS { get; set; }
    }
}
