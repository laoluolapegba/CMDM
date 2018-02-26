using CMdm.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.DqParam
{
    public partial class DqParamListModel : BaseModel
    {
        [DisplayName("Search")]
        [AllowHtml]
        public string SearchName { get; set; }
        public DqParamListModel()
        {
            EntityList = new List<SelectListItem>();
            Dimensions = new List<SelectListItem>();
            Catalogs = new List<SelectListItem>();
        }
        [Key]
        public int ENTITY_DETAIL_ID { get; set; }

        [DisplayName("Entity Table")]
        public int ENTITY_ID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Entity tablename")]
        public string ENTITY_TAB_NAME { get; set; }

        [StringLength(50)]
        [DisplayName("Entity name")]
        
        public string ENTITY_COL_NAME { get; set; }

        public bool FLG_MANDATORY { get; set; }
        //public bool FLG_MANDATORY_BOOL
        //{
        //    get
        //    {
        //        return (FLG_MANDATORY !=0);
        //    }
        //}


        public int WEIGHT_ID { get; set; }

        [StringLength(20)]
        public string CREATED_BY { get; set; }

        //public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string LAST_MODIFIED_BY { get; set; }

        //public DateTime? LAST_MODIFIED_DATE { get; set; }

        public bool RECORD_STATUS { get; set; }
        [DisplayName("Regular Exression")]
        public int REGEX { get; set; }
        public string DEFAULT_VALUE { get; set; }
        public int COLUMN_ORDER { get; set; }
        [DisplayName("Data Catalog")]
        public int? CATALOG_ID { get; set; }
        [DisplayName("Use for Dqi")]
        public bool USE_FOR_DQI { get; set; }
        [DisplayName("Quality Dimension")]
        public int DQ_DIMENSION { get; set; }
        public string WEIGHT_DESC { get; set; }
        public WeightsViewModel Weights
        {
            get;
            set;
        }
        public CatalogsViewModel CatalogsCbo
        {
            get;
            set;
        }
        public DimensionsViewModel DimsCbo
        {
            get;
            set;
        }
        public RegexsViewModel RegexCbo
        {
            get;
            set;
        }
        public IList<SelectListItem> Dimensions { get; set; }
        public IList<SelectListItem> Catalogs { get; set; }
        //public IList<SelectListItem> RegexList { get; set; }
        public IList<SelectListItem> EntityList { get; set; }
        
        
    }

    public class WeightsViewModel
    {
        public int WEIGHT_ID { get; set; }
        public string WEIGHT_DESC { get; set; }
    }
    public class DimensionsViewModel
    {
        public int DIMENSIONID { get; set; }
        public string DIMENSION_NAME { get; set; }
    }
    public class CatalogsViewModel
    {
        public int CATALOG_ID { get; set; }
        public string CATALOG_NAME { get; set; }
    }
    public class RegexsViewModel
    {
        public int REGEX_ID { get; set; }
        public string REGEX_NAME { get; set; }
    }
}