namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CDMA_OCCUPATION_LIST")]
    public partial class CDMA_OCCUPATION_LIST
    {
        public CDMA_OCCUPATION_LIST()
        {
            OccupationList = new HashSet<CDMA_EMPLOYMENT_DETAILS>();
        }


        [Key]
        public decimal OCCUPATION_CODE { get; set; }
        public string OCCUPATION { get; set; }

        public ICollection<CDMA_EMPLOYMENT_DETAILS> OccupationList { get; private set; }
    }
}
