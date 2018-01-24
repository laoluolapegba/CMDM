using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Cdma.Web.DQI
{
    public partial class GoldenRecord3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            foreach (GridItem item in gridSummary.MasterTableView.Items)
            {
                GridDataItem dataitem = (GridDataItem)item;
                TableCell cell = dataitem["ClientSelectColumn"];
                CheckBox checkBox = (CheckBox)cell.Controls[0];
                if (checkBox.Checked)
                {
                    string value = dataitem.GetDataKeyValue("RECORD_ID").ToString();
                }
            }
        }
    }
}