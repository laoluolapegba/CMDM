using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMdm.Framework.Kendoui;
using CMdm.Services.DqParam;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Framework.Mvc;
using Kendo.Mvc.Extensions;
using CMdm.Data;
using CMdm.UI.Web.Models.Audit;
using CMdm.Services.Messaging;

namespace CMdm.UI.Web.Controllers
{
    public class AuditTrailController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IMessagingService _messagingService;

        public AuditTrailController()
        {
            _messagingService = new MessagingService();
        }


        public ActionResult Index()
        {
            return RedirectToAction("List");
            //return View(db.MDM_WEIGHTS.ToList());
        }
        public ActionResult List()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            var model = new AuditTrailListModel();
            //model.CATALOG_ID = CatalogId;

            //model.Catalogs = new SelectList(db.MdmCatalogs, "CATALOG_ID", "CATALOG_NAME").ToList();
            //model.EntityList = new SelectList(db.EntityMast, "ENTITY_ID", "ENTITY_NAME").ToList();

            //model.EntityList.Add(new SelectListItem
            //{
            //    Value = "0",
            //    Text = "All"
            //});
            //var weights = from Weights in db.MDM_WEIGHTS
            //              select new WeightsViewModel
            //              {
            //                  WEIGHT_ID = Weights.WEIGHT_ID,
            //                  WEIGHT_DESC = Weights.WEIGHT_DESC
            //              };

            //model.Weights = weights.ToList();
            _messagingService.SaveUserActivity(identity.ProfileId, "Viewed Audit Trail Report", DateTime.Now);
            return View(model);
        }
        [HttpPost]
        public virtual ActionResult List(DataSourceRequest command, AuditTrailListModel model, string sort, string sortDir, string SearchName = "")
        {
            SearchName = Request.Form.Get(0).ToString();
            SearchName = (String.IsNullOrEmpty(SearchName)) ? SearchName : SearchName.ToUpper();
            var data = (from c in db.CDMA_INDIVIDUAL_BIO_DATA
                        join au in db.CDMA_INDIVIDUAL_PROFILE_LOG on c.CUSTOMER_NO equals au.CUSTOMER_NO
                        join u in db.CM_USER_PROFILE on au.LOGGED_BY equals u.PROFILE_ID
                        where au.COMMENTS.ToUpper().Contains(SearchName)
                        select new { Cust = c, Log = au, Actor = u }
                        );


            var gridModel = new DataSourceResult
            {
                Data = data.Select(c => new AuditTrailListModel
                {
                    LogId = c.Log.LOG_ID,
                    CustNo = c.Cust.CUSTOMER_NO,
                    AffectedCat = c.Log.AFFECTED_CATEGORY,
                    Lastname = c.Cust.SURNAME,
                    Firstname = c.Cust.FIRST_NAME,
                    Loggedby = c.Actor.FIRSTNAME + " " + c.Actor.LASTNAME,
                    Comments = c.Log.COMMENTS.Replace("<td>","").Replace("</td>","").Replace("<tr>", "").Replace("</tr>", "").Replace("<strong>", "").Replace("</strong>", ""),
                    LoggedDate = c.Log.LOGGED_DATE

                    //Weights = new WeightsViewModel()
                    //{
                    //    WEIGHT_ID = c.MDM_WEIGHTS.WEIGHT_ID,
                    //    WEIGHT_DESC = c.MDM_WEIGHTS.WEIGHT_DESC
                    //},
                    //RegexCbo = new RegexsViewModel()
                    //{
                    //    REGEX_ID = c.MdmRegex.REGEX_ID,
                    //    REGEX_NAME = c.MdmRegex.REGEX_NAME
                    //},


                }),
                Total = data.Count()
            };


            return Json(gridModel);
        }

    }
}