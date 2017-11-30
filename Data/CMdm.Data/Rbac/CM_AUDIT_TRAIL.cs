using System;


namespace CMdm.Data.Rbac
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CM_AUDIT_TRAIL")]
    public partial class Audit
    {
        public Audit()
        {
        }
        //A new SessionId that will be used to link an entire users "Session" of Audit Logs
        //together to help identifer patterns involving erratic behavior
        [Key]
        public int AUDITID { get; set; }
        public string SESSIONID { get; set; }
        public string IPADDRESS { get; set; }
        public string USERID { get; set; }
        public string URLACCESSED { get; set; }
        public DateTime TIMEACCESSED { get; set; }
        //A new Data property that is going to store JSON string objects that will later be able to
        //be deserialized into objects if necessary to view details about a Request
        public string AUDITDATA { get; set; }
        public string AUDITACTION { get; set; }
        public string SERIALIZEDDATA { get; set; }


    }


}