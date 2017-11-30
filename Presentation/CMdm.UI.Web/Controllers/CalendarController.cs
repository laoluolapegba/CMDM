using System.Web.Mvc;

namespace CMdm.UI.Web.Controllers
{
    [AllowAnonymous]
    public class CalendarController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}