using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMdm.UI.Web.BLL;
using Oracle.DataAccess.Client;
using Elmah;

namespace Cdma.Web.CustomerInfo
{
    public partial class CustomerReports : System.Web.UI.Page
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
               

//////////////////////////////////////////////////////////////////////////////////////////////////////////
            
                String strQuery2 = "SELECT branch_code from CDMA_CUSTOMER_BRANCHES";
                ddlBranch.AppendDataBoundItems = true;
                //OracleCommand objCmd = new OracleCommand();
                OracleCommand objCmd2 = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd2.CommandType = CommandType.Text;
                objCmd2.CommandText = strQuery2;
                objCmd2.Connection = con;
                try
                {
                    con.Open();
                    ddlBranch.DataSource = objCmd2.ExecuteReader();
                    ddlBranch.DataTextField = "branch_code";
                    ddlBranch.DataValueField = "branch_code";
                    ddlBranch.DataBind();

                    ddlbranch2.DataSource = objCmd2.ExecuteReader();
                    ddlbranch2.DataTextField = "branch_code";
                    ddlbranch2.DataValueField = "branch_code";
                    ddlbranch2.DataBind();
                    //ddlBranch.Items.Add(new ListItem("001", "001"));

                    string Branch = ddlBranch.SelectedItem.Value == null ? "001" : ddlBranch.SelectedItem.Value;

                    DQIBeforeBVNCompletenessPageDataBind(ddlCategory.SelectedItem.Value, ddlBranch.SelectedItem.Value);
                    DQIAfterBVNCompletenessPageDataBind(ddlCat2.SelectedItem.Value, ddlbranch2.SelectedItem.Value);
                    DQIBeforeBVNCorrectnessPageDataBind(ddlCategory.SelectedItem.Value, ddlBranch.SelectedItem.Value);
                    DQIAfterBVNCorrectnessPageDataBind(ddlCat2.SelectedItem.Value, ddlbranch2.SelectedItem.Value);
                    DQIBE4BVNPageDataBind();
                    DQIAfterBVNPageDataBind();
                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                    //throw ex;
                }
                finally
                {
                    con.Close();
                  // con.Dispose();
                }
/////////////////////////////////////////////////////////////////////////////////////////////
                
