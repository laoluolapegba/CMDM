namespace CMdm.Entities.Domain.Mdm
{
    using Entity;
    using Dqi;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MDM_AGGR_DIMENSION")]
    public partial class MdmAggrDimensions 
    {
        public MdmAggrDimensions()
        {
            MDM_DQI_AGGR_TRANSACTIONS = new HashSet<DqiAggrTransactions>();
            MdmDqRules = new HashSet<MdmDqRule>();
            MDM_ENTITY_DETAILS = new HashSet<MdmEntityDetails>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DIMENSIONID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Quality Dimension")]
        public string DIMENSION_NAME { get; set; }

        [StringLength(20)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string LAST_MODIFIED_BY { get; set; }

        public DateTime? LAST_MODIFIED_DATE { get; set; }

        [StringLength(1)]
        public string RECORD_STATUS { get; set; }

        public virtual ICollection<DqiAggrTransactions> MDM_DQI_AGGR_TRANSACTIONS { get; set; }
        public virtual ICollection<MdmDqRule> MdmDqRules { get; set; }
        public virtual ICollection<MdmEntityDetails> MDM_ENTITY_DETAILS { get; set; }
    }
}
