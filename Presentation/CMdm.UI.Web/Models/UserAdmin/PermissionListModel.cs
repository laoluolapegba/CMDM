using CMdm.Data.Rbac;
using CMdm.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.UserAdmin
{
    public class PermissionListModel :BaseEntityModel
    {
        public PermissionListModel()
        {
            Permissions = new List<SelectListItem>();
            UserRoles = new List<CM_USER_ROLES>();
        }
        [DisplayName("Search Permission")]
        [AllowHtml]
        public string SearchPermission { get; set; }
        //public decimal Id { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Permission Description")]
        public string PERMISSIONDESCRIPTION { get; set; }

        [Required]
        [StringLength(25)]
        [DisplayName("Action")]
        public string ACTION_NAME { get; set; }

        [Required]
        [StringLength(25)]
        [DisplayName("Controller")]
        public string CONTROLLER_NAME { get; set; }
        [DisplayName("Parent Permission")]

        public int PARENT_PERMISSION { get; set; }

        [StringLength(100)]
        [DisplayName("Form Url")]
        public string FORM_URL { get; set; }

        [StringLength(100)]
        public string IMAGE_URL { get; set; }
        public string ICON_CLASS { get; set; }
        public string ISOPEN_CLASS { get; set; }
        public string TOGGLE_ICON { get; set; }
        [DisplayName("Display on Menu?")]

        public bool ISACTIVE { get; set; }

        public IList<SelectListItem> Permissions { get; set; }

        //public IList<SelectListItem> UserRoles { get; set; }
        public ICollection<CM_USER_ROLES> UserRoles { get; set; }
    }
}