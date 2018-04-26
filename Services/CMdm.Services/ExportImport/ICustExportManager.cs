using System;
using CMdm.Entities.Domain.CustomModule.Fcmb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.ExportImport
{
    public partial interface ICustExportManager
    {
        /// <summary>
        /// Export documents list to XLSX
        /// </summary>
        /// <param name="documents">documents</param>
        byte[] ExportDocumentsToXlsx(IList<CustSegment> documents);
    }
}
