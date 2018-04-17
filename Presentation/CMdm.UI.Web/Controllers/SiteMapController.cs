using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using CMdm.UI.Web.Models.UserAdmin;

namespace CMdm.UI.Web.Controllers
{
    public class SiteMapController : BaseController
    {
        // GET: SiteMap
        public ActionResult Index()
        {
            List<SiteMapModel> lists = new List<SiteMapModel>();

            var controllers = Assembly.GetExecutingAssembly().GetExportedTypes().Where(t => typeof(ControllerBase).IsAssignableFrom(t)).Select(t => t);

            foreach (Type controller in controllers)
            {

                var actions = controller.GetMethods().Where(t => t.Name != "Dispose" && !t.IsSpecialName && t.DeclaringType.IsSubclassOf(typeof(ControllerBase)) && t.IsPublic && !t.IsStatic).ToList();

                foreach (var action in actions)
                {
                    string myAttributes = action.GetCustomAttributes(false).ToString();
                    for (int j = 0; j < myAttributes.Length; j++)
                    {
                        SiteMapModel list = new SiteMapModel();

                        list.ActionName = action.Name;
                        list.Url = myAttributes[j].ToString();

                        lists.Add(list);
                    }
                        //s += string.Format("ActionName: {0}, Attribute: {1}< br >", action.Name, myAttributes[j]);
                }
            }

            ViewBag.Display = lists;
            return View();
        }
    }
}