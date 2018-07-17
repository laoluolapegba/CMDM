using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.CustomModule.Fcmb
{
    public class WrongSegmentModel
    {
        public WrongSegmentModel()
        {
            Branches = new List<SelectListItem>();
        }

        [DisplayName("Customer ID ")]
        public string ORGKEY { get; set; }
        [DisplayName("First Name ")]
        public string CUST_FIRST_NAME { get; set; }
        [DisplayName("Middle Name ")]
        public string CUST_MIDDLE_NAME { get; set; }
        [DisplayName("Last Name ")]
        public string CUST_LAST_NAME { get; set; }
        [DisplayName("Gender ")]
        public string GENDER { get; set; }
        [DisplayName("Date of Birth ")]
        public DateTime? CUST_DOB { get; set; }
        [DisplayName("Branch Name ")]
        public string PRIMARY_SOL_ID { get; set; }
        [DisplayName("Segmentation Class ")]
        public string SEGMENTATION_CLASS { get; set; }
        [DisplayName("Segment Name ")]
        public string SEGMENTNAME { get; set; }
        [DisplayName("Subsegment ")]
        public string SUBSEGMENT { get; set; }
        [DisplayName("Subsegment Name ")]
        public string SUBSEGMENTNAME { get; set; }
        [DisplayName("Corporate ID ")]
        public int? CORP_ID { get; set; }
        [DisplayName("Date of Run ")]
        public DateTime? DATE_OF_RUN { get; set; }
        [DisplayName("Scheme Code ")]
        public string SCHEME_CODE { get; set; }
        [DisplayName("Account Number ")]
        public string ACCOUNTNO { get; set; }

        public int Id
        {
            get; set;
        }

        public IList<SelectListItem> Branches { get; set; }
    }
}