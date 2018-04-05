using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CMDM_PHONEVALIDATION_RESULTS")]
    public partial class CMDM_PHONEVALIDATION_RESULTS
    {
        [Key]
        public string CUSTOMER_NO { get; set; }
        public string BRANCH_CODE { get; set; }
        public string CUST_LAST_NAME { get; set; }
        public string CUST_MIDDLE_NAME { get; set; }
        public string CUST_FIRST_NAME { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LAST_RUN_DATE { get; set; }
        public string REASON { get; set; }
        public string PHONE_NO { get; set; }


    }
}
