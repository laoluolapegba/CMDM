using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Oracle.DataAccess.Client;
using Oracle;
using System.IO;
using Excel;
using CMdm.UI.Web.BLL;

namespace Cdma.Web.CustomerInfo
{
    public partial class BulkBasicCustInfo : System.Web.UI.Page
    {
        private static string logs = "";
        private Customer custObj;
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        // public OracleCommand cmd = new OracleCommand();
        public OracleDataReader rdr;
        public OracleDataAdapter adapter = new OracleDataAdapter();

        //
        //public NewWebRef.Service1 rtlWS = new NewWebRef.Service1();
        //

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void btnBulkCustInfoUpload_Click(object sender, EventArgs e)
        {
            List<Customer> BasicCustList = new List<Customer>();

            int totalRec = 0;
            logs = "";
            FileStream stream;
            IExcelDataReader excelReader = null;
            System.Data.DataTable dt = null;
            DataSet ds;

            int currRow = 0;
            try
            {

                if (fUpload.FileName == string.Empty)
                {
                    lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: " + "No file selected for Bulk Upload");
                    return;
                }
                else
                {
                    if (fUpload.HasFile)
                    {
                        string v_file_name = fUpload.FileName; //FileName
                        string v_file_path = Server.MapPath(v_file_name); //File Path

                        string v_ext = Path.GetExtension(v_file_path);//v_file_name.Substring(fUpload.FileName.LastIndexOf("."), 4).ToLower(); // File Extension

                        fUpload.SaveAs(v_file_path);
                        stream = File.Open(v_file_path, FileMode.Open, FileAccess.ReadWrite);

                        if (v_ext == ".xls")
                        {
                            excelReader = ExcelReaderFactory.CreateBinaryReader(stream); // For Excel 2003
                        }
                        else if (v_ext == ".xlsx")
                        {
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream); // For Excel 2007 to 2010
                        }
                        else
                        {
                            lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid file type");
                            return;
                        }

                        // Delete the file after upload
                       // File.Delete(v_file_path);

                        excelReader.IsFirstRowAsColumnNames = true; // Specify if there is a header in the Excel
                        ds = excelReader.AsDataSet(); // Fill the dataset with the excel data
                        dt = ds.Tables[0]; // FIll the datatable

                        excelReader.Close();
                        excelReader.Dispose();

                        int rowCnt = dt.Rows.Count;
                        int colCount = dt.Columns.Count;
                        // int count = 0;

                        //return;


                        if (colCount > 26)
                        {
                            lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Too Many Columns");
                        
                            return;
                        }
                        if (colCount < 26)
                        {

                            lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Lesser number of Columns");
                            
                            return;
                        }


                        //Customer clist = new Customer();
                        for (int i = 0; i < rowCnt; i++)
                        {
                             currRow = i;

                             custObj = new Customer
                            {
                            
                               CustomerId = dt.Rows[i][0].ToString(), //CustomerId
                               CustomerName = dt.Rows[i][1].ToString(),// CustomerName
                               SecretQuestion = dt.Rows[i][2].ToString(), // SecretQuestion
                               SecretQuestionAnswer = dt.Rows[i][3].ToString(), // SecretQuestionAnswer
                               EmailAddress2 = dt.Rows[i][4].ToString(), // EmailAddress2
                               WebAddress = dt.Rows[i][5].ToString(), //WebAddress
                               AnniversaryDate1 =  dt.Rows[i][6].ToString(), // AnniversaryDate1
                               AnniversaryType2 =  dt.Rows[i][7].ToString(), // AnniversaryType2
                               AnniversaryDate2 = dt.Rows[i][8].ToString(), //AnniversaryDate2
                               AnniversaryType3 = dt.Rows[i][9].ToString(),// AnniversaryType3
                               AnniversaryDate3 = dt.Rows[i][10].ToString(), // AnniversaryDate3
                               AnniversaryType4 = dt.Rows[i][11].ToString(), // AnniversaryType4
                               AnniversaryDate4 = dt.Rows[i][12].ToString(), // AnniversaryDate4
                               AnniversaryType5 = dt.Rows[i][13].ToString(), //AnniversaryType5
                               AnniversaryDate5 = dt.Rows[i][14].ToString(),// AnniversaryDate5
                               AnniversaryType6 = dt.Rows[i][15].ToString(), // AnniversaryType6
                               AnniversaryDate6 = dt.Rows[i][16].ToString(), // AnniversaryDate6
                               AnniversaryType7 = dt.Rows[i][17].ToString(), // AnniversaryType7
                               AnniversaryDate7 = dt.Rows[i][18].ToString(), // AnniversaryDate7
                               AdministrativeArea = dt.Rows[i][19].ToString(), // adminArea
                               Locality = dt.Rows[i][20].ToString(),// locality
                               LocationCoordinates = dt.Rows[i][21].ToString(), // locationCoordinates
                               PostCode =  dt.Rows[i][22].ToString(), // postCode
                               PostOfficeBox = dt.Rows[i][23].ToString(), // postOfficeBox
                               AreaCode = dt.Rows[i][24].ToString(),// areaCode
                               ExtensionNo = dt.Rows[i][25].ToString()// extensionNo
                                
                        };

                             BasicCustList.Add(custObj);

                        }

                        totalRec += InsertExcelData(BasicCustList);

                        // int cnt = 0;
                        //foreach (Customer item in BasicCustList)
                        //{
                        //    totalRec += InsertExcelData(item);
                        //    //UpdateRefData(item);
                          
                        //}

                }//end of else

                    if (totalRec > 0)
                    { 
                        lblMsg.Text = MessageFormatter.GetFormattedSuccessMessage(string.Format("Uploaded {0} records", totalRec));
                    
                        //lnkView.Visible = true;
                    }
                   
                }//end of else
            }
            catch (Exception ex)
            {
                
                lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: " + ex.Message + " at row " + (currRow + 1));
            }

