using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.CustomModule.Fcmb
{
    [Table("VW_DISTINCT_REF_CODE")]
    public partial class DistinctRefCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string ACCOUNTOFFICER_NAME { get; set; }
        public string REF_CODE { get; set; }
    }
}
