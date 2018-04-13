namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CDMA_ACCT_SERVICES_LOG")]
   

    public partial class CDMA_ACCT_SERVICES_LOG
    {
        [Key]
        public string CUSTOMER_NO { get; set; }       
        public string ACCOUNT_NUMBER { get; set; }
        public string CARD_PREFERENCE { get; set; }
        public string ELECTRONIC_BANKING_PREFERENCE { get; set; }
        public string STATEMENT_PREFERENCES { get; set; }
        public string TRANSACTION_ALERT_PREFERENCE { get; set; }
        public string STATEMENT_FREQUENCY { get; set; }
        public string CHEQUE_BOOK_REQUISITION { get; set; }
        public string CHEQUE_LEAVES_REQUIRED { get; set; }
        public string CHEQUE_CONFIRMATION { get; set; }
        public string CHEQUE_CONFIRMATION_THRESHOLD { get; set; }
        public string ONLINE_TRANSFER_LIMIT { get; set; }
        public string TOKEN { get; set; }
        public string ACCOUNT_SIGNATORY { get; set; }
        public string SECOND_SIGNATORY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string AUTHORISED { get; set; }         
        public string AUTHORISED_BY { get; set; }         
        public DateTime? AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }
        public string TIED { get; set; }


    }
}
