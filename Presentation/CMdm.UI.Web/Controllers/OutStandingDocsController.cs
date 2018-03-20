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
using CMdm.Framework.Kendoui;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Services.CustomModule.Fcmb;
using CMdm.UI.Web.Models.CustomModule.Fcmb;

namespace CMdm.UI.Web.Controllers
{
    public class OutStandingDocsController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private ICustomService _dqQueService;
        #region Constructors
        public OutStandingDocsController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new CustomService();
        }
        #endregion
        // GET: OutStandingDocs
        public ActionResult Index()
        {
            return View(db.OutStandingDocs.ToList());
        }

        // GET: OutStandingDocs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OutStandingDoc outStandingDoc = db.OutStandingDocs.Find(id);
            if (outStandingDoc == null)
            {
                return HttpNotFound();
            }
            return View(outStandingDoc);
        }

        
        // POST: OutStandingDocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FORACID,ACCT_NAME,SOL_ID,SCHM_CODE,SCHM_DESC,SCHM_TYPE,ACID,DOCUMENT_CODE,DUE_DATE,FREZ_REASON_CODE")] OutStandingDoc outStandingDoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outStandingDoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(outStandingDoc);
        }

        public ActionResult List()
        {

            var model = new OutstandingDocModel();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            var curBranchList = db.CM_BRANCH; //.Where(a => a.BRANCH_ID == identity.BranchId);
            model.Branches = new SelectList(curBranchList, "BRANCH_ID", "BRANCH_NAME").ToList();


            model.Branches.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });
            return View(model);
        }
        [HttpPost]
        public virtual ActionResult List(DataSourceRequest command, OutstandingDocModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllOutDocItems(model.SearchName, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new OutstandingDocModel
                {
                    ACCT_NAME = x.ACCT_NAME,
                    ACID = x.ACID,
                    FORACID = x.FORACID,
                    DOCUMENT_CODE = x.DOCUMENT_CODE,
                    DUE_DATE = x.DUE_DATE,

                    REF_DESC = x.REF_DESC,
                    SCHM_CODE = x.SCHM_CODE,
                    SCHM_DESC = x.SCHM_DESC,
                    SCHM_TYPE = x.SCHM_TYPE,
                    FREZ_REASON_CODE = x.FREZ_REASON_CODE,
                    SOL_ID = x.SOL_ID
                    //Id = x.RECORD_ID

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
