using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CDMA_COMPANY_INFORMATION")]
    public partial class CDMA_COMPANY_INFORMATION
    {
        [Key, Column(Order = 0)]
        [DisplayName("Customer NO")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Company Name")]
        public string COMPANY_NAME { get; set; }
        [DisplayName("Date of Incorporation Registration")]
        public DateTime? DATE_OF_INCORP_REGISTRATION { get; set; }
        [DisplayName("Customer Type")]
        public string CUSTOMER_TYPE { get; set; }
        [DisplayName("Registered Address")]
        public string REGISTERED_ADDRESS { get; set; }
        [DisplayName("Category of Business")]
        public string CATEGORY_OF_BUSINESS { get; set; }
        [DisplayName("Branch name")]
        public string BRANCH_CODE { get; set; }
        [DisplayName("EOC Risk")]
        public string EOC_RISK { get; set; }
        [DisplayName("Current Line of Business")]
        public string CURRENT_LINE_OF_BUSINESS { get; set; }
        [DisplayName("Company Networth")]
        public int? COMPANY_NETWORTH_SOA { get; set; }
        [DisplayName("Introduced By")]
        public string INTRODUCED_BY { get; set; }
        [DisplayName("Brf Investigation Media Report")]
        public string BRF_INVESTIGATION_MEDIA_REPORT { get; set; }
        [DisplayName("Investigation Media Report")]
        public string INVESTIGATION_MEDIA_REPORT { get; set; }
        [DisplayName("Add Verification Status")]
        public string ADD_VERIFICATION_STATUS { get; set; }
        [DisplayName("Conterparties Clients of Customer")]
        public string COUNTERPARTIES_CLIENTS_OF_CUST { get; set; }
        [DisplayName("Anticipated Trans Outflow")]
        public int? ANTICIPATED_TRANS_OUTFLOW { get; set; }
        [DisplayName("Anticipated Trans Inflow")]
        public int? ANTICIPATED_TRANS_INFLOW { get; set; }
        [DisplayName("Length of Stay Inbus")]
        public int? LENGTH_OF_STAY_INBUS { get; set; }
        [DisplayName("Date of Commencement")]
        public DateTime? DATE_OF_COMMENCEMENT { get; set; }
        [DisplayName("Source of Asset")]
        public string SOURCE_OF_ASSET { get; set; }
        [DisplayName("History of Customer")]
        public string HISTORY_OF_CUSTOMER { get; set; }
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
