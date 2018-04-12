using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CMdm.Entities.Domain.Customer
{
    [Table("TMP_ACCOUNTOFFICER_EXCEPTIONS")]
    public partial class TMP_ACCOUNTOFFICER_EXCEPTIONS
    {
        [Key]
        public string ACCOUNT_NUMBER { get; set; }
        public string ACCOUNT_NAME { get; set; }
        public string SOL_ID { get; set; }
        public string BRANCH { get; set; }
        public string SCHM_CODE { get; set; }
        public DateTime? ACCT_OPN_DATE { get; set; }
        public string AO_CODE { get; set; }
        public string AO_NAME { get; set; }
        public string SBU_CODE { get; set; }
        public string SBU_NAME { get; set; }
        public string BROKER_CODE { get; set; }
        public string BROKER_NAME { get; set; }
        public DateTime? RUN_DATE { get; set; }
    }
}
