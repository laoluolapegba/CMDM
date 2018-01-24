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
    public partial class BulkIndividualCustInfo : System.Web.UI.Page
    {
        private static string logs = "";
        private CustomerIndividualBulkUpload IndivObj;
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
            List<CustomerIndividualBulkUpload> BulkCustIndividualList = new List<CustomerIndividualBulkUpload>();

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


                        if (colCount > 71)
                        {
                            lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Too Many Columns");

                            return;
                        }
                        if (colCount < 71)
                        {

                            lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Lesser number of Columns");

                            return;
                        }


                        //CustomerIndividualBulkUpload Indivlist = new CustomerIndividualBulkUpload();

                        for (int i = 0; i < rowCnt; i++)
                        {
                            currRow = i;

                            IndivObj = new CustomerIndividualBulkUpload
                            {
                                CustomerId = dt.Rows[i][0].ToString(),
                                CustomerName = dt.Rows[i][1].ToString(),
                                MaidenName = dt.Rows[i][2].ToString(),
                                SocialSecurityNo = dt.Rows[i][3].ToString(),
                                TaxNo = dt.Rows[i][4].ToString(),
                                Title = dt.Rows[i][5].ToString(),
                                DateOfBirth = dt.Rows[i][6].ToString(),
                                PlaceOfBirth = dt.Rows[i][7].ToString(),
                                Religion = dt.Rows[i][8].ToString(),
                                StateOfOrigin = dt.Rows[i][9].ToString(),
                                Nationality = dt.Rows[i][10].ToString(),
                                Heigth = dt.Rows[i][11].ToString(),
                                Complextion = dt.Rows[i][12].ToString(),
                                EyeColor = dt.Rows[i][13].ToString(),
                                Disability = dt.Rows[i][14].ToString(),
                                Race = dt.Rows[i][15].ToString(),
                                DocumentType = dt.Rows[i][16].ToString(),
                                DocumentNo = dt.Rows[i][17].ToString(),
                                IssuingAuth = dt.Rows[i][18].ToString(),
                                IssuingCountry = dt.Rows[i][19].ToString(),
                                IssuingState = dt.Rows[i][20].ToString(),
                                IssuingDate = dt.Rows[i][21].ToString(),
                                ExpiryDate = dt.Rows[i][22].ToString(),
                                IssuingCity = dt.Rows[i][23].ToString(),
                                IncomeSource = dt.Rows[i][24].ToString(),
                                IncomeSegment = dt.Rows[i][25].ToString(),
                                IncomeBand = dt.Rows[i][26].ToString(),
                                AccountNumber = dt.Rows[i][27].ToString(),
                                Branch = dt.Rows[i][28].ToString(),
                                AccountType = dt.Rows[i][29].ToString(),
                                Currency = dt.Rows[i][30].ToString(),
                                BankCode = dt.Rows[i][31].ToString(),
                                BankName = dt.Rows[i][32].ToString(),
                                NOKFamilyRelationshipType = dt.Rows[i][33].ToString(),
                                NOKFullName = dt.Rows[i][34].ToString(),
                                NOKAddress = dt.Rows[i][35].ToString(),
                                NOKTelephoneNumber = dt.Rows[i][36].ToString(),
                                NOKDateOfBirth = dt.Rows[i][37].ToString(),
                                NOKEmploymentDetail = dt.Rows[i][38].ToString(),
                                NOKOccupation = dt.Rows[i][39].ToString(),
                                ArrivalDate = dt.Rows[i][40].ToString(),
                                DepartureDate = dt.Rows[i][41].ToString(),
                                VisaValidFrom = dt.Rows[i][42].ToString(),
                                VisaValidTill = dt.Rows[i][43].ToString(),
                                PassportNumber = dt.Rows[i][44].ToString(),
                                PassportIssueDate = dt.Rows[i][45].ToString(),
                                PassportExpiryDate = dt.Rows[i][46].ToString(),
                                PassportIssueCountry = dt.Rows[i][47].ToString(),
                                ResidentPermitNumber = dt.Rows[i][48].ToString(),
                                CaseNumber = dt.Rows[i][49].ToString(),
                                CaseDescription = dt.Rows[i][50].ToString(),
                                CaseDate = dt.Rows[i][51].ToString(),
                                PoliceSummation = dt.Rows[i][52].ToString(),
                                FamilyRelationship = dt.Rows[i][53].ToString(),
                                FamilyFullname = dt.Rows[i][54].ToString(),
                                FamilyAddress = dt.Rows[i][55].ToString(),
                                FamilyPhoneNumber = dt.Rows[i][56].ToString(),
                                FamilyDateOfBirth = dt.Rows[i][57].ToString(),
                                FamilyEmploymentDetail = dt.Rows[i][58].ToString(),
                                FamilyOccupation = dt.Rows[i][59].ToString(),
                                EmploymentType = dt.Rows[i][60].ToString(),
                                CurrentEmployerName = dt.Rows[i][61].ToString(),
                                CurrentEmployerAddress = dt.Rows[i][62].ToString(),
                                CurrentEmployerPhone = dt.Rows[i][63].ToString(),
                                CurrentEmployerPositionHeld = dt.Rows[i][64].ToString(),
                                EmploymentDate = dt.Rows[i][65].ToString(),
                                PreviousEmployerAddress = dt.Rows[i][66].ToString(),
                                PreviousEmployerName = dt.Rows[i][67].ToString(),
                                PrevEmployerPositionHeld = dt.Rows[i][68].ToString(),
                                TimeSpentWithPrevEmployer = dt.Rows[i][69].ToString(),
                                BusinessOccupation = dt.Rows[i][70].ToString(),


                            };
                            BulkCustIndividualList.Add(IndivObj);
                        }

                        //int cnt = 0;
                        //foreach (CustomerIndividualBulkUpload item in BulkCustIndividualList)
                        //{
                        totalRec += InsertExcelData(BulkCustIndividualList);
                        //    //UpdateRefData(item);

                        //}

                    }//end of else

                    if (totalRec > 0)
                    {
                        //lnkView.Visible = true;
                    }
                    lblMsg.Text = MessageFormatter.GetFormattedSuccessMessage(string.Format("Uploaded {0} records", totalRec));

                }//end of else
            }
            catch (Exception ex)
            {

                lblMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: " + ex.Message + " at row " + (currRow + 1));
            }

          
        }

        private int InsertExcelData(List<CustomerIndividualBulkUpload> BulkCustIndividualList)
        {
            int res = 0;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "pkg_cdms2.prc_individual_bulk_upload2";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.BindByName = true;
            int cnt = 0;
             try
            {
                con.Open();
                foreach (CustomerIndividualBulkUpload item in BulkCustIndividualList)
                {

                    //int userId = SessionHelper.UserId();
                    

                    cmd.Parameters.Add("p_customer_id", OracleDbType.Int32).Value = item.CustomerId.Trim();
                    cmd.Parameters.Add("p_maiden_name", OracleDbType.Varchar2).Value = item.MaidenName.Trim();
                    cmd.Parameters.Add("p_s_s_no", OracleDbType.Varchar2).Value = item.SocialSecurityNo.Trim();
                    cmd.Parameters.Add("p_tax_number", OracleDbType.Varchar2).Value = item.TaxNo.Trim();
                    cmd.Parameters.Add("p_title", OracleDbType.Varchar2).Value = item.Title.Trim();
                    cmd.Parameters.Add("p_date_of_birth", OracleDbType.Varchar2).Value = item.DateOfBirth.Trim();
                    cmd.Parameters.Add("p_place_of_birth", OracleDbType.Varchar2).Value = item.PlaceOfBirth.Trim();
                    cmd.Parameters.Add("p_religion", OracleDbType.Varchar2).Value = item.Religion.Trim();
                    cmd.Parameters.Add("p_state", OracleDbType.Varchar2).Value = item.StateOfOrigin.Trim();
                    cmd.Parameters.Add("p_nationality", OracleDbType.Varchar2).Value = item.Nationality.Trim();
                    cmd.Parameters.Add("p_height", OracleDbType.Varchar2).Value = item.Heigth.Trim();
                    cmd.Parameters.Add("p_complexion", OracleDbType.Varchar2).Value = item.Complextion.Trim();
                    cmd.Parameters.Add("p_eye_color", OracleDbType.Varchar2).Value = item.EyeColor.Trim();
                    cmd.Parameters.Add("p_disability", OracleDbType.Varchar2).Value = item.Disability.Trim();
                    cmd.Parameters.Add("p_race", OracleDbType.Varchar2).Value = item.Race.Trim();
                    cmd.Parameters.Add("p_document_type", OracleDbType.Varchar2).Value = item.DocumentType.Trim();
                    cmd.Parameters.Add("p_document_number", OracleDbType.Varchar2).Value = item.DocumentNo.Trim();
                    cmd.Parameters.Add("p_issuing_authority", OracleDbType.Varchar2).Value = item.IssuingAuth.Trim();
                    cmd.Parameters.Add("p_issuing_country", OracleDbType.Varchar2).Value = item.IssuingCountry.Trim();
                    cmd.Parameters.Add("p_issuing_state", OracleDbType.Varchar2).Value = item.IssuingState.Trim();
                    cmd.Parameters.Add("p_issued_date", OracleDbType.Varchar2).Value = item.IssuingDate.Trim();
                    cmd.Parameters.Add("p_expiry_date", OracleDbType.Varchar2).Value = item.ExpiryDate.Trim();
                    cmd.Parameters.Add("p_issuing_city", OracleDbType.Varchar2).Value = item.IssuingCity.Trim();
                    cmd.Parameters.Add("p_income_source", OracleDbType.Varchar2).Value = item.IncomeSource.Trim();
                    cmd.Parameters.Add("p_income_segment", OracleDbType.Varchar2).Value = item.IncomeSegment.Trim();
                    cmd.Parameters.Add("p_income_band", OracleDbType.Varchar2).Value = item.IncomeBand.Trim();
                    cmd.Parameters.Add("p_account_number", OracleDbType.Varchar2).Value = item.AccountNumber.Trim();
                    cmd.Parameters.Add("p_branch", OracleDbType.Varchar2).Value = item.Branch.Trim();
                    cmd.Parameters.Add("p_account_type", OracleDbType.Varchar2).Value = item.AccountType.Trim();
                    cmd.Parameters.Add("p_currency", OracleDbType.Varchar2).Value = item.Currency.Trim();
                    cmd.Parameters.Add("p_bank_code", OracleDbType.Varchar2).Value = item.BankCode.Trim();
                    cmd.Parameters.Add("p_bank_name", OracleDbType.Varchar2).Value = item.BankName.Trim();
                    cmd.Parameters.Add("p_family_type", OracleDbType.Varchar2).Value = item.NOKFamilyRelationshipType.Trim();
                    cmd.Parameters.Add("p_ful_name ", OracleDbType.Varchar2).Value = item.NOKFullName.Trim();
                    cmd.Parameters.Add("p_address", OracleDbType.Varchar2).Value = item.NOKAddress.Trim();
                    cmd.Parameters.Add("p_telephone_number", OracleDbType.Varchar2).Value = item.NOKTelephoneNumber.Trim();
                    cmd.Parameters.Add("p_date_birth", OracleDbType.Varchar2).Value = item.NOKDateOfBirth.Trim();
                    cmd.Parameters.Add("p_employment_detail", OracleDbType.Varchar2).Value = item.NOKEmploymentDetail.Trim();
                    cmd.Parameters.Add("p_occupation", OracleDbType.Varchar2).Value = item.NOKOccupation.Trim();
                    cmd.Parameters.Add("p_arrival_date", OracleDbType.Varchar2).Value = item.ArrivalDate.Trim();
                    cmd.Parameters.Add("p_departure_date", OracleDbType.Varchar2).Value = item.DepartureDate.Trim();
                    cmd.Parameters.Add("p_valid_from", OracleDbType.Varchar2).Value = item.VisaValidFrom.Trim();
                    cmd.Parameters.Add("p_valid_till", OracleDbType.Varchar2).Value = item.VisaValidTill.Trim();
                    cmd.Parameters.Add("p_passport_number", OracleDbType.Varchar2).Value = item.PassportNumber.Trim();
                    cmd.Parameters.Add("p_passport_issue_date", OracleDbType.Varchar2).Value = item.PassportIssueDate.Trim();
                    cmd.Parameters.Add("p_passport_expiry_date", OracleDbType.Varchar2).Value = item.PassportExpiryDate.Trim();
                    cmd.Parameters.Add("p_issue_country", OracleDbType.Varchar2).Value = item.IssuingCountry.Trim();
                    cmd.Parameters.Add("p_permit_number", OracleDbType.Varchar2).Value = item.ResidentPermitNumber.Trim();
                    cmd.Parameters.Add("p_case_number", OracleDbType.Varchar2).Value = item.CaseNumber.Trim();
                    cmd.Parameters.Add("p_case_description", OracleDbType.Varchar2).Value = item.CaseDescription.Trim();//HttpContext.Current.User.Identity;
                    cmd.Parameters.Add("p_case_date", OracleDbType.Varchar2).Value = item.CaseDate.Trim();
                    cmd.Parameters.Add("p_police_summation", OracleDbType.Varchar2).Value = item.PoliceSummation.Trim();
                    cmd.Parameters.Add("p_family_relationship_type", OracleDbType.Varchar2).Value = item.FamilyRelationship.Trim();
                    cmd.Parameters.Add("p_family_full_name", OracleDbType.Varchar2).Value = item.FamilyFullname.Trim();
                    cmd.Parameters.Add("p_family_address", OracleDbType.Varchar2).Value = item.FamilyAddress.Trim();
                    cmd.Parameters.Add("p_family_telephone_number", OracleDbType.Varchar2).Value = item.FamilyPhoneNumber.Trim();
                    cmd.Parameters.Add("p_family_date_birth", OracleDbType.Varchar2).Value = item.FamilyDateOfBirth.Trim();
                    cmd.Parameters.Add("p_family_employment_detail", OracleDbType.Varchar2).Value = item.FamilyEmploymentDetail.Trim();
                    cmd.Parameters.Add("p_family_occupation", OracleDbType.Varchar2).Value = item.FamilyOccupation.Trim();
                    cmd.Parameters.Add("p_employment_type", OracleDbType.Varchar2).Value = item.EmploymentType.Trim();
                    cmd.Parameters.Add("p_current_employer_name", OracleDbType.Varchar2).Value = item.CurrentEmployerName.Trim();
                    cmd.Parameters.Add("p_current_employer_address", OracleDbType.Varchar2).Value = item.CurrentEmployerAddress.Trim();
                    cmd.Parameters.Add("p_current_employer_phone", OracleDbType.Varchar2).Value = item.CurrentEmployerPhone.Trim();
                    cmd.Parameters.Add("p_current_emp_pos_held", OracleDbType.Varchar2).Value = item.CurrentEmployerPositionHeld.Trim();
                    cmd.Parameters.Add("p_employment_date", OracleDbType.Varchar2).Value = item.EmploymentDate.Trim();
                    cmd.Parameters.Add("p_previous_employer_address", OracleDbType.Varchar2).Value = item.PreviousEmployerAddress.Trim();
                    cmd.Parameters.Add("p_previous_employer_name", OracleDbType.Varchar2).Value = item.PreviousEmployerName.Trim();
                    cmd.Parameters.Add("p_prev_employer_position_held", OracleDbType.Varchar2).Value = item.PrevEmployerPositionHeld.Trim();
                    cmd.Parameters.Add("p_time_spent_wiv_prev_emp", OracleDbType.Varchar2).Value = item.TimeSpentWithPrevEmployer.Trim();
                    cmd.Parameters.Add("p_business_occupation", OracleDbType.Varchar2).Value = item.BusinessOccupation.Trim();
                    cmd.Parameters.Add("p_created_by", OracleDbType.Varchar2).Value = User.Identity.Name;
                    // cmd.Parameters.Add("p_modified_by", OracleDbType.Varchar2).Value = Profile.UserName.Trim();
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