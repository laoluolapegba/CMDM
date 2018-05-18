using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CMDM_WRONGSCHCODECLASS")]
    public partial class CMDM_WRONGSCHCODECLASS
    {
        [Key]
        public string CIF_ID { get; set; }
        public string FORACID { get; set; }
        public string SCHM_CODE { get; set; }
        public string ACCOUNTOFFICER_CODE { get; set; }
        public string ACCOUNTOFFICER_NAME { get; set; }
        public string SCHMECODE_CLASSIFIATION { get; set; }
        public string ACCT_NAME { get; set; }
        public string SOL_ID { get; set; }
        public string CUSTOMER_TYPE { get; set; }
        public DateTime? DATE_OF_RUN { get; set; }
    }
}
