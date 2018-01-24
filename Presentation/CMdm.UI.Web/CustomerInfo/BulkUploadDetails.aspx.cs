using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMdm.UI.Web.BLL;
using Oracle.DataAccess.Client;
using System.Configuration;

namespace Cdma.Web.CustomerInfo
{
    public partial class BulkUploadDetails : System.Web.UI.Page
    {
        private static string logs = "";
        private Customer custObj;
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        // public OracleCommand cmd = new OracleCommand();
        public OracleDataReader rdr;
        public OracleDataAdapter adapter = new OracleDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) //check if the webpage is loaded for the first time.
            {
                ViewState["PreviousPage"] =
            Request.UrlReferrer;//Saves the Previous page url in ViewState

            }

            string Prc = Request.QueryString["prc"];
            string CustomerID = Request.QueryString["CUSTOMER_ID"];

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "pkg_cdms2." + Prc;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.BindByName = true;

            try
            {
                con.Open();

                //cmd.Parameters.Add("p_maker", OracleDbType.Varchar2).Value = this.DDLMakerList.SelectedValue;
                cmd.Parameters.Add(new OracleParameter("p_customer_id", OracleDbType.Varchar2, 50));
                cmd.Parameters[0].Value = CustomerID;

                cmd.Parameters.Add("p_out", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    int columncount = GridView1.Rows[0].Cells.Count;
                    GridView1.Rows[0].Cells.Clear();
                    GridView1.Rows[0].Cells.Add(new TableCell());
                    GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView1.Rows[0].Cells[0].Text = "No Records Found";
                }


            }
            catch (Exception ex)
            {
                lblMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message + ex.StackTrace + " <br />");
            }
            finally
            {
                con.Close();
                con.Dispose();
            }


        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (ViewState["PreviousPage"] != null)	//Check if the ViewState 
            //contains Previous page URL
            {
                Response.Redirect(ViewState["PreviousPage"].ToString());//Redirect to 
                //Previous page by retrieving the PreviousPage Url from ViewState.
            }
        }

    }
}