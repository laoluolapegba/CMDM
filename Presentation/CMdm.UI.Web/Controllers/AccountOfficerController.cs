using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CMdm.Data;
using CMdm.Entities.Domain.CustomModule.Fcmb;
using CMdm.Framework.Kendoui;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Services.CustomModule.Fcmb;
using CMdm.UI.Web.Models.CustomModule.Fcmb;
using CMdm.Framework.Controllers;
using CMdm.Core;
using CMdm.Services.ExportImport;

namespace CMdm.UI.Web.Controllers
{
    public class AccountOfficerController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IAccountOfficerService _dqQueService;
        private IAccExportManager _exportManager;

        #region Constructors
        public AccountOfficerController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new AccountOfficerService();
            _exportManager = new AccExportManager();
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

            var model = new AccountOfficerModel();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            var curBranchList = db.CM_BRANCH; //.Where(a => a.BRANCH_ID == identity.BranchId);
            model.Branches = new SelectList(curBranchList, "BRANCH_ID", "BRANCH_NAME").ToList();

            model.Branches.Add(new SelectListItem
            {
                Value = "0",
                Text = "All",
                Selected = true
            });

            model.AccountOfficers.Add(new SelectListItem
            {
                Value = "VACANT",
                Text = "Vacant"
            });

            model.AccountOfficers.Add(new SelectListItem
            {
                Value = null,
                Text = "None"
            });

            model.AccountOfficers.Add(new SelectListItem
            {  
                Value = "0",
                Text = "All",
                Selected = true
            });

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult AccountOfficersList(DataSourceRequest command, AccountOfficerModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllAccountOfficers(model.SearchName, model.AO_NAME, model.SOL_ID, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new AccountOfficerModel
                {
                    Id = x.ID,
                    ACCOUNT_NUMBER = x.ACCOUNT_NUMBER,
                    ACCOUNT_NAME = x.ACCOUNT_NAME,
                    SOL_ID = x.SOL_ID,
                    BRANCH = x.BRANCH,
                    SCHM_CODE = x.SCHM_CODE,
                    ACCT_OPN_DATE = x.ACCT_OPN_DATE,
                    AO_CODE = x.AO_CODE,
                    AO_NAME = x.AO_NAME,
                    SBU_CODE = x.SBU_CODE,
                    SBU_NAME = x.SBU_NAME,
                    BROKER_CODE = x.BROKER_CODE,
                    BROKER_NAME = x.BROKER_NAME,
                    RUN_DATE = x.RUN_DATE,
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost, ActionName("List")]
        [FormValueRequired("exportexcel-all")]
        public virtual ActionResult ExportExcelAll(AccountOfficerModel model)
        {

            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var items = _dqQueService.GetAllAccountOfficers(model.SearchName, model.AO_NAME, model.SOL_ID);

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

            var docs = new List<AccountOfficer>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                docs.AddRange(_dqQueService.GetAccountOfficerbyIds(ids));
            }

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(docs);
                return File(bytes, MimeTypes.TextXlsx, "accountOfficers.xlsx");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

        #region scaffolded

        // GET: AccountOfficer/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TMP_ACCOUNTOFFICER_EXCEPTIONS tMP_ACCOUNTOFFICER_EXCEPTIONS = db.TMP_ACCOUNTOFFICER_EXCEPTIONS.Find(id);
        //    if (tMP_ACCOUNTOFFICER_EXCEPTIONS == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tMP_ACCOUNTOFFICER_EXCEPTIONS);
        //}

        //// GET: AccountOfficer/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: AccountOfficer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ACCOUNT_NUMBER,ACCOUNT_NAME,SOL_ID,BRANCH,SCHM_CODE,ACCT_OPN_DATE,AO_CODE,AO_NAME,SBU_CODE,SBU_NAME,BROKER_CODE,BROKER_NAME,RUN_DATE")] TMP_ACCOUNTOFFICER_EXCEPTIONS tMP_ACCOUNTOFFICER_EXCEPTIONS)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TMP_ACCOUNTOFFICER_EXCEPTIONS.Add(tMP_ACCOUNTOFFICER_EXCEPTIONS);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(tMP_ACCOUNTOFFICER_EXCEPTIONS);
        //}

        //// GET: AccountOfficer/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TMP_ACCOUNTOFFICER_EXCEPTIONS tMP_ACCOUNTOFFICER_EXCEPTIONS = db.TMP_ACCOUNTOFFICER_EXCEPTIONS.Find(id);
        //    if (tMP_ACCOUNTOFFICER_EXCEPTIONS == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tMP_ACCOUNTOFFICER_EXCEPTIONS);
        //}

        //// POST: AccountOfficer/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ACCOUNT_NUMBER,ACCOUNT_NAME,SOL_ID,BRANCH,SCHM_CODE,ACCT_OPN_DATE,AO_CODE,AO_NAME,SBU_CODE,SBU_NAME,BROKER_CODE,BROKER_NAME,RUN_DATE")] TMP_ACCOUNTOFFICER_EXCEPTIONS tMP_ACCOUNTOFFICER_EXCEPTIONS)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tMP_ACCOUNTOFFICER_EXCEPTIONS).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(tMP_ACCOUNTOFFICER_EXCEPTIONS);
        //}

        //// GET: AccountOfficer/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TMP_ACCOUNTOFFICER_EXCEPTIONS tMP_ACCOUNTOFFICER_EXCEPTIONS = db.TMP_ACCOUNTOFFICER_EXCEPTIONS.Find(id);
        //    if (tMP_ACCOUNTOFFICER_EXCEPTIONS == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tMP_ACCOUNTOFFICER_EXCEPTIONS);
        //}

        //// POST: AccountOfficer/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    TMP_ACCOUNTOFFICER_EXCEPTIONS tMP_ACCOUNTOFFICER_EXCEPTIONS = db.TMP_ACCOUNTOFFICER_EXCEPTIONS.Find(id);
        //    db.TMP_ACCOUNTOFFICER_EXCEPTIONS.Remove(tMP_ACCOUNTOFFICER_EXCEPTIONS);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        #endregion scaffolded
    }
}
