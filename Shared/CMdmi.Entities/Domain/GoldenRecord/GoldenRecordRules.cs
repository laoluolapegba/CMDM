using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.GoldenRecord
{
        using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   
    [Table("CDMA_GOLDEN_RECORD_RULES")]
    public partial class GoldenRecordRules
    {
        public GoldenRecordRules()
        {
           
        }
        [Key]
        public int RULE_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string RULE_NAME { get; set; }

        [StringLength(150)]
        public string RULE_DESCRIPTION { get; set; }


        public bool RULE_STATUS { get; set; }

        
    }
}
