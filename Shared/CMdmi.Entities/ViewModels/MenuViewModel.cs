using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.ViewModels
{
    public class MenuViewModel
    {
        

        public decimal PermissionId { get; set; }

        public int RoleId { get; set; }
        public string PermissionDesc { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public int ParentPermission { get; set; }
        public string IconClass { get; set; }
        public string IsopenClass { get; set; }
        public string ToggleIconClass { get; set; }
        public string Url;

    }
}
