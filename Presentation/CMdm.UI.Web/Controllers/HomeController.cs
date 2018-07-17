using CMdm.Data;
using CMdm.Entities.Domain.Dqi;
using CMdm.Services.DqQue;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using CMdm.Entities.Domain.Kpi;
using System.Configuration;
using CMdm.Data.DAC;
using System;
using CMdm.Services.Security;
using CMdm.Data.Rbac;
using CMdm.Entities.Domain.User;

namespace CMdm.UI.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        #region Fields
        private AppDbContext dashdata;
        private IDqQueService _dqQueService;
        private KPIDAC _kpidac;
        private IPermissionsService _permissionservice;
        private CustomIdentity identity;
        #endregion
        #region Ctor
        public HomeController()
        {
            dashdata = new AppDbContext();
            //_dqQueService = new DqQueService();
            _kpidac = new KPIDAC();
            _permissionservice = new PermissionsService();
        }
        #endregion
        public ActionResult DashboardV1()
        {
            return View();
        }
        public ActionResult DashboardV2()
        {
            return View();
        }
        public ActionResult BranchDqi()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            identity = ((CustomPrincipal)User).CustomIdentity;
            List<BrnKpi> kpirow = _kpidac.GetBrnKPI(DateTime.Now, identity.BranchId);
            ViewBag.brnDQI = kpirow[0].BRN_DQI;

            ViewData["BranchId"] = identity.BranchId;
            ViewData["CatalogId"] = 1;
            return View();
        }

        public JsonResult StatisticsTrend()
        {
            identity = ((CustomPrincipal)User).CustomIdentity;
            var trendingData = _kpidac.GetBrnKPI(DateTime.Now, identity.BranchId);

            return Json(trendingData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Corporate()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            identity = ((CustomPrincipal)User).CustomIdentity;


            List<CorpKpi> kpirow = new List<CorpKpi>();

            _permissionservice = new PermissionsService(identity.Name, identity.UserRoleId);

            string[] curBranchList = dashdata.CM_BRANCH.Select(x => x.BRANCH_ID).ToArray(); //.Where(a => a.BRANCH_ID == identity.BranchId);

            if (_permissionservice.IsLevel(AuthorizationLevel.Enterprise))
            {
                curBranchList = curBranchList;
            }
            else if (_permissionservice.IsLevel(AuthorizationLevel.Regional))
            {
                curBranchList = dashdata.CM_BRANCH.Where(a => a.REGION_ID == identity.RegionId).Select(x => x.BRANCH_ID).ToArray();
            }
            else if (_permissionservice.IsLevel(AuthorizationLevel.Zonal))
            {
                curBranchList = dashdata.CM_BRANCH.Where(a => a.ZONECODE == identity.ZoneId).Select(x => x.BRANCH_ID).ToArray();
            }
            else if (_permissionservice.IsLevel(AuthorizationLevel.Branch))
            {
                curBranchList = dashdata.CM_BRANCH.Where(a => a.BRANCH_ID == identity.BranchId).Select(x => x.BRANCH_ID).ToArray(); ;
            }
            else
            {
                curBranchList = dashdata.CM_BRANCH.Where(a => a.BRANCH_ID == "-1").Select(x => x.BRANCH_ID).ToArray();
            }

            kpirow = _kpidac.GetCorpKPI(DateTime.Now, curBranchList);

            if (kpirow != null)
            {
                ViewBag.allkpi = kpirow;
            }

            //Change this to corporate table
            int unAuthorized = dashdata.CDMA_INDIVIDUAL_BIO_DATA.Where(a => a.BRANCH_CODE == identity.BranchId && a.AUTHORISED == "U").Count();
            ViewBag.unAuthorized = unAuthorized;
            return View();
        }

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            identity = ((CustomPrincipal)User).CustomIdentity;


            List<BrnKpi> kpirow = new List<BrnKpi>();

            _permissionservice = new PermissionsService(identity.Name, identity.UserRoleId);

            string[] curBranchList = dashdata.CM_BRANCH.Select(x => x.BRANCH_ID).ToArray(); //.Where(a => a.BRANCH_ID == identity.BranchId);

            if (_permissionservice.IsLevel(AuthorizationLevel.Enterprise))
            {
                curBranchList = curBranchList;
            }
            else if (_permissionservice.IsLevel(AuthorizationLevel.Regional))
            {
                curBranchList = dashdata.CM_BRANCH.Where(a => a.REGION_ID == identity.RegionId).Select(x => x.BRANCH_ID).ToArray();
            }
            else if (_permissionservice.IsLevel(AuthorizationLevel.Zonal))
            {
                curBranchList = dashdata.CM_BRANCH.Where(a => a.ZONECODE == identity.ZoneId).Select(x => x.BRANCH_ID).ToArray();
            }
            else if (_permissionservice.IsLevel(AuthorizationLevel.Branch))
            {
                curBranchList = dashdata.CM_BRANCH.Where(a => a.BRANCH_ID == identity.BranchId).Select(x => x.BRANCH_ID).ToArray(); ;
            }
            else
            {
                curBranchList = dashdata.CM_BRANCH.Where(a => a.BRANCH_ID == "-1").Select(x => x.BRANCH_ID).ToArray();
            }

            

            kpirow = _kpidac.GetMultipleBrnKPI(DateTime.Now, curBranchList);
            //using (AppDbContext ctx = new AppDbContext())
            //{
            //    /*
            //    OracleParameter brnParameter = new OracleParameter();
            //    brnParameter.ParameterName = "var_p_branch_code";
            //    brnParameter.Direction = ParameterDirection.Input;
            //    brnParameter.OracleDbType = OracleDbType.Varchar2;

            //    OracleParameter cursorParameter = new OracleParameter();
            //    cursorParameter.ParameterName = "var_p_result";
            //    cursorParameter.Direction = ParameterDirection.Output;
            //    cursorParameter.OracleDbType = OracleDbType.RefCursor; */
            //    object[] prm = new object[] {
            //    new OracleParameter(":var_p_branch_code", OracleDbType.Varchar2, ParameterDirection.Input).Value = identity.BranchId,
            //    new OracleParameter(":var_p_result", OracleDbType.RefCursor, ParameterDirection.Output)
            //    };


            //    List<BrnKpi> kpi = ctx.Database.SqlQuery<BrnKpi>(" exec cmdm_kpi.prc_get_brn_kpi :var_p_branch_code :var_p_result", prm ).ToList();
            //    kpirow = kpi.FirstOrDefault();
            //       //  CommandType.StoredProcedure, cursorParameter);
            //}
            if(kpirow != null)
            {
                ViewBag.allkpi = kpirow;
                //ViewBag.openbrnExceptions = kpirow[0].BRN_OPEN_EXCEPTIONS.ToString("##,##");
                //ViewBag.openbrnExceptions1 = kpirow[1].BRN_OPEN_EXCEPTIONS.ToString("##,##");
                //ViewBag.openbrnExceptions2 = kpirow[2].BRN_OPEN_EXCEPTIONS.ToString("##,##");
                //ViewBag.openbrnExceptions3 = kpirow[3].BRN_OPEN_EXCEPTIONS.ToString("##,##");

                //ViewBag.brnPct = kpirow[kpirow.Count].BRN_PCT_CONTRIB;
                //ViewBag.brnDQI = kpirow[kpirow.Count].BRN_DQI;
                //ViewBag.brnCustomers = kpirow[kpirow.Count].BRN_CUST_COUNT.ToString("##,##");
                //ViewBag.recurringExption = kpirow[kpirow.Count].BRN_RECURRING_ERRORS;
                //ViewBag.bankCustomers = kpirow[kpirow.Count].BANK_CUST_COUNT.ToString("##,##");
            }
           
            /*
            int statusCode = (int)IssueStatus.Open;
            //int openbrnExceptions = dashdata.MdmDqRunExceptions.Where(a=>a.BRANCH_CODE == identity.BranchId && a.ISSUE_STATUS == statusCode).Count();
            int openbrnExceptions = 1;
                //_dqQueService.GetAllBrnQueIssues("", null, identity.BranchId, IssueStatus.Open, null,
            //0, int.MaxValue, "").Count;
            ViewBag.openbrnExceptions = openbrnExceptions;
            int totalExceptions = dashdata.MdmDqRunExceptions.Where(a => a.ISSUE_STATUS == statusCode).Count();
            //_dqQueService.GetAllBrnQueIssues("", null, null, IssueStatus.Open, null,
            // 0, int.MaxValue, "").Count;
            // decimal brnPct = openbrnExceptions/totalExceptions * 100;
            decimal brnPct = 1;
            ViewBag.brnPct = brnPct;
            //

            decimal brnDQI = dashdata.BranchDqiSummaries
                .Where(a => a.BRANCH_CODE == identity.BranchId)
                .Select(a => a.DQI)
                .Average();// FirstOrDefault();
            //
            //.SingleOrDefault();
            ViewBag.brnDQI = brnDQI;
            string brnString = identity.BranchId.ToString();
            int brnCustomers = dashdata.CDMA_INDIVIDUAL_BIO_DATA.Where(a => a.BRANCH_CODE == brnString).Count();
            int bankCustomers = dashdata.CDMA_INDIVIDUAL_BIO_DATA.Count();
            ViewBag.brnCustomers = brnCustomers.ToString("##,##"); 
            ViewBag.bankCustomers = bankCustomers.ToString("##,##");

            int unAuthorized = dashdata.CDMA_INDIVIDUAL_BIO_DATA.Where(a => a.BRANCH_CODE == brnString && a.AUTHORISED == "U").Count();
            ViewBag.unAuthorized = unAuthorized;

            int recurringExption = 0;
            ViewBag.recurringExption = recurringExption;
            */
            //int bankCustomers = dashdata.CDMA_INDIVIDUAL_BIO_DATA.Count();
            //ViewBag.bankCustomers = bankCustomers.ToString("##,##");
            int unAuthorized = dashdata.CDMA_INDIVIDUAL_BIO_DATA.Where(a => a.BRANCH_CODE == identity.BranchId && a.AUTHORISED == "U").Count();
            ViewBag.unAuthorized = unAuthorized;
            return View();
        }
        public ActionResult BankDash()
        {
           
            return View();
        }
        public ActionResult GetBranches()
        {
            var identity = ((CustomPrincipal)User).CustomIdentity;
            
            var cashdb = new AppDbContext();
            //if (User.IsInRole("Admin"))
            //{
                var branches = (from o in cashdb.CM_BRANCH
                                    //join r in cashdb.Region on o.REGION_ID equals r.REGION_ID
                                where o.BRANCH_ID == identity.BranchId
                                select new
                                {
                                    BranchId = o.BRANCH_ID,
                                    BranchName = o.BRANCH_NAME,
                                 //   BranchSchedulerColor = "#F9722E"
                                });
                return Json(branches, JsonRequestBehavior.AllowGet);

            //}
           
            //else
            //{
            //    var branches = (from o in cashdb.CM_BRANCH
            //                    select new
            //                    {
            //                        BranchId = o.BRANCH_ID,
            //                        BranchName = o.BRANCH_NAME,
            //                        BranchSchedulerColor = "#F9722E"
            //                    });

            //    return Json(branches, JsonRequestBehavior.AllowGet);
            //}

            //return View();
        }
        public ActionResult GetBrnDQI(string BranchCode)
        {
            string brnCode = string.Empty;
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (BranchCode == null || BranchCode == string.Empty)
            {
                brnCode = identity.BranchId.ToString();
            }
            else
            {
                brnCode = BranchCode;
            }
            
            var brnDQI =
            (
             from p in dashdata.BrnKpis
             where p.BRANCH_CODE == brnCode
             select (decimal)p.BRN_DQI
            ).FirstOrDefault();

                       
            var v_util = new[]
            {
                new { limit = 100,
                      dqi = brnDQI }
            };
           
            return Json(v_util, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBranchDqiSummary(string BranchCode, string CatalogId)
        {
            int catId = CatalogId == string.Empty ? 0 : Convert.ToInt32(CatalogId);

            var dqidata = (from o in dashdata.BranchDqiSummaries 
                           join e in dashdata.EntityMast on o.TABLE_NAME equals e.ENTITY_TAB_NAME
                           where e.CATALOG_ID == catId
                           where o.BRANCH_CODE == BranchCode
                            

                           select new 
                        {
                               BRANCH_CODE = o.BRANCH_CODE,
                            TABLENAME = o.TABLE_NAME,
                            ATTRIBUTE = o.ATTRIBUTE,
                            DQI = o.DQI
                        }).AsEnumerable();
            //using (FileStream fs = System.IO.File.Open(@"c:\temp\utildata3" + DateTime.Now.Ticks + ".json", FileMode.Append))
            //using (StreamWriter sw = new StreamWriter(fs))
            //using (JsonWriter jw = new JsonTextWriter(sw))
            //{
            //    jw.Formatting = Formatting.Indented;

            //    JsonSerializer serializer = new JsonSerializer();
            //    serializer.Serialize(jw, cash);
            //}

            return Json(dqidata, JsonRequestBehavior.AllowGet);  //, JsonRequestBehavior.AllowGet  //Content(JsonConvert.SerializeObject(cash))
        }
    }
}