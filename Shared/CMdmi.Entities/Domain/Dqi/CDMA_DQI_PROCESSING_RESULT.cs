using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CMdm.Entities.Domain.Dqi
{
    [Table("CDMA_DQI_PROCESSING_RESULT")]
    public partial class CDMA_DQI_PROCESSING_RESULT
    {
        [Key]
        public int PROCESS_ID { get; set; }
        public string BRANCH_CODE { get; set; }
        public int? DQI_RESULT { get; set; }
        public int? PREVIOUS_DQI_RESULT { get; set; }
        public string MDM_CATALOG_ID { get; set; }
        public DateTime? DAT_LAST_RUN { get; set; }
    }
}
