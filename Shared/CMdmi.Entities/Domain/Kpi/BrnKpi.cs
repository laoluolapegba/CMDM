using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMdm.Entities.Domain.Kpi
{
    public class BrnKpi
    {
        public DateTime TRAN_DATE { get; set; }
        public string BRANCH_CODE { get; set; }
        public int BRN_CUST_COUNT { get; set; }
        public decimal BRN_DQI { get; set; }
        public int BRN_OPEN_EXCEPTIONS { get; set; }
        public decimal BRN_PCT_CONTRIB { get; set; }
        public int BRN_RECURRING_ERRORS { get; set; }
        public int BRN_RESOLVED_ERRORS { get; set; }
        public int BANK_CUST_COUNT { get; set; }
        

    }
}