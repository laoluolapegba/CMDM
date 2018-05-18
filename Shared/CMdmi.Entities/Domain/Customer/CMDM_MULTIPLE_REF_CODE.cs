using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CMDM_MULTIPLE_REF_CODE")]
    public partial class CMDM_MULTIPLE_REF_CODE
    {
        [Key]
        public string FORACID { get; set; }
        public string DUPLICATION_ID { get; set; }
        public string ACCOUNTOFFICER_NAME { get; set; }
        public string REF_CODE { get; set; }
        public string SOL_ID { get; set; }
        public string CIF_ID { get; set; }
    }
}
