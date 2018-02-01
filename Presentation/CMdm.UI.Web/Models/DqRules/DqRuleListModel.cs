using CMdm.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.DqRule
{
    public partial class DqRuleListModel : BaseModel
    {
        [DisplayName("Search")]
        [AllowHtml]
        public string SearchName { get; set; }
        public DqRuleListModel()
        {
            QualityDimensions = new List<SelectListItem>();
        }
        [DisplayName("Record Id")]
        public int RECORD_ID { get; set; }

        [DisplayName("Datasource")]
        public string DATA_SOURCE { get; set; }
        [DisplayName("Catalog")]

        public int CATALOG_ID { get; set; }
        [DisplayName("Rule Name")]
        [Required]

        public string RULE_NAME { get; set; }
        [DisplayName("Population Query")]
        public string POP_QUERY { get; set; }
        [DisplayName("Exception Query")]
        //[AllowHtml]
        [Required]
        public string EXCEPTION_QUERY { get; set; }
        [DisplayName("Descriptoon and Resolution")]
        public string DESCRIPTION_RESOLUTION { get; set; }

        [DisplayName("Schedule")]
        public string RUN_SCHEDULE { get; set; }
        [DisplayName("Dimension")]
        public string DIMENSION { get; set; }
        [DisplayName("Severity")]
        public string SEVERITY { get; set; }
        [DisplayName("Last Run Date")]
        public DateTime? LAST_RUN { get; set; }
        public bool RECORD_STATUS { get; set; }
        [DisplayName("Quality Dimension")]
        public int DimensionId { get; set; }
        public IList<SelectListItem> QualityDimensions { get; set; }

    }
}