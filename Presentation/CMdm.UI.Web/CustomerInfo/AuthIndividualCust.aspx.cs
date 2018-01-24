using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Linq;
//
using System.Linq.Expressions;
using System.Reflection;
//using System.Data.OracleClient;
using Oracle.DataAccess.Client;
using System.Drawing;
using CMdm.UI.Web.BLL;
using System.Globalization;
//using System.Net.Mail;
//using System.Text;
//using System.Net;
//using Cdma.Web.Properties;
//using System.Security.Cryptography.X509Certificates;
//using System.Net.Security;


namespace Cdma.Web.CustomerInfo
{
    public partial class AuthIndividualCust : System.Web.UI.Page
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        public OracleCommand cmd = new OracleCommand();
        public OracleDataReader rdr;
        public OracleDataAdapter adapter = new OracleDataAdapter();
        private Audit audit = new Audit();
        private CustomerRepository customer;
        private UtilityClass utility;

        protected void Page_Load(object sender, EventArgs e)
        {
            string CurrentUser = (String)(Session["UserID"]);

            this.txtCurrentUser.Text = CurrentUser;
        }

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    hidTAB.Value = "#tab1";
        //    //lblstatus.Text = "";
        //    //callClearBOx(); 



        //    OracleCommand objCmd = new OracleCommand();

        //    objCmd.Connection = con;

        //    objCmd.CommandText = "pkg_cdms_checker.get_indiv_details";

        //    objCmd.CommandType = CommandType.StoredProcedure;
        //    objCmd.Parameters.Add("customer_no", OracleDbType.Varchar2).Value = this.txtCustInfoID.Text.Trim();
        //    //
        //    objCmd.Parameters.Add("custinfo", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        //    //
        //    //int created = 0; 
        //    if (con.State == ConnectionState.Closed)
        //        con.Open();//



        //    try
        //    {
        //        OracleDataReader reader;
        //        reader = objCmd.ExecuteReader();

        //        if (reader.HasRows)
        //        {

        //            while (reader.Read())
        //            {
        //                //txtCustInfoID.Text = reader["customer_id"].ToString();

        //                //if (txtCustInfoID.Text != reader["customer_no"].ToString())
        //                //{//txtCustInfoID.Text != reader["customer_id"].ToString())txtCustInfoID.Text == null
        //                //    //txtCustInfoID.Text != reader["customer_id"].ToString()
        //                //    //reader.Close();

        //                //    lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error: Record not found");

        //                //    //Response.Redirect("/CDMA/CustomerInfo/IndividualCustomer.aspx"); 
        //                //    //this.txtSurname.Text = "";

        //                //    //return;
        //                //}

        //                //else
        //                //{

        //                //
        //                //txtCustInfoID.Text = txtschKin.Text = txtshAccOtherBk.Text = txtshEmplinfo.Text = txtshfamily.Text = txtshForeigner.Text = txtsearchIncome.Text = txtshLegal.Text = reader["customer_id"].ToString();
        //                this.txtTitle.Text = Convert.ToString(reader["title"]);
        //                this.txtSurname.Text = reader["surname"].ToString();
        //                this.txtFirstName.Text = reader["first_name"].ToString();

        //                //
        //                this.txtCustNoIncome.Text = txtCustInfoID.Text;
        //                //this.txtCustIncomeName.Text = txtShrtName.Text;
        //                //Account with Bank
        //                this.txtCustNoTCAcct.Text = txtCustInfoID.Text;
        //                //this.txtCustOtherAcctName.Text = txtShrtName.Text;
        //                //Next of Kin
        //                this.txtCustNoNOK.Text = txtCustInfoID.Text;
        //                //this.txtCustNOKfName.Text = txtShrtName.Text;
        //                //Foreign Customer
        //                this.txtCustNoForgner.Text = txtCustInfoID.Text;
        //                //this.txtCustFfName.Text = txtShrtName.Text;
        //                //Jurat Details
        //                this.txtCustNoJurat.Text = txtCustInfoID.Text;
        //                //this.txtCustLFullName.Text = txtShrtName.Text;
        //                //Family
        //                this.txtCustNoAI.Text = txtCustInfoID.Text;
        //                //this.txtCustFIfName.Text = txtShrtName.Text;

        //                //Employment Info
        //                this.txtCustNoEmpInfo.Text = txtCustInfoID.Text;
        //                //this.txtCustEIfName.Text = txtShrtName.Text;

        //                //Financial Inc details
        //                this.txtCustNoA4FinInc.Text = txtCustInfoID.Text;

        //                //Trust acct.
        //                this.txtCustNoTCAcct.Text = txtCustInfoID.Text;

        //                //Additional Info
        //                this.txtCustNoAddInfo.Text = txtCustInfoID.Text;



        //                this.txtOtherName.Text = reader["other_name"].ToString();
        //                this.txtNickname.Text = reader["nickname_alias"].ToString();
        //                this.txtSex.Text = reader["sex"].ToString();
        //                this.txtDateOfBirth.Text = Convert.ToDateTime(reader["date_of_birth"]).ToString("dd/MM/yyyy");
        //                //Convert.ToDateTime(reader["date_of_birth"], CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat).ToString();//DateTime.Parse(reader["date_of_birth"].ToString()).ToString("dd/MM/yyyy");//
        //                this.txtAge.Text = reader["age"].ToString();//String.Format("{0:dd/MM/yyyy}", 
        //                this.txtPlacefBirth.Text = reader["place_of_birth"].ToString();
        //                this.txtCountryofBirth.Text = reader["country_of_birth"].ToString();
        //                this.txtNationality.Text = reader["nationality"].ToString();
        //                this.txtStateOfOrigin.Text = reader["state_of_origin"].ToString();
        //                this.txtMaritalStatus.Text = reader["marital_status"].ToString();
        //                this.txtMothersMaidenName.Text = reader["mother_maiden_name"].ToString();
        //                this.txtNoOfChildren.Text = reader["number_of_children"].ToString();
        //                this.txtReligion.Text = reader["religion"].ToString();
        //                this.txtComplexion.Text = reader["complexion"].ToString();
        //                this.txtDisability.Text = reader["disability"].ToString();
        //                this.txtCountryofResidence.Text = reader["country_of_residence"].ToString();
        //                this.txtStateOfResidence.Text = reader["state_of_residence"].ToString();
        //                this.txtLGAOfResidence.Text = reader["lga_of_residence"].ToString();
        //                this.txtCityofResidence.Text = reader["city_town_of_residence"].ToString();
        //                this.txtResidentialAddy.Text = reader["residential_address"].ToString();
        //                this.txtNearestBusStop.Text = reader["nearest_bus_stop_landmark"].ToString();
        //                this.rbtOwnedorRented.Text = reader["residence_owned_or_rent"].ToString();
        //                this.txtZipCode.Text = reader["zip_postal_code"].ToString();

        //                this.txtMobileNo.Text = reader["mobile_no"].ToString();
        //                this.txtEmail.Text = reader["email_address"].ToString();
        //                this.txtMailingAddy.Text = reader["mailing_address"].ToString();

