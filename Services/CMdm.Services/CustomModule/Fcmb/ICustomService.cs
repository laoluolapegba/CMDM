using CMdm.Core;
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
    public interface ICustomService
    {

        /// <summary>
        /// Gets a Queitem by item reference identifier
        /// </summary>
        /// <param name="recordId">que identifier</param>
        /// <returns>Vendor</returns>
        OutStandingDoc GetOutDocItembyId(int recordId);
        /// <summary>
        /// Gets all items
        /// </summary>
        /// <param name="name"> name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortExpression">A value indicating whether to show hidden records</param>
        /// <returns>Vendors</returns>
        IPagedList<OutStandingDoc> GetAllOutDocItems(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "");
          }
}