            //   }
        }

        private int InsertExcelData(List<Customer> BasicCustList)
        {

            
            int res = 0;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "pkg_cdms2.prc_basic_cust_det_bulk_upload";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.BindByName = true;
            //
            int cnt = 0;
            try
            {
                con.Open();
                
                foreach (Customer item in BasicCustList)
                {

                //int userId = SessionHelper.UserId();
                con.Open();

                cmd.Parameters.Add("p_customer_id", OracleDbType.Int32).Value = Convert.ToInt32(item.CustomerId);
                cmd.Parameters.Add("p_secret_question", OracleDbType.Varchar2).Value = item.SecretQuestion.Trim();
                cmd.Parameters.Add("p_secret_question_answer", OracleDbType.Varchar2).Value = item.SecretQuestionAnswer.Trim();
                cmd.Parameters.Add("p_email_address_1_1", OracleDbType.Varchar2).Value = item.EmailAddress2.ToLower().Trim();
                cmd.Parameters.Add("p_web_address", OracleDbType.Varchar2).Value = item.WebAddress.ToLower().Trim();
                cmd.Parameters.Add("p_anniversary_date1", OracleDbType.Varchar2).Value = item.AnniversaryDate1.Trim();
                cmd.Parameters.Add("p_anniversary_type2", OracleDbType.Varchar2).Value = item.AnniversaryType2.Trim();
                cmd.Parameters.Add("p_anniversary_date2", OracleDbType.Varchar2).Value = item.AnniversaryDate2.Trim();
                cmd.Parameters.Add("p_anniversary_type3", OracleDbType.Varchar2).Value = item.AnniversaryType3.Trim();
                cmd.Parameters.Add("p_anniversary_date3", OracleDbType.Varchar2).Value = item.AnniversaryDate3.Trim();
                cmd.Parameters.Add("p_anniversary_type4", OracleDbType.Varchar2).Value = item.AnniversaryType4.Trim();
                cmd.Parameters.Add("p_anniversary_date4", OracleDbType.Varchar2).Value = item.AnniversaryDate4.Trim();
                cmd.Parameters.Add("p_anniversary_type5", OracleDbType.Varchar2).Value = item.AnniversaryType5.Trim();
                cmd.Parameters.Add("p_anniversary_date5", OracleDbType.Varchar2).Value = item.AnniversaryDate5.Trim();
                cmd.Parameters.Add("p_anniversary_type6", OracleDbType.Varchar2).Value = item.AnniversaryType6.Trim();
                cmd.Parameters.Add("p_anniversary_date6", OracleDbType.Varchar2).Value = item.AnniversaryDate6.Trim();
                cmd.Parameters.Add("p_anniversary_type7", OracleDbType.Varchar2).Value = item.AnniversaryType7.Trim();
                cmd.Parameters.Add("p_anniversary_date7", OracleDbType.Varchar2).Value = item.AnniversaryDate7.Trim();
                cmd.Parameters.Add("p_created_by", OracleDbType.Varchar2).Value = HttpContext.Current.User.Identity;
                cmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";
                cmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = HttpContext.Current.Request.UserHostAddress;
                cmd.Parameters.Add("p_administrative_area", OracleDbType.Varchar2).Value = item.AdministrativeArea.Trim();
                cmd.Parameters.Add("p_locality", OracleDbType.Varchar2).Value = item.Locality.Trim();
                cmd.Parameters.Add("p_location_coordinates", OracleDbType.Varchar2).Value = item.LocationCoordinates.Trim();
                cmd.Parameters.Add("p_post_code", OracleDbType.Varchar2).Value = item.PostCode.Trim();
                cmd.Parameters.Add("p_post_office_box", OracleDbType.Varchar2).Value = item.PostOfficeBox.Trim();
                cmd.Parameters.Add("p_area_code", OracleDbType.Varchar2).Value = item.AreaCode.Trim();
                cmd.Parameters.Add("p_extension_no", OracleDbType.Varchar2).Value = item.ExtensionNo.Trim();
                res = cmd.ExecuteNonQuery();
                //cmd.Connection = con;
                cmd.Parameters.Clear();
                ++cnt;

                }

            }
            catch (Exception ex)
            {
               
                lblMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message + " <br />");
            }
            finally
            {
                con.Close();
            }
            return cnt;
           // return res;

        }

      

    }
}