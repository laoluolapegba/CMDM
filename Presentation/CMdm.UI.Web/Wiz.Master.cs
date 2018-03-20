using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMdm.Data;
using System.Web.Security;
using CMdm.UI.Web.BLL;
using CMdm.Data.Rbac;
using CMdm.Entities.ViewModels;

namespace CMdm.UI.Web
{
    public partial class Wiz : System.Web.UI.MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        //public Cdma.External.CDMA_Model db = new Cdma.External.CDMA_Model();
        AppDbContext db = new AppDbContext();
        public CustomMembershipProvider mp = new CustomMembershipProvider();
        //public string DQIValue
        //{
        //    get
        //    {
        //        return lblDataQuality.Text;
        //    }
        //    set
        //    {
        //        lblDataQuality.Text = value;
        //    }
        //}
        //public int CustCount
        //{
        //    get
        //    {
        //        return Convert.ToInt32(lblNoOfCustomers.Text);
        //    }
        //    set
        //    {
        //        lblNoOfCustomers.Text = value.ToString();
        //    }
        //}
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
                //DQIProfileBiz rep = new DQIProfileBiz();
                //string dqi = rep.DQIValue();
                //int custCount = rep.CustCount("UBA_CDMA");

                //lblDataQuality.Text = dqi.ToString();
                //lblNoOfCustomers.Text = custCount.ToString();

                string path = HttpContext.Current.Request.Url.AbsolutePath;

                //int roleID = 0;
                try
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        var identity = ((CustomPrincipal)HttpContext.Current.User).CustomIdentity;
                        lblName.Text = string.Format("Welcome {0},", identity.FirstName);

                        //this.lblProfileName.Text = identity.DisplayName; // getCustomIdentities(usrName, "DisplayName");

                        //callMenu(identity.UserRoleId);
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

        private bool checkCurrentURL(string path, int roleID)
        {
            bool status; int rID = roleID;
            int menu = (from n in db.CM_ROLE_PERM_XREF
                        join p in db.CM_PERMISSIONS on n.PERMISSION_ID equals p.PERMISSION_ID
                        where n.ROLE_ID == rID && p.FORM_URL.Contains(path)
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
            //List<MenuViewModel> menuList = null;
            var menu = from n in db.CM_ROLE_PERM_XREF
                       join p in db.CM_PERMISSIONS on n.PERMISSION_ID equals p.PERMISSION_ID
                       join r in db.CM_USER_ROLES on n.ROLE_ID equals r.ROLE_ID
                       where n.ROLE_ID == roleID
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
                           //PermissionId = n.CM_PERMISSIONS.PERMISSION_ID,
                           //MenuDesc = n.CM_PERMISSIONS.PERMISSIONDESCRIPTION,
                           //URL = n.CM_PERMISSIONS.FORM_URL,
                           //ControllerName = n.CM_PERMISSIONS.CONTROLLER_NAME,
                           //ParentPermission = n.CM_PERMISSIONS.PARENT_PERMISSION,
                           //PermissionDesc = n.CM_PERMISSIONS.PERMISSIONDESCRIPTION,
                           //ActionName = n.CM_PERMISSIONS.ACTION_NAME,
                           //RoleID = n.ROLE_ID,
                           //RoleName = n.CM_USER_ROLES.ROLE_NAME,
                           //IconClass = n.CM_PERMISSIONS.ICON_CLASS,
                           //IsopenClass = n.CM_PERMISSIONS.ISOPEN_CLASS,
                           //ToggleIconClass = n.CM_PERMISSIONS.TOGGLE_ICON,
                           //Url = n.CM_PERMISSIONS.FORM_URL
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
                Url = o.URL
            }).ToList();

            //this.trMenu.DataSource = menuData.ToList();
            //this.trMenu.DataBind();


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

        }

    }
}