using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System.Web.Security;
using CMdm.UI.Web.BLL;
using System.Data.Entity.Validation;
using System.Diagnostics;


using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using Oracle.DataAccess.Client;
using System.Text;
using System.Net.Mail;
using System.Net;
using CMdm.Data;

namespace Cdma.Web.CustomerInfo
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        public CustomMembershipProvider mp = new CustomMembershipProvider();
        //public Cdma.External.CDMA_Model db = new Cdma.External.CDMA_Model();
        AppDbContext db = new AppDbContext();

        private static string logs = "";
        private Customer custObj;
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        // public OracleCommand cmd = new OracleCommand();
        public OracleDataReader rdr;
        public OracleDataAdapter adapter = new OracleDataAdapter(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                var getRole = from r in db.CM_USER_ROLES
                              select new { ID = r.ROLE_ID, Name = r.ROLE_NAME };

                this.ddlRoleName.DataSource = getRole.ToList();
                this.ddlRoleName.DataValueField = "ID";
                this.ddlRoleName.DataTextField = "Name";
                this.ddlRoleName.DataBind();
                this.ddlRoleName.Items.Insert(0, new ListItem("--Select Role--", ""));


                this.ddlRoleforPerm.DataSource = getRole.ToList();
                this.ddlRoleforPerm.DataValueField = "ID";
                this.ddlRoleforPerm.DataTextField = "Name";
                this.ddlRoleforPerm.DataBind();
                this.ddlRoleforPerm.Items.Insert(0, new ListItem("--Select Role--", ""));

                var getPermissions = from p in db.CM_PERMISSIONS
                                     select new { ID = p.PERMISSION_ID, Name = p.PERMISSIONDESCRIPTION };

                this.ddlPermforPerm.DataSource = getPermissions.ToList();
                this.ddlPermforPerm.DataValueField = "ID";
                this.ddlPermforPerm.DataTextField = "Name";
                this.ddlPermforPerm.DataBind();
                this.ddlPermforPerm.Items.Insert(0, new ListItem("--Select Permission--", ""));


            //    var getMakerUsers = (from p in db.CM_USER_PROFILE
            //                         join r in db.CM_USER_ROLES on p.ROLE_ID equals r.ROLE_ID 
            //                         join q in db.CM_MAKER_CHECKER_XREFS on p.PROFILE_ID != q.maker_id
            //                         where r.ROLE_NAME == "maker"
            //                         //&& p.PROFILE_ID != p.CM_MAKER_CHECKER_XREF.FirstOrDefault().MAKER_ID
            //                         select new { ID = p.PROFILE_ID, Name = p.USER_ID });

            //    //&& p.PROFILE_ID (from x in db.CM_MAKER_CHECKER_XREF select new { x.MAKER_ID})
            //    //(from x in db.CM_MAKER_CHECKER_XREF select new { x.MAKER_ID })
            //    //join x in db.CM_MAKER_CHECKER_XREF on p.PROFILE_ID != x.MAKER_ID 
            //    this.ddlMakers.DataSource = getMakerUsers.ToList();
            //    this.ddlMakers.DataValueField = "ID";
            //    this.ddlMakers.DataTextField = "Name";
            //    this.ddlMakers.DataBind();
            //    this.ddlMakers.Items.Insert(0, new ListItem("--Select Makers--", ""));


            //    var getCheckerUsers = (from p in db.CM_USER_PROFILE
            //                           join r in db.CM_USER_ROLES on p.ROLE_ID equals r.ROLE_ID
            //                           where r.ROLE_NAME == "checker"
            //                           select new { ID = p.PROFILE_ID, Name = p.USER_ID });

            //    this.ddlcheckers.DataSource = getCheckerUsers.ToList();
            //    this.ddlcheckers.DataValueField = "ID";
            //    this.ddlcheckers.DataTextField = "Name";
            //    this.ddlcheckers.DataBind();
            //    this.ddlcheckers.Items.Insert(0, new ListItem("--Select Checker--", ""));

            }

            if (!IsPostBack)
            {
                String strQuery2 = "select p.profile_id as ID,P.ROLE_ID,P.USER_ID as Name from cm_user_profile p,cm_user_roles r " +
                                    "where P.ROLE_ID = R.ROLE_ID and R.ROLE_NAME = 'maker' and p.profile_id " +
                                    "not in (select distinct MAKER_ID from cm_maker_checker_xref)";

                //OracleCommand objCmd = new OracleCommand();
                OracleCommand objCmd2 = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd2.CommandType = CommandType.Text;
                objCmd2.CommandText = strQuery2;
                objCmd2.Connection = con;
                try
                {
                    con.Open();
                    ddlMakers.DataSource = objCmd2.ExecuteReader();
                    this.ddlMakers.DataValueField = "ID";
                    this.ddlMakers.DataTextField = "Name";
                    this.ddlMakers.DataBind();
                    this.ddlMakers.Items.Insert(0, new ListItem("--Select Makers--", ""));

                }
                catch (Exception ex)
                {
                      //throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }

                if (!IsPostBack)
                {
                    String strQuery4 = "select p.profile_id as ID,P.ROLE_ID,P.USER_ID as Name from cm_user_profile p,cm_user_roles r "+
                                        "where P.ROLE_ID = R.ROLE_ID and R.ROLE_NAME = 'checker'";

                    //OracleCommand objCmd = new OracleCommand();
                    OracleCommand objCmd4 = new OracleCommand();
                    con = new OracleConnection(new Connection().ConnectionString);
                    objCmd4.CommandType = CommandType.Text;
                    objCmd4.CommandText = strQuery4;
                    objCmd4.Connection = con;
                    try
                    {
                        con.Open();
                        ddlcheckers.DataSource = objCmd4.ExecuteReader();
                        this.ddlcheckers.DataValueField = "ID";
                this.ddlcheckers.DataTextField = "Name";
                this.ddlcheckers.DataBind();
                this.ddlcheckers.Items.Insert(0, new ListItem("--Select Checker--", ""));


                    }
                    catch (Exception ex)
                    {
                        // throw ex;
                    }
                    finally
                    {
                        con.Close();
                        con.Dispose();
                    }
                
                }

            this.btnCreateUser.Visible = true;
            this.btnUpdateUser.Visible = false;

            BindPageGrid();
            BindPageGridPerm();
            BindPageGridRolePermX();
            //BindPageGridMakerCheckerX();
        }

        private void BindPageGrid()
        {
            try
            {

                var getUserDetails = from u in db.CM_USER_PROFILE
                                     orderby u.PROFILE_ID descending
                                     select new
                                     {
                                         PROFILE_ID = u.PROFILE_ID,
                                         FIRSTNAME = u.FIRSTNAME.ToUpper(),
                                         LASTNAME = u.LASTNAME,
                                         EMAIL_ADDRESS = u.EMAIL_ADDRESS,
                                         USER_ID = u.USER_ID,
                                         ISAPPROVED = u.ISAPPROVED == true ? "yes" : "No",
                                         CREATED_DATE = u.CREATED_DATE,
                                         ROLE_NAME = u.CM_USER_ROLES.ROLE_NAME



                                     };

                this.GridView1.DataSource = getUserDetails.ToList();
                this.GridView1.DataBind();

            }
            catch (Exception ex)
            {
                this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
        }

        private void BindPageGridPerm()
        {
            try
            {

                var getPermDetails = from u in db.CM_PERMISSIONS
                                     orderby u.PERMISSION_ID descending
                                     select new
                                     {
                                         PermID = u.PERMISSION_ID,
                                         FormURL = u.FORM_URL,
                                         Desc = u.PERMISSIONDESCRIPTION,
                                         Controller = u.CONTROLLER_NAME,
                                         Action = u.ACTION_NAME

                                     };

                this.GridView3.DataSource = getPermDetails.ToList();
                this.GridView3.DataBind();
            }
            catch (Exception ex)
            {
                this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
        }

        private void BindPageGridRolePermX()
        {
            try
            {

                var getROlePermXDetails = from u in db.CM_ROLE_PERM_XREF
                                          orderby u.ROLE_ID descending
                                          select new
                                          {
                                              recordID = u.RECORD_ID,
                                              //Roles = u.PermRoles,
                                              //Permission = u.Permissions,
                                              Role = u.CM_USER_ROLES.ROLE_NAME,
                                              Permission = u.CM_PERMISSIONS.PERMISSIONDESCRIPTION,
                                              Createdby = u.CREATED_BY,
                                              DateCreated = u.CREATED_DATE


                                          };

                this.GridView4.DataSource = getROlePermXDetails.ToList();
                this.GridView4.DataBind();
            }
            catch (Exception ex)
            {
                this.lblAssgnPermsnMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
        }

        //private void BindPageGridMakerCheckerX()
       // {
           // try
           // {

            //    var getMakerCheckerXDetails = from u in db.CM_MAKER_CHECKER_XREF
            //                              orderby u.CHECKER_ID descending
            //                              select new
            //                              {
            //                                  recordID = u.RECORD_ID,
            //                                  Maker = u.CM_USER_PROFILE.USER_ID,
            //                                  Checker = u.CM_USER_PROFILE.USER_ID,
            //                                 // Createdby = u.CREATED_BY,
            //                                 // DateCreated = u.CREATED_DATE
            //                              };

            //    this.GridView5.DataSource = getMakerCheckerXDetails.ToList();
            //    this.GridView5.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    this.lblAssgnUserMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            //}
      //  }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";

            if (string.IsNullOrEmpty(this.txtUsername.Text))
            {
                this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("User Name required!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtPassword.Text))
            {
                this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Password required!");
                return;
            }

            //if (string.IsNullOrEmpty(this.txtConfirmPasswd.Text))
            //{
            //    this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Confirm Password required!");
            //    return;
            //}

            if (string.IsNullOrEmpty(this.txtEmail.Text))
            {
                this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Email required!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtfName.Text))
            {
                this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("First Name required!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtlName.Text))
            {
                this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Last Name required!");
                return;
            }

            if (string.IsNullOrEmpty(this.ddlRoleName.SelectedValue))
            {
                this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Role required!");
                return;
            }

            //if (this.txtPassword.Text != this.txtConfirmPasswd.Text)
            //{
            //    this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Password and confirm password must be thesame!");
            //    return;
            //}

            if (mp.getUserr(this.txtUsername.Text, this.txtEmail.Text) == true)
            {
                this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("User already exist!");

                return;
            }
            else
            {
                //Create new user
                try
                {
                    bool newUser = mp.CreateUser(this.txtUsername.Text, this.txtPassword.Text, this.ddlRoleName.SelectedValue, this.txtfName.Text, this.txtlName.Text, this.txtEmail.Text);

                    if (newUser == true)
                    {
                        this.lblUsrMsg.Text = MessageFormatter.GetFormattedSuccessMessage("User Created Succesfully!");

                        EmailHelper Email = new EmailHelper();                         
                       Email.UserCreationMailSender(this.txtEmail.Text,this.ddlRoleName.SelectedItem.ToString());
                        
                        clearForm();
                        GridView1.DataBind();
                    }
                    else
                    {
                        this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error Creating User!");
                    }

                }
                catch (Exception ex)
                {
                    this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message + ex.InnerException);
                }
            }


        }

      

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            try
            {
                bool updateUser = mp.UpdateUser(this.txtUsername.Text, this.txtfName.Text, this.txtlName.Text, this.ddlRoleName.SelectedValue, this.txtEmail.Text, Convert.ToDecimal(this.hidUerID.Value));

                if (updateUser == true)
                {
                    this.lblUsrMsg.Text = MessageFormatter.GetFormattedSuccessMessage("User Updated Succesfully!");
                    clearForm();
                    GridView2.DataBind();
                }
                else
                {
                    this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error Updating User!");
                    clearForm();
                }

            }
            catch (Exception ex)
            {
                this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
                clearForm();
            }


            this.txtUsername.Enabled = true; this.txtPassword.Enabled = true;
           // this.txtConfirmPasswd.Enabled = true;
            this.btnUpdateUser.Visible = false;
            this.btnCreateUser.Visible = true;
            Usrs.Attributes.Add("class", "active");
        }
        protected void OnEdit_User(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            this.txtUsername.Enabled = false; this.txtPassword.Enabled = false;
            //this.txtConfirmPasswd.Enabled = false;
            this.btnUpdateUser.Visible = true;
            this.btnCreateUser.Visible = false;

            LinkButton lnk = sender as LinkButton;

            decimal UserId = Convert.ToDecimal(lnk.Attributes["RecId"]);

            var edit = from n in db.CM_USER_PROFILE
                       where n.PROFILE_ID == UserId
                       select n;
            try
            {

                foreach (var u in edit)
                {
                    this.txtUsername.Text = u.USER_ID;
                    this.hidUerID.Value = Convert.ToString(u.PROFILE_ID);
                    this.txtEmail.Text = u.EMAIL_ADDRESS;
                    this.txtfName.Text = u.FIRSTNAME;
                    this.txtlName.Text = u.LASTNAME;
                    this.ddlRoleName.SelectedIndex = this.ddlRoleName.Items.IndexOf(this.ddlRoleName.Items.FindByValue(u.CM_USER_ROLES.ROLE_NAME));

                }
            }
            catch (Exception ex)
            {
                lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: " + ex.Message);
            }
            //Usrs.Attributes.Add("class", "active");

        }


        protected void OnDelete_Permission(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];

            try
            {
                if (mp.DeletePermission(Convert.ToDecimal(recId)) == true)
                {
                    this.lblPermsnMsg.Text = MessageFormatter.GetFormattedSuccessMessage("Permission Deleted Succesfully!");

                    GridView2.DataBind();

                }
                else
                {
                    this.lblPermsnMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error Deleted Permission!");
                }

            }
            catch (Exception ex)
            {
                this.lblPermsnMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
            }
            // Permssn.Attributes.Add("class", "active");

        }

        protected void OnDelete_User(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];

            try
            {
                if (mp.DeleteUser(Convert.ToDecimal(recId)) == true)
                {
                    this.lblUsrMsg.Text = MessageFormatter.GetFormattedSuccessMessage("User Deleted Succesfully!");

                    GridView2.DataBind();
                }
                else
                {
                    this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error Deleted User!");
                }

            }
            catch (Exception ex)
            {
                this.lblUsrMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
            }
            // Usrs.Attributes.Add("class", "active");

        }

        protected void OnDelete_AssignedPermission(object sender, EventArgs e)
        {
            // AssignPermssns.Attributes.Add("class", "active");
            hidTAB.Value = "#tab4";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];

            try
            {
                if (mp.DeleteAssignedPermissions(Convert.ToDecimal(recId)) == true)
                {
                    this.lblAssgnPermsnMsg.Text = MessageFormatter.GetFormattedSuccessMessage("Assigned Permission Deleted Succesfully!");

                    GridView2.DataBind();
                }
                else
                {
                    this.lblAssgnPermsnMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error Deleted Assigned Permission!");
                }

            }
            catch (Exception ex)
            {
                this.lblAssgnPermsnMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
            }

        }


        protected void OnDelete_AssignedUser(object sender, EventArgs e)
        {
            // AssignPermssns.Attributes.Add("class", "active");
            hidTAB.Value = "#tab5";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];

            try
            {
                if (mp.DeleteAssignedUsers(Convert.ToDecimal(recId)) == true)
                {
                    this.lblAssgnUserMsg.Text = MessageFormatter.GetFormattedSuccessMessage("Assigned User Deleted Succesfully!");

                    GridView5.DataBind();
                }
                else
                {
                    this.lblAssgnUserMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error Deleted Assigned User!");
                }

            }
            catch (Exception ex)
            {
                this.lblAssgnUserMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
            }

        }



        private void clearForm()
        {
            this.txtUsername.Text = "";
            this.txtPassword.Text = "";
            this.txtfName.Text = "";
            this.txtlName.Text = "";
            this.ddlRoleName.SelectedValue = "";
            this.txtEmail.Text = "";
            this.ddlPermforPerm.SelectedValue = "";
            this.ddlRoleforPerm.SelectedValue = "";
        }

        
        protected void btnCreateRole_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab2";
            if (string.IsNullOrEmpty(this.txtRole.Text))
            {
                return;
            }

            if (mp.getRole(this.txtRole.Text) == true)
            {
                this.lblRolMsg.Text = MessageFormatter.GetFormattedErrorMessage("Role already exist!");

                return;
            }

            try
            {
                if (mp.CreateRole(this.txtRole.Text) == true)
                {
                    this.lblRolMsg.Text = MessageFormatter.GetFormattedSuccessMessage("Role Created Succesfully!");
                    txtRole.Text = "";
                    GridView2.DataBind();
                }
                else
                {
                    this.lblRolMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error Creating Role!");
                }

            }
            catch (Exception ex)
            {
                this.lblRolMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
            }
            //Rols.Attributes.Add("class", "active");

        }

        protected void btnPermission_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";
            if (string.IsNullOrEmpty(this.txtPermDesc.Text))
            {
                this.lblPermsnMsg.Text = MessageFormatter.GetFormattedErrorMessage("Permission Description required!");
                return;
            }

            if (mp.getPermssn(this.txtPermDesc.Text, this.txtController.Text, this.txtAction.Text, this.txtFormURL.Text) == true)
            {
                this.lblPermsnMsg.Text = MessageFormatter.GetFormattedErrorMessage("Permission already exist!");

                return;
            }
            else
            {

                try
                {
                    bool newPermssn = mp.CreatePermissions(this.txtPermDesc.Text, this.txtController.Text, this.txtAction.Text, this.txtFormURL.Text);

                    if (newPermssn == true)
                    {
                        this.lblPermsnMsg.Text = MessageFormatter.GetFormattedSuccessMessage("Permission Created Succesfully!");
                        clearPermissionForm();
                    }
                    else
                    {
                        this.lblPermsnMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error Creating Permission!");
                    }

                }
                catch (Exception ex)
                {
                    this.lblPermsnMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
                }

                //User for getting indebt exception error messages.
                //catch (DbEntityValidationException dbEx)
                //{
                //    foreach (var validationErrors in dbEx.EntityValidationErrors)
                //    {
                //        foreach (var validationError in validationErrors.ValidationErrors)
                //        {
                //            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                //        }
                //    }
                //}


            }
            // Permssn.Attributes.Add("class", "active");

        }

        private void clearPermissionForm()
        {
            this.txtFormURL.Text = "";
            this.txtController.Text = "";
            this.txtAction.Text = "";
            this.txtPermDesc.Text = "";

        }

        protected void btnAssignPermission_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab4";
            if (string.IsNullOrEmpty(this.ddlRoleforPerm.Text))
            {
                this.lblAssgnPermsnMsg.Text = MessageFormatter.GetFormattedErrorMessage("Role required!");
                return;
            }

            if (string.IsNullOrEmpty(this.ddlPermforPerm.Text))
            {
                this.lblAssgnPermsnMsg.Text = MessageFormatter.GetFormattedErrorMessage("Permission required!");
                return;
            }

            try
            {
                bool newRPX = mp.MapRolestoPermission(Convert.ToInt32(this.ddlRoleforPerm.Text), Convert.ToInt32(this.ddlPermforPerm.Text));

                if (newRPX == true)
                {
                    this.lblAssgnPermsnMsg.Text = MessageFormatter.GetFormattedSuccessMessage("Permission assigned Succesfully!");
                    clearForm();
                    GridView4.DataBind();
                }
                else
                {
                    this.lblAssgnPermsnMsg.Text = MessageFormatter.GetFormattedErrorMessage("Permission already asigned!");
                }

            }
            catch (Exception ex)
            {
                this.lblAssgnPermsnMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
            }
            //AssignPermssns.Attributes.Add("class", "active");

        }
        // }


        protected void btnAssignUser_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab5";
            if (string.IsNullOrEmpty(this.ddlMakers.Text))
            {
                this.lblAssgnUserMsg.Text = MessageFormatter.GetFormattedErrorMessage("Maker Name required!");
                return;
            }

            if (string.IsNullOrEmpty(this.ddlcheckers.Text))
            {
                this.lblAssgnUserMsg.Text = MessageFormatter.GetFormattedErrorMessage("Checker Name required!");
                return;
            }



            con.Open();

            try
            {

                OracleCommand cmd = new OracleCommand();
                // Set the command text on an OracleCommand object
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.BindByName = true;
                cmd.CommandText = "insert into CDMS.CM_MAKER_CHECKER_XREF (MAKER_ID,CHECKER_ID)" +
                                    " values (:p_MakerID,:p_CheckerID)";//

                OracleParameter prm = new OracleParameter();
                cmd.Parameters.Add(":p_MakerID", OracleDbType.Int32).Value = Convert.ToInt32(this.ddlMakers.SelectedValue);
                cmd.Parameters.Add(":p_CheckerID", OracleDbType.Int32).Value = Convert.ToInt32(this.ddlcheckers.SelectedValue);
                
                // Execute the command
                var rst = cmd.ExecuteNonQuery();
                this.lblAssgnUserMsg.Text = MessageFormatter.GetFormattedSuccessMessage("User assigned sucessfully!");

                //Emailing
                EmailHelper Email = new EmailHelper();
                Email.UserAssignmentMailSender(this.ddlMakers.SelectedValue, this.ddlcheckers.SelectedValue);
                
                this.ddlMakers.SelectedValue = "";
                this.ddlcheckers.SelectedValue = "";
            }
            catch (Exception ex)
            {
                lblAssgnUserMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }

            // Close and Dispose OracleConnection object
            con.Close();
            con.Dispose();

            //bind data to gridview
            GridView5.DataBind();

        }


      
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            hidTAB.Value = "#tab1";
            GridView1.PageIndex = e.NewPageIndex;
            BindPageGrid();
        }
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            hidTAB.Value = "#tab2";
            GridView2.PageIndex = e.NewPageIndex;
            BindPageGrid();
        }

        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            hidTAB.Value = "#tab3";
            GridView3.PageIndex = e.NewPageIndex;
            BindPageGrid();
        }

        protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            hidTAB.Value = "#tab4";
            GridView4.PageIndex = e.NewPageIndex;
            BindPageGrid();

        }

        protected void GridView5_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            hidTAB.Value = "#tab5";
            GridView5.PageIndex = e.NewPageIndex;
            BindPageGrid();

        }

        
    }
}