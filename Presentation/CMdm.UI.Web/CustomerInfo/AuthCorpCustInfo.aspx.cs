using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Oracle.DataAccess.Client;
using System.Drawing;
using CMdm.UI.Web.BLL;
using System.Globalization;

namespace Cdma.Web.CustomerInfo
{
    public partial class AuthCorpCustInfo : System.Web.UI.Page
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        public OracleCommand cmd = new OracleCommand();
        public OracleDataReader rdr;
        public OracleDataAdapter adapter = new OracleDataAdapter();
        private CustomerRepository customer;
        private Audit audit = new Audit();
        private CustomerRepository repository;
        protected void Page_Load(object sender, EventArgs e)
        {
            string CurrentUser = (String)(Session["UserID"]);

            this.txtCurrentUser.Text = CurrentUser;
        }


        protected void OnEdit_CompInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetCompanyInfo(recId, "TMP_COMPANY_INFORMATION");

            txtCustomerNo.Text = f.CustomerNo;
            txtCopmanyname.Text = f.CompanyName;
            txtIncopDate.Text = Convert.ToDateTime(f.DateOfIncorpRegistration).ToString("dd/MM/yyyy");
            txtCompanyType.Text = f.CustomerType;
            txtaddress.Text = f.RegisteredAddress;
            txtCatOfBiz.Text = f.BizCategory;

            //
            getCustID rst = new getCustID();
            //

            if (con.State == ConnectionState.Closed)
                con.Open();//

