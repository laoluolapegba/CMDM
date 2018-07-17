using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CMdm.Data;
using CMdm.Framework.Kendoui;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Services.GoldenRecord;
using CMdm.UI.Web.Models.GoldenRecord;
using CMdm.Framework.Controllers;
using CMdm.Core;
using CMdm.Services.ExportImport;
using CMdm.Entities.Domain.GoldenRecord;
using CMdm.Services.Security;
using CMdm.Data.Rbac;
using CMdm.Entities.Domain.User;
using CMdm.Services.Messaging;

namespace CMdm.UI.Web.Controllers
{
    public class MultipleIdsController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IGoldenRecordService _dqQueService;
        private IGrdExportManager _exportManager;
        private IPermissionsService _permissionservice;
        private CustomIdentity identity;
        private IMessagingService _messagingService;

        public MultipleIdsController()
        {
            _dqQueService = new GoldenRecordService();
            _exportManager = new GrdExportManager();
            _messagingService = new MessagingService();

            _permissionservice = new PermissionsService();
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
            //return View(db.AccountOfficers.ToList());
        }

        public ActionResult List()
        {
            var model = new GoldenRecordModel();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            identity = ((CustomPrincipal)User).CustomIdentity;
            _permissionservice = new PermissionsService(identity.Name, identity.UserRoleId);

            IQueryable<CM_BRANCH> curBranchList = db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME); //.Where(a => a.BRANCH_ID == identity.BranchId);

            if (_permissionservice.IsLevel(AuthorizationLevel.Enterprise))
            {

            }
            else if (_permissionservice.IsLevel(AuthorizationLevel.Regional))
            {
                curBranchList = curBranchList.Where(a => a.REGION_ID == identity.RegionId);
            }
            else if (_permissionservice.IsLevel(AuthorizationLevel.Zonal))
            {
                curBranchList = curBranchList.Where(a => a.ZONECODE == identity.ZoneId).OrderBy(a => a.BRANCH_NAME);
            }
            else if (_permissionservice.IsLevel(AuthorizationLevel.Branch))
            {
                curBranchList = curBranchList.Where(a => a.BRANCH_ID == identity.BranchId).OrderBy(a => a.BRANCH_NAME);
            }
            else
            {
                curBranchList = curBranchList.Where(a => a.BRANCH_ID == "-1");
            }

            model.Branches = new SelectList(curBranchList, "BRANCH_ID", "BRANCH_NAME").ToList();


            if (_permissionservice.IsLevel(AuthorizationLevel.Enterprise))
            {
                model.Branches.Add(new SelectListItem
                {
                    Value = "0",
                    Text = "All",
                    Selected = true
                });
            }
            _messagingService.SaveUserActivity(identity.ProfileId, "Viewed Customers with Multiple Ids Report", DateTime.Now);

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult GoldenRecordsList(DataSourceRequest command, GoldenRecordModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllQueItems(model.FULL_NAME, model.CUSTOMER_NO, model.EMAIL, model.ACCOUNT_NO, model.BVN, model.GOLDEN_RECORD, model.BRANCH_CODE, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new GoldenRecordModel
                {
                    Id = x.RECORD_ID,
                    GOLDEN_RECORD = x.GOLDEN_RECORD,
                    CUSTOMER_NO = x.CUSTOMER_NO,
                    BVN = x.BVN,
                    FULL_NAME = x.FULL_NAME,
                    DATE_OF_BIRTH = x.DATE_OF_BIRTH,
                    RESIDENTIAL_ADDRESS = x.RESIDENTIAL_ADDRESS,
                    CUSTOMER_TYPE = x.CUSTOMER_TYPE,
                    SEX = x.SEX,
                    BRANCH_CODE = x.BRANCH_CODE,
                    PHONE_NUMBER = x.PHONE_NUMBER,
                    SCHEME_CODE = x.SCHEME_CODE,
                    ACCOUNT_NO = x.ACCOUNT_NO,
                    EMAIL = x.EMAIL,
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost, ActionName("List")]
        [FormValueRequired("exportexcel-all")]
        public virtual ActionResult ExportExcelAll(GoldenRecordModel model)
        {

            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var items = _dqQueService.GetAllQueItems(model.FULL_NAME, model.CUSTOMER_NO, model.EMAIL, model.ACCOUNT_NO, model.BVN, model.GOLDEN_RECORD, model.BRANCH_CODE);

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(items);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Customers with Multiple Ids Report", DateTime.Now);
                return File(bytes, MimeTypes.TextXlsx, "multipleIDs.xlsx");
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

            var docs = new List<CdmaGoldenRecord>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                docs.AddRange(_dqQueService.GetQueItembyIds(ids));
            }

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(docs);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Customers with Multiple Ids Report", DateTime.Now);
                return File(bytes, MimeTypes.TextXlsx, "multipleIDs.xlsx");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

        #region scaffolded
        // GET: MultipleIds
        public ActionResult Index_()
        {
            return View(db.CdmaGoldenRecords.ToList());
        }

        // GET: MultipleIds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CdmaGoldenRecord cdmaGoldenRecord = db.CdmaGoldenRecords.Find(id);
            if (cdmaGoldenRecord == null)
            {
                return HttpNotFound();
            }
            return View(cdmaGoldenRecord);
        }

        // GET: MultipleIds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MultipleIds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RECORD_ID,GOLDEN_RECORD,CUSTOMER_NO,BVN,FULL_NAME,DATE_OF_BIRTH,RESIDENTIAL_ADDRESS,CUSTOMER_TYPE,SEX,BRANCH_CODE,RECORD_STATUS")] CdmaGoldenRecord cdmaGoldenRecord)
        {
            if (ModelState.IsValid)
            {
                db.CdmaGoldenRecords.Add(cdmaGoldenRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cdmaGoldenRecord);
        }

        // GET: MultipleIds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CdmaGoldenRecord cdmaGoldenRecord = db.CdmaGoldenRecords.Find(id);
            if (cdmaGoldenRecord == null)
            {
                return HttpNotFound();
            }
            return View(cdmaGoldenRecord);
        }

        // POST: MultipleIds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RECORD_ID,GOLDEN_RECORD,CUSTOMER_NO,BVN,FULL_NAME,DATE_OF_BIRTH,RESIDENTIAL_ADDRESS,CUSTOMER_TYPE,SEX,BRANCH_CODE,RECORD_STATUS")] CdmaGoldenRecord cdmaGoldenRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cdmaGoldenRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cdmaGoldenRecord);
        }

        // GET: MultipleIds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CdmaGoldenRecord cdmaGoldenRecord = db.CdmaGoldenRecords.Find(id);
            if (cdmaGoldenRecord == null)
            {
                return HttpNotFound();
            }
            return View(cdmaGoldenRecord);
        }

        // POST: MultipleIds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CdmaGoldenRecord cdmaGoldenRecord = db.CdmaGoldenRecords.Find(id);
            db.CdmaGoldenRecords.Remove(cdmaGoldenRecord);
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
