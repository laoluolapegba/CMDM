using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.CustomModule.Fcmb
{
    [Table("VW_PHONEVALIDATION_RESULTS")]
    public partial class PhoneValidation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string CUSTOMER_NO { get; set; }
        public string BRANCH_CODE { get; set; }
        public string CUST_LAST_NAME { get; set; }
        public string CUST_MIDDLE_NAME { get; set; }
        public string CUST_FIRST_NAME { get; set; }
        public DateTime? LAST_RUN_DATE { get; set; }
        public string REASON { get; set; }
        public string PHONE_NO { get; set; }
        public string TYPE { get; set; }
        public string BRANCH_NAME { get; set; }
    }
}
