using CMdm.Core;
using CMdm.Entities.Domain.Dqi;
using CMdm.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.DqQue
{
    public interface IDqQueService
    {
        void ApproveExceptionQueItems(string selectedIds);  //List<MdmDqRunException> queitems
        void DisApproveExceptionQueItems(string selectedIds, string comments);
        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        void UpdateExceptionQueItem(MdmDqRunException queitem);
        /// <summary>
        /// Gets a queitem by item identifiers
        /// </summary>
        /// <param name="recordId">recordId identifier</param>
        /// <returns>Vendor</returns>
        IList<MdmDqRunException> GetQueItembyIds(int[] recordIds);
        /// <summary>
        /// Gets a Queitem by item reference identifier
        /// </summary>
        /// <param name="recordId">que identifier</param>
        /// <returns>Vendor</returns>
        MdmDqRunException GetQueDetailItembyId(int recordId);
        /// <summary>
        /// Gets a Queitem by item reference identifier
        /// </summary>
        /// <param name="recordId">que identifier</param>
        /// <returns>Vendor</returns>
        MdmDQQue GetQueItembyId(int recordId);
        /// <summary>
        /// Gets all items
        /// </summary>
        /// <param name="name"> name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortExpression">A value indicating whether to show hidden records</param>
        /// <returns>Vendors</returns>
        IPagedList<MdmDQQue> GetAllQueItems(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "");
        IPagedList<MdmDqRunException> GetAllBrnQueIssues(string name = "", int? catalogId = null, string customerId = null, int? ruleId = null,  string BranchId = null, IssueStatus? issueStatus = null, int? priority = null,
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = ""); //DateTime? createdOnFrom = null,        DateTime? createdOnTo = null,
        IPagedList<CustExceptionsModel> GetAllBrnUnAuthIssues(string name = "", int? catalogId = null, string customerId = null, int? ruleId = null, string BranchId = null, IssueStatus? issueStatus = null, int? priority = null,
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = ""); //DateTime? createdOnFrom = null,        DateTime? createdOnTo = null,

    }
}
