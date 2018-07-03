namespace CMdm.Entities.Domain.CustomModule.Fcmb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   
    [Table("VW_OUTSTANDING_DOCUMENTATION")]
    public partial class OutStandingDoc
    {
        public OutStandingDoc()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string FORACID { get; set; }

        [Required]
        
        public string ACCT_NAME { get; set; }

        
        public string SOL_ID { get; set; }

        
        public string SCHM_CODE { get; set; }

        
        public string SCHM_DESC { get; set; }


        public string SCHM_TYPE { get; set; }
        public string ACID { get; set; }

        public string DOCUMENT_CODE { get; set; }

        public string REF_DESC { get; set; }
        public DateTime? DUE_DATE { get; set; }

        public string FREZ_REASON_CODE { get; set; }

        public string ACCTOFFICER_CODE { get; set; }

        public string ACCTOFFICER_NAME { get; set; }

        public string BRANCH_NAME { get; set; }

        public string CIF_ID { get; set; }

    }
}
