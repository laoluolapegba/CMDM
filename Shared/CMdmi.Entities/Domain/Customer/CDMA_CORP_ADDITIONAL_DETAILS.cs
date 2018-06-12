using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CDMA_CORP_ADDITIONAL_DETAILS")]
    public partial class CDMA_CORP_ADDITIONAL_DETAILS
    {
        [Key, Column(Order = 0)]
        [DisplayName("Customer NO")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Branch name")]
        public string BRANCH_CODE { get; set; }
        [DisplayName("Counterparties Clients of Customer")]
        public string COUNTERPARTIES_CLIENTS_OF_CUST { get; set; }
        [DisplayName("Parent Company")]
        public string PARENT_COMPANY_CTRY_INCORP { get; set; }
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
