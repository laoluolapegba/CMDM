using CMdm.Core;
using CMdm.Data.DAC;
using CMdm.Entities.Domain.Dqi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.DqRule
{
    public class DqRuleService : IDqRuleService
    {
        #region Fields

        //private readonly IRepository<MdmDQRule> _dqruleRepository;
        private DqRuleDAC _dqruleDAC;
        #endregion
        #region Ctor
        public DqRuleService()
        {
            _dqruleDAC = new DqRuleDAC();
        }
        
        #endregion
        #region Methods
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="ruleitem">RuleItem</param>
        public virtual void DeleteRuleItem(MdmDqRule ruleitem)
        {
            if (ruleitem == null)
                throw new ArgumentNullException("ruleitem");

            UpdateRuleItem(ruleitem);

            //event notification
            //_eventPublisher.EntityDeleted(vendor);
        }
        /// <summary>
        /// Inserts a ruleitem
        /// </summary>
        /// <param name="ruleitem">ruleitem</param>
        public virtual void InsertRuleItem(MdmDqRule ruleitem)
        {
            if (ruleitem == null)
                throw new ArgumentNullException("ruleitem");

            _dqruleDAC.Insert(ruleitem);

            //event notification
            //_eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Updates the ruleitem
        /// </summary>
        /// <param name="ruleitem">ruleitem</param>
        public virtual void UpdateRuleItem(MdmDqRule ruleitem)
        {
            if (ruleitem == null)
                throw new ArgumentNullException("ruleitem");

            _dqruleDAC.Update(ruleitem);

            //event notification
            //_eventPublisher.EntityUpdated(vendor);
        }
        /// <summary>
        /// Gets a ruleitem by item identifier
        /// </summary>
        /// <param name="recordId">MdmDQRule identifier</param>
        /// <returns>Vendor</returns>
        public virtual MdmDqRule GetRuleItembyId(int recordId)
        {
            if (recordId == 0)
                return null;

            return _dqruleDAC.SelectById(recordId);
        }
        /// <summary>
        /// Gets all ruleitems
        /// </summary>
        /// <param name="name">Vendor name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Ruleitems</returns>
        public virtual IPagedList<MdmDqRule> GetAllRuleItems(string name = "", int? dimensionId = null,
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "")
        {
            List<MdmDqRule> result = default(List<MdmDqRule>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "LAST_RUN DESC";
           // Step 1 - Calling Select on the DAC.
            result = _dqruleDAC.Select(name, pageIndex, pageSize, sortExpression, dimensionId) ;

            // Step 2 - Get count.
            //totalRowCount = _dqruleDAC.Count(name); i dont need this cos i can do items.totalcount

            //return result;

            //var rulery = _dqruleRepository.Table;
            //if (!String.IsNullOrWhiteSpace(name))
            //    rulery = rulery.Where(v => v.ERROR_DESC.Contains(name));
            //// if (!showHidden)
            ////    rulery = rulery.Where(v => v.Active);
            ////rulery = rulery.Where(v => !v.Deleted);
            //rulery = query.OrderBy(v => v.CREATED_DATE).ThenBy(v => v.ERROR_DESC);

            var queitems = new PagedList<MdmDqRule>(result, pageIndex, pageSize);
            return queitems;
        }

        #endregion
    }
}
