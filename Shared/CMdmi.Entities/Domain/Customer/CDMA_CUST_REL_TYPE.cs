using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Customer
{
  

    [Table("CDMA_CUST_REL_TYPE")]

    public partial class CDMA_CUST_REL_TYPE
    {
        public CDMA_CUST_REL_TYPE()
        {
            CdmaNextOfKins = new HashSet<CDMA_INDIVIDUAL_NEXT_OF_KIN>();
        }

        
        [Key]
        public int REL_CODE { get; set; }
        public string REL_DESC { get; set; }
        public ICollection<CDMA_INDIVIDUAL_NEXT_OF_KIN> CdmaNextOfKins { get; private set; }
    }
}
