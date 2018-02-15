using CMdm.Data;
using CMdm.Data.Identity.Models;
using CMdm.Data.Rbac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.Security
{
    public class PermissionService
    {
        #region Fields

        public int ProfileId { get; set; }
        public string UserId { get; set; }
        public int RoleId { get; set; }
        private List<CM_USER_ROLES> Roles = new List<CM_USER_ROLES>();
        #endregion
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>

        public PermissionService(string userId, int roleId)
        {
            this.UserId = userId;
            this.RoleId = roleId;
        }

        #endregion
        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionName">Permission record system name</param>
        /// <param name="user">User</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string permissionName, ApplicationUser user)
        {
            if (String.IsNullOrEmpty(permissionName))
                return false;

            //var customerRoles = customer.CustomerRoles.Where(cr => cr.Active);
            //foreach (var role in customerRoles)
            //    if (Authorize(permissionRecordSystemName, role))
            //        //yes, we have such permission
            //        return true;
            //no permission found
            return false;
        }
        public bool HasPermission(string requiredController, string requiredAction)
        {
            bool bFound = false;
            //bFound = (role.Permissions.Where(p => p.PermissionDescription.ToLower() == requiredPermission.ToLower()).ToList().Count > 0);
            foreach (CM_USER_ROLES role in this.Roles)
            {
                bFound = (role.CM_PERMISSIONS.Where(p => p.CONTROLLER_NAME.ToLower() == requiredController.ToLower()   && p.ACTION_NAME == requiredAction).ToList().Count > 0);
                if (bFound)
                    break;
            }
            return bFound;
        }
        private void GetDatabaseUserRolesPermissions()
        {
            using (AppDbContext _data = new AppDbContext())
            {
                CM_USER_PROFILE _user = _data.CM_USER_PROFILE.Where(u => u.USER_ID == this.UserId).FirstOrDefault();
                if (_user != null)
                {
                    this.ProfileId = (int)_user.PROFILE_ID;

                    CM_USER_ROLES _userRole = _data.CM_USER_ROLES.Where(u => u.ROLE_ID == this.RoleId).FirstOrDefault();

                    foreach (CM_PERMISSIONS _permission in _userRole.CM_PERMISSIONS)
                    {
                        _userRole.CM_PERMISSIONS.Add(new CM_PERMISSIONS { PERMISSION_ID = (int)_permission.PERMISSION_ID, PERMISSIONDESCRIPTION = _permission.PERMISSIONDESCRIPTION });
                    }
                    this.Roles.Add(_userRole);
                    //foreach (CM_USER_ROLES _role in _user.CM_USER_ROLES)
                    //{
                    //    UserRole _userRole = new UserRole { Role_Id = _role.ROLE_ID, RoleName = _role.ROLE_NAME };

                    //    this.Roles.Add(_userRole);

                    //    //if (!this.IsSysAdmin)
                    //    //    this.IsSysAdmin = (bool)!_role.IS_DEFAULT;
                    //}
                }
            }
        }
    }
}
