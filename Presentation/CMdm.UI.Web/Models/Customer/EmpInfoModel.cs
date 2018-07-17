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
            Branches = new List<SelectListItem>();
            LGAs = new List<SelectListItem>();
            States = new List<SelectListItem>();
        }

        [DisplayName("Customer No")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Occupation")]
        public string OCCUPATION { get; set; }
        [DisplayName("Employment Status")]
        public string EMPLOYMENT_STATUS { get; set; }
        [DisplayName("Fax No")]
        public string FAX_NO { get; set; }
        [DisplayName("Date of Employment")]
        [UIHint("DateNullable")]
        public DateTime? DATE_OF_EMPLOYMENT { get; set; }
        [DisplayName("Address No")]
        public int? EMP_ADDRESS_NO { get; set; }
        [DisplayName("Address LGA")]
        public int? EMPLOYMENT_ADD_LGA { get; set; }
        [DisplayName("Address City")]
        public string EMPLOYMENT_ADDRESS_CITY { get; set; }
        [DisplayName("Employer Name / Institution Name")]
        public string EMPLOYER_INSTITUTION_NAME { get; set; }
        [DisplayName("Nature of Business")]
        public decimal? NATURE_OF_BUSINESS_OCCUPATION { get; set; }
        [DisplayName("Office Number")]
        public string OFFICE_NO_CUSTOMER { get; set; }
        [DisplayName("State")]
        public int? EMPLOYER_ADD_STATE { get; set; }
        [DisplayName("Street Name")]
        public string STREET_NAME { get; set; }
        [DisplayName("Annual Income")]
        public decimal? ANNUAL_INCOME { get; set; }
        [DisplayName("Bustop Landmark")]
        public string BUSTOP_LANDMARK_EMPLOYMENT_ADD { get; set; }
        [DisplayName("Branch")]
        public string BRANCH_CODE { get; set; }
        [DisplayName("Tier")]
        public int? TIER { get; set; }
        public int? QUEUE_STATUS { get; set; }

        public List<SelectListItem> Occupationtype { get; set; }
        public List<SelectListItem> Subsectortype { get; set; }
        public List<SelectListItem> Businessnature { get; set; }
        public List<SelectListItem> Indsegment { get; set; }
        public List<SelectListItem> EmploymentStatus { get; set; }
        public List<SelectListItem> Branches { get; set; }
        public List<SelectListItem> LGAs { get; set; }
        public List<SelectListItem> States { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }
    }
}
