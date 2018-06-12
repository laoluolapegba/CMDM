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
    public class WrongSchemeCodeService : IWrongSchemeCodeService
    {
        #region Fields

        //private readonly IRepository<MdmDQQue> _dqqueRepository;
        private WrongSchemeCodesDAC _wscDAC;
        #endregion
        #region Ctor
        public WrongSchemeCodeService()
        {
            _wscDAC = new WrongSchemeCodesDAC();
        }

        #endregion
        #region WrongSchemeCode
        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateWrongSchemeCodes(WrongSchemeCode queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _wscDAC.UpdateWrongSchemeCode(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="queitem">QueItem</param>
        public virtual void DeleteQueItem(WrongSchemeCode queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            UpdateWrongSchemeCodes(queitem);

            //event notification
            //_eventPublisher.EntityDeleted(vendor);
        }
        /// <summary>
        /// Inserts a queitem
        /// </summary>
        /// <param name="queitem">Queitem</param>
        public virtual void InsertWrongSchemeCode(WrongSchemeCode queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _wscDAC.InsertWrongSchemeCode(queitem);

            //event notification
            //_eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateWrongSchemeCode(WrongSchemeCode queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _wscDAC.UpdateWrongSchemeCode(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Gets a queitem by item identifiers
        /// </summary>
        /// <param name="recordId">recordId identifier</param>
        /// <returns>Vendor</returns>
        public virtual IList<WrongSchemeCode> GetWrongSchemeCodebyIds(int[] recordIds)
        {
            if (recordIds == null || recordIds.Length == 0)
                return null;

            return _wscDAC.SelectByIds(recordIds);
        }
        /// <summary>
        /// Gets a queitem by item identifier
        /// </summary>
        /// <param name="recordId">MdmDQQue identifier</param>
        /// <returns>Vendor</returns>
        public virtual WrongSchemeCode GetWrongSchCodebyId(int recordId)
        {
            if (recordId == 0)
                return null;

            return _wscDAC.SelectWrongSchemeCodeById(recordId);
        }
        /// <summary>
        /// Gets all queitems
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Queitems</returns>
        public virtual IPagedList<WrongSchemeCode> GetAllWrongSchemeCodes(string accno = "", string custid = "", string branchCode = "",
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<WrongSchemeCode> result = default(List<WrongSchemeCode>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "DATE_OF_RUN DESC";
            // Step 1 - Calling Select on the DAC.
            result = _wscDAC.SelectWrongSchemeCode(accno, custid, branchCode, pageIndex, pageSize, sortExpression);

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

            var queitems = new PagedList<WrongSchemeCode>(result, pageIndex, pageSize);
            return queitems;
        }
        #endregion WrongSchemeCode
    }
}