                //BuzContactDetailPageDataBind();
                
            }
        
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }
        protected void btnDownloadExcelIndivCompReport_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=IndivCompletenessReportExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                DQIBeforeBVNCompletenessPageDataBind(ddlCategory.SelectedItem.Value, ddlBranch.SelectedItem.Value);//.BindGrid();

                GridView1.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        protected void btnDownloadExcelIndivCorrReport_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=IndivCorrectnessReportExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView2.AllowPaging = false;
                DQIBeforeBVNCorrectnessPageDataBind(ddlCategory.SelectedItem.Value, ddlBranch.SelectedItem.Value);//.BindGrid();

                GridView2.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView2.HeaderRow.Cells)
                {
                    cell.BackColor = GridView2.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView2.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView2.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView2.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

            }
        }
        protected void btnDownloadExcelCorpCompReport_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab2";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=CorpCompletenessReportExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView3.AllowPaging = false;
                DQIAfterBVNCompletenessPageDataBind(ddlCat2.SelectedItem.Value, ddlbranch2.SelectedItem.Value);//.BindGrid();

                GridView3.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView3.HeaderRow.Cells)
                {
                    cell.BackColor = GridView3.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView3.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView3.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView3.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView3.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        protected void btnDownloadExcelCorpCorrReport_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab2";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=CorpCorrectnessReportExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView4.AllowPaging = false;
                DQIAfterBVNCorrectnessPageDataBind(ddlCat2.SelectedItem.Value, ddlbranch2.SelectedItem.Value);//.BindGrid();

                GridView4.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView4.HeaderRow.Cells)
                {
                    cell.BackColor = GridView4.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView4.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView4.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView4.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView4.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        private void DQIBeforeBVNCompletenessPageDataBind(string Cat, string Branch)
        {
            hidTAB.Value = "#tab1";
            string cat = Cat;
            string branch = Branch;

           
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "pkg_cdms_dqi_INDIV_NEW.dqi_completeness";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.BindByName = true;

            try
            {
                con.Open();

                cmd.Parameters.Add("p_category", OracleDbType.Varchar2).Value = cat;
                cmd.Parameters.Add("p_branch", OracleDbType.Varchar2).Value = branch;

                cmd.Parameters.Add("cust", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

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
                lblMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message + " <br />"); //ex.StackTrace +
            }
            finally
            {
                con.Close();
                // con.Dispose();
            }
        }
        private void DQIBeforeBVNCorrectnessPageDataBind(string Cat, string Branch)
        {
            hidTAB.Value = "#tab1";
            string cat = Cat;
            string branch = Branch;

           
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "pkg_cdms_dqi_INDIV_NEW.dqi_validity";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.BindByName = true;

            try
            {
                con.Open();

                cmd.Parameters.Add("p_category", OracleDbType.Varchar2).Value = cat;
                cmd.Parameters.Add("p_branch", OracleDbType.Varchar2).Value = branch;

                cmd.Parameters.Add("cust", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

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
                    int columncount = GridView1.Rows[0].Cells.Count;
                    GridView2.Rows[0].Cells.Clear();
                    GridView2.Rows[0].Cells.Add(new TableCell());
                    GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView2.Rows[0].Cells[0].Text = "No Records Found";
                }


            }
            catch (Exception ex)
            {
                lblMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message + " <br />"); //ex.StackTrace +
            }
            finally
            {
                con.Close();
                // con.Dispose();
            }
        }

        private void DQIAfterBVNCompletenessPageDataBind(string Cat, string Branch)
        {
            hidTAB.Value = "#tab2";

            string cat = Cat;
            string branch = Branch;//ddlBranch.SelectedValue;

           

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "pkg_cdms_dqi_INDIV_NEW.dqi_completeness_bvn_branch";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.BindByName = true;

            try
            {
                con.Open();

                cmd.Parameters.Add("p_category", OracleDbType.Varchar2).Value = cat;
                cmd.Parameters.Add("p_branch", OracleDbType.Varchar2).Value = branch;

                cmd.Parameters.Add("cust", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView3.DataSource = ds;
                    GridView3.DataBind();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    GridView3.DataSource = ds;
                    GridView3.DataBind();
                    int columncount = GridView3.Rows[0].Cells.Count;
                    GridView3.Rows[0].Cells.Clear();
                    GridView3.Rows[0].Cells.Add(new TableCell());
                    GridView3.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView3.Rows[0].Cells[0].Text = "No Records Found";
                }


            }
            catch (Exception ex)
            {
                lblMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message + " <br />"); //ex.StackTrace +
            }
            finally
            {
                con.Close();
                // con.Dispose();
            }
        }


        private void DQIAfterBVNCorrectnessPageDataBind(string Cat, string Branch)
        {
            hidTAB.Value = "#tab2";

            string cat = Cat;
            string branch = Branch;//ddlBranch.SelectedValue;

          

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "pkg_cdms_dqi_INDIV_NEW.dqi_validity_bvn_branch";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.BindByName = true;

            try
            {
                con.Open();

               
                cmd.Parameters.Add("p_category", OracleDbType.Varchar2).Value = cat;
                cmd.Parameters.Add("p_branch", OracleDbType.Varchar2).Value = branch;


                cmd.Parameters.Add("cust", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView4.DataSource = ds;
                    GridView4.DataBind();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    GridView4.DataSource = ds;
                    GridView4.DataBind();
                    int columncount = GridView4.Rows[0].Cells.Count;
                    GridView4.Rows[0].Cells.Clear();
                    GridView4.Rows[0].Cells.Add(new TableCell());
                    GridView4.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView4.Rows[0].Cells[0].Text = "No Records Found";
                }


            }
            catch (Exception ex)
            {
                lblMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message + " <br />"); //ex.StackTrace +
            }
            finally
            {
                con.Close();
                // con.Dispose();
            }
        }


        private void DQIBE4BVNPageDataBind()
        {
            hidTAB.Value = "#tab3";
            //string cat = "INDIVIDUAL";
            //string branch = "001";
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "select table_category,table_name,column_name, sum(failed_pct) as Completeness_Failed_PCT,sum(VALIDITY_FAILED_PCT) as Correctness_Failed_PCT "+
            "from DQI_PROFILE_RESULT group by table_category,table_name,column_name order by table_category desc";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.BindByName = true;

            try
            {
                con.Open();

                //cmd.Parameters.Add("cust", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView5.DataSource = ds;
                    GridView5.DataBind();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    GridView5.DataSource = ds;
                    GridView5.DataBind();
                    int columncount = GridView5.Rows[0].Cells.Count;
                    GridView5.Rows[0].Cells.Clear();
                    GridView5.Rows[0].Cells.Add(new TableCell());
                    GridView5.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView5.Rows[0].Cells[0].Text = "No Records Found";
                }


            }
            catch (Exception ex)
            {
                lblMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message + " <br />"); //ex.StackTrace +
            }
            finally
            {
                con.Close();
                // con.Dispose();
            }
        }

        private void DQIAfterBVNPageDataBind()
        {
            hidTAB.Value = "#tab3";
            //string cat = "INDIVIDUAL";
            //string branch = "001";
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "select table_category,table_name,column_name, sum(failed_pct) as Completeness_Failed_PCT,sum(VALIDITY_FAILED_PCT)  as Correctness_Failed_PCT "+ 
                "from DQI_PROFILE_RESULT_BVN group by table_category,table_name,column_name order by table_category desc";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.BindByName = true;

            try
            {
                con.Open();

                //cmd.Parameters.Add("cust", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView6.DataSource = ds;
                    GridView6.DataBind();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    GridView6.DataSource = ds;
                    GridView6.DataBind();
                    int columncount = GridView6.Rows[0].Cells.Count;
                    GridView6.Rows[0].Cells.Clear();
                    GridView6.Rows[0].Cells.Add(new TableCell());
                    GridView6.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView6.Rows[0].Cells[0].Text = "No Records Found";
                }


            }
            catch (Exception ex)
            {
                lblMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message + " <br />"); //ex.StackTrace +
            }
            finally
            {
                con.Close();
                // con.Dispose();
            }
        }
        protected void btnDownloadExcelDQIBE4BVNReport_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=DQIBE4BVNReportExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView5.AllowPaging = false;
                DQIBE4BVNPageDataBind();//.BindGrid();

                GridView5.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView5.HeaderRow.Cells)
                {
                    cell.BackColor = GridView5.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView5.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView5.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView5.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView5.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        protected void btnDownloadExcelDQIAfterBVNReport_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=DQIAfterBVNReportExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView6.AllowPaging = false;
                DQIAfterBVNPageDataBind();//.BindGrid();

                GridView5.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView6.HeaderRow.Cells)
                {
                    cell.BackColor = GridView6.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView6.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView6.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView6.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView6.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            hidTAB.Value = "#tab1";
            GridView1.PageIndex = e.NewPageIndex;
            DQIBeforeBVNCompletenessPageDataBind(ddlCategory.SelectedItem.Value, ddlBranch.SelectedItem.Value);

        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            hidTAB.Value = "#tab1";

            GridView2.PageIndex = e.NewPageIndex;
            DQIBeforeBVNCorrectnessPageDataBind(ddlCategory.SelectedItem.Value, ddlBranch.SelectedItem.Value);


        }
        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            hidTAB.Value = "#tab2";

            GridView3.PageIndex = e.NewPageIndex;
            DQIAfterBVNCompletenessPageDataBind(ddlCat2.SelectedItem.Value, ddlbranch2.SelectedItem.Value);


        }

        protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            hidTAB.Value = "#tab2";

            GridView4.PageIndex = e.NewPageIndex;
            DQIAfterBVNCorrectnessPageDataBind(ddlCat2.SelectedItem.Value, ddlbranch2.SelectedItem.Value);

        }
        protected void GridView5_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            hidTAB.Value = "#tab3";

            GridView5.PageIndex = e.NewPageIndex;
            DQIBE4BVNPageDataBind();


        }

        protected void GridView6_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            hidTAB.Value = "#tab3";

            GridView6.PageIndex = e.NewPageIndex;
            DQIAfterBVNPageDataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DQIBeforeBVNCompletenessPageDataBind(ddlCategory.SelectedItem.Value, ddlBranch.SelectedItem.Value);
            DQIBeforeBVNCorrectnessPageDataBind(ddlCategory.SelectedItem.Value, ddlBranch.SelectedItem.Value);
        }

        protected void btnSearch2_Click(object sender, EventArgs e)
        {
            DQIAfterBVNCompletenessPageDataBind(ddlCat2.SelectedItem.Value, ddlbranch2.SelectedItem.Value);
            DQIAfterBVNCorrectnessPageDataBind(ddlCat2.SelectedItem.Value, ddlbranch2.SelectedItem.Value);
        }
    }
    
}

