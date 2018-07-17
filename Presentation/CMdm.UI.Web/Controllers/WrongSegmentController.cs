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
using CMdm.Data.Rbac;
using CMdm.Entities.Domain.User;
using CMdm.Services.Messaging;

namespace CMdm.UI.Web.Controllers
{
    public class WrongSegmentController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        private IWrongSegmentService _dqQueService;
        private IWSegExportManager _exportManager;
        private IPermissionsService _permissionservice;
        private CustomIdentity identity;
        private IMessagingService _messagingService;

        #region Constructors
        public WrongSegmentController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new WrongSegmentService();
            _exportManager = new WSegExportManager();
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

            var model = new WrongSegmentModel();
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

            _messagingService.SaveUserActivity(identity.ProfileId, "Viewed Segment / Subsegment Mapping Report", DateTime.Now);
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult WrongSegmentsList(DataSourceRequest command, WrongSegmentModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllWrongSegments(model.ORGKEY, model.ACCOUNTNO, model.CUST_FIRST_NAME, model.CUST_MIDDLE_NAME, model.CUST_LAST_NAME, model.PRIMARY_SOL_ID, 
                command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new WrongSegmentModel
                {
                    Id = x.ID,
                    ORGKEY = x.ORGKEY,
                    CUST_FIRST_NAME = x.CUST_FIRST_NAME,
                    CUST_MIDDLE_NAME = x.CUST_MIDDLE_NAME,
                    CUST_LAST_NAME = x.CUST_LAST_NAME,
                    GENDER = x.GENDER,
                    CUST_DOB = x.CUST_DOB,
                    PRIMARY_SOL_ID = x.PRIMARY_SOL_ID,
                    SEGMENTATION_CLASS = x.SEGMENTATION_CLASS,
                    SEGMENTNAME = x.SEGMENTNAME,
                    SUBSEGMENT = x.SUBSEGMENT,
                    SUBSEGMENTNAME = x.SUBSEGMENTNAME,
                    CORP_ID = x.CORP_ID,
                    DATE_OF_RUN = x.DATE_OF_RUN,
                    SCHEME_CODE = x.SCHEME_CODE,
                    ACCOUNTNO = x.ACCOUNTNO,
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost, ActionName("List")]
        [FormValueRequired("exportexcel-all")]
        public virtual ActionResult ExportExcelAll(WrongSegment model)
        {

            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var items = _dqQueService.GetAllWrongSegments(model.ORGKEY, model.ACCOUNTNO, model.CUST_FIRST_NAME, model.CUST_MIDDLE_NAME, model.CUST_LAST_NAME, model.PRIMARY_SOL_ID);

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(items);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Segment Subsegment Mapping Report", DateTime.Now);
                return File(bytes, MimeTypes.TextXlsx, "wrongSegment.xlsx");
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

            var docs = new List<WrongSegment>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                docs.AddRange(_dqQueService.GetWrongSegmentbyIds(ids));
            }

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(docs);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Segment Subsegment Mapping Report", DateTime.Now);
                return File(bytes, MimeTypes.TextXlsx, "wrongSegment.xlsx");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }
        #region scaffolded
        // GET: WrongSegment
        public ActionResult Index_()
        {
            return View(db.CMDM_WRONG_SEGMENTSUBSEGMENT.ToList());
        }

        // GET: WrongSegment/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_WRONG_SEGMENTSUBSEGMENT cMDM_WRONG_SEGMENTSUBSEGMENT = db.CMDM_WRONG_SEGMENTSUBSEGMENT.Find(id);
            if (cMDM_WRONG_SEGMENTSUBSEGMENT == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_WRONG_SEGMENTSUBSEGMENT);
        }

        // GET: WrongSegment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WrongSegment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ORGKEY,CUST_FIRST_NAME,CUST_MIDDLE_NAME,CUST_LAST_NAME,GENDER,CUST_DOB,PRIMARY_SOL_ID,SEGMENTATION_CLASS,SEGMENTNAME,SUBSEGMENT,SUBSEGMENTNAME,CORP_ID,DATE_OF_RUN")] CMDM_WRONG_SEGMENTSUBSEGMENT cMDM_WRONG_SEGMENTSUBSEGMENT)
        {
            if (ModelState.IsValid)
            {
                db.CMDM_WRONG_SEGMENTSUBSEGMENT.Add(cMDM_WRONG_SEGMENTSUBSEGMENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cMDM_WRONG_SEGMENTSUBSEGMENT);
        }

        // GET: WrongSegment/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_WRONG_SEGMENTSUBSEGMENT cMDM_WRONG_SEGMENTSUBSEGMENT = db.CMDM_WRONG_SEGMENTSUBSEGMENT.Find(id);
            if (cMDM_WRONG_SEGMENTSUBSEGMENT == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_WRONG_SEGMENTSUBSEGMENT);
        }

        // POST: WrongSegment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ORGKEY,CUST_FIRST_NAME,CUST_MIDDLE_NAME,CUST_LAST_NAME,GENDER,CUST_DOB,PRIMARY_SOL_ID,SEGMENTATION_CLASS,SEGMENTNAME,SUBSEGMENT,SUBSEGMENTNAME,CORP_ID,DATE_OF_RUN")] CMDM_WRONG_SEGMENTSUBSEGMENT cMDM_WRONG_SEGMENTSUBSEGMENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cMDM_WRONG_SEGMENTSUBSEGMENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cMDM_WRONG_SEGMENTSUBSEGMENT);
        }

        // GET: WrongSegment/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_WRONG_SEGMENTSUBSEGMENT cMDM_WRONG_SEGMENTSUBSEGMENT = db.CMDM_WRONG_SEGMENTSUBSEGMENT.Find(id);
            if (cMDM_WRONG_SEGMENTSUBSEGMENT == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_WRONG_SEGMENTSUBSEGMENT);
        }

        // POST: WrongSegment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CMDM_WRONG_SEGMENTSUBSEGMENT cMDM_WRONG_SEGMENTSUBSEGMENT = db.CMDM_WRONG_SEGMENTSUBSEGMENT.Find(id);
            db.CMDM_WRONG_SEGMENTSUBSEGMENT.Remove(cMDM_WRONG_SEGMENTSUBSEGMENT);
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
