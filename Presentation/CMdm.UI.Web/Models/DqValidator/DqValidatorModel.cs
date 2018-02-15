using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.DqValidator
{
    public class DqValidatorModel
    {
        [DisplayName("Regex Id")]
        public int REGEX_ID { get; set; }
        [DisplayName("Name")]
        public string REGEX_NAME { get; set; }
        [DisplayName("Description")]
        public string REGEX_DESC { get; set; }
        [DisplayName("Regular Expression")]
        public string REGEX_STRING { get; set; }
    }
}