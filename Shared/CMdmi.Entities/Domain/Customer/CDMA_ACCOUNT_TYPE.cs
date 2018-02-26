namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CDMA_ACCOUNT_TYPE")]
    public partial class CDMA_ACCOUNT_TYPE
    {
        [Key]
        public decimal ACCOUNT_ID { get; set; }
        public string ACCOUNT_NAME { get; set; }
    }
}
