using CMdm.Data;
using CMdm.Data.Identity.Models;
using CMdm.Core;
using CMdm.Data.DAC;
using CMdm.Data.Rbac;
using CMdm.Entities.Domain.Dqi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.Security
{
    public class PermissionsService : IPermissionsService
    {
        #region Fields

        public int ProfileId { get; set; }
        public string UserId { get; set; }
        public int RoleId { get; set; }
        private List<CM_USER_ROLES> Roles = new List<CM_USER_ROLES>();
        private List<CM_PERMISSIONS> Permissions = new List<CM_PERMISSIONS>();
        #endregion
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>

        public PermissionsService(string userId, int roleId)
        {
            this.UserId = userId;
            this.RoleId = roleId;
            GetDatabaseUserRolesPermissions();
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
        public bool HasRole(string role)
        {
            return (Roles.Where(p => p.ROLE_NAME == role).ToList().Count > 0);
        }
        public bool IsLevel(CMdm.Entities.Domain.User.AuthorizationLevel AuthLevel)
        {
            int level = (int)AuthLevel;
            return (Roles.Where(p => p.ROLE_ID == RoleId &&  p.USER_LEVEL == level).ToList().Count > 0);
        }
        private void GetDatabaseUserRolesPermissions()
        {
            using (AppDbContext _data = new AppDbContext())
            {
                CM_USER_PROFILE _user = _data.CM_USER_PROFILE.Where(u => u.USER_ID.ToUpper() == this.UserId.ToUpper()).FirstOrDefault();
                if (_user != null)
                {
                    this.ProfileId = (int)_user.PROFILE_ID;

                    CM_USER_ROLES _userRole = _data.CM_USER_ROLES.Where(u => u.ROLE_ID == this.RoleId).FirstOrDefault();

                    List<CM_ROLE_PERM_XREF> _permxref = _data.CM_ROLE_PERM_XREF.Where(p => p.ROLE_ID == _userRole.ROLE_ID).ToList();

                    foreach (CM_ROLE_PERM_XREF _permission in _permxref)
                    {
                        //_userRole.CM_PERMISSIONS.Add(new CM_PERMISSIONS { PERMISSION_ID = (int)_permission.PERMISSION_ID, PERMISSIONDESCRIPTION = _permission.PERMISSIONDESCRIPTION });
                        Permissions.Add(new CM_PERMISSIONS { PERMISSION_ID = (int)_permission.PERMISSION_ID, PERMISSIONDESCRIPTION = "" });
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
        #region Fields


        private PermissionsDAC _permissionsDAC;
        #endregion
        #region Ctor
        public PermissionsService()
        {
            _permissionsDAC = new PermissionsDAC();
        }

        #endregion
        #region Methods
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="queitem">QueItem</param>
        public virtual void DeleteQueItem(CM_PERMISSIONS queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("permission");

            UpdateQueItem(queitem);

            //event notification
            //_eventPublisher.EntityDeleted(vendor);
        }
        /// <summary>
        /// Inserts a queitem
        /// </summary>
        /// <param name="queitem">Queitem</param>
        public virtual void InsertQueItem(CM_PERMISSIONS queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("permission");

            _permissionsDAC.Insert(queitem);

            //event notification
            //_eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateQueItem(CM_PERMISSIONS queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("permission");

            _permissionsDAC.Update(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Gets a queitem by item identifier
        /// </summary>
        /// <param name="recordId">CM_USER_PROFILE identifier</param>
        /// <returns>Vendor</returns>
        public virtual CM_PERMISSIONS GetItembyId(int recordId)
        {
            if (recordId == 0)
                return null;

            return _permissionsDAC.SelectById(recordId);
        }
        /// <summary>
        /// Gets all queitems
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Queitems</returns>
        public virtual IPagedList<CM_PERMISSIONS> GetAllPermissions(string permdesc = null, int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<CM_PERMISSIONS> result = default(List<CM_PERMISSIONS>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "PERMISSIONDESCRIPTION DESC";
            // Step 1 - Calling Select on the DAC.
            result = _permissionsDAC.Select(pageIndex, pageSize, sortExpression, permdesc);

            // Step 2 - Get count.
            //totalRowCount = _useradminDAC.Count(name); i dont need this cos i can do items.totalcount

            //return result;

            //var query = _dqqueRepository.Table;
            //if (!String.IsNullOrWhiteSpace(name))
            //    query = query.Where(v => v.ERROR_DESC.Contains(name));
            //// if (!showHidden)
            ////    query = query.Where(v => v.Active);
            ////query = query.Where(v => !v.Deleted);
            //query = query.OrderBy(v => v.CREATED_DATE).ThenBy(v => v.ERROR_DESC);

            var queitems = new PagedList<CM_PERMISSIONS>(result, pageIndex, pageSize);
            return queitems;
        }

        public virtual IPagedList<CM_PERMISSIONS> GetAllRoles(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<CM_PERMISSIONS> result = default(List<CM_PERMISSIONS>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "PERMISSIONDESCRIPTION DESC";
            result = _permissionsDAC.SelectRoles(name, pageIndex, pageSize, sortExpression);

            var queitems = new PagedList<CM_PERMISSIONS>(result, pageIndex, pageSize);
            return queitems;
        }

        #endregion
    }
}
