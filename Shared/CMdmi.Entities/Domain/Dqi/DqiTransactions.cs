namespace CMdm.Entities.Domain.Dqi
{
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MDM_DQI_RECORD_TRANSACTIONS")]
    public partial class DqiTransactions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RECORD_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string TXN_SEQ_NO { get; set; }

        [Required]
        [StringLength(50)]
        public string ENTITY_KEY_VALUE { get; set; }

        public short? ENTITY_ID { get; set; }

        public short? ENTITY_DETAIL_ID { get; set; }

        public short METRIC_ID { get; set; }

        public decimal? DQI_SCORE { get; set; }

        public DateTime? RUNDATE { get; set; }

        public virtual MdmEntityDetails MDM_ENTITY_DETAILS { get; set; }
    }
}
