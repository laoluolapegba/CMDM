﻿

namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("CDMA_INDIVIDUAL_CONTACT_DETAIL_LOG")]

    public partial class CDMA_INDIVIDUAL_CONTACT_DETAIL_LOG
    {

        [Key]
        public string CUSTOMER_NO { get; set; }
        public string MOBILE_NO { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public string MAILING_ADDRESS { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }
        public string AUTHORISED { get; set; }        
        public string AUTHORISED_BY { get; set; }
        public Nullable<System.DateTime> AUTHORISED_DATE { get; set; }         
        public string IP_ADDRESS { get; set; }
         

    }
}
