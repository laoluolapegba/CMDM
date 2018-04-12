using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CDMA_CHEQUE_CONFIRM_THRESHOLD")]
    public partial class CDMA_CHEQUE_CONFIRM_THRESHOLD
    {

        [Key]
        public decimal INCOME_ID { get; set; }
        public string EXPECTED_INCOME_BAND { get; set; }
    }
}
