namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     
    [Table("CDMA_INITIAL_DEPOSIT_RANGE")]
    public partial class CDMA_INITIAL_DEPOSIT_RANGE
    {
        [Key]
        public int DEPOSIT_ID { get; set; }
        public string INITIAL_DEPOSIT_RANGE { get; set; }

    }
}
