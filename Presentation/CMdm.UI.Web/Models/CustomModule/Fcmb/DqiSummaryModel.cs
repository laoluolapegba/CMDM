using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.CustomModule.Fcmb
{
    public class DqiSummaryModel
    {
        public DqiSummaryModel()
        {
            Branches = new List<SelectListItem>();
        }

        [DisplayName("Process ID ")]
        public int? PROCESS_ID { get; set; }
        [DisplayName("Branch Name ")]
        public string BRANCH_CODE { get; set; }
        [DisplayName("DQI Result ")]
        public int? DQI_RESULT { get; set; }
        [DisplayName("Previous DQI Result ")]
        public int? PREVIOUS_DQI_RESULT { get; set; }
        [DisplayName("Catalog ID ")]
        public string MDM_CATALOG_ID { get; set; }
        [DisplayName("Date Last Run ")]
        public DateTime? DAT_LAST_RUN { get; set; }

        public int Id
        {
            get; set;
        }

        public IList<SelectListItem> Branches { get; set; }
    }
}