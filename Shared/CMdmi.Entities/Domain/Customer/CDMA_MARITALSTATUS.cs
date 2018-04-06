namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    [Table("CDMA_MARITALSTATUS")]
    public partial class CDMA_MARITALSTATUS
    {
        [Key]
        public int CODE { get; set; }
        public string STATUS { get; set; }
    }
}
