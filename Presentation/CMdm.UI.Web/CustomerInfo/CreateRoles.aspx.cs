using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.UI.Web.BLL;

namespace Cdma.Web.CustomerInfo
{
    public partial class CreateRoles : System.Web.UI.Page
    {

       public CustomMembershipProvider mp = new CustomMembershipProvider();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateRole_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtRole.Text))
            {
             return;
            }

            if (mp.getRole(this.txtRole.Text) == true)
            {
                this.lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Role already exist!");

                return;
            }

            try
            {
                if (mp.CreateRole(this.txtRole.Text) == true)
                {
                    this.lblMsg.Text = MessageFormatter.GetFormattedSuccessMessage("Role Created Succesfully!");
                    txtRole.Text = "";
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
}