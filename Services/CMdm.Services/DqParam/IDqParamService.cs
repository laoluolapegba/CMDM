using CMdm.Core;
using CMdm.Entities.Domain.Dqi;
using CMdm.Entities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.DqParam
{
    public interface IDqParamService
    {
        /// <summary>
        /// Gets a Queitem by item reference identifier
        /// </summary>
        /// <param name="recordId">que identifier</param>
        /// <returns>Vendor</returns>
        MdmEntityDetails GetItembyId(int recordId);
        /// <summary>
        /// Gets all items
        /// </summary>
        /// <param name="name"> name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortExpression">A value indicating whether to show hidden records</param>
        /// <returns>Vendors</returns>
        IPagedList<MdmEntityDetails> GetAllEntities(string name = "", int? dimId = null, int? entityId = null, int? catalogId = null,
            int pageIndex = 0, int pageSize = int.MaxValue, string sortExpression = "");
        /// <summary>
        /// Updates the record
        /// </summary>
        /// <param name="queitem">MdmEntityDetails</param>
        void UpdateParamItem(MdmEntityDetails queitem);

    }
}
