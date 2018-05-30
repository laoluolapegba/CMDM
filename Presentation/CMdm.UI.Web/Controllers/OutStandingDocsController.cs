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
using CMdm.Framework.Controllers;
using CMdm.Core;
using CMdm.Services.ExportImport;
using CMdm.Services.Security;
using CMdm.Data.Rbac;
using CMdm.Entities.Domain.User;

namespace CMdm.UI.Web.Controllers
{
    public class OutStandingDocsController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private ICustomService _dqQueService;
        private IExportManager _exportManager;
        private IPermissionsService _permissionservice;
        private CustomIdentity identity;
        #region Constructors
        public OutStandingDocsController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new CustomService();
             
            _exportManager = new ExportManager();
            _permissionservice = new PermissionsService();
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
        public ActionResult Edit([Bind(Include = "FORACID,ACCT_NAME,SOL_ID,SCHM_CODE,SCHM_DESC,SCHM_TYPE,ACID,DOCUMENT_CODE,DUE_DATE,FREZ_REASON_CODE,ACCTOFFICER_CODE,ACCTOFFICER_NAME")] OutStandingDoc outStandingDoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outStandingDoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(outStandingDoc);
        }
        //[Authorize]
        public ActionResult List()
        {

            var model = new OutstandingDocModel();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            identity = ((CustomPrincipal)User).CustomIdentity;
            _permissionservice = new PermissionsService(identity.Name, identity.UserRoleId);

            IQueryable<CM_BRANCH> curBranchList = db.CM_BRANCH; //.Where(a => a.BRANCH_ID == identity.BranchId);

            if(_permissionservice.IsLevel(AuthorizationLevel.Enterprise))
            {
                
            }
            else if(_permissionservice.IsLevel(AuthorizationLevel.Regional))
            {
                curBranchList = curBranchList.Where(a => a.REGION_ID == identity.RegionId);
            }
            else if (_permissionservice.IsLevel(AuthorizationLevel.Zonal))
            {
                curBranchList = curBranchList.Where(a => a.ZONECODE == identity.ZoneId).OrderBy(a => a.BRANCH_NAME);
            }
            else if (_permissionservice.IsLevel(AuthorizationLevel.Branch))
            {
                curBranchList = curBranchList.Where(a => a.BRANCH_ID == identity.BranchId).OrderBy(a=>a.BRANCH_NAME);
            }
            else
            {
                curBranchList = curBranchList.Where(a => a.BRANCH_ID == "-1");
            }

            model.Branches = new SelectList(curBranchList, "BRANCH_ID", "BRANCH_NAME").ToList();


            if(_permissionservice.IsLevel(AuthorizationLevel.Enterprise))
            {
                model.Branches.Add(new SelectListItem
                {
                    Value = "0",
                    Text = "All"
                });
            }
           
            return View(model);
        }
        [HttpPost]
        
        public virtual ActionResult DocumentsList(DataSourceRequest command, OutstandingDocModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllOutDocItems(model.SearchName, model.ACID, model.BRANCH_CODE, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new OutstandingDocModel
                {
                    Id = x.ID,
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
                    SOL_ID = x.SOL_ID,
                    ACCTOFFICER_CODE = x.ACCTOFFICER_CODE,
                    ACCTOFFICER_NAME = x.ACCTOFFICER_NAME
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
        #region Export / Import

        [HttpPost, ActionName("List")]
        [FormValueRequired("exportexcel-all")]
        public virtual ActionResult ExportExcelAll(OutstandingDocModel model)
        {

            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var items = _dqQueService.GetAllOutDocItems(model.SearchName, model.ACID);

           

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(items);
                return File(bytes, MimeTypes.TextXlsx, "outstandingDocs.xlsx");
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

            var docs = new List<OutStandingDoc>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                docs.AddRange(_dqQueService.GetOutDocItembyIds(ids));
            }

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(docs);
                return File(bytes, MimeTypes.TextXlsx, "outstandingDocs.xlsx");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

       #endregion
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
