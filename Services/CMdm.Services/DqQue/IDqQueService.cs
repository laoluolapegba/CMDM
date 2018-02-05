using CMdm.Core;
using CMdm.Entities.Domain.Dqi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.DqQue
{
    public interface IDqQueService
    {
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
        IPagedList<MdmDqRunException> GetAllBrnQueIssues(string name = "", int? ruleId = null,  int? BranchId = null, int? status = null, int? priority = null,
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = ""); //DateTime? createdOnFrom = null,        DateTime? createdOnTo = null,

    }
}
