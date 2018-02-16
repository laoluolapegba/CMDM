using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Dqi
{
    [Table("CDMA_INDIVIDUAL_BRANCH_DQI")]
    public partial class BranchDqiSummary
    {
        [Key]
        public string BRANCH_CODE { get; set; }
        public string TABLE_NAME { get; set; }
        public string ATTRIBUTE { get; set; }
        public decimal DQI { get; set; }
    }
}
