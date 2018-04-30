using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class CustomerIncomeModel
    {
        public CustomerIncomeModel()
        {
            IncomeBand = new List<SelectListItem>();
            InitialDeposit = new List<SelectListItem>();
        }

        [DisplayName("Customer NO")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Income Band")]
        public string INCOME_BAND { get; set; }
        [DisplayName("Initial Deposit")]
        public string INITIAL_DEPOSIT { get; set; }

        public List<SelectListItem> IncomeBand { get; set; }
        public List<SelectListItem> InitialDeposit { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }
    }
}