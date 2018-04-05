using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace CMdm.UI.Web.Models.Customer
{
    public class EmpInfoModel
    {
        public EmpInfoModel()
        {
            Occupationtype = new List<SelectListItem>();
            Subsectortype = new List<SelectListItem>();
            Businessnature = new List<SelectListItem>();
            Indsegment = new List<SelectListItem>();          
            EmploymentStatus = new List<SelectListItem>();          
        }

        [DisplayName("Customer No")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Employment Status")]
        public string EMPLOYMENT_STATUS { get; set; }
        [DisplayName("Employer Name/Institution Name")]
        public string EMPLOYER_INSTITUTION_NAME { get; set; }
        [DisplayName("Date of Employment")]
        [UIHint("DateNullable")]
        public DateTime? DATE_OF_EMPLOYMENT { get; set; }
        [DisplayName("Sector Class")]
        public decimal? SECTOR_CLASS { get; set; }
        [DisplayName("Sub Sector")]
        public string SUB_SECTOR { get; set; }
        [DisplayName("Nature of Business")]
        public decimal? NATURE_OF_BUSINESS_OCCUPATION { get; set; }
        [DisplayName("Industry Segment")]
        public string INDUSTRY_SEGMENT { get; set; }

        public List<SelectListItem> Occupationtype { get; set; }
        public List<SelectListItem> Subsectortype { get; set; }
        public List<SelectListItem> Businessnature { get; set; }
        public List<SelectListItem> Indsegment { get; set; }
        public List<SelectListItem> EmploymentStatus { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
    }
}
