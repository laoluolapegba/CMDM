using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CMdm.UI.Web.Models.Customer
{
    public class EmpDetailsModel
    {
        public EmpDetailsModel()
        {
            Occupationtype = new List<SelectListItem>();
            Subsectortype = new List<SelectListItem>();
            Businessnature = new List<SelectListItem>();
            Indsegment = new List<SelectListItem>();
          
        }

        [DisplayName("Customer No")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Employment Status")]
        public string EMPLOYMENT_STATUS { get; set; }
        [DisplayName("Employer Name/Institution Name")]
        public string EMPLOYER_INSTITUTION_NAME { get; set; }
        [DisplayName("Date of Employment")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DATE_OF_EMPLOYMENT { get; set; }
        [DisplayName("Sector Class")]
        public decimal? SECTOR_CLASS { get; set; }
        [DisplayName("Sub Sector")]
        public string SUB_SECTOR { get; set; }
        [DisplayName("Nature of Business")]
        public decimal? NATURE_OF_BUSINESS_OCCUPATION { get; set; }
        [DisplayName("Industry Segment")]
        public string INDUSTRY_SEGMENT { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public string AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }

        public List<SelectListItem> Occupationtype { get; set; }
        public List<SelectListItem> Subsectortype { get; set; }
        public List<SelectListItem> Businessnature { get; set; }
        public List<SelectListItem> Indsegment { get; set; }
    }
}
