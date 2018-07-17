using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.CustomModule.Fcmb
{
    public class OutstandingDocModel
    {
        public OutstandingDocModel()
        {
            Branches = new List<SelectListItem>();
            CustomerTypes = new List<SelectListItem>();
        }
        

        [DisplayName("Search ")]
        public string SearchName { get; set; }
        
        [DisplayName("Record Id")]
        public string FORACID { get; set; }

        
        [DisplayName("Account Name")]
        public string ACCT_NAME { get; set; }

        [DisplayName("Id")]
        public string SOL_ID { get; set; }

        
        
        [DisplayName("Code")]
        public string SCHM_CODE { get; set; }

        [DisplayName("Description")]
        public string SCHM_DESC { get; set; }

        
        [DisplayName("Type")]
        public string SCHM_TYPE { get; set; }
        [DisplayName("Customer Type")]
        public string ACID { get; set; }

        [DisplayName("Document Code")]
        
        public string DOCUMENT_CODE { get; set; }

        [DisplayName("Document Desc")]
        public string REF_DESC { get; set; }

        [DisplayName("Due Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:dd-mm-yyyy", ApplyFormatInEditMode = true)]
        public DateTime? DUE_DATE { get; set; }

        [DisplayName("Reason")]
        public string FREZ_REASON_CODE { get; set; }

        public IList<SelectListItem> Branches { get; set; }
        public IList<SelectListItem> CustomerTypes { get; set; }
        [DisplayName("Branch Name")]
        public string BRANCH_CODE { get; set; }
        public string BRANCH_NAME { get; set; }
        [DisplayName("Account Officer Code")]
        public string ACCTOFFICER_CODE { get; set; }
        [DisplayName("Account Officer Name")]
        public string ACCTOFFICER_NAME { get; set; }
        [DisplayName("Customer ID")]
        public string CIF_ID { get; set; }
        [DisplayName("Customer Type")]
        public string CUSTOMERTYPE { get; set; }

        public int Id { get; set; }
    }
}