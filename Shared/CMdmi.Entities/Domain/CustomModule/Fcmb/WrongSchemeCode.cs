using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.CustomModule.Fcmb
{
    [Table("VW_WRONGSCHCODECLASS")]
    public partial class WrongSchemeCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string CIF_ID { get; set; }
        public string FORACID { get; set; }
        public string SCHM_CODE { get; set; }
        public string ACCOUNTOFFICER_CODE { get; set; }
        public string ACCOUNTOFFICER_NAME { get; set; }
        public string SCHMECODE_CLASSIFIATION { get; set; }
        public string ACCT_NAME { get; set; }
        public string SOL_ID { get; set; }
        public string CUSTOMER_TYPE { get; set; }
        public DateTime? DATE_OF_RUN { get; set; }
    }
}
