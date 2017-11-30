using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CMdm.Entities.Domain.Dqi
{
    [Table("MDM_DQ_RUNSCHEDULE")]
    public partial class MdmDqRunSchedule
    {
        public MdmDqRunSchedule()
        {
            MdmDqRules = new HashSet<MdmDqRule>();
        }
        [Key]
        [DisplayName("Schedule Id")]
        public int SCHEDULE_ID { get; set; }
        [DisplayName("Running Schedule")]
        public string SCHEDULE_DESCRIPTION { get; set; }
        public virtual ICollection<MdmDqRule> MdmDqRules { get; set; }
    }
}
