namespace CMdm.Entities.Domain.GoldenRecord
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   
    [Table("CDMA_GOLDEN_RECORD")]
    public partial class CdmaGoldenRecord
    {
        public CdmaGoldenRecord()
        {
           // EntityDetails = new HashSet<MdmEntityDetails>();
        }

        public int GOLDEN_RECORD { get; set; }

        [Required]
        [StringLength(50)]
        public string CUSTOMER_NO { get; set; }

        [StringLength(150)]
        public string BVN { get; set; }

        [Required]
        [StringLength(300)]
        public string FULL_NAME { get; set; }


        public DateTime? DATE_OF_BIRTH { get; set; }

        [StringLength(500)]
        public string RESIDENTIAL_ADDRESS { get; set; }

        public string CUSTOMER_TYPE { get; set; }

        [StringLength(20)]
        public string SEX { get; set; }
        public string BRANCH_CODE { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RECORD_ID { get; set; }
        public string RECORD_STATUS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string EMAIL { get; set; }
        public string SCHEME_CODE { get; set; }
        public string ACCOUNT_NO { get; set; }
    }
}
