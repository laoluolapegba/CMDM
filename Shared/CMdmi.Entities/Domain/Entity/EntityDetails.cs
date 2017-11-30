namespace CMdm.Entities.Domain.Entity
{
    using Mdm;
    using Dqi;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MDM_ENTITY_DETAILS")]
    public partial class EntityDetails
    {
        public EntityDetails()
        {
            MDM_DQI_RECORD_TRANSACTIONS = new HashSet<DqiTransactions>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short ENTITY_DETAIL_ID { get; set; }

        public short ENTITY_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ENTITY_TAB_NAME { get; set; }

        [StringLength(50)]
        public string ENTITY_COL_NAME { get; set; }

        public bool FLG_MANDATORY { get; set; }

        public short WEIGHT_ID { get; set; }

        [StringLength(20)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string LAST_MODIFIED_BY { get; set; }

        public DateTime? LAST_MODIFIED_DATE { get; set; }

        [StringLength(1)]
        public string RECORD_STATUS { get; set; }

        public virtual ICollection<DqiTransactions> MDM_DQI_RECORD_TRANSACTIONS { get; set; }

        public virtual MdmWeights MDM_WEIGHTS { get; set; }

        public virtual EntityMast EntityMast { get; set; }
    }
}
