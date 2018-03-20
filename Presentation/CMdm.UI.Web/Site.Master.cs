using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Data.Rbac;
using CMdm.Data;
using System.Web.Security;
using CMdm.Entities.ViewModels;

namespace CMdm.UI.Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        public AppDbContext db = new AppDbContext();
        public CustomMembershipProvider mp = new CustomMembershipProvider();
        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
               // DQIProfileBiz rep = new DQIProfileBiz();
               //string dqi = rep.DQIValue();
               // int custCount = rep.CustCount("UBA_CDMA_MAIN");

               // lblDataQuality.Text = dqi.ToString();
               // lblNoOfCustomers.Text = custCount.ToString();
                string path = HttpContext.Current.Request.Url.AbsolutePath;

                //int roleID = 0;
                try
                {
                    //callMenu(81);
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        var identity = ((CustomPrincipal)HttpContext.Current.User).CustomIdentity;
                        lblName.Text = string.Format("Welcome {0},", identity.FirstName);
                        //var identity = ((CustomPrincipal)HttpContext.Current.User).CustomIdentity;
                        //this.lblRole.Text = identity.UserRoleName;// getCustomIdentities(usrName, "RoleName");

                        //this.lblProfileName.Text = identity.DisplayName; // getCustomIdentities(usrName, "DisplayName");

                        //Generate menu.

                    }

                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    //throw ex;
                    // Response.Redirect("../Default.aspx?msg=The Page is unavailable.");
                }

            }
        }

        private void callMenu(int roleID)
        {
            //CM_ROLE_PERM_XREF rpx = new CM_ROLE_PERM_XREF();
            //List<MenuViewModel> menuList = null;
            var menu = from n in db.CM_ROLE_PERM_XREF
                       join p in db.CM_PERMISSIONS on n.PERMISSION_ID equals p.PERMISSION_ID
                       join r in db.CM_USER_ROLES on n.ROLE_ID equals r.ROLE_ID
                       where n.ROLE_ID == roleID
                       where p.ISACTIVE == true
                       //where n.CM_PERMISSIONS.PARENT_PERMISSION != 0
                       //orderby n.CM_PERMISSIONS.PERMISSIONDESCRIPTION
                       select new
                       {
                           PermissionId = n.PERMISSION_ID,
                           MenuDesc = p.PERMISSIONDESCRIPTION,
                           URL = p.FORM_URL,
                           ControllerName = p.CONTROLLER_NAME,
                           ParentPermission = p.PARENT_PERMISSION,
                           PermissionDesc = p.PERMISSIONDESCRIPTION,
                           ActionName = p.ACTION_NAME,
                           RoleID = n.ROLE_ID,
                           RoleName = r.ROLE_NAME,
                           IconClass = p.ICON_CLASS,
                           IsopenClass = p.ISOPEN_CLASS,
                           ToggleIconClass = p.TOGGLE_ICON,
                           Url = p.FORM_URL
                       };
            //var menu = from n in db.CM_ROLE_PERM_XREF
            //           join p in db.CM_PERMISSIONS on n.PERMISSION_ID equals p.PERMISSION_ID
            //           where n.ROLE_ID == roleID
            //           //where n.CM_PERMISSIONS.PARENT_PERMISSION != 0
            //           orderby p.PERMISSIONDESCRIPTION
            //           select new
            //           {
            //               PermissionId = n.CM_PERMISSIONS.PERMISSION_ID,
            //               MenuDesc = n.CM_PERMISSIONS.PERMISSIONDESCRIPTION,
            //               URL = n.CM_PERMISSIONS.FORM_URL,
            //               ControllerName = n.CM_PERMISSIONS.CONTROLLER_NAME,
            //               ParentPermission = n.CM_PERMISSIONS.PARENT_PERMISSION,
            //               PermissionDesc = n.CM_PERMISSIONS.PERMISSIONDESCRIPTION,
            //               ActionName = n.CM_PERMISSIONS.ACTION_NAME,
            //               RoleID = n.ROLE_ID,
            //               RoleName = n.CM_USER_ROLES.ROLE_NAME,
            //               IconClass = n.CM_PERMISSIONS.ICON_CLASS,
            //               IsopenClass = n.CM_PERMISSIONS.ISOPEN_CLASS,
            //               ToggleIconClass = n.CM_PERMISSIONS.TOGGLE_ICON,
            //               Url = n.CM_PERMISSIONS.FORM_URL
            //           };

                           var menuData = menu.Select(o => new MenuViewModel
            {
                PermissionId = o.PermissionId,
                RoleId = o.RoleID,
                PermissionDesc = o.PermissionDesc,
                ActionName = o.ActionName,
                ControllerName = o.ControllerName,
                ParentPermission = o.ParentPermission,
                IconClass = o.IconClass,
                IsopenClass = o.IsopenClass,
                ToggleIconClass = o.ToggleIconClass,
                Url = o.URL
            }).ToList();

            //this.trMenu.DataSource = menuData.ToList();
            //this.trMenu.DataBind();


        }
    }
}