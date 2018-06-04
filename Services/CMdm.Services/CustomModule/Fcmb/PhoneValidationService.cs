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
    public class PhoneValidationService : IPhoneValidationService
    {
        #region Fields

        //private readonly IRepository<MdmDQQue> _dqqueRepository;
        private PhoneValidationDAC _pvDAC;
        #endregion
        #region Ctor
        public PhoneValidationService()
        {
            _pvDAC = new PhoneValidationDAC();
        }

        #endregion
        #region PhoneValidation
        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdatePhoneValidations(PhoneValidation queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _pvDAC.UpdatePhoneValidation(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="queitem">QueItem</param>
        public virtual void DeleteQueItem(PhoneValidation queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            UpdatePhoneValidations(queitem);

            //event notification
            //_eventPublisher.EntityDeleted(vendor);
        }
        /// <summary>
        /// Inserts a queitem
        /// </summary>
        /// <param name="queitem">Queitem</param>
        public virtual void InsertPhoneValidation(PhoneValidation queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _pvDAC.InsertPhoneValidation(queitem);

            //event notification
            //_eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdatePhoneValidation(PhoneValidation queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _pvDAC.UpdatePhoneValidation(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Gets a queitem by item identifiers
        /// </summary>
        /// <param name="recordId">recordId identifier</param>
        /// <returns>Vendor</returns>
        public virtual IList<PhoneValidation> GetPhoneValidationbyIds(int[] recordIds)
        {
            if (recordIds == null || recordIds.Length == 0)
                return null;

            return _pvDAC.SelectByIds(recordIds);
        }
        /// <summary>
        /// Gets a queitem by item identifier
        /// </summary>
        /// <param name="recordId">MdmDQQue identifier</param>
        /// <returns>Vendor</returns>
        public virtual PhoneValidation GetPValbyId(int recordId)
        {
            if (recordId == 0)
                return null;

            return _pvDAC.SelectPhoneValidationById(recordId);
        }
        /// <summary>
        /// Gets all queitems
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Queitems</returns>
        public virtual IPagedList<PhoneValidation> GetAllPhoneValidations(string custId = "", string fname = "", string mname = "", string lname = "",
            string branchCode = "", int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<PhoneValidation> result = default(List<PhoneValidation>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "LAST_RUN_DATE DESC";
            // Step 1 - Calling Select on the DAC.
            result = _pvDAC.SelectPhoneValidation(custId, fname, mname, lname, branchCode, pageIndex, pageSize, sortExpression);

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

            var queitems = new PagedList<PhoneValidation>(result, pageIndex, pageSize);
            return queitems;
        }
        #endregion PhoneValidation
    }
}
