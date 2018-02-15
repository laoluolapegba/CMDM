using CMdm.Data;
using CMdm.Entities.Domain.Dqi;
using CMdm.Services.DqQue;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity;
namespace CMdm.UI.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        #region Fields
        private AppDbContext dashdata;
        private IDqQueService _dqQueService;
        #endregion
        #region Ctor
        public HomeController()
        {
            dashdata = new AppDbContext();
            _dqQueService = new DqQueService();
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

            int statusCode = (int)IssueStatus.Open;
            int openbrnExceptions = dashdata.MdmDqRunExceptions.Where(a=>a.BRANCH_CODE == identity.BranchId && a.ISSUE_STATUS == statusCode).Count();
                //_dqQueService.GetAllBrnQueIssues("", null, identity.BranchId, IssueStatus.Open, null,
            //0, int.MaxValue, "").Count;
            ViewBag.openbrnExceptions = openbrnExceptions;
            int totalExceptions = dashdata.MdmDqRunExceptions.Where(a => a.ISSUE_STATUS == statusCode).Count();
            //_dqQueService.GetAllBrnQueIssues("", null, null, IssueStatus.Open, null,
            // 0, int.MaxValue, "").Count;
            decimal brnPct = openbrnExceptions/totalExceptions * 100;
            ViewBag.brnPct = brnPct;

            decimal brnDQI = dashdata.BranchDqiSummaries.Where(a => a.BRANCH_CODE == identity.BranchId).Select(a => a.DQI).SingleOrDefault();
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
            return View();
        }
        public ActionResult BankDash()
        {
            return View();
        }
    }
}