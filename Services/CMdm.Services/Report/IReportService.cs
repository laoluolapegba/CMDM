using CMdm.Entities.Domain.Dqi;
using CMdm.Entities.Domain.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.Report
{
    /// <summary>
    /// Report Service interface
    /// </summary>
    public interface IReportService
    {
        MdmDqRunException GetPendingExceptions(ReportRequestModel reportParams);
    }
}
