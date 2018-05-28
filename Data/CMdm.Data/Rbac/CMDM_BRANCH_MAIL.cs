using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Data.Rbac
{
    [Table("CMDM_BRANCH_MAIL")]
    public class CMDM_BRANCH_MAIL
    {
        [Key]
        public string BRANCH_ID { get; set; }
        public string BRANCH_NAME { get; set; }
        public string CSO_MAIL { get; set; }
        public string CSM_MAIL { get; set; }
        public string ZSM_MAIL { get; set; }
        public string FINCON_MAIL { get; set; }
        public string BM_MAIL { get; set; }
        public string ZONALHEAD_MAIL { get; set; }
        public string ZONALCONTROLHEAD_MAIL { get; set; }
        public string AMU_MAIL { get; set; }
        public string AMUCSM_MAIL { get; set; }
        public string AMUZSM_MAIL { get; set; }
        public string HRHELPDESK_MAIL { get; set; }
    }
}
