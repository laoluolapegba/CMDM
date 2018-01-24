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
    public partial class BulkCorpCustInfo : System.Web.UI.Page
    {
        private static string logs = "";
        private CustomerCorpBulkUpload Corplist;
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
            List<CustomerCorpBulkUpload> BulkCustCorpList = new List<CustomerCorpBulkUpload>();

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


                        if (colCount > 54)
                        {
                            lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Too Many Columns");

                            return;
                        }
                        if (colCount < 54)
                        {

                            lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Lesser number of Columns");

                            return;
                        }


                        //CustomerCorpBulkUpload Corplist = new CustomerCorpBulkUpload();

                        for (int i = 0; i < rowCnt; i++)
                        {
                            currRow = i;

                            Corplist = new CustomerCorpBulkUpload
                            {
                                CustomerId = dt.Rows[i][0].ToString(),
                                CustomerName = dt.Rows[i][1].ToString(),
                                TaxIdentificationNumber = dt.Rows[i][2].ToString(),
                                RcNumber = dt.Rows[i][3].ToString(),
                                BusinessName = dt.Rows[i][4].ToString(),
                                BusinessType = dt.Rows[i][5].ToString(),
                                IncorporationState = dt.Rows[i][6].ToString(),
                                IncorporationCountry = dt.Rows[i][7].ToString(),
                                IncorporationDate = dt.Rows[i][8].ToString(),
                                CbnIsicCategorization = dt.Rows[i][9].ToString(),
                                DirectorName = dt.Rows[i][10].ToString(),
                                CompanyId = dt.Rows[i][11].ToString(),
                                FirstName = dt.Rows[i][12].ToString(),
                                MiddleName = dt.Rows[i][13].ToString(),
                                LastName = dt.Rows[i][14].ToString(),
                                DateOfBirth = dt.Rows[i][15].ToString(),
                                Gender = dt.Rows[i][16].ToString(),
                                DocumentType = dt.Rows[i][17].ToString(),
                                DocumentNo = dt.Rows[i][18].ToString(),
                                IssuingAuth = dt.Rows[i][19].ToString(),
                                IssuingCountry = dt.Rows[i][20].ToString(),
                                IssuingState = dt.Rows[i][21].ToString(),
                                IssuingCity = dt.Rows[i][22].ToString(),
                                IssuingDate = dt.Rows[i][23].ToString(),
                                ExpiryDate = dt.Rows[i][24].ToString(),
                                PoliticallyExposedPerson = dt.Rows[i][25].ToString(),
                                FinanciallyExposedPerson = dt.Rows[i][26].ToString(),
                                PreferredMeanOfCommunication = dt.Rows[i][27].ToString(),
                                CompanyName = dt.Rows[i][28].ToString(),
                                Signatory = dt.Rows[i][29].ToString(),
                                EmailAddress1 = dt.Rows[i][30].ToString(),
                                EmailAddress2 = dt.Rows[i][31].ToString(),
                                AddressType = dt.Rows[i][32].ToString(),
                                HouseIdentifier = dt.Rows[i][33].ToString(),
                                AddressLine1 = dt.Rows[i][34].ToString(),
                                AddressLine2 = dt.Rows[i][35].ToString(),
                                AdministrativeArea = dt.Rows[i][36].ToString(),
                                Locality = dt.Rows[i][37].ToString(),
                                LocationCoordinates = dt.Rows[i][38].ToString(),
                                PostCode = dt.Rows[i][39].ToString(),
                                PostOfficeBox = dt.Rows[i][40].ToString(),
                                Country = dt.Rows[i][41].ToString(),
                                State = dt.Rows[i][42].ToString(),
                                City = dt.Rows[i][43].ToString(),
                                PhoneCategory = dt.Rows[i][44].ToString(),
                                AreaCode = dt.Rows[i][45].ToString(),
                                CountryCode = dt.Rows[i][46].ToString(),
                                PhoneNumber = dt.Rows[i][47].ToString(),
                                ExtensionNo = dt.Rows[i][48].ToString(),
                                PhoneType = dt.Rows[i][49].ToString(),
                                ChannelSupported = dt.Rows[i][50].ToString(),
                                ReachableHour = dt.Rows[i][51].ToString(),
                                InmcomeBand = dt.Rows[i][52].ToString(),
                                InmcomeSegment = dt.Rows[i][53].ToString(),
                            };
                            BulkCustCorpList.Add(Corplist);
                        }

                        //int cnt = 0;
                        //foreach (CustomerCorpBulkUpload item in BulkCustCorpList)
                        //{
                        totalRec += InsertExcelData(BulkCustCorpList);
                            //UpdateRefData(item);

                        //}

                    }//end of else

                    if (totalRec > 0)
                    {
                        //lnkView.Visible = true;
                        lblMsg.Text = MessageFormatter.GetFormattedSuccessMessage(string.Format("Uploaded {0} records", totalRec));
                    }
                    

                }//end of else
            }
            catch (Exception ex)
            {

                lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: " + ex.Message + " at row " + (currRow + 1));
            }


        }

        private int InsertExcelData(List<CustomerCorpBulkUpload> BulkCustCorpList)
        {
            int res = 0;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "pkg_cdms2.prc_corp_customer_bulk_up_new";//prc_corp_customer_bulk_upload";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.BindByName = true;
            //
            int cnt = 0;
            try
            {
                con.Open();

                foreach (CustomerCorpBulkUpload item in BulkCustCorpList)
                {
                    //int userId = SessionHelper.UserId();
                    

                    cmd.Parameters.Add("p_customer_id", OracleDbType.Int32).Value = Convert.ToInt32(item.CustomerId);
                    cmd.Parameters.Add("p_tax_identification_number", OracleDbType.Varchar2).Value = item.TaxIdentificationNumber.Trim();
                    cmd.Parameters.Add("p_rc_number", OracleDbType.Varchar2).Value = item.RcNumber.Trim();
                    cmd.Parameters.Add("p_business_name", OracleDbType.Varchar2).Value = item.BusinessName.Trim();
                    cmd.Parameters.Add("p_business_type", OracleDbType.Varchar2).Value = item.BusinessType.Trim();
                    cmd.Parameters.Add("p_incorporation_state", OracleDbType.Varchar2).Value = item.IncorporationState.Trim();
                    cmd.Parameters.Add("p_incorporation_country", OracleDbType.Varchar2).Value = item.IncorporationCountry.Trim();
                    cmd.Parameters.Add("p_incorporation_date", OracleDbType.Varchar2).Value = item.IncorporationDate.Trim();
                    cmd.Parameters.Add("p_cbn_isic_categorization", OracleDbType.Varchar2).Value = item.CbnIsicCategorization.Trim();
                    cmd.Parameters.Add("p_director_name", OracleDbType.Varchar2).Value = item.DirectorName.Trim();
                    cmd.Parameters.Add("p_companyid", OracleDbType.Varchar2).Value = item.CompanyId.Trim();
                    cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = item.FirstName.Trim();
                    cmd.Parameters.Add("p_middle_name", OracleDbType.Varchar2).Value = item.MiddleName.Trim();
                    cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = item.LastName.Trim();
                    cmd.Parameters.Add("p_date_of_birth", OracleDbType.Varchar2).Value = item.DateOfBirth.Trim();
                    cmd.Parameters.Add("p_gender", OracleDbType.Varchar2).Value = item.Gender.Trim();
                    cmd.Parameters.Add("p_document_type", OracleDbType.Varchar2).Value = item.DocumentType.Trim();
                    cmd.Parameters.Add("p_document_number", OracleDbType.Varchar2).Value = item.DocumentNo.Trim();
                    cmd.Parameters.Add("p_issuing_authority", OracleDbType.Varchar2).Value = item.IssuingAuth.Trim();
                    cmd.Parameters.Add("p_issuing_country", OracleDbType.Varchar2).Value = item.IssuingCountry.Trim();
                    cmd.Parameters.Add("p_issuing_state", OracleDbType.Varchar2).Value = item.IssuingState.Trim();
                    cmd.Parameters.Add("p_issuing_date", OracleDbType.Varchar2).Value = item.IssuingDate.Trim();
                    cmd.Parameters.Add("p_issuing_city", OracleDbType.Varchar2).Value = item.IssuingCity.Trim();

                    cmd.Parameters.Add("p_expiry_date", OracleDbType.Varchar2).Value = item.ExpiryDate.Trim();
                    cmd.Parameters.Add("p_politically_exposed_person", OracleDbType.Varchar2).Value = item.PoliticallyExposedPerson.Trim();
                    cmd.Parameters.Add("p_financially_exposed_person", OracleDbType.Varchar2).Value = item.FinanciallyExposedPerson.Trim();
                    cmd.Parameters.Add("p_preferred_means_com", OracleDbType.Varchar2).Value = item.PreferredMeanOfCommunication.Trim();
                    cmd.Parameters.Add("p_company_name", OracleDbType.Varchar2).Value = item.CompanyName.Trim();
                    cmd.Parameters.Add("p_signatory", OracleDbType.Varchar2).Value = item.Signatory.Trim();
                    cmd.Parameters.Add("p_email_address_1", OracleDbType.Varchar2).Value = item.EmailAddress1.Trim();
                    cmd.Parameters.Add("p_email_address_2", OracleDbType.Varchar2).Value = item.EmailAddress2.Trim();
                    cmd.Parameters.Add("p_address_type", OracleDbType.Varchar2).Value = item.AddressType.Trim();
                    cmd.Parameters.Add("p_house_identifier ", OracleDbType.Varchar2).Value = item.HouseIdentifier.Trim();
                    cmd.Parameters.Add("p_address_line_1", OracleDbType.Varchar2).Value = item.AddressLine1.Trim();
                    cmd.Parameters.Add("p_address_line_2", OracleDbType.Varchar2).Value = item.AddressLine2.Trim();
                    cmd.Parameters.Add("p_administrative_area", OracleDbType.Varchar2).Value = item.AdministrativeArea.Trim();
                    cmd.Parameters.Add("p_locality", OracleDbType.Varchar2).Value = item.Locality.Trim();
                    cmd.Parameters.Add("p_location_coordinates", OracleDbType.Varchar2).Value = item.LocationCoordinates.Trim();
                    cmd.Parameters.Add("p_post_code", OracleDbType.Varchar2).Value = item.PostCode.Trim();
                    cmd.Parameters.Add("p_po_box", OracleDbType.Varchar2).Value = item.PostOfficeBox.Trim();
                    cmd.Parameters.Add("p_country", OracleDbType.Varchar2).Value = item.Country.Trim();
                    cmd.Parameters.Add("p_state", OracleDbType.Varchar2).Value = item.State.Trim();
                    cmd.Parameters.Add("p_city", OracleDbType.Varchar2).Value = item.City.Trim();
                    cmd.Parameters.Add("p_phone_category", OracleDbType.Varchar2).Value = item.PhoneCategory.Trim();
                    cmd.Parameters.Add("p_area_code", OracleDbType.Varchar2).Value = item.AreaCode.Trim();
                    cmd.Parameters.Add("p_country_code", OracleDbType.Varchar2).Value = item.CountryCode.Trim();
                    cmd.Parameters.Add("p_phone_number", OracleDbType.Varchar2).Value = item.PhoneNumber.Trim();
                    cmd.Parameters.Add("p_extension_number", OracleDbType.Varchar2).Value = item.ExtensionNo.Trim();
                    cmd.Parameters.Add("p_phone_type", OracleDbType.Varchar2).Value = item.PhoneType.Trim();
                    cmd.Parameters.Add("p_channel_supported", OracleDbType.Varchar2).Value = item.ChannelSupported.Trim();//HttpContext.Current.User.Identity;
                    cmd.Parameters.Add("p_reachable_hour", OracleDbType.Varchar2).Value = item.ReachableHour.Trim();
                    cmd.Parameters.Add("p_turnover", OracleDbType.Varchar2).Value = item.InmcomeSegment.Trim();
                    cmd.Parameters.Add("p_band_range", OracleDbType.Varchar2).Value = item.InmcomeBand.Trim();
                    cmd.Parameters.Add("p_created_by", OracleDbType.Varchar2).Value = User.Identity.Name;
                    cmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = HttpContext.Current.Request.UserHostAddress;
                    cmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";
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

        }

    }
}