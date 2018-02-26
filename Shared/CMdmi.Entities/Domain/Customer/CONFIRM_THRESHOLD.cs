namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    [Table("CDMA_CHEQUE_CONFIRM_THRESHOLD")]
    public partial class CONFIRM_THRESHOLD
    {
        [Key]
        public decimal INCOME_ID { get; set; }
        public string EXPECTED_INCOME_BAND { get; set; }
       
    }
}