//private void BuzContactDetailPageDataBind()
//{
//    hidTAB.Value = "#tab2";
//    OracleCommand cmd = new OracleCommand();
//    cmd.Connection = con;
//    cmd.CommandText = "pkg_cdms.prc_business_contact_detail";
//    cmd.CommandType = System.Data.CommandType.StoredProcedure;
//    cmd.BindByName = true;

//    try
//    {
//        con.Open();

//        cmd.Parameters.Add("p_start_date", OracleDbType.Date).Value = txtBizCntFromDate.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtBizCntFromDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

//        cmd.Parameters.Add("p_end_date", OracleDbType.Date).Value = txtBizCntToDate.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtBizCntToDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

//        cmd.Parameters.Add("p_out", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

//        DataSet ds = new DataSet();
//        OracleDataAdapter da = new OracleDataAdapter();
//        da.SelectCommand = cmd;
//        da.Fill(ds);

//        if (ds.Tables[0].Rows.Count > 0)
//        {
//            GridView2.DataSource = ds;
//            GridView2.DataBind();
//        }
//        else
//        {
//            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
//            GridView2.DataSource = ds;
//            GridView2.DataBind();
//            int columncount = GridView2.Rows[0].Cells.Count;
//            GridView2.Rows[0].Cells.Clear();
//            GridView2.Rows[0].Cells.Add(new TableCell());
//            GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
//            GridView2.Rows[0].Cells[0].Text = "No Records Found";
//        }


