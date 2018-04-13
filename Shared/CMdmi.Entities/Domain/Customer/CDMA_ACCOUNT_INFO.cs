using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.Customer
{     
    [Table("CDMA_ACCOUNT_INFO")]
    public partial class CDMA_ACCOUNT_INFO
    {
        [Key, Column(Order = 0)]
        public string CUSTOMER_NO { get; set; }
        public string ACCOUNT_HOLDER { get; set; }
        public string TYPE_OF_ACCOUNT { get; set; }
        public string ACCOUNT_NUMBER { get; set; }
        public string ACCOUNT_OFFICER { get; set; }
        public string ACCOUNT_TITLE { get; set; }
        public string BRANCH { get; set; }
        public string BRANCH_CLASS { get; set; }
        public string BUSINESS_DIVISION { get; set; }
        public string BUSINESS_SEGMENT { get; set; }
        public string BUSINESS_SIZE { get; set; }
        public string BVN_NUMBER { get; set; }
        public string CUSTOMER_IC { get; set; }
        public string CAV_REQUIRED { get; set; }
        public string CUSTOMER_SEGMENT { get; set; }
        public string CUSTOMER_TYPE { get; set; }
        public string OPERATING_INSTRUCTION { get; set; }
        public string ORIGINATING_BRANCH { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }
        [Key, Column(Order = 1)]
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public DateTime? AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }

        public virtual CDMA_ACCOUNT_TYPE TypesOfAccount { get; set; }
        //public virtual CM_BRANCH Branches { get; set; }
        public virtual CDMA_BRANCH_CLASS BranchClasses { get; set; }
        public virtual CDMA_BUSINESS_DIVISION BusinessDivisions { get; set; }
        public virtual CDMA_BUSINESS_SEGMENT BusinessSegments { get; set; }
        public virtual CDMA_BUSINESS_SIZE BusinessSizes { get; set; }
        public virtual CDMA_CUSTOMER_SEGMENT CustomerSegments { get; set; }
        public virtual CDMA_CUSTOMER_TYPE CustomerTypes { get; set; }
        //public virtual CM_BRANCH OriginatingBranch { get; set; }
    }
}
