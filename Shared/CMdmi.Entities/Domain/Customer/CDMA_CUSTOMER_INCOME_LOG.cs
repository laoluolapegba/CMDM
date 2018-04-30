namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CDMA_CUSTOMER_INCOME_LOG")]
    public partial class CDMA_CUSTOMER_INCOME_LOG
    {
        [Key]
        public string CUSTOMER_NO { get; set; }
        public string INCOME_BAND { get; set; }
        public string INITIAL_DEPOSIT { get; set; }       
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public DateTime? AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }
        public string TIED { get; set; }
    }
}
