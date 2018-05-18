using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.CustomModule.Fcmb
{
    [Table("VW_EMAIL_PHONENO")]
    public partial class EmailPhone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
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
    }
}
