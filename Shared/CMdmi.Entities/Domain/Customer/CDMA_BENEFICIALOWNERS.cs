using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CDMA_BENEFICIALOWNERS")]
    public partial class CDMA_BENEFICIALOWNERS
    {
        [Key, Column(Order = 0)]
        [DisplayName("Customer NO")]
        public string ORGKEY { get; set; }
        [DisplayName("Percentage of Beneficiary")]
        public decimal? PERCENTAGE_OF_BENEFICIARY { get; set; }
        [DisplayName("Names of Beneficiary")]
        public string NAMES_OF_BENEFICIARY { get; set; }
        [DisplayName("Branch name")]
        public string PRIMARY_SOL_ID { get; set; }
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
