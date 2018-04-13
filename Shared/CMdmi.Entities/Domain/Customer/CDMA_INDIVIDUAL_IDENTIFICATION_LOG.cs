namespace CMdm.Entities.Domain.Customer
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("CDMA_INDIVIDUAL_ID_LOG")]

    public partial class CDMA_INDIVIDUAL_IDENTIFICATION_LOG
    {

        [Key]
        public string CUSTOMER_NO { get; set; }
        public string IDENTIFICATION_TYPE { get; set; }
        public string ID_NO { get; set; }
        public DateTime? ID_EXPIRY_DATE { get; set; }
        public DateTime? ID_ISSUE_DATE { get; set; }
        public string PLACE_OF_ISSUANCE { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }     
        public string LAST_MODIFIED_BY { get; set; }
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public DateTime? AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }
        public string TIED { get; set; }



    }
}
