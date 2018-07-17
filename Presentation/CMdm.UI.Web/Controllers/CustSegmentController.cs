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
    public class CustSegmentController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private ICustSegmentService _dqQueService;
        private ICustExportManager _exportManager;
        private IPermissionsService _permissionservice;
        private CustomIdentity identity;
        private IMessagingService _messagingService;
        #region Constructors
        public CustSegmentController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new CustSegmentService();
            _exportManager = new CustExportManager();
            _messagingService = new MessagingService();

            _permissionservice = new PermissionsService();
        }
        #endregion Constructors

        // GET: CustSegment
        public ActionResult Index()
        {
            return RedirectToAction("List");
            //return View(db.AccountOfficers.ToList());
        }

        public ActionResult List()
        {

            var model = new CustSegmentModel();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            identity = ((CustomPrincipal)User).CustomIdentity;
            _permissionservice = new PermissionsService(identity.Name, identity.UserRoleId);

            model.CustomerTypes.Add(new SelectListItem
            {
                Text = "INDIVIDUAL",
                Value = "INDIVIDUAL"
            });
            model.CustomerTypes.Add(new SelectListItem
            {
                Text = "CORPORATE",
                Value = "CORPORATE"
            });
            model.CustomerTypes.Add(new SelectListItem
            {
                Value = "0",
                Text = "All",
                Selected = true
            });
            model.SectorList.Add(new SelectListItem
            {
                Value = "Sector is null",
                Text = "Sector is null"
            });
            model.SectorList.Add(new SelectListItem
            {
                Value = "Sector is not null",
                Text = "Sector is not null"
            });
            model.SectorList.Add(new SelectListItem
            {
                Value = "0",
                Text = "All",
                Selected = true
            });
            model.SubsectorList.Add(new SelectListItem
            {
                Value = "Subsector is null",
                Text = "Subsector is null"
            });
            model.SubsectorList.Add(new SelectListItem
            {
                Value = "Subsector is not null",
                Text = "Subsector is not null"
            });
            model.SubsectorList.Add(new SelectListItem
            {
                Value = "0",
                Text = "All",
                Selected = true
            });

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

            _messagingService.SaveUserActivity(identity.ProfileId, "Viewed Wrong Sector / Subsector Mapping Report", DateTime.Now);

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult CustSegmentList(DataSourceRequest command, CustSegmentModel model, string sort, string sortDir)
        {

            var items = _dqQueService.GetAllCustSegments(model.ORGKEY, model.CUSTOMER_TYPE, model.ACCOUNT_NO, model.CUST_FIRST_NAME, model.CUST_MIDDLE_NAME, model.CUST_LAST_NAME, model.PRIMARY_SOL_ID, 
                model.SECTOR, model.SUBSECTOR, command.Page - 1, command.PageSize, string.Format("{0} {1}", sort, sortDir));
            //var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
            //    logLevel, command.Page - 1, command.PageSize);
            DateTime _today = DateTime.Now.Date;
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new CustSegmentModel
                {
                    Id = x.ID,
                    ORGKEY = x.ORGKEY,
                    CUST_FIRST_NAME = x.CUST_FIRST_NAME,
                    CUST_MIDDLE_NAME = x.CUST_MIDDLE_NAME,
                    CUST_LAST_NAME = x.CUST_LAST_NAME,
                    GENDER = x.GENDER,
                    CUST_DOB = x.CUST_DOB,
                    CUSTOMERMINOR = x.CUSTOMERMINOR,
                    MAIDENNAMEOFMOTHER = x.MAIDENNAMEOFMOTHER,
                    NICK_NAME = x.NICK_NAME,
                    PLACEOFBIRTH = x.PLACEOFBIRTH,
                    PRIMARY_SOL_ID = x.PRIMARY_SOL_ID,
                    SEGMENTATION_CLASS = x.SEGMENTATION_CLASS,
                    SECTOR = x.SECTOR,
                    SECTORNAME = x.SECTORNAME,
                    SUBSECTOR = x.SUBSECTOR,
                    SUBSECTORNAME = x.SUBSECTORNAME,
                    SUBSEGMENT = x.SUBSEGMENT,
                    CORP_ID = x.CORP_ID,
                    SCHEME_CODE = x.SCHEME_CODE,
                    ACCOUNT_NO = x.ACCOUNT_NO,
                    CUSTOMER_TYPE = x.CUSTOMER_TYPE,
                    REASON = x.REASON
                }),
                Total = items.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost, ActionName("List")]
        [FormValueRequired("exportexcel-all")]
        public virtual ActionResult ExportExcelAll(CustSegmentModel model)
        {

            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var items = _dqQueService.GetAllCustSegments(model.ORGKEY, model.CUSTOMER_TYPE, model.ACCOUNT_NO, model.CUST_FIRST_NAME, 
                model.CUST_MIDDLE_NAME, model.CUST_LAST_NAME, model.PRIMARY_SOL_ID, model.SECTOR, model.SUBSECTOR);

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(items);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Wrong Sector / Subsector Mapping Report", DateTime.Now);
                return File(bytes, MimeTypes.TextXlsx, "wrongSectorSubsector.xlsx");
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

            var docs = new List<CustSegment>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                docs.AddRange(_dqQueService.GetCustSegmentbyIds(ids));
            }

            try
            {
                byte[] bytes = _exportManager.ExportDocumentsToXlsx(docs);
                identity = ((CustomPrincipal)User).CustomIdentity;
                _messagingService.SaveUserActivity(identity.ProfileId, "Downloaded Wrong Sector / Subsector Mapping Report", DateTime.Now);
                return File(bytes, MimeTypes.TextXlsx, "wrongSectorSubsector.xlsx");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

        #region scaffolded
        //// GET: CustSegment
        //public ActionResult Index()
        //{
        //    return View(db.TMP_SEC_SUB3.ToList());
        //}

        //// GET: CustSegment/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TMP_SEC_SUB3 tMP_SEC_SUB3 = db.TMP_SEC_SUB3.Find(id);
        //    if (tMP_SEC_SUB3 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tMP_SEC_SUB3);
        //}

        //// GET: CustSegment/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: CustSegment/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ORGKEY,CUST_FIRST_NAME,CUST_MIDDLE_NAME,CUST_LAST_NAME,GENDER,CUST_DOB,CUSTOMERMINOR,MAIDENNAMEOFMOTHER,NICK_NAME,PLACEOFBIRTH,PRIMARY_SOL_ID,SEGMENTATION_CLASS,SECTOR,SECTORNAME,SUBSECTOR,SUBSECTORNAME,SUBSEGMENT,CORP_ID")] TMP_SEC_SUB3 tMP_SEC_SUB3)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TMP_SEC_SUB3.Add(tMP_SEC_SUB3);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(tMP_SEC_SUB3);
        //}

        //// GET: CustSegment/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TMP_SEC_SUB3 tMP_SEC_SUB3 = db.TMP_SEC_SUB3.Find(id);
        //    if (tMP_SEC_SUB3 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tMP_SEC_SUB3);
        //}

        //// POST: CustSegment/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ORGKEY,CUST_FIRST_NAME,CUST_MIDDLE_NAME,CUST_LAST_NAME,GENDER,CUST_DOB,CUSTOMERMINOR,MAIDENNAMEOFMOTHER,NICK_NAME,PLACEOFBIRTH,PRIMARY_SOL_ID,SEGMENTATION_CLASS,SECTOR,SECTORNAME,SUBSECTOR,SUBSECTORNAME,SUBSEGMENT,CORP_ID")] TMP_SEC_SUB3 tMP_SEC_SUB3)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tMP_SEC_SUB3).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(tMP_SEC_SUB3);
        //}

        //// GET: CustSegment/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TMP_SEC_SUB3 tMP_SEC_SUB3 = db.TMP_SEC_SUB3.Find(id);
        //    if (tMP_SEC_SUB3 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tMP_SEC_SUB3);
        //}

        //// POST: CustSegment/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    TMP_SEC_SUB3 tMP_SEC_SUB3 = db.TMP_SEC_SUB3.Find(id);
        //    db.TMP_SEC_SUB3.Remove(tMP_SEC_SUB3);
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
