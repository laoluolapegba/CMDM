using CMdm.Core;
using CMdm.Data.Rbac;
using CMdm.Entities.Domain.Dqi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.UserAdmin
{
    public interface IUserService
    {
        /// <summary>
        /// Gets a item by item reference identifier
        /// </summary>
        /// <param name="recordId">que identifier</param>
        /// <returns>Vendor</returns>
        CM_USER_PROFILE GetItembyId(int recordId);
        /// <summary>
        /// Gets all items
        /// </summary>
        /// <param name="name"> name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortExpression">A value indicating whether to show hidden records</param>
        /// <returns>Vendors</returns>
        IPagedList<CM_USER_PROFILE> GetAllUsers(int[] userRoleIds = null, string email = null, string username = null,
            string firstName = null, string lastName = null,
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "");
        IPagedList<CM_USER_ROLES> GetAllRoles(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "");
    }
}
