namespace CMdm.Data.Rbac
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CM_USER_ROLES")]
    public partial class CM_USER_ROLES
    {
        public CM_USER_ROLES()
        {
            CM_ROLE_PERM_XREF = new HashSet<CM_ROLE_PERM_XREF>();
            CM_USER_PROFILE = new HashSet<CM_USER_PROFILE>();
            CM_USER_ROLE_XREF = new HashSet<CM_USER_ROLE_XREF>();
            CM_PERMISSIONS = new HashSet<CM_PERMISSIONS>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ROLE_ID { get; set; }

        [Required]
        [StringLength(120)]
        [DisplayName("Role name")]
        public string ROLE_NAME { get; set; }
        [DisplayName("Password Lifetime")]


        public int PSWD_LIFE_DAYS { get; set; }
        [DisplayName("User Authorization Level")]


        public int USER_LEVEL { get; set; }

        [StringLength(20)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string LAST_MODIFIED_BY { get; set; }

        public DateTime? LAST_MODIFIED_DATE { get; set; }
        [DisplayName("Default role?")]


        public bool IS_DEFAULT { get; set; }
        [DisplayName("Parent Role Id")]

        public int? PARENT_ID { get; set; }

        [DisplayName("Checker Role")]
        public int? CHECKER_ID { get; set; }

        public virtual ICollection<CM_ROLE_PERM_XREF> CM_ROLE_PERM_XREF { get; set; }

        public virtual ICollection<CM_USER_PROFILE> CM_USER_PROFILE { get; set; }

        public virtual ICollection<CM_USER_ROLE_XREF> CM_USER_ROLE_XREF { get; set; }
        public virtual ICollection<CM_PERMISSIONS> CM_PERMISSIONS { get; set; }

    }
}
