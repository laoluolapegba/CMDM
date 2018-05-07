using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMdm.Entities.Domain.Kpi
{
    [Table("CMDM_COMMON_KPI")]
    public partial class BrnKpi
    {
        public DateTime TRAN_DATE { get; set; }
        [Key]
        public string BRANCH_CODE { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public int BRN_CUST_COUNT { get; set; }

        public decimal BRN_DQI { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public int BRN_OPEN_EXCEPTIONS { get; set; }

        public decimal BRN_PCT_CONTRIB { get; set; }

        public int BRN_RECURRING_ERRORS { get; set; }

        public int BRN_RESOLVED_ERRORS { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public int BANK_CUST_COUNT { get; set; }
    }
}