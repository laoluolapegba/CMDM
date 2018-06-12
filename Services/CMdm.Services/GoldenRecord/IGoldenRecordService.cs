using CMdm.Core;
using CMdm.Entities.Domain.GoldenRecord;
using CMdm.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.GoldenRecord
{
    public interface IGoldenRecordService
    {
        /// <summary>
        /// Updates the queitem
        /// </summary>
        /// <param name="queitem">queitem</param>
        void UpdateQueItem(CdmaGoldenRecord queitem);
        /// <summary>
        /// Gets a queitem by item identifiers
        /// </summary>
        /// <param name="recordId">recordId identifier</param>
        /// <returns>Vendor</returns>
        IList<CdmaGoldenRecord> GetQueItembyIds(int[] recordIds);
        /// <summary>
        /// Gets a Queitem by item reference identifier
        /// </summary>
        /// <param name="recordId">que identifier</param>
        /// <returns>Vendor</returns>
        CdmaGoldenRecord GetQueItembyId(int recordId);
        /// <summary>
        /// Gets all items
        /// </summary>
        /// <param name="name"> name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortExpression">A value indicating whether to show hidden records</param>
        /// <returns>Vendors</returns>
        IPagedList<CdmaGoldenRecord> GetAllQueItems(string name = "", string custid = "", string email = "", string accno = "", string bvn = "", int? recordId = null, string BranchId = null,
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "");
            
    }
}