//    }
//    catch (Exception ex)
//    {
//        lblBizCntMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message + " <br />"); //ex.StackTrace +
//    }
//    finally
//    {
//        con.Close();
//        con.Dispose();
//    }
//}


/////////////////////////////////////////////////////////////////////


//protected void btnExportCustInfoDetailReport_Click(object sender, EventArgs e)
//{
//    hidTAB.Value = "#tab3";
//    Response.Clear();
//    Response.Buffer = true;
//    Response.AddHeader("content-disposition", "attachment;filename=CustInfoDetailReportExport.xls");
//    Response.Charset = "";
//    Response.ContentType = "application/vnd.ms-excel";
//    using (StringWriter sw = new StringWriter())
//    {
//        HtmlTextWriter hw = new HtmlTextWriter(sw);

//        //To Export all pages
//        GridView3.AllowPaging = false;
//        CustInfoDetailPageDataBind();//.BindGrid();

//        GridView3.HeaderRow.BackColor = Color.White;
//        foreach (TableCell cell in GridView3.HeaderRow.Cells)
//        {
//            cell.BackColor = GridView3.HeaderStyle.BackColor;
//        }
//        foreach (GridViewRow row in GridView3.Rows)
//        {
//            row.BackColor = Color.White;
//            foreach (TableCell cell in row.Cells)
//            {
//                if (row.RowIndex % 2 == 0)
//                {
//                    cell.BackColor = GridView3.AlternatingRowStyle.BackColor;
//                }
//                else
//                {
//                    cell.BackColor = GridView3.RowStyle.BackColor;
//                }
//                cell.CssClass = "textmode";
//            }
//        }

