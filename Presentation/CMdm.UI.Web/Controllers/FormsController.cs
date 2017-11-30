using System.Web.Mvc;

namespace CMdm.Controllers
{
    public class FormsController : Controller
    {
        public ActionResult General()
        {
            return View();
        }
        public ActionResult Advanced()
        {
            return View();
        }
        public ActionResult Editors()
        {
            return View();
        }
        public ActionResult NoNavView()
        {
            return View();
        }
    }
}