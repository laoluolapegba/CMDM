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
    public class MultipleRefCodeController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IMultipleRefCodeService _dqQueService;
        private IMrcExportManager _exportManager;

        #region Constructors
        public MultipleRefCodeController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new MultipleRefCodeService();
            _exportManager = new MrcExportManager();
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
            var model = new MultipleRefCodeModel();
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
        public virtual ActionResult MultipleRefCodesList(DataSourceRequest command, WrongSchemeCodeModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllMultipleRefCodes(model.FORACID, model.SOL_ID, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
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

        [HttpPost, ActionName("List")]
        [FormValueRequired("exportexcel-all")]
        public virtual ActionResult ExportExcelAll(MultipleRefCode model)
        {

            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var items = _dqQueService.GetAllMultipleRefCodes(model.FORACID, model.SOL_ID);

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(items);
                return File(bytes, MimeTypes.TextXlsx, "multipleRefCodes.xlsx");
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
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(docs);
                return File(bytes, MimeTypes.TextXlsx, "multipleRefCodes.xlsx");
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
