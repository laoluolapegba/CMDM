namespace CMdm.UI.Web.Models.Customer
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;
    public class IndividualBioDataModel
    {
        public IndividualBioDataModel()
        {
          CountryofBirth = new List<SelectListItem>();
           Nationalities = new List<SelectListItem>();
            Religions = new List<SelectListItem>();
            Branchs = new List<SelectListItem>();
            State = new List<SelectListItem>();
            IndDisAbility = new List<SelectListItem>();
            TitleTypes = new List<SelectListItem>();
            Gender = new List<SelectListItem>();
            CusComplexion = new List<SelectListItem>();
            MaritalStatus = new List<SelectListItem>();
        }
        [DisplayName("Customer NO")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Title")]
        public string TITLE { get; set; }
        [DisplayName("Surname")]
        public string SURNAME { get; set; }
        [DisplayName("First Name")]
        public string FIRST_NAME { get; set; }
        [DisplayName("Other Name")]
        public string OTHER_NAME { get; set; }
        [DisplayName("Nickname")]
        public string NICKNAME_ALIAS { get; set; }
         
       // public string LAST_MODIFIED_BY { get; set; }
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DATE_OF_BIRTH { get; set; }
        [DisplayName("Place of Birth")]
        public string PLACE_OF_BIRTH { get; set; }
        [DisplayName("Country of Birth")]
        public string COUNTRY_OF_BIRTH { get; set; }
        [DisplayName("Sex")]
        public string SEX { get; set; }
        [DisplayName("Age")]
        public Nullable<int> AGE { get; set; }
        [DisplayName("Marital Status")]
        public string MARITAL_STATUS { get; set; }
        [DisplayName("Nationality")]
        public string NATIONALITY { get; set; }
        [DisplayName("State of Origin")]
        public string STATE_OF_ORIGIN { get; set; }
        [DisplayName("Mother Maiden Name")]
        public string MOTHER_MAIDEN_NAME { get; set; }
        [DisplayName("Disability")]
        [Required(AllowEmptyStrings = true)]
        public string DISABILITY { get; set; }
        [DisplayName("Complexion")]
        public string COMPLEXION { get; set; }
        [DisplayName("No of Children")]
        public Nullable<int> NUMBER_OF_CHILDREN { get; set; }
        [DisplayName("Religion")]
        public string RELIGION { get; set; }


        ////[DataType(DataType.Date)]
        ////[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        ////public Nullable<System.DateTime> CREATED_DATE { get; set; }
        ////public string CREATED_BY { get; set; }

        ////[DataType(DataType.Date)]
        ////[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        ////public Nullable<System.DateTime> LAST_MODIFIED_DATE { get; set; }
        ////public string AUTHORISED { get; set; }
        ////public string AUTHORISED_BY { get; set; }

        ////[DataType(DataType.Date)]
        ////[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        ////public Nullable<System.DateTime> AUTHORISED_DATE { get; set; }
        ////public string IP_ADDRESS { get; set; }
        //[DisplayName("Branch")]
        //public string BRANCH_CODE { get; set; }

        public List<SelectListItem> CountryofBirth { get; set; }
        public List<SelectListItem> Nationalities { get; set; }
        public List<SelectListItem> Religions { get; set; }
        public List<SelectListItem> Branchs { get; set; }
        public List<SelectListItem> State { get; set; }
        public List<SelectListItem> IndDisAbility { get; set; }
        public List<SelectListItem> TitleTypes { get; set; }
        public List<SelectListItem> Gender { get; set; }
        public List<SelectListItem> CusComplexion { get; set; }
        public List<SelectListItem> MaritalStatus { get; set; }
    }
}