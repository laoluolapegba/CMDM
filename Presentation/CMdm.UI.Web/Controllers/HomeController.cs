using CMdm.Data;
using CMdm.Entities.Domain.Dqi;
using CMdm.Services.DqQue;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System.Web.Mvc;

namespace CMdm.UI.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        #region Fields
        private AppDbContext _db;
        private IDqQueService _dqQueService;
        #endregion
        #region Ctor
        public HomeController()
        {
            _db = new AppDbContext();
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
            int openbrnExceptions = _dqQueService.GetAllBrnQueIssues("", null, identity.BranchId, IssueStatus.Open, null,
            0, int.MaxValue, "").Count;
            ViewBag.openbrnExceptions = openbrnExceptions;
            //int totalExceptions = _dqQueService.GetAllBrnQueIssues("", null, null, IssueStatus.Open, null,
           // 0, int.MaxValue, "").Count;
           // ViewBag.brnPct = openbrnExceptions/totalExceptions * 100;
            
            return View();
        }
        public ActionResult BankDash()
        {
            return View();
        }
    }
}