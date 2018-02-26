namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    [Table("CDMA_ONLINE_LIMIT_RANGE")]

    public partial class Limit_Range
    {
        [Key]
        public string ID { get; set; }
        public string LIMIT { get; set; }


    }
}
