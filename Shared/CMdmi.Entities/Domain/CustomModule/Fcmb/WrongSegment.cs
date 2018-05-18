using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.CustomModule.Fcmb
{
    [Table("VW_WRONG_SEGMENTSUBSEGMENT")]
    public partial class WrongSegment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string ORGKEY { get; set; }
        public string CUST_FIRST_NAME { get; set; }
        public string CUST_MIDDLE_NAME { get; set; }
        public string CUST_LAST_NAME { get; set; }
        public string GENDER { get; set; }
        public DateTime? CUST_DOB { get; set; }
        public string PRIMARY_SOL_ID { get; set; }
        public string SEGMENTATION_CLASS { get; set; }
        public string SEGMENTNAME { get; set; }
        public string SUBSEGMENT { get; set; }
        public string SUBSEGMENTNAME { get; set; }
        public int? CORP_ID { get; set; }
        public DateTime? DATE_OF_RUN { get; set; }
    }
}
