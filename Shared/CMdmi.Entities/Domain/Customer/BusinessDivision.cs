namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [Table("CDMA_BUSINESS_DIVISION")]
    public partial class BusinessDivision
    {
        [Key]
        public decimal ID { get; set; }
        public string DIVISION { get; set; }


    }
}
