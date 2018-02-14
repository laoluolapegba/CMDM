using CMdm.Framework.Mvc;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CMdm.UI.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ModelBinders.Binders.Add(typeof(DateTime), new ModelBinder());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                var identity = new CustomIdentity(HttpContext.Current.User.Identity);
                var principal = new CustomPrincipal(identity);
                HttpContext.Current.User = principal;

            }

        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {



            var newCulture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            newCulture.NumberFormat.CurrencySymbol = "₦";
            newCulture.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
            //CultureInfo info = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            //info.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            //System.Threading.Thread.CurrentThread.CurrentCulture = info;

            System.Threading.Thread.CurrentThread.CurrentCulture = newCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = newCulture;
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated) //System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //using (var context = new AnalysisEntities())
                //{
                //    var user = (from u in context.CM_USER_PROFILE
                //                where String.Compare(u.USER_ID, User.Identity.Name, StringComparison.OrdinalIgnoreCase) == 0
                //                //&& !u.Deleted
                //                select u).FirstOrDefault();
                //    var membershipUser = new CustomMembershipUser(user);
                //    Session["BranchId"] = membershipUser.BranchId;
                //}
            }
        }
        protected void Application_Error()
        {
            var lastException = Server.GetLastError();
            var customException = new Exception("custom message", lastException);
            Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(customException));
        }
    }
}
