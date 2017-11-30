namespace CMdm.Entities.Domain.Mdm
{
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MDM_WEIGHTS")]
    public partial class MdmWeights
    {
        public MdmWeights()
        {
            MDM_ENTITY_DETAILS = new HashSet<EntityDetails>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short WEIGHT_ID { get; set; }

        public short? WEIGHT_VALUE { get; set; }

        [StringLength(20)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string LAST_MODIFIED_BY { get; set; }

        public DateTime? LAST_MODIFIED_DATE { get; set; }

        [StringLength(1)]
        public string RECORD_STATUS { get; set; }

        public virtual ICollection<EntityDetails> MDM_ENTITY_DETAILS { get; set; }
    }
}
