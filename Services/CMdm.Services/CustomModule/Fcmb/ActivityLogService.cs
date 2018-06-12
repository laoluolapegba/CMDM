using CMdm.Core;
using CMdm.Data.DAC;
using CMdm.Entities.Domain.CustomModule.Fcmb;
using CMdm.Entities.Domain.Dqi;
using CMdm.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.CustomModule.Fcmb
{
    public class ActivityLogService : IActivityLogService
    {
        #region Fields

        //private readonly IRepository<MdmDQQue> _dqqueRepository;
        private ActivityLogDAC _alDAC;
        #endregion
        #region Ctor
        public ActivityLogService()
        {
            _alDAC = new ActivityLogDAC();
        }

        #endregion
        #region ActivityLog
        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateActivityLogs(ActivityLog queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _alDAC.UpdateActivityLog(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="queitem">QueItem</param>
        public virtual void DeleteQueItem(ActivityLog queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            UpdateActivityLogs(queitem);

            //event notification
            //_eventPublisher.EntityDeleted(vendor);
        }
        /// <summary>
        /// Inserts a queitem
        /// </summary>
        /// <param name="queitem">Queitem</param>
        public virtual void InsertActivityLog(ActivityLog queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _alDAC.InsertActivityLog(queitem);

            //event notification
            //_eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateActivityLog(ActivityLog queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _alDAC.UpdateActivityLog(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Gets a queitem by item identifiers
        /// </summary>
        /// <param name="recordId">recordId identifier</param>
        /// <returns>Vendor</returns>
        public virtual IList<ActivityLog> GetActivityLogbyIds(int[] recordIds)
        {
            if (recordIds == null || recordIds.Length == 0)
                return null;

            return _alDAC.SelectByIds(recordIds);
        }
        /// <summary>
        /// Gets a queitem by item identifier
        /// </summary>
        /// <param name="recordId">MdmDQQue identifier</param>
        /// <returns>Vendor</returns>
        public virtual ActivityLog GetAccLogbyId(int recordId)
        {
            if (recordId == 0)
                return null;

            return _alDAC.SelectActivityLogById(recordId);
        }
        /// <summary>
        /// Gets all queitems
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Queitems</returns>
        public virtual IPagedList<ActivityLog> GetAllActivityLogs(string username = "", string fullname = "", string branchCode = "",
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<ActivityLog> result = default(List<ActivityLog>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "ACTIVITY_DATE DESC";
            // Step 1 - Calling Select on the DAC.
            result = _alDAC.SelectActivityLog(username, fullname, branchCode, pageIndex, pageSize, sortExpression);

            // Step 2 - Get count.
            //totalRowCount = _dqqueDAC.Count(name); i dont need this cos i can do items.totalcount

            //return result;

            //var query = _dqqueRepository.Table;
            //if (!String.IsNullOrWhiteSpace(name))
            //    query = query.Where(v => v.ERROR_DESC.Contains(name));
            //// if (!showHidden)
            ////    query = query.Where(v => v.Active);
            ////query = query.Where(v => !v.Deleted);
            //query = query.OrderBy(v => v.CREATED_DATE).ThenBy(v => v.ERROR_DESC);

            var queitems = new PagedList<ActivityLog>(result, pageIndex, pageSize);
            return queitems;
        }
        #endregion ActivityLog
    }
}
