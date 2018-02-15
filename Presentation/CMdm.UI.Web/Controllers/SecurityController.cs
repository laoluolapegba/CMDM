using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security
        
        public virtual ActionResult AccessDenied(string pageUrl)
        {
            var userIdentity = User;
            if (!userIdentity.Identity.IsAuthenticated)
            {
                ViewBag.Info = string.Format("Access denied to anonymous request on {0}", pageUrl);
                //_logger.Information(string.Format("Access denied to anonymous request on {0}", pageUrl));
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            //_logger.Information(string.Format("Access denied to user #{0} '{1}' on {2}", currentCustomer.Email, currentCustomer.Email, pageUrl));


            
        }

    }
}