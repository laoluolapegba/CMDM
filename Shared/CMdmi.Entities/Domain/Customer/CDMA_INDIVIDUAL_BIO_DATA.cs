namespace CMdm.Entities.Domain.Customer
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CDMA_INDIVIDUAL_BIO_DATA")]
    
    public partial class CDMA_INDIVIDUAL_BIO_DATA
    {
        [Key, Column(Order = 0)]
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
        [DisplayName("Customer NO")]
        public string NICKNAME_ALIAS { get; set; }
        [DisplayName("Nickname")]
        public string LAST_MODIFIED_BY { get; set; }
        [DisplayName("Date of Birth")]
        public DateTime? DATE_OF_BIRTH { get; set; }
        [DisplayName("Place of Birth")]
        public string PLACE_OF_BIRTH { get; set; }
        [DisplayName("Country of Birth")]
        public string COUNTRY_OF_BIRTH { get; set; }
        [DisplayName("Sex")]
        public string SEX { get; set; }
        [DisplayName("Age")]
        public int? AGE { get; set; }
        [DisplayName("Marital Status")]
        public string MARITAL_STATUS { get; set; }
        [DisplayName("Nationality")]
        public string NATIONALITY { get; set; }
        [DisplayName("State of Origin")]
        public string STATE_OF_ORIGIN { get; set; }
        [DisplayName("Mother Maiden Name")]
        public string MOTHER_MAIDEN_NAME { get; set; }
        [DisplayName("Disability")]
        //[Required(AllowEmptyStrings = true)]
        public string DISABILITY { get; set; }
        [DisplayName("Complexion")]
        public string COMPLEXION { get; set; }
        [DisplayName("No of Children")]
        public int? NUMBER_OF_CHILDREN { get; set; }
        [DisplayName("Religion")]
        public string RELIGION { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        [Key, Column(Order = 1)]
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public DateTime? AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }
        public string BRANCH_CODE { get; set; }
    }
}
