using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Configuration
{
    [Table("CMDM_SETTING")]
    public partial class Setting
    {
        
        [Key]
        public int ID { get; set; }
        public string SETTING_NAME { get; set; }
        public string SETTING_VALUE { get; set; }
    }
}
