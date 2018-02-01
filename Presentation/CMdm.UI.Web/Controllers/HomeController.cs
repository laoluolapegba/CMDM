using System.Web.Mvc;

namespace CMdm.UI.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

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
            return View();
        }
    }
}