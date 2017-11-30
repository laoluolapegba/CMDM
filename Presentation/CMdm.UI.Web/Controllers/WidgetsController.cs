using System.Web.Mvc;

namespace CMdm.Controllers
{
    [AllowAnonymous]
    public class WidgetsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}