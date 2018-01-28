using CMdm.Core;
using CMdm.Core.Data;
using CMdm.Entities.Domain.Dqi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.DqQue
{
    public class DqQueServiceNopStyle
    {
        #region Fields

        //private readonly IRepository<MdmDQQue> _dqqueRepository;

        #endregion
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

            //_dqqueRepository.Insert(queitem);

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

            //_dqqueRepository.Update(queitem);

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

            return null; // _dqqueRepository.GetById(recordId);
        }
        /// <summary>
        /// Gets all queitems
        /// </summary>
        /// <param name="name">Vendor name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Queitems</returns>
        //public virtual IPagedList<MdmDQQue> GetAllQueItems(string name = "",
        //    int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        //{
        //   // var query = null// _dqqueRepository.Table;
        //   // if (!String.IsNullOrWhiteSpace(name))
        //   //     query = query.Where(v => v.ERROR_DESC.Contains(name));
        //   //// if (!showHidden)
        //   // //    query = query.Where(v => v.Active);
        //   // //query = query.Where(v => !v.Deleted);
        //   // query = query.OrderBy(v => v.CREATED_DATE).ThenBy(v => v.ERROR_DESC);

        //   // var queitems = new PagedList<MdmDQQue>(query, pageIndex, pageSize);
        //   // return queitems;
        //}

    }
}
