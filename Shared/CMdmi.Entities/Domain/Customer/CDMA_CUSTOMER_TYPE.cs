using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CDMA_CUSTOMER_TYPE")]

    public partial class CDMA_CUSTOMER_TYPE
    {
        [Key]
        public decimal TYPE_ID { get; set; }
        public string CUSTOMER_TYPE { get; set; }
    }
}
