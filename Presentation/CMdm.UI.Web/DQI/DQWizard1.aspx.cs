using CMdm.Data;
using CMdm.Data;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using Elmah;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMdm.UI.Web.BLL;
using Elmah;
using Oracle.DataAccess.Client;
using CMdm.Data.Rbac;

namespace Cdma.Web
{
    public partial class DQWizard1 : System.Web.UI.Page
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);

        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        //public Cdma.External.CDMA_Model db = new Cdma.External.CDMA_Model();
        AppDbContext db = new AppDbContext();
        public CustomMembershipProvider mp = new CustomMembershipProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                lblCurrentUsers.Text = "5"; //Convert.ToString(mp.GetNumberOfUsersOnline());
                lblNoOfCustomers.Text = "2,500,000";//String.Format("{0:#,###,###}", Convert.ToInt32(mp.getNoOfCustomers())); //"6,897,752";
                lblDataQuality.Text = "34.7";//mp.getDQIPercentage();
                string path = HttpContext.Current.Request.Url.AbsolutePath;

                //int roleID = 0;
                try
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        var identity = ((CustomPrincipal)User).CustomIdentity;
                        this.lblRole.Text = identity.UserRoleName;// getCustomIdentities(usrName, "RoleName");

                        this.lblProfileName.Text = identity.DisplayName; // getCustomIdentities(usrName, "DisplayName");

                        //string getRoleID = getCustomIdentities(usrName, "RoleID");

                        //Generate menu.
                        callMenu(identity.UserRoleId);//Convert.ToInt32(getRoleID)


                        if (!checkCurrentURL(path, identity.UserRoleId))
                        {
                            Response.Redirect("~/Default.aspx?msg=The Page is unavailable.");
                        }
                    }
                        //string x = "admin"; 
                        //string usrName = (String)(Session["UserID"]);//Request.QueryString["id"];//"maker";//(String)(Session["UserID"]);
                        
                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                   
                }

            }
            //RadListBoxSource.SelectedIndexChanged += new EventHandler(RadListBoxSource_SelectedIndexChanged);
            //RadListBoxSource.SelectedIndexChanged += new EventHandler(RadListBoxSource_SelectedIndexChanged);
        }
        protected void RadListBoxSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.RadListBoxDestination.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //DataCatalogBiz bizRule = new DataCatalogBiz();

            //if (bizRule.IsCatalogExist(RadListBoxSource.SelectedValue))
            //{
            //    this.lblmsgs.Text = MessageFormatter.GetFormattedNoticeMessage("Catalog Already Exists");
            //    return;
            //}
            //if (RadListBoxDestination.CheckedItems.Count < 1)
            //{
            //    this.lblmsgs.Text = MessageFormatter.GetFormattedNoticeMessage("At least 1 Catalog Attribute must be selected.");
            //    return;
            //}
            //using (OracleConnection connection = new OracleConnection(connString))
            //{
            //    string strSQL = @"insert into MDM_DQI_PARAMETERS(table_categories, table_names,table_desc,column_names,column_desc,
            //        created_by,created_date,record_status,column_order)
            //        values(:table_categories, :table_names,:table_desc,:column_names,:column_desc,
            //        :created_by,:created_date,:record_status, :column_order)";
            //    try
            //    {

            //        connection.Open();

            //        OracleCommand command = new OracleCommand(strSQL, connection);
            //        command.BindByName = true;
            //        command.CommandType = System.Data.CommandType.Text;

            //        foreach (var item in RadListBoxDestination.CheckedItems)
            //        {
            //            command.Parameters.Add(":table_categories", OracleDbType.Varchar2).Value = "A";
            //            command.Parameters.Add(":table_names", OracleDbType.Varchar2).Value = RadListBoxSource.SelectedValue;
            //            command.Parameters.Add(":table_desc", OracleDbType.Varchar2).Value = RadListBoxSource.SelectedValue;
            //            command.Parameters.Add(":column_names", OracleDbType.Varchar2).Value = item.Text;
            //            command.Parameters.Add(":column_desc", OracleDbType.Varchar2).Value = item.Text;
            //            command.Parameters.Add(":created_by", OracleDbType.Varchar2).Value = User.Identity.Name;
            //            command.Parameters.Add(":created_date", OracleDbType.Date).Value = DateTime.Now;

            //            command.Parameters.Add(":record_status", OracleDbType.Varchar2).Value = "Y";
            //            command.Parameters.Add(":column_order", OracleDbType.Int32).Value = item.Value;
            //            command.ExecuteNonQuery();
            //            command.Parameters.Clear();
            //        }
            //        gridCat.Rebind();
            //        this.lblmsgs.Text = MessageFormatter.GetFormattedSuccessMessage("Catalog Added Succesfully");
                    


                //}
                //catch (Exception ex)
                //{
                //    ErrorSignal.FromCurrentContext().Raise(ex);
                //    lblmsgs.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
                //}

            }
        protected void Page_PreLoad(object sender, EventArgs e)
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

            Page.PreLoad += Page_PreLoad;
        }

        private void callMenu(int roleID)
        {
            CM_ROLE_PERM_XREF rpx = new CM_ROLE_PERM_XREF();

            var menu = from n in db.CM_ROLE_PERM_XREF
                                  join p in db.CM_PERMISSIONS on n.PERMISSION_ID equals p.PERMISSION_ID
                                  join r in db.CM_USER_ROLES on n.ROLE_ID equals r.ROLE_ID
                                  where n.ROLE_ID == roleID
                                  orderby p.PERMISSIONDESCRIPTION
                       select new
                       {
                           MenuDesc = p.PERMISSIONDESCRIPTION,
                           URL = p.FORM_URL,
                           Controller = p.CONTROLLER_NAME,
                           Action = p.ACTION_NAME,
                           RoleID = n.ROLE_ID,
                           RoleName = r.ROLE_NAME
                           //MenuDesc = n.CM_PERMISSIONS.PERMISSIONDESCRIPTION,
                           //URL = n.CM_PERMISSIONS.FORM_URL,
                           //Controller = n.CM_PERMISSIONS.CONTROLLER_NAME,
                           //Action = n.CM_PERMISSIONS.ACTION_NAME,
                           //RoleID = n.ROLE_ID,
                           //RoleName = n.CM_USER_ROLES.ROLE_NAME
                       };
            
            this.Repeater1.DataSource = menu.ToList();
            this.Repeater1.DataBind();
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

    }
       

}