using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
namespace Cdma.Web.DQI
{
    public partial class GRRawData : System.Web.UI.Page
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDataForgridValidity();
            }
        }
        private void LoadDataForgridValidity()
        {
            gridRawData.DataSource = GetDataTable();
        }
        protected void gridValidity_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = GetDataTable();
        }
        public DataTable GetDataTable()
        {

            OracleConnection conn = new OracleConnection(connString);
            DataTable myDataTable = new DataTable();
            try
            {
                string key = Request.QueryString["key"].ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("select fld_key,BVN,CUST_FIRST_NAME,CUST_MIDDLE_NAME,CUST_LAST_NAME,MINOR_FLAG,PHONE,EMAIL,DOB,GENDER from new_uba_cdma where bvn_key = ");
                //sb.Append(column);
                sb.Append(key);

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = new OracleCommand(sb.ToString(), conn);

                conn.Open();
                adapter.Fill(myDataTable);
            }
            finally
            {
                conn.Close();
            }

            return myDataTable;
        }

        protected void gridValidity_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            LoadDataForgridValidity();
        }

        protected void gridValidity_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LoadDataForgridValidity();
        }

        protected void gridValidity_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadDataForgridValidity();
        }

        protected void gridValidity_PreRender(object sender, EventArgs e)
        {
            foreach (GridCommandItem item in gridRawData.MasterTableView.GetItems(GridItemType.CommandItem))
            {
                Button btn = (Button)item.FindControl("AddNewRecordButton");
                //LinkButton Addbtn = (LinkButton)item.FindControl("InitInsertButton");
                btn.Visible = false;
                //Addbtn.Visible = false;
            }
        }
    }
}