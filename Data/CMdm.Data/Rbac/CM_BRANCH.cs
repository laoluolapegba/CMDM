using CMdm.Entities.Domain.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Data.Rbac
{

     [Table("CM_BRANCH")]
    public partial class CM_BRANCH
    {
        public CM_BRANCH()
        {
            this.CM_USER_PROFILE = new HashSet<CM_USER_PROFILE>();
    }

        [Key]
        public string BRANCH_ID { get; set; }
        public string BRANCH_NAME { get; set; }
        public Nullable<decimal> VAULT_LIMIT { get; set; }
        public string REGION_ID { get; set; }
        public int? STATE_ID { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string LAST_UPDATED_BY { get; set; }
        public Nullable<System.DateTime> LAST_UPDATED_DATE { get; set; }
        public string ZONECODE { get; set; }
        public string ZONENAME { get; set; }
        public string REGION_NAME { get; set; }
        public virtual ICollection<CM_USER_PROFILE> CM_USER_PROFILE { get; set; }

        public static implicit operator string(CM_BRANCH v)
        {
            throw new NotImplementedException();
        }
    }
}
