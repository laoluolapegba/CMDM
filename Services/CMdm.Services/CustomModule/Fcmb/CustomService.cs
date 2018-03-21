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
    public class CustomService : ICustomService
    {
        #region Fields

        //private readonly IRepository<MdmDQQue> _dqqueRepository;
        private CustomActionsDAC _dqqueDAC;
        #endregion
        #region Ctor
        public CustomService()
        {
           _dqqueDAC = new CustomActionsDAC();
        }

        #endregion
        #region Methods
        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateOutStandingDocItem(OutStandingDoc queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _dqqueDAC.UpdateOutstandingDoc(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="queitem">QueItem</param>
        public virtual void DeleteQueItem(OutStandingDoc queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            UpdateOutStandingDocItem(queitem);

            //event notification
            //_eventPublisher.EntityDeleted(vendor);
        }
        /// <summary>
        /// Inserts a queitem
        /// </summary>
        /// <param name="queitem">Queitem</param>
        public virtual void InsertOutDocItem(OutStandingDoc queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _dqqueDAC.InsertOutstandingDoc(queitem);

            //event notification
            //_eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateOutDocItem(OutStandingDoc queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _dqqueDAC.UpdateOutstandingDoc(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }

        /// <summary>
        /// Gets a queitem by item identifier
        /// </summary>
        /// <param name="recordId">MdmDQQue identifier</param>
        /// <returns>Vendor</returns>
        public virtual OutStandingDoc GetOutDocItembyId(int recordId)
        {
            if (recordId == 0)
                return null;

            return _dqqueDAC.SelectOutStandingDocById(recordId);
        }
        /// <summary>
        /// Gets all queitems
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Queitems</returns>
        public virtual IPagedList<OutStandingDoc> GetAllOutDocItems(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<OutStandingDoc> result = default(List<OutStandingDoc>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "DUE_DATE DESC";
           // Step 1 - Calling Select on the DAC.
            result = _dqqueDAC.SelectOutStandingDoc(name, pageIndex, pageSize, sortExpression);

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

            var queitems = new PagedList<OutStandingDoc>(result, pageIndex, pageSize);
            return queitems;
        }

       

        #endregion
    }
}
