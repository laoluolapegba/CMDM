﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CMdm.Entities.Domain.Customer
{
    [Table("CDMA_BUSINESS_SIZE")]
    public partial class CDMA_BUSINESS_SIZE
    {
        public CDMA_BUSINESS_SIZE()
        {
            CdmaAccountInfo = new HashSet<CDMA_ACCOUNT_INFO>();
        }

        [Key]
        public decimal SIZE_ID { get; set; }
        public string SIZE_RANGE { get; set; }

        public ICollection<CDMA_ACCOUNT_INFO> CdmaAccountInfo { get; private set; }

    }
}
