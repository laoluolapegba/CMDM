namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("CDMA_INDIVIDUAL_OTHER_DET_LOG")]

    public partial class CDMA_INDIVIDUAL_OTHER_DETAILS_LOG
    {

        [Key]
        public string CUSTOMER_NO { get; set; }
        public string TIN_NO { get; set; }
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
