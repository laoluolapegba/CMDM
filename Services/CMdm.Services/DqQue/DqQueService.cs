using CMdm.Core;
using CMdm.Data.DAC;
using CMdm.Entities.Domain.Dqi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.DqQue
{
    public class DqQueService : IDqQueService
    {
        #region Fields

        //private readonly IRepository<MdmDQQue> _dqqueRepository;
        private DqQueDAC _dqqueDAC;
        #endregion
        #region Ctor
        public DqQueService()
        {
           _dqqueDAC = new DqQueDAC();
        }
        
        #endregion
        #region Methods
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="queitem">QueItem</param>
        public virtual void DeleteQueItem(MdmDQQue queitem)
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
        public virtual void InsertQueItem(MdmDQQue queitem)
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
        public virtual void UpdateQueItem(MdmDQQue queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _dqqueDAC.Update(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Gets a queitem by item identifier
        /// </summary>
        /// <param name="recordId">MdmDQQue identifier</param>
        /// <returns>Vendor</returns>
        public virtual MdmDQQue GetQueItembyId(int recordId)
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
        public virtual IPagedList<MdmDQQue> GetAllQueItems(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<MdmDQQue> result = default(List<MdmDQQue>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "Created_Date DESC";
           // Step 1 - Calling Select on the DAC.
            result = _dqqueDAC.Select(name, pageIndex, pageSize, sortExpression);

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

            var queitems = new PagedList<MdmDQQue>(result, pageIndex, pageSize);
            return queitems;
        }

        /// <summary>
        /// Gets all queitems
        /// </summary>
        /// <param name="name">description</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Queitems</returns>
        public virtual IPagedList<MdmDqRunException> GetAllBrnQueIssues(string name = "", int? ruleId =null, string BranchId = null, IssueStatus? issueStatus = null, int? priority = null,
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")  //DateTime? createdOnFrom = null,            DateTime? createdOnTo = null,
        {
            List<MdmDqRunException> result = default(List<MdmDqRunException>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "RUN_DATE DESC";
            // Step 1 - Calling Select on the DAC.
            result = _dqqueDAC.SelectBrnIssues(name,  pageIndex, pageSize, sortExpression, ruleId=null, BranchId, issueStatus, priority); //createdOnFrom = null, createdOnTo = null,


            var queitems = new PagedList<MdmDqRunException>(result, pageIndex, pageSize);
            return queitems;
        }
       
        #endregion
    }
}
