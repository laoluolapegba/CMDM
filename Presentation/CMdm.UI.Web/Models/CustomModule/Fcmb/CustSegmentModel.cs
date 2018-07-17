using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.CustomModule.Fcmb
{
    public class CustSegmentModel
    {
        public CustSegmentModel()
        {
            Branches = new List<SelectListItem>();
            CustomerTypes = new List<SelectListItem>();
            SectorList = new List<SelectListItem>();
            SubsectorList = new List<SelectListItem>();
        }

        [DisplayName("Organization Key ")]
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
        [DisplayName("Customer Minor ")]
        public string CUSTOMERMINOR { get; set; }
        [DisplayName("Mother's Maiden Name")]
        public string MAIDENNAMEOFMOTHER { get; set; }
        [DisplayName("Nick Name ")]
        public string NICK_NAME { get; set; }
        [DisplayName("Place of Birth ")]
        public string PLACEOFBIRTH { get; set; }
        [DisplayName("Branch Code ")]
        public string PRIMARY_SOL_ID { get; set; }
        [DisplayName("Segmentation ")]
        public string SEGMENTATION_CLASS { get; set; }
        [DisplayName("Sector ")]
        public string SECTOR { get; set; }
        [DisplayName("Sector Name ")]
        public string SECTORNAME { get; set; }
        [DisplayName("Subsector ")]
        public string SUBSECTOR { get; set; }
        [DisplayName("Subsector Name ")]
        public string SUBSECTORNAME { get; set; }
        [DisplayName("Subsegment ")]
        public string SUBSEGMENT { get; set; }
        [DisplayName("Corporate ID ")]
        public int? CORP_ID { get; set; }
        [DisplayName("Scheme Code ")]
        public string SCHEME_CODE { get; set; }
        [DisplayName("Account No ")]
        public string ACCOUNT_NO { get; set; }
        [DisplayName("Customer Type")]
        public string CUSTOMER_TYPE { get; set; }
        [DisplayName("Reason")]
        public string REASON { get; set; }
        public int Id
        {
            get; set;
        }
        public IList<SelectListItem> Branches { get; set; }
        public IList<SelectListItem> CustomerTypes { get; set; }
        public IList<SelectListItem> SectorList { get; set; }
        public IList<SelectListItem> SubsectorList { get; set; }
    }
}