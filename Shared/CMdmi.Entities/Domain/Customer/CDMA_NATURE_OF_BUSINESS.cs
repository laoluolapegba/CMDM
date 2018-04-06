namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CDMA_NATURE_OF_BUSINESS")]
    public partial class CDMA_NATURE_OF_BUSINESS
    {
        public CDMA_NATURE_OF_BUSINESS()
        {
            Businessnature = new HashSet<CDMA_EMPLOYMENT_DETAILS>();
        }

        [Key]
        public decimal BUSINESS_CODE { get; set; }
        public string BUSINESS { get; set; }

        public ICollection<CDMA_EMPLOYMENT_DETAILS> Businessnature { get; private set; }
    }
}
