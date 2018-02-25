using CMdm.Core;
using CMdm.Data.DAC;
using CMdm.Data.Rbac;
using CMdm.Entities.Domain.Dqi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.UserAdmin
{
    public class UserService : IUserService
    {
        #region Fields

        
        private UserAdminDAC _useradminDAC;
        #endregion
        #region Ctor
        public UserService()
        {
            _useradminDAC = new UserAdminDAC();
        }
        
        #endregion
        #region Methods
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="queitem">QueItem</param>
        public virtual void DeleteQueItem(CM_USER_PROFILE queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("user");

            UpdateQueItem(queitem);

            //event notification
            //_eventPublisher.EntityDeleted(vendor);
        }
        /// <summary>
        /// Inserts a queitem
        /// </summary>
        /// <param name="queitem">Queitem</param>
        public virtual void InsertQueItem(CM_USER_PROFILE queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _useradminDAC.Insert(queitem);

            //event notification
            //_eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateQueItem(CM_USER_PROFILE queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _useradminDAC.Update(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Gets a queitem by item identifier
        /// </summary>
        /// <param name="recordId">CM_USER_PROFILE identifier</param>
        /// <returns>Vendor</returns>
        public virtual CM_USER_PROFILE GetItembyId(int recordId)
        {
            if (recordId == 0)
                return null;

            return _useradminDAC.SelectById(recordId);
        }
        /// <summary>
        /// Gets all queitems
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Queitems</returns>
        public virtual IPagedList<CM_USER_PROFILE> GetAllUsers(int[] userRoleIds = null, string email = null, string username = null,
            string firstName = null, string lastName = null,
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<CM_USER_PROFILE> result = default(List<CM_USER_PROFILE>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "CREATED_DATE DESC";
           // Step 1 - Calling Select on the DAC.
            result = _useradminDAC.Select(pageIndex, pageSize, sortExpression, userRoleIds, email, username,
             firstName, lastName);

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

            var queitems = new PagedList<CM_USER_PROFILE>(result, pageIndex, pageSize);
            return queitems;
        }

        public virtual IPagedList<CM_USER_ROLES> GetAllRoles(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<CM_USER_ROLES> result = default(List<CM_USER_ROLES>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "Created_Date DESC";
            result = _useradminDAC.SelectRoles(name, pageIndex, pageSize, sortExpression);

            var queitems = new PagedList<CM_USER_ROLES>(result, pageIndex, pageSize);
            return queitems;
        }

        #endregion
    }
}
