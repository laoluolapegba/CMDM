namespace CMdm.Entities.Domain.Customer
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CDMA_INDIVIDUAL_BIO_LOG")]
    
     
    public partial class CDMA_INDIVIDUAL_BIO_LOG
    {
        [Key]
        public string CUSTOMER_NO { get; set; }
        public string TITLE { get; set; }
        public string SURNAME { get; set; }
        public string FIRST_NAME { get; set; }
        public string OTHER_NAME { get; set; }
        public string NICKNAME_ALIAS { get; set; }
        public DateTime? DATE_OF_BIRTH { get; set; }
        public string PLACE_OF_BIRTH { get; set; }
        public string COUNTRY_OF_BIRTH { get; set; }
        public string SEX { get; set; }
        public int? AGE { get; set; }
        public string MARITAL_STATUS { get; set; }
        public string NATIONALITY { get; set; }
        public string STATE_OF_ORIGIN { get; set; }
        public string MOTHER_MAIDEN_NAME { get; set; }
        public string DISABILITY { get; set; }
        public string COMPLEXION { get; set; }
        public int? NUMBER_OF_CHILDREN { get; set; }
        public string RELIGION { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public DateTime? AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }
        public string BRANCH_CODE { get; set; }
        public string TIED { get; set; }


    }
}
