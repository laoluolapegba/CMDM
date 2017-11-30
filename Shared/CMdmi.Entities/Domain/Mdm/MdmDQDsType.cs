using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Mdm
{
    [Table("MDM_DQ_DSTYPE")]
    public class MdmDQDsType
    {
        public MdmDQDsType ()
        {
            MdmDQDataSources = new HashSet<MdmDQDataSource>();
        }
        [Key]
        [DisplayName("Type Id")]
        public int TYPE_ID { get; set; }
        [DisplayName("Name")]
        public string TYPE_NAME { get; set; }
        public virtual ICollection<MdmDQDataSource> MdmDQDataSources { get; set; }
    }
}
