using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cdma.Web.DQI
{
    public partial class GoldenRecord1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        bool NewEnabled
        {
            get
            {

                return (Session["NewEnabled"] == null) ? false : Convert.ToBoolean(Session["NewEnabled"]);
            }
            set
            {
                Session["NewEnabled"] = value;
            }
        }
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            NewEnabled = (sender as CheckBox).Checked;
        }
    }
}