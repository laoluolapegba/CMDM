namespace CMdm.Entities.Domain.Mdm
{
    using Dqi;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MDM_WEIGHTS")]
    public partial class MdmWeights
    {
        public MdmWeights()
        {
            MDM_ENTITY_DETAILS = new HashSet<MdmEntityDetails>();
            MdmDqiCatalogs = new HashSet<MdmDqCatalog>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WEIGHT_ID { get; set; }

        public int? WEIGHT_VALUE { get; set; }

        [StringLength(20)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string LAST_MODIFIED_BY { get; set; }

        public DateTime? LAST_MODIFIED_DATE { get; set; }

        //[StringLength(1)]
        public int RECORD_STATUS { get; set; }
        [DisplayName("Weight")]
        public string WEIGHT_DESC { get; set; }
        public virtual ICollection<MdmEntityDetails> MDM_ENTITY_DETAILS { get; set; }
        public virtual ICollection<MdmDqCatalog> MdmDqiCatalogs { get; set; }
    }
}
