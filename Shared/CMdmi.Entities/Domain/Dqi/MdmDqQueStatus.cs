using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CMdm.Entities.Domain.Dqi
{
    [Table("MDM_DQ_QUESTATUS")]
    public partial class MdmDQQueStatus
    {
        public MdmDQQueStatus()
        {
            MdmDQQues = new HashSet<MdmDQQue>();
            MdmDqRunExceptions = new HashSet<MdmDqRunException>();
        }
        [Key]
        public int STATUS_CODE { get; set; }
        public string STATUS_DESCRIPTION { get; set; }
        public virtual ICollection<MdmDQQue> MdmDQQues { get; set; }
        public  virtual ICollection<MdmDqRunException> MdmDqRunExceptions { get; set; }
    }
}
