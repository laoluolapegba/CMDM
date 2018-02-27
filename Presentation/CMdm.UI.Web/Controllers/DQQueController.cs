using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMdm.UI.Web.BLL;
using CMdm.Data;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Entities.Domain.Dqi;
using CMdm.Framework.Kendoui;
using CMdm.Services.DqQue;
using CMdm.UI.Web.Models.DqQue;
//using CMdm.Core;

namespace CMdm.UI.Web.Controllers
{
    public class DQQueController : BaseController
    {
        #region Fields

        private IDqQueService _dqQueService;
        private AppDbContext db = new AppDbContext();
        private DQQueBiz bizrule;
        #endregion
        #region Constructors
        public DQQueController()
        {
            bizrule = new DQQueBiz();
            _dqQueService = new DqQueService();
        }
        #endregion

        #region Methods
        #region Que list / create / edit / delete
        public ActionResult Index()
        {
            /*
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            ViewBag.BrnQueCount = bizrule.GetDQQuesCountbyBrn(identity.BranchId);
            var mdmDQQues = db.MdmDQQues.Include(m => m.MdmDQImpacts).Include(m => m.MdmDQPriorities).Include(m => m.MdmDQQueStatuses);
            return View(mdmDQQues.ToList().OrderBy(a => a.RECORD_ID));*/
            return RedirectToAction("List");
        }
        // GET: MdmDQQues
        public ActionResult Index_()
        {
            //|TODO implement a permission provider Service
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageDataQualityQue))o
            //    return AccessDeniedView();
            //var dqQue - new 
            if(User == null)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            ViewBag.BrnQueCount = bizrule.GetDQQuesCountbyBrn(identity.BranchId);
            var mdmDQQues = db.MdmDQQues.Include(m => m.MdmDQImpacts).Include(m => m.MdmDQPriorities).Include(m => m.MdmDQQueStatuses);
            return View(mdmDQQues.ToList().OrderBy(a=>a.RECORD_ID));
        }
        public ActionResult List()
        {

            var model = new DqQueListModel();
            return View(model);
        }
        [HttpPost]
        public virtual ActionResult List(DataSourceRequest command, DqQueListModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllQueItems(model.SearchName, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new DqQueListModel
                {
                    RECORD_ID = x.RECORD_ID,
                    DATA_SOURCE = x.DATA_SOURCE,
                    CATALOG_NAME = x.CATALOG_NAME,
                    DQ_PROCESS_NAME = x.DQ_PROCESS_NAME,
                    ERROR_DESC = x.ERROR_DESC,
                    DaysonQue = Math.Round((_today - x.CREATED_DATE).Value.TotalDays) + " days",
                    PCT_COMPLETION = x.PCT_COMPLETION,
                    PRIORITY = x.PRIORITY,
                    ISSUE_PRIORITY_DESC = x.MdmDQPriorities.PRIORITY_DESCRIPTION,
                    CATALOG_ID = x.CATALOG_ID,
                    CREATED_DATE = x.CREATED_DATE// _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc)
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
        public ActionResult BrnIssueList(int? CatalogId, int? branchid)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;

            var model = new DqquebrnListModel();
            

            model.CATALOG_ID = CatalogId == null ? 0: Convert.ToInt32(CatalogId);
            //foreach (var at in _dqService.GetAllActivityTypes())
            //{
            //    model.ActivityLogType.Add(new SelectListItem
            //    {
            //        Value = at.Id.ToString(),
            //        Text = at.Name
            //    });
            //}
            var curBranchList = db.CM_BRANCH.Where(a => a.BRANCH_ID == identity.BranchId);
            model.Branches = new SelectList(curBranchList, "BRANCH_ID", "BRANCH_NAME").ToList();
            
            model.Statuses = new SelectList(db.MdmDQQueStatuses, "STATUS_CODE", "STATUS_DESCRIPTION").ToList();
            model.Priorities = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION").ToList();
            model.Catalogs = new SelectList(db.MdmCatalogs, "CATALOG_ID", "CATALOG_NAME", CatalogId).ToList();
            model.Statuses.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });
            model.Priorities.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });
            model.Catalogs.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });
            //model.Branches.Add(new SelectListItem
            //{
            //    Value = "0",
            //    Text = "All"
            //});
            return View(model);
        }
        [HttpPost]
        public virtual ActionResult BrnIssueList(DataSourceRequest command, DqquebrnListModel model, string sort, string sortDir)
        {
            DateTime? startDateValue = (model.CreatedOnFrom == null) ? null
                : (DateTime?)model.CreatedOnFrom.Value;

            DateTime? endDateValue = (model.CreatedOnTo == null) ? null
                            : (DateTime?)model.CreatedOnTo.Value.AddDays(1);
            //startDateValue, endDateValue,
            IssueStatus? issueStatus = model.STATUS_CODE > 0 ? (IssueStatus?)(model.STATUS_CODE) : null;

            var identity = ((CustomPrincipal)User).CustomIdentity;

            var items = _dqQueService.GetAllBrnQueIssues(model.SearchName, model.CATALOG_ID, model.RULE_ID,  identity.BranchId, issueStatus, model.PRIORITY_CODE, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new DqquebrnListModel
                {
                    CUST_ID = x.CUST_ID,
                    RULE_NAME = x.RULE_NAME,
                    ISSUE_STATUS_DESC = x.MdmDQQueStatuses.STATUS_DESCRIPTION,
                    ISSUE_PRIORITY_DESC = x.MdmDQPriorities.PRIORITY_DESCRIPTION,
                    RUN_DATE = x.RUN_DATE,
                    BRANCH_CODE = x.BRANCH_CODE,
                    BRANCH_NAME = x.BRANCH_NAME,
                    CREATED_DATE = x.CREATED_DATE,
                    PRIORITY_CODE = x.ISSUE_PRIORITY,
                    STATUS_CODE = x.ISSUE_STATUS,
                    REASON = x.REASON,
                    CATALOG_ID = x.CATALOG_ID,
                    CATALOG_TABLE_NAME =x.CATALOG_TABLE_NAME

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

        public ActionResult AuthList()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;

            var model = new DqqueAuthListModel();
            //foreach (var at in _dqService.GetAllActivityTypes())
            //{
            //    model.ActivityLogType.Add(new SelectListItem
            //    {
            //        Value = at.Id.ToString(),
            //        Text = at.Name
            //    });
            //}
            var curBranchList = db.CM_BRANCH.Where(a => a.BRANCH_ID == identity.BranchId);
            model.Branches = new SelectList(curBranchList, "BRANCH_ID", "BRANCH_NAME").ToList();

            model.Statuses = new SelectList(db.MdmDQQueStatuses, "STATUS_CODE", "STATUS_DESCRIPTION").ToList();
            model.Priorities = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION").ToList();
            model.Catalogs = new SelectList(db.MdmCatalogs, "CATALOG_ID", "CATALOG_NAME").ToList();
            model.Statuses.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });
            model.Priorities.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });
            model.Catalogs.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });
            //model.Branches.Add(new SelectListItem
            //{
            //    Value = "0",
            //    Text = "All"
            //});
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult AuthList(DataSourceRequest command, DqqueAuthListModel model, string sort, string sortDir)
        {
            DateTime? startDateValue = (model.CreatedOnFrom == null) ? null
                : (DateTime?)model.CreatedOnFrom.Value;

            DateTime? endDateValue = (model.CreatedOnTo == null) ? null
                            : (DateTime?)model.CreatedOnTo.Value.AddDays(1);
            //startDateValue, endDateValue,
            IssueStatus? issueStatus = model.STATUS_CODE > 0 ? (IssueStatus?)(model.STATUS_CODE) : null;

            var identity = ((CustomPrincipal)User).CustomIdentity;

            var items = _dqQueService.GetAllBrnUnAuthIssues(model.SearchName, model.CATALOG_ID, model.RULE_ID, identity.BranchId, issueStatus, model.PRIORITY_CODE, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new DqqueAuthListModel
                {
                    EXCEPTION_ID = x.EXCEPTION_ID,
                    CUST_ID = x.CUST_ID,
                    RULE_NAME = x.RULE_NAME,
                    BRANCH_CODE = x.BRANCH_CODE,
                    ISSUE_STATUS_DESC = x.ISSUE_STATUS_DESC,
                    ISSUE_PRIORITY_DESC = x.ISSUE_PRIORITY_DESC,
                    RUN_DATE = x.RUN_DATE,
                    BRANCH_NAME = x.BRANCH_NAME,
                    CREATED_DATE = x.CREATED_DATE,
                    PRIORITY_CODE = x.ISSUE_PRIORITY,
                    STATUS_CODE = x.ISSUE_STATUS,
                    REASON = x.REASON,
                    FIRSTNAME = x.FIRST_NAME,
                    SURNAME = x.SURNAME,
                    OTHERNAME = x.OTHERNAME,
                    CATALOG_ID = x.CATALOG_ID,
                    CATALOG_TABLE_NAME = x.CATALOG_TABLE_NAME

                }),
                Total = items.TotalCount
            };

            

            return Json(gridModel);
        }

        public ActionResult Indexa()
        {
            var mdmDQQues = db.MdmDQQues.Include(m => m.MdmDQImpacts).Include(m => m.MdmDQPriorities).Include(m => m.MdmDQQueStatuses);
            return View(mdmDQQues.ToList());
        }
        public ActionResult DqDetails(int ruleid, string branchid)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            //ViewBag.BrnQueCount = bizrule.GetDQQuesCountbyBrn(identity.BranchId);
            var mdmDqRunExceptions = db.MdmDqRunExceptions.Where(a=>a.RULE_ID == ruleid).Where(a=>a.BRANCH_CODE==branchid).Include(m => m.MdmDQPriorities).Include(m => m.MdmDQQueStatuses);
            return View(mdmDqRunExceptions.ToList());
        }

        // GET: MdmDQQues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDQQue mdmDQQue = db.MdmDQQues.Find(id);
            if (mdmDQQue == null)
            {
                return HttpNotFound();
            }
            return View(mdmDQQue);
        }

        // GET: MdmDQQues/Create
        public ActionResult Create()
        {
            ViewBag.IMPACT_LEVEL = new SelectList(db.MdmDQImpacts, "IMPACT_CODE", "IMPACT_DESCRIPTION");
            ViewBag.PRIORITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION");
            ViewBag.QUE_STATUS = new SelectList(db.MdmDQQueStatuses, "STATUS_CODE", "STATUS_DESCRIPTION");
            return View();
        }

        // POST: MdmDQQues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MdmDQQue mdmDQQue)
        {
            if (ModelState.IsValid)
            {
                db.MdmDQQues.Add(mdmDQQue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IMPACT_LEVEL = new SelectList(db.MdmDQImpacts, "IMPACT_CODE", "IMPACT_DESCRIPTION", mdmDQQue.IMPACT_LEVEL);
            ViewBag.PRIORITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION", mdmDQQue.PRIORITY);
            ViewBag.QUE_STATUS = new SelectList(db.MdmDQQueStatuses, "STATUS_CODE", "STATUS_DESCRIPTION", mdmDQQue.QUE_STATUS);
            return View(mdmDQQue);
        }

        // GET: MdmDQQues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDQQue mdmDQQue = db.MdmDQQues.Find(id);
            if (mdmDQQue == null)
            {
                return HttpNotFound();
            }
            ViewBag.IMPACT_LEVEL = new SelectList(db.MdmDQImpacts, "IMPACT_CODE", "IMPACT_DESCRIPTION", mdmDQQue.IMPACT_LEVEL);
            ViewBag.PRIORITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION", mdmDQQue.PRIORITY);
            ViewBag.QUE_STATUS = new SelectList(db.MdmDQQueStatuses, "STATUS_CODE", "STATUS_DESCRIPTION", mdmDQQue.QUE_STATUS);
            return View(mdmDQQue);
        }

        // POST: MdmDQQues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RECORD_ID,DATA_SOURCE,CATALOG_NAME,ERROR_CODE,ERROR_DESC,DQ_PROCESS_NAME,IMPACT_LEVEL,PRIORITY,QUE_STATUS,CREATED_BY,CREATED_DATE")] MdmDQQue mdmDQQue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mdmDQQue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IMPACT_LEVEL = new SelectList(db.MdmDQImpacts, "IMPACT_CODE", "IMPACT_DESCRIPTION", mdmDQQue.IMPACT_LEVEL);
            ViewBag.PRIORITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION", mdmDQQue.PRIORITY);
            ViewBag.QUE_STATUS = new SelectList(db.MdmDQQueStatuses, "STATUS_CODE", "STATUS_DESCRIPTION", mdmDQQue.QUE_STATUS);
            return View(mdmDQQue);
        }

        // GET: MdmDQQues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDQQue mdmDQQue = db.MdmDQQues.Find(id);
            if (mdmDQQue == null)
            {
                return HttpNotFound();
            }
            return View(mdmDQQue);
        }

        // POST: MdmDQQues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MdmDQQue mdmDQQue = db.MdmDQQues.Find(id);
            db.MdmDQQues.Remove(mdmDQQue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
        #endregion
    }
}
