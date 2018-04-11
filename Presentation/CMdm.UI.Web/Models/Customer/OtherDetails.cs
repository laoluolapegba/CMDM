namespace CMdm.UI.Web.Models.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;
    public class OtherDetails
    {
       public string CUSTOMER_NO { get; set; }
        [DisplayName("TIN Number")]
        public string TIN_NO { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> LAST_MODIFIED_DATE { get; set; }

        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public Nullable<System.DateTime> AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }
    }
}