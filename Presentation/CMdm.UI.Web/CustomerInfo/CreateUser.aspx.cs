using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System.Web.Security;
using CMdm.UI.Web.BLL;
using CMdm.Data;

namespace Cdma.Web.CustomerInfo
{
    public partial class CreateUser : System.Web.UI.Page
    {
        public CustomMembershipProvider mp = new CustomMembershipProvider();
        //public Cdma.External.CDMA_Model db = new Cdma.External.CDMA_Model();
        AppDbContext db = new AppDbContext();
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
            }
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(this.txtUsername.Text))
            {
                this.lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("User Name required!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtPassword.Text))
            {
                this.lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Password required!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtConfirmPasswd.Text))
            {
                this.lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Confirm Password required!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtEmail.Text))
            {
                this.lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Email required!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtfName.Text))
            {
                this.lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("First Name required!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtlName.Text))
            {
                this.lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Last Name required!");
                return;
            }

            if (string.IsNullOrEmpty(this.ddlRoleName.SelectedValue))
            {
                this.lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Role required!");
                return;
            }

            if (mp.getUserr(this.txtUsername.Text, this.txtEmail.Text) == true)
            {
                this.lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("User already exist!");

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
                        this.lblMsg.Text = MessageFormatter.GetFormattedSuccessMessage("User Created Succesfully!");
                        clearForm();
                    }
                    else
                    {
                        this.lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error Creating Role!");
                    }

                }
                catch (Exception ex)
                {
                    this.lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
                }
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
        }
    }
}
