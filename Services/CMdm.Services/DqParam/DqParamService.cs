using CMdm.Core;
using CMdm.Data.DAC;
using CMdm.Entities.Domain.Dqi;
using CMdm.Entities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.DqParam
{
    public class DqParamService : IDqParamService
    {
        #region Fields

        //private readonly IRepository<MdmDQQue> _dqqueRepository;
        private DqParamDAC _dqparamDAC;
        #endregion
        #region Ctor
        public DqParamService()
        {
           _dqparamDAC = new DqParamDAC();
        }
        
        #endregion
        #region Methods
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="queitem">QueItem</param>
        public virtual void DeleteQueItem(MdmEntityDetails queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            UpdateParamItem(queitem);

            //event notification
            //_eventPublisher.EntityDeleted(vendor);
        }
        /// <summary>
        /// Inserts a queitem
        /// </summary>
        /// <param name="queitem">Queitem</param>
        public virtual void InsertQueItem(MdmEntityDetails queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _dqparamDAC.Insert(queitem);

            //event notification
            //_eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateParamItem(MdmEntityDetails queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _dqparamDAC.Update(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Gets a queitem by item identifier
        /// </summary>
        /// <param name="recordId">MdmDQQue identifier</param>
        /// <returns>Vendor</returns>
        public virtual MdmEntityDetails GetItembyId(int recordId)
        {
            if (recordId == 0)
                return null;

            return _dqparamDAC.SelectById(recordId);
        }
        /// <summary>
        /// Gets all queitems
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Queitems</returns>
        public virtual IPagedList<MdmEntityDetails> GetAllEntities(string name = null,
            int? dimId = null, int? entityId = null, int? catalogId = null,
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<MdmEntityDetails> result = default(List<MdmEntityDetails>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "CREATED_DATE DESC";
           // Step 1 - Calling Select on the DAC.
            result = _dqparamDAC.Select(name, pageIndex, pageSize, sortExpression, dimId, entityId, catalogId);

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

            var queitems = new PagedList<MdmEntityDetails>(result, pageIndex, pageSize);
            return queitems;
        }

        #endregion
    }
}
