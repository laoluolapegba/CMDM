
namespace CMdm.Data.Rbac
{
    using System;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CM_MAKER_CHECKER_XREF")]
    public partial class CM_MAKER_CHECKER_XREF
    {

        [Key]
        [Column(Order = 0)]
        public int MAKER_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int CHECKER_ID { get; set; }

        //[Required]

        //public int PARENT_TASK { get; set; }

        //[StringLength(20)]
        //public string CREATED_BY { get; set; }

        //public DateTime? CREATED_DATE { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RECORD_ID { get; set; }

        public virtual CM_USER_PROFILE CM_USER_PROFILE { get; set; }

        public virtual CM_USER_ROLES CM_USER_ROLES { get; set; }
    }

}