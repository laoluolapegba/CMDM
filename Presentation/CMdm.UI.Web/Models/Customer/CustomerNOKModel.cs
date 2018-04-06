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
        }
        [DisplayName("Customer NO")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Title")]
        public int? TITLE { get; set; }
        [DisplayName("Surname")]
        public string SURNAME { get; set; }
        [DisplayName("Firstname")]
        public string FIRST_NAME { get; set; }
        [DisplayName("Othername")]
        public string OTHER_NAME { get; set; }
        [DisplayName("Date of Birth")]
        [UIHint("DateNullable")]
        public DateTime? DATE_OF_BIRTH { get; set; }
        [DisplayName("Gender")]
        public string SEX { get; set; }
        [DisplayName("Relationship")]
        public int? RELATIONSHIP { get; set; }
        [DisplayName("Office No")]
        public string OFFICE_NO { get; set; }
        [DisplayName("Mobile No")]
        public string MOBILE_NO { get; set; }
        [DisplayName("Email Address")]
        public string EMAIL_ADDRESS { get; set; }
        [DisplayName("ouse Number")]
        public string HOUSE_NUMBER { get; set; }
        [DisplayName("Identification Type")]
        public decimal? IDENTIFICATION_TYPE { get; set; }
        [DisplayName("Expiry Date")]
        [UIHint("DateNullable")]
        public DateTime? ID_EXPIRY_DATE { get; set; }
        [DisplayName("Issue Date")]
        [UIHint("DateNullable")]
        public DateTime? ID_ISSUE_DATE { get; set; }
        [DisplayName("Resident Permit Number")]

        public string RESIDENT_PERMIT_NUMBER { get; set; }
        [DisplayName("Place of Issue")]

        public string PLACE_OF_ISSUANCE { get; set; }
        [DisplayName("Street name")]

        public string STREET_NAME { get; set; }
        [DisplayName("Nearest BStop or Landmark")]

        public string NEAREST_BUS_STOP_LANDMARK { get; set; }
        [DisplayName("City Town")]
        public string CITY_TOWN { get; set; }
        public decimal? LGA { get; set; }
        [DisplayName("Zip or Postal Code")]
        public string ZIP_POSTAL_CODE { get; set; }
        public decimal? STATE { get; set; }
        public decimal? COUNTRY { get; set; }
        public List<SelectListItem> States { get;  set; }
        public List<SelectListItem> LocalGovts { get;  set; }
        public List<SelectListItem> IdTypes { get; set; }
        public List<SelectListItem> TitleTypes { get; set; }
        public List<SelectListItem> RelationshipTypes { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Genders { get; set; }
        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
    }
}