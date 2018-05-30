using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CMDM_CMMN_EMAIL_PHONENO")]
    public partial class CMDM_CMMN_EMAIL_PHONENO
    {
        [Key]
        public string ORGKEY { get; set; }
        public string DUPLICATE_ID { get; set; }
        public string CUST_FIRST_NAME { get; set; }
        public string CUST_MIDDLE_NAME { get; set; }
        public string CUST_LAST_NAME { get; set; }
        public DateTime? CUST_DOB { get; set; }
        public string BVN { get; set; }
        public string GENDER { get; set; }
        public string CUSTOMERMINOR { get; set; }
        public string PREFERREDPHONE { get; set; }
        public string EMAIL { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BRANCH_NAME { get; set; }
    }
}
