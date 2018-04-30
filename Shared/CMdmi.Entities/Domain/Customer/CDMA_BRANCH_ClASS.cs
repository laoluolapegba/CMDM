using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CDMA_BRANCH_CLASS")]
    public partial class CDMA_BRANCH_CLASS
    {
        [Key]
        public decimal ID { get; set; }
        public string CLASS { get; set; }
    }

}
