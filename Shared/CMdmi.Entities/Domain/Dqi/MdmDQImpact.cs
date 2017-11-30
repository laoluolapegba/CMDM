using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CMdm.Entities.Domain.Dqi
{
    [Table("MDM_DQ_IMPACT")]
    public partial class MdmDQImpact
    {
        public MdmDQImpact()
        {
            MdmDQQues = new HashSet<MdmDQQue>();
        }
        [Key]
        public int IMPACT_CODE { get; set; }
        public string IMPACT_DESCRIPTION { get; set; }
        public virtual ICollection<MdmDQQue> MdmDQQues { get; set; }
    }
}
