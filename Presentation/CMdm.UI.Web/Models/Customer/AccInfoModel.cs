using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class AccInfoModel
    {
        public AccInfoModel()
        {
            AccountHolder = new List<SelectListItem>();
            TypesOfAccount = new List<SelectListItem>();
            Branches = new List<SelectListItem>();
            BranchClasses = new List<SelectListItem>();
            BusinessDivisions = new List<SelectListItem>();
            BusinessSegments = new List<SelectListItem>();
            BusinessSizes = new List<SelectListItem>();
            CavRequired = new List<SelectListItem>();
            CustomerSegments = new List<SelectListItem>();
            CustomerTypes = new List<SelectListItem>();
            OriginatingBranch = new List<SelectListItem>();
        }

        [DisplayName("Customer NO")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Account Holder")]
        public string ACCOUNT_HOLDER { get; set; }
        [DisplayName("Type of Account")]
        public string TYPE_OF_ACCOUNT { get; set; }
        [DisplayName("Account Number")]
        public string ACCOUNT_NUMBER { get; set; }
        [DisplayName("Account Officer")]
        public string ACCOUNT_OFFICER { get; set; }
        [DisplayName("Account Title")]
        public string ACCOUNT_TITLE { get; set; }
        [DisplayName("Branch")]
        public string BRANCH { get; set; }
        [DisplayName("Branch Class")]
        public string BRANCH_CLASS { get; set; }
        [DisplayName("Business Division")]
        public string BUSINESS_DIVISION { get; set; }
        [DisplayName("Business Segment")]
        public string BUSINESS_SEGMENT { get; set; }
        [DisplayName("Business Size")]
        public string BUSINESS_SIZE { get; set; }
        [DisplayName("BVN Number")]
        public string BVN_NUMBER { get; set; }
        [DisplayName("CAV Required")]
        public string CAV_REQUIRED { get; set; }
        [DisplayName("Customer Ic")]
        public string CUSTOMER_IC { get; set; }
        [DisplayName("Customer Segment")]
        public string CUSTOMER_SEGMENT { get; set; }
        [DisplayName("Customer Type")]
        public string CUSTOMER_TYPE { get; set; }
        [DisplayName("Operating Instruction")]
        public string OPERATING_INSTRUCTION { get; set; }
        [DisplayName("Originating Branch")]
        public string ORIGINATING_BRANCH { get; set; }

        public List<SelectListItem> AccountHolder { get; set; }
        public List<SelectListItem> TypesOfAccount { get; set; }
        public List<SelectListItem> Branches { get; set; }
        public List<SelectListItem> BranchClasses { get; set; }
        public List<SelectListItem> BusinessDivisions { get; set; }
        public List<SelectListItem> BusinessSegments { get; set; }
        public List<SelectListItem> BusinessSizes { get; set; }
        public List<SelectListItem> CavRequired { get; set; }
        public List<SelectListItem> CustomerSegments { get; set; }
        public List<SelectListItem> CustomerTypes { get; set; }
        public List<SelectListItem> OriginatingBranch { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }
    }
}