namespace CMdm.Entities.Domain.Entity
{
    using Mdm;
    using Dqi;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MDM_ENTITY_DETAILS")]
    public partial class MdmEntityDetails
    {
        public MdmEntityDetails()
        {
            MDM_DQI_RECORD_TRANSACTIONS = new HashSet<DqiTransactions>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ENTITY_DETAIL_ID { get; set; }

        public int ENTITY_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ENTITY_TAB_NAME { get; set; }

        [StringLength(50)]
        public string ENTITY_COL_NAME { get; set; }

        public bool FLG_MANDATORY { get; set; }

        public int WEIGHT_ID { get; set; }

        [StringLength(20)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string LAST_MODIFIED_BY { get; set; }

        public DateTime? LAST_MODIFIED_DATE { get; set; }

        public bool RECORD_STATUS { get; set; }
        public int REGEX { get; set; }
        public string DEFAULT_VALUE { get; set; }
        public int COLUMN_ORDER { get; set; }
        public int CATALOG_ID { get; set; }
        public bool USE_FOR_DQI { get; set; }
        public int DQ_DIMENSION { get; set; }
        //public int RECORD_STATUS { get; set; }
        

        public virtual ICollection<DqiTransactions> MDM_DQI_RECORD_TRANSACTIONS { get; set; }

        public virtual MdmWeights MDM_WEIGHTS { get; set; }

        public virtual EntityMast EntityMast { get; set; }
        public virtual MdmRegex MdmRegex { get; set; }
        public virtual MdmCatalog MdmCatalog { get; set; }
        public virtual MdmAggrDimensions MdmAggrDimensions { get; set; }

    }
}
