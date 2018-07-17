using CMdm.Services.Security;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMdm.UI.Web.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //Create permission string based on the requested controller name and action name in the format 'controllername-action'
            string requiredPermission = String.Format("{0}-{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);
            string requiredController = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string requiredAction = filterContext.ActionDescriptor.ActionName;
            var userId = filterContext.HttpContext.User.Identity.Name; // GetUserId();
            var identity = ((CustomPrincipal)HttpContext.Current.User).CustomIdentity;
            PermissionsService _permissionService = new PermissionsService(userId, identity.UserRoleId);
            //Create an instance of our custom user authorization object passing requesting user's 'Windows Username' into constructor
            //CustomMembershipUser requestingUser = new CustomMembershipUser(filterContext.RequestContext.HttpContext.User.Identity.Name);
            //Check if the requesting user has the permission to run the controller's action
            if (!_permissionService.HasPermission(requiredController, requiredAction))
            {
                //User doesn't have the required permission and is not a SysAdmin, return our custom “401 Unauthorized” access error
                //Since we are setting filterContext.Result to contain an ActionResult page, the controller's action will not be run.
                //The custom “401 Unauthorized” access error will be returned to the browser in response to the initial request.
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "UnAuthorized" }, { "controller", "Error" } });
            }
            //If the user has the permission to run the controller's action, then filterContext.Result will be uninitialized and
            //executing the controller's action is dependant on whether filterContext.Result is uninitialized.
        }
        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new System.Web.Mvc.HttpStatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                // throw new System.Web.HttpException((int)System.Net.HttpStatusCode.Forbidden, "Forbidden");
                var authenticatedunathRouteValues =
                    new RouteValueDictionary(new { controller = "Error", action = "UnAuthorized" });
                filterContext.Result = new RedirectToRouteResult(authenticatedunathRouteValues);
                filterContext.Controller.TempData["message"] = "You are not authorized to view this page.";
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}