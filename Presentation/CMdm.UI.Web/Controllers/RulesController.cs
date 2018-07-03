using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMdm.Data;
using System.Configuration;
using Elmah;
//using CMdm.UI.Web.ActionFilters;
using Cdma.Web.Helpers.CrossCutting.StringHelper;
using Korzh.EasyQuery.Db;
using Korzh.EasyQuery.Services;
using Korzh.EasyQuery.Services.Db;
using Korzh.EasyQuery.Mvc;
//using Korzh.EasyQuery.
using Korzh.Utils.Db;
using Korzh.EasyQuery;
using System.Text;
using CMdm.Entities.Domain.Dqi;
using CMdm.UI.Web.Models.DqRule;
using CMdm.Framework.Kendoui;
using CMdm.Services.DqRule;
using System.Data.SqlClient;

namespace CMdm.UI.Web.Controllers
{
    [ValidateInput(false)]
    public class RulesController : Controller
    {
        private static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        private AppDbContext db = new AppDbContext();
        private EqServiceProviderDb eqService;
        private IDqRuleService _dqRuleService;
        #region methods

        // GET: Rules
        public ActionResult Index()
        {
            return RedirectToAction("List");
            //var mdmDqRules = db.MdmDqRules.Include(m => m.MdmAggrDimensions).Include(m => m.MdmDQDataSources).Include(m => m.MdmDQPriorities).Include(m => m.MdmDqRunSchedules);
            //return View(mdmDqRules.ToList());
        }
        public ActionResult List()
        {
            var model = new DqRuleListModel();
            //model.QualityDimensions = db.MDM_DQI_AGGR_TRANSACTIONS.ToList();
            model.QualityDimensions = new SelectList(db.MDM_AGGR_DIMENSION, "DIMENSIONID", "DIMENSION_NAME").ToList();
            model.QualityDimensions.Insert(0, new SelectListItem { Text = "All", Value = "0" });
            return View(model);
        }
        [HttpPost]
        public virtual ActionResult List(DataSourceRequest command, DqRuleListModel model, string sort, string sortDir)
        {

            var items = _dqRuleService.GetAllRuleItems(model.SearchName, model.DimensionId, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new DqRuleListModel
                {
                    RECORD_ID = x.RECORD_ID,
                    RULE_NAME = x.RULE_NAME,
                    DIMENSION = x.MdmAggrDimensions.DIMENSION_NAME,
                    DATA_SOURCE = x.MdmDQDataSources.DS_NAME,
                    SEVERITY  = x.MdmDQPriorities.PRIORITY_DESCRIPTION,
                    RUN_SCHEDULE = x.MdmDqRunSchedules.SCHEDULE_DESCRIPTION,
                    LAST_RUN = x.LAST_RUN
                    //CATALOG_NAME = x.CATALOG_NAME,
                    //ERROR_DESC = x.ERROR_DESC,
                    //CREATED_DATE = x.CREATED_DATE// _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc)
                }),
                Total = items.TotalCount
            };

            //var gridModel = new DataSourceResult
            //{
            //    Data = items.Select(x =>
            //    {
            //        var itemsModel = x.ToModel();
            //        PrepareSomethingModel(itemsModel, x, false, false);
            //        return itemsModel;
            //    }),
            //    Total = items.TotalCount,
            //};

            return Json(gridModel);
        }

