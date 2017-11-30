namespace CMdm.Data.Rbac
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CM_ROLE_PERM_XREF")]
    public partial class CM_ROLE_PERM_XREF
    {
        [Key]
        [Column(Order = 0)]
        public int ROLE_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal PERMISSION_ID { get; set; }

        [Required]

        public int PARENT_TASK { get; set; }

        [StringLength(20)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        public decimal RECORD_ID { get; set; }

        public virtual CM_PERMISSIONS CM_PERMISSIONS { get; set; }

        public virtual CM_USER_ROLES CM_USER_ROLES { get; set; }
    }
}
