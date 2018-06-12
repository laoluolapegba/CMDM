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
    public class MultipleRefCodeService : IMultipleRefCodeService
    {
        #region Fields

        //private readonly IRepository<MdmDQQue> _dqqueRepository;
        private MultipleRefCodeDAC _mrcDAC;
        private DistinctRefCodeDAC _drcDAC;
        #endregion
        #region Ctor
        public MultipleRefCodeService()
        {
            _mrcDAC = new MultipleRefCodeDAC();
            _drcDAC = new DistinctRefCodeDAC();
        }

        #endregion
        #region MultipleRefCode
        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateMultipleRefCodes(MultipleRefCode queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _mrcDAC.UpdateMultipleRefCode(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="queitem">QueItem</param>
        public virtual void DeleteQueItem(MultipleRefCode queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            UpdateMultipleRefCodes(queitem);

            //event notification
            //_eventPublisher.EntityDeleted(vendor);
        }
        /// <summary>
        /// Inserts a queitem
        /// </summary>
        /// <param name="queitem">Queitem</param>
        public virtual void InsertMultipleRefCode(MultipleRefCode queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _mrcDAC.InsertMultipleRefCode(queitem);

            //event notification
            //_eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateMultipleRefCode(MultipleRefCode queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _mrcDAC.UpdateMultipleRefCode(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Gets a queitem by item identifiers
        /// </summary>
        /// <param name="recordId">recordId identifier</param>
        /// <returns>Vendor</returns>
        public virtual IList<MultipleRefCode> GetMultipleRefCodebyIds(int[] recordIds)
        {
            if (recordIds == null || recordIds.Length == 0)
                return null;

            return _mrcDAC.SelectByIds(recordIds);
        }
        /// <summary>
        /// Gets a queitem by item identifier
        /// </summary>
        /// <param name="recordId">MdmDQQue identifier</param>
        /// <returns>Vendor</returns>
        public virtual MultipleRefCode GetMultRefCodebyId(int recordId)
        {
            if (recordId == 0)
                return null;

            return _mrcDAC.SelectMultipleRefCodeById(recordId);
        }
        /// <summary>
        /// Gets all queitems
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Queitems</returns>
        public virtual IPagedList<MultipleRefCode> GetAllMultipleRefCodes(string name = "", string refCode = "", string branchCode = "",
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<MultipleRefCode> result = default(List<MultipleRefCode>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "FORACID DESC";
            // Step 1 - Calling Select on the DAC.
            result = _mrcDAC.SelectMultipleRefCode(name, refCode, branchCode, pageIndex, pageSize, sortExpression);

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

            var queitems = new PagedList<MultipleRefCode>(result, pageIndex, pageSize);
            return queitems;
        }
        #endregion MultipleRefCode
        #region DistinctRefCode
        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateDistinctRefCodes(DistinctRefCode queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _drcDAC.UpdateDistinctRefCode(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="queitem">QueItem</param>
        public virtual void _DeleteQueItem(DistinctRefCode queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            UpdateDistinctRefCodes(queitem);

            //event notification
            //_eventPublisher.EntityDeleted(vendor);
        }
        /// <summary>
        /// Inserts a queitem
        /// </summary>
        /// <param name="queitem">Queitem</param>
        public virtual void InsertDistinctRefCode(DistinctRefCode queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _drcDAC.InsertDistinctRefCode(queitem);

            //event notification
            //_eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateDistinctRefCode(DistinctRefCode queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _drcDAC.UpdateDistinctRefCode(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Gets a queitem by item identifiers
        /// </summary>
        /// <param name="recordId">recordId identifier</param>
        /// <returns>Vendor</returns>
        public virtual IList<DistinctRefCode> GetDistinctRefCodebyIds(int[] recordIds)
        {
            if (recordIds == null || recordIds.Length == 0)
                return null;

            return _drcDAC.SelectByIds(recordIds);
        }
        /// <summary>
        /// Gets a queitem by item identifier
        /// </summary>
        /// <param name="recordId">MdmDQQue identifier</param>
        /// <returns>Vendor</returns>
        public virtual DistinctRefCode GetDistRefCodebyId(int recordId)
        {
            if (recordId == 0)
                return null;

            return _drcDAC.SelectDistinctRefCodeById(recordId);
        }
        /// <summary>
        /// Gets all queitems
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Queitems</returns>
        public virtual IPagedList<DistinctRefCode> GetAllDistinctRefCodes(string accountofficer = "", string refCode = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<DistinctRefCode> result = default(List<DistinctRefCode>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "ACCOUNTOFFICER_NAME ASC";
            // Step 1 - Calling Select on the DAC.
            result = _drcDAC.SelectDistinctRefCode(accountofficer, refCode,  pageIndex, pageSize, sortExpression);

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

            var queitems = new PagedList<DistinctRefCode>(result, pageIndex, pageSize);
            return queitems;
        }
        #endregion DistinctRefCode
    }
}
