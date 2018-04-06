namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CDMA_INCOME_BAND")]
    public partial class CDMA_INCOME_BAND
    {
        [Key]
        public int INCOME_ID { get; set; }
        public string EXPECTED_INCOME_BAND { get; set; }
        
    }
}
