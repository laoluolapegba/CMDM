namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    [Table("CDMA_INDIVIDUAL_BIODATA_LOG")]
    public partial class CDMA_INDIVIDUAL_PROFILE_LOG
    {
        [Key]
        public decimal LOG_ID { get; set; }
        public string AFFECTED_CATEGORY { get; set; }
        public string CUSTOMER_NO { get; set; }
        public decimal LOGGED_BY { get; set; }
        public string TIED { get; set; }
        public string COMMENTS { get; set; }
        public string CHANGE_INDEX { get; set; }
        public DateTime?  LOGGED_DATE { get; set; }      
    }
}
