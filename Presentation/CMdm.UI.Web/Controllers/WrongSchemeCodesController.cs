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

namespace CMdm.UI.Web.Controllers
{
    public class WrongSchemeCodesController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IWrongSchemeCodeService _dqQueService;
        private IWscExportManager _exportManager;

        #region Constructors
        public WrongSchemeCodesController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new WrongSchemeCodeService();
            _exportManager = new WscExportManager();
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
            var identity = ((CustomPrincipal)User).CustomIdentity;
            var curBranchList = db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME); //.Where(a => a.BRANCH_ID == identity.BranchId);
            model.Branches = new SelectList(curBranchList, "BRANCH_ID", "BRANCH_NAME").ToList();

            model.Branches.Add(new SelectListItem
            {
                Value = "0",
                Text = "All",
                Selected = true
            });
            //Test OrderBy
            //model.Branches.OrderBy(x => x.Text);

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult WrongSchemeCodesList(DataSourceRequest command, WrongSchemeCodeModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllWrongSchemeCodes(model.FORACID, model.SOL_ID, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
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
            var items = _dqQueService.GetAllWrongSchemeCodes(model.FORACID, model.SOL_ID);

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(items);
                return File(bytes, MimeTypes.TextXlsx, "accountOfficers.xlsx");
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
