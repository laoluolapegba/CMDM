using CMdm.Core;
using CMdm.Data.Identity.Models;
using CMdm.Data.Rbac;
using CMdm.Entities.Domain.Dqi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.Security
{
    public interface IPermissionsService
    {
        /// <summary>
        /// Gets a item by item reference identifier
        /// </summary>
        /// <param name="recordId">que identifier</param>
        /// <returns>Vendor</returns>
        CM_PERMISSIONS GetItembyId(int recordId);
        /// <summary>
        /// Gets all items
        /// </summary>
        /// <param name="name"> name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortExpression">A value indicating whether to show hidden records</param>
        /// <returns>Vendors</returns>
        IPagedList<CM_PERMISSIONS> GetAllPermissions(string permdesc,int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "");
        IPagedList<CM_PERMISSIONS> GetAllRoles(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "");
        bool HasPermission(string requiredController, string requiredAction);
        bool Authorize(string permissionName, ApplicationUser user);
        bool HasRole(string role);
        bool IsLevel(CMdm.Entities.Domain.User.AuthorizationLevel AuthLevel);

    }
}
