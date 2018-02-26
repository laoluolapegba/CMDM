using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.DqQue
{
    public class ChangedCustomerModel
    {
        public ChangedCustomerModel()
        {

            Priorities = new List<SelectListItem>();
            Statuses = new List<SelectListItem>();
            Branches = new List<SelectListItem>();
            Catalogs = new List<SelectListItem>();
        }
        [DisplayName("Search")]
        [AllowHtml]
        public string SearchName { get; set; }
        [DisplayName("FromDate")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnFrom { get; set; }

        [DisplayName("ToDate")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnTo { get; set; }

        public int CATALOG_ID { get; set; }
        public IList<SelectListItem> Branches { get; set; }
        public IList<SelectListItem> Priorities { get; set; }
        public IList<SelectListItem> Statuses { get; set; }
        public IList<SelectListItem> Catalogs { get; set; }
    }
}