namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    [Table("CDMA_IDENTIFICATION_TYPE")]

    public partial class CDMA_IDENTIFICATION_TYPE
    {
        public CDMA_IDENTIFICATION_TYPE()
        {
            CdmaNextOfKins = new HashSet<CDMA_INDIVIDUAL_NEXT_OF_KIN>();
        }

        
        [Key]
        public int CODE { get; set; }
        public string ID_TYPE { get; set; }
        public ICollection<CDMA_INDIVIDUAL_NEXT_OF_KIN> CdmaNextOfKins { get; private set; }
    }
}
