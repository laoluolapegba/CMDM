namespace CMdm.Data.Rbac
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CM_USER_ROLE_XREF")]
    public partial class CM_USER_ROLE_XREF
    {
        [Key]
        [ForeignKey("CM_USER_PROFILE")]
        [Column(Order = 0)]
        public int USER_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("CM_USER_ROLES")]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ROLE_ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RECORD_ID { get; set; }


        

        public virtual CM_USER_PROFILE CM_USER_PROFILE { get; set; }

        public virtual CM_USER_ROLES CM_USER_ROLES { get; set; }
    }
}
