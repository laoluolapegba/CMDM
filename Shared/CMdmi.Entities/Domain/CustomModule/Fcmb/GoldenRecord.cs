using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.CustomModule.Fcmb
{
    [Table("VW_GOLDEN_RECORD")]
    public partial class GoldenRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public int GOLDEN_RECORD { get; set; }
        public string CUSTOMER_NO { get; set; }
        public string BVN { get; set; }
        public string FULL_NAME { get; set; }
        public DateTime? DATE_OF_BIRTH { get; set; }
        public string RESIDENTIAL_ADDRESS { get; set; }
        public string CUSTOMER_TYPE { get; set; }
        public string SEX { get; set; }
        public string BRANCH_CODE { get; set; }
        public int RECORD_ID { get; set; }
        public string RECORD_STATUS { get; set; }
    }
}
