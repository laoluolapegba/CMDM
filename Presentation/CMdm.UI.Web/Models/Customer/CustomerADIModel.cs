using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class CustomerADIModel
    {
        public CustomerADIModel()
        {
            Salaries = new List<SelectListItem>();
        }
        [DisplayName("Customer NO")]
        public string CUSTOMER_NO { get; set; }
        public string ANNUAL_SALARY_EXPECTED_INC { get; set; }
        [DisplayName("Fax Number")]
        public string FAX_NUMBER { get; set; }
        public List<SelectListItem> Salaries { get; set; }
        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }
    }
}