using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.DqCatalogs
{
    public class DqCatalogListModel
    {
        
        public DqCatalogListModel ()
        {
            Categories = new List<SelectListItem>();
        }
        public int CATALOG_ID { get; set; }
        [DisplayName("Catalog name")]
        public string CATALOG_NAME { get; set; }
        [DisplayName("Category")]
        [Required]
        public int CATEGORY_ID { get; set; }
        [DisplayName("Created by")]
        public string CREATED_BY { get; set; }
        [DisplayName("Created Date")]

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        [DisplayName("Last Modified by")]
        public string LAST_MODIFIED_BY { get; set; }
        [DisplayName("Last Modified Date")]

        public DateTime? LAST_MODIFIED_DATE { get; set; }

        //default category
        [DisplayName("Category")]
        [AllowHtml]
        public int DefaultCategoryId { get; set; }
        public IList<SelectListItem> Categories { get; set; }

    }
}