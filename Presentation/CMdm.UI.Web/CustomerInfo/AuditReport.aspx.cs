using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using ClosedXML.Excel;
using System.Drawing;
using System.Configuration;

namespace Cdma.Web.CustomerInfo
{
    public partial class AuditReport : System.Web.UI.Page
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        public OracleCommand cmd = new OracleCommand();
        public OracleDataReader rdr;
        public OracleDataAdapter adapter = new OracleDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }
        //protected void btnDownloadExcel_Click(object sender, EventArgs e)
        //{
        //    {
        //        using (OracleConnection conn = new OracleConnection(constr))
        //        {
        //            using (OracleCommand cmd = new OracleCommand("SELECT GL_CODE,GL_DESC FROM T_OPEX_SPACE_GL"))
        //            {
        //                using (OracleDataAdapter oda = new OracleDataAdapter())
        //                {
        //                    cmd.Connection = conn;
        //                    oda.SelectCommand = cmd;

        //                    using (DataTable dt = new DataTable())
        //                    {
        //                        oda.Fill(dt);
        //                        using (XLWorkbook wb = new XLWorkbook())
        //                        {
        //                            wb.Worksheets.Add(dt, "T_OPEX_SPACE_GL");
        //                            Response.Clear();
        //                            Response.Buffer = true;
        //                            Response.Charset = "";
        //                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //                            Response.AddHeader("content-disposition", "attachment;filename=PtlOpexSpaceGL.xlsx");
        //                            using (MemoryStream MyMemoryStream = new MemoryStream())
        //                            {
        //                                wb.SaveAs(MyMemoryStream);
        //                                MyMemoryStream.WriteTo(Response.OutputStream);
        //                                Response.Flush();
        //                                Response.End();
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}


        protected void btnDownloadExcelAudittrail_Click(object sender, EventArgs e)
        {
            //hidTAB.Value = "#tab3";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=AudittrailReportExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                //CustInfoDetailPageDataBind();//.BindGrid();
                GridView1.DataBind(); 
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
    }
}