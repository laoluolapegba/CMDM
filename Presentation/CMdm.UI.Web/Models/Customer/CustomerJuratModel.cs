using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class CustomerJuratModel
    {
        [DisplayName("Customer NO")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Date of Oath")]
        [UIHint("DateNullable")]
        public DateTime? DATE_OF_OATH { get; set; }
        [DisplayName("Name of Interpreter")]
        public string NAME_OF_INTERPRETER { get; set; }
        [DisplayName("Address of Interpreter")]
        public string ADDRESS_OF_INTERPRETER { get; set; }
        [DisplayName("Telephone No")]
        public string TELEPHONE_NO { get; set; }
        [DisplayName("Language of Interpretation")]
        public string LANGUAGE_OF_INTERPRETATION { get; set; }
        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }
    }
}