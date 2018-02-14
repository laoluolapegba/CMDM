using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Data.Rbac
{
    public partial class SRC_CDMA_LGA
    { 

        [Key]
        public decimal LGA_ID { get; set; }
        public string LGA_NAME { get; set; }
        public decimal STATE_ID { get; set; }
    }
}
