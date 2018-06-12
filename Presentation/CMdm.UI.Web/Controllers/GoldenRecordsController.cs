using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMdm.Data;
using CMdm.Entities.Domain.GoldenRecord;
using CMdm.UI.Web.Models.GoldenRecord;
using CMdm.Framework.Kendoui;
using CMdm.Services.GoldenRecord;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System.Web.Routing;
using System.Web.UI;

namespace CMdm.UI.Web.Controllers
{
    public class GoldenRecordsController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IGoldenRecordService _dqQueService;
        #region Constructors
        public GoldenRecordsController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new GoldenRecordService();
        }
        #endregion
        // GET: GoldenRecords
        public ActionResult Index()
        {
            return RedirectToAction("List");
            //return View(db.CdmaGoldenRecords.ToList());
        }

        public ActionResult List()
        {

            var model = new GoldenRecordModel();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            var curBranchList = db.CM_BRANCH.Where(a => a.BRANCH_ID == identity.BranchId);
            model.Branches = new SelectList(curBranchList, "BRANCH_ID", "BRANCH_NAME").ToList();


            model.Branches.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });
            return View(model);
        }
        [HttpPost]
        public virtual ActionResult List(DataSourceRequest command, GoldenRecordModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllQueItems(model.SearchName, model.CUSTOMER_NO, model.EMAIL, model.ACCOUNT_NO, model.BVN, model.GOLDEN_RECORD, model.BRANCH_CODE, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new GoldenRecordModel
                {
                    GOLDEN_RECORD = x.GOLDEN_RECORD,
                    FULL_NAME = x.FULL_NAME,
                    DATE_OF_BIRTH = x.DATE_OF_BIRTH,
                    SEX = x.SEX,
                    RESIDENTIAL_ADDRESS = x.RESIDENTIAL_ADDRESS,
                    
                    BVN = x.BVN,
                    BRANCH_CODE = x.BRANCH_CODE,
                    CUSTOMER_NO = x.CUSTOMER_NO,
                    CUSTOMER_TYPE = x.CUSTOMER_TYPE,
                    Id = x.RECORD_ID

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
        public ActionResult AuthList(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;

            var model = new GoldenRecordModel();
            model.GOLDEN_RECORD = id;

            //foreach (var at in _dqService.GetAllActivityTypes())
            //{
            //    model.ActivityLogType.Add(new SelectListItem
            //    {
            //        Value = at.Id.ToString(),
            //        Text = at.Name
            //    });
            //}
            var curBranchList = db.CM_BRANCH.Where(a => a.BRANCH_ID == identity.BranchId);
            model.Branches = new SelectList(curBranchList, "BRANCH_ID", "BRANCH_NAME").ToList();

            
            model.Branches.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });
            
            //model.Branches.Add(new SelectListItem
            //{
            //    Value = "0",
            //    Text = "All"
            //});
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult AuthList(DataSourceRequest command, GoldenRecordModel model, string sort, string sortDir)
        {
            //DateTime? startDateValue = (model.CreatedOnFrom == null) ? null
            //    : (DateTime?)model.CreatedOnFrom.Value;

            //DateTime? endDateValue = (model.CreatedOnTo == null) ? null
            //                : (DateTime?)model.CreatedOnTo.Value.AddDays(1);
            
            var identity = ((CustomPrincipal)User).CustomIdentity;
            var routeValues = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values;
            //RouteValueDictionary routeValues;

            int goldenRecord = 0;
            if (routeValues.ContainsKey("id"))
                goldenRecord = int.Parse((string)routeValues["id"]);

            var items = _dqQueService.GetAllQueItems(model.SearchName, model.CUSTOMER_NO, model.EMAIL, model.ACCOUNT_NO, model.BVN, goldenRecord, model.BRANCH_CODE, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new GoldenRecordModel
                {
                    GOLDEN_RECORD = x.GOLDEN_RECORD,
                    FULL_NAME = x.FULL_NAME,
                    DATE_OF_BIRTH = x.DATE_OF_BIRTH,
                    SEX = x.SEX,
                    RESIDENTIAL_ADDRESS = x.RESIDENTIAL_ADDRESS,
                    BRANCH_CODE = x.BRANCH_CODE,
                    BVN = x.BVN,
                    CUSTOMER_NO = x.CUSTOMER_NO,
                    CUSTOMER_TYPE = x.CUSTOMER_TYPE,
                    Id = x.RECORD_ID
                }),
                Total = items.TotalCount
            };



            return Json(gridModel);
        }

        [HttpPost]
        public virtual ActionResult ApproveSelected(string selectedIds)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            var goldenrecords = new List<CdmaGoldenRecord>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray(); 
                goldenrecords.AddRange(_dqQueService.GetQueItembyIds(ids));
            }

            try
            {
                foreach (var item in goldenrecords)
                {
                    item.RECORD_STATUS = "A";
                    _dqQueService.UpdateQueItem(item);
                }

                return RedirectToAction("List");

            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public virtual ActionResult DisapproveSelected(string selectedIds)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            var goldenrecords = new List<CdmaGoldenRecord>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                goldenrecords.AddRange(_dqQueService.GetQueItembyIds(ids));
            }

            try
            {
                foreach (var item in goldenrecords)
                {
                    item.RECORD_STATUS = "U";
                    _dqQueService.UpdateQueItem(item);
                }
                return RedirectToAction("List");

            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
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
