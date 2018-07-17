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
    public class WrongSchemeCodesController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IWrongSchemeCodeService _dqQueService;
        private IWscExportManager _exportManager;
        private IPermissionsService _permissionservice;
        private CustomIdentity identity;
        private IMessagingService _messagingService;

        #region Constructors
        public WrongSchemeCodesController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new WrongSchemeCodeService();
            _exportManager = new WscExportManager();
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

            var model = new WrongSchemeCodeModel();
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
            _messagingService.SaveUserActivity(identity.ProfileId, "Viewed Wrong Customer / Scheme Codes Mapping Report", DateTime.Now);
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult WrongSchemeCodesList(DataSourceRequest command, WrongSchemeCodeModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllWrongSchemeCodes(model.CIF_ID, model.FORACID, model.SOL_ID, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new WrongSchemeCodeModel
                {
                    Id = x.ID,
                    CIF_ID = x.CIF_ID,
                    FORACID = x.FORACID,
                    SCHM_CODE = x.SCHM_CODE,
                    ACCOUNTOFFICER_CODE = x.ACCOUNTOFFICER_CODE,
                    ACCOUNTOFFICER_NAME = x.ACCOUNTOFFICER_NAME,
                    SCHMECODE_CLASSIFIATION = x.SCHMECODE_CLASSIFIATION,
                    ACCT_NAME = x.ACCT_NAME,
                    SOL_ID = x.SOL_ID,
                    CUSTOMER_TYPE = x.CUSTOMER_TYPE,
                    DATE_OF_RUN = x.DATE_OF_RUN,
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost, ActionName("List")]
        [FormValueRequired("exportexcel-all")]
        public virtual ActionResult ExportExcelAll(WrongSchemeCode model)
        {

            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var items = _dqQueService.GetAllWrongSchemeCodes(model.CIF_ID, model.FORACID, model.SOL_ID);

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(items);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Wrong Customer / Scheme Codes Mapping Report", DateTime.Now);
                return File(bytes, MimeTypes.TextXlsx, "wrongSchemeCodes.xlsx");
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

            var docs = new List<WrongSchemeCode>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                docs.AddRange(_dqQueService.GetWrongSchemeCodebyIds(ids));
            }

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(docs);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Wrong Customer / Scheme Codes Mapping Report", DateTime.Now);
                return File(bytes, MimeTypes.TextXlsx, "wrongSchemeCodes.xlsx");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

        #region scaffolded
        // GET: WrongSchemeCodes
        public ActionResult Index_()
        {
            return View(db.CMDM_WRONGSCHCODECLASS.ToList());
        }

        // GET: WrongSchemeCodes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_WRONGSCHCODECLASS cMDM_WRONGSCHCODECLASS = db.CMDM_WRONGSCHCODECLASS.Find(id);
            if (cMDM_WRONGSCHCODECLASS == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_WRONGSCHCODECLASS);
        }

        // GET: WrongSchemeCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WrongSchemeCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CIF_ID,FORACID,SCHM_CODE,ACCOUNTOFFICER_CODE,ACCOUNTOFFICER_NAME,SCHMECODE_CLASSIFIATION,ACCT_NAME,SOL_ID,CUSTOMER_TYPE,DATE_OF_RUN")] CMDM_WRONGSCHCODECLASS cMDM_WRONGSCHCODECLASS)
        {
            if (ModelState.IsValid)
            {
                db.CMDM_WRONGSCHCODECLASS.Add(cMDM_WRONGSCHCODECLASS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cMDM_WRONGSCHCODECLASS);
        }

        // GET: WrongSchemeCodes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_WRONGSCHCODECLASS cMDM_WRONGSCHCODECLASS = db.CMDM_WRONGSCHCODECLASS.Find(id);
            if (cMDM_WRONGSCHCODECLASS == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_WRONGSCHCODECLASS);
        }

        // POST: WrongSchemeCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CIF_ID,FORACID,SCHM_CODE,ACCOUNTOFFICER_CODE,ACCOUNTOFFICER_NAME,SCHMECODE_CLASSIFIATION,ACCT_NAME,SOL_ID,CUSTOMER_TYPE,DATE_OF_RUN")] CMDM_WRONGSCHCODECLASS cMDM_WRONGSCHCODECLASS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cMDM_WRONGSCHCODECLASS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cMDM_WRONGSCHCODECLASS);
        }

        // GET: WrongSchemeCodes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_WRONGSCHCODECLASS cMDM_WRONGSCHCODECLASS = db.CMDM_WRONGSCHCODECLASS.Find(id);
            if (cMDM_WRONGSCHCODECLASS == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_WRONGSCHCODECLASS);
        }

        // POST: WrongSchemeCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CMDM_WRONGSCHCODECLASS cMDM_WRONGSCHCODECLASS = db.CMDM_WRONGSCHCODECLASS.Find(id);
            db.CMDM_WRONGSCHCODECLASS.Remove(cMDM_WRONGSCHCODECLASS);
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
