namespace CMdm.Entities.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   
    [Table("MDM_ENTITY_MAST")]
    public partial class EntityMast
    {
        public EntityMast()
        {
            EntityDetails = new HashSet<MdmEntityDetails>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ENTITY_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ENTITY_NAME { get; set; }

        [StringLength(100)]
        public string ENTITY_DESC { get; set; }

        [Required]
        [StringLength(50)]
        public string ENTITY_TAB_NAME { get; set; }

        [Required]
        [StringLength(50)]
        public string ENTITY_KEY { get; set; }

        [StringLength(20)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string LAST_MODIFIED_BY { get; set; }

        public DateTime? LAST_MODIFIED_DATE { get; set; }

        [StringLength(1)]
        public string RECORD_STATUS { get; set; }

        public int CATALOG_ID { get; set; }
        public virtual ICollection<MdmEntityDetails> EntityDetails { get; set; }
    }
}
