using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CDMA_INDUSTRY_SEGMENT")]
    public partial class CDMA_INDUSTRY_SEGMENT
    {
        public CDMA_INDUSTRY_SEGMENT()
        {
            Indsegment = new HashSet<CDMA_EMPLOYMENT_DETAILS>();
        }


        [Key]
        public string SEGMENT_CODE { get; set; }
        public string SEGMENT { get; set; }
        public ICollection<CDMA_EMPLOYMENT_DETAILS> Indsegment { get; private set; }
    }


}
