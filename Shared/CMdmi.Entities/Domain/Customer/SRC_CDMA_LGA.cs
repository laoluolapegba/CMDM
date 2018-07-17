using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Customer
{
    public partial class SRC_CDMA_LGA
    { 
        public SRC_CDMA_LGA()
        {
            CdmaNextOfKins = new HashSet<CDMA_INDIVIDUAL_NEXT_OF_KIN>();
        }

        
        [Key]
        public int LGA_ID { get; set; }
        public string LGA_NAME { get; set; }
        public decimal STATE_ID { get; set; }
        public ICollection<CDMA_INDIVIDUAL_NEXT_OF_KIN> CdmaNextOfKins { get; private set; }
    }
}