            if (rst.get_corp_customer_id(this.txtCustomerNo.Text) > 0)
            {

                lblmsg_CompanyInfo.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);
            }
        }

        protected void btnApprove_CompInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            this.AuthoriseCustomerInfo("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnReject_CompInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            string CompInfoRejectComment = this.HidCompInfoComment.Value;
            this.AuthoriseCustomerInfo("N", "Record Rejected!!!", System.Drawing.Color.Red, CompInfoRejectComment);
        }

        private void AuthoriseCustomerInfo(string authorized, string statusMsg, Color color, string checkersComment)
        {
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_company_information";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustomerNo.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtCustomerNo.Text, "tmp_company_information");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_corp_customer_id(txtCustomerNo.Text) == 0)
                {
                    lblmsg_CompanyInfo.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

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

                        lblmsg_CompanyInfo.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtCustomerNo.Text, authorized, "COMPANY INFORMATION", checkersComment);

                        txtCustomerNo.Text = txtCopmanyname.Text = txtIncopDate.Text = txtCompanyType.Text = txtaddress.Text = txtCatOfBiz.Text = "";
                        GridView1.DataBind();
                    }
                    else
                    {

                        lblmsg_CompanyInfo.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblmsg_CompanyInfo.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }


        protected void OnEdit_AccountInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";
            LinkButton lnk = sender as LinkButton;

            try
            {
                string recId = lnk.Attributes["RecId"];

                customer = new CustomerRepository();
                var inc = customer.GetAccountInformation(recId, "TMP_CORP_ACCT_SERV_REQUIRED");

                txtCustNoAcctInfo.Text = inc.CustomerNo;
                txtAcctInfoAccttype.Text = inc.AccountType;
                txtAcctInfoDomBranch.Text = inc.DomicileBranch;
                txtAcctInfoReferralCode.Text = inc.ReferralCode;
                txtAcctInfoAcctNo.Text = inc.AccountNo;
                txtAcctInfoAcctName.Text = inc.AccountName;
                //rbtAcctInfoCrdPref.SelectedValue = inc.CardPreference;
                if (inc.CardPreference.Trim() == "Y")
                {
                    this.rbtAcctInfoCrdPref.Items[0].Selected = true;
                }
                else if (inc.CardPreference.Trim() == "N")
                {
                    this.rbtAcctInfoCrdPref.Items[1].Selected = true;
                }
                else
                {
                    this.rbtAcctInfoCrdPref.Items[0].Selected = false;
                    this.rbtAcctInfoCrdPref.Items[1].Selected = false;
                }
                //rbtAcctInfoEBankingPref.SelectedValue = inc.ElectronicBankingPrefer;
                if (inc.ElectronicBankingPrefer.Trim() == "Y")
                {
                    this.rbtAcctInfoEBankingPref.Items[0].Selected = true;
                }
                else if (inc.ElectronicBankingPrefer.Trim() == "N")
                {
                    this.rbtAcctInfoEBankingPref.Items[1].Selected = true;
                }
                else
                {
                    this.rbtAcctInfoEBankingPref.Items[0].Selected = false;
                    this.rbtAcctInfoEBankingPref.Items[1].Selected = false;
                }
                //rbtAcctInfoStatmntPref.SelectedValue = inc.StatementPreferences;
                if (inc.StatementPreferences.Trim() == "Y")
                {
                    this.rbtAcctInfoStatmntPref.Items[0].Selected = true;
                }
                else if (inc.StatementPreferences.Trim() == "N")
                {
                    this.rbtAcctInfoStatmntPref.Items[1].Selected = true;
                }
                else
                {
                    this.rbtAcctInfoStatmntPref.Items[0].Selected = false;
                    this.rbtAcctInfoStatmntPref.Items[1].Selected = false;
                }
                //rbtTranxAlertPref.SelectedValue = inc.TransactionAlertPreference;
                if (inc.TransactionAlertPreference.Trim() == "Y")
                {
                    this.rbtTranxAlertPref.Items[0].Selected = true;
                }
                else if (inc.TransactionAlertPreference.Trim() == "N")
                {
                    this.rbtTranxAlertPref.Items[1].Selected = true;
                }
                else
                {
                    this.rbtTranxAlertPref.Items[0].Selected = false;
                    this.rbtTranxAlertPref.Items[1].Selected = false;
                }
                //rbtAcctInfoStatmntFreq.SelectedValue = inc.StatementPreferences;
                if (inc.StatementPreferences.Trim() == "Y")
                {
                    this.rbtAcctInfoStatmntFreq.Items[0].Selected = true;
                }
                else if (inc.StatementPreferences.Trim() == "N")
                {
                    this.rbtAcctInfoStatmntFreq.Items[1].Selected = true;
                }
                else
                {
                    this.rbtAcctInfoStatmntFreq.Items[0].Selected = false;
                    this.rbtAcctInfoStatmntFreq.Items[1].Selected = false;
                }
                //rbtAcctInfoChequeConfmtnReq.SelectedValue = inc.ChequeConfirmation;
                if (inc.ChequeConfirmation.Trim() == "Y")
                {
                    this.rbtAcctInfoChequeConfmtnReq.Items[0].Selected = true;
                }
                else if (inc.ChequeConfirmation.Trim() == "N")
                {
                    this.rbtAcctInfoChequeConfmtnReq.Items[1].Selected = true;
                }
                else
                {
                    this.rbtAcctInfoChequeConfmtnReq.Items[0].Selected = false;
                    this.rbtAcctInfoChequeConfmtnReq.Items[1].Selected = false;
                }
                //rbtAcctInfoChequeConfmtn.SelectedValue = inc.ChequeConfirmation;
                if (inc.ChequeConfirmation.Trim() == "Y")
                {
                    this.rbtAcctInfoChequeConfmtn.Items[0].Selected = true;
                }
                else if (inc.ChequeConfirmation.Trim() == "N")
                {
                    this.rbtAcctInfoChequeConfmtn.Items[1].Selected = true;
                }
                else
                {
                    this.rbtAcctInfoChequeConfmtn.Items[0].Selected = false;
                    this.rbtAcctInfoChequeConfmtn.Items[1].Selected = false;
                }
                //rbtAcctInfoChequeConfmtnThreshold.SelectedValue = inc.ChequeConfirmThreshold;
                if (inc.ChequeConfirmThreshold.Trim() == "Y")
                {
                    this.rbtAcctInfoChequeConfmtnThreshold.Items[0].Selected = true;
                }
                else if (inc.ChequeConfirmThreshold.Trim() == "N")
                {
                    this.rbtAcctInfoChequeConfmtnThreshold.Items[1].Selected = true;
                }
                else
                {
                    this.rbtAcctInfoChequeConfmtnThreshold.Items[0].Selected = false;
                    this.rbtAcctInfoChequeConfmtnThreshold.Items[1].Selected = false;
                }


                getCustID rst = new getCustID();
                //

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(this.txtCustomerNo.Text) > 0)
                {

                    lblAccountInfo.Text = inc.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(inc.ErrorMessage);
                }



            }
            catch (Exception ex)
            {

                lblAccountInfo.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }


        }
        protected void btnApprove_AccountInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";
            this.AuthoriseAccountInfo("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnReject_AccountInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";
            string AccountInfoRejectComment = this.HidAccountInfoComment.Value;
            this.AuthoriseAccountInfo("N", "Record Rejected!!!", System.Drawing.Color.Red, AccountInfoRejectComment);
        }
        private void AuthoriseAccountInfo(string authorized, string statusMsg, Color color, string checkersComment)
        {
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_corp_acct_service_require";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustNoAcctInfo.Text;
            objCmd.Parameters.Add("p_account_number", OracleDbType.Varchar2).Value = txtAcctInfoAcctNo.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtCustNoAcctInfo.Text, "TMP_CORP_ACCT_SERV_REQUIRED");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_corp_customer_id(txtCustNoAcctInfo.Text) == 0)
                {
                    lblAccountInfo.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

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

                        lblAccountInfo.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtCustNoAcctInfo.Text, authorized, "ACCOUNT INFORMATION", checkersComment);
                        
                        txtCustNoAcctInfo.Text = txtAcctInfoDomBranch.Text = txtAcctInfoAccttype.Text = txtAcctInfoReferralCode.Text = txtAcctInfoAcctNo.Text = txtAcctInfoAcctName.Text = rbtAcctInfoCrdPref.SelectedValue = rbtAcctInfoEBankingPref.SelectedValue = rbtAcctInfoStatmntPref.SelectedValue = rbtTranxAlertPref.SelectedValue = rbtAcctInfoStatmntFreq.Text = rbtAcctInfoChequeConfmtnReq.SelectedValue = rbtAcctInfoChequeConfmtn.SelectedValue = rbtAcctInfoChequeConfmtnThreshold.SelectedValue = "";
                        GridView3.DataBind();
                    }
                    else
                    {

                        lblAccountInfo.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("fetch returns more than requested number of rows"))
                {
                    lblAccountInfo.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                    Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtCustNoAcctInfo.Text, authorized, "ACCOUNT INFORMATION", checkersComment);

                    txtCustNoAcctInfo.Text = txtAcctInfoDomBranch.Text = txtAcctInfoAccttype.Text = txtAcctInfoReferralCode.Text = txtAcctInfoAcctNo.Text = txtAcctInfoAcctName.Text = rbtAcctInfoCrdPref.SelectedValue = rbtAcctInfoEBankingPref.SelectedValue = rbtAcctInfoStatmntPref.SelectedValue = rbtTranxAlertPref.SelectedValue = rbtAcctInfoStatmntFreq.Text = rbtAcctInfoChequeConfmtnReq.SelectedValue = rbtAcctInfoChequeConfmtn.SelectedValue = rbtAcctInfoChequeConfmtnThreshold.SelectedValue = "";
                    GridView3.DataBind();
                }
                else
                {
                    lblAccountInfo.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
                }
            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void OnEdit_CompDetail(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab2";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            try
            {
                var f = customer.GetCompanyDetails(recId, "TMP_COMPANY_DETAILS");

                txtCustomerNoCompDetails.Text = f.CustomerNo.ToString();
                txtCompDRegNo.Text = f.CertOfIncorpRegNo;
                txtCompDJurOfInc.Text = f.JurisdictionOfIncorpReg;
                txtCompDSCUMLNo.Text = f.ScumlNo;
                //rbtCompDGender.Text = f.GenderControlling51Perc;
                if (f.GenderControlling51Perc.Trim() == "Male")
                {
                    this.rbtCompDGender.Items[0].Selected = true;
                }
                else if (f.GenderControlling51Perc.Trim() == "Female")
                {
                    this.rbtCompDGender.Items[1].Selected = true;
                }
                else
                {
                    this.rbtCompDGender.Items[0].Selected = false;
                    this.rbtCompDGender.Items[1].Selected = false;
                }
                txtCompDSector.Text = f.SectorOrIndustry.ToString();
                txtCompDOpBiz1.Text = f.OperatingBusiness1;
                txtCompDCity1.Text = f.City1;
                txtCompDCountry1.Text = f.Country1;
                txtCompDZipCode1.Text = f.ZipCode1;
                txtCompDBizAddy1.Text = f.BizAddressRegOffice1;
                txtCompDOpBiz1.Text = f.OperatingBusiness2;
                txtCompDCity1.Text = f.City2;
                txtCompDCountry2.Text = f.Country2;
                txtCompDZipCode2.Text = f.ZipCode2;
                txtCompDBizAddy2.Text = f.BizAddressRegOffice2;
                txtCompDCompEmailAddy.Text = f.CompanyEmailAddress;
                txtCompDWebsite.Text = f.Website;
                txtCompDOfficeNo.Text = f.OfficeNumber.ToString();
                txtCompDMobineNo.Text = f.MobileNumber.ToString();
                txtCompDTIN.Text = f.Tin;
                txtCompDBorrwerCode.Text = f.CrmbNoBorrowerCode;
                txtCompDAnnTurnover.Text = f.ExpectedAnnualTurnover;
                //rbtCompDOnStckExnge.Text = f.IsCompanyOnStockExch;
                if (f.IsCompanyOnStockExch.Trim() == "Y")
                {
                    this.rbtCompDOnStckExnge.Items[0].Selected = true;
                }
                else if (f.IsCompanyOnStockExch.Trim() == "N")
                {
                    this.rbtCompDOnStckExnge.Items[1].Selected = true;
                }
                else
                {
                    this.rbtCompDOnStckExnge.Items[0].Selected = false;
                    this.rbtCompDOnStckExnge.Items[1].Selected = false;
                }
                txtCompDStkExhange.Text = f.StockExchangeName;


                getCustID rst = new getCustID();
                //

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(this.txtCustomerNo.Text) > 0)
                {

                    lblCompDetails.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);
                }


            }
            catch (Exception ex)
            {
                lblCompDetails.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }


        }

        protected void btnApprove_CompDetail(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab2";
            this.AuthoriseCustomerDetails("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnReject_CompDetail(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab2";
            string CompDetailRejectComment = this.HidCompDetailComment.Value;
            this.AuthoriseCustomerDetails("N", "Record Rejected!!!", System.Drawing.Color.Red, CompDetailRejectComment);
        }

        private void AuthoriseCustomerDetails(string authorized, string statusMsg, Color color, string checkersComment)
        {
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_company_details";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustomerNoCompDetails.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtCustomerNoCompDetails.Text, "TMP_COMPANY_DETAILS");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_corp_customer_id(txtCustomerNoCompDetails.Text) == 0)
                {
                    lblCompDetails.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

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

                        lblCompDetails.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtCustomerNoCompDetails.Text, authorized, "COMPANY DETAILS", checkersComment);

                        txtCompDRegNo.Text = txtCompDJurOfInc.Text = txtCompDSCUMLNo.Text = rbtCompDGender.Text = txtCompDSector.Text = txtCompDOpBiz1.Text = txtCompDCountry1.Text = txtCompDCity1.Text = "";
                        txtCompDZipCode1.Text = txtCompDOpBiz2.Text = txtCompDCountry2.Text = txtCompDBizAddy1.Text = txtCompDCity2.Text = "";
                        txtCompDZipCode2.Text = txtCompDBizAddy2.Text = "";
                        txtCompDCompEmailAddy.Text = txtCompDWebsite.Text = rbtCompDOnStckExnge.SelectedValue = txtCompDOfficeNo.Text = txtCompDMobineNo.Text = txtCompDTIN.Text = txtCompDStkExhange.Text = "";
                        GridView4.DataBind();

                    }
                    else
                    {

                        lblCompDetails.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblCompDetails.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void OnEdit_DIRAddressInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab11";
            LinkButton lnk = sender as LinkButton;
            try { 
            string[] arguments = lnk.CommandArgument.Split(';');
            string customer_no = arguments[0];
            string Mngt_no = arguments[1];
            //string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetCorpAddressInfo(customer_no.Trim(), Mngt_no.Trim(), "TMP_MANAGEMENT_ADDRESS");

            txtDIRAddyCustNo.Text = f.CustomerNo;
            txtDIRAddyManID.Text = Convert.ToString(f.ManagementNo);
            txtDIRAddyHouseNo.Text = f.HomeNo;
            txtDIRAddyStreetName.Text = f.StreetName;
            txtDIRAddyBStop.Text = f.NearestBstop;
            txtDIRAddyLGA.Text = f.LGA;
            txtDIRAddyCity.Text = f.City;
            txtDIRAddyState.Text = f.State;
            txtDIRAddyCountry.Text = f.Country;
            txtDIRAddyOfficeNo.Text = f.OfficeNo;
            txtDIRAddyMobileNo.Text = f.MobileNo;
            txtDIRAddyEmailAddy.Text = f.Email;

            getCustID rst = new getCustID();
            //

            if (con.State == ConnectionState.Closed)
                con.Open();//

            //
            if (rst.get_corp_customer_id(this.txtDIRAddyCustNo.Text) > 0)
            {

                lblDIRAddress.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);
            }
            }
            catch (Exception ex)
            {
                lblDIRAddress.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }
        }
        protected void btnApprove_DIRAddress(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab11";
            this.AuthoriseCorpAddressInfo("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }
        protected void btnReject_DIRAddress(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab11";
            string DIRAddressRejectComment = this.HidDIRAddressComment.Value;
            this.AuthoriseCorpAddressInfo("N", "Record Rejected!!!", System.Drawing.Color.Red, DIRAddressRejectComment);
        }
        private void AuthoriseCorpAddressInfo(string authorized, string statusMsg, Color color, string checkersComment)
        {
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_management_address";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtDIRAddyCustNo.Text;
            objCmd.Parameters.Add("p_management_id", OracleDbType.Varchar2).Value = txtDIRAddyManID.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtDIRAddyCustNo.Text, "TMP_MANAGEMENT_ADDRESS");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_corp_customer_id(txtDIRAddyCustNo.Text) == 0)
                {
                    lblDIRAddress.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

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

                        lblDIRAddress.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtDIRAddyCustNo.Text, authorized, "MANAGEMENT ADDRESS", checkersComment);


                        txtDIRAddyCustNo.Text = txtDIRAddyManID.Text = txtDIRAddyHouseNo.Text = txtDIRAddyStreetName.Text = txtDIRAddyBStop.Text = txtDIRAddyLGA.Text = txtDIRAddyCity.Text = txtDIRAddyState.Text = txtDIRAddyCountry.Text = txtDIRAddyOfficeNo.Text = txtDIRAddyMobileNo.Text = txtDIRAddyEmailAddy.Text = "";
                        GridView11.DataBind();
                    }
                    else
                    {

                        lblDIRAddress.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblDIRAddress.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void OnEdit_DIRBioInfo(object sender, EventArgs e)
        {

            hidTAB.Value = "#tab6";
            LinkButton lnk = sender as LinkButton;
            try
            {
                string[] arguments = lnk.CommandArgument.Split(';');
                string customer_no = arguments[0];
                string Mngt_no = arguments[1];
                //string recId = lnk.Attributes["RecId"];
                customer = new CustomerRepository();
                var f = customer.GetCorpBiodataInfo(customer_no.Trim(), Mngt_no.Trim(), "TMP_MANAGEMENT_BIO_DATA");

                txtDIRCustNo.Text = f.CustomerNo;
                txtDIRMngtNo.Text = Convert.ToString(f.ManagementNo);
                txtDIRType.Text = f.ManagementType;
                txtDIRTitle.Text = f.Title;
                txtDIRMStatus.Text = f.MaritalStatus;
                txtDIRSurname.Text = f.Surname;
                txtDIRFirstName.Text = f.FirstName;
                txtDIROtherName.Text = f.Othernames;
                txtDIRDOB.Text = f.DOB;
                txtDIRPlaceOfBirth.Text = f.POB;
                txtDIRSex.Text = f.Sex;
                txtDIRNationality.Text = f.Nationality;
                txtDIRMMName.Text = f.MothersMaidenName;
                txtDIROccupation.Text = f.Occupation;
                txtDIRJobTitle.Text = f.JobTitle;
                txtDIRClassOfSign.Text = f.ClassOfSignitory;

                getCustID rst = new getCustID();
                //

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(this.txtDIRCustNo.Text) > 0)
                {

                    lblDIRMsg.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                lblDIRMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message + ex.StackTrace);
            }
        }

        protected void btnApprove_DIRBio(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab6";
            this.AuthoriseBiodataInfo("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnReject_DIRBio(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab6";
            string DIRBioRejectComment = this.HidDIRBioComment.Value;
            this.AuthoriseBiodataInfo("N", "Record Rejected!!!", System.Drawing.Color.Red, DIRBioRejectComment);
        }

        private void AuthoriseBiodataInfo(string authorized, string statusMsg, Color color, string checkersComment)
        {
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_management_bio_data";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtDIRCustNo.Text;
            objCmd.Parameters.Add("p_management_id", OracleDbType.Varchar2).Value = txtDIRMngtNo.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtDIRCustNo.Text, "TMP_MANAGEMENT_BIO_DATA");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_corp_customer_id(txtDIRCustNo.Text) == 0)
                {
                    lblDIRMsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

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

                        lblDIRMsg.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtDIRCustNo.Text, authorized, "MANAGEMENT BIODATA", checkersComment);


                        txtDIRCustNo.Text = txtDIRMngtNo.Text = txtDIRType.Text = txtDIRTitle.Text = txtDIRMStatus.Text = txtDIRSurname.Text = txtDIRFirstName.Text = txtDIROtherName.Text = txtDIRDOB.Text = txtDIRPlaceOfBirth.Text = txtDIRSex.Text = txtDIRNationality.Text = txtDIRMMName.Text = txtDIROccupation.Text = txtDIRJobTitle.Text = txtDIRClassOfSign.Text = "";
                        GridView6.DataBind();
                    }
                    else
                    {

                        lblDIRMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblDIRMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void OnEdit_CompAddInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab10";
            LinkButton lnk = sender as LinkButton;
            try
            {
                string recId = lnk.Attributes["RecId"];
                customer = new CustomerRepository();
                var f = customer.GetCorpAdditionalInfo(recId, "TMP_CORP_ADDITIONAL_DETAILS");

                txtAddInfoCustID.Text = f.CustomerNo;
                txtAddInfoAffltdCompName.Text = f.AffliliatedCompBody;
                txtAddInfoCountry.Text = f.ParentCompanyIncCountry;


                getCustID rst = new getCustID();
                //

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(this.txtAddInfoCustID.Text) > 0)
                {

                    lblCompAddInfoMsg.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                lblCompAddInfoMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }

        }

        protected void btnApprove_AddInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab10";
            this.AuthoriseAdditionalInfo("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnReject_AddInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab10";
            string AddInfoRejectComment = this.HidAddInfoComment.Value;
            this.AuthoriseAdditionalInfo("N", "Record Rejected!!!", System.Drawing.Color.Red, AddInfoRejectComment);
        }

        private void AuthoriseAdditionalInfo(string authorized, string statusMsg, Color color, string checkersComment)
        {
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_additional_information";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtAddInfoCustID.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtAddInfoCustID.Text, "TMP_CORP_ADDITIONAL_DETAILS");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_corp_customer_id(txtAddInfoCustID.Text) == 0)
                {
                    lblCompAddInfoMsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

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

                        lblCompAddInfoMsg.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtAddInfoCustID.Text, authorized, "COMPANY ADDITIONAL INFORMATION", checkersComment);

                        txtAddInfoCustID.Text = txtAddInfoAffltdCompName.Text = txtAddInfoCountry.Text = "";
                        GridView10.DataBind();
                    }
                    else
                    {

                        lblCompAddInfoMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblCompAddInfoMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void OnEdit_DIRIDInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab7";
            LinkButton lnk = sender as LinkButton;
            try
            {
                string[] arguments = lnk.CommandArgument.Split(';');
                string customer_no = arguments[0];
                string Mngt_no = arguments[1];

                //string recId = lnk.Attributes["RecId"];
                customer = new CustomerRepository();
                var f = customer.GetCorpIDInfo(customer_no.Trim(), Mngt_no.Trim(), "TMP_MANAGEMENT_IDENTIFIER");

                txtDIRDICustNo.Text = f.CustomerNo;
                txtDIRDIManID.Text = Convert.ToString(f.ManagementNo);
                txtDIRIDTypeOfID.Text = f.TypeOfID;
                txtDIRIDNo.Text = f.IDNo;
                txtDIRIDIssueDate.Text = Convert.ToString(f.IDIssueDate);
                txtDIRIDExpiryDate.Text = Convert.ToString(f.IDExpiryDate);
                txtDIRIDBVNID.Text = f.BVNID;
                txtDIRIDTIN.Text = f.TIN;

                getCustID rst = new getCustID();
                //

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(this.txtDIRDICustNo.Text) > 0)
                {

                    lblDIRIDMsg.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                lblDIRIDMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }
        }

        protected void btnApprove_DIRID(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab7";
            this.AuthoriseIdentityInfo("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnReject_DIRID(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab7";
            string DIRIDRejectComment = this.HidDIRIDComment.Value;
            this.AuthoriseIdentityInfo("N", "Record Rejected!!!", System.Drawing.Color.Red,DIRIDRejectComment);
        }

        private void AuthoriseIdentityInfo(string authorized, string statusMsg, Color color, string checkersComment)
        {
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_management_identifier";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtDIRDICustNo.Text;
            objCmd.Parameters.Add("p_management_id", OracleDbType.Varchar2).Value = txtDIRDIManID.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtDIRDICustNo.Text, "TMP_MANAGEMENT_IDENTIFIER");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_corp_customer_id(txtDIRDICustNo.Text) == 0)
                {
                    lblDIRIDMsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

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

                        lblDIRIDMsg.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtAddInfoCustID.Text, authorized, "MANAGEMENT IDENTIFICATION", checkersComment);

                        txtDIRDICustNo.Text = txtDIRDIManID.Text = txtDIRIDTypeOfID.Text = txtDIRIDNo.Text = txtDIRIDIssueDate.Text = txtDIRIDExpiryDate.Text = txtDIRIDBVNID.Text = txtDIRIDTIN.Text = "";
                        GridView7.DataBind();
                    }
                    else
                    {

                        lblDIRIDMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblDIRIDMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void OnEdit_AWOB(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab5";
            LinkButton lnk = sender as LinkButton;
            try
            {
                string recId = lnk.Attributes["RecId"];
                customer = new CustomerRepository();
                var f = customer.GetCorpAWOBInfo(recId, "TMP_ACCT_HELD_WITH_OTHER_BANK");

                txtAWOBCustNo.Text = f.CustomerNo;
                txtAWOBAcctNo.Text = f.AccountNo;
                txtAWOBAcctName.Text = f.AccountName;
                txtAWOBBankName.Text = f.BankName;
                txtAWOBBankAddy.Text = f.BankAddress;
                txtAWOBStatus.Text = f.Status;

                getCustID rst = new getCustID();
                //

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(this.txtAWOBCustNo.Text) > 0)
                {

                    lblAWOBmsg.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                lblAWOBmsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }
        }

        protected void btnApprove_AWOB(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab5";
            this.AuthoriseAWOBInfo("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnReject_AWOB(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab5";
            string AWOBRejectComment = this.HidAWOBComment.Value;
            this.AuthoriseAWOBInfo("N", "Record Rejected!!!", System.Drawing.Color.Red, AWOBRejectComment);
        }

        private void AuthoriseAWOBInfo(string authorized, string statusMsg, Color color, string checkersComment)
        {
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_acct_held_with_other_bank";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtAWOBCustNo.Text;
            objCmd.Parameters.Add("p_bank_name", OracleDbType.Varchar2).Value = txtAWOBAcctName.Text;
            objCmd.Parameters.Add("p_account_number", OracleDbType.Varchar2).Value = txtAWOBAcctNo.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtAWOBCustNo.Text, "TMP_ACCT_HELD_WITH_OTHER_BANK");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_corp_customer_id(txtAWOBCustNo.Text) == 0)
                {
                    lblAWOBmsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

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

                        lblAWOBmsg.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtAWOBCustNo.Text, authorized, "ACCOUNT HELD WITH OTHER BANKS", checkersComment);

                        txtAWOBCustNo.Text = txtAWOBAcctNo.Text = txtAWOBAcctName.Text = txtAWOBBankName.Text = txtAWOBBankAddy.Text = txtAWOBStatus.Text = "";
                        GridView5.DataBind();
                    }
                    else
                    {

                        lblAWOBmsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblAWOBmsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void OnEdit_ForeignerInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab8";
            LinkButton lnk = sender as LinkButton;
            try { 
            string[] arguments = lnk.CommandArgument.Split(';');
            string customer_no = arguments[0];
            string Mngt_no = arguments[1];
            //string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetCorpForeignerInfo(customer_no.Trim(), Mngt_no.Trim(), "TMP_MANAGEMENT_FOREIGNER");

            txtDIRForeginCustNo.Text = f.CustomerNo;
            txtDIRFMngtID.Text = Convert.ToString(f.ManagementNo);
            //rbtDIRForeigner.Text = f.isForeigner;
            if (f.isForeigner.Trim() == "Y")
            {
                this.rbtDIRForeigner.Items[0].Selected = true;
            }
            else if (f.isForeigner.Trim() == "N")
            {
                this.rbtDIRForeigner.Items[1].Selected = true;
            }
            else
            {
                this.rbtDIRForeigner.Items[0].Selected = false;
                this.rbtDIRForeigner.Items[1].Selected = false;
            }
            txtDIRFPermitNo.Text = f.ResidentPermitNo;
            txtDIRFNationality.Text = f.Nationality;
            txtDIRFPIssueDate.Text = Convert.ToString(f.PermitIssueDate);
            txtDIRFPExpiryDate.Text = Convert.ToString(f.PermitExpiryDate);
            txtDIRMFTelephoneNo.Text = f.ForeignTelNo;
            txtDIRMResPermitNo.Text = f.PassportResidentPermitNo;
            txtDIRMForeignAddy.Text = f.ForeignAddy;
            txtDIRMCity.Text = f.City;
            txtDIRMCountry.Text = f.Country;
            txtDIRMZipCode.Text = f.ZipPostalCode;


            getCustID rst = new getCustID();
            //

            if (con.State == ConnectionState.Closed)
                con.Open();//

            //
            if (rst.get_corp_customer_id(this.txtDIRForeginCustNo.Text) > 0)
            {

                lblDIRForeignerMsg.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);
            }
            }
            catch (Exception ex)
            {
                lblDIRForeignerMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }
        }

        protected void btnApprove_Foreigner(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab8";
            this.AuthoriseForeignerInfo("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnReject_Foreigner(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab8";
            string ForeignerRejectComment = this.HidForeignerComment.Value;
            this.AuthoriseForeignerInfo("N", "Record Rejected!!!", System.Drawing.Color.Red, ForeignerRejectComment);
        }

        private void AuthoriseForeignerInfo(string authorized, string statusMsg, Color color, string checkersComment)
        {
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_management_next_of_kin";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtDIRForeginCustNo.Text;
            objCmd.Parameters.Add("p_management_id", OracleDbType.Varchar2).Value = txtDIRFMngtID.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtDIRForeginCustNo.Text, "TMP_MANAGEMENT_FOREIGNER");


            try
            {

                getCustID rst = new getCustID();

                if (rst.get_corp_customer_id(txtDIRForeginCustNo.Text) == 0)
                {
                    lblDIRForeignerMsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

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

                        lblDIRForeignerMsg.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtDIRForeginCustNo.Text, authorized, "MANAGEMENT FORIEGN DETAILS", checkersComment);

                        txtDIRForeginCustNo.Text = txtDIRFMngtID.Text = rbtDIRForeigner.SelectedValue = rbtDIRMultipleCitizenship.SelectedValue = txtDIRFNationality.Text = txtDIRFPIssueDate.Text = txtDIRFPExpiryDate.Text = txtDIRMFTelephoneNo.Text = txtDIRMResPermitNo.Text = txtDIRMForeignAddy.Text = txtDIRMCity.Text = txtDIRMCountry.Text = txtDIRMZipCode.Text = "";
                        GridView8.DataBind();
                    }
                    else
                    {

                        lblDIRForeignerMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblDIRForeignerMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void OnEdit_DIRNOKInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab9";
            LinkButton lnk = sender as LinkButton;
            try { 
            string[] arguments = lnk.CommandArgument.Split(';');
            string customer_no = arguments[0];
            string Mngt_no = arguments[1];

            //string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetCorpNOKInfo(customer_no.Trim(), Mngt_no.Trim(), "TMP_MANAGEMENT_NEXT_OF_KIN");

            txtDIRNOKCustNo.Text = f.CustomerNo;
            txtDIRNOKMngtID.Text = Convert.ToString(f.ManagementNo);
            txtDIRNOKTitle.Text = f.Title;
            txtDIRNOKSurame.Text = f.Surname;
            txtDIRNOKFirstName.Text = f.FirstName;
            txtDIRNOKOtherName.Text = Convert.ToString(f.Othernames);
            txtDIRNOKDOB.Text = Convert.ToString(f.DOB);
            txtDIRNOKSex.Text = f.Sex;
            txtDIRNOKRelationship.Text = f.Relationship;
            txtDIRNOKOfficeNo.Text = f.OfficeNo;
            txtDIRNOKMobileNo.Text = f.MobileNo;
            txtDIRNOKEmailAddy.Text = f.Email;
            txtDIRNOKHouseNo.Text = f.HouseNo;
            txtDIRNOKStrName.Text = f.StreetName;
            txtDIRNOKBusStop.Text = f.NearestBStop;
            txtDIRNOKCity.Text = f.City;
            txtDIRNOKLGA.Text = f.LGA;
            txtDIRNOKZip.Text = f.ZipPostalCode;
            txtDIRNOKState.Text = f.State;
            txtDIRNOKCountry.Text = f.Country;


            getCustID rst = new getCustID();
            //

            if (con.State == ConnectionState.Closed)
                con.Open();//

            //
            if (rst.get_corp_customer_id(this.txtDIRNOKCustNo.Text) > 0)
            {

                lblDIRNOKmsg.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);
            }
            }
            catch (Exception ex)
            {
                lblDIRNOKmsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }
        }

        protected void btnApprove_DIRNOK(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab9";
            this.AuthoriseNOKInfo("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnReject_DIRNOK(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab9";
            string DIRNOKRejectComment = this.HidDIRNOKComment.Value;
            this.AuthoriseNOKInfo("N", "Record Rejected!!!", System.Drawing.Color.Red, DIRNOKRejectComment);
        }

        private void AuthoriseNOKInfo(string authorized, string statusMsg, Color color,string checkersComment)
        {
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_management_next_of_kin";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtDIRNOKCustNo.Text;
            objCmd.Parameters.Add("p_management_id", OracleDbType.Varchar2).Value = txtDIRNOKMngtID.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtDIRNOKCustNo.Text, "TMP_MANAGEMENT_NEXT_OF_KIN");


            try
            {

                getCustID rst = new getCustID();

                if (rst.get_corp_customer_id(txtDIRNOKCustNo.Text) == 0)
                {
                    lblDIRNOKmsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

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

                        lblDIRNOKmsg.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtDIRNOKCustNo.Text, authorized, "MANAGEMENT NEXT OF KIN", checkersComment);

                        txtDIRNOKCustNo.Text = txtDIRNOKMngtID.Text = txtDIRNOKTitle.Text = txtDIRNOKSurame.Text = txtDIRNOKFirstName.Text = txtDIRNOKOtherName.Text = txtDIRNOKDOB.Text = txtDIRNOKSex.Text = txtDIRNOKRelationship.Text = txtDIRNOKOfficeNo.Text = txtDIRNOKMobileNo.Text = txtDIRNOKEmailAddy.Text = txtDIRNOKHouseNo.Text = txtDIRNOKStrName.Text = txtDIRNOKBusStop.Text = txtDIRNOKCity.Text = txtDIRNOKLGA.Text = txtDIRNOKZip.Text = txtDIRNOKCountry.Text = "";
                        GridView9.DataBind();
                    }
                    else
                    {

                        lblDIRNOKmsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblDIRNOKmsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void OnEdit_POAInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab4";
            LinkButton lnk = sender as LinkButton;


            string[] arguments = lnk.CommandArgument.Split(';');
            string customer_no = arguments[0];
            string account_no = arguments[1];
            //string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetCorpPOAInfo(customer_no.Trim(), account_no.Trim(), "TMP_POWER_OF_ATTORNEY");

            txtPOACustNo.Text = f.CustomerNo;
            txtPOAAccountNo.Text = f.AccountNo;
            txtPOAHolderName.Text = f.HoldersNAme;
            txtPOAAddy.Text = f.Address;
            txtPOACountry.Text = f.Country;
            txtPOANationality.Text = f.Nationality;
            txtPOAPhoneNo.Text = f.Telephone;


            getCustID rst = new getCustID();
            //

            if (con.State == ConnectionState.Closed)
                con.Open();//

            //
            if (rst.get_corp_customer_id(this.txtPOACustNo.Text) > 0)
            {

                lblPOAMsg.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);
            }

        }

        protected void btnApprove_POA(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab4";
            this.AuthorisePOAInfo("Y", "Record Approved successfully!!!", System.Drawing.Color.Green,"");
        }

        protected void btnReject_POA(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab4";
            string POARejectComment = this.HidPOAComment.Value;
            this.AuthorisePOAInfo("N", "Record Rejected!!!", System.Drawing.Color.Red, POARejectComment);
        }

        private void AuthorisePOAInfo(string authorized, string statusMsg, Color color, string checkersComment)
        {
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_checker.prc_power_of_attorney";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtPOACustNo.Text;
            objCmd.Parameters.Add("p_account_number", OracleDbType.Varchar2).Value = txtPOAAccountNo.Text;
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
            objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";
            //
            EmailHelper Emailer = new EmailHelper();

            string makerName = Emailer.getIndivMakerwithCustNo(txtPOACustNo.Text, "TMP_POWER_OF_ATTORNEY");

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_corp_customer_id(txtPOACustNo.Text) == 0)
                {
                    lblPOAMsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

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

                        lblPOAMsg.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                        Emailer.C2MNotificationMailSender((String)(Session["UserID"]), makerName, txtPOACustNo.Text, authorized, "POWER OF ATTORNEY", checkersComment);


                        txtPOACustNo.Text = txtPOAAccountNo.Text = txtPOAHolderName.Text = txtPOAAddy.Text = txtPOACountry.Text = txtPOANationality.Text = txtPOAPhoneNo.Text = "";
                        GridView2.DataBind();
                    }
                    else
                    {

                        lblPOAMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblPOAMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();

            };
        }

        protected void btnSearchCompInfo_Click(object sender, EventArgs e)
        {

        }

    }
}