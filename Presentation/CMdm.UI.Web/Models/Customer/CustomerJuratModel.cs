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
    }
}