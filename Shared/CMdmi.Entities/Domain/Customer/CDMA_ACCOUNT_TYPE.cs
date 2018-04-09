namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CDMA_ACCOUNT_TYPE")]
    public partial class CDMA_ACCOUNT_TYPE
    {
        public CDMA_ACCOUNT_TYPE()
        {
            CdmaAccountInfo = new HashSet<CDMA_ACCOUNT_INFO>();
        }

        [Key]
        public decimal ACCOUNT_ID { get; set; }
        public string ACCOUNT_NAME { get; set; }

        public ICollection<CDMA_ACCOUNT_INFO> CdmaAccountInfo { get; private set; }
    }
}
