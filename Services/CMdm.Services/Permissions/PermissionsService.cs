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
    public class PermissionsService : IPermissionsService
    {
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
