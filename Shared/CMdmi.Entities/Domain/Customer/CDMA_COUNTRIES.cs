namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    [Table("CDMA_COUNTRIES")]

    public partial class CDMA_COUNTRIES
    {

        [Key]
        public decimal COUNTRY_ID { get; set; }
        public string COUNTRY_NAME { get; set; }
        public string COUNTRY_ABBREVIATION { get; set; }
        public decimal UNITED_NATION_NUMBER { get; set; }
        public string DIALING_CODE { get; set; }
   


    }
}