//        GridView3.RenderControl(hw);

//        //style to format numbers to string
//        string style = @"<style> .textmode { } </style>";
//        Response.Write(style);
//        Response.Output.Write(sw.ToString());
//        Response.Flush();
//        Response.End();
//    }
//}


//protected void btnSearchBasicReport_Click(object sender, EventArgs e)
//{
//    hidTAB.Value = "#tab1";
//    BasicPageDataBind();

//}

//protected void btnExportBasicReport_Click(object sender, EventArgs e)
//{
//    hidTAB.Value = "#tab1";
//    Response.Clear();
//    Response.Buffer = true;
//    Response.AddHeader("content-disposition", "attachment;filename=BasicReportExport.xls");
//    Response.Charset = "";
//    Response.ContentType = "application/vnd.ms-excel";
//    using (StringWriter sw = new StringWriter())
//    {
//        HtmlTextWriter hw = new HtmlTextWriter(sw);

//        //To Export all pages
//        GridView1.AllowPaging = false;
//        BasicPageDataBind();//.BindGrid();

//        GridView1.HeaderRow.BackColor = Color.White;
//        foreach (TableCell cell in GridView1.HeaderRow.Cells)
//        {
//            cell.BackColor = GridView1.HeaderStyle.BackColor;
//        }
//        foreach (GridViewRow row in GridView1.Rows)
//        {
//            row.BackColor = Color.White;
//            foreach (TableCell cell in row.Cells)
//            {
//                if (row.RowIndex % 2 == 0)
//                {
//                    cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
//                }
//                else
//                {
//                    cell.BackColor = GridView1.RowStyle.BackColor;
//                }
//                cell.CssClass = "textmode";
//            }
//        }

//        GridView1.RenderControl(hw);

//        //style to format numbers to string
//        string style = @"<style> .textmode { } </style>";
//        Response.Write(style);
//        Response.Output.Write(sw.ToString());
//        Response.Flush();
//        Response.End();
//    }
//}
// }


////////////////////////////////////////////////////////


//protected void btnSearchBizContactDetailReport_Click(object sender, EventArgs e)
//{
//    hidTAB.Value = "#tab2";
//    BuzContactDetailPageDataBind();

//}

//protected void btnExportBizContactDetailReport_Click(object sender, EventArgs e)
//{
//    hidTAB.Value = "#tab2";
//    Response.Clear();
//    Response.Buffer = true;
//    Response.AddHeader("content-disposition", "attachment;filename=BizContactDetailReportExport.xls");
//    Response.Charset = "";
//    Response.ContentType = "application/vnd.ms-excel";
//    using (StringWriter sw = new StringWriter())
//    {
//        HtmlTextWriter hw = new HtmlTextWriter(sw);

//        //To Export all pages
//        GridView2.AllowPaging = false;
//        BuzContactDetailPageDataBind();//.BindGrid();

//        GridView2.HeaderRow.BackColor = Color.White;
//        foreach (TableCell cell in GridView2.HeaderRow.Cells)
//        {
//            cell.BackColor = GridView2.HeaderStyle.BackColor;
//        }
//        foreach (GridViewRow row in GridView2.Rows)
//        {
//            row.BackColor = Color.White;
//            foreach (TableCell cell in row.Cells)
//            {
//                if (row.RowIndex % 2 == 0)
//                {
//                    cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
//                }
//                else
//                {
//                    cell.BackColor = GridView2.RowStyle.BackColor;
//                }
//                cell.CssClass = "textmode";
//            }
//        }

//        GridView2.RenderControl(hw);

//        //style to format numbers to string
//        string style = @"<style> .textmode { } </style>";
//        Response.Write(style);
//        Response.Output.Write(sw.ToString());
//        Response.Flush();
//        Response.End();
//    }
//}

