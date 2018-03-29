using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Dqi
{
    /// <summary>
    /// Represents an issue status
    /// </summary>
    public enum IssueStatus
    {
        Open = 1,
        Closed = 2,
        Inprogress = 3,
        Rejected = 4
        //Completed = 5
        //        Acknowledged = 4,
    }
}
