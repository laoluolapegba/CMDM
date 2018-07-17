using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class CustomerNOKModel
    {
        public CustomerNOKModel()
        {
            States = new List<SelectListItem>();
            LocalGovts = new List<SelectListItem>();
            Countries = new List<SelectListItem>();
            IdTypes = new List<SelectListItem>();
            RelationshipTypes = new List<SelectListItem>();
            TitleTypes = new List<SelectListItem>();
            Genders = new List<SelectListItem>();
            Branches = new List<SelectListItem>();
        }
        [DisplayName("Customer No")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Title")]
        public int? TITLE { get; set; }
        [DisplayName("Surname")]
        public string SURNAME { get; set; }
        [DisplayName("First Name")]
        public string FIRST_NAME { get; set; }
        [DisplayName("Middle Name")]
        public string OTHER_NAME { get; set; }
        [DisplayName("Date of Birth")]
        [UIHint("DateNullable")]
        public DateTime? DATE_OF_BIRTH { get; set; }
        [DisplayName("Gender")]
        public string SEX { get; set; }
        [DisplayName("Next of Kin Relationship")]
        public int? RELATIONSHIP { get; set; }
        [DisplayName("Email")]
        public string EMAIL_ADDRESS { get; set; }
        [DisplayName("Residential Street")]
        public string NEXT_OF_KIN_RESIDENTIALSTREET { get; set; }
        [DisplayName("Address No")]
        public int? NOK_ADDRESS_NO { get; set; }
        [DisplayName("City")]
        public string CITY_TOWN { get; set; }
        [DisplayName("LGA")]
        public int? LGA { get; set; }
        [DisplayName("Nearest Bus Stop")]
        public string NEAREST_BUS_STOP_LANDMARK { get; set; }
        [DisplayName("State")]
        public int? STATE { get; set; }
        [DisplayName("Phone Number")]
        public string NEXT_OF_KIN_PHONE_NUMBER { get; set; }
        [DisplayName("Alternate Phone Number")]
        public string NEXT_OF_KIN_PHONE_NUMBER2 { get; set; }
        [DisplayName("Branch")]
        public string BRANCH_CODE { get; set; }
        public int? QUEUE_STATUS { get; set; }

        public List<SelectListItem> States { get;  set; }
        public List<SelectListItem> LocalGovts { get;  set; }
        public List<SelectListItem> IdTypes { get; set; }
        public List<SelectListItem> TitleTypes { get; set; }
        public List<SelectListItem> RelationshipTypes { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Genders { get; set; }
        public List<SelectListItem> Branches { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }
    }
}