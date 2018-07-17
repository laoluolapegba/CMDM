using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class CorpADDModel
    {
        public CorpADDModel()
        {
            Branches = new List<SelectListItem>();
        }
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Branch name")]
        public string BRANCH_CODE { get; set; }
        [DisplayName("Counterparties Clients of Customer")]
        public string COUNTERPARTIES_CLIENTS_OF_CUST { get; set; }
        public int? QUEUE_STATUS { get; set; }

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