using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CDMA_JURAT")]
    public partial class CDMA_JURAT
    {
        [Key, Column(Order = 0)]
        public string CUSTOMER_NO { get; set; }
        public DateTime? DATE_OF_OATH { get; set; }
        public string NAME_OF_INTERPRETER { get; set; }
        public string ADDRESS_OF_INTERPRETER { get; set; }
        public string TELEPHONE_NO { get; set; }
        public string LANGUAGE_OF_INTERPRETATION { get; set; }
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
