using System;
using System.Collections.Generic;
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
        public string CUSTOMER_NO { get; set; }
        public int? TITLE { get; set; }
        public string SURNAME { get; set; }
        public string FIRST_NAME { get; set; }
        public string OTHER_NAME { get; set; }
        public string DATE_OF_BIRTH { get; set; }
        public string SEX { get; set; }
        public int? RELATIONSHIP { get; set; }
        public string OFFICE_NO { get; set; }
        public string MOBILE_NO { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public string HOUSE_NUMBER { get; set; }
        public decimal? IDENTIFICATION_TYPE { get; set; }
        public string ID_EXPIRY_DATE { get; set; }
        public string ID_ISSUE_DATE { get; set; }
        public string RESIDENT_PERMIT_NUMBER { get; set; }
        public string PLACE_OF_ISSUANCE { get; set; }
        public string STREET_NAME { get; set; }
        public string NEAREST_BUS_STOP_LANDMARK { get; set; }
        public string CITY_TOWN { get; set; }
        public decimal? LGA { get; set; }
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
    }
}