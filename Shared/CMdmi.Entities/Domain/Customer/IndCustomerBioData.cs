using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMdm.Core;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CMdm.Entities.Domain.Customer
{
    
    [Table("CDMA_INDIVIDUAL_BIO_DATA")]
    public partial class IndCustomerBioData //: BaseEntity
    {
        [Key]
        [DisplayName("Customer Id")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Title")]
        public string TITLE { get; set; }
        [DisplayName("Surname")]
        public string SURNAME { get; set; }
        [DisplayName("Firstname")]
        public string FIRST_NAME { get; set; }
        [DisplayName("Othername")]
        public string OTHER_NAME { get; set; }
        [DisplayName("Alias")]
        public string NICKNAME_ALIAS { get; set; }
        [DisplayName("Date of Birth")]
        public DateTime? DATE_OF_BIRTH { get; set; }
        [DisplayName("Place of Birth")]
        public string PLACE_OF_BIRTH { get; set; }
        [DisplayName("Country of Birth")]
        public string COUNTRY_OF_BIRTH { get; set; }
        [DisplayName("Sex")]
        public string SEX { get; set; }
        [DisplayName("Age")]
        public string AGE { get; set; }
        [DisplayName("Marital Status")]
        public string MARITAL_STATUS { get; set; }
        [DisplayName("Nationality")]
        public string NATIONALITY { get; set; }
        [DisplayName("State of Origin")]
        public string STATE_OF_ORIGIN { get; set; }
        [DisplayName("Mother's maiden Name")]
        public string MOTHER_MAIDEN_NAME { get; set; }
        [DisplayName("Disability")]
        public string DISABILITY { get; set; }

        public string COMPLEXION { get; set; }
        [DisplayName("Number of Children")]
        public decimal? NUMBER_OF_CHILDREN { get; set; }
        [DisplayName("Religion")]
        public string RELIGION { get; set; }
        [DisplayName("Created Date")]
        public DateTime? CREATED_DATE { get; set; }
        [DisplayName("Datasource Name")]
        public string CREATED_BY { get; set; }
        [DisplayName("Last Modified Date")]
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        [DisplayName("Last Modified By")]
        public string LAST_MODIFIED_BY { get; set; }
        [DisplayName("Authorized Status")]
        public string AUTHORISED { get; set; }
        [DisplayName("Authorized By")]
        public string AUTHORISED_BY { get; set; }
        [DisplayName("Authorized Date")]
        public DateTime? AUTHORISED_DATE { get; set; }
        [DisplayName("Terminal Id")]
        public string IP_ADDRESS { get; set; }
        [DisplayName("Branch Name")]
        public string BRANCH_CODE { get; set; }
    }
}
