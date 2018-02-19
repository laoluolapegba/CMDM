using CMdm.Data;
using CMdm.Entities.Domain.Dqi;
using CMdm.Services.DqQue;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity;
using Oracle.DataAccess.Client;
using System.Data;
using System.Collections.Generic;
using CMdm.Entities.Domain.Kpi;
using System.Configuration;
using CMdm.Data.DAC;
using System;

namespace CMdm.UI.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        #region Fields
        private AppDbContext dashdata;
        private IDqQueService _dqQueService;
        private KPIDAC _kpidac;
        #endregion
        #region Ctor
        public HomeController()
        {
            dashdata = new AppDbContext();
            //_dqQueService = new DqQueService();
            _kpidac = new KPIDAC();
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
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;

             
            BrnKpi kpirow = _kpidac.GetBrnKPI(DateTime.Now, identity.BranchId);
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
                ViewBag.openbrnExceptions = kpirow.BRN_OPEN_EXCEPTIONS;
                ViewBag.brnPct = kpirow.BRN_PCT_CONTRIB;
                ViewBag.brnDQI = kpirow.BRN_DQI;
                ViewBag.brnCustomers = kpirow.BRN_CUST_COUNT.ToString("##,##");
                ViewBag.recurringExption = kpirow.BRN_RECURRING_ERRORS;
                ViewBag.bankCustomers = kpirow.BANK_CUST_COUNT.ToString("##,##");
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
    }
}