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
    public partial class MergedWriteback : System.Web.UI.Page
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        //public MdmDqi.Data.MdmModel DQIdb = new MdmDqi.Data.MdmModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDataForgridValidity();
            }
        }
        private void LoadDataForgridValidity()
        {
            gridAll.DataSource = GetDataTable();
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
                //` string column = Request.QueryString["column"].ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT BANK_ID,CIF_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,DOB,GENDER,OCCUPATION,MINOR_FLAG,MARITAL_STATUS,EMPLOYER_NAME,NATIONALITY from CDMA_RESULT where rownum < 500");
                //sb.Append(column);
                //sb.Append(" is null and rownum < 100");

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
            foreach (GridCommandItem item in gridAll.MasterTableView.GetItems(GridItemType.CommandItem))
            {
                Button btn = (Button)item.FindControl("AddNewRecordButton");
                //LinkButton Addbtn = (LinkButton)item.FindControl("InitInsertButton");
                btn.Visible = false;
                //Addbtn.Visible = false;
            }
        }
    }
}