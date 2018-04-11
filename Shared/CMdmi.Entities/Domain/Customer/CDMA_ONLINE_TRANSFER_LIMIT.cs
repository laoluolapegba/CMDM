using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CDMA_ONLINE_TRANSFER_LIMIT")]
    public partial class CDMA_ONLINE_TRANSFER_LIMIT
    {
        public CDMA_ONLINE_TRANSFER_LIMIT()
        {
            CdmaAccountServices = new HashSet<CDMA_ACCT_SERVICES_REQUIRED>();
        }

        [Key]
        public decimal LIMIT_ID { get; set; }
        public string DESCRIPTION { get; set; }

        public ICollection<CDMA_ACCT_SERVICES_REQUIRED> CdmaAccountServices { get; private set; }
    }
}
