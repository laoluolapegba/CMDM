using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CDMA_COUNTRIES")]
    public partial class CDMA_COUNTRIES
    {
        public CDMA_COUNTRIES()
        {
            CdmaNextOfKins = new HashSet<CDMA_INDIVIDUAL_NEXT_OF_KIN>();
            CdmaForeigner = new HashSet<CDMA_FOREIGN_DETAILS>();
        }

        [Key]
        public decimal COUNTRY_ID { get; set; }
        public string COUNTRY_NAME { get; set; }
        public string COUNTRY_ABBREVIATION { get; set; }
        public decimal UNITED_NATION_NUMBER { get; set; }
        public string DIALING_CODE { get; set; }
        public ICollection<CDMA_INDIVIDUAL_NEXT_OF_KIN> CdmaNextOfKins { get; private set; }
        public ICollection<CDMA_FOREIGN_DETAILS> CdmaForeigner { get; private set; }
    }
}
