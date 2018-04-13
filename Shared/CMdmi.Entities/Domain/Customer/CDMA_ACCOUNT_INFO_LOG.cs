namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     
    [Table("CDMA_ACCOUNT_INFO_LOG")]
    public partial class CDMA_ACCOUNT_INFO_LOG
    {
        [Key]
        public string CUSTOMER_NO { get; set; }
        public string TYPE_OF_ACCOUNT { get; set; }
        public string ACCOUNT_NUMBER { get; set; }
        public string ACCOUNT_OFFICER { get; set; }
        public string ACCOUNT_TITLE { get; set; }
        public string BRANCH { get; set; }
        public string BRANCH_CLASS { get; set; }
        public string BUSINESS_DIVISION { get; set; }
        public string BUSINESS_SEGMENT { get; set; }
        public string BUSINESS_SIZE { get; set; }
        public string BVN_NUMBER { get; set; }
        public string CAV_REQUIRED { get; set; }
        public string CHEQUE_CONFIRM_THRESHLDRANGE { get; set; }
        public string TIED { get; set; }
        public string ONLINE_TRANSFER_LIMIT_RANGE { get; set; }
        public string CUSTOMER_IC { get; set; }
        public string CUSTOMER_SEGMENT { get; set; }
        public string CUSTOMER_TYPE { get; set; }
        public string OPERATING_INSTRUCTION { get; set; }
        public string ORIGINATING_BRANCH { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public DateTime? AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }
    }
}
