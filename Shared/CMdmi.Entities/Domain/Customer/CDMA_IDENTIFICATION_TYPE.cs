namespace CMdm.Entities.Domain.Customer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    [Table("CDMA_IDENTIFICATION_TYPE")]

    public partial class CDMA_IDENTIFICATION_TYPE
    {
        [Key]
        public decimal CODE { get; set; }
        public string ID_TYPE { get; set; }
    }
}
