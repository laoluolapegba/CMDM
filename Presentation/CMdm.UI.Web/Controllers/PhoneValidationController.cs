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
using CMdm.Services.Messaging;

namespace CMdm.UI.Web.Controllers
{
    public class PhoneValidationController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IPhoneValidationService _dqQueService;
        private IPValExportManager _exportManager;
        private IPermissionsService _permissionservice;
        private CustomIdentity identity;
        private IMessagingService _messagingService;

        public PhoneValidationController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new PhoneValidationService();
            _exportManager = new PValExportManager();
            _messagingService = new MessagingService();

            _permissionservice = new PermissionsService();
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new PhoneValidationModel();
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

            _messagingService.SaveUserActivity(identity.ProfileId, "Viewed Phone Number Validation Report", DateTime.Now);
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult PhoneValidationList(DataSourceRequest command, PhoneValidationModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllPhoneValidations(model.CUSTOMER_NO, model.ACCOUNTNO, model.CUST_FIRST_NAME, model.CUST_MIDDLE_NAME,
                model.CUST_LAST_NAME, model.BRANCH_CODE, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new PhoneValidationModel
                {
                    Id = x.ID,
                    CUSTOMER_NO = x.CUSTOMER_NO,
                    BRANCH_CODE = x.BRANCH_CODE,
                    CUST_LAST_NAME = x.CUST_LAST_NAME,
                    CUST_MIDDLE_NAME = x.CUST_MIDDLE_NAME,
                    CUST_FIRST_NAME = x.CUST_FIRST_NAME,
                    LAST_RUN_DATE = x.LAST_RUN_DATE,
                    REASON = x.REASON,
                    PHONE_NO = x.PHONE_NO,
                    TYPE = x.TYPE,
                    BRANCH_NAME = x.BRANCH_NAME,
                    SCHEME_CODE = x.SCHEME_CODE,
                    ACCOUNTNO = x.ACCOUNTNO,
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost, ActionName("List")]
        [FormValueRequired("exportexcel-all")]
        public virtual ActionResult ExportExcelAll(PhoneValidationModel model)
        {

            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var items = _dqQueService.GetAllPhoneValidations(model.CUSTOMER_NO, model.ACCOUNTNO, model.CUST_FIRST_NAME, model.CUST_MIDDLE_NAME, model.CUST_LAST_NAME, model.BRANCH_CODE);

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(items);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Phone Number Validation Report", DateTime.Now);
                return File(bytes, MimeTypes.TextXlsx, "phoneValidation.xlsx");
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

            var docs = new List<PhoneValidation>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                docs.AddRange(_dqQueService.GetPhoneValidationbyIds(ids));
            }

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(docs);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Phone Number Validation Report", DateTime.Now);
                return File(bytes, MimeTypes.TextXlsx, "phoneValidation.xlsx");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }
    }

}
public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}