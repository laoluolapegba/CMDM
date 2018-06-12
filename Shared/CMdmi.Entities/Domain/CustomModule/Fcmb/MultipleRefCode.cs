using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.CustomModule.Fcmb
{
    [Table("VW_MULTIPLE_REF_CODE")]
    public partial class MultipleRefCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string FORACID { get; set; }
        public string DUPLICATION_ID { get; set; }
        public string ACCOUNTOFFICER_NAME { get; set; }
        public string REF_CODE { get; set; }
        public string SOL_ID { get; set; }
        public string CIF_ID { get; set; }
        public DateTime? RUN_DATE { get; set; }
        public string SCHM_CODE { get; set; }
    }
}
