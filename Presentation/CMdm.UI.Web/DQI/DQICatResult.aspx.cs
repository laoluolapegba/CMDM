using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Configuration;
using Oracle.DataAccess.Client;
using System.Text;

namespace Cdma.Web.DQI
{
    public partial class DQICatResult : System.Web.UI.Page
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
            gridSummary.DataSource = GetDataTable();
        }
        protected void gridSummary_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = GetDataTable();
        }
        public DataTable GetDataTable()
        {

            OracleConnection conn = new OracleConnection(connString);
            DataTable myDataTable = new DataTable();
            try
            {
                string key = Request.QueryString["branchcode"].ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("select a.process_id, c.catalog_id , catalog_name , dqi_result  dqi_result, previous_dqi_result previous_result, dat_last_run ");
                sb.Append("from cdma_dqi_processing_result a, mdm_catalog c ");
                sb.Append("where a.mdm_catalog_id = c.catalog_id ");
                sb.Append("and branch_code= ");
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
            foreach (GridCommandItem item in gridSummary.MasterTableView.GetItems(GridItemType.CommandItem))
            {
                Button btn = (Button)item.FindControl("AddNewRecordButton");
                //LinkButton Addbtn = (LinkButton)item.FindControl("InitInsertButton");
                btn.Visible = false;
                //Addbtn.Visible = false;
            }
        }
    }
}