using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMdm.Data.Rbac;
using CMdm.Data;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Web.Routing;
using System.Linq;
using CMdm.Business;
using Microsoft.AspNet.Identity;
using CMdm.UI.Web.Helpers.CrossCutting.Security;

namespace CMdm.UI.Web
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        public AppDbContext db = new AppDbContext();
        public CustomMembershipProvider mp = new CustomMembershipProvider();
        //lblNoOfCustomers
        
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

                //lblCurrentUsers.Text = "5"; //Convert.ToString(mp.GetNumberOfUsersOnline());
                //lblNoOfCustomers.Text = "2,500,000";//String.Format("{0:#,###,###}", Convert.ToInt32(mp.getNoOfCustomers())); //"6,897,752";
                //lblDataQuality.Text = "34.7";//mp.getDQIPercentage();
                DQIProfileBiz rep = new DQIProfileBiz();
                string dqi = rep.DQIValue();
                int custCount = rep.CustCount("UBA_CDMA_MAIN");

                lblDataQuality.Text = dqi.ToString();
                lblNoOfCustomers.Text = custCount.ToString();
                string path = HttpContext.Current.Request.Url.AbsolutePath;

                //int roleID = 0;
                try
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        var identity = ((CustomPrincipal)HttpContext.Current.User).CustomIdentity;
                        this.lblRole.Text = identity.UserRoleName;// getCustomIdentities(usrName, "RoleName");

                        this.lblProfileName.Text = identity.DisplayName; // getCustomIdentities(usrName, "DisplayName");

                       // string getRoleID = identity.DisplayName;// getCustomIdentities(usrName, "RoleID");

                        //Generate menu.
                        callMenu(identity.UserRoleId);


                        //if (!checkCurrentURL(path, identity.UserRoleId))
                        //{
                        //    Response.Redirect("~/Default.aspx?msg=The Page is unavailable.");
                        //}
                    }

                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    //throw ex;
                    // Response.Redirect("../Default.aspx?msg=The Page is unavailable.");
                }

            }
            //else 
            //{
            //    Response.Redirect("../CDMA/Login?msg=You have been logged out,Please login again.");
            //}
        }

        private bool checkCurrentURL(string path, int roleID)
        {
            bool status; int rID = roleID;
            int menu = (from n in db.CM_ROLE_PERM_XREF
                        where n.ROLE_ID == rID && n.CM_PERMISSIONS.FORM_URL.Contains(path)
                        select n).Count();
            //{
            //    URL = n.CM_PERMISSIONS.FORM_URL,
            //    Controller = n.CM_PERMISSIONS.CONTROLLER_NAME,
            //    Action = n.CM_PERMISSIONS.ACTION_NAME,
            //};


            if (menu > 0 || path.Contains("/Default"))//&& (m.Controller+"/"+m.Action == path)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }

        public string getCustomIdentities(string usrName, string type)
        {
            string Value = "";

            var r = (from n in db.CM_USER_PROFILE
                     where n.USER_ID == usrName
                     select n).FirstOrDefault();

            if (type == "RoleID")
            {
                Value = r.ROLE_ID.ToString();
            }

            else if (type == "RoleName")
            {
                Value = r.CM_USER_ROLES.ROLE_NAME.ToString();
            }

            else if (type == "DisplayName")
            {
                Value = r.DISPLAY_NAME.ToString();
            }
            else if (type == "Email")
            {
                Value = r.EMAIL_ADDRESS.ToString();
            }
            else if (type == "FirstName")
            {
                Value = r.FIRSTNAME.ToString();
            }
            else if (type == "LastName")
            {
                Value = r.LASTNAME.ToString();
            }
            else if (type == "UserID")
            {
                Value = r.USER_ID.ToString();
            }
            return Value;

        }


        private void callMenu(int roleID)
        {
            //CM_ROLE_PERM_XREF rpx = new CM_ROLE_PERM_XREF();
            List<MenuViewModel> menuList = null;
            var menu = from n in db.CM_ROLE_PERM_XREF
                       where n.ROLE_ID == roleID
                       //where n.CM_PERMISSIONS.PARENT_PERMISSION != 0
                       orderby n.CM_PERMISSIONS.PERMISSIONDESCRIPTION
                       select new
                       {
                           PermissionId = n.CM_PERMISSIONS.PERMISSION_ID,
                           MenuDesc = n.CM_PERMISSIONS.PERMISSIONDESCRIPTION,
                           URL = n.CM_PERMISSIONS.FORM_URL,
                           ControllerName = n.CM_PERMISSIONS.CONTROLLER_NAME,
                           ParentPermission = n.CM_PERMISSIONS.PARENT_PERMISSION,
                           PermissionDesc = n.CM_PERMISSIONS.PERMISSIONDESCRIPTION,
                           ActionName = n.CM_PERMISSIONS.ACTION_NAME,
                           RoleID = n.ROLE_ID,
                           RoleName = n.CM_USER_ROLES.ROLE_NAME,
                           IconClass = n.CM_PERMISSIONS.ICON_CLASS,
                           IsopenClass = n.CM_PERMISSIONS.ISOPEN_CLASS,
                           ToggleIconClass = n.CM_PERMISSIONS.TOGGLE_ICON
                       };

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
                URL = o.URL
            }).ToList();

            this.Repeater1.DataSource = menuData.ToList();
            this.Repeater1.DataBind();


        }



        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            //using (var context = new CDMA_Model())
            //{
            //    string Uname = (String)(Session["UserID"]);
            //    Cdma.External.CM_USER_PROFILE CM_USER_PROFILE = db.CM_USER_PROFILE.Find(mp.getProfileID(Uname));

            //    CM_USER_PROFILE.ISLOCKED = 0;   //isloggedout
            //    db.Entry(CM_USER_PROFILE).State = EntityState.Modified;
            //    db.SaveChanges();
            //}
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

        }

       


    }

}