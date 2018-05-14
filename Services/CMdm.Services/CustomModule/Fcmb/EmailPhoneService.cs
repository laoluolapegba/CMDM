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
    public class EmailPhoneService : IEmailPhoneService
    {
        #region Fields

        //private readonly IRepository<MdmDQQue> _dqqueRepository;
        private EmailPhoneDAC _emailPhoneDAC;
        #endregion
        #region Ctor
        public EmailPhoneService()
        {
            _emailPhoneDAC = new EmailPhoneDAC();
        }

        #endregion
        #region EmailPhone
        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateEmailPhones(EmailPhone queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _emailPhoneDAC.UpdateEmailPhone(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="queitem">QueItem</param>
        public virtual void DeleteQueItem(EmailPhone queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            UpdateEmailPhone(queitem);

            //event notification
            //_eventPublisher.EntityDeleted(vendor);
        }
        /// <summary>
        /// Inserts a queitem
        /// </summary>
        /// <param name="queitem">Queitem</param>
        public virtual void InsertEmailPhone(EmailPhone queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _emailPhoneDAC.InsertEmailPhone(queitem);

            //event notification
            //_eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        public virtual void UpdateEmailPhone(EmailPhone queitem)
        {
            if (queitem == null)
                throw new ArgumentNullException("queitem");

            _emailPhoneDAC.UpdateEmailPhone(queitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Gets a queitem by item identifiers
        /// </summary>
        /// <param name="recordId">recordId identifier</param>
        /// <returns>Vendor</returns>
        public virtual IList<EmailPhone> GetEmailPhonebyIds(int[] recordIds)
        {
            if (recordIds == null || recordIds.Length == 0)
                return null;

            return _emailPhoneDAC.SelectByIds(recordIds);
        }
        /// <summary>
        /// Gets a queitem by item identifier
        /// </summary>
        /// <param name="recordId">MdmDQQue identifier</param>
        /// <returns>Vendor</returns>
        public virtual EmailPhone GetEmailPhonebyId(int recordId)
        {
            if (recordId == 0)
                return null;

            return _emailPhoneDAC.SelectEmailPhoneById(recordId);
        }
        /// <summary>
        /// Gets all queitems
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Queitems</returns>
        public virtual IPagedList<EmailPhone> GetAllEmailPhones(string custid = "", string firstname = "", string midname = "", string lastname = "",
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<EmailPhone> result = default(List<EmailPhone>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "CUST_DOB DESC";
            // Step 1 - Calling Select on the DAC.
            result = _emailPhoneDAC.SelectEmailPhone(custid, firstname, midname, lastname, pageIndex, pageSize, sortExpression);

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

            var queitems = new PagedList<EmailPhone>(result, pageIndex, pageSize);
            return queitems;
        }
        #endregion EmailPhone
    }
}
