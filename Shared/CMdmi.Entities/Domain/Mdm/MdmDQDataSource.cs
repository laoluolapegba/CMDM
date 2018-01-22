using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using CMdm.Entities.Domain.Dqi;

namespace CMdm.Entities.Domain.Mdm
{
    [Table("MDM_DQ_DATASOURCE")]
    public partial class MdmDQDataSource 
    {
        public MdmDQDataSource()
        {
            MdmDqRules = new HashSet<MdmDqRule>();
        }
        [Key]
        [DisplayName("Datasource Id")]
        public int DS_ID { get; set; }
        [DisplayName("Datasource Username")]
        public string DS_USERNAME { get; set; }
        public string DS_PASSWORD { get; set; }
        public string DS_HOSTNAME { get; set; }
        public string DS_SID { get; set; }
        [DisplayName("Datasource Name")]
        public string DS_NAME { get; set; }
        [DisplayName("Type")]
        public int DS_TYPE { get; set; }
        [DisplayName("Status")]
        public string STATUS { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }
        public virtual MdmDQDsType MdmDQDsTypes { get; set; }

        public virtual ICollection<MdmDqRule> MdmDqRules { get; set; }
    }
}
