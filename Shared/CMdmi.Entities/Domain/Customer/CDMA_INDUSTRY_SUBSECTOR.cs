namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CDMA_INDUSTRY_SUBSECTOR")]
    public partial class CDMA_INDUSTRY_SUBSECTOR
    {
        public CDMA_INDUSTRY_SUBSECTOR()
        {
            Subsectortype = new HashSet<CDMA_EMPLOYMENT_DETAILS>();
        }
        [Key]
        public string SUBSECTOR_CODE { get; set; }
        public string SUBSECTOR_NAME { get; set; }
        public ICollection<CDMA_EMPLOYMENT_DETAILS> Subsectortype { get; private set; }
    }
}
