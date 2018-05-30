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
    public class EmailPhoneController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IEmailPhoneService _dqQueService;
        private IEPhoneExportManager _exportManager;

        #region Constructors
        public EmailPhoneController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new EmailPhoneService();
            _exportManager = new EPhoneExportManager();
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
            var model = new EmailPhoneModel();
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

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult EmailPhoneList(DataSourceRequest command, EmailPhoneModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllEmailPhones(model.ORGKEY, model.CUST_FIRST_NAME, model.CUST_MIDDLE_NAME, model.CUST_LAST_NAME, model.BRANCH_CODE,
                command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new EmailPhoneModel
                {
                    Id = x.ID,
                    ORGKEY = x.ORGKEY,
                    DUPLICATE_ID = x.DUPLICATE_ID,
                    CUST_FIRST_NAME = x.CUST_FIRST_NAME,
                    CUST_MIDDLE_NAME = x.CUST_MIDDLE_NAME,
                    CUST_LAST_NAME = x.CUST_LAST_NAME,
                    CUST_DOB = x.CUST_DOB,
                    BRANCH_CODE = x.BRANCH_CODE,
                    BRANCH_NAME = x.BRANCH_NAME,
                    BVN = x.BVN,
                    GENDER = x.GENDER,
                    CUSTOMERMINOR = x.CUSTOMERMINOR,
                    PREFERREDPHONE = x.PREFERREDPHONE,
                    EMAIL = x.EMAIL,
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost, ActionName("List")]
        [FormValueRequired("exportexcel-all")]
        public virtual ActionResult ExportExcelAll(EmailPhone model)
        {

            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var items = _dqQueService.GetAllEmailPhones(model.ORGKEY, model.CUST_FIRST_NAME, model.CUST_MIDDLE_NAME, model.CUST_LAST_NAME);

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(items);
                return File(bytes, MimeTypes.TextXlsx, "emailPhone.xlsx");
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

            var docs = new List<EmailPhone>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                docs.AddRange(_dqQueService.GetEmailPhonebyIds(ids));
            }

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(docs);
                return File(bytes, MimeTypes.TextXlsx, "emailPhone.xlsx");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }
        #region scaffolded
        // GET: EmailPhone
        public ActionResult Index_()
        {
            return View(db.CMDM_CMMN_EMAIL_PHONENO.ToList());
        }

        // GET: EmailPhone/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_CMMN_EMAIL_PHONENO cMDM_CMMN_EMAIL_PHONENO = db.CMDM_CMMN_EMAIL_PHONENO.Find(id);
            if (cMDM_CMMN_EMAIL_PHONENO == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_CMMN_EMAIL_PHONENO);
        }

        // GET: EmailPhone/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailPhone/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ORGKEY,DUPLICATE_ID,CUST_FIRST_NAME,CUST_MIDDLE_NAME,CUST_LAST_NAME,CUST_DOB,BVN,GENDER,CUSTOMERMINOR,PREFERREDPHONE,EMAIL")] CMDM_CMMN_EMAIL_PHONENO cMDM_CMMN_EMAIL_PHONENO)
        {
            if (ModelState.IsValid)
            {
                db.CMDM_CMMN_EMAIL_PHONENO.Add(cMDM_CMMN_EMAIL_PHONENO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cMDM_CMMN_EMAIL_PHONENO);
        }

        // GET: EmailPhone/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_CMMN_EMAIL_PHONENO cMDM_CMMN_EMAIL_PHONENO = db.CMDM_CMMN_EMAIL_PHONENO.Find(id);
            if (cMDM_CMMN_EMAIL_PHONENO == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_CMMN_EMAIL_PHONENO);
        }

        // POST: EmailPhone/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ORGKEY,DUPLICATE_ID,CUST_FIRST_NAME,CUST_MIDDLE_NAME,CUST_LAST_NAME,CUST_DOB,BVN,GENDER,CUSTOMERMINOR,PREFERREDPHONE,EMAIL")] CMDM_CMMN_EMAIL_PHONENO cMDM_CMMN_EMAIL_PHONENO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cMDM_CMMN_EMAIL_PHONENO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cMDM_CMMN_EMAIL_PHONENO);
        }

        // GET: EmailPhone/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_CMMN_EMAIL_PHONENO cMDM_CMMN_EMAIL_PHONENO = db.CMDM_CMMN_EMAIL_PHONENO.Find(id);
            if (cMDM_CMMN_EMAIL_PHONENO == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_CMMN_EMAIL_PHONENO);
        }

        // POST: EmailPhone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CMDM_CMMN_EMAIL_PHONENO cMDM_CMMN_EMAIL_PHONENO = db.CMDM_CMMN_EMAIL_PHONENO.Find(id);
            db.CMDM_CMMN_EMAIL_PHONENO.Remove(cMDM_CMMN_EMAIL_PHONENO);
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