        //                this.txtIDType.Text = Convert.ToString(reader["identification_type"]);
        //                this.txtIDNo.Text = reader["id_no"].ToString();
        //                this.txtIDIssueDate.Text = reader["id_issue_date"].ToString();
        //                this.txtIDExpiryDate.Text = reader["id_expiry_date"].ToString();
        //                this.txtPlaceOfIssue.Text = reader["place_of_issuance"].ToString();
        //                this.txtTINNo.Text = reader["tin_no"].ToString();

        //                lblstatus.Text = MessageFormatter.GetFormattedSuccessMessage("Record found Successfully!");
        //                //this.txtCustInfoID.Text = "";
        //                // return;

        //                GridView2.DataBind(); GridView0.DataBind(); GridView9.DataBind();
        //                GridView1.DataBind(); GridView3.DataBind(); GridView4.DataBind();
        //                GridView5.DataBind(); GridView6.DataBind(); GridView7.DataBind();
        //                GridView8.DataBind();
        //                //  }
        //            }//end of while
        //        }
        //        else
        //        {
        //            lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error: Record not found");
        //            callClearBOx();
        //            return;
        //        }

        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
        //        this.txtCustInfoID.Text = "";
        //        callClearBOx();
        //        // return;
        //    }
        //    finally
        //    {

        //        objCmd = null;
        //        con.Close();
        //    }


        //}



