using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CMDM_ACTIVITY_LOG")]
    public partial class CMDM_ACTIVITY_LOG
    {
        [Key]
        public int ACTIVITY_ID { get; set; }
        public int USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string FULLNAME { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BRANCH_NAME { get; set; }
        public string ACTIVITY_DESC { get; set; }
        public DateTime ACTIVITY_DATE { get; set; }
    }
}
