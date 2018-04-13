using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CDMA_ADDITIONAL_INFORMATION")]
    public partial class CDMA_ADDITIONAL_INFORMATION
    {
        [Key, Column(Order = 0)]
        public string CUSTOMER_NO { get; set; }
        public string ANNUAL_SALARY_EXPECTED_INC { get; set; }
        public string FAX_NUMBER { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }
        [Key, Column(Order = 1)]
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public DateTime? AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }
    }
}