        // GET: Rules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDqRule mdmDqRule = db.MdmDqRules.Find(id);
            if (mdmDqRule == null)
            {
                return HttpNotFound();
            }
            return View(mdmDqRule);
        }

        // GET: Rules/Create
        public ActionResult Create()
        {
            //var query = eqService.GetQuery();
            //ViewBag.QueryJson = query.SaveToDictionary().ToJson();
            ViewBag.Message = TempData["Message"];
            //return View("EasyQuery");
            ViewBag.DIMENSION = new SelectList(db.MDM_AGGR_DIMENSION, "DIMENSIONID", "DIMENSION_NAME");
            ViewBag.DATA_SOURCE_ID = new SelectList(db.MdmDQDataSources, "DS_ID", "DS_USERNAME");
            ViewBag.SEVERITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION");
            ViewBag.RUN_SCHEDULE = new SelectList(db.MdmDqRunSchedules, "SCHEDULE_ID", "SCHEDULE_DESCRIPTION");
            ViewBag.CATALOG_ID = new SelectList(db.MdmCatalogs, "CATALOG_ID", "CATALOG_NAME");
            return View();
        }

        // POST: Rules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[MultipleButton(Name = "action", Argument = "Create")]
        public ActionResult Create([Bind(Include = "RECORD_ID,DATA_SOURCE_ID,CATALOG_ID,RULE_NAME,POP_QUERY,EXCEPTION_QUERY,DESCRIPTION_RESOLUTION,RUN_SCHEDULE,DIMENSION,SEVERITY,LAST_RUN")] MdmDqRule mdmDqRule)
        {
            if (ModelState.IsValid)
            {
                db.MdmDqRules.Add(mdmDqRule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //var query = eqService.GetQuery();
            //ViewBag.QueryJson = query.SaveToDictionary().ToJson();
            ViewBag.Message = TempData["Message"];
            //return View("EasyQuery");

            ViewBag.DIMENSION = new SelectList(db.MDM_AGGR_DIMENSION, "DIMENSIONID", "DIMENSION_NAME", mdmDqRule.DIMENSION);
            ViewBag.DATA_SOURCE_ID = new SelectList(db.MdmDQDataSources, "DS_ID", "DS_USERNAME", mdmDqRule.DATA_SOURCE_ID);
            ViewBag.SEVERITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION", mdmDqRule.SEVERITY);
            ViewBag.RUN_SCHEDULE = new SelectList(db.MdmDqRunSchedules, "SCHEDULE_ID", "SCHEDULE_DESCRIPTION", mdmDqRule.RUN_SCHEDULE);
            ViewBag.CATALOG_ID = new SelectList(db.MdmCatalogs, "CATALOG_ID", "CATALOG_NAME");
            return View(mdmDqRule);
        }

        // GET: Rules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDqRule mdmDqRule = db.MdmDqRules.Find(id);
            if (mdmDqRule == null)
            {
                return HttpNotFound();
            }
            ViewBag.DIMENSION = new SelectList(db.MDM_AGGR_DIMENSION, "DIMENSIONID", "DIMENSION_NAME", mdmDqRule.DIMENSION);
            ViewBag.DATA_SOURCE_ID = new SelectList(db.MdmDQDataSources, "DS_ID", "DS_USERNAME", mdmDqRule.DATA_SOURCE_ID);
            ViewBag.SEVERITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION", mdmDqRule.SEVERITY);
            ViewBag.RUN_SCHEDULE = new SelectList(db.MdmDqRunSchedules, "SCHEDULE_ID", "SCHEDULE_DESCRIPTION", mdmDqRule.RUN_SCHEDULE);
            ViewBag.CATALOG_ID = new SelectList(db.MdmCatalogs, "CATALOG_ID", "CATALOG_NAME");
            return View(mdmDqRule);
        }

        // POST: Rules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RECORD_ID,DATA_SOURCE_ID,CATALOG_ID,RULE_NAME,POP_QUERY,EXCEPTION_QUERY,DESCRIPTION_RESOLUTION,RUN_SCHEDULE,DIMENSION,SEVERITY,LAST_RUN")] MdmDqRule mdmDqRule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mdmDqRule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DIMENSION = new SelectList(db.MDM_AGGR_DIMENSION, "DIMENSIONID", "DIMENSION_NAME", mdmDqRule.DIMENSION);
            ViewBag.DATA_SOURCE_ID = new SelectList(db.MdmDQDataSources, "DS_ID", "DS_USERNAME", mdmDqRule.DATA_SOURCE_ID);
            ViewBag.SEVERITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION", mdmDqRule.SEVERITY);
            ViewBag.RUN_SCHEDULE = new SelectList(db.MdmDqRunSchedules, "SCHEDULE_ID", "SCHEDULE_DESCRIPTION", mdmDqRule.RUN_SCHEDULE);
            ViewBag.CATALOG_ID = new SelectList(db.MdmCatalogs, "CATALOG_ID", "CATALOG_NAME");
            return View(mdmDqRule);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[MultipleButton(Name = "action", Argument = "Preview")]
        public ActionResult Preview(MdmDqRule mdmDqRule)
        {
            if (ModelState.IsValid)
            {
                string strMessage =  ExecuteSql(HMTLHelperExtensions.HtmlEncode(HttpUtility.HtmlDecode(mdmDqRule.EXCEPTION_QUERY)));
                if(strMessage != string.Empty)
                {
                    ModelState.AddModelError("EXCEPTION_QUERY", strMessage);
                }                
                
            }
            ViewBag.DIMENSION = new SelectList(db.MDM_AGGR_DIMENSION, "DIMENSIONID", "DIMENSION_NAME", mdmDqRule.DIMENSION);
            ViewBag.DATA_SOURCE_ID = new SelectList(db.MdmDQDataSources, "DS_ID", "DS_USERNAME", mdmDqRule.DATA_SOURCE_ID);
            ViewBag.SEVERITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION", mdmDqRule.SEVERITY);
            ViewBag.RUN_SCHEDULE = new SelectList(db.MdmDqRunSchedules, "SCHEDULE_ID", "SCHEDULE_DESCRIPTION", mdmDqRule.RUN_SCHEDULE);
            ViewBag.CATALOG_ID = new SelectList(db.MdmCatalogs, "CATALOG_ID", "CATALOG_NAME");
            return View("Create", mdmDqRule);
        }

        // GET: Rules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDqRule mdmDqRule = db.MdmDqRules.Find(id);
            if (mdmDqRule == null)
            {
                return HttpNotFound();
            }
            return View(mdmDqRule);
        }

        // POST: Rules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MdmDqRule mdmDqRule = db.MdmDqRules.Find(id);
            db.MdmDqRules.Remove(mdmDqRule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        static string  ExecuteSql(string in_strSQL)
        {
           
            using (SqlConnection connection = new SqlConnection(connString))
            {
                string strSQL = in_strSQL + " and 1 = 0";
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strSQL, connection);
                    //command.BindByName = true;
                    command.CommandType = System.Data.CommandType.Text;

                    //command.Parameters.Add(":table_name", OracleDbType.Varchar2).Value = RadListBoxSource.SelectedValue;
                    SqlDataReader rdr = command.ExecuteReader();
                    //gridCat.Rebind();
                    if (rdr.HasRows)
                    { }
                    return string.Empty;
                    // BindDDL();
                    //this.lblmsgs.Text = MessageFormatter.GetFormattedSuccessMessage("Catalog Added Succesfully");

                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                    return ex.Message;
                    //lblmsgs.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
                }

            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #region QueryBuilder

        
        public RulesController()
        {
            _dqRuleService = new DqRuleService();
            eqService = new EqServiceProviderDb();
            eqService.DefaultModelName = "cdmamodel"; // "NWindSQL";            

            eqService.SessionGetter = key => Session[key];
            eqService.SessionSetter = (key, value) => Session[key] = value;
            eqService.StoreQueryInSession = true;

            eqService.Formats.SetDefaultFormats(FormatType.Oracle);
            eqService.Formats.UseSchema = true;
            eqService.Formats.EOL = EOLSymbol.None;

            string dataPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data");
            eqService.DataPath = dataPath;
            //eqService.Connection = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(dataPath, "Northwind.sdf"));
            eqService.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ToString());
            eqService.CustomListResolver = (listname) =>
            {
                if (listname == "Regions")
                {
                    return new List<ListItem> {
                        new ListItem("US", "USA", new List<ListItem> {
                            new ListItem("CA","California"),
                            new ListItem("CO", "Colorado"),
                            new ListItem("OR","Oregon"),
                            new ListItem("WA", "Washington")
                        }),

                        new ListItem("CA", "CANADA", new List<ListItem> {
                            new ListItem("AB","Alberta"),
                            new ListItem("ON", "Ontario"),
                            new ListItem("QC", "Québec")
                        }),

                    };
                }
                return Enumerable.Empty<ListItem>();
            };

        }

        //public ActionResult Index()
        //{
        //    //var query = eqService.GetQuery();
        //    //ViewBag.QueryJson = query.SaveToDictionary().ToJson();
        //    ViewBag.Message = TempData["Message"];
        //    return View("EasyQuery");
        //}


        //reserved for future version
        //protected override void OnActionExecuting(ActionExecutingContext filterContext) {
        //    base.OnActionExecuting(filterContext);

        //    var prms  = filterContext.HttpContext.Request.Params;
        //    if (prms.Get("modelName") != null && prms.Get("modelId") == null)
        //        filterContext.ActionParameters["modelId"] = prms["modelName"];
        //    if (prms.Get("queryName") != null && prms.Get("queryId") == null)
        //        filterContext.ActionParameters["queryId"] = prms["queryName"];
        //}

        /// <summary>
        /// Creates a <see cref="T:System.Web.Mvc.JsonResult" /> object that serializes the specified object to JavaScript Object Notation (JSON) format using the content type, content encoding, and the JSON request behavior.
        /// </summary>
        /// <remarks>We override this method to set MaxJsonLength property to the maximum possible value</remarks>
        /// <param name="data">The JavaScript object graph to serialize.</param>
        /// <param name="contentType">The content type (MIME type).</param>
        /// <param name="contentEncoding">The content encoding.</param>
        /// <param name="behavior">The JSON request behavior</param>
        /// <returns>The result object that serializes the specified object to JSON format.</returns>
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = int.MaxValue
            };
        }

        #region public actions
        /// <summary>
        /// Gets the model by its name
        /// </summary>
        /// <param name="modelName">The name.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetModel(string modelName)
        {
            var model = eqService.GetModel(modelName);
            return Json(model.SaveToDictionary());
        }

        /// <summary>
        /// Gets the query by its name
        /// </summary>
        /// <param name="queryName">The name.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetQuery(string queryId)
        {
            var query = eqService.GetQuery(queryId);
            return Json(query.SaveToDictionary());
        }

        /// <summary>
        /// Saves the query.
        /// </summary>
        /// <param name="queryJson">The JSON representation of the query .</param>
        /// <param name="queryName">Query name.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveQuery(string queryJson, string queryName)
        {
            eqService.SaveQueryDict(queryJson.ToDictionary(), queryName);
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("result", "OK");
            return Json(dict);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public JsonResult GetQueryList(string modelName)
        {
            var queries = eqService.GetQueryList(modelName);
            return Json(queries, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// It's called when it's necessary to synchronize query on client side with its server-side copy.
        /// Additionally this action can be used to return a generated SQL statement (or several statements) as JSON string
        /// </summary>
        /// <param name="queryJson">The JSON representation of the query .</param>
        /// <param name="optionsJson">The additional parameters which can be passed to this method to adjust query statement generation.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SyncQuery(string queryJson, string optionsJson)
        {
            var query = eqService.SyncQueryDict(queryJson.ToDictionary());
            var statement = eqService.BuildQuery(query, optionsJson.ToDictionary());
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("statement", statement);
            return Json(dict);
        }

        /// <summary>
        /// This action returns a custom list by different list request options (list name).
        /// </summary>
        /// <param name="options">List request options - an instance of <see cref="ListRequestOptions"/> type.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetList(ListRequestOptions options)
        {
            return Json(eqService.GetList(options));
        }

        /// <summary>
        /// Executes the query passed as JSON string and returns the result record set (again as JSON).
        /// </summary>
        /// <param name="queryJson">The JSON representation of the query.</param>
        /// <param name="optionsJson">Different options in JSON format</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExecuteQuery(string queryJson, string optionsJson)
        {
            Dictionary<string, object> resultDict = new Dictionary<string, object>();
            var query = eqService.LoadQueryDict(queryJson.ToDictionary());

            //we can set different options before SQL generation:
            //query.Options.SelectDistinct = true;
            //query.Options.SelectTop = "100";
            //query.Options.LimitClause = "20";

            var sql = eqService.BuildQuery(query, optionsJson.ToDictionary());

            var resultSet = eqService.GetDataSetBySql(sql);

            var resultSetDict = eqService.DataSetToDictionary(resultSet, optionsJson.ToDictionary());

            resultDict.Add("statement", sql);
            resultDict.Add("resultSet", resultSetDict);
            resultDict.Add("resultCount", resultSet.Tables[0].Rows.Count + " record(s) found");


            return Json(resultDict);
        }


        private void ErrorResponse(string msg)
        {
            Response.StatusCode = 400;
            Response.Write(msg);
            Response.Output.Flush();
        }


        [HttpGet]
        public void ExportToFileExcel()
        {
            Response.Clear();

            var query = eqService.GetQuery();

            if (!query.IsEmpty)
            {
                var sql = eqService.BuildQuery(query);
                eqService.Paging.Enabled = false;
                DataSet dataset = eqService.GetDataSetBySql(sql);
                if (dataset != null)
                {
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition",
                        string.Format("attachment; filename=\"{0}\"", HttpUtility.UrlEncode("report.xls")));
                    DbExport.ExportToExcelHtml(dataset, Response.Output, HtmlFormats.Default);
                }
                else
                    ErrorResponse("Empty dataset");
            }
            else
                ErrorResponse("Empty query");

        }

        [HttpGet]
        public void ExportToFileCsv()
        {
            Response.Clear();
            var query = eqService.GetQuery();

            if (!query.IsEmpty)
            {
                var sql = eqService.BuildQuery(query);
                eqService.Paging.Enabled = false;
                DataSet dataset = eqService.GetDataSetBySql(sql);
                if (dataset != null)
                {
                    Response.ContentType = "text/csv";
                    Response.AddHeader("Content-Disposition",
                        string.Format("attachment; filename=\"{0}\"", HttpUtility.UrlEncode("report.csv")));
                    DbExport.ExportToCsv(dataset, Response.Output, CsvFormats.Default);
                }
                else
                    ErrorResponse("Empty dataset");
            }
            else
                ErrorResponse("Empty query");

        }

        [HttpGet]
        public FileResult GetCurrentQuery()
        {
            var query = eqService.GetQuery();
            FileContentResult result = new FileContentResult(Encoding.UTF8.GetBytes(query.SaveToString()), "Content-disposition: attachment;");
            result.FileDownloadName = "CurrentQuery.xml";
            return result;
        }

        [HttpPost]
        public ActionResult LoadQueryFromFile(HttpPostedFileBase queryFile)
        {
            if (queryFile != null && queryFile.ContentLength > 0)
                try
                {
                    var query = eqService.GetQuery();
                    query.LoadFromStream(queryFile.InputStream);
                    eqService.SyncQuery(query);
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                TempData["Message"] = "You have not specified a file.";
            }

            return RedirectToAction("Index");
        }


        #endregion
        #endregion
        #endregion
    }
}
