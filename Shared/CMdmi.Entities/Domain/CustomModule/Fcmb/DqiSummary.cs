using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.CustomModule.Fcmb
{
    [Table("VW_DQI_PROCESSING_RESULT")]
    public partial class DqiSummary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public int? PROCESS_ID { get; set; }
        public string BRANCH_CODE { get; set; }
        public int? DQI_RESULT { get; set; }
        public int? PREVIOUS_DQI_RESULT { get; set; }
        public string MDM_CATALOG_ID { get; set; }
        public DateTime? DAT_LAST_RUN { get; set; }
    }
}
