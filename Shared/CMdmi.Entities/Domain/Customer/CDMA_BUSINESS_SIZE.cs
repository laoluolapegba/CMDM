using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CMdm.Entities.Domain.Customer
{
    [Table("CDMA_BUSINESS_SIZE")]
    public partial class CDMA_BUSINESS_SIZE
    {
        [Key]
        public decimal SIZE_ID { get; set; }
        public string SIZE_RANGE { get; set; }

    }
}
