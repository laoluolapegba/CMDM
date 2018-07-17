using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CDMA_FOREIGN_DETAILS")]
    public partial class CDMA_FOREIGN_DETAILS
    {
        [Key, Column(Order = 0)]
        public string CUSTOMER_NO { get; set; }
        public string FOREIGNER { get; set; }
        public string PASSPORT_RESIDENCE_PERMIT { get; set; }
        public DateTime? PERMIT_ISSUE_DATE { get; set; }
        public DateTime? PERMIT_EXPIRY_DATE { get; set; }
        public string FOREIGN_ADDRESS { get; set; }
        public string CITY { get; set; }
        public int? COUNTRY { get; set; }
        public string ZIP_POSTAL_CODE { get; set; }
        public string FOREIGN_TEL_NUMBER { get; set; }
        public string PURPOSE_OF_ACCOUNT { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }
        [Key, Column(Order = 1)]
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public DateTime? AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }
        public string MULTIPLE_CITEZENSHIP { get; set; }

        public virtual CDMA_COUNTRIES Countries { get; set; }
    }
}
