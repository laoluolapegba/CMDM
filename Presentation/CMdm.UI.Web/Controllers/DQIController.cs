using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMdm.Data;
using CMdm.Entities.Domain.Mdm;
using CMdm.Framework.Kendoui;
using CMdm.UI.Web.Models.DqParam;
using CMdm.Services.DqParam;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Framework.Mvc;
using Kendo.Mvc.Extensions;
//using Kendo.Mvc.UI;
//using Kendo.Mvc.UI;

namespace CMdm.UI.Web.Controllers
{
    public class DQIController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IDqParamService _dqParamService;
        public DQIController ()
        {
            
            _dqParamService = new DqParamService();
        }
        // GET: DQI
        public ActionResult Index()
        {
            return RedirectToAction("ListParams");
            //return View(db.MDM_WEIGHTS.ToList());
        }

        // GET: DQI/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmWeights mdmWeights = db.MDM_WEIGHTS.Find(id);
            if (mdmWeights == null)
            {
                return HttpNotFound();
            }
            return View(mdmWeights);
        }

        // GET: DQI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DQI/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WEIGHT_ID,WEIGHT_VALUE,CREATED_BY,CREATED_DATE,LAST_MODIFIED_BY,LAST_MODIFIED_DATE,RECORD_STATUS")] MdmWeights mdmWeights)
        {
            if (ModelState.IsValid)
            {
                db.MDM_WEIGHTS.Add(mdmWeights);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mdmWeights);
        }

        // GET: DQI/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmWeights mdmWeights = db.MDM_WEIGHTS.Find(id);
            if (mdmWeights == null)
            {
                return HttpNotFound();
            }
            return View(mdmWeights);
        }

        // POST: DQI/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WEIGHT_ID,WEIGHT_VALUE,CREATED_BY,CREATED_DATE,LAST_MODIFIED_BY,LAST_MODIFIED_DATE,RECORD_STATUS")] MdmWeights mdmWeights)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mdmWeights).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mdmWeights);
        }

        // GET: DQI/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmWeights mdmWeights = db.MDM_WEIGHTS.Find(id);
            if (mdmWeights == null)
            {
                return HttpNotFound();
            }
            return View(mdmWeights);
        }

        // POST: DQI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            MdmWeights mdmWeights = db.MDM_WEIGHTS.Find(id);
            db.MDM_WEIGHTS.Remove(mdmWeights);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DQIParams()
        {
            var dqiParams = db.MdmDqiParams.Include(m => m.MdmWeights); //.Include(m => m.MdmRegex);
            return View(dqiParams.ToList());
        }
        public ActionResult ListParams(int? CatalogId)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            var model = new DqParamListModel();
            model.CATALOG_ID = CatalogId;

            model.Catalogs = new SelectList(db.MdmCatalogs.Where(x => x.ENABLED == 1), "CATALOG_ID", "CATALOG_NAME").ToList();
            //model.RegexList = new SelectList(db.MdmRegex, "REGEX_ID", "REGEX_NAME").ToList();
            model.EntityList = new SelectList(db.EntityMast.Where(x => x.RECORD_STATUS == "1"), "ENTITY_ID", "ENTITY_NAME").ToList();
            model.Dimensions = new SelectList(db.MDM_AGGR_DIMENSION, "DIMENSIONID", "DIMENSION_NAME").ToList();
            model.Catalogs.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });

            model.EntityList.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });
            model.Dimensions.Add(new SelectListItem
            {
                Value = "0",
                Text = "All",
                Selected = true
            });
            //var weights = from Weights in db.MDM_WEIGHTS
            //              select new WeightsViewModel
            //              {
            //                  WEIGHT_ID = Weights.WEIGHT_ID,
            //                  WEIGHT_DESC = Weights.WEIGHT_DESC
            //              };

            //model.Weights = weights.ToList();
            PopulateWeights();
            return View(model);
        }

        public ActionResult ListParams_()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            var model = new DqParamListModel();

            model.Catalogs = new SelectList(db.MdmCatalogs, "CATALOG_ID", "CATALOG_NAME").ToList();
            //model.RegexList = new SelectList(db.MdmRegex, "REGEX_ID", "REGEX_NAME").ToList();
            model.EntityList = new SelectList(db.EntityMast, "ENTITY_ID", "ENTITY_NAME").ToList();
            model.Dimensions = new SelectList(db.MDM_AGGR_DIMENSION, "DIMENSIONID", "DIMENSION_NAME").ToList();
            model.Catalogs.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });
           
            model.EntityList.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });
            model.Dimensions.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });
            //var weights = from Weights in db.MDM_WEIGHTS
            //              select new WeightsViewModel
            //              {
            //                  WEIGHT_ID = Weights.WEIGHT_ID,
            //                  WEIGHT_DESC = Weights.WEIGHT_DESC
            //              };

            //model.Weights = weights.ToList();
            PopulateWeights();
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult ListParams(DataSourceRequest command, DqParamListModel model, string sort, string sortDir)
        {
            //model.DQ_DIMENSION = 1;

            var items = _dqParamService.GetAllEntities(model.SearchName, model.DQ_DIMENSION, model.ENTITY_ID, model.CATALOG_ID, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(c => new DqParamListModel
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
                    RECORD_STATUS = c.RECORD_STATUS,
                    REGEX = c.REGEX,
                    DEFAULT_VALUE = c.DEFAULT_VALUE,
                    COLUMN_ORDER = c.COLUMN_ORDER,
                    CATALOG_ID = c.CATALOG_ID,
                    USE_FOR_DQI = c.USE_FOR_DQI,
                    DQ_DIMENSION = c.DQ_DIMENSION,
                    WEIGHT_DESC = c.MDM_WEIGHTS.WEIGHT_DESC,
                    Weights = new WeightsViewModel()
                    {
                        WEIGHT_ID = c.MDM_WEIGHTS.WEIGHT_ID,
                        WEIGHT_DESC = c.MDM_WEIGHTS.WEIGHT_DESC
                    },
                    RegexCbo = new RegexsViewModel()
                    {
                        REGEX_ID = c.MdmRegex.REGEX_ID,
                        REGEX_NAME = c.MdmRegex.REGEX_NAME
                    },
                    DimsCbo = new DimensionsViewModel()
                    {
                        DIMENSIONID = c.MdmAggrDimensions.DIMENSIONID,
                        DIMENSION_NAME = c.MdmAggrDimensions.DIMENSION_NAME
                    },
                    CatalogsCbo = new CatalogsViewModel()
                    {
                        CATALOG_ID = c.MdmCatalog.CATALOG_ID,
                        CATALOG_NAME = c.MdmCatalog.CATALOG_NAME
                    },
                    //RECORD_ID = x.RECORD_ID,
                    //DATA_SOURCE = x.DATA_SOURCE,
                    //CATALOG_NAME = x.CATALOG_NAME,
                    //ERROR_DESC = x.ERROR_DESC,
                    //CREATED_DATE = x.CREATED_DATE
                }),
                Total = items.TotalCount
            };

        
            return Json(gridModel);
        }
        [HttpPost]
        public virtual ActionResult UpdateParams([Kendo.Mvc.UI.DataSourceRequest] Kendo.Mvc.UI.DataSourceRequest request, IEnumerable<DqParamListModel> paramList)
        {            
            if (paramList != null && ModelState.IsValid)
            {
                foreach (var pModel in paramList)
                {
                    var dbrec = _dqParamService.GetItembyId(pModel.ENTITY_DETAIL_ID);

                    if (dbrec != null)
                    {
                        dbrec.DEFAULT_VALUE = pModel.DEFAULT_VALUE;
                        dbrec.USE_FOR_DQI = pModel.USE_FOR_DQI;
                        dbrec.WEIGHT_ID = pModel.WEIGHT_ID;
                        dbrec.REGEX = pModel.REGEX;
                        dbrec.FLG_MANDATORY = pModel.FLG_MANDATORY;
                        _dqParamService.UpdateParamItem(dbrec);
                    }
                        
                }
            }
            //    var gridModel = new DataSourceResult();
            //return new NullJsonResult();
            return Json(paramList.ToDataSourceResult(request, ModelState));
        }

        

        private void PopulateWeights()
        {
            var dataContext = new AppDbContext();
            var Weights = dataContext.MDM_WEIGHTS
                        .Select(c => new WeightsViewModel
                        {
                            WEIGHT_ID = c.WEIGHT_ID,
                            WEIGHT_DESC = c.WEIGHT_DESC
                        })
                        .OrderBy(e => e.WEIGHT_DESC);

            ViewData["weights"] = Weights;
            ViewData["defaultWeight"] = Weights.First();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
