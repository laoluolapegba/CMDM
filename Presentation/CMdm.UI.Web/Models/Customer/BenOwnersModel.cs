    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class BenOwnersModel
    {
        public BenOwnersModel()
        {
            Branches = new List<SelectListItem>();
        }
        [DisplayName("Customer NO")]
        public string ORGKEY { get; set; }
        [DisplayName("Percentage of Beneficiary")]
        public decimal? PERCENTAGE_OF_BENEFICIARY { get; set; }
        [DisplayName("Names of Beneficiary")]
        public string NAMES_OF_BENEFICIARY { get; set; }
        [DisplayName("Branch name")]
        public string PRIMARY_SOL_ID { get; set; }
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