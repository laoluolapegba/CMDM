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
using CMdm.Services.Messaging;

namespace CMdm.UI.Web.Controllers
{
    public class OutStandingDocsController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private ICustomService _dqQueService;
        private IExportManager _exportManager;
        private IPermissionsService _permissionservice;
        private CustomIdentity identity;
        private IMessagingService _messagingService;

        #region Constructors
        public OutStandingDocsController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new CustomService();             
            _exportManager = new ExportManager();
            _messagingService = new MessagingService();

            _permissionservice = new PermissionsService();
        }
        #endregion
        // GET: OutStandingDocs
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

       
        public ActionResult List()
        {

            var model = new DistinctDocsModel();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            identity = ((CustomPrincipal)User).CustomIdentity;
            _permissionservice = new PermissionsService(identity.Name, identity.UserRoleId);

            IQueryable<CM_BRANCH> curBranchList = db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME); //.Where(a => a.BRANCH_ID == identity.BranchId);

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
                    Text = "All",
                    Selected = true
                });
            }

            model.CustomerTypes.Add(new SelectListItem
            {
                Value = "Individual",
                Text = "Individual",
            });

            model.CustomerTypes.Add(new SelectListItem
            {
                Value = "Corporate",
                Text = "Corporate",
            });

            model.CustomerTypes.Add(new SelectListItem
            {
                Value = "0",
                Text = "All",
                Selected = true
            });

            _messagingService.SaveUserActivity(identity.ProfileId, "Viewed Customer Outstanding Documents Report", DateTime.Now);
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult DistinctList(DataSourceRequest command, DistinctDocsModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllDistinctDocs(model.ACCT_NAME, model.CIF_ID, model.SOL_ID, model.CUSTOMERTYPE, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new DistinctDocsModel
                {
                    Id = x.ID,
                    ACCT_NAME = x.ACCT_NAME,
                    DUE_DATE = x.DUE_DATE,
                    BRANCH_NAME = x.BRANCH_NAME,
                    FREZ_REASON_CODE = x.FREZ_REASON_CODE,
                    SOL_ID = x.SOL_ID,
                    ACCTOFFICER_CODE = x.ACCTOFFICER_CODE,
                    ACCTOFFICER_NAME = x.ACCTOFFICER_NAME,
                    CIF_ID = x.CIF_ID,
                    CUSTOMERTYPE = x.CUSTOMERTYPE
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost]        
        public virtual ActionResult DocumentsList(DataSourceRequest command, OutstandingDocModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllOutDocItems(model.SearchName, model.CIF_ID, model.FORACID, model.BRANCH_CODE, model.CUSTOMERTYPE, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
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
                    BRANCH_NAME = x.BRANCH_NAME,
                    REF_DESC = x.REF_DESC,
                    SCHM_CODE = x.SCHM_CODE,
                    SCHM_DESC = x.SCHM_DESC,
                    SCHM_TYPE = x.SCHM_TYPE,
                    FREZ_REASON_CODE = x.FREZ_REASON_CODE,
                    SOL_ID = x.SOL_ID,
                    ACCTOFFICER_CODE = x.ACCTOFFICER_CODE,
                    ACCTOFFICER_NAME = x.ACCTOFFICER_NAME,
                    CIF_ID = x.CIF_ID,
                    CUSTOMERTYPE = x.CUSTOMERTYPE
                    //Id = x.RECORD_ID

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

            var model = new OutstandingDocModel();
            model.CIF_ID = id;

            var curBranchList = db.CM_BRANCH.Where(a => a.BRANCH_ID == identity.BranchId);
            model.Branches = new SelectList(curBranchList, "BRANCH_ID", "BRANCH_NAME").ToList();
            
            model.Branches.Add(new SelectListItem
            {
                Value = "0",
                Text = "All",
                Selected = true
            });

            model.CustomerTypes.Add(new SelectListItem
            {
                Value = "Individual",
                Text = "Individual",
            });

            model.CustomerTypes.Add(new SelectListItem
            {
                Value = "Corporate",
                Text = "Corporate",
            });

            model.CustomerTypes.Add(new SelectListItem
            {
                Value = "0",
                Text = "All",
                Selected = true
            });

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult AuthList(DataSourceRequest command, OutstandingDocModel model, string sort, string sortDir)
        {
            var identity = ((CustomPrincipal)User).CustomIdentity;
            var routeValues = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values;
            //RouteValueDictionary routeValues;

            string goldenRecord = "";
            if (routeValues.ContainsKey("id"))
                goldenRecord = (string)routeValues["id"];

            var items = _dqQueService.GetAllOutDocItems(model.ACCT_NAME, goldenRecord, model.FORACID, model.BRANCH_CODE, model.CUSTOMERTYPE, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
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
                    BRANCH_NAME = x.BRANCH_NAME,
                    REF_DESC = x.REF_DESC,
                    SCHM_CODE = x.SCHM_CODE,
                    SCHM_DESC = x.SCHM_DESC,
                    SCHM_TYPE = x.SCHM_TYPE,
                    FREZ_REASON_CODE = x.FREZ_REASON_CODE,
                    SOL_ID = x.SOL_ID,
                    ACCTOFFICER_CODE = x.ACCTOFFICER_CODE,
                    ACCTOFFICER_NAME = x.ACCTOFFICER_NAME,
                    CIF_ID = x.CIF_ID,
                    CUSTOMERTYPE = x.CUSTOMERTYPE
                    //Id = x.RECORD_ID

                    }),
                    Total = items.TotalCount,
                };

            return Json(gridModel);
        }


        #region Export / Import
        [HttpPost, ActionName("List")]
        [FormValueRequired("exportexcel-all")]
        public virtual ActionResult ExportExcelAll(DistinctDocsModel model)
        {

            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var items = _dqQueService.GetAllDistinctDocs(model.ACCT_NAME, model.CIF_ID, model.SOL_ID);

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(items);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Customers With Outstanding Documents Report", DateTime.Now);
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

            var docs = new List<DistinctDocs>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                docs.AddRange(_dqQueService.GetDistinctDocsbyIds(ids));
            }

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(docs);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Customers With Outstanding Documents Report", DateTime.Now);
                return File(bytes, MimeTypes.TextXlsx, "outstandingDocs.xlsx");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public virtual ActionResult ExportOutExcelSelected(string selectedIds)
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
                byte[] bytes = _exportManager.ExportOutDocumentsToXlsx(docs);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Customers With Outstanding Documents Report", DateTime.Now);
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
