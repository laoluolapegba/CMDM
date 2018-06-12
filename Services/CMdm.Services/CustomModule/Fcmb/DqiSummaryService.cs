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
    public class DqiSummaryService : IDqiSummaryService
    {
        #region Fields

        //private readonly IRepository<MdmDQQue> _dqqueRepository;
        private DqiSummaryDAC _dqDAC;
        #endregion
        #region Ctor
        public DqiSummaryService()
        {
            _dqDAC = new DqiSummaryDAC();
        }

        #endregion
        #region DqiSummary
        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateDqiSummaries(DqiSummary queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _dqDAC.UpdateDqiSummary(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="queitem">QueItem</param>
        public virtual void DeleteQueItem(DqiSummary queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            UpdateDqiSummaries(queitem);

            //event notification
            //_eventPublisher.EntityDeleted(vendor);
        }
        /// <summary>
        /// Inserts a queitem
        /// </summary>
        /// <param name="queitem">Queitem</param>
        public virtual void InsertDqiSummary(DqiSummary queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _dqDAC.InsertDqiSummary(queitem);

            //event notification
            //_eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateDqiSummary(DqiSummary queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _dqDAC.UpdateDqiSummary(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Gets a queitem by item identifiers
        /// </summary>
        /// <param name="recordId">recordId identifier</param>
        /// <returns>Vendor</returns>
        public virtual IList<DqiSummary> GetDqiSummarybyIds(int[] recordIds)
        {
            if (recordIds == null || recordIds.Length == 0)
                return null;

            return _dqDAC.SelectByIds(recordIds);
        }
        /// <summary>
        /// Gets a queitem by item identifier
        /// </summary>
        /// <param name="recordId">MdmDQQue identifier</param>
        /// <returns>Vendor</returns>
        public virtual DqiSummary GetDqiSummarybyId(int recordId)
        {
            if (recordId == 0)
                return null;

            return _dqDAC.SelectDqiSummaryById(recordId);
        }
        /// <summary>
        /// Gets all queitems
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Queitems</returns>
        public virtual IPagedList<DqiSummary> GetAllDqiSummary(string branchCode = "", int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<DqiSummary> result = default(List<DqiSummary>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "DAT_LAST_RUN DESC";
            // Step 1 - Calling Select on the DAC.
            result = _dqDAC.SelectDqiSummary(branchCode, pageIndex, pageSize, sortExpression);

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

            var queitems = new PagedList<DqiSummary>(result, pageIndex, pageSize);
            return queitems;
        }
        #endregion DqiSummary
    }
}
