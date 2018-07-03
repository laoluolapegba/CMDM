namespace CMdm.Data.Rbac
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CM_PERMISSIONS")]
    public partial class CM_PERMISSIONS
    {
        public CM_PERMISSIONS()
        {
            CM_ROLE_PERM_XREF = new HashSet<CM_ROLE_PERM_XREF>();
            CM_USER_ROLES = new HashSet<CM_USER_ROLES>();
        }


        [Key]
        public int PERMISSION_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string PERMISSIONDESCRIPTION { get; set; }

        [Required]
        [StringLength(25)]
        public string ACTION_NAME { get; set; }

        [Required]
        [StringLength(25)]
        public string CONTROLLER_NAME { get; set; }

        public int PARENT_PERMISSION { get; set; }

        [StringLength(100)]
        public string FORM_URL { get; set; }

        [StringLength(100)]
        public string IMAGE_URL { get; set; }
        public string ICON_CLASS { get; set; }
        public string ISOPEN_CLASS { get; set; }
        public string TOGGLE_ICON { get; set; }

        public bool ISACTIVE { get; set; }

        public virtual ICollection<CM_ROLE_PERM_XREF> CM_ROLE_PERM_XREF { get; set; }

        public virtual ICollection<CM_USER_ROLES> CM_USER_ROLES { get; set; }

    }
}