        protected void OnEdit_CustomerInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            LinkButton lnk = sender as LinkButton;
            string recId = lnk.Attributes["RecId"];

            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.get_indiv_details";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = recId;
            //
            objCmd.Parameters.Add("custinfo", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                OracleDataReader reader;
                reader = objCmd.ExecuteReader();

                if (reader.Read())
                {

                    if (recId != reader["customer_no"].ToString())
                    {
                        lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error: Record not found");

                    }
                    else
                    //    created = 0;
                    {

                        this.txtCustInfoID.Text = reader["customer_no"].ToString();
                        this.txtTitle.Text = reader["title"] == DBNull.Value ? string.Empty : reader["title"].ToString();
                        this.txtSurname.Text = reader["surname"] == DBNull.Value ? string.Empty : reader["surname"].ToString();
                        this.txtFirstName.Text = reader["first_name"] == DBNull.Value ? string.Empty : reader["first_name"].ToString();
                        this.txtOtherName.Text = reader["other_name"] == DBNull.Value ? string.Empty : reader["other_name"].ToString();
                        this.txtNickname.Text = reader["nickname_alias"] == DBNull.Value ? string.Empty : reader["nickname_alias"].ToString();
                        this.txtSex.Text = reader["sex"] == DBNull.Value ? string.Empty : reader["sex"].ToString();
                        this.txtDateOfBirth.Text = reader["date_of_birth"] == DBNull.Value ? null : Convert.ToDateTime(reader["date_of_birth"]).ToString("dd/MM/yyyy");
                        //this.txtAccountNo.Text = reader["ACCOUNT_NO"].ToString();
                        this.txtAge.Text = reader["age"] == DBNull.Value ? string.Empty : reader["age"].ToString();
                        //Convert.ToDateTime(reader["DATE_OF_BIRTH"]).Date.ToString();
                        this.txtPlacefBirth.Text = reader["place_of_birth"] == DBNull.Value ? string.Empty : reader["place_of_birth"].ToString();
                        //this.txtNationality.Text = reader["nationality"].ToString();
                        this.txtCountryofBirth.Text = reader["country_of_birth"] == DBNull.Value ? string.Empty : reader["country_of_birth"].ToString().ToUpper();
                        this.txtNationality.Text = reader["nationality"] == DBNull.Value ? string.Empty : reader["nationality"].ToString().ToUpper();
                        this.txtStateOfOrigin.Text = reader["state_of_origin"] == DBNull.Value ? string.Empty : reader["state_of_origin"].ToString();
                        this.txtMaritalStatus.Text = reader["marital_status"] == DBNull.Value ? string.Empty : reader["marital_status"].ToString();
                        this.txtMothersMaidenName.Text = reader["mother_maiden_name"] == DBNull.Value ? string.Empty : reader["mother_maiden_name"].ToString();
                        this.txtNoOfChildren.Text = reader["number_of_children"] == DBNull.Value ? string.Empty : reader["number_of_children"].ToString();
                        this.txtReligion.Text = reader["religion"] == DBNull.Value ? string.Empty : reader["religion"].ToString();
                        this.txtComplexion.Text = reader["complexion"] == DBNull.Value ? string.Empty : reader["complexion"].ToString();
                        this.txtDisability.Text = reader["disability"] == DBNull.Value ? string.Empty : reader["disability"].ToString();
                        this.txtCountryofResidence.Text = reader["country_of_residence"] == DBNull.Value ? string.Empty : reader["country_of_residence"].ToString();
                        this.txtStateOfResidence.Text = reader["state_of_residence"] == DBNull.Value ? string.Empty : reader["state_of_residence"].ToString();
                        this.txtLGAOfResidence.Text = reader["lga_of_residence"] == DBNull.Value ? string.Empty : reader["lga_of_residence"].ToString();
                        this.txtCityofResidence.Text = reader["city_town_of_residence"] == DBNull.Value ? string.Empty : reader["city_town_of_residence"].ToString();
                        this.txtResidentialAddy.Text = reader["residential_address"] == DBNull.Value ? string.Empty : reader["residential_address"].ToString();
                        this.txtNearestBusStop.Text = reader["nearest_bus_stop_landmark"] == DBNull.Value ? string.Empty : reader["nearest_bus_stop_landmark"].ToString();
                        this.rbtOwnedorRented.Text = reader["residence_owned_or_rent"] == DBNull.Value ? string.Empty : reader["residence_owned_or_rent"].ToString();
                        this.txtZipCode.Text = reader["zip_postal_code"] == DBNull.Value ? string.Empty : reader["zip_postal_code"].ToString();

                        this.txtMobileNo.Text = reader["mobile_no"] == DBNull.Value ? string.Empty : reader["mobile_no"].ToString();
                        this.txtEmail.Text = reader["email_address"] == DBNull.Value ? string.Empty : reader["email_address"].ToString();
                        this.txtMailingAddy.Text = reader["mailing_address"] == DBNull.Value ? string.Empty : reader["mailing_address"].ToString();

                        this.txtIDType.Text = reader["identification_type"] == DBNull.Value ? string.Empty : Convert.ToString(reader["identification_type"]);
                        this.txtIDNo.Text = reader["id_no"] == DBNull.Value ? string.Empty : reader["id_no"].ToString();
                        this.txtIDIssueDate.Text = reader["id_issue_date"] == DBNull.Value ? null : Convert.ToDateTime(reader["id_issue_date"]).ToString("dd/MM/yyyy");
                        this.txtIDExpiryDate.Text = reader["id_expiry_date"] == DBNull.Value ? null : Convert.ToDateTime(reader["id_expiry_date"]).ToString("dd/MM/yyyy");
                        this.txtPlaceOfIssue.Text = reader["place_of_issuance"] == DBNull.Value ? string.Empty : reader["place_of_issuance"].ToString();
                        this.txtTINNo.Text = reader["tin_no"] == DBNull.Value ? string.Empty : reader["tin_no"].ToString();


                        lblstatus.Text = MessageFormatter.GetFormattedSuccessMessage("Record found Successfully!");
                        //lblstatus.ForeColor = System.Drawing.Color.Green;
                        //
                    }
                }//end of while

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("SelectedValue which is invalid"))
                {
                    lblstatus.Text = MessageFormatter.GetFormattedSuccessMessage("Record found Successfully");
                }
                else
                {

                    lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message + ex.InnerException);
                }
            }
            finally
            {
                //reader.Close();
                objCmd = null;
                con.Close();
            }
        }
        protected void OnEdit_Income(object sender, EventArgs e)
        {

            hidTAB.Value = "#tab2";
            LinkButton lnk = sender as LinkButton;
            try
            {
                string recId = lnk.Attributes["RecId"];
                customer = new CustomerRepository();
                var inc = customer.GetCustomerIncome(recId.Trim(), "TMP_CUSTOMER_INCOME");//int.Parse()
                //this.txtCustIncomeRefID.Text = inc.ReferenceId.ToString();
                this.txtCustNoIncome.Text = inc.CustomerNo;// == null ? string.Empty : inc.CustomerNo;
                this.txtCustIncomeBand.Text = inc.IncomeBand;
                this.txtCustIncomeInitDeposit.Text = inc.InitialDeposit;

                lblstatusCI.Text = inc.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(inc.ErrorMessage);
            }
            catch (Exception ex)
            {
                lblstatusCI.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);

            }
        }
        protected void OnEdit_trustClientAcct(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";
            LinkButton lnk = sender as LinkButton;
            try
            {
                string recId = lnk.Attributes["RecId"];
                customer = new CustomerRepository();
                var tca = customer.GetTrustClientAcct(recId.Trim(), "TMP_TRUSTS_CLIENT_ACCOUNTS");
                this.txtCustNoTCAcct.Text = tca.CustomerNo;
                //this.rblTCAcct.Text = tca.TrustsClientAccounts;
                if (tca.TrustsClientAccounts.Trim() == "Y")
                {
                    this.rblTCAcct.Items[0].Selected = true;
                }
                else if (tca.TrustsClientAccounts.Trim() == "N")
                {
                    this.rblTCAcct.Items[1].Selected = true;
                }
                else
                {
                    this.rblTCAcct.Items[0].Selected = false;
                    this.rblTCAcct.Items[1].Selected = false;
                }

                this.txtCustTCAcctBeneficialName.Text = tca.NameOfBeneficialOwner;
                this.txtCustTCAcctSpouseName.Text = tca.SpouseName;
                this.txtCustTCAcctDOB.Text = Convert.ToString(tca.SpouseDateOfBirth);
                this.txtCustTCAcctOccptn.Text = tca.SpouseOccupation;
                this.txtCustTCAcctOtherScrIncome.Text = tca.OtherSourceExpectAnnInc;
                //this.rblTCAcctInsiderRelation.Text = tca.InsiderRelation;
                if (tca.InsiderRelation.Trim() == "Y")
                {
                    this.rblTCAcctInsiderRelation.Items[0].Selected = true;
                }
                else if (tca.InsiderRelation.Trim() == "N")
                {
                    this.rblTCAcctInsiderRelation.Items[1].Selected = true;
                }
                else
                {
                    this.rblTCAcctInsiderRelation.Items[0].Selected = false;
                    this.rblTCAcctInsiderRelation.Items[1].Selected = false;
                }

                this.txtCustTCAcctNameOfAssBiz.Text = tca.NameOfAssociatedBusiness;
                //this.rblFreqIntTraveler.Text = tca.FreqInternationalTraveler;
                if (tca.FreqInternationalTraveler.Trim() == "Y")
                {
                    this.rblFreqIntTraveler.Items[0].Selected = true;
                }
                else if (tca.FreqInternationalTraveler.Trim() == "N")
                {
                    this.rblFreqIntTraveler.Items[1].Selected = true;
                }
                else
                {
                    this.rblFreqIntTraveler.Items[0].Selected = false;
                    this.rblFreqIntTraveler.Items[1].Selected = false;
                }
                //this.rblTCAcctPolExposed.Text = tca.PoliticallyExposedPerson;
                if (tca.PoliticallyExposedPerson.Trim() == "Y")
                {
                    this.rblTCAcctPolExposed.Items[0].Selected = true;
                }
                else if (tca.PoliticallyExposedPerson.Trim() == "N")
                {
                    this.rblTCAcctPolExposed.Items[1].Selected = true;
                }
                else
                {
                    this.rblTCAcctPolExposed.Items[0].Selected = false;
                    this.rblTCAcctPolExposed.Items[1].Selected = false;
                }
                //this.rtlTCAcctPowerOfAttony.Text = tca.PowerOfAttorney;
                if (tca.PowerOfAttorney.Trim() == "Y")
                {
                    this.rtlTCAcctPowerOfAttony.Items[0].Selected = true;
                }
                else if (tca.PowerOfAttorney.Trim() == "N")
                {
                    this.rtlTCAcctPowerOfAttony.Items[1].Selected = true;
                }
                else
                {
                    this.rtlTCAcctPowerOfAttony.Items[0].Selected = false;
                    this.rtlTCAcctPowerOfAttony.Items[1].Selected = false;
                }
                this.txtTCAcctHoldersName.Text = tca.HolderName;
                this.txtTCAcctAddress.Text = tca.Address;
                this.txtTCAcctCountry.Text = tca.Country;
                this.txtTCAcctNationality.Text = tca.Nationality;
                this.txtTCAcctTelPhone.Text = tca.TelephoneNumber;
                this.txtCustTCAcctSrcOfFund.Text = tca.SourcesOfFundToAccount;


                this.lblstatusTCAcct.Text = tca.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(tca.ErrorMessage);
            }
            catch (Exception ex)
            {
                this.lblstatusTCAcct.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message + ex.InnerException);

            }
        }
        protected void OnEdit_NextOfKin(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab4";
            LinkButton lnk = sender as LinkButton;
            try
            {
                string recId = lnk.Attributes["RecId"];
                customer = new CustomerRepository();
                var nok = customer.GetNextOfKin(recId.Trim(), "TMP_INDIVIDUAL_NEXT_OF_KIN");//recId.Trim()
                this.txtCustNoNOK.Text = nok.CustomerNo;
                this.txtCustNOKTitle.Text = nok.Title;
                this.txtCustNOKSurname.Text = nok.Surname;
                this.txtCustNOKfirstName.Text = nok.FirstName;
                this.txtCustNOKOtherName.Text = nok.OtherName;
                this.txtCustNOKDateOfBirth.Text = Convert.ToString(nok.DateOfBirth);
                this.txtCustNOKSex.Text = nok.Sex;
                this.txtCustNOKReltnship.Text = nok.Relationship;
                this.txtCustNOKOfficeNo.Text = nok.OfficeNo;
                this.txtCustNOKMobileNo.Text = nok.MobileNo;
                this.txtCustNOKEmail.Text = nok.EmailAddress;
                this.txtCustNOKHouseNo.Text = nok.HouseNo;
                this.txtCustNOKIdType.Text = nok.IDType;
                this.txtCustNOKIssuedDate.Text = Convert.ToString(nok.IDIssueDate);
                this.txtCustNOKExpiryDate.Text = Convert.ToString(nok.IDExpiryDate);
                this.txtCustNOKPermitNo.Text = nok.ResidentPermitNo;
                this.txtCustNOKPlaceOfIssue.Text = nok.PlaceOfIssuance;
                this.txtCustNOKStreetName.Text = nok.StreetName;
                this.txtCustNOKBusstop.Text = nok.NearestBStop;
                this.txtCustNOKZipCode.Text = nok.ZipCode;
                this.txtCustNOKCountry.Text = nok.Country;
                this.txtCustNOKState.Text = nok.State;
                this.txtCustNOKLGA.Text = nok.LGA;
                this.txtCustNOKCity.Text = nok.CityTown;


                this.lblstatusNOK.Text = nok.ErrorMessage == string.Empty ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(nok.ErrorMessage);
            }
            catch (Exception ex)
            {
                this.lblstatusNOK.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);

            }
        }
        protected void OnEdit_Foreigner(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab5";
            LinkButton lnk = sender as LinkButton;
            try
            {
                string customer_no = lnk.Attributes["RecId"];
                customer = new CustomerRepository();
                var f = customer.GetForeigner(customer_no, "TMP_FOREIGN_DETAILS");
                this.lblstatusFD.Text = f.ErrorMessage;
                this.txtCustNoForgner.Text = f.CustomerNo;
                //this.txtCustIDForgnerRefID.Text = f.ReferenceId.ToString();
                //this.txtCustFfName.Text = f.CustomerName;

                this.txtCustFPassPermit.Text = f.PassportResidencePermit.ToString();
                this.txtCustFIssueDate.Text = f.PermitIssueDate.ToString();
                this.txtCustFExpiryDate.Text = f.PermitExpiryDate.ToString();
                this.txtCustfForeignAddy.Text = f.ForeignAddress.ToString();
                this.txtCustfCity.Text = f.city.ToString();
                this.txtCustfCountry.Text = f.country.ToString();
                //

                this.txtCustfForeignPhoneNo.Text = f.foreign_tel_number.ToString();
                this.txtCustfZipPostalCode.Text = f.zip_postal_code.ToString();
                this.txtCustfPurposeOfAcc.Text = f.purpose_of_account.ToString();



                lblstatusFD.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);
            }
            catch (Exception ex)
            {
                lblstatusFD.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);

            }
        }
        protected void OnEdit_jurat(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab6";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var j = customer.GetJuratInfo(recId, "TMP_JURAT");
            try
            {
                this.txtCustNoJurat.Text = j.CustomerNo;
                this.txtCustJDateOfOath.Text = Convert.ToString(j.OathDate);
                this.txtCustJNameOfInerpreter.Text = j.NameOfInterpreter.ToString();
                this.txtCustJAddyOfInterperter.Text = j.AddressOfInterpreter;
                this.txtCustJPhoneNo.Text = j.TelephoneNo;
                this.txtCustJLangOfInterpretation.Text = j.LanguageOfInterpretation;

                lblstatusJurat.Text = j.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(j.ErrorMessage);//"Record Loaded" : f.ErrorMessage;
            }
            catch (Exception ex)
            {

                lblstatusJurat.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }

        }
        protected void OnEdit_AccountInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab7";
            LinkButton lnk = sender as LinkButton;

            // string recId = lnk.Attributes["RecId"];
            //LinkButton lnkRowSelection = (LinkButton)sender;
            string[] arguments = lnk.CommandArgument.Split(';');
            string customer_no = arguments[0];
            string account_no = arguments[1];

            try
            {
                customer = new CustomerRepository();
                var a = customer.GetAccountInfo4Checkers(customer_no.Trim(), account_no.Trim());
                this.txtCustNoAI.Text = a.CustomerNo;
                //this.rbtCustAIAccHolders.Text = a.AccountHolder;
                this.txtCustAITypeOfAcc.Text = a.TypeOfAccount;
                this.txtCustAIAccNo.Text = a.AccountNumber;
                this.txtCustAIAccOfficer.Text = a.AccountOfficer;
                this.txtCustAIAccTitle.Text = a.AccountTitle;
                this.txtCustAIBranch.Text = a.Branch;
                this.txtCustAIBranchClass.Text = a.BranchClass;
                this.txtCustAIOnlineTrnsfLimit.Text = a.OnlineTransferLimitRange;
                this.txtCustAIBizDiv.Text = a.BusinessDivision;
                this.txtCustBizSeg.Text = a.BizSegment;
                this.txtCustBizSize.Text = a.BizSize;
                this.txtCustAIBVNNo.Text = a.BVNNo;
                //this.rblCAVRequired.SelectedValue = a.CAVRequired;
                if (a.CAVRequired.Trim() == "Y")
                {
                    this.rblCAVRequired.Items[0].Selected = true;
                }
                else if (a.CAVRequired.Trim() == "N")
                {
                    this.rblCAVRequired.Items[1].Selected = true;
                }
                else
                {
                    this.rblCAVRequired.Items[0].Selected = false;
                    this.rblCAVRequired.Items[1].Selected = false;
                }
                this.txtCustAICustIc.Text = a.CustomerIc;
                //this.txtCustAICustId.Text = a.CustomerId;
                this.txtCustAICustSeg.Text = a.CustomerSegment;
                this.txtCustAICusType.Text = a.CustomerType;
                this.txtCustAIOpInsttn.Text = a.OperatingInstruction;
                this.txtCustAIOriginatingBranch.Text = a.OriginatingBranch;
                //Account Services Required
                //this.rbtASRCardRef.Text = a.CardPreference;
                if (a.CardPreference.Trim() == "Y")
                {
                    this.rbtASRCardRef.Items[0].Selected = true;
                }
                else if (a.CardPreference.Trim() == "N")
                {
                    this.rbtASRCardRef.Items[1].Selected = true;
                }
                else
                {
                    this.rbtASRCardRef.Items[0].Selected = false;
                    this.rbtASRCardRef.Items[1].Selected = false;
                }
                //this.rbtASREBP.Text = a.ElectronicBankingPreference;
                if (a.ElectronicBankingPreference.Trim() == "Y")
                {
                    this.rbtASREBP.Items[0].Selected = true;
                }
                else if (a.ElectronicBankingPreference.Trim() == "N")
                {
                    this.rbtASREBP.Items[1].Selected = true;
                }
                else
                {
                    this.rbtASREBP.Items[0].Selected = false;
                    this.rbtASREBP.Items[1].Selected = false;
                }

                //this.rbtASRStatementPref.Text = a.StatementPreferences;
                if (a.StatementPreferences.Trim() == "Y")
                {
                    this.rbtASRStatementPref.Items[0].Selected = true;
                }
                else if (a.StatementPreferences.Trim() == "N")
                {
                    this.rbtASRStatementPref.Items[1].Selected = true;
                }
                else
                {
                    this.rbtASRStatementPref.Items[0].Selected = false;
                    this.rbtASRStatementPref.Items[1].Selected = false;
                }

                //this.rbtASRTransAlertPref.Text = a.TranxAlertPreference;
                if (a.TranxAlertPreference.Trim() == "Y")
                {
                    this.rbtASRTransAlertPref.Items[0].Selected = true;
                }
                else if (a.TranxAlertPreference.Trim() == "N")
                {
                    this.rbtASRTransAlertPref.Items[1].Selected = true;
                }
                else
                {
                    this.rbtASRTransAlertPref.Items[0].Selected = false;
                    this.rbtASRTransAlertPref.Items[1].Selected = false;
                }

                //this.rbtASRStatementFreq.Text = a.StatementFrequency;
                if (a.StatementFrequency.Trim() == "Y")
                {
                    this.rbtASRStatementFreq.Items[0].Selected = true;
                }
                else if (a.StatementFrequency.Trim() == "N")
                {
                    this.rbtASRStatementFreq.Items[1].Selected = true;
                }
                else
                {
                    this.rbtASRStatementFreq.Items[0].Selected = false;
                    this.rbtASRStatementFreq.Items[1].Selected = false;
                }

                //this.rbtASRChequeBookReqtn.Text = a.ChequeBookRequisition;
                if (a.ChequeBookRequisition.Trim() == "Y")
                {
                    this.rbtASRChequeBookReqtn.Items[0].Selected = true;
                }
                else if (a.ChequeBookRequisition.Trim() == "N")
                {
                    this.rbtASRChequeBookReqtn.Items[1].Selected = true;
                }
                else
                {
                    this.rbtASRChequeBookReqtn.Items[0].Selected = false;
                    this.rbtASRChequeBookReqtn.Items[1].Selected = false;
                }
                //this.rbtASRChequeLeaveReq.Text = a.ChequeLeavesRequired;
                if (a.ChequeLeavesRequired.Trim() == "Y")
                {
                    this.rbtASRChequeLeaveReq.Items[0].Selected = true;
                }
                else if (a.ChequeLeavesRequired.Trim() == "N")
                {
                    this.rbtASRChequeLeaveReq.Items[1].Selected = true;
                }
                else
                {
                    this.rbtASRChequeLeaveReq.Items[0].Selected = false;
                    this.rbtASRChequeLeaveReq.Items[1].Selected = false;
                }

                //this.rbtASRChequeConfmtn.Text = a.ChequeConfirmation;
                if (a.ChequeConfirmation.Trim() == "Y")
                {
                    this.rbtASRChequeConfmtn.Items[0].Selected = true;
                }
                else if (a.ChequeConfirmation.Trim() == "N")
                {
                    this.rbtASRChequeConfmtn.Items[1].Selected = true;
                }
                else
                {
                    this.rbtASRChequeConfmtn.Items[0].Selected = false;
                    this.rbtASRChequeConfmtn.Items[1].Selected = false;
                }

                //this.rbtASRChequeConfmtnThreshold.Text = a.ChequeConfirmationThreshold;
                if (a.ChequeConfirmationThreshold.Trim() == "Y")
                {
                    this.rbtASRChequeConfmtnThreshold.Items[0].Selected = true;
                }
                else if (a.ChequeConfirmationThreshold.Trim() == "N")
                {
                    this.rbtASRChequeConfmtnThreshold.Items[1].Selected = true;
                }
                else
                {
                    this.rbtASRChequeConfmtnThreshold.Items[0].Selected = false;
                    this.rbtASRChequeConfmtnThreshold.Items[1].Selected = false;
                }

                this.txtASRChequeConfmtnThresholdRange.Text = a.ChequeConfirmationThresholdRange;

                //this.rbtASROnlineTraxLimit.Text = a.OnlineTransferLimit;
                if (a.OnlineTransferLimit.Trim() == "Y")
                {
                    this.rbtASROnlineTraxLimit.Items[0].Selected = true;
                }
                else if (a.OnlineTransferLimit.Trim() == "N")
                {
                    this.rbtASROnlineTraxLimit.Items[1].Selected = true;
                }
                else
                {
                    this.rbtASROnlineTraxLimit.Items[0].Selected = false;
                    this.rbtASROnlineTraxLimit.Items[1].Selected = false;
                }

                //this.rbtASRToken.Text = a.Token;
                if (a.Token.Trim() == "Y")
                {
                    this.rbtASRToken.Items[0].Selected = true;
                }
                else if (a.Token.Trim() == "N")
                {
                    this.rbtASRToken.Items[1].Selected = true;
                }
                else
                {
                    this.rbtASRToken.Items[0].Selected = false;
                    this.rbtASRToken.Items[1].Selected = false;
                }

                this.txtASRAcctSignitory.Text = a.AccountSignatory;
                this.txtASR2ndAcctSignitory.Text = a.SecondSignatory;

                lblstatusAI.Text = a.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(a.ErrorMessage);//"Record Loaded" : f.ErrorMessage;
            }
            catch (Exception ex)
            {

                lblstatusAI.Text = ex.Message.Contains("SelectedValue which is invalid") ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }
        }
        protected void OnEdit_Employment(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab8";
            LinkButton lnk = sender as LinkButton;
            try
            {
                string recId = lnk.Attributes["RecId"];
                customer = new CustomerRepository();
                var f = customer.GetEmployment(recId, "TMP_EMPLOYMENT_DETAILS");
                this.txtCustNoEmpInfo.Text = f.CustomerNo;


                this.txtCustNoEmpInfo.Text = f.CustomerNo;
                this.txtCustEIEmpStatus.Text = f.EmploymentStatus;
                this.txtCustEIEmployerName.Text = f.EmployerInstName;
                this.txtCustEIDateOfEmp.Text = f.EmploymentDate.ToString();
                this.txtCustEISectorClass.Text = f.SectorClass;
                this.txtCustEISubSector.Text = f.SubSector;
                this.txtCustEINatureOfBiz.Text = f.NatureOfBuzOcc;
                this.txtCustEIIndustrySeg.Text = f.IndustrySegment;



                lblstatusEI.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);
            }
            catch (Exception ex)
            {
                lblstatusEI.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);

            }
        }
        protected void OnEdit_additionalInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab10";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetAdditionalInfo(recId, "TMP_ADDITIONAL_INFORMATION");
            try
            {
                this.txtCustNoAddInfo.Text = f.CustomerNo;
                this.txtAddInfoAnnualSalary.Text = f.AnnualSalaryExpectedInc.ToString();
                this.txtAddInfoFaxNumber.Text = f.FaxNumber.ToString();


                this.lblCustAddtnalInfo.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);//"Record Loaded" : f.ErrorMessage;
            }
            catch (Exception ex)
            {

                lblCustAddtnalInfo.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }

        }
        protected void OnEdit_A4FinInc(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab9";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetAuthFinInclusion(recId, "TMP_AUTH_FINANCE_INCLUSION");
            try
            {
                this.txtCustNoA4FinInc.Text = f.CustomerNo;
                //this.rbtA4FinIncSocFinDisadv.Items[0].Selected = f.SocialFinancialDisadvtage == "Y" ? ;  

                if (f.SocialFinancialDisadvtage.Trim() == "Y")
                {
                    this.rbtA4FinIncSocFinDisadv.Items[0].Selected = true;
                }
                else if (f.SocialFinancialDisadvtage.Trim() == "N")
                {
                    this.rbtA4FinIncSocFinDisadv.Items[1].Selected = true;
                }
                else
                {
                    this.rbtA4FinIncSocFinDisadv.Items[0].Selected = false;
                    this.rbtA4FinIncSocFinDisadv.Items[1].Selected = false;
                }

                this.txtA4FinIncSocFinDoc.Text = f.SocialFinancialDocuments;
                //this.rbtA4FinIncEnjoyKYC.DataValueField = f.EnjoyedTieredKyc;

                if (f.EnjoyedTieredKyc.Trim() == "Y")
                {
                    this.rbtA4FinIncEnjoyKYC.Items[0].Selected = true;
                }
                else if (f.EnjoyedTieredKyc.Trim() == "N")
                {
                    this.rbtA4FinIncEnjoyKYC.Items[1].Selected = true;
                }
                else
                {
                    this.rbtA4FinIncEnjoyKYC.Items[0].Selected = false;
                    this.rbtA4FinIncEnjoyKYC.Items[1].Selected = false;
                }

                this.txtA4FinIncRiskCat.Text = f.RiskCategory;
                this.txtA4FinIncMandRule.Text = f.MandateAuthCombineRule;
                this.txtA4FinIncAcctWithOtherBanks.Text = f.AccountWithOtherBanks;

                this.lblCustNoA4FinInc.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);//"Record Loaded" : f.ErrorMessage;
            }
            catch (Exception ex)
            {

                lblCustNoA4FinInc.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }

        }


        //private string getCheckedRadioButtonList(string checkedOption)
        //{
        //    if (f.EnjoyedTieredKyc.Trim() == "Y")
        //        {
        //            this.rbtA4FinIncEnjoyKYC.Items[0].Selected = true;
        //        }
        //        else if (f.EnjoyedTieredKyc.Trim() == "N")
        //        {
        //            this.rbtA4FinIncEnjoyKYC.Items[1].Selected = true;
        //        }
        //        else
        //        {
        //            this.rbtA4FinIncEnjoyKYC.Items[0].Selected = false;
        //            this.rbtA4FinIncEnjoyKYC.Items[1].Selected = false;
        //        }
        //    return;
        //}


        private void callClearBOx()
        {
            this.txtCustInfoID.Text = this.txtSurname.Text = this.txtFirstName.Text = this.txtTitle.Text
 = this.txtOtherName.Text = this.txtNickname.Text = this.txtSex.Text = this.txtDateOfBirth.Text = this.txtAge.Text =
this.txtMothersMaidenName.Text = "";
            this.txtPlacefBirth.Text = this.txtCountryofBirth.Text = this.txtCountryofBirth.Text = this.txtNationality.Text = this.txtStateOfOrigin.Text = this.txtMaritalStatus.Text = this.txtMothersMaidenName.Text = "";
            this.txtNoOfChildren.Text = this.txtReligion.Text = this.txtComplexion.Text = this.txtDisability.Text = this.txtCountryofResidence.Text = this.txtStateOfResidence.Text = "";
            this.txtStateOfResidence.Text = this.txtLGAOfResidence.Text = this.txtCityofResidence.Text = this.txtResidentialAddy.Text = this.txtNearestBusStop.Text = "";
            this.rbtOwnedorRented.Text = null;
            this.txtZipCode.Text = this.txtMobileNo.Text = this.txtEmail.Text = this.txtMailingAddy.Text = this.txtIDType.Text = this.txtIDType.Text = this.txtIDNo.Text = this.txtIDIssueDate.Text = this.txtIDExpiryDate.Text = this.txtPlaceOfIssue.Text = this.txtTINNo.Text = "";


        }

        protected void btnApproveCustInfo_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            this.AuthoriseCustomerInfo("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }
        protected void btnRejectCustInfo_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            string CustInfoRejectComment = HidCustInfoComment.Value; 
            //this.txtCustInfoComment.Text = "";
            this.AuthoriseCustomerInfo("N", "Record Rejected!!!", System.Drawing.Color.Red, CustInfoRejectComment);
        }
        private void AuthoriseCustomerInfo(string authorized, string statusMsg, Color color,string checkersComment)
        {
            hidTAB.Value = "#tab1";
            
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_individual_details";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustInfoID.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtCustInfoID.Text, "TMP_INDIVIDUAL_BIO_DATA");

            try
            {
                getCustID rst = new getCustID();

                if (rst.get_customer_id(txtCustInfoID.Text) == 0)
                {
                    lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

                    return;

                }
                else
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();//
                    }

                    int rstCmd = objCmd.ExecuteNonQuery();

                    if (rstCmd == -1)
                    {
                        lblstatus.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);
                        // + Session["UserID"] + " | " + makerName + " | " + txtCustInfoID.Text + " | " + authorized + "Biodata Information");

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtCustInfoID.Text, authorized, "BIODATA INFORMATION", checkersComment);

                        
                        callClearBOx();
                        GridView0.DataBind();
                     
                        ///////////////////////////////////////////////////////////////////////
                    }
                    else
                    {

                        lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message + ex.InnerException + ex.Source + ex.StackTrace);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }


        protected void btnApproveAI_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab7";
            this.AuthoriseAcctInfo("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnRejectAI_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab7";
            string CustAIRejectComment = this.HidCustAIComment.Value;
            this.AuthoriseAcctInfo("N", "Record Rejected!!!", System.Drawing.Color.Red, CustAIRejectComment);
        }

        private void AuthoriseAcctInfo(string authorized, string statusMsg, Color color, string checkersComment)
        {
            hidTAB.Value = "#tab7";
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_account_info";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustNoAI.Text;
            objCmd.Parameters.Add("p_account_number", OracleDbType.Varchar2).Value = this.txtCustAIAccNo.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";

            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtCustNoAI.Text, "tmp_account_info");
            //
            try
            {

                getCustID rst = new getCustID();

                if (rst.get_customer_id(txtCustNoAI.Text) == 0)
                {
                    lblstatusAI.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

                    return;

                }
                else
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();//
                    }

                    int rstCmd = objCmd.ExecuteNonQuery();

                    if (rstCmd == -1)
                    {

                        lblstatusAI.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg + Session["UserID"] + " | " + makerName + " | " + txtCustNoAI.Text + " | " + authorized + "ACCOUNT Information");

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtCustNoAI.Text, authorized, "ACCOUNT INFORMATION", checkersComment);

                        txtCustNoAI.Text = txtCustAIAccNo.Text = txtCustAIAccOfficer.Text = txtCustAIAccTitle.Text = txtCustAIBVNNo.Text = rblCAVRequired.Text = txtCustAICustIc.Text = txtCustAIOpInsttn.Text = "";
                        txtASRAcctSignitory.Text = txtASR2ndAcctSignitory.Text = "";

                        txtCustAITypeOfAcc.Text = txtCustAIBranch.Text = txtCustAIBranchClass.Text = txtCustAIBizDiv.Text = txtCustBizSeg.Text = txtCustBizSize.Text = txtCustAICustSeg.Text = txtCustAICusType.Text = txtCustAIOnlineTrnsfLimit.Text = txtCustAIOriginatingBranch.Text = "";
                        txtASRChequeConfmtnThresholdRange.Text = "";
                        //rbtASRCardRef.SelectedValue = rbtASREBP.SelectedValue = rbtASRStatementPref.SelectedValue = rbtASRTransAlertPref.SelectedValue = rbtASRStatementFreq.SelectedValue = rbtASRChequeBookReqtn.SelectedValue = rbtASRChequeConfmtnThreshold.SelectedValue = rbtASROnlineTraxLimit.SelectedValue = rbtASRToken.SelectedValue = 


                        GridView6.DataBind();
                        // callClearBOx();

                    }
                    else
                    {

                        lblstatusAI.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblstatusAI.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }



        protected void btnApproveIncome_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab2";
            this.AuthoriseCustIncome("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnRejectIncome_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab2";
            string CustIncomeRejectComment = this.HidCustIncomeComment.Value;
            this.AuthoriseCustIncome("N", "Record Rejected!!!", System.Drawing.Color.Red, CustIncomeRejectComment);
        }

        private void AuthoriseCustIncome(string authorized, string statusMsg, Color color, string checkersComment)
        {
            hidTAB.Value = "#tab2";
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_customer_income";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustNoIncome.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //

            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtCustNoIncome.Text, "tmp_customer_income");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_customer_id(txtCustNoIncome.Text) == 0)
                {
                    lblstatusCI.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

                    return;

                }
                else
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();//
                    }

                    int rstCmd = objCmd.ExecuteNonQuery();

                    if (rstCmd == -1)
                    {

                        lblstatusCI.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtCustNoIncome.Text, authorized, "CUSTOMER INCOME", checkersComment);


                        txtCustIncomeBand.Text = txtCustIncomeInitDeposit.Text = "";

                        GridView1.DataBind();
                        //callClearBOx();

                    }
                    else
                    {

                        lblstatusCI.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblstatusCI.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void btnApproveNOK_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab4";
            this.AuthoriseNOK("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnRejectNOK_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab4";
            string CustNOKRejectComment = this.HidCustNOKComment.Value;
            this.AuthoriseNOK("N", "Record Rejected!!!", System.Drawing.Color.Red, CustNOKRejectComment);
        }

        private void AuthoriseNOK(string authorized, string statusMsg, Color color, string checkersComment)
        {
            hidTAB.Value = "#tab4";
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_next_of_kin";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustNoNOK.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //

            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtCustNoNOK.Text, "tmp_individual_next_of_kin");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_customer_id(txtCustNoNOK.Text) == 0)
                {
                    lblstatusNOK.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

                    return;

                }
                else
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();//
                    }

                    int rstCmd = objCmd.ExecuteNonQuery();

                    if (rstCmd == -1)
                    {

                        lblstatusNOK.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtCustNoNOK.Text, authorized, "NEXT OF KIN", checkersComment);

                        txtCustNoNOK.Text = txtCustNOKSurname.Text = txtCustNOKfirstName.Text = txtCustNOKOtherName.Text = txtCustNOKDateOfBirth.Text = txtCustNOKOfficeNo.Text = txtCustNOKMobileNo.Text = txtCustNOKEmail.Text = txtCustNOKHouseNo.Text = txtCustNOKIssuedDate.Text = txtCustNOKExpiryDate.Text = txtCustNOKPermitNo.Text = txtCustNOKPlaceOfIssue.Text = txtCustNOKStreetName.Text = txtCustNOKBusstop.Text = txtCustNOKCity.Text = txtCustNOKLGA.Text = txtCustNOKZipCode.Text = txtCustNOKCity.Text = "";
                        txtCustNOKIdType.Text = txtCustNOKTitle.Text = txtCustNOKSex.Text = txtCustNOKReltnship.Text = txtCustNOKState.Text = txtCustNOKCountry.Text = "";

                        GridView5.DataBind();
                        //callClearBOx();

                    }
                    else
                    {

                        lblstatusNOK.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblstatusNOK.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }
        // }

        protected void btnApproveForDet_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab5";
            this.AuthoriseForDet("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnRejectForDet_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab5";
            string CustForDetRejectComment = this.HidCustForDetComment.Value;
            this.AuthoriseForDet("N", "Record Rejected!!!", System.Drawing.Color.Red, CustForDetRejectComment);
        }

        private void AuthoriseForDet(string authorized, string statusMsg, Color color, string checkersComment)
        {
            hidTAB.Value = "#tab5";
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_foreign_details";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustNoForgner.Text;
            objCmd.Parameters.Add("p_foreigner", OracleDbType.Varchar2).Value = rbtForeigner.Text;
            objCmd.Parameters.Add("p_country", OracleDbType.Varchar2).Value = txtCustfCountry.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtCustNoForgner.Text,"tmp_foreign_details");

            try
            {
                getCustID rst = new getCustID();

                if (rst.get_customer_id(txtCustNoForgner.Text) == 0)
                {
                    lblstatusFD.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

                    return;

                }
                else
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();//
                    }

                    int rstCmd = objCmd.ExecuteNonQuery();

                    if (rstCmd == -1)
                    {

                        lblstatusFD.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtCustNoForgner.Text, authorized, "FOREIGNER DETAILS", checkersComment);

                        //rbtForeigner.Text = rbtMultipleCitizenship.Text = 
                        txtCustFPassPermit.Text = txtCustFIssueDate.Text = txtCustFExpiryDate.Text = txtCustfForeignAddy.Text = txtCustfForeignPhoneNo.Text = txtCustfCountry.Text = txtCustfCity.Text = txtCustfPurposeOfAcc.Text = txtCustfZipPostalCode.Text = "";
                        //callClearBOx();
                        GridView3.DataBind();
                    }
                    else
                    {

                        lblstatusFD.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblstatusFD.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }


        protected void btnApproveJurat_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab6";
            this.AuthoriseJurat("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }
        //}

        protected void btnRejectJurat_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab6";
            string CustJuratRejectComment = this.HidCustForDetComment.Value;
            this.AuthoriseJurat("N", "Record Rejected!!!", System.Drawing.Color.Red, CustJuratRejectComment);
        }

        private void AuthoriseJurat(string authorized, string statusMsg, Color color, string checkersComment)
        {
            hidTAB.Value = "#tab6";
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_jurat";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustNoJurat.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtCustNoJurat.Text, "tmp_jurat");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_customer_id(txtCustNoJurat.Text) == 0)
                {
                    lblstatusJurat.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

                    return;

                }
                else
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();//
                    }

                    int rstCmd = objCmd.ExecuteNonQuery();

                    if (rstCmd == -1)
                    {

                        lblstatusJurat.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtCustNoJurat.Text, authorized, "JURAT", checkersComment);

                        this.txtCustNoJurat.Text = this.txtCustJNameOfInerpreter.Text = this.txtCustJAddyOfInterperter.Text = this.txtCustJPhoneNo.Text = txtCustJLangOfInterpretation.Text = txtCustJDateOfOath.Text = "";

                        GridView4.DataBind();

                    }
                    else
                    {

                        lblstatusJurat.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblstatusJurat.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void btnApproveEI_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab8";
            this.AuthoriseEmpInfo("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnRejectEI_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab8";
            string CustEIRejectComment = this.HidCustEIComment.Value;
            this.AuthoriseEmpInfo("N", "Record Rejected!!!", System.Drawing.Color.Red, CustEIRejectComment);
        }

        private void AuthoriseEmpInfo(string authorized, string statusMsg, Color color, string checkersComment)
        {
            hidTAB.Value = "#tab8";
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_employment_details";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustNoEmpInfo.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtCustNoEmpInfo.Text, "tmp_employment_details");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_customer_id(txtCustNoEmpInfo.Text) == 0)
                {
                    lblstatusEI.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

                    return;

                }
                else
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();//
                    }

                    int rstCmd = objCmd.ExecuteNonQuery();

                    if (rstCmd == -1)
                    {

                        lblstatusEI.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtCustNoEmpInfo.Text, authorized, "EMPLOYEE INFORMATION", checkersComment);

                        txtCustNoEmpInfo.Text = txtCustEIEmpStatus.Text = txtCustEIEmployerName.Text = txtCustEIDateOfEmp.Text = txtCustEISectorClass.Text = txtCustEISubSector.Text = txtCustEINatureOfBiz.Text = txtCustEIIndustrySeg.Text = "";

                        GridView7.DataBind();

                    }
                    else
                    {

                        lblstatusEI.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblstatusEI.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void btnAproveTCAcct_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";
            this.AuthoriseTCAcct("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnRejectTCAcct_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";
            string CustTCAcctRejectComment = this.HidCustTCAcctComment.Value;
            this.AuthoriseTCAcct("N", "Record Rejected!!!", System.Drawing.Color.Red, CustTCAcctRejectComment);
        }

        private void AuthoriseTCAcct(string authorized, string statusMsg, Color color, string checkersComment)
        {
            hidTAB.Value = "#tab3";
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_trusts_client_accounts";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustNoTCAcct.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtCustNoTCAcct.Text, "tmp_trusts_client_accounts");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_customer_id(txtCustNoTCAcct.Text) == 0)
                {
                    lblstatusTCAcct.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

                    return;

                }
                else
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();//
                    }

                    int rstCmd = objCmd.ExecuteNonQuery();

                    if (rstCmd == -1)
                    {

                        lblstatusTCAcct.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtCustNoTCAcct.Text, authorized, "TRUSTED CLIENT ACCOUNTS", checkersComment);


                        txtCustTCAcctSpouseName.Text = txtCustTCAcctBeneficialName.Text = txtCustTCAcctDOB.Text = txtCustTCAcctOccptn.Text = txtCustTCAcctSrcOfFund.Text = txtCustTCAcctOtherScrIncome.Text = txtCustTCAcctNameOfAssBiz.Text = txtTCAcctHoldersName.Text = txtTCAcctAddress.Text = txtTCAcctTelPhone.Text = "";
                        txtTCAcctCountry.Text = txtTCAcctNationality.Text = "";
                        //rblTCAcct.SelectedValue = rblFreqIntTraveler.SelectedValue = rbtASRTransAlertPref.SelectedValue = rblTCAcctInsiderRelation.SelectedValue = rblTCAcctPolExposed.SelectedValue = rtlTCAcctPowerOfAttony.SelectedValue = 

                        GridView2.DataBind();

                    }
                    else
                    {

                        lblstatusTCAcct.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblstatusTCAcct.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void btnApproveFinInc_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab9";
            this.AuthoriseFinInc("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnRejectFinInc_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab9";
            string CusFinIncRejectComment = this.HidCustFinIncComment.Value;
            this.AuthoriseFinInc("N", "Record Rejected!!!", System.Drawing.Color.Red, CusFinIncRejectComment);
        }

        private void AuthoriseFinInc(string authorized, string statusMsg, Color color, string checkersComment)
        {
            hidTAB.Value = "#tab9";
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_auth_finance_inclusion";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustNoA4FinInc.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtCustNoA4FinInc.Text, "tmp_auth_finance_inclusion");
            
            try
            {

                getCustID rst = new getCustID();

                if (rst.get_customer_id(txtCustNoA4FinInc.Text) == 0)
                {
                    lblCustNoA4FinInc.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

                    return;

                }
                else
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();//
                    }

                    int rstCmd = objCmd.ExecuteNonQuery();

                    if (rstCmd == -1)
                    {

                        lblCustNoA4FinInc.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtCustNoA4FinInc.Text, authorized, "TRUST/CLIENT ACCOUNT", checkersComment);

                        this.txtCustNoA4FinInc.Text = this.txtA4FinIncSocFinDoc.Text = txtA4FinIncRiskCat.Text = txtA4FinIncMandRule.Text = "";
                        this.txtA4FinIncAcctWithOtherBanks.Text = "";

                        GridView8.DataBind();

                    }
                    else
                    {

                        lblCustNoA4FinInc.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblCustNoA4FinInc.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void btnApproveAddInfo_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab10";
            this.AuthoriseAddInfo("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnRejectAddInfo_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab10";
            string CusAddInfoRejectComment = this.HidCustFinIncComment.Value;
            this.AuthoriseAddInfo("N", "Record Rejected!!!", System.Drawing.Color.Red, CusAddInfoRejectComment);
        }

        private void AuthoriseAddInfo(string authorized, string statusMsg, Color color, string checkersComment)
        {
            hidTAB.Value = "#tab10";
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_additional_information";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustNoAddInfo.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtCustNoAddInfo.Text,"tmp_additional_information");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_customer_id(txtCustNoAddInfo.Text) == 0)
                {
                    lblCustAddtnalInfo.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

                    return;

                }
                else
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();//
                    }

                    int rstCmd = objCmd.ExecuteNonQuery();

                    if (rstCmd == -1)
                    {

                        lblCustAddtnalInfo.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtCustNoAddInfo.Text, authorized, "ADDITIONAL INFORMATION", checkersComment);

                        this.txtAddInfoAnnualSalary.Text = this.txtAddInfoFaxNumber.Text = "";

                        GridView9.DataBind();

                    }
                    else
                    {

                        lblCustAddtnalInfo.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblCustAddtnalInfo.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

    }
}