using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class IndividualIDDetail
    {
        public IndividualIDDetail()
        {
            IdType = new List<SelectListItem>();
            Branches = new List<SelectListItem>();
        }
        [DisplayName("Customer NO")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Identification Type")]
        public string IDENTIFICATION_TYPE { get; set; }
        [DisplayName("Reference NO")]
        public string ID_NO { get; set; }
        [DisplayName("Document Expiry Date")]
        [UIHint("DateNullable")]
        public DateTime? ID_EXPIRY_DATE { get; set; }
        [DisplayName("Document Issue Date")]
        [UIHint("DateNullable")]
        public DateTime? ID_ISSUE_DATE { get; set; }
        [DisplayName("Branch")]
        public string BRANCH_CODE { get; set; }
        public int? TIER { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public DateTime? AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }
        public int? QUEUE_STATUS { get; set; }

        public List<SelectListItem> IdType { get; set; }
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