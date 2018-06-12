using CMdm.Core;
using CMdm.Data.DAC;
using CMdm.Entities.Domain.GoldenRecord;
using CMdm.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.GoldenRecord
{
    public class GoldenRecordService : IGoldenRecordService
    {
        #region Fields

        //private readonly IRepository<MdmDQQue> _dqqueRepository;
        private GoldenRecordDAC _dqqueDAC;
        #endregion
        #region Ctor
        public GoldenRecordService()
        {
           _dqqueDAC = new GoldenRecordDAC();
        }
        
        #endregion
        #region Methods
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="queitem">QueItem</param>
        public virtual void DeleteQueItem(CdmaGoldenRecord queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            UpdateQueItem(queitem);

            //event notification
            //_eventPublisher.EntityDeleted(vendor);
        }
        /// <summary>
        /// Inserts a queitem
        /// </summary>
        /// <param name="queitem">Queitem</param>
        public virtual void InsertQueItem(CdmaGoldenRecord queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _dqqueDAC.Insert(queitem);

            //event notification
            //_eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateQueItem(CdmaGoldenRecord queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _dqqueDAC.Update(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Gets a queitem by item identifiers
        /// </summary>
        /// <param name="recordId">recordId identifier</param>
        /// <returns>Vendor</returns>
        public virtual IList<CdmaGoldenRecord> GetQueItembyIds(int[] recordIds)
        {
            if (recordIds == null || recordIds.Length == 0)
                return null;

            return _dqqueDAC.SelectByIds(recordIds);
        }
        /// <summary>
        /// Gets a queitem by item identifier
        /// </summary>
        /// <param name="recordId">MdmDQQue identifier</param>
        /// <returns>Vendor</returns>
        public virtual CdmaGoldenRecord GetQueItembyId(int recordId)
        {
            if (recordId == 0)
                return null;

            return _dqqueDAC.SelectById(recordId);
        }
        /// <summary>
        /// Gets all queitems
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Queitems</returns>
        public virtual IPagedList<CdmaGoldenRecord> GetAllQueItems(string name = "", string custid = "", string email = "", string accno = "", string bvn = "",
            int? recordId = null, string BranchId = null, int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<CdmaGoldenRecord> result = default(List<CdmaGoldenRecord>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "FULL_NAME DESC";
           // Step 1 - Calling Select on the DAC.
            result = _dqqueDAC.Select(name, custid, email, accno, bvn, pageIndex, pageSize, sortExpression, recordId, BranchId);

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

            var queitems = new PagedList<CdmaGoldenRecord>(result, pageIndex, pageSize);
            return queitems;
        }

        
        #endregion
    }
}
