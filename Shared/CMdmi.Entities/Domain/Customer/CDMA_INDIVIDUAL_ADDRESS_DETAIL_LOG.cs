
namespace CMdm.Entities.Domain.Customer
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    [Table("CDMA_INDIVIDUAL_ADDRESS_LOG")]

    public partial class CDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG
    {

        [Key]
        public string CUSTOMER_NO { get; set; }
        public string RESIDENTIAL_ADDRESS { get; set; }
        public string CITY_TOWN_OF_RESIDENCE { get; set; }
        public string LGA_OF_RESIDENCE { get; set; }
        public string NEAREST_BUS_STOP_LANDMARK { get; set; }
        public string STATE_OF_RESIDENCE { get; set; }
        public string COUNTRY_OF_RESIDENCE { get; set; }
        public string RESIDENCE_OWNED_OR_RENT { get; set; }
        public string ZIP_POSTAL_CODE { get; set; }
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
