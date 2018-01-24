using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMdm.UI.Web.BLL;
using Oracle.DataAccess.Client;

namespace Cdma.Web.CustomerInfo
{
    public partial class AuthBulkBasicCustInfo : System.Web.UI.Page
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
            

            if (!this.IsPostBack)
            {
                OracleCommand objCmd = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                
                using (objCmd.Connection = con)
                {
                    using ( objCmd = new OracleCommand("Select LAST_MODIFIED_BY from CDMS.V_MAKERS"))
                    {
                        objCmd.CommandType = CommandType.Text;
                        objCmd.Connection = con;
                        con.Open();
                        DDLMakerList.DataSource = objCmd.ExecuteReader();
                        DDLMakerList.DataTextField = "LAST_MODIFIED_BY";
                        DDLMakerList.DataValueField = "LAST_MODIFIED_BY";
                        DDLMakerList.DataBind();
                        con.Close();
                    }
                }
                DDLMakerList.Items.Insert(0, new ListItem("--Select Maker--", "0"));
            }


            

        }


        protected void btnSearchMaker_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "pkg_cdms2.prc_basic_customer_per_maker";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.BindByName = true;

            try
            {
                con.Open();

                //cmd.Parameters.Add("p_maker", OracleDbType.Varchar2).Value = this.DDLMakerList.SelectedValue;
                cmd.Parameters.Add(new OracleParameter("p_maker", OracleDbType.Varchar2, 50));
                cmd.Parameters[0].Value = this.DDLMakerList.SelectedValue;

                cmd.Parameters.Add("p_out", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    int columncount = GridView2.Rows[0].Cells.Count;
                    GridView2.Rows[0].Cells.Clear();
                    GridView2.Rows[0].Cells.Add(new TableCell());
                    GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView2.Rows[0].Cells[0].Text = "No Records Found";
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

        protected void OnDecline_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            string recId = lnk.Attributes["RecId"];

            ProcessRequest(Convert.ToInt32(recId),"NULL");

        }

        protected void OnApprove_Click(object sender, EventArgs e)
        {

            LinkButton lnk = sender as LinkButton;
            string recId = lnk.Attributes["RecId"];

            ProcessRequest(Convert.ToInt32(recId), "Y");
        }



        private int ProcessRequest(Int32 CustomerID, string status)
        {
           
            int cnt = 0;
            //audit = new Audit();
            OracleConnection con = new OracleConnection();
            con.ConnectionString = new Connection().ConnectionString;
            con.Open();
            try
            {
                OracleCommand cmd = new OracleCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.CommandText = "pkg_cdms2.prc_basic_detail_bulk_approve";//prc_basic_bulk_approve 

                cmd.BindByName = true;

                OracleParameter prm = new OracleParameter();
                cmd.Parameters.Add("p_customer_id", OracleDbType.Int32).Value = CustomerID;
                cmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = status;
                cmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
                    
                cmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";

                int result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                // Close and Dispose OracleConnection object
                if (result == -1)
                {
                    //this.lblMsg.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Record Successfully Updated!");
                    cnt++;

                    GridView2.DataBind();
                }

                


            }
            catch (Exception ex)
            {
                this.lblMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Source + "  " + ex.Message);
                
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return cnt;
        }


        protected void btnApproveBulkMaker_Click(object sender, EventArgs e)
        {

            int totalRec = 0;
            
            foreach (GridViewRow row in GridView2.Rows)
            {
                
                totalRec += ProcessRequest(Convert.ToInt32(row.Cells[0].Text), "Y");
                //values.Add(row.Cells[0].Text);
            }


            this.lblMsg.Text = MessageFormatter.GetFormattedSuccessMessage(string.Format("{0} Customer Record Successfully Authorized!",totalRec));

            //lblMsg.Text = MessageFormatter.GetFormattedSuccessMessage(string.Format("Uploaded {0} records", totalRec));
        }

        protected void btnDeclineBulkMaker_Click(object sender, EventArgs e)
        {
            int totalRec = 0;
            foreach (GridViewRow row in GridView2.Rows)
            {

                totalRec += ProcessRequest(Convert.ToInt32(row.Cells[0].Text), "NULL");
                //values.Add(row.Cells[0].Text);
            }
           
            this.lblMsg.Text = MessageFormatter.GetFormattedNoticeMessage(string.Format("{0} Customer Record Successfully Declined!", totalRec));
        }
    }
}