using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using CMdm.Entities.Domain.Entity;
using CMdm.Data;
using CMdm.UI.Web.Models.DqParam;

namespace CMdm.UI.Web.Controllers
{
    public class ParamsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult ParamList()
        {
            return View();
        }

        public ActionResult EntityDetails_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<MdmEntityDetails> entitydetails = db.EntityDetails;
            DataSourceResult result = entitydetails.ToDataSourceResult(request, c => new DqParamListModel 
            {
                ENTITY_DETAIL_ID = c.ENTITY_DETAIL_ID,
                ENTITY_ID = c.ENTITY_ID,
                ENTITY_TAB_NAME = c.ENTITY_TAB_NAME,
                ENTITY_COL_NAME = c.ENTITY_COL_NAME,
                FLG_MANDATORY = c.FLG_MANDATORY,
                WEIGHT_ID = c.WEIGHT_ID,
                CREATED_BY = c.CREATED_BY,
                //CREATED_DATE = c.CREATED_DATE,
                LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
                //LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                //RECORD_STATUS = c.RECORD_STATUS,
                REGEX = c.REGEX,
                DEFAULT_VALUE = c.DEFAULT_VALUE,
                COLUMN_ORDER = c.COLUMN_ORDER,
                CATALOG_ID = c.CATALOG_ID,
                USE_FOR_DQI = c.USE_FOR_DQI,
                DQ_DIMENSION = c.DQ_DIMENSION,
            });

            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
