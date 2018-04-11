using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Customer
{
    
    [Table("CDMA_BUSINESS_SEGMENT")]
    public partial class CDMA_BUSINESS_SEGMENT
    {
        public CDMA_BUSINESS_SEGMENT()
        {
            CdmaAccountInfo = new HashSet<CDMA_ACCOUNT_INFO>();
        }

        [Key]
        public decimal ID { get; set; }
        public string SEGMENT { get; set; }

        public ICollection<CDMA_ACCOUNT_INFO> CdmaAccountInfo { get; private set; }
    }
}
