using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CM_BACK_JOBS")]
    public partial class CM_BACK_JOBS
    {
        [Key]
        public int JOBID { get; set; }
        public int JOB_TYPE { get; set; }
        public string FROM_EMAIL { get; set; }
        public DateTime? DATE_LOGGED { get; set; }
        public string CREATED_BY { get; set; }
        public string RCPT_EMAIL { get; set; }
        public int FLG_STATUS { get; set; }
        public DateTime? DATE_SENT { get; set; }
        public int? COUNT_RETRIES { get; set; }
        public string MSG_SUBJECT { get; set; }
        public int BRANCH_ID { get; set; }
        public string RECIPIENTNAME { get; set; }
        public DateTime? REQUIREDDATE { get; set; }
        public string MAILBODY { get; set; }
        public string MAILBODY_2 { get; set; }
        public int? MAILTYPE { get; set; }
        public string MAIL_TEMPLATE { get; set; }
        public string USERFULLNAME { get; set; }
        public string BRANCHNAME { get; set; }
        public string CUSTOMER_NO { get; set; }
        public int ACCOUNT_OFFICER { get; set; }
        public int WRONG_SECTOR { get; set; }
        public int WRONG_SCHEME_CODES { get; set; }
        public int MULTIPLE_AO_CODES { get; set; }
        public int EMAIL_PHONE_VAL { get; set; }
        public int SEGMENT_MAPPING { get; set; }
        public int MULTIPLE_ID { get; set; }
        public int OUTSTANDING_DOCS { get; set; }
        public int PHONE_NUMBER_VAL { get; set; }
    }
}
