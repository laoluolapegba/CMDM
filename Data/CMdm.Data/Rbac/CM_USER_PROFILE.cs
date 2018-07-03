namespace CMdm.Data.Rbac
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CM_USER_PROFILE")]
    public partial class CM_USER_PROFILE
    {
        public CM_USER_PROFILE()
        {
            CM_USER_ROLE_XREF = new HashSet<CM_USER_ROLE_XREF>();
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PROFILE_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string USER_ID { get; set; }

        [Required]
        [StringLength(128)]
        public string COD_PASSWORD { get; set; }

        [StringLength(128)]
        public string PASSWORDSALT { get; set; }

        [StringLength(10)]
        public string MOBILEPIN { get; set; }

        [StringLength(255)]
        public string PASSWORDQUESTION { get; set; }

        [StringLength(255)]
        public string PASSWORDANSWER { get; set; }

        public bool ISAPPROVED { get; set; }

        public bool ISLOCKED { get; set; }

        public DateTime CREATED_DATE { get; set; }

        public DateTime? LASTLOGINDATE { get; set; }

        public DateTime? LASTPASSWORDCHANGEDDATE { get; set; }

        public DateTime? LASTLOCKOUTDATE { get; set; }

        public int? FAILEDPASSWORDATTEMPTCOUNT { get; set; }

        public bool FLAG_ALLOW_DOC_SHARING { get; set; }

        public bool FLAG_ALLOW_SMS_ALERT { get; set; }

        public int ROLE_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string DISPLAY_NAME { get; set; }

        [Required]
        [StringLength(20)]
        public string FIRSTNAME { get; set; }

        [Required]
        [StringLength(20)]
        public string LASTNAME { get; set; }

        [Required]
        [StringLength(50)]
        public string EMAIL_ADDRESS { get; set; }

        public string BRANCH_ID { get; set; }

        public virtual CM_USER_ROLES CM_USER_ROLES { get; set; }

        public virtual ICollection<CM_USER_ROLE_XREF> CM_USER_ROLE_XREF { get; set; }
        public virtual CM_BRANCH CM_BRANCH { get; set; }

        //public virtual ICollection<CM_USER_ROLES> CM_USER_ROLES { get; set; }

        //public virtual ICollection<> CM_USER_ROLE_XREF { get; set; }
        public virtual ICollection<CM_MAKER_CHECKER_XREF> CM_MAKER_CHECKER_XREF { get; set; }
    }
}
