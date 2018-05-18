using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.Customer
{
    [Table("TMP_SEC_SUB3")]
    public partial class TMP_SEC_SUB3
    {
        [Key]
        public string ORGKEY { get; set; }
        public string CUST_FIRST_NAME { get; set; }
        public string CUST_MIDDLE_NAME { get; set; }
        public string CUST_LAST_NAME { get; set; }
        public string GENDER { get; set; }
        public DateTime? CUST_DOB { get; set; }
        public string CUSTOMERMINOR { get; set; }
        public string MAIDENNAMEOFMOTHER { get; set; }
        public string NICK_NAME { get; set; }
        public string PLACEOFBIRTH { get; set; }
        public string PRIMARY_SOL_ID { get; set; }
        public string SEGMENTATION_CLASS { get; set; }
        public string SECTOR { get; set; }
        public string SECTORNAME { get; set; }
        public string SUBSECTOR { get; set; }
        public string SUBSECTORNAME { get; set; }
        public string SUBSEGMENT { get; set; }
        public int? CORP_ID { get; set; }
    }
}
