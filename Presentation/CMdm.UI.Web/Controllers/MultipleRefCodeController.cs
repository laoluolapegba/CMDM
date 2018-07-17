using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMdm.Data;
using CMdm.Entities.Domain.CustomModule.Fcmb;
using CMdm.Entities.Domain.Customer;
using CMdm.Framework.Kendoui;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Services.CustomModule.Fcmb;
using CMdm.UI.Web.Models.CustomModule.Fcmb;
using CMdm.Framework.Controllers;
using CMdm.Core;
using CMdm.Services.ExportImport;
using CMdm.Services.Security;
using CMdm.Services.Messaging;

namespace CMdm.UI.Web.Controllers
{
    public class MultipleRefCodeController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IMultipleRefCodeService _dqQueService;
        private IMrcExportManager _exportManager;
        private IPermissionsService _permissionservice;
        private CustomIdentity identity;
        private IMessagingService _messagingService;

        #region Constructors
        public MultipleRefCodeController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new MultipleRefCodeService();
            _exportManager = new MrcExportManager();
            _messagingService = new MessagingService();

            _permissionservice = new PermissionsService();
        }
        #endregion Constructors

        // GET: AccountOfficer
        public ActionResult Index()
        {
            return RedirectToAction("List");
            //return View(db.AccountOfficers.ToList());
        }

        public ActionResult List()
        {
            var model = new DistinctRefCodeModel();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            identity = ((CustomPrincipal)User).CustomIdentity;
            _permissionservice = new PermissionsService(identity.Name, identity.UserRoleId);

            _messagingService.SaveUserActivity(identity.ProfileId, "Viewed Multiple AO Codes Report", DateTime.Now);
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult DistinctRefCodesList(DataSourceRequest command, DistinctRefCodeModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllDistinctRefCodes(model.ACCOUNTOFFICER_NAME, model.REF_CODE, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new DistinctRefCodeModel
                {
                    Id = x.ID,
                    ACCOUNTOFFICER_NAME = x.ACCOUNTOFFICER_NAME,
                    REF_CODE = x.REF_CODE,
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }


        [HttpPost]
        public virtual ActionResult MultipleRefCodesList(DataSourceRequest command, MultipleRefCodeModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllMultipleRefCodes(model.FORACID, model.REF_CODE, model.SOL_ID, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new MultipleRefCodeModel
                {
                    Id = x.ID,
                    CIF_ID = x.CIF_ID,
                    FORACID = x.FORACID,
                    DUPLICATION_ID = x.DUPLICATION_ID,
                    ACCOUNTOFFICER_NAME = x.ACCOUNTOFFICER_NAME,
                    REF_CODE = x.REF_CODE,
                    SOL_ID = x.SOL_ID,
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult AuthList(string id)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;

            var model = new MultipleRefCodeModel();
            model.REF_CODE = id;

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


            model.Branches.Add(new SelectListItem
            {
                Value = "0",
                Text = "All",
                Selected = true
            });

            //model.Branches.Add(new SelectListItem
            //{
            //    Value = "0",
            //    Text = "All"
            //});
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult AuthList(DataSourceRequest command, MultipleRefCodeModel model, string sort, string sortDir)
        {
            //DateTime? startDateValue = (model.CreatedOnFrom == null) ? null
            //    : (DateTime?)model.CreatedOnFrom.Value;

            //DateTime? endDateValue = (model.CreatedOnTo == null) ? null
            //                : (DateTime?)model.CreatedOnTo.Value.AddDays(1);

            var identity = ((CustomPrincipal)User).CustomIdentity;
            var routeValues = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values;
            //RouteValueDictionary routeValues;

            string goldenRecord = "";
            if (routeValues.ContainsKey("id"))
                goldenRecord = (string)routeValues["id"];

            var items = _dqQueService.GetAllMultipleRefCodes(model.FORACID, goldenRecord, model.SOL_ID, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new MultipleRefCodeModel
                {
                    Id = x.ID,
                    CIF_ID = x.CIF_ID,
                    FORACID = x.FORACID,
                    DUPLICATION_ID = x.DUPLICATION_ID,
                    ACCOUNTOFFICER_NAME = x.ACCOUNTOFFICER_NAME,
                    REF_CODE = x.REF_CODE,
                    SOL_ID = x.SOL_ID,
                    RUN_DATE = x.RUN_DATE,
                    SCHM_CODE = x.SCHM_CODE
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost, ActionName("List")]
        [FormValueRequired("exportexcel-all")]
        public virtual ActionResult ExportExcelAll(DistinctRefCode model)
        {

            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var items = _dqQueService.GetAllDistinctRefCodes(model.ACCOUNTOFFICER_NAME, model.REF_CODE);

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(items);
                return File(bytes, MimeTypes.TextXlsx, "multipleAOCodes.xlsx");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public virtual ActionResult ExportExcelSelected(string selectedIds)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            var docs = new List<DistinctRefCode>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                docs.AddRange(_dqQueService.GetDistinctRefCodebyIds(ids));
            }

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(docs);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Multiple AO Codes Report", DateTime.Now);
                return File(bytes, MimeTypes.TextXlsx, "multipleAOCodes.xlsx");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public virtual ActionResult ExportMultExcelSelected(string selectedIds)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            var docs = new List<MultipleRefCode>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                docs.AddRange(_dqQueService.GetMultipleRefCodebyIds(ids));
            }

            try
            {
                byte[] bytes = _exportManager.ExportMrcToXlsx(docs);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Multiple AO Codes Report", DateTime.Now);
                return File(bytes, MimeTypes.TextXlsx, "multipleAOCodes.xlsx");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

        #region scaffolded
        // GET: MultipleRefCode
        public ActionResult Index_()
        {
            return View(db.CMDM_MULTIPLE_REF_CODE.ToList());
        }

        // GET: MultipleRefCode/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_MULTIPLE_REF_CODE cMDM_MULTIPLE_REF_CODE = db.CMDM_MULTIPLE_REF_CODE.Find(id);
            if (cMDM_MULTIPLE_REF_CODE == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_MULTIPLE_REF_CODE);
        }

        // GET: MultipleRefCode/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MultipleRefCode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FORACID,DUPLICATION_ID,ACCOUNTOFFICER_NAME,REF_CODE,SOL_ID,CIF_ID")] CMDM_MULTIPLE_REF_CODE cMDM_MULTIPLE_REF_CODE)
        {
            if (ModelState.IsValid)
            {
                db.CMDM_MULTIPLE_REF_CODE.Add(cMDM_MULTIPLE_REF_CODE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cMDM_MULTIPLE_REF_CODE);
        }

        // GET: MultipleRefCode/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_MULTIPLE_REF_CODE cMDM_MULTIPLE_REF_CODE = db.CMDM_MULTIPLE_REF_CODE.Find(id);
            if (cMDM_MULTIPLE_REF_CODE == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_MULTIPLE_REF_CODE);
        }

        // POST: MultipleRefCode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FORACID,DUPLICATION_ID,ACCOUNTOFFICER_NAME,REF_CODE,SOL_ID,CIF_ID")] CMDM_MULTIPLE_REF_CODE cMDM_MULTIPLE_REF_CODE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cMDM_MULTIPLE_REF_CODE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cMDM_MULTIPLE_REF_CODE);
        }

        // GET: MultipleRefCode/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_MULTIPLE_REF_CODE cMDM_MULTIPLE_REF_CODE = db.CMDM_MULTIPLE_REF_CODE.Find(id);
            if (cMDM_MULTIPLE_REF_CODE == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_MULTIPLE_REF_CODE);
        }

        // POST: MultipleRefCode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CMDM_MULTIPLE_REF_CODE cMDM_MULTIPLE_REF_CODE = db.CMDM_MULTIPLE_REF_CODE.Find(id);
            db.CMDM_MULTIPLE_REF_CODE.Remove(cMDM_MULTIPLE_REF_CODE);
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
        #endregion scaffolded
    }
}
