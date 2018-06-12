using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.GoldenRecord
{
    public class GoldenRecordModel
    {
        public GoldenRecordModel()
        {
            Branches = new List<SelectListItem>();
        }
        

        [DisplayName("Search ")]
        public string SearchName { get; set; }
        
        [DisplayName("Record Id")]
        public int GOLDEN_RECORD { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Customer Id")]
        public string CUSTOMER_NO { get; set; }

        [StringLength(150)]
        [DisplayName("BVN")]
        public string BVN { get; set; }

        [Required]
        [StringLength(300)]
        [DisplayName("Fullname")]
        public string FULL_NAME { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime? DATE_OF_BIRTH { get; set; }

        [StringLength(500)]
        [DisplayName("Address")]
        public string RESIDENTIAL_ADDRESS { get; set; }
        [DisplayName("Customer Type")]
        public string CUSTOMER_TYPE { get; set; }

        [DisplayName("Gender")]
        [StringLength(20)]
        public string SEX { get; set; }
        public IList<SelectListItem> Branches { get; set; }
        [DisplayName("Branch Name")]
        public string BRANCH_CODE { get; set; }
        [DisplayName("Phone Number")]
        public string PHONE_NUMBER { get; set; }
        [DisplayName("Email")]
        public string EMAIL { get; set; }
        [DisplayName("Scheme Code")]
        public string SCHEME_CODE { get; set; }
        [DisplayName("Account Number")]
        public string ACCOUNT_NO { get; set; }
        public int Id { get; set; }
    }
}