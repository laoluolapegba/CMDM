namespace CMdm.Entities.Domain.Customer
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("SRC_CDMA_STATE")]
public partial class SRC_CDMA_STATE
    {
        [Key]
        public decimal STATE_ID { get; set; }
        public string STATE_NAME { get; set; }
       

    }
}
