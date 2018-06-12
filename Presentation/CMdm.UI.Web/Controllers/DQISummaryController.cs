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
using CMdm.Services.Security;
using CMdm.Data.Rbac;
using CMdm.Entities.Domain.User;

namespace CMdm.UI.Web.Controllers
{
    public class DQISummaryController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IDqiSummaryService _dqQueService;
        private IDqiExportManager _exportManager;
        private IPermissionsService _permissionservice;
        private CustomIdentity identity;

        #region Constructors
        public DQISummaryController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new DqiSummaryService();
            _exportManager = new DqiExportManager();

            _permissionservice = new PermissionsService();
        }
        #endregion Constructors

        // GET: DQISummary
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new DqiSummaryModel();
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
                    Text = "All"
                });
            }

            
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult DqiSummaryList(DataSourceRequest command, DqiSummaryModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllDqiSummary(model.BRANCH_CODE, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new DqiSummaryModel
                {
                    Id = x.ID,
                    PROCESS_ID = x.PROCESS_ID,
                    BRANCH_CODE = x.BRANCH_CODE,
                    PREVIOUS_DQI_RESULT = x.PREVIOUS_DQI_RESULT,
                    DQI_RESULT = x.DQI_RESULT,
                    MDM_CATALOG_ID = x.MDM_CATALOG_ID,
                    DAT_LAST_RUN = x.DAT_LAST_RUN,
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost, ActionName("List")]
        [FormValueRequired("exportexcel-all")]
        public virtual ActionResult ExportExcelAll(DqiSummaryModel model)
        {

            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var items = _dqQueService.GetAllDqiSummary(model.BRANCH_CODE);

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(items);
                return File(bytes, MimeTypes.TextXlsx, "dqiSummary.xlsx");
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

            var docs = new List<DqiSummary>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                docs.AddRange(_dqQueService.GetDqiSummarybyIds(ids));
            }

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(docs);
                return File(bytes, MimeTypes.TextXlsx, "dqiSummary.xlsx");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }
    }
}
