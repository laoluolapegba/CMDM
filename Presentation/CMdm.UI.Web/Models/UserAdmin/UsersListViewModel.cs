using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.UserAdmin
{
    public class UsersListViewModel
    {


        public UsersListViewModel()
        {
            SearchRoleIds = new List<int>();
            AvailableRoles = new List<SelectListItem>();
            Branches = new List<SelectListItem>();
            UserRoles = new List<SelectListItem>();
            Regions = new List<SelectListItem>();
            Zones = new List<SelectListItem>();
        }
        [UIHint("MultiSelect")]
        [DisplayName("Roles")]
        public IList<int> SearchRoleIds { get; set; }
        public IList<SelectListItem> AvailableRoles { get; set; }

        [DisplayName("SearchEmail")]
        [AllowHtml]
        public string SearchEmail { get; set; }

        [DisplayName("SearchUsername")]
        [AllowHtml]
        public string SearchUsername { get; set; }
        [DisplayName("SearchFirstName")]
        [AllowHtml]
        public string SearchFirstName { get; set; }
        [DisplayName("SearchLastName")]
        [AllowHtml]
        public string SearchLastName { get; set; }
        public decimal PROFILE_ID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("User Id")]
        public string USER_ID { get; set; }

        [Required]
        [StringLength(128)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string COD_PASSWORD { get; set; }

        [StringLength(128)]
        public string PASSWORDSALT { get; set; }

        [StringLength(10)]
        public string MOBILEPIN { get; set; }

        [StringLength(255)]
        public string PASSWORDQUESTION { get; set; }

        [StringLength(255)]
        public string PASSWORDANSWER { get; set; }

        public decimal? ISAPPROVED { get; set; }
        [DisplayName("Active")]
        public bool ISLOCKED { get; set; }
        [DisplayName("CreatedOn")]

        public DateTime CREATED_DATE { get; set; }
        [DisplayName("LastActivityDate")]
        public DateTime? LASTLOGINDATE { get; set; }

        public DateTime? LASTPASSWORDCHANGEDDATE { get; set; }

        public DateTime? LASTLOCKOUTDATE { get; set; }

        public decimal? FAILEDPASSWORDATTEMPTCOUNT { get; set; }

        public decimal? FLAG_ALLOW_DOC_SHARING { get; set; }

        public decimal? FLAG_ALLOW_SMS_ALERT { get; set; }
        [DisplayName("Role")]
        public int ROLE_ID { get; set; }

        //[Required]
        [StringLength(100)]
        public string DISPLAY_NAME { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Firstname")]
        public string FIRSTNAME { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Lastname")]
        public string LASTNAME { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Email")]
        public string EMAIL_ADDRESS { get; set; }
        [DisplayName("Branch")]
        public string BRANCH_ID { get; set; }
        [DisplayName("Zone")]
        public string ZONE_ID { get; set; }
        [DisplayName("Region")]
        public string REGION_ID { get; set; }
        public string UserTypes { get; set; }

        public IList<SelectListItem> UserRoles { get; set; }
        public IList<SelectListItem> Branches { get; set; }
        public IList<SelectListItem> Zones { get; set; }
        public IList<SelectListItem> Regions { get; set; }

        [DisplayName("Active")]
        //public bool ISACTIVE { get; set; }

        //this conversion no longer required cos SQL Server has bit
        public bool ISACTIVE
        {
            get
            {
                return ISLOCKED; // return !(ISLOCKED > 0);
            }
        }
        public string ROLE_NAME;
        public string BRANCH_NAME;
        public string FULLNAME;
        //public virtual CM_USER_ROLES CM_USER_ROLES { get; set; }
    }
}