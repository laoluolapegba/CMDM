//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Collections;
//using System.Web.Profile;
//using System.Web.Security;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
//using System.Xml;
//using System.Xml.Linq;
//using System.Linq.Expressions;
//using System.Reflection;
//using Oracle.DataAccess.Client;
//using System.Drawing;
//using CMdm.UI.Web.BLL;
//using System.Globalization;
//using System.Text;
//using System.Net.Mail;
//using Cdma.Web.Properties;
//using System.Net;
//using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Oracle.DataAccess.Client;
using System.Drawing;
using CMdm.UI.Web.BLL;
using System.Globalization;
using CMdm.UI.Web.Helpers.CrossCutting.Security;


namespace Cdma.Web.CustomerInfo
{
    public partial class IndividualCustomer : System.Web.UI.Page
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        public OracleCommand cmd = new OracleCommand();
        public OracleDataReader rdr;
        public OracleDataAdapter adapter = new OracleDataAdapter();
        //private CustomerRepository family;
        private CustomerRepository customer;
        private Audit audit = new Audit();
        public EmailHelper Email;
        public CustomMembershipProvider mp = new CustomMembershipProvider();

        protected void Page_Init(object sender, EventArgs e)
        {

            this.txtCustNoTCAcct.Enabled = false;
            //this.rblTCAcct.Enabled= false;
            this.txtCustTCAcctBeneficialName.Enabled = false;
            this.txtCustTCAcctSpouseName.Enabled = false;
            this.txtCustTCAcctDOB.Enabled = false;
            this.txtCustTCAcctOccptn.Enabled = false;
            this.txtCustTCAcctOtherScrIncome.Enabled = false;
            this.rblTCAcctInsiderRelation.Enabled = false;
            this.txtCustTCAcctNameOfAssBiz.Enabled = false;
            this.rblFreqIntTraveler.Enabled = false;
            this.rblTCAcctPolExposed.Enabled = false;
            this.rtlTCAcctPowerOfAttony.Enabled = false;
            this.txtTCAcctHoldersName.Enabled = false;
            this.txtTCAcctAddress.Enabled = false;
            this.ddlTCAcctCountry.Enabled = false;
            this.ddlTCAcctNationality.Enabled = false;
            this.txtTCAcctTelPhone.Enabled = false;
            this.txtCustTCAcctSrcOfFund.Enabled = false;

        }

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
        //    // your code!
        //    if (txtDateOfBirth.Text != string.Empty || txtDateOfBirth.Text != null)
        //    {
        //        try
        //        {
        //            DateTime dob = DateTime.ParseExact(this.txtDateOfBirth.Text, "dd/MM/yyyy", null);//Convert.ToDateTime(txtDateOfBirth.Text);
        //            DateTime presentYear = DateTime.Now;
        //            TimeSpan ts = presentYear - dob;
        //            DateTime Age = DateTime.MinValue.AddDays(ts.Days);
        //            int age = Age.Year - 1;
        //            this.txtAge.Text = Convert.ToString(age);
        //        }
        //        catch (Exception ex)
        //        {
        //            return;
        //        }
        //    }
        //}


        protected void Page_Load(object sender, EventArgs e)
        {

            #region PageLoad
            //Hide controls
            this.txtCountryofBirth.Visible = false;
            this.txtStateOfResidence.Visible = false;
            this.txtIDType.Visible = false;
            this.txtCustEINatureOfBiz.Visible = false;
            this.txtStateOfOrigin.Visible = false;
            this.txtLGAOfResidence.Visible = false;
            this.txtCustNOKIdType.Visible = false;
            this.txtCustNOKLGA.Visible = false;
            this.txtCustNOKState.Visible = false;
            //Disable controls
            //this.rbtMultipleCitizenship.Enabled = false;
            //show controls
            this.ddlStateOfResidence.Visible = true;
            this.ddlLGAofResidence.Visible = true;
            this.ddlCustNOKLGA.Visible = true;
            this.ddlCustNOKLGA.Visible = true;
            this.ddlCustNOKState.Visible = true;
            this.ddlStateOfOrigin.Visible = true;

            //string recId = this.Request.
           

            if (!IsPostBack)
            {
                ddlStateOfResidence.Items.Clear();
                //ddlStateOfResidence.Items.Add(new ListItem("--Select State--", ""));
                ddlLGAofResidence.Items.Clear();
                //ddlLGAofResidence.Items.Add(new ListItem("--Select LGA--", ""));

                this.ddlCountryofResidence.AppendDataBoundItems = true;
                //this.ddlCountryOfBirth.AppendDataBoundItems = true;
                //String strConnString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
                String strQuery = "SELECT COUNTRY_NAME,COUNTRY_ABBREVIATION FROM CDMA_COUNTRIES";
                OracleCommand objCmd = new OracleCommand();
                //OracleCommand objCmd2 = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strQuery;
                objCmd.Connection = con;
                try
                {
                    con.Open();
                    ddlCountryofResidence.DataSource = objCmd.ExecuteReader();
                    ddlCountryofResidence.DataTextField = "COUNTRY_NAME";
                    ddlCountryofResidence.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlCountryofResidence.DataBind();
                    ddlCountryofResidence.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));

                    //con.Open();
                    ddlCountryOfBirth.DataSource = objCmd.ExecuteReader();
                    ddlCountryOfBirth.DataTextField = "COUNTRY_NAME";
                    ddlCountryOfBirth.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlCountryOfBirth.DataBind();
                    ddlCountryOfBirth.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));

                    ddlNationality.DataSource = objCmd.ExecuteReader();
                    ddlNationality.DataTextField = "COUNTRY_NAME";
                    ddlNationality.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlNationality.DataBind();
                    ddlNationality.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));

                    ddlCustNOKCountry.DataSource = objCmd.ExecuteReader();
                    ddlCustNOKCountry.DataTextField = "COUNTRY_NAME";
                    ddlCustNOKCountry.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlCustNOKCountry.DataBind();
                    ddlCustNOKCountry.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));


                    ddlCustfCountry.DataSource = objCmd.ExecuteReader();
                    ddlCustfCountry.DataTextField = "COUNTRY_NAME";
                    ddlCustfCountry.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlCustfCountry.DataBind();
                    ddlCustfCountry.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));


                    ddlTCAcctCountry.DataSource = objCmd.ExecuteReader();
                    ddlTCAcctCountry.DataTextField = "COUNTRY_NAME";
                    ddlTCAcctCountry.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlTCAcctCountry.DataBind();
                    ddlTCAcctCountry.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));


                    ddlTCAcctNationality.DataSource = objCmd.ExecuteReader();
                    ddlTCAcctNationality.DataTextField = "COUNTRY_NAME";
                    ddlTCAcctNationality.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlTCAcctNationality.DataBind();
                    ddlTCAcctNationality.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));
                }
                catch (Exception ex)
                {
                    // throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
                // BindPageGrid();

            }

            if (!IsPostBack)
            {
                String strQuery2 = "SELECT DISTINCT LIMIT_ID,LIMIT_RANGE FROM CDMA_ONLINE_TRANSACTION_LIMIT";

                //OracleCommand objCmd = new OracleCommand();
                OracleCommand objCmd2 = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd2.CommandType = CommandType.Text;
                objCmd2.CommandText = strQuery2;
                objCmd2.Connection = con;
                try
                {
                    con.Open();
                    ddlCustAIOnlineTrnsfLimit.DataSource = objCmd2.ExecuteReader();
                    ddlCustAIOnlineTrnsfLimit.DataTextField = "LIMIT_RANGE";
                    ddlCustAIOnlineTrnsfLimit.DataValueField = "LIMIT_RANGE";
                    ddlCustAIOnlineTrnsfLimit.DataBind();
                    ddlCustAIOnlineTrnsfLimit.Items.Insert(0, new ListItem("--SELECT LIMIT--", ""));


                }
                catch (Exception ex)
                {
                    //  throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }


                if (!IsPostBack)
                {
                    String strQuery4 = "SELECT DISTINCT BANK_ID,BANK_NAME FROM CDMA_BANKS";

                    //OracleCommand objCmd = new OracleCommand();
                    OracleCommand objCmd4 = new OracleCommand();
                    con = new OracleConnection(new Connection().ConnectionString);
                    objCmd4.CommandType = CommandType.Text;
                    objCmd4.CommandText = strQuery4;
                    objCmd4.Connection = con;
                    try
                    {
                        con.Open();
                        cblA4FinIncAcctWithOtherBanks.DataSource = objCmd4.ExecuteReader();
                        cblA4FinIncAcctWithOtherBanks.DataTextField = "BANK_NAME";
                        cblA4FinIncAcctWithOtherBanks.DataValueField = "BANK_NAME";
                        cblA4FinIncAcctWithOtherBanks.DataBind();
                        // cblA4FinIncAcctWithOtherBanks.Items.Insert(0, new ListItem("--SELECT LIMIT--", ""));


                    }
                    catch (Exception ex)
                    {
                        // throw ex;
                    }
                    finally
                    {
                        con.Close();
                        con.Dispose();
                    }
                }

                if (!IsPostBack)
                {
                    String strQuery3 = "SELECT DEPOSIT_ID,INITIAL_DEPOSIT_RANGE FROM CDMA_INITIAL_DEPOSIT_RANGE";

                    //OracleCommand objCmd = new OracleCommand();
                    OracleCommand objCmd3 = new OracleCommand();
                    con = new OracleConnection(new Connection().ConnectionString);
                    objCmd3.CommandType = CommandType.Text;
                    objCmd3.CommandText = strQuery3;
                    objCmd3.Connection = con;
                    try
                    {
                        con.Open();
                        ddlCustIncomeInitDeposit.DataSource = objCmd3.ExecuteReader();
                        ddlCustIncomeInitDeposit.DataTextField = "INITIAL_DEPOSIT_RANGE";
                        ddlCustIncomeInitDeposit.DataValueField = "INITIAL_DEPOSIT_RANGE";
                        ddlCustIncomeInitDeposit.DataBind();
                        ddlCustIncomeInitDeposit.Items.Insert(0, new ListItem("--SELECT RANGE--", ""));


                    }
                    catch (Exception ex)
                    {
                        //  throw ex;
                    }
                    finally
                    {
                        con.Close();
                        con.Dispose();
                    }
                }

                if (!IsPostBack)
                {
                    String strQuery3 = "SELECT DEPOSIT_ID,INITIAL_DEPOSIT_RANGE FROM CDMA_INITIAL_DEPOSIT_RANGE";

                    //OracleCommand objCmd = new OracleCommand();
                    OracleCommand objCmd3 = new OracleCommand();
                    con = new OracleConnection(new Connection().ConnectionString);
                    objCmd3.CommandType = CommandType.Text;
                    objCmd3.CommandText = strQuery3;
                    objCmd3.Connection = con;
                    try
                    {
                        con.Open();
                        ddlCustIncomeInitDeposit.DataSource = objCmd3.ExecuteReader();
                        ddlCustIncomeInitDeposit.DataTextField = "INITIAL_DEPOSIT_RANGE";
                        ddlCustIncomeInitDeposit.DataValueField = "INITIAL_DEPOSIT_RANGE";
                        ddlCustIncomeInitDeposit.DataBind();
                        ddlCustIncomeInitDeposit.Items.Insert(0, new ListItem("--SELECT RANGE--", ""));


                    }
                    catch (Exception ex)
                    {
                        //  throw ex;
                    }
                    finally
                    {
                        con.Close();
                        con.Dispose();
                    }
                }



                if (!IsPostBack)
                {
                    String strQuery5 = "SELECT INCOME_ID,EXPECTED_INCOME_BAND FROM CDMA_EXPECTED_ANNUAL_INCOME";

                    //OracleCommand objCmd = new OracleCommand();
                    OracleCommand objCmd5 = new OracleCommand();
                    con = new OracleConnection(new Connection().ConnectionString);
                    objCmd5.CommandType = CommandType.Text;
                    objCmd5.CommandText = strQuery5;
                    objCmd5.Connection = con;
                    try
                    {
                        con.Open();
                        ddlAddInfoAnnualSalary.DataSource = objCmd5.ExecuteReader();
                        ddlAddInfoAnnualSalary.DataTextField = "EXPECTED_INCOME_BAND";
                        ddlAddInfoAnnualSalary.DataValueField = "EXPECTED_INCOME_BAND";
                        ddlAddInfoAnnualSalary.DataBind();
                        ddlAddInfoAnnualSalary.Items.Insert(0, new ListItem("--SELECT BAND--", ""));


                    }
                    catch (Exception ex)
                    {
                        //  throw ex;
                    }
                    finally
                    {
                        con.Close();
                        con.Dispose();
                    }


                    if (!IsPostBack)
                    {
                        String strQuery6 = "SELECT INCOME_ID,EXPECTED_INCOME_BAND FROM CDMA_INCOME_BAND";

                        //OracleCommand objCmd = new OracleCommand();
                        OracleCommand objCmd6 = new OracleCommand();
                        con = new OracleConnection(new Connection().ConnectionString);
                        objCmd6.CommandType = CommandType.Text;
                        objCmd6.CommandText = strQuery6;
                        objCmd6.Connection = con;
                        try
                        {
                            con.Open();
                            ddlCustIncomeBand.DataSource = objCmd6.ExecuteReader();
                            ddlCustIncomeBand.DataTextField = "EXPECTED_INCOME_BAND";
                            ddlCustIncomeBand.DataValueField = "EXPECTED_INCOME_BAND";
                            ddlCustIncomeBand.DataBind();
                            ddlCustIncomeBand.Items.Insert(0, new ListItem("--SELECT BAND--", ""));


                        }
                        catch (Exception ex)
                        {
                            //  throw ex;
                        }
                        finally
                        {
                            con.Close();
                            con.Dispose();
                        }


                        if (!IsPostBack)
                        {
                            String strQuery7 = "SELECT SUBSECTOR_CODE,SUBSECTOR_NAME FROM CDMA_INDUSTRY_SUBSECTOR";

                            //OracleCommand objCmd = new OracleCommand();
                            OracleCommand objCmd7 = new OracleCommand();
                            con = new OracleConnection(new Connection().ConnectionString);
                            objCmd7.CommandType = CommandType.Text;
                            objCmd7.CommandText = strQuery7;
                            objCmd7.Connection = con;
                            try
                            {
                                con.Open();
                                ddlCustEISubSector.DataSource = objCmd7.ExecuteReader();
                                ddlCustEISubSector.DataTextField = "SUBSECTOR_NAME";
                                ddlCustEISubSector.DataValueField = "SUBSECTOR_NAME";
                                ddlCustEISubSector.DataBind();
                                ddlCustEISubSector.Items.Insert(0, new ListItem("--SELECT SUBSECTOR--", ""));


                            }
                            catch (Exception ex)
                            {
                                //  throw ex;
                            }
                            finally
                            {
                                con.Close();
                                con.Dispose();
                            }

                        }

                        if (!IsPostBack)
                        {
                            String strQuery8 = "SELECT BUSINESS_CODE,BUSINESS FROM CDMA_NATURE_OF_BUSINESS";

                            //OracleCommand objCmd = new OracleCommand();
                            OracleCommand objCmd8 = new OracleCommand();
                            con = new OracleConnection(new Connection().ConnectionString);
                            objCmd8.CommandType = CommandType.Text;
                            objCmd8.CommandText = strQuery8;
                            objCmd8.Connection = con;
                            try
                            {
                                con.Open();
                                ddlCustEINatureOfBiz.DataSource = objCmd8.ExecuteReader();
                                ddlCustEINatureOfBiz.DataTextField = "BUSINESS";
                                ddlCustEINatureOfBiz.DataValueField = "BUSINESS";
                                ddlCustEINatureOfBiz.DataBind();
                                ddlCustEINatureOfBiz.Items.Insert(0, new ListItem("--SELECT BUSINESS--", ""));


                            }
                            catch (Exception ex)
                            {
                                //  throw ex;
                            }
                            finally
                            {
                                con.Close();
                                con.Dispose();
                            }

                        }



                        if (!IsPostBack)
                        {
                            String strQuery9 = "SELECT SEGMENT_CODE,SEGMENT FROM CDMA_INDUSTRY_SEGMENT";

                            //OracleCommand objCmd = new OracleCommand();
                            OracleCommand objCmd9 = new OracleCommand();
                            con = new OracleConnection(new Connection().ConnectionString);
                            objCmd9.CommandType = CommandType.Text;
                            objCmd9.CommandText = strQuery9;
                            objCmd9.Connection = con;
                            try
                            {
                                con.Open();
                                ddlCustEIIndustrySeg.DataSource = objCmd9.ExecuteReader();
                                ddlCustEIIndustrySeg.DataTextField = "SEGMENT";
                                ddlCustEIIndustrySeg.DataValueField = "SEGMENT";
                                ddlCustEIIndustrySeg.DataBind();
                                ddlCustEIIndustrySeg.Items.Insert(0, new ListItem("--SELECT SEGMENT--", ""));


                            }
                            catch (Exception ex)
                            {
                                //  throw ex;
                            }
                            finally
                            {
                                con.Close();
                                con.Dispose();
                            }

                        }


                        if (!IsPostBack)
                        {
                            String strQuery10 = "SELECT INCOME_ID,EXPECTED_INCOME_BAND FROM CDMA_CHEQUE_CONFIRM_THRESHOLD";

                            //OracleCommand objCmd = new OracleCommand();
                            OracleCommand objCmd10 = new OracleCommand();
                            con = new OracleConnection(new Connection().ConnectionString);
                            objCmd10.CommandType = CommandType.Text;
                            objCmd10.CommandText = strQuery10;
                            objCmd10.Connection = con;
                            try
                            {
                                con.Open();
                                ddlASRChequeConfmtnThresholdRange.DataSource = objCmd10.ExecuteReader();
                                ddlASRChequeConfmtnThresholdRange.DataTextField = "EXPECTED_INCOME_BAND";
                                ddlASRChequeConfmtnThresholdRange.DataValueField = "EXPECTED_INCOME_BAND";
                                ddlASRChequeConfmtnThresholdRange.DataBind();
                                ddlASRChequeConfmtnThresholdRange.Items.Insert(0, new ListItem("--SELECT RANGE--", ""));


                            }
                            catch (Exception ex)
                            {
                                //  throw ex;
                            }
                            finally
                            {
                                con.Close();
                                con.Dispose();
                            }

                        }



                        if (!IsPostBack)
                        {
                            String strQuery11 = "SELECT BRANCH_CODE,BRANCH_NAME FROM VW_CDMA_BRANCH ORDER BY BRANCH_NAME ASC";

                            //OracleCommand objCmd = new OracleCommand();
                            OracleCommand objCmd11 = new OracleCommand();
                            con = new OracleConnection(new Connection().ConnectionString);
                            objCmd11.CommandType = CommandType.Text;
                            objCmd11.CommandText = strQuery11;
                            objCmd11.Connection = con;
                            try
                            {
                                con.Open();
                                ddlCustAIBranch.DataSource = objCmd11.ExecuteReader();
                                ddlCustAIBranch.DataTextField = "BRANCH_NAME";
                                ddlCustAIBranch.DataValueField = "BRANCH_NAME";
                                ddlCustAIBranch.DataBind();
                                ddlCustAIBranch.Items.Insert(0, new ListItem("--SELECT BRANCH--", ""));

                                ddlCustAIOriginatingBranch.DataSource = objCmd11.ExecuteReader();
                                ddlCustAIOriginatingBranch.DataTextField = "BRANCH_NAME";
                                ddlCustAIOriginatingBranch.DataValueField = "BRANCH_NAME";
                                ddlCustAIOriginatingBranch.DataBind();
                                ddlCustAIOriginatingBranch.Items.Insert(0, new ListItem("--SELECT BRANCH--", ""));


                            }
                            catch (Exception ex)
                            {
                                //  throw ex;
                            }
                            finally
                            {
                                con.Close();
                                con.Dispose();
                            }

                        }


                        if (!IsPostBack)
                        {
                            String strQuery12 = "SELECT CLASS_ID,BRANCH_CLASS FROM CDMA_BRANCH_CLASS";

                            //OracleCommand objCmd = new OracleCommand();
                            OracleCommand objCmd12 = new OracleCommand();
                            con = new OracleConnection(new Connection().ConnectionString);
                            objCmd12.CommandType = CommandType.Text;
                            objCmd12.CommandText = strQuery12;
                            objCmd12.Connection = con;
                            try
                            {
                                con.Open();
                                ddlCustAIBranchClass.DataSource = objCmd12.ExecuteReader();
                                ddlCustAIBranchClass.DataTextField = "BRANCH_CLASS";
                                ddlCustAIBranchClass.DataValueField = "BRANCH_CLASS";
                                ddlCustAIBranchClass.DataBind();
                                ddlCustAIBranchClass.Items.Insert(0, new ListItem("--SELECT CLASS--", ""));



                            }
                            catch (Exception ex)
                            {
                                //  throw ex;
                            }
                            finally
                            {
                                con.Close();
                                con.Dispose();
                            }

                        }


                        if (!IsPostBack)
                        {
                            String strQuery13 = "SELECT TYPE_ID,CUSTOMER_TYPE FROM CDMA_CUSTOMER_TYPE";

                            //OracleCommand objCmd = new OracleCommand();
                            OracleCommand objCmd13 = new OracleCommand();
                            con = new OracleConnection(new Connection().ConnectionString);
                            objCmd13.CommandType = CommandType.Text;
                            objCmd13.CommandText = strQuery13;
                            objCmd13.Connection = con;
                            try
                            {
                                con.Open();
                                ddlCustAICusType.DataSource = objCmd13.ExecuteReader();
                                ddlCustAICusType.DataTextField = "CUSTOMER_TYPE";
                                ddlCustAICusType.DataValueField = "CUSTOMER_TYPE";
                                ddlCustAICusType.DataBind();
                                ddlCustAICusType.Items.Insert(0, new ListItem("--SELECT TYPE--", ""));


                            }
                            catch (Exception ex)
                            {
                                //  throw ex;
                            }
                            finally
                            {
                                con.Close();
                                con.Dispose();
                            }

                        }


                        if (!IsPostBack)
                        {
                            String strQuery14 = "SELECT DIVISION_CODE,BUSINESS_DIVISION FROM VW_CDMA_BUSINESS_DIVISION";

                            //OracleCommand objCmd = new OracleCommand();
                            OracleCommand objCmd14 = new OracleCommand();
                            con = new OracleConnection(new Connection().ConnectionString);
                            objCmd14.CommandType = CommandType.Text;
                            objCmd14.CommandText = strQuery14;
                            objCmd14.Connection = con;
                            try
                            {
                                con.Open();
                                ddlCustAIBizDiv.DataSource = objCmd14.ExecuteReader();
                                ddlCustAIBizDiv.DataTextField = "BUSINESS_DIVISION";
                                ddlCustAIBizDiv.DataValueField = "BUSINESS_DIVISION";
                                ddlCustAIBizDiv.DataBind();
                                ddlCustAIBizDiv.Items.Insert(0, new ListItem("--SELECT DIVISION--", ""));


                            }
                            catch (Exception ex)
                            {
                                //  throw ex;
                            }
                            finally
                            {
                                con.Close();
                                con.Dispose();
                            }

                        }


                        if (!IsPostBack)
                        {
                            String strQuery15 = "SELECT SEGMENT_CODE,BUSINESS_SEGMENT FROM VW_CDMA_BUSINESS_SEGMENT";

                            //OracleCommand objCmd = new OracleCommand();
                            OracleCommand objCmd15 = new OracleCommand();
                            con = new OracleConnection(new Connection().ConnectionString);
                            objCmd15.CommandType = CommandType.Text;
                            objCmd15.CommandText = strQuery15;
                            objCmd15.Connection = con;
                            try
                            {
                                con.Open();
                                ddlCustBizSeg.DataSource = objCmd15.ExecuteReader();
                                ddlCustBizSeg.DataTextField = "BUSINESS_SEGMENT";
                                ddlCustBizSeg.DataValueField = "BUSINESS_SEGMENT";
                                ddlCustBizSeg.DataBind();
                                ddlCustBizSeg.Items.Insert(0, new ListItem("--SELECT SEGMENT--", ""));


                            }
                            catch (Exception ex)
                            {
                                //  throw ex;
                            }
                            finally
                            {
                                con.Close();
                                con.Dispose();
                            }

                        }

                        if (!IsPostBack)
                        {
                            String strQuery16 = "SELECT ACCOUNT_CLASS,DESCRIPTION FROM VW_CDMA_ACCOUNT_PRODUCT_TYPE";

                            //OracleCommand objCmd = new OracleCommand();
                            OracleCommand objCmd16 = new OracleCommand();
                            con = new OracleConnection(new Connection().ConnectionString);
                            objCmd16.CommandType = CommandType.Text;
                            objCmd16.CommandText = strQuery16;
                            objCmd16.Connection = con;
                            try
                            {
                                con.Open();
                                ddlCustAITypeOfAcc.DataSource = objCmd16.ExecuteReader();
                                ddlCustAITypeOfAcc.DataTextField = "DESCRIPTION";
                                ddlCustAITypeOfAcc.DataValueField = "DESCRIPTION";
                                ddlCustAITypeOfAcc.DataBind();
                                ddlCustAITypeOfAcc.Items.Insert(0, new ListItem("--SELECT TYPE--", ""));


                            }
                            catch (Exception ex)
                            {
                                //  throw ex;
                            }
                            finally
                            {
                                con.Close();
                                con.Dispose();
                            }

                        }


                        if (!IsPostBack)
                        {
                            String strQuery17 = "SELECT OCCUPATION_CODE,OCCUPATION FROM CDMA_OCCUPATION_LIST";

                            //OracleCommand objCmd = new OracleCommand();
                            OracleCommand objCmd17 = new OracleCommand();
                            con = new OracleConnection(new Connection().ConnectionString);
                            objCmd17.CommandType = CommandType.Text;
                            objCmd17.CommandText = strQuery17;
                            objCmd17.Connection = con;
                            try
                            {
                                con.Open();
                                ddlCustEISectorClass.DataSource = objCmd17.ExecuteReader();
                                ddlCustEISectorClass.DataTextField = "OCCUPATION";
                                ddlCustEISectorClass.DataValueField = "OCCUPATION";
                                ddlCustEISectorClass.DataBind();
                                ddlCustEISectorClass.Items.Insert(0, new ListItem("--SELECT OCCUPATION--", ""));


                            }
                            catch (Exception ex)
                            {
                                //  throw ex;
                            }
                            finally
                            {
                                con.Close();
                                con.Dispose();
                            }

                        }



                    }
                }
            }
            #endregion

            string recId = Request["CustId"];
            //string productId;  // && int.TryParse(rawId, out productId)
            if (!String.IsNullOrEmpty(recId))
            {
                Edit_CustomerInfo(recId);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }
        //protected void Page_Activate(object sender, EventArgs e)
        // {
        //if (!IsPostBack)
        //{
        //hidTAB.Value = "#tab1";

        //}
        // }
        //protected void ddlNationality_OnDataBound(object sender, EventArgs e)
        //{
        //    hidTAB.Value = "#tab1";
        //    if (ddlNationality.SelectedValue.Trim() == "NGA")
        //    {
        //        //ddlLGAofResidence.Items.Clear();
        //        this.ddlStateOfOrigin.AppendDataBoundItems = true;
        //        String strQuery = "SELECT STATE_NAME,STATE_ID FROM SRC_CDMA_STATE";
        //        OracleCommand objCmd = new OracleCommand();
        //        con = new OracleConnection(new Connection().ConnectionString);
        //        objCmd.CommandType = CommandType.Text;
        //        objCmd.CommandText = strQuery;
        //        objCmd.Connection = con;
        //        try
        //        {
        //            con.Open();
        //            ddlStateOfOrigin.DataSource = objCmd.ExecuteReader();
        //            ddlStateOfOrigin.DataTextField = "STATE_NAME";
        //            ddlStateOfOrigin.DataValueField = "STATE_NAME";
        //            ddlStateOfOrigin.DataBind();
        //            ddlStateOfOrigin.Items.Insert(0, new ListItem("--SELECT STATE--", ""));

        //            //this.btnUpdateDQIParam.Visible = false;
        //            //this.GridView1.Visible = false;
        //            //this.GridView1.Visible = true;
        //            this.txtStateOfOrigin.Visible = false;
        //            this.ddlStateOfOrigin.Visible = true;

        //        }
        //        catch (Exception ex)
        //        {
        //            // throw ex;
        //        }
        //        finally
        //        {
        //            con.Close();
        //            con.Dispose();
        //        }
        //    }
        //    else
        //    {
        //        this.txtStateOfOrigin.Visible = true;
        //        this.ddlStateOfOrigin.Visible = false;
        //    }
        //}

        protected void ddlNationality_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            if (ddlNationality.SelectedValue.Trim() == "NGA")
            {
                //ddlLGAofResidence.Items.Clear();
                this.ddlStateOfOrigin.AppendDataBoundItems = true;
                String strQuery = "SELECT STATE_NAME,STATE_ID FROM SRC_CDMA_STATE ORDER BY STATE_NAME ASC";
                OracleCommand objCmd = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strQuery;
                objCmd.Connection = con;
                try
                {
                    con.Open();
                    ddlStateOfOrigin.DataSource = objCmd.ExecuteReader();
                    ddlStateOfOrigin.DataTextField = "STATE_NAME";
                    ddlStateOfOrigin.DataValueField = "STATE_NAME";
                    ddlStateOfOrigin.DataBind();
                    ddlStateOfOrigin.Items.Insert(0, new ListItem("--SELECT STATE--", ""));

                    //this.btnUpdateDQIParam.Visible = false;
                    //this.GridView1.Visible = false;
                    //this.GridView1.Visible = true;
                    this.txtStateOfOrigin.Visible = false;
                    this.ddlStateOfOrigin.Visible = true;

                }
                catch (Exception ex)
                {
                    // throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            else
            {
                this.txtStateOfOrigin.Visible = true;
                this.ddlStateOfOrigin.Visible = false;
            }

        }

        //protected void ddlNationality_OnPreRender(object sender, EventArgs e)
        //{
        //     if (!IsPostBack)
        //    {
        //    hidTAB.Value = "#tab1";
        //    if (ddlNationality.SelectedValue.Trim() == "NGA")
        //    {
        //        //ddlLGAofResidence.Items.Clear();
        //        this.ddlStateOfOrigin.AppendDataBoundItems = true;
        //        String strQuery = "SELECT STATE_NAME,STATE_ID FROM SRC_CDMA_STATE";
        //        OracleCommand objCmd = new OracleCommand();
        //        con = new OracleConnection(new Connection().ConnectionString);
        //        objCmd.CommandType = CommandType.Text;
        //        objCmd.CommandText = strQuery;
        //        objCmd.Connection = con;
        //        try
        //        {
        //            con.Open();
        //            ddlStateOfOrigin.DataSource = objCmd.ExecuteReader();
        //            ddlStateOfOrigin.DataTextField = "STATE_NAME";
        //            ddlStateOfOrigin.DataValueField = "STATE_NAME";
        //            ddlStateOfOrigin.DataBind();
        //            ddlStateOfOrigin.Items.Insert(0, new ListItem("--SELECT STATE--", ""));

        //            //this.btnUpdateDQIParam.Visible = false;
        //            //this.GridView1.Visible = false;
        //            //this.GridView1.Visible = true;
        //            this.txtStateOfOrigin.Visible = false;
        //            this.ddlStateOfOrigin.Visible = true;

        //        }
        //        catch (Exception ex)
        //        {
        //            // throw ex;
        //        }
        //        finally
        //        {
        //            con.Close();
        //            con.Dispose();
        //        }
        //    }
        //    else if (ddlStateOfOrigin.SelectedValue == string.Empty)
        //    {
        //        this.ddlStateOfOrigin.SelectedValue = "";
        //        //this.txtStateOfOrigin.Visible = true;
        //        //this.ddlStateOfOrigin.Visible = false;
        //    }
        //    }
        //}

        protected void ddlCustNOKCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab4";
            if (ddlCustNOKCountry.SelectedValue.Trim() == "NGA")
            {
                //ddlLGAofResidence.Items.Clear();
                this.ddlCustNOKState.AppendDataBoundItems = true;
                String strQuery = "SELECT STATE_NAME,STATE_ID FROM SRC_CDMA_STATE ORDER BY STATE_NAME ASC";
                OracleCommand objCmd = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strQuery;
                objCmd.Connection = con;
                try
                {
                    con.Open();
                    ddlCustNOKState.DataSource = objCmd.ExecuteReader();
                    ddlCustNOKState.DataTextField = "STATE_NAME";
                    ddlCustNOKState.DataValueField = "STATE_NAME";
                    ddlCustNOKState.DataBind();
                    ddlCustNOKState.Items.Insert(0, new ListItem("--SELECT STATE--", ""));

                    //this.btnUpdateDQIParam.Visible = false;
                    //this.GridView1.Visible = false;
                    //this.GridView1.Visible = true;

                    this.txtCustNOKState.Visible = false;
                    this.ddlCustNOKState.Visible = true;
                    this.txtCustNOKLGA.Visible = false;
                    this.ddlCustNOKLGA.Visible = true;
                    this.ddlCustNOKLGA.SelectedValue = string.Empty;
                    this.ddlCustNOKState.SelectedValue = string.Empty;
                }
                catch (Exception ex)
                {
                    // throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            else
            {
                this.txtCustNOKState.Visible = true;
                this.ddlCustNOKState.Visible = false;
                this.txtCustNOKLGA.Visible = true;
                this.ddlCustNOKLGA.Visible = false;
            }
          
        }
        protected void ddlIDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            if (ddlIDType.SelectedValue == "Others")
            {
                this.txtIDType.Visible = true;
                //this.ddlCustNOKIdType.Enabled = false;
            }
            else
            {
                this.txtIDType.Visible = false;
                this.txtIDType.Text = string.Empty;
            }


        }
        protected void rbtASRChequeConfmtnThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab7";
            if (rbtASRChequeConfmtnThreshold.SelectedValue == "N")
            {
                this.ddlASRChequeConfmtnThresholdRange.Enabled = false;
            }
            else
            {
                this.ddlASRChequeConfmtnThresholdRange.Enabled = true;
            }
        }
        protected void rbtASROnlineTraxLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab7";
            if (rbtASROnlineTraxLimit.SelectedValue == "N")
            {
                this.ddlCustAIOnlineTrnsfLimit.Enabled = false;
            }
            else
            {
                this.ddlCustAIOnlineTrnsfLimit.Enabled = true;
            }
        }
        protected void rbtA4FinIncSocFinDisadv_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab9";
            if (rbtA4FinIncSocFinDisadv.SelectedValue == "N")
            {
                this.txtA4FinIncSocFinDoc.Enabled = false;
            }
            else
            {
                this.txtA4FinIncSocFinDoc.Enabled = true;
            }
        }
        protected void rbtA4FinIncEnjoyKYC_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab9";
            if (rbtA4FinIncEnjoyKYC.SelectedValue == "N")
            {
                this.ddlA4FinIncRiskCat.Enabled = false;
            }
            else
            {
                this.ddlA4FinIncRiskCat.Enabled = true;
            }
        }
        //protected void txtDateOfBirth_DataBinding(object sender, EventArgs e)
        //{

        //    //if (DateTime.Now <= (Convert.ToDateTime(txtDateOfBirth.Text)))
        //    //{

        //    //    lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("It is incorrect for the date of birth to be todays date or a future date.");
        //    //    return;
        //    //}
        //    hidTAB.Value = "#tab1";

        //    try
        //    {
        //        DateTime dob = Convert.ToDateTime(txtDateOfBirth.Text);
        //        DateTime presentYear = DateTime.Now;
        //        TimeSpan ts = presentYear - dob;
        //        DateTime Age = DateTime.MinValue.AddDays(ts.Days);
        //        int age = Age.Year - 1;
        //        this.txtAge.Text = Convert.ToString(age);
        //    }
        //    catch (Exception ex)
        //    {
        //        return;
        //    }
        //}
        protected void txtDateOfBirth_TextChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";

            try
            {
                DateTime dob = DateTime.ParseExact(this.txtDateOfBirth.Text, "dd/MM/yyyy", null);//Convert.ToDateTime(txtDateOfBirth.Text);
                DateTime presentYear = DateTime.Now;
                TimeSpan ts = presentYear - dob;
                DateTime Age = DateTime.MinValue.AddDays(ts.Days);
                int age = Age.Year - 1;
                this.txtAge.Text = Convert.ToString(age);
            }
            catch (Exception ex)
            {
                return;
                //throw ex;
            }
        }

        protected void txtDateOfBirth_OnPreRender(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";

            try
            {
                DateTime dob = DateTime.ParseExact(this.txtDateOfBirth.Text, "dd/MM/yyyy", null);//Convert.ToDateTime(txtDateOfBirth.Text);
                DateTime presentYear = DateTime.Now;
                TimeSpan ts = presentYear - dob;
                DateTime Age = DateTime.MinValue.AddDays(ts.Days);
                int age = Age.Year - 1;
                this.txtAge.Text = Convert.ToString(age);
            }
            catch (Exception ex)
            {
                return;
                //throw ex;
            }
        }


        protected void rblTCAcct_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";
            if (rblTCAcct.SelectedValue == "N")
            {
                this.txtCustNoTCAcct.Enabled = false;
                //this.rblTCAcct.Enabled= false;
                this.txtCustTCAcctBeneficialName.Enabled = false;
                this.txtCustTCAcctSpouseName.Enabled = false;
                this.txtCustTCAcctDOB.Enabled = false;
                this.txtCustTCAcctOccptn.Enabled = false;
                this.txtCustTCAcctOtherScrIncome.Enabled = false;
                this.rblTCAcctInsiderRelation.Enabled = false;
                this.txtCustTCAcctNameOfAssBiz.Enabled = false;
                this.rblFreqIntTraveler.Enabled = false;
                this.rblTCAcctPolExposed.Enabled = false;
                this.rtlTCAcctPowerOfAttony.Enabled = false;
                this.txtTCAcctHoldersName.Enabled = false;
                this.txtTCAcctAddress.Enabled = false;
                this.ddlTCAcctCountry.Enabled = false;
                this.ddlTCAcctNationality.Enabled = false;
                this.txtTCAcctTelPhone.Enabled = false;
                this.txtCustTCAcctSrcOfFund.Enabled = false;
            }
            else
            {
                this.txtCustNoTCAcct.Enabled = true;
                //this.rblTCAcct.Enabled = true;
                this.txtCustTCAcctBeneficialName.Enabled = true;
                this.txtCustTCAcctSpouseName.Enabled = true;
                this.txtCustTCAcctDOB.Enabled = true;
                this.txtCustTCAcctOccptn.Enabled = true;
                this.txtCustTCAcctOtherScrIncome.Enabled = true;
                this.rblTCAcctInsiderRelation.Enabled = true;
                this.txtCustTCAcctNameOfAssBiz.Enabled = true;
                this.rblFreqIntTraveler.Enabled = true;
                this.rblTCAcctPolExposed.Enabled = true;
                this.rtlTCAcctPowerOfAttony.Enabled = true;
                this.txtTCAcctHoldersName.Enabled = true;
                this.txtTCAcctAddress.Enabled = true;
                this.ddlTCAcctCountry.Enabled = true;
                this.ddlTCAcctNationality.Enabled = true;
                this.txtTCAcctTelPhone.Enabled = true;
                this.txtCustTCAcctSrcOfFund.Enabled = true;
            }
        }
        protected void ddlCustEIEmpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab8";
            if (ddlCustEIEmpStatus.SelectedValue == "ASSISTED")
            {
                this.txtCustEIDateOfEmp.Enabled = false;
                this.ddlCustEISectorClass.Enabled = false;
                this.ddlCustEISubSector.Enabled = false;
                this.ddlCustEINatureOfBiz.Enabled = false;
                this.ddlCustEIIndustrySeg.Enabled = false;
                this.txtCustEINatureOfBiz.Visible = false;
            }
            else
            {
                this.txtCustEIDateOfEmp.Enabled = true;
                this.ddlCustEISectorClass.Enabled = true;
                this.ddlCustEISubSector.Enabled = true;
                this.ddlCustEINatureOfBiz.Enabled = true;
                this.ddlCustEIIndustrySeg.Enabled = true;
                this.txtCustEINatureOfBiz.Enabled = true;
                //this.txtCustNOKIdType.Text = string.Empty;
            }
        }
        protected void ddlCustNOKIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab4";
            if (ddlCustNOKIdType.SelectedValue == "Others")
            {
                this.txtCustNOKIdType.Visible = true;
                //this.ddlCustNOKIdType.Enabled = false;
            }
            else
            {
                this.txtCustNOKIdType.Visible = false;
                this.txtCustNOKIdType.Text = string.Empty;
            }


        }
        protected void ddlCustNOKState_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab4";
            if (ddlCustNOKCountry.SelectedValue.Trim() == "NGA") //&& ddlCustNOKState.SelectedValue.Trim() != string.Empty
            {
                //ddlLGAofResidence.Items.Clear();
                this.ddlCustNOKState.AppendDataBoundItems = true;
                String strQuery = "SELECT LGA_NAME FROM SRC_CDMA_LGA ORDER BY LGA_NAME ASC ";
                OracleCommand objCmd = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strQuery;
                objCmd.Connection = con;
                try
                {
                    con.Open();
                    ddlCustNOKLGA.DataSource = objCmd.ExecuteReader();
                    ddlCustNOKLGA.DataTextField = "LGA_NAME";
                    ddlCustNOKLGA.DataValueField = "LGA_NAME";
                    ddlCustNOKLGA.DataBind();
                    ddlCustNOKLGA.Items.Insert(0, new ListItem("--SELECT LGA--", ""));

                    this.txtCustNOKLGA.Visible = false;
                    this.ddlCustNOKLGA.Visible = true;
                    this.ddlCustNOKLGA.SelectedValue = string.Empty;

                }
                catch (Exception ex)
                {
                    // throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            else
            {

                this.txtCustNOKLGA.Visible = true;
                this.ddlCustNOKLGA.Visible = false;
            }
        }
        protected void rbtForeigner_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab5";
            if (rbtForeigner.SelectedValue == "N")
            {
                this.rbtMultipleCitizenship.Enabled = false;
                this.txtCustFPassPermit.Enabled = false;
                this.txtCustFIssueDate.Enabled = false;
                this.txtCustFExpiryDate.Enabled = false;
                this.txtCustfForeignAddy.Enabled = false;
                this.txtCustfCity.Enabled = false;
                this.ddlCustfCountry.Enabled = false;
                //
                this.txtCustfForeignPhoneNo.Enabled = false;
                this.txtCustfZipPostalCode.Enabled = false;
                this.txtCustfPurposeOfAcc.Enabled = false;

            }
            else
            {
                this.rbtMultipleCitizenship.Enabled = true;
                this.txtCustFPassPermit.Enabled = true;
                this.txtCustFIssueDate.Enabled = true;
                this.txtCustFExpiryDate.Enabled = true;
                this.txtCustfForeignAddy.Enabled = true;
                this.txtCustfCity.Enabled = true;
                this.ddlCustfCountry.Enabled = true;
                //
                this.txtCustfForeignPhoneNo.Enabled = true;
                this.txtCustfZipPostalCode.Enabled = true;
                this.txtCustfPurposeOfAcc.Enabled = true;

            }
        }
        protected void rbtCustAIAccHolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab7";
            if (rbtCustAIAccHolders.SelectedValue == "N")
            {
                //this.txtCustNoAI.Enabled = false;
                //this.rbtCustAIAccHolders.SelectedValue = a.AccountHolder;
                this.ddlCustAITypeOfAcc.Enabled = false;
                this.txtCustAIAccNo.Enabled = false;
                this.txtCustAIAccOfficer.Enabled = false;
                this.txtCustAIAccTitle.Enabled = false;
                this.ddlCustAIBranch.Enabled = false;
                this.ddlCustAIBranchClass.Enabled = false;
                this.ddlCustAIBizDiv.Enabled = false;
                this.ddlCustAIOnlineTrnsfLimit.Enabled = false;
                this.ddlCustBizSeg.Enabled = false;
                this.ddlCustBizSize.Enabled = false;
                this.txtCustAIBVNNo.Enabled = false;
                this.rblCAVRequired.Enabled = false;
                this.txtCustAICustIc.Enabled = false;
                //this.txtCustAICustId.Enabled = false;
                this.ddlCustAICustSeg.Enabled = false;
                this.ddlCustAICusType.Enabled = false;
                this.txtCustAIOpInsttn.Enabled = false;
                this.ddlCustAIOriginatingBranch.Enabled = false;
                //Account Services Required
                this.rbtASRCardRef.Enabled = false;
                this.rbtASREBP.Enabled = false;
                this.rbtASRStatementPref.Enabled = false;
                this.rbtASRTransAlertPref.Enabled = false;
                this.rbtASRStatementFreq.Enabled = false;
                this.rbtASRChequeBookReqtn.Enabled = false;
                this.rbtASRChequeLeaveReq.Enabled = false;
                this.rbtASRChequeConfmtn.Enabled = false;
                this.rbtASRChequeConfmtnThreshold.Enabled = false;
                this.ddlASRChequeConfmtnThresholdRange.Enabled = false;
                this.rbtASROnlineTraxLimit.Enabled = false;
                this.rbtASRToken.Enabled = false;
                this.txtASRAcctSignitory.Enabled = false;
                this.txtASR2ndAcctSignitory.Enabled = false;
            }
            else
            {
                this.ddlCustAITypeOfAcc.Enabled = true;
                this.txtCustAIAccNo.Enabled = true;
                this.txtCustAIAccOfficer.Enabled = true;
                this.txtCustAIAccTitle.Enabled = true;
                this.ddlCustAIBranch.Enabled = true;
                this.ddlCustAIBranchClass.Enabled = true;
                this.ddlCustAIBizDiv.Enabled = true;
                this.ddlCustAIOnlineTrnsfLimit.Enabled = true;
                this.ddlCustBizSeg.Enabled = true;
                this.ddlCustBizSize.Enabled = true;
                this.txtCustAIBVNNo.Enabled = true;
                this.rblCAVRequired.Enabled = true;
                this.txtCustAICustIc.Enabled = true;
                //this.txtCustAICustId.Enabled = true;
                this.ddlCustAICustSeg.Enabled = true;
                this.ddlCustAICusType.Enabled = true;
                this.txtCustAIOpInsttn.Enabled = true;
                this.ddlCustAIOriginatingBranch.Enabled = true;
                //Account Services Required
                this.rbtASRCardRef.Enabled = true;
                this.rbtASREBP.Enabled = true;
                this.rbtASRStatementPref.Enabled = true;
                this.rbtASRTransAlertPref.Enabled = true;
                this.rbtASRStatementFreq.Enabled = true;
                this.rbtASRChequeBookReqtn.Enabled = true;
                this.rbtASRChequeLeaveReq.Enabled = true;
                this.rbtASRChequeConfmtn.Enabled = true;
                this.rbtASRChequeConfmtnThreshold.Enabled = true;
                this.ddlASRChequeConfmtnThresholdRange.Enabled = false;
                this.rbtASROnlineTraxLimit.Enabled = true;
                this.rbtASRToken.Enabled = true;
                this.txtASRAcctSignitory.Enabled = true;
                this.txtASR2ndAcctSignitory.Enabled = true;
            }
        }
        protected void ddlCountryofResidence_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.txtStateOfResidence.Visible = true;
            //this.txtLGAOfResidence.Visible = true;

            if (ddlCountryofResidence.SelectedValue.Trim() == "NGA")
            {
                ddlLGAofResidence.Items.Clear();
                this.ddlStateOfResidence.AppendDataBoundItems = true;
                String strQuery = "SELECT STATE_NAME,STATE_ID FROM SRC_CDMA_STATE ORDER BY STATE_NAME ASC";
                OracleCommand objCmd = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strQuery;
                objCmd.Connection = con;
                try
                {
                    con.Open();
                    ddlStateOfResidence.DataSource = objCmd.ExecuteReader();
                    ddlStateOfResidence.DataTextField = "STATE_NAME";
                    ddlStateOfResidence.DataValueField = "STATE_ID";
                    ddlStateOfResidence.DataBind();
                    ddlStateOfResidence.Items.Insert(0, new ListItem("--SELECT STATE--", ""));

                    //this.btnUpdateDQIParam.Visible = false;
                    //this.GridView1.Visible = false;
                    //this.GridView1.Visible = true;
                    this.ddlStateOfResidence.Visible = true;
                    this.ddlLGAofResidence.Visible = true;
                }
                catch (Exception ex)
                {
                    ///  throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            else
            {
                this.txtStateOfResidence.Visible = true;
                this.ddlStateOfResidence.Visible = false;
                this.ddlStateOfResidence.SelectedValue = string.Empty;
                this.txtLGAOfResidence.Visible = true;
                this.ddlLGAofResidence.Visible = false;
                this.ddlLGAofResidence.SelectedValue = string.Empty;
            }
        }

        //protected void ddlCountryofResidence_OnPreRender(object sender, EventArgs e)
        //{
        //    //this.txtStateOfResidence.Visible = true;
        //    //this.txtLGAOfResidence.Visible = true;
        //     if (!IsPostBack)
        //    {

        //    if (ddlCountryofResidence.SelectedValue.Trim() == "NGA")
        //    {
        //        ddlLGAofResidence.Items.Clear();
        //        this.ddlStateOfResidence.AppendDataBoundItems = true;
        //        String strQuery = "SELECT STATE_NAME,STATE_ID FROM SRC_CDMA_STATE";
        //        OracleCommand objCmd = new OracleCommand();
        //        con = new OracleConnection(new Connection().ConnectionString);
        //        objCmd.CommandType = CommandType.Text;
        //        objCmd.CommandText = strQuery;
        //        objCmd.Connection = con;
        //        try
        //        {
        //            con.Open();
        //            ddlStateOfResidence.DataSource = objCmd.ExecuteReader();
        //            ddlStateOfResidence.DataTextField = "STATE_NAME";
        //            ddlStateOfResidence.DataValueField = "STATE_ID";
        //            ddlStateOfResidence.DataBind();
        //            ddlStateOfResidence.Items.Insert(0, new ListItem("--SELECT STATE--", ""));

        //            //this.btnUpdateDQIParam.Visible = false;
        //            //this.GridView1.Visible = false;
        //            //this.GridView1.Visible = true;
        //            this.ddlStateOfResidence.Visible = true;
        //            this.ddlLGAofResidence.Visible = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            ///  throw ex;
        //        }
        //        finally
        //        {
        //            con.Close();
        //            con.Dispose();
        //        }
        //    }
        //    else if (this.ddlStateOfResidence.SelectedValue == string.Empty)
        //    {


        //        this.ddlStateOfResidence.Text = "";
        //    }
        //    }
        //}

        //protected void ddlStateOfResidence_OnPreRender(object sender, EventArgs e)
        //{
        //    if (ddlCountryofResidence.SelectedValue.Trim() == "NGA")// && ddlStateOfResidence.SelectedValue.Trim() != string.Empty
        //    {
        //        ddlLGAofResidence.AppendDataBoundItems = true;
        //       // int StateOfRes = Convert.ToInt16(this.ddlStateOfResidence.SelectedItem.Value);
        //        String strQuery = "SELECT LGA_NAME FROM SRC_CDMA_LGA";//WHERE STATE_ID =:P_STATE_ID";
        //        OracleCommand objCmd = new OracleCommand();
        //        con = new OracleConnection(new Connection().ConnectionString);
        //        //cmd.Parameters.Add(":P_STATE_ID", StateOfRes);//":p_STATE_ID", Convert.ToInt32(StateOfRes)
        //        objCmd.CommandType = CommandType.Text;
        //        objCmd.BindByName = true;
        //        objCmd.CommandText = strQuery;
        //        objCmd.Connection = con;
        //        try
        //        {
        //            con.Open();
        //            ddlLGAofResidence.DataSource = objCmd.ExecuteReader();
        //            ddlLGAofResidence.DataTextField = "LGA_NAME";
        //            ddlLGAofResidence.DataValueField = "LGA_NAME";
        //            ddlLGAofResidence.DataBind();
        //            ddlLGAofResidence.Items.Insert(0, new ListItem("--SELECT LGA--", ""));

        //            //this.btnUpdateDQIParam.Visible = false;
        //            //this.GridView1.Visible = false;
        //            //this.GridView1.Visible = true;


        //        }
        //        catch (Exception ex)
        //        {
        //            // throw ex;
        //        }
        //        finally
        //        {
        //            con.Close();
        //            con.Dispose();
        //        }
        //    }
        //    else
        //    {
        //        this.txtLGAOfResidence.Visible = true;
        //        this.ddlLGAofResidence.Visible = false;
        //    }
        //}
        protected void ddlStateOfResidence_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountryofResidence.SelectedValue.Trim() == "NGA")
            {
                ddlLGAofResidence.AppendDataBoundItems = true;
                // int StateOfRes = Convert.ToInt16(this.ddlStateOfResidence.SelectedItem.Value);
                String strQuery = "SELECT LGA_NAME FROM SRC_CDMA_LGA ORDER BY LGA_NAME ASC";//WHERE STATE_ID =:P_STATE_ID";
                OracleCommand objCmd = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                //cmd.Parameters.Add(":P_STATE_ID", StateOfRes);//":p_STATE_ID", Convert.ToInt32(StateOfRes)
                objCmd.CommandType = CommandType.Text;
                objCmd.BindByName = true;
                objCmd.CommandText = strQuery;
                objCmd.Connection = con;
                try
                {
                    con.Open();
                    ddlLGAofResidence.DataSource = objCmd.ExecuteReader();
                    ddlLGAofResidence.DataTextField = "LGA_NAME";
                    ddlLGAofResidence.DataValueField = "LGA_NAME";
                    ddlLGAofResidence.DataBind();
                    ddlLGAofResidence.Items.Insert(0, new ListItem("--SELECT LGA--", ""));

                    //this.btnUpdateDQIParam.Visible = false;
                    //this.GridView1.Visible = false;
                    //this.GridView1.Visible = true;


                }
                catch (Exception ex)
                {
                    // throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            else
            {
                this.txtLGAOfResidence.Visible = true;
                this.ddlLGAofResidence.Visible = false;
            }
        }
        protected void ddlCustEINatureOfBiz_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustEINatureOfBiz.SelectedValue.Trim() == "Others")
            {
                txtCustEINatureOfBiz.Visible = true;
            }
            else
            {
                txtCustEINatureOfBiz.Visible = false;
                txtCustEINatureOfBiz.Text = "";
            }
        }
        protected void btnCustInfoUpdate_Click(object sender, EventArgs e)
        {
            if (txtCustInfoID.Text == string.Empty)
            {
                lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }


            if (this.txtIDIssueDate.Text != string.Empty || this.txtIDExpiryDate.Text != string.Empty || this.txtDateOfBirth.Text != string.Empty)
            {

                DateTime IssueDate = DateTime.ParseExact(this.txtIDIssueDate.Text, "dd/MM/yyyy", null);
                DateTime ExpiryDate = DateTime.ParseExact(this.txtIDExpiryDate.Text, "dd/MM/yyyy", null);
                if (mp.checkDateDiff(Convert.ToDateTime(IssueDate, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat), Convert.ToDateTime(ExpiryDate, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat)) == false)
                {
                    lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Incorrect: Issue date can not be ahead of Expiry date.");
                    return;

                }
                DateTime DateOfBirth = DateTime.ParseExact(this.txtDateOfBirth.Text, "dd/MM/yyyy", null);
                if (DateTime.Now <= (Convert.ToDateTime(DateOfBirth)))
                {

                    lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Incorrect: Date of birth cant't be todays date or a future date.");
                    return;
                }
            }
            hidTAB.Value = "#tab1";
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_individual_details";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = this.txtCustInfoID.Text;
            objCmd.Parameters.Add("p_title", OracleDbType.Varchar2).Value = this.ddlTitle.SelectedValue.ToUpper();
            objCmd.Parameters.Add("p_surname", OracleDbType.Varchar2).Value = this.txtSurname.Text;
            objCmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = this.txtFirstName.Text;
            objCmd.Parameters.Add("p_other_name", OracleDbType.Varchar2).Value = this.txtOtherName.Text;
            objCmd.Parameters.Add("p_nickname_alias", OracleDbType.Varchar2).Value = this.txtNickname.Text;
            objCmd.Parameters.Add("p_sex", OracleDbType.Varchar2).Value = this.ddlSex.SelectedValue;
            objCmd.Parameters.Add("p_date_of_birth", OracleDbType.Date).Value = this.txtDateOfBirth.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtDateOfBirth.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            objCmd.Parameters.Add("p_age", OracleDbType.Int32).Value = this.txtAge.Text == string.Empty ? 0 : Convert.ToInt32(this.txtAge.Text);
            objCmd.Parameters.Add("p_place_of_birth", OracleDbType.Varchar2).Value = this.txtPlacefBirth.Text;
            objCmd.Parameters.Add("p_country_of_birth", OracleDbType.Varchar2).Value = this.ddlCountryOfBirth.SelectedValue;
            objCmd.Parameters.Add("p_nationality", OracleDbType.Varchar2).Value = this.ddlNationality.SelectedValue;
            objCmd.Parameters.Add("p_state_of_origin", OracleDbType.Varchar2).Value = this.ddlStateOfOrigin.SelectedValue == string.Empty ? this.txtStateOfOrigin.Text : this.ddlStateOfOrigin.SelectedValue;
            objCmd.Parameters.Add("p_marital_status", OracleDbType.Varchar2).Value = this.ddlMaritalStatus.SelectedValue;
            objCmd.Parameters.Add("p_mother_maiden_name", OracleDbType.Varchar2).Value = this.txtMothersMaidenName.Text;
            objCmd.Parameters.Add("p_number_of_children", OracleDbType.Int32).Value = this.txtNoOfChildren.Text == string.Empty ? 0 : Convert.ToInt32(this.txtNoOfChildren.Text);
            objCmd.Parameters.Add("p_religion", OracleDbType.Varchar2).Value = this.ddlReligion.SelectedValue;
            objCmd.Parameters.Add("p_complexion", OracleDbType.Varchar2).Value = this.txtComplexion.Text;
            objCmd.Parameters.Add("p_disability", OracleDbType.Varchar2).Value = this.ddlDisability.Text;

            objCmd.Parameters.Add("p_country_of_residence", OracleDbType.Varchar2).Value = this.ddlCountryofResidence.SelectedValue;
            objCmd.Parameters.Add("p_state_of_residence", OracleDbType.Varchar2).Value = this.ddlCountryofResidence.SelectedValue == "NGR" ? this.ddlStateOfResidence.SelectedValue : this.txtStateOfResidence.Text;
            objCmd.Parameters.Add("p_lga_of_residence", OracleDbType.Varchar2).Value = this.ddlCountryofResidence.SelectedValue == "NGR" ? this.ddlLGAofResidence.SelectedValue : this.txtLGAOfResidence.Text;
            objCmd.Parameters.Add("p_city_town_of_residence", OracleDbType.Varchar2).Value = this.txtCityofResidence.Text;
            objCmd.Parameters.Add("p_residential_address", OracleDbType.Varchar2).Value = this.txtResidentialAddy.Text;
            objCmd.Parameters.Add("p_nearest_bus_stop_landmark", OracleDbType.Varchar2).Value = this.txtNearestBusStop.Text;
            objCmd.Parameters.Add("p_residence_owned_or_rent", OracleDbType.Varchar2).Value = this.rbtOwnedorRented.Text;
            objCmd.Parameters.Add("p_zip_postal_code", OracleDbType.Varchar2).Value = this.txtZipCode.Text;

            objCmd.Parameters.Add("p_mobile_no", OracleDbType.Varchar2).Value = this.txtMobileNo.Text;
            objCmd.Parameters.Add("p_email_address", OracleDbType.Varchar2).Value = this.txtEmail.Text;
            objCmd.Parameters.Add("p_mailing_address", OracleDbType.Varchar2).Value = this.txtMailingAddy.Text;

            objCmd.Parameters.Add("p_identification_type", OracleDbType.Varchar2).Value = this.ddlIDType.SelectedValue == "Others" ? this.txtIDType.Text : this.ddlIDType.SelectedValue;
            objCmd.Parameters.Add("p_id_no", OracleDbType.Varchar2).Value = this.txtIDNo.Text;
            objCmd.Parameters.Add("p_id_issue_date", OracleDbType.Date).Value = this.txtIDIssueDate.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtIDIssueDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            objCmd.Parameters.Add("p_id_expiry_date", OracleDbType.Date).Value = this.txtIDExpiryDate.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtIDExpiryDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            objCmd.Parameters.Add("p_place_of_issuance", OracleDbType.Varchar2).Value = this.txtPlaceOfIssue.Text;

            objCmd.Parameters.Add("p_tin_no", OracleDbType.Varchar2).Value = this.txtTINNo.Text;

            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorized", OracleDbType.Varchar2).Value = "N";
            //objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = "";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";
            //
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

                        lblstatus.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Record Updated Successfully");

                        EmailHelper Emailer = new EmailHelper();
                        Emailer.NotificationMailSender((String)(Session["UserID"]));

                        callClearBOx();


                        //try
                        //{
                        //    //////////////////////////////////////////////////////////////////////////////////////
                        //    // Create the mail message
                        //    MailMessage mail = new MailMessage();
                        //    // Set the host, 
                        //    SmtpClient smtp = new SmtpClient();
                        //    // Set the from address and to address
                        //    mail.From = new MailAddress("cdma@diamondbank.com");
                        //    mail.To.Add(new MailAddress("emobiechina@diamondbank.com"));//n

                        //    //StringBuilder sb = new StringBuilder();
                        //    // Set the subject and body
                        //    mail.Subject = "CDMA Customer record update notification!!!";

                        //    mail.Body = "Testing";//n
                        //    //mail.BodyEncoding = System.Text.Encoding.ASCII;
                        //    mail.IsBodyHtml = true;

                        //    smtp.UseDefaultCredentials = false;
                        //    //smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                        //    smtp.Credentials = new NetworkCredential("cdma@diamondbank.com", "pa22WORD@2");
                        //    smtp.Port = Convert.ToInt32("25");
                        //    smtp.Host = "10.0.47.118";//"smtp.office365.com";
                        //    Settings.Default.Save();
                        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        //    smtp.EnableSsl = false;
                        //    smtp.UseDefaultCredentials = false;

                        //    // ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate,X509Chain chain, SslPolicyErrors sslPolicyErrors){ return true; };

                        //    smtp.Send(mail);

                        //    //sb.AppendLine();
                        //    // sb.Append(" " + Util.getUserName(n) +" ("+ getEmpEmailwithID(Convert.ToInt32(n)).ToString() + ") , ");
                        //}
                        //catch (Exception ex)
                        //{

                        //    lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Msg:" + ex.Message + "            Scr:" + ex.Source + "              Inner:" + ex.InnerException + "                stackTrack:" + ex.StackTrace);
                        //}

                        //////////////////////////////////////////////////////////////////////////////////

                    }
                    else
                    {
                        lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();
            }

        }

        private void LoadDefaults()
        {

            if (!Page.IsPostBack)
            {

            }
            else
            {

            }

            //ScriptManager.GetCurrent(Page).RegisterPostBackControl(cmdSearch_BizPartner);
            //ScriptManager.GetCurrent(Page).RegisterPostBackControl(cmdSearch_ContractType);

            SetDefaults();
        }

        private void SetDefaults()
        {
            if (!Page.IsPostBack)
            {

            }
            else
            {

            }

        }


        //protected void ddlIDType_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (this.ddlIDType.SelectedItem.Value == "Driver License")


        //        this.ddlIssuingAuth.SelectedValue = "FRSC";
        //    if (this.ddlIDType.SelectedItem.Value == "International Passport")

        //        this.ddlIssuingAuth.SelectedValue = "NIS";
        //    if (this.ddlIDType.SelectedItem.Value == "National ID")

        //        this.ddlIssuingAuth.SelectedValue = "DNCR";
        //}


        protected void OnEdit_CustomerInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            LinkButton lnk = sender as LinkButton;
            string recId = lnk.Attributes["RecId"];

            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.get_indiv_details";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = recId;
            //
            objCmd.Parameters.Add("custinfo", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {//

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
                        this.ddlTitle.SelectedValue = reader["title"] == DBNull.Value ? string.Empty : reader["title"].ToString().ToUpper();
                        this.txtSurname.Text = reader["surname"] == DBNull.Value ? string.Empty : reader["surname"].ToString();
                        this.txtFirstName.Text = reader["first_name"] == DBNull.Value ? string.Empty : reader["first_name"].ToString();
                        this.txtOtherName.Text = reader["other_name"] == DBNull.Value ? string.Empty : reader["other_name"].ToString();
                        this.txtNickname.Text = reader["nickname_alias"] == DBNull.Value ? string.Empty : reader["nickname_alias"].ToString();
                        this.ddlSex.SelectedValue = reader["sex"] == DBNull.Value ? string.Empty : reader["sex"].ToString();
                        this.txtDateOfBirth.Text = reader["date_of_birth"] == DBNull.Value ? null : Convert.ToDateTime(reader["date_of_birth"]).ToString("dd/MM/yyyy");
                        //this.txtAccountNo.Text = reader["ACCOUNT_NO"].ToString();
                        this.txtAge.Text = reader["age"] == DBNull.Value ? string.Empty : reader["age"].ToString();
                        //Convert.ToDateTime(reader["DATE_OF_BIRTH"]).Date.ToString();
                        this.txtPlacefBirth.Text = reader["place_of_birth"] == DBNull.Value ? string.Empty : reader["place_of_birth"].ToString();
                        //this.ddlNationality.Text = reader["nationality"].ToString();
                        this.ddlCountryOfBirth.SelectedValue = reader["country_of_birth"] == DBNull.Value ? string.Empty : reader["country_of_birth"].ToString().ToUpper();
                        this.ddlNationality.SelectedValue = reader["nationality"] == DBNull.Value ? string.Empty : reader["nationality"].ToString().ToUpper();
                        this.ddlStateOfOrigin.SelectedValue = reader["state_of_origin"] == DBNull.Value ? string.Empty : reader["state_of_origin"].ToString();
                        this.ddlMaritalStatus.SelectedValue = reader["marital_status"] == DBNull.Value ? string.Empty : reader["marital_status"].ToString();
                        this.txtMothersMaidenName.Text = reader["mother_maiden_name"] == DBNull.Value ? string.Empty : reader["mother_maiden_name"].ToString();
                        this.txtNoOfChildren.Text = reader["number_of_children"] == DBNull.Value ? string.Empty : reader["number_of_children"].ToString();
                        this.ddlReligion.SelectedValue = reader["religion"] == DBNull.Value ? string.Empty : reader["religion"].ToString();
                        this.txtComplexion.Text = reader["complexion"] == DBNull.Value ? string.Empty : reader["complexion"].ToString();
                        this.ddlDisability.Text = reader["disability"] == DBNull.Value ? string.Empty : reader["disability"].ToString();
                        this.ddlCountryofResidence.SelectedValue = reader["country_of_residence"] == DBNull.Value ? string.Empty : reader["country_of_residence"].ToString();
                        this.ddlStateOfResidence.SelectedValue = reader["state_of_residence"] == DBNull.Value ? string.Empty : reader["state_of_residence"].ToString();
                        this.ddlLGAofResidence.SelectedValue = reader["lga_of_residence"] == DBNull.Value ? string.Empty : reader["lga_of_residence"].ToString();
                        this.txtCityofResidence.Text = reader["city_town_of_residence"] == DBNull.Value ? string.Empty : reader["city_town_of_residence"].ToString();
                        this.txtResidentialAddy.Text = reader["residential_address"] == DBNull.Value ? string.Empty : reader["residential_address"].ToString();
                        this.txtNearestBusStop.Text = reader["nearest_bus_stop_landmark"] == DBNull.Value ? string.Empty : reader["nearest_bus_stop_landmark"].ToString();
                        this.rbtOwnedorRented.Text = reader["residence_owned_or_rent"] == DBNull.Value ? string.Empty : reader["residence_owned_or_rent"].ToString();
                        this.txtZipCode.Text = reader["zip_postal_code"] == DBNull.Value ? string.Empty : reader["zip_postal_code"].ToString();

                        this.txtMobileNo.Text = reader["mobile_no"] == DBNull.Value ? string.Empty : reader["mobile_no"].ToString();
                        this.txtEmail.Text = reader["email_address"] == DBNull.Value ? string.Empty : reader["email_address"].ToString();
                        this.txtMailingAddy.Text = reader["mailing_address"] == DBNull.Value ? string.Empty : reader["mailing_address"].ToString();

                        this.ddlIDType.SelectedValue = reader["identification_type"] == DBNull.Value ? string.Empty : Convert.ToString(reader["identification_type"]);//DataTextField
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
                lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message + ex.InnerException);

            }
            finally
            {
                //reader.Close();
                objCmd = null;
                con.Close();
            }
        }
        protected void Edit_CustomerInfo(string recId)
        {
            hidTAB.Value = "#tab1";
            con = new OracleConnection(new Connection().ConnectionString);
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.get_indiv_details";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = recId;
            //
            objCmd.Parameters.Add("custinfo", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {//

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
                        this.ddlTitle.SelectedValue = reader["title"] == DBNull.Value ? string.Empty : reader["title"].ToString().ToUpper();
                        this.txtSurname.Text = reader["surname"] == DBNull.Value ? string.Empty : reader["surname"].ToString();
                        this.txtFirstName.Text = reader["first_name"] == DBNull.Value ? string.Empty : reader["first_name"].ToString();
                        this.txtOtherName.Text = reader["other_name"] == DBNull.Value ? string.Empty : reader["other_name"].ToString();
                        this.txtNickname.Text = reader["nickname_alias"] == DBNull.Value ? string.Empty : reader["nickname_alias"].ToString();
                        this.ddlSex.SelectedValue = reader["sex"] == DBNull.Value ? string.Empty : reader["sex"].ToString();
                        this.txtDateOfBirth.Text = reader["date_of_birth"] == DBNull.Value ? null : Convert.ToDateTime(reader["date_of_birth"]).ToString("dd/MM/yyyy");
                        //this.txtAccountNo.Text = reader["ACCOUNT_NO"].ToString();
                        this.txtAge.Text = reader["age"] == DBNull.Value ? string.Empty : reader["age"].ToString();
                        //Convert.ToDateTime(reader["DATE_OF_BIRTH"]).Date.ToString();
                        this.txtPlacefBirth.Text = reader["place_of_birth"] == DBNull.Value ? string.Empty : reader["place_of_birth"].ToString();
                        //this.ddlNationality.Text = reader["nationality"].ToString();
                        this.ddlCountryOfBirth.SelectedValue = reader["country_of_birth"] == DBNull.Value ? string.Empty : reader["country_of_birth"].ToString().ToUpper();
                        this.ddlNationality.SelectedValue = reader["nationality"] == DBNull.Value ? string.Empty : reader["nationality"].ToString().ToUpper();
                        this.ddlStateOfOrigin.SelectedValue = reader["state_of_origin"] == DBNull.Value ? string.Empty : reader["state_of_origin"].ToString();
                        this.ddlMaritalStatus.SelectedValue = reader["marital_status"] == DBNull.Value ? string.Empty : reader["marital_status"].ToString();
                        this.txtMothersMaidenName.Text = reader["mother_maiden_name"] == DBNull.Value ? string.Empty : reader["mother_maiden_name"].ToString();
                        this.txtNoOfChildren.Text = reader["number_of_children"] == DBNull.Value ? string.Empty : reader["number_of_children"].ToString();
                        this.ddlReligion.SelectedValue = reader["religion"] == DBNull.Value ? string.Empty : reader["religion"].ToString();
                        this.txtComplexion.Text = reader["complexion"] == DBNull.Value ? string.Empty : reader["complexion"].ToString();
                        this.ddlDisability.Text = reader["disability"] == DBNull.Value ? string.Empty : reader["disability"].ToString();
                        this.ddlCountryofResidence.SelectedValue = reader["country_of_residence"] == DBNull.Value ? string.Empty : reader["country_of_residence"].ToString();
                        this.ddlStateOfResidence.SelectedValue = reader["state_of_residence"] == DBNull.Value ? string.Empty : reader["state_of_residence"].ToString();
                        this.ddlLGAofResidence.SelectedValue = reader["lga_of_residence"] == DBNull.Value ? string.Empty : reader["lga_of_residence"].ToString();
                        this.txtCityofResidence.Text = reader["city_town_of_residence"] == DBNull.Value ? string.Empty : reader["city_town_of_residence"].ToString();
                        this.txtResidentialAddy.Text = reader["residential_address"] == DBNull.Value ? string.Empty : reader["residential_address"].ToString();
                        this.txtNearestBusStop.Text = reader["nearest_bus_stop_landmark"] == DBNull.Value ? string.Empty : reader["nearest_bus_stop_landmark"].ToString();
                        this.rbtOwnedorRented.Text = reader["residence_owned_or_rent"] == DBNull.Value ? string.Empty : reader["residence_owned_or_rent"].ToString();
                        this.txtZipCode.Text = reader["zip_postal_code"] == DBNull.Value ? string.Empty : reader["zip_postal_code"].ToString();

                        this.txtMobileNo.Text = reader["mobile_no"] == DBNull.Value ? string.Empty : reader["mobile_no"].ToString();
                        this.txtEmail.Text = reader["email_address"] == DBNull.Value ? string.Empty : reader["email_address"].ToString();
                        this.txtMailingAddy.Text = reader["mailing_address"] == DBNull.Value ? string.Empty : reader["mailing_address"].ToString();

                        this.ddlIDType.SelectedValue = reader["identification_type"] == DBNull.Value ? string.Empty : Convert.ToString(reader["identification_type"]);//DataTextField
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
                lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message + ex.InnerException);

            }
            finally
            {
                //reader.Close();
                objCmd = null;
                con.Close();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            //lblstatus.Text = "";
            //callClearBOx(); 



            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.get_indiv_details";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("customer_no", OracleDbType.Varchar2).Value = this.txtCustInfoID.Text.Trim();
            //
            objCmd.Parameters.Add("custinfo", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            //
            //int created = 0; 
            if (con.State == ConnectionState.Closed)
                con.Open();//



            try
            {
                OracleDataReader reader;
                reader = objCmd.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        //txtCustInfoID.Text = reader["customer_id"].ToString();

                        //if (txtCustInfoID.Text != reader["customer_no"].ToString())
                        //{//txtCustInfoID.Text != reader["customer_id"].ToString())txtCustInfoID.Text == null
                        //    //txtCustInfoID.Text != reader["customer_id"].ToString()
                        //    //reader.Close();

                        //    lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error: Record not found");

                        //    //Response.Redirect("/CDMA/CustomerInfo/IndividualCustomer.aspx"); 
                        //    //this.txtSurname.Text = "";

                        //    //return;
                        //}

                        //else
                        //{

                        //
                        //txtCustInfoID.Text = txtschKin.Text = txtshAccOtherBk.Text = txtshEmplinfo.Text = txtshfamily.Text = txtshForeigner.Text = txtsearchIncome.Text = txtshLegal.Text = reader["customer_id"].ToString();
                        this.ddlTitle.SelectedValue = Convert.ToString(reader["title"]).ToUpper();
                        this.txtSurname.Text = reader["surname"].ToString();
                        this.txtFirstName.Text = reader["first_name"].ToString();

                        //
                        this.txtCustNoIncome.Text = txtCustInfoID.Text;
                        //this.txtCustIncomeName.Text = txtShrtName.Text;
                        //Account with Bank
                        this.txtCustNoTCAcct.Text = txtCustInfoID.Text;
                        //this.txtCustOtherAcctName.Text = txtShrtName.Text;
                        //Next of Kin
                        this.txtCustNoNOK.Text = txtCustInfoID.Text;
                        //this.txtCustNOKfName.Text = txtShrtName.Text;
                        //Foreign Customer
                        this.txtCustNoForgner.Text = txtCustInfoID.Text;
                        //this.txtCustFfName.Text = txtShrtName.Text;
                        //Jurat Details
                        this.txtCustNoJurat.Text = txtCustInfoID.Text;
                        //this.txtCustLFullName.Text = txtShrtName.Text;
                        //Family
                        this.txtCustNoAI.Text = txtCustInfoID.Text;
                        //this.txtCustFIfName.Text = txtShrtName.Text;

                        //Employment Info
                        this.txtCustNoEmpInfo.Text = txtCustInfoID.Text;
                        //this.txtCustEIfName.Text = txtShrtName.Text;

                        //Financial Inc details
                        this.txtCustNoA4FinInc.Text = txtCustInfoID.Text;

                        //Trust acct.
                        this.txtCustNoTCAcct.Text = txtCustInfoID.Text;

                        //Additional Info
                        this.txtCustNoAddInfo.Text = txtCustInfoID.Text;



                        this.txtOtherName.Text = reader["other_name"].ToString();
                        this.txtNickname.Text = reader["nickname_alias"].ToString();
                        this.ddlSex.SelectedValue = reader["sex"].ToString();
                        this.txtDateOfBirth.Text = reader["date_of_birth"] == DBNull.Value ? null : Convert.ToDateTime(reader["date_of_birth"]).ToString("dd/MM/yyyy");
                        this.txtAge.Text = reader["age"].ToString();//String.Format("{0:dd/MM/yyyy}", 
                        this.txtPlacefBirth.Text = reader["place_of_birth"].ToString();
                        this.ddlCountryOfBirth.SelectedValue = reader["country_of_birth"].ToString();
                        this.ddlNationality.SelectedValue = reader["nationality"].ToString();
                        this.ddlStateOfOrigin.SelectedValue = reader["state_of_origin"].ToString();
                        this.ddlMaritalStatus.SelectedValue = reader["marital_status"].ToString();
                        this.txtMothersMaidenName.Text = reader["mother_maiden_name"].ToString();
                        this.txtNoOfChildren.Text = reader["number_of_children"].ToString();
                        this.ddlReligion.SelectedValue = reader["religion"].ToString();
                        this.txtComplexion.Text = reader["complexion"].ToString();
                        this.ddlDisability.Text = reader["disability"].ToString();
                        this.ddlCountryofResidence.SelectedValue = reader["country_of_residence"].ToString();
                        this.ddlStateOfResidence.SelectedValue = reader["state_of_residence"] == DBNull.Value ? string.Empty : reader["state_of_residence"].ToString();
                        this.ddlLGAofResidence.SelectedValue = reader["lga_of_residence"].ToString();
                        this.txtCityofResidence.Text = reader["city_town_of_residence"].ToString();
                        this.txtResidentialAddy.Text = reader["residential_address"].ToString();
                        this.txtNearestBusStop.Text = reader["nearest_bus_stop_landmark"].ToString();
                        this.rbtOwnedorRented.Text = reader["residence_owned_or_rent"].ToString();
                        this.txtZipCode.Text = reader["zip_postal_code"].ToString();

                        this.txtMobileNo.Text = reader["mobile_no"].ToString();
                        this.txtEmail.Text = reader["email_address"].ToString();
                        this.txtMailingAddy.Text = reader["mailing_address"].ToString();

                        this.ddlIDType.SelectedValue = Convert.ToString(reader["identification_type"]);//DataTextField
                        this.txtIDNo.Text = reader["id_no"].ToString();
                        this.txtIDIssueDate.Text = reader["id_issue_date"] == DBNull.Value ? null : Convert.ToDateTime(reader["id_issue_date"]).ToString("dd/MM/yyyy");
                        this.txtIDExpiryDate.Text = reader["id_expiry_date"] == DBNull.Value ? null : Convert.ToDateTime(reader["id_expiry_date"]).ToString("dd/MM/yyyy");
                        this.txtPlaceOfIssue.Text = reader["place_of_issuance"].ToString();
                        this.txtTINNo.Text = reader["tin_no"].ToString();

                        lblstatus.Text = MessageFormatter.GetFormattedSuccessMessage("Record found Successfully!");
                        //this.txtCustInfoID.Text = "";
                        // return;

                        GridView2.DataBind(); GridView0.DataBind(); GridView9.DataBind();
                        GridView1.DataBind(); GridView3.DataBind(); GridView4.DataBind();
                        GridView5.DataBind(); GridView6.DataBind(); GridView7.DataBind();
                        GridView8.DataBind();
                        //  }
                    }//end of while
                }
                else
                {
                    lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error: Record not found");
                    callClearBOx();
                    return;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
                this.txtCustInfoID.Text = "";
                callClearBOx();
                // return;
            }
            finally
            {

                objCmd = null;
                con.Close();
            }


        }


        protected void OnEdit_Income(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab2";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var inc = customer.GetCustomerIncome(recId.Trim(), "CDMA_CUSTOMER_INCOME");//int.Parse()
            //this.txtCustIncomeRefID.Text = inc.ReferenceId.ToString();
            this.txtCustNoIncome.Text = inc.CustomerNo;// == null ? string.Empty : inc.CustomerNo;
            this.ddlCustIncomeBand.SelectedValue = inc.IncomeBand;
            this.ddlCustIncomeInitDeposit.SelectedValue = inc.InitialDeposit;

            lblstatusCI.Text = inc.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(inc.ErrorMessage);

        }

        protected void OnEdit_trustClientAcct(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var tca = customer.GetTrustClientAcct(recId, "CDMA_TRUSTS_CLIENT_ACCOUNTS");
            this.txtCustNoTCAcct.Text = tca.CustomerNo;
            this.rblTCAcct.SelectedValue = tca.TrustsClientAccounts;
            this.txtCustTCAcctBeneficialName.Text = tca.NameOfBeneficialOwner;
            this.txtCustTCAcctSpouseName.Text = tca.SpouseName;
            this.txtCustTCAcctDOB.Text = Convert.ToString(tca.SpouseDateOfBirth);
            this.txtCustTCAcctOccptn.Text = tca.SpouseOccupation;
            this.txtCustTCAcctOtherScrIncome.Text = tca.OtherSourceExpectAnnInc;
            this.rblTCAcctInsiderRelation.SelectedValue = tca.InsiderRelation;
            this.txtCustTCAcctNameOfAssBiz.Text = tca.NameOfAssociatedBusiness;
            this.rblFreqIntTraveler.SelectedValue = tca.FreqInternationalTraveler;
            this.rblTCAcctPolExposed.SelectedValue = tca.PoliticallyExposedPerson;
            this.rtlTCAcctPowerOfAttony.SelectedValue = tca.PowerOfAttorney;
            this.txtTCAcctHoldersName.Text = tca.HolderName;
            this.txtTCAcctAddress.Text = tca.Address;
            this.ddlTCAcctCountry.SelectedValue = tca.Country;
            this.ddlTCAcctNationality.SelectedValue = tca.Nationality;
            this.txtTCAcctTelPhone.Text = tca.TelephoneNumber;
            this.txtCustTCAcctSrcOfFund.Text = tca.SourcesOfFundToAccount;


            this.lblstatusTCAcct.Text = tca.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(tca.ErrorMessage);//"Record Loaded" : tca.ErrorMessage;
        }

        protected void OnEdit_NextOfKin(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab4";
            LinkButton lnk = sender as LinkButton;
            try
            {
                string recId = lnk.Attributes["RecId"];
                customer = new CustomerRepository();
                var nok = customer.GetNextOfKin(recId.Trim(), "CDMA_INDIVIDUAL_NEXT_OF_KIN");//recId.Trim()
                this.txtCustNoNOK.Text = nok.CustomerNo;
                this.DDLCustNOKTitle.SelectedValue = nok.Title;
                this.txtCustNOKSurname.Text = nok.Surname;
                this.txtCustNOKfirstName.Text = nok.FirstName;
                this.txtCustNOKOtherName.Text = nok.OtherName;
                this.txtCustNOKDateOfBirth.Text = Convert.ToString(nok.DateOfBirth);
                this.ddlCustNOKSex.SelectedValue = nok.Sex;
                this.ddlCustNOKReltnship.SelectedValue = nok.Relationship;
                this.txtCustNOKOfficeNo.Text = nok.OfficeNo;
                this.txtCustNOKMobileNo.Text = nok.MobileNo;
                this.txtCustNOKEmail.Text = nok.EmailAddress;
                this.txtCustNOKHouseNo.Text = nok.HouseNo;
                this.ddlCustNOKIdType.SelectedValue = nok.IDType;
                this.txtCustNOKIssuedDate.Text = Convert.ToString(nok.IDIssueDate);
                this.txtCustNOKExpiryDate.Text = Convert.ToString(nok.IDExpiryDate);
                this.txtCustNOKPermitNo.Text = nok.ResidentPermitNo;
                this.txtCustNOKPlaceOfIssue.Text = nok.PlaceOfIssuance;
                this.txtCustNOKStreetName.Text = nok.StreetName;
                this.txtCustNOKBusstop.Text = nok.NearestBStop;
                this.txtCustNOKZipCode.Text = nok.ZipCode;
                this.ddlCustNOKCountry.SelectedValue = nok.Country;
                this.ddlCustNOKState.SelectedValue = nok.State;
                this.ddlCustNOKLGA.SelectedValue = nok.LGA;
                this.txtCustNOKCity.Text = nok.CityTown;


                this.lblstatusNOK.Text = nok.ErrorMessage == string.Empty ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(nok.ErrorMessage);//"Record Loaded" : nok.ErrorMessage;
            }
            catch (NullReferenceException ex)
            {
                //throw ex;
                this.lblstatusNOK.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message + ex.InnerException);
            }
        }

        protected void OnEdit_Foreigner(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab5";
            LinkButton lnk = sender as LinkButton;

            string customer_no = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetForeigner(customer_no, "CDMA_FOREIGN_DETAILS");
            this.lblstatusFD.Text = f.ErrorMessage;
            this.txtCustNoForgner.Text = f.CustomerNo;
            //this.txtCustIDForgnerRefID.Text = f.ReferenceId.ToString();
            //this.txtCustFfName.Text = f.CustomerName;

            this.txtCustFPassPermit.Text = f.PassportResidencePermit.ToString();
            this.txtCustFIssueDate.Text = f.PermitIssueDate.ToString("dd/MM/yyyy");
            this.txtCustFExpiryDate.Text = f.PermitExpiryDate.ToString("dd/MM/yyyy");
            this.txtCustfForeignAddy.Text = f.ForeignAddress.ToString();
            this.txtCustfCity.Text = f.city.ToString();
            this.ddlCustfCountry.Text = f.country.ToString();
            //

            this.txtCustfForeignPhoneNo.Text = f.foreign_tel_number.ToString();
            this.txtCustfZipPostalCode.Text = f.zip_postal_code.ToString();
            this.txtCustfPurposeOfAcc.Text = f.purpose_of_account.ToString();



            lblstatusFD.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);//"Record Loaded" : f.ErrorMessage; ;
        }
        protected void OnEdit_jurat(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab6";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var j = customer.GetJuratInfo(recId, "CDMA_JURAT");
            try
            {
                this.txtCustJNameOfInerpreter.Text = j.NameOfInterpreter.ToString();
                this.txtCustJAddyOfInterperter.Text = j.AddressOfInterpreter;
                this.txtCustJPhoneNo.Text = j.TelephoneNo;
                this.txtCustJLangOfInterpretation.Text = j.LanguageOfInterpretation;

                lblstatusJurat.Text = j.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(j.ErrorMessage);//"Record Loaded" : f.ErrorMessage;
            }
            catch (Exception ex)
            {

                lblstatusJurat.Text = ex.Message;
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


            customer = new CustomerRepository();
            var a = customer.GetAccountInfo4Makers(customer_no.Trim(), account_no.Trim());
            this.txtCustNoAI.Text = a.CustomerNo;
            //this.rbtCustAIAccHolders.SelectedValue = a.AccountHolder;//, "CDMA_ACCOUNT_INFO c", "CDMA_ACCT_SERVICES_REQUIRED a"
            this.ddlCustAITypeOfAcc.SelectedValue = a.TypeOfAccount;
            this.txtCustAIAccNo.Text = a.AccountNumber;
            this.txtCustAIAccOfficer.Text = a.AccountOfficer;
            this.txtCustAIAccTitle.Text = a.AccountTitle;
            this.ddlCustAIBranch.SelectedValue = a.Branch;
            this.ddlCustAIBranchClass.SelectedValue = a.BranchClass;
            this.ddlCustAIOnlineTrnsfLimit.SelectedValue = a.OnlineTransferLimitRange;
            this.ddlCustAIBizDiv.SelectedValue = a.BusinessDivision;
            this.ddlCustBizSeg.SelectedValue = a.BizSegment;
            this.ddlCustBizSize.SelectedValue = a.BizSize;
            this.txtCustAIBVNNo.Text = a.BVNNo;
            this.rblCAVRequired.Text = a.CAVRequired;
            this.txtCustAICustIc.Text = a.CustomerIc;
            //this.txtCustAICustId.Text = a.CustomerId;
            this.ddlCustAICustSeg.SelectedValue = a.CustomerSegment;
            this.ddlCustAICusType.SelectedValue = a.CustomerType;
            this.txtCustAIOpInsttn.Text = a.OperatingInstruction;
            this.ddlCustAIOriginatingBranch.SelectedValue = a.OriginatingBranch;
            //Account Services Required
            this.rbtASRCardRef.SelectedValue = a.CardPreference == string.Empty ? "N" : a.CardPreference;
            this.rbtASREBP.SelectedValue = a.ElectronicBankingPreference == string.Empty ? "N" : a.ElectronicBankingPreference;
            this.rbtASRStatementPref.SelectedValue = a.StatementPreferences == string.Empty ? "N" : a.StatementPreferences;
            this.rbtASRTransAlertPref.SelectedValue = a.TranxAlertPreference == string.Empty ? "N" : a.TranxAlertPreference;
            this.rbtASRStatementFreq.SelectedValue = a.StatementFrequency == string.Empty ? "N" : a.StatementFrequency;
            this.rbtASRChequeBookReqtn.SelectedValue = a.ChequeBookRequisition == string.Empty ? "N" : a.ChequeBookRequisition;
            this.rbtASRChequeLeaveReq.SelectedValue = a.ChequeLeavesRequired == string.Empty ? "N" : a.ChequeLeavesRequired;
            this.rbtASRChequeConfmtn.SelectedValue = a.ChequeConfirmation == string.Empty ? "N" : a.ChequeConfirmation;
            this.rbtASRChequeConfmtnThreshold.SelectedValue = a.ChequeConfirmationThreshold == string.Empty ? "N" : a.ChequeConfirmationThreshold;
            this.ddlASRChequeConfmtnThresholdRange.SelectedValue = a.ChequeConfirmationThresholdRange;
            this.rbtASROnlineTraxLimit.SelectedValue = a.OnlineTransferLimit == string.Empty ? "N" : a.OnlineTransferLimit;
            this.rbtASRToken.SelectedValue = a.Token;
            this.txtASRAcctSignitory.Text = a.AccountSignatory;
            this.txtASR2ndAcctSignitory.Text = a.SecondSignatory;

            lblstatusAI.Text = a.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(a.ErrorMessage);//"Record Loaded" : f.ErrorMessage;
        }

        protected void OnEdit_Employment(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab8";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetEmployment(recId, "CDMA_EMPLOYMENT_DETAILS");
            this.txtCustNoEmpInfo.Text = f.CustomerNo;


            this.txtCustNoEmpInfo.Text = f.CustomerNo;
            this.ddlCustEIEmpStatus.Text = f.EmploymentStatus;
            this.txtCustEIEmployerName.Text = f.EmployerInstName;
            this.txtCustEIDateOfEmp.Text = f.EmploymentDate.ToString("dd/MM/yyyy");
            this.ddlCustEISectorClass.Text = f.SectorClass;
            this.ddlCustEISubSector.Text = f.SubSector;
            this.ddlCustEINatureOfBiz.Text = f.NatureOfBuzOcc;
            this.ddlCustEIIndustrySeg.Text = f.IndustrySegment;



            lblstatusEI.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);//"Record Loaded" : f.ErrorMessage;
        }



        protected void btnJurat_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab6";
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_jurat";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;
            //objCmd.Parameters.Add("p_reference_id", OracleDbType.Varchar2).Value = this.txtCustRefIDLegal.Text == "" ? null : this.txtCustRefIDLegal.Text;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = this.txtCustNoJurat.Text;
            objCmd.Parameters.Add("p_date_of_oath", OracleDbType.Date).Value = this.txtCustJDateOfOath.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtCustJDateOfOath.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);//Convert.ToDateTime(this.txtCustJDateOfOath.Text);

            objCmd.Parameters.Add("p_name_of_interpreter", OracleDbType.Varchar2).Value = this.txtCustJNameOfInerpreter.Text;
            objCmd.Parameters.Add("p_address_of_interpreter", OracleDbType.Varchar2).Value = this.txtCustJAddyOfInterperter.Text;
            objCmd.Parameters.Add("p_telephone_no", OracleDbType.Varchar2).Value = this.txtCustJPhoneNo.Text;
            objCmd.Parameters.Add("p_language_of_interpretation", OracleDbType.Varchar2).Value = this.txtCustJLangOfInterpretation.Text;

            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";



            //
            if (txtCustNoJurat.Text == string.Empty)
            {
                this.lblstatusJurat.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_customer_id(this.txtCustNoJurat.Text) == 0)
                {
                    this.lblstatusJurat.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");
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
                        this.lblstatusJurat.Text = MessageFormatter.GetFormattedSuccessMessage("Record Added Successfully");

                        EmailHelper Emailer = new EmailHelper();
                        Emailer.NotificationMailSender((String)(Session["UserID"]));

                        this.txtCustNoJurat.Text = this.txtCustJNameOfInerpreter.Text = this.txtCustJAddyOfInterperter.Text = this.txtCustJPhoneNo.Text = txtCustJLangOfInterpretation.Text = txtCustJDateOfOath.Text = "";

                        GridView4.DataBind();

                    }
                    else
                    {
                        this.lblstatusJurat.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                this.lblstatusJurat.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();
            }
        }
        protected void btnNextofKin_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab4";
            OracleCommand objCmd = new OracleCommand();

            if (this.txtCustNOKIssuedDate.Text != string.Empty || this.txtCustNOKExpiryDate.Text != string.Empty)
            {
                DateTime IssueDate = DateTime.ParseExact(this.txtCustNOKIssuedDate.Text, "dd/MM/yyyy", null);
                DateTime ExpiryDate = DateTime.ParseExact(this.txtCustNOKExpiryDate.Text, "dd/MM/yyyy", null);
                if (mp.checkDateDiff(Convert.ToDateTime(IssueDate, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat), Convert.ToDateTime(ExpiryDate, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat)) == false)
                {

                    lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Incorrect: Issue date can not be ahead of Expiry date.");
                    return;

                }
            }
            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_next_of_kin";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = this.txtCustNoNOK.Text;
            objCmd.Parameters.Add("p_title", OracleDbType.Varchar2).Value = this.DDLCustNOKTitle.SelectedValue;
            objCmd.Parameters.Add("p_surname", OracleDbType.Varchar2).Value = this.txtCustNOKSurname.Text;
            //
            objCmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = this.txtCustNOKfirstName.Text;
            objCmd.Parameters.Add("p_other_name", OracleDbType.Varchar2).Value = this.txtCustNOKOtherName.Text;
            objCmd.Parameters.Add("p_date_of_birth", OracleDbType.Date).Value = this.txtCustNOKDateOfBirth.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtCustNOKDateOfBirth.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            objCmd.Parameters.Add("p_sex", OracleDbType.Varchar2).Value = this.ddlCustNOKSex.SelectedValue;
            objCmd.Parameters.Add("p_relationship", OracleDbType.Varchar2).Value = this.ddlCustNOKReltnship.SelectedValue;
            objCmd.Parameters.Add("p_office_no", OracleDbType.Varchar2).Value = this.txtCustNOKOfficeNo.Text;
            objCmd.Parameters.Add("p_mobile_no", OracleDbType.Varchar2).Value = this.txtCustNOKMobileNo.Text;
            objCmd.Parameters.Add("p_email_address", OracleDbType.Varchar2).Value = this.txtCustNOKEmail.Text;
            //
            objCmd.Parameters.Add("p_house_number", OracleDbType.Varchar2).Value = this.txtCustNOKHouseNo.Text;
            objCmd.Parameters.Add("p_identification_type", OracleDbType.Varchar2).Value = this.ddlCustNOKIdType.SelectedValue == "Others" ? this.txtCustNOKIdType.Text : this.ddlCustNOKIdType.SelectedValue;
            objCmd.Parameters.Add("p_id_issue_date", OracleDbType.Date).Value = this.txtCustNOKIssuedDate.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtCustNOKIssuedDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            objCmd.Parameters.Add("p_id_expiry_date", OracleDbType.Date).Value = this.txtCustNOKExpiryDate.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtCustNOKExpiryDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            objCmd.Parameters.Add("p_resident_permit_number", OracleDbType.Varchar2).Value = this.txtCustNOKPermitNo.Text;
            objCmd.Parameters.Add("p_place_of_issuance", OracleDbType.Varchar2).Value = this.txtCustNOKPlaceOfIssue.Text;
            objCmd.Parameters.Add("p_street_name", OracleDbType.Varchar2).Value = this.txtCustNOKStreetName.Text;
            //
            objCmd.Parameters.Add("p_nearest_bus_stop_landmark", OracleDbType.Varchar2).Value = this.txtNearestBusStop.Text;
            objCmd.Parameters.Add("p_city_town", OracleDbType.Varchar2).Value = this.txtCustNOKCity.Text;
            objCmd.Parameters.Add("p_lga", OracleDbType.Varchar2).Value = this.ddlCustNOKCountry.SelectedValue == "NGR" ? this.ddlCustNOKState.SelectedValue : this.txtCustNOKLGA.Text;
            objCmd.Parameters.Add("p_zip_postal_code", OracleDbType.Varchar2).Value = this.txtCustNOKZipCode.Text;
            objCmd.Parameters.Add("p_state", OracleDbType.Varchar2).Value = this.ddlCustNOKCountry.SelectedValue == "NGR" ? this.ddlCustNOKState.SelectedValue : this.txtCustNOKState.Text;
            objCmd.Parameters.Add("p_country", OracleDbType.Varchar2).Value = this.ddlCustNOKCountry.SelectedValue;

            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = new Audit().IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";

            //lblfamilyinfo.Text = Profile.GeneralSetupModule.GrantMaker ? "MAKER" : "CHECKER";

            if (this.txtCustNoNOK.Text == string.Empty)
            {
                this.lblstatusNOK.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }


            try
            {
                //
                getCustID rst = new getCustID();
                //

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_customer_id(this.txtCustNoNOK.Text) == 0)
                {
                    this.lblstatusNOK.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");
                    return;

                }

                int rstCmd = objCmd.ExecuteNonQuery();

                if (rstCmd == -1)
                {
                    this.lblstatusNOK.Text = MessageFormatter.GetFormattedSuccessMessage("Next of Kin Record Added Successfully");
                    lblstatusNOK.Focus();
                    txtCustNoNOK.Text = txtCustNOKSurname.Text = txtCustNOKfirstName.Text = txtCustNOKOtherName.Text = txtCustNOKDateOfBirth.Text = txtCustNOKOfficeNo.Text = txtCustNOKMobileNo.Text = txtCustNOKEmail.Text = txtCustNOKHouseNo.Text = txtCustNOKIssuedDate.Text = txtCustNOKExpiryDate.Text = txtCustNOKPermitNo.Text = txtCustNOKPlaceOfIssue.Text = txtCustNOKStreetName.Text = txtCustNOKBusstop.Text = txtCustNOKCity
.Text = ddlCustNOKLGA.SelectedValue = txtCustNOKZipCode.Text = txtCustNOKCity.Text = "";

                    ddlCustNOKIdType.SelectedValue =
                    DDLCustNOKTitle.SelectedValue = ddlCustNOKSex.SelectedValue = ddlCustNOKReltnship.SelectedValue = ddlCustNOKState.SelectedValue = ddlCustNOKCountry.SelectedValue = "";

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));
                }
                else
                {
                    this.lblstatusNOK.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                }

            }
            catch (Exception ex)
            {
                this.lblstatusNOK.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
            }
            finally
            {
                objCmd = null;
                con.Close();
            }
        }
        protected void btnCustIncome_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab2";
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_customer_income";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustNoIncome.Text;

            //objCmd.Parameters.Add("p_reference_id", OracleDbType.Varchar2).Value = this.txtCustIncomeRefID.Text == "" ? null : this.txtCustIncomeRefID.Text;

            objCmd.Parameters.Add("p_income_band", OracleDbType.Varchar2).Value = this.ddlCustIncomeBand.SelectedValue;

            objCmd.Parameters.Add("p_initial_deposit", OracleDbType.Varchar2).Value = this.ddlCustIncomeInitDeposit.SelectedValue;

            //
            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            //objCmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = "";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";
            //
            if (txtCustNoIncome.Text == string.Empty)
            {
                this.lblstatusCI.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");
                return;
            }
            //
            try
            {

                getCustID rst = new getCustID();

                if (rst.get_customer_id(txtCustNoIncome.Text) == 0)
                {
                    this.lblstatusCI.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");
                    return;

                }
                else
                {
                    // int rstCmd=0;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();//
                    }

                    int rstCmd = objCmd.ExecuteNonQuery();

                    if (rstCmd == -1)
                    {
                        this.lblstatusCI.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Income Record Added Successfully");
                        this.lblstatusCI.Focus();
                        ddlCustIncomeBand.SelectedValue = ddlCustIncomeInitDeposit.SelectedValue = "";
                        GridView1.DataBind();

                        EmailHelper Emailer = new EmailHelper();
                        Emailer.NotificationMailSender((String)(Session["UserID"]));
                        //MailSender((String)(Session["UserID"]));


                    }
                    else
                    {
                        this.lblstatusCI.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                this.lblstatusCI.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message + ex.Source);
            }
            finally
            {
                objCmd = null;
                con.Close();
            }
        }


        //public void MailSender(string UserName)
        //{
        //    EmailHelper Email = new EmailHelper();
        //    StringBuilder sb = new StringBuilder();



        //    try
        //    {
        //        //////////////////////////////////////////////////////////////////////////////////////
        //        // Create the mail message
        //        MailMessage mail = new MailMessage();
        //        // Set the host, 
        //        SmtpClient smtp = new SmtpClient();
        //        // Set the from address and to address
        //        mail.From = new MailAddress(Settings.Default.Account);
        //        mail.To.Add(Email.getUserEmailwithUserID(UserName));//"taofeekcomsc@gmail.com"

        //        //StringBuilder sb = new StringBuilder();
        //        // Set the subject and body
        //        mail.Subject = "CDMS customer record update notification!!!";

        //        mail.Body = Email.FetchMailBody(UserName);//n

        //        smtp.UseDefaultCredentials = false;
        //        smtp.Credentials = new NetworkCredential(Settings.Default.Account, Settings.Default.Password);
        //        smtp.Port = Convert.ToInt32(Settings.Default.Port);
        //        smtp.Host = Settings.Default.SmtpHost;//"smtp.office365.com";
        //        Settings.Default.Save();
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.EnableSsl = false;

        //        smtp.Send(mail);


        //        sb.AppendLine();
        //        // sb.Append(" " + Util.getUserName(n) +" ("+ getEmpEmailwithID(Convert.ToInt32(n)).ToString() + ") , ");



        //    }
        //    catch (Exception ex)
        //    {
        //        // this.labelMessage.Text = MessageFormatter.GetFormattedErrorMessage("Error sending reminder to Staff");
        //        // Errorlog.logError(ex.Message, ex.Source, ex.StackTrace, User.Identity.Name);
        //        throw ex;
        //    }

        //}

        protected void btnCustTCAcctSave_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_trusts_client_accounts";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = this.txtCustNoTCAcct.Text;
            objCmd.Parameters.Add("p_trusts_client_accounts", OracleDbType.Varchar2).Value = this.rblTCAcct.SelectedValue;
            objCmd.Parameters.Add("p_name_of_beneficial_owner", OracleDbType.Varchar2).Value = this.txtCustTCAcctBeneficialName.Text;
            objCmd.Parameters.Add("p_spouse_name", OracleDbType.Varchar2).Value = this.txtCustTCAcctSpouseName.Text;
            objCmd.Parameters.Add("p_spouse_date_of_birth", OracleDbType.Date).Value = this.txtCustTCAcctDOB.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtCustTCAcctDOB.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            objCmd.Parameters.Add("p_spouse_occupation", OracleDbType.Varchar2).Value = this.txtCustTCAcctOccptn.Text;
            //objCmd.Parameters.Add("p_cheque_book_requisition", OracleDbType.Char).Value = this.rbtASRChequeBookReqtn.SelectedValue;
            objCmd.Parameters.Add("p_sources_of_fund_to_account", OracleDbType.Varchar2).Value = this.txtCustTCAcctSrcOfFund.Text;
            objCmd.Parameters.Add("p_other_source_expect_ann_inc", OracleDbType.Varchar2).Value = this.txtCustTCAcctOtherScrIncome.Text;
            objCmd.Parameters.Add("p_name_of_associated_business", OracleDbType.Varchar2).Value = this.txtCustTCAcctNameOfAssBiz.Text;
            objCmd.Parameters.Add("p_freq_international_traveler", OracleDbType.Varchar2).Value = this.rblFreqIntTraveler.SelectedValue;
            objCmd.Parameters.Add("p_insider_relation", OracleDbType.Varchar2).Value = this.rblTCAcctInsiderRelation.SelectedValue;
            objCmd.Parameters.Add("p_politically_exposed_person", OracleDbType.Varchar2).Value = this.rblTCAcctPolExposed.SelectedValue;
            objCmd.Parameters.Add("p_power_of_attorney", OracleDbType.Varchar2).Value = this.rtlTCAcctPowerOfAttony.SelectedValue;

            objCmd.Parameters.Add("p_holder_name", OracleDbType.Varchar2).Value = this.txtTCAcctHoldersName.Text;
            objCmd.Parameters.Add("p_address", OracleDbType.Varchar2).Value = this.txtTCAcctAddress.Text;
            objCmd.Parameters.Add("p_country", OracleDbType.Varchar2).Value = this.ddlTCAcctCountry.SelectedValue;
            objCmd.Parameters.Add("p_nationality", OracleDbType.Varchar2).Value = this.ddlTCAcctNationality.SelectedValue;
            objCmd.Parameters.Add("p_telephone_number", OracleDbType.Varchar2).Value = this.txtTCAcctTelPhone.Text;

            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";

            //
            if (txtCustNoTCAcct.Text == string.Empty)
            {
                this.lblstatusTCAcct.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_customer_id(txtCustNoTCAcct.Text) == 0)
                {
                    this.lblstatusTCAcct.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");
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

                        this.lblstatusTCAcct.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Record Added Successfully");
                        this.lblstatusTCAcct.Focus();

                        txtCustTCAcctSpouseName.Text = txtCustTCAcctBeneficialName.Text = txtCustTCAcctDOB.Text = txtCustTCAcctOccptn.Text = txtCustTCAcctSrcOfFund.Text = txtCustTCAcctOtherScrIncome.Text = txtCustTCAcctNameOfAssBiz.Text = txtTCAcctHoldersName.Text = txtTCAcctAddress.Text = txtTCAcctTelPhone.Text = "";
                        rblTCAcct.SelectedValue = rblFreqIntTraveler.SelectedValue = rbtASRTransAlertPref.SelectedValue = rblTCAcctInsiderRelation.SelectedValue = rblTCAcctPolExposed.SelectedValue = rtlTCAcctPowerOfAttony.SelectedValue = ddlTCAcctCountry.SelectedValue = ddlTCAcctNationality.SelectedValue = "";

                        EmailHelper Emailer = new EmailHelper();
                        Emailer.NotificationMailSender((String)(Session["UserID"]));
                    }
                    else
                    {

                        this.lblstatusTCAcct.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("SelectedValue which is invalid"))
                {
                    lblstatusTCAcct.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Record Added Successfully");
                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));
                }
                else
                {
                    this.lblstatusTCAcct.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
                }
            }
            finally
            {
                objCmd = null;
                con.Close();


            }
        }
        protected void btnAccountInfo_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab7";
            if (this.rbtCustAIAccHolders.SelectedValue == string.Empty)
            {
                lblstatusAI.Text = MessageFormatter.GetFormattedNoticeMessage("Please specify if you are an account holder or not");
                return;
            }

            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;


            objCmd.CommandText = "pkg_cdms_maker.prc_account_info";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = this.txtCustNoAI.Text;
            objCmd.Parameters.Add("p_account_holder", OracleDbType.Char).Value = this.rbtCustAIAccHolders.SelectedValue;
            objCmd.Parameters.Add("p_type_of_account", OracleDbType.Varchar2).Value = this.ddlCustAITypeOfAcc.SelectedValue;
            objCmd.Parameters.Add("p_account_number", OracleDbType.Varchar2).Value = this.txtCustAIAccNo.Text;

            objCmd.Parameters.Add("p_account_officer", OracleDbType.Varchar2).Value = this.txtCustAIAccOfficer.Text;
            objCmd.Parameters.Add("p_account_title", OracleDbType.Varchar2).Value = this.txtCustAIAccTitle.Text;
            objCmd.Parameters.Add("p_branch", OracleDbType.Varchar2).Value = this.ddlCustAIBranch.SelectedValue;
            objCmd.Parameters.Add("p_branch_class", OracleDbType.Varchar2).Value = this.ddlCustAIBranchClass.SelectedValue;
            objCmd.Parameters.Add("p_business_division", OracleDbType.Varchar2).Value = this.ddlCustAIBizDiv.SelectedValue;
            //
            objCmd.Parameters.Add("p_business_segment", OracleDbType.Varchar2).Value = this.ddlCustBizSeg.SelectedValue;
            objCmd.Parameters.Add("p_business_size", OracleDbType.Varchar2).Value = this.ddlCustBizSize.SelectedValue;
            objCmd.Parameters.Add("p_bvn_number", OracleDbType.Varchar2).Value = this.txtCustAIBVNNo.Text;
            objCmd.Parameters.Add("p_cav_required", OracleDbType.Varchar2).Value = this.rblCAVRequired.Text;
            //objCmd.Parameters.Add("p_cheque_confirmation", OracleDbType.Varchar2).Value = this.rbtASRChequeConfmtn.SelectedValue;
            //objCmd.Parameters.Add("p_cheque_confirm_threshold", OracleDbType.Varchar2).Value = this.ddlCustAIChequeConftnThreshold.SelectedValue;
            objCmd.Parameters.Add("p_customer_ic", OracleDbType.Varchar2).Value = this.txtCustAICustIc.Text;
            //objCmd.Parameters.Add("p_customer_id", OracleDbType.Varchar2).Value = this.txtCustAICustId.Text;
            objCmd.Parameters.Add("p_customer_segment", OracleDbType.Varchar2).Value = this.ddlCustAICustSeg.SelectedValue;
            objCmd.Parameters.Add("p_customer_type", OracleDbType.Varchar2).Value = this.ddlCustAICusType.SelectedValue;
            objCmd.Parameters.Add("p_online_transfer_limit_range", OracleDbType.Varchar2).Value = this.ddlCustAIOnlineTrnsfLimit.SelectedValue;
            objCmd.Parameters.Add("p_operating_instruction", OracleDbType.Varchar2).Value = this.txtCustAIOpInsttn.Text;
            objCmd.Parameters.Add("p_originating_branch", OracleDbType.Varchar2).Value = this.ddlCustAIOriginatingBranch.SelectedValue;


            //account services required.


            objCmd.Parameters.Add("p_card_preference", OracleDbType.Char).Value = this.rbtASRCardRef.SelectedValue;
            objCmd.Parameters.Add("p_electronic_banking_prefer", OracleDbType.Char).Value = this.rbtASREBP.SelectedValue;
            objCmd.Parameters.Add("p_statement_preferences", OracleDbType.Char).Value = this.rbtASRStatementPref.SelectedValue;
            objCmd.Parameters.Add("p_transaction_alert_preference", OracleDbType.Char).Value = this.rbtASRTransAlertPref.SelectedValue;
            objCmd.Parameters.Add("p_statement_frequency", OracleDbType.Char).Value = this.rbtASRStatementFreq.SelectedValue;
            //objCmd.Parameters.Add("p_cheque_book_requisition", OracleDbType.Char).Value = this.rbtASRChequeBookReqtn.SelectedValue;
            objCmd.Parameters.Add("p_cheque_book_requisition", OracleDbType.Char).Value = this.rbtASRChequeBookReqtn.SelectedValue;
            objCmd.Parameters.Add("p_cheque_confirmation", OracleDbType.Char).Value = this.rbtASRChequeConfmtn.SelectedValue;
            objCmd.Parameters.Add("p_cheque_confirm_threshold", OracleDbType.Char).Value = this.rbtASRChequeConfmtnThreshold.SelectedValue;
            objCmd.Parameters.Add("p_cheque_confirm_threshldrange", OracleDbType.Char).Value = this.ddlASRChequeConfmtnThresholdRange.SelectedValue;
            objCmd.Parameters.Add("p_cheque_leaves_required", OracleDbType.Char).Value = this.rbtASRChequeLeaveReq.SelectedValue;

            objCmd.Parameters.Add("p_online_transfer_limit", OracleDbType.Char).Value = this.rbtASROnlineTraxLimit.SelectedValue;
            objCmd.Parameters.Add("p_token", OracleDbType.Char).Value = this.rbtASRToken.SelectedValue;
            objCmd.Parameters.Add("p_account_signatory", OracleDbType.Varchar2).Value = this.txtASRAcctSignitory.Text;
            objCmd.Parameters.Add("p_second_signatory", OracleDbType.Varchar2).Value = this.txtASR2ndAcctSignitory.Text;

            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = new Audit().IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = this.lblstatusAI.Text = "MAKER";
            //
            if (txtCustNoAI.Text == string.Empty)
            {
                lblstatusAI.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }




            try
            {

                //
                getCustID rst = new getCustID();
                //

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_customer_id(txtCustNoAI.Text.Trim()) == 0)
                {
                    lblstatusAI.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");
                    return;

                }

                int rstCmd = objCmd.ExecuteNonQuery();
                lblstatusAI.Text = rstCmd.ToString();
                if (rstCmd == -1)
                {
                    //txtCustAICustId.Text =
                    lblstatusAI.Text = MessageFormatter.GetFormattedSuccessMessage("Account Record Added Successfully");
                    //lblstatusAI.Focus();

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));

                    txtCustNoAI.Text = txtCustAIAccNo.Text = txtCustAIAccOfficer.Text = txtCustAIAccTitle.Text = txtCustAIBVNNo.Text = rblCAVRequired.Text = txtCustAICustIc.Text = txtCustAIOpInsttn.Text = "";
                    txtASRAcctSignitory.Text = txtASR2ndAcctSignitory.Text = "";

                    ddlCustAITypeOfAcc.SelectedValue = ddlCustAIBranch.SelectedValue = ddlCustAIBranchClass.SelectedValue = ddlCustAIBizDiv.SelectedValue = ddlCustBizSeg.SelectedValue = ddlCustBizSize.SelectedValue = ddlCustAICustSeg.SelectedValue = ddlCustAICusType.SelectedValue = ddlCustAIOnlineTrnsfLimit.SelectedValue = ddlCustAIOriginatingBranch.SelectedValue = "";
                    rbtASRCardRef.SelectedValue = rbtASREBP.SelectedValue = rbtASRStatementPref.SelectedValue = rbtASRTransAlertPref.SelectedValue = rbtASRStatementFreq.SelectedValue = rbtASRChequeBookReqtn.SelectedValue = rbtASRChequeConfmtnThreshold.SelectedValue = rbtASROnlineTraxLimit.SelectedValue = rbtASRToken.SelectedValue = ddlASRChequeConfmtnThresholdRange.SelectedValue = "";

                    GridView6.DataBind();
                }
                else
                {
                    lblstatusAI.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("SelectedValue which is invalid"))
                {
                    lblstatusAI.Text = MessageFormatter.GetFormattedSuccessMessage("Account Record Added Successfully");

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));
                }
                else
                {
                    lblstatusAI.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
                }
            }
            finally
            {
                objCmd = null;
                con.Close();
            }
        }
        protected void btnEmpInfo_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab8";
            OracleCommand objCmd = new OracleCommand();

            if (txtCustNoEmpInfo.Text == string.Empty)
            {
                this.lblstatusEI.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }


            if (txtCustEIDateOfEmp.Text == string.Empty)
            {
                lblstatusEI.Text = MessageFormatter.GetFormattedErrorMessage("Employment Date Required");

                return;
            }

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_employment_details";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = this.txtCustNoEmpInfo.Text;

            objCmd.Parameters.Add("p_employment_status", OracleDbType.Varchar2).Value = this.ddlCustEIEmpStatus.SelectedItem;
            objCmd.Parameters.Add("p_employer_institution_name", OracleDbType.Varchar2).Value = this.txtCustEIEmployerName.Text;

            objCmd.Parameters.Add("p_date_of_employment", OracleDbType.Date).Value = this.txtCustEIDateOfEmp.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtCustEIDateOfEmp.Text.Trim(), CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            objCmd.Parameters.Add("p_sector_class", OracleDbType.Varchar2).Value = this.ddlCustEISectorClass.SelectedItem;
            objCmd.Parameters.Add("p_sub_sector", OracleDbType.Varchar2).Value = this.ddlCustEISubSector.SelectedItem;
            objCmd.Parameters.Add("p_nature_of_biz_occupation", OracleDbType.Varchar2).Value = this.ddlCustEINatureOfBiz.SelectedValue == "Others" ? this.txtCustEINatureOfBiz.Text : this.ddlCustEINatureOfBiz.SelectedValue;
            objCmd.Parameters.Add("p_industry_segment", OracleDbType.Varchar2).Value = this.ddlCustEIIndustrySeg.SelectedItem;

            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";




            try
            {


                getCustID rst = new getCustID();


                if (con.State == ConnectionState.Closed)
                    con.Open();//


                if (rst.get_customer_id(txtCustNoEmpInfo.Text) == 0)
                {
                    lblstatusEI.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");



                    return;

                }

                int rstCmd = objCmd.ExecuteNonQuery();

                if (rstCmd == -1)
                {

                    lblstatusEI.Text = MessageFormatter.GetFormattedSuccessMessage("Employment Record Added Successfully");
                    lblstatusEI.Focus();
                    txtCustNoEmpInfo.Text = ddlCustEIEmpStatus.SelectedValue = txtCustEIEmployerName.Text = txtCustEIDateOfEmp.Text = ddlCustEISectorClass.SelectedValue = ddlCustEISubSector.SelectedValue = ddlCustEINatureOfBiz.SelectedValue = ddlCustEIIndustrySeg.SelectedValue = "";
                    GridView7.DataBind();

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));
                }
                else
                {

                    lblstatusEI.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");


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
            }
        }
        protected void btnForeDet_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab5";
            OracleCommand objCmd = new OracleCommand();

            if (this.txtCustFIssueDate.Text != string.Empty || this.txtCustFExpiryDate.Text != string.Empty)
            {

                if (mp.checkDateDiff(Convert.ToDateTime(txtCustFIssueDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat),
                    Convert.ToDateTime(txtCustFExpiryDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat)) == false)
                {
                    lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Incorrect: Issue date can not be ahead of Expiry date.");
                    return;
                }
            }

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_foreign_details";
            objCmd.BindByName = true;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = this.txtCustNoForgner.Text;

            objCmd.Parameters.Add("p_foreigner", OracleDbType.Varchar2).Value = this.rbtForeigner.SelectedValue;
            objCmd.Parameters.Add("p_multiple_citizenship", OracleDbType.Varchar2).Value = this.rbtMultipleCitizenship.SelectedValue;
            objCmd.Parameters.Add("p_passport_residence_permit", OracleDbType.Varchar2).Value = this.txtCustFPassPermit.Text;

            objCmd.Parameters.Add("p_permit_issue_date", OracleDbType.Date).Value = this.txtCustFIssueDate.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtCustFIssueDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            objCmd.Parameters.Add("p_permit_expiry_date", OracleDbType.Date).Value = this.txtCustFExpiryDate.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtCustFExpiryDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            objCmd.Parameters.Add("p_foreign_address", OracleDbType.Varchar2).Value = this.txtCustfForeignAddy.Text;
            objCmd.Parameters.Add("p_foreign_tel_number", OracleDbType.Varchar2).Value = this.txtCustfForeignPhoneNo.Text;
            objCmd.Parameters.Add("p_city", OracleDbType.Varchar2).Value = this.txtCustfCity.Text;
            objCmd.Parameters.Add("p_country", OracleDbType.Varchar2).Value = this.ddlCustfCountry.SelectedValue;
            objCmd.Parameters.Add("p_zip_postal_code", OracleDbType.Varchar2).Value = this.txtCustfZipPostalCode.Text;
            objCmd.Parameters.Add("p_purpose_of_account", OracleDbType.Varchar2).Value = this.txtCustfPurposeOfAcc.Text;
            //
            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";


            //
            if (txtCustNoForgner.Text == string.Empty)
            {
                this.lblstatusFD.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }
            //
            //if (txtCustfPssprtNo.Text == string.Empty)
            //{
            //    lblstatusFD.Text = MessageFormatter.GetFormattedErrorMessage("Passport Number Required");

            //    return;
            //}

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

                        lblstatusFD.Text = MessageFormatter.GetFormattedSuccessMessage("Foreigner Record Added Successfully");
                        lblstatusFD.Focus();
                        //txtCustNoForgner.Text = 
                        rbtForeigner.Text = rbtMultipleCitizenship.Text = txtCustFPassPermit.Text = txtCustFIssueDate.Text = txtCustFExpiryDate.Text = txtCustfForeignAddy.Text = txtCustfForeignPhoneNo.Text = ddlCustfCountry.SelectedValue = txtCustfCity.Text = txtCustfPurposeOfAcc.Text = txtCustfZipPostalCode.Text = "";

                        EmailHelper Emailer = new EmailHelper();
                        Emailer.NotificationMailSender((String)(Session["UserID"]));
                    }
                    else
                    {

                        lblstatusFD.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("SelectedValue which is invalid"))
                {
                    lblstatusFD.Text = MessageFormatter.GetFormattedSuccessMessage("Foreigner Record Added Successfully");

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));
                }
                else
                {

                    lblstatusFD.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

                }

            }
            finally
            {
                objCmd = null;
                con.Close();
            }
        }

        protected void btnAdditionalInfo_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab10";
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_additional_information";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = this.txtCustNoAddInfo.Text;
            objCmd.Parameters.Add("p_annual_salary_expected_inc", OracleDbType.Varchar2).Value = this.ddlAddInfoAnnualSalary.SelectedValue;
            objCmd.Parameters.Add("p_fax_number", OracleDbType.Varchar2).Value = this.txtAddInfoFaxNumber.Text;


            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";



            //
            if (this.txtCustNoAddInfo.Text == string.Empty)
            {
                this.lblCustAddtnalInfo.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_customer_id(this.txtCustNoAddInfo.Text) == 0)
                {
                    this.lblCustAddtnalInfo.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");
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
                        this.lblCustAddtnalInfo.Text = MessageFormatter.GetFormattedSuccessMessage("Record Added Successfully");
                        this.ddlAddInfoAnnualSalary.SelectedValue = this.txtAddInfoFaxNumber.Text = "";
                        lblCustAddtnalInfo.Focus();

                        EmailHelper Emailer = new EmailHelper();
                        Emailer.NotificationMailSender((String)(Session["UserID"]));
                    }
                    else
                    {
                        this.lblCustAddtnalInfo.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {
                this.lblCustAddtnalInfo.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();
            }
        }

        protected void OnEdit_additionalInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab10";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetAdditionalInfo(recId, "CDMA_ADDITIONAL_INFORMATION");
            try
            {
                this.ddlAddInfoAnnualSalary.SelectedValue = f.AnnualSalaryExpectedInc.ToString();
                this.txtAddInfoFaxNumber.Text = f.FaxNumber.ToString();


                this.lblCustAddtnalInfo.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);//"Record Loaded" : f.ErrorMessage;
            }
            catch (Exception ex)
            {

                lblCustAddtnalInfo.Text = ex.Message;
            }

        }


        protected void btnA4FinInc_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab9";

            /////////////////
            string FinIncAcctWithOtherBanks = "";

            foreach (ListItem lst in cblA4FinIncAcctWithOtherBanks.Items)
            {
                if (lst.Selected == true)
                {
                    FinIncAcctWithOtherBanks += lst.Value + ",";
                }
            }
            /////////////////
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_auth_finance_inclusion";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.BindByName = true;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = this.txtCustNoA4FinInc.Text;
            objCmd.Parameters.Add("p_social_financial_disadvtage", OracleDbType.Varchar2).Value = this.rbtA4FinIncSocFinDisadv.Text;
            objCmd.Parameters.Add("p_social_financial_documents", OracleDbType.Varchar2).Value = this.txtA4FinIncSocFinDoc.Text;
            objCmd.Parameters.Add("p_enjoyed_tiered_kyc", OracleDbType.Varchar2).Value = this.rbtA4FinIncEnjoyKYC.Text;
            objCmd.Parameters.Add("p_risk_category", OracleDbType.Varchar2).Value = this.ddlA4FinIncRiskCat.Text;
            objCmd.Parameters.Add("p_mandate_auth_combine_rule", OracleDbType.Varchar2).Value = this.txtA4FinIncMandRule.Text;
            objCmd.Parameters.Add("p_account_with_other_banks", OracleDbType.Varchar2).Value = FinIncAcctWithOtherBanks;

            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";

            //
            if (txtCustNoA4FinInc.Text == string.Empty)
            {
                this.lblCustNoA4FinInc.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }

            try
            {

                getCustID rst = new getCustID();

                if (rst.get_customer_id(this.txtCustNoA4FinInc.Text) == 0)
                {
                    this.lblCustNoA4FinInc.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");
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
                        this.lblCustNoA4FinInc.Text = MessageFormatter.GetFormattedSuccessMessage("Record Added Successfully");


                        this.txtCustNoA4FinInc.Text = this.txtA4FinIncSocFinDoc.Text = ddlA4FinIncRiskCat.SelectedValue = txtA4FinIncMandRule.Text = "";
                        this.cblA4FinIncAcctWithOtherBanks.SelectedValue = "";
                        lblCustNoA4FinInc.Focus();

                        EmailHelper Emailer = new EmailHelper();
                        Emailer.NotificationMailSender((String)(Session["UserID"]));
                    }
                    else
                    {
                        this.lblCustNoA4FinInc.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                    }

                }
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("SelectedValue which is invalid"))
                {
                    lblCustNoA4FinInc.Text = MessageFormatter.GetFormattedSuccessMessage("Record Added Successfully");

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));
                }
                else
                {
                    this.lblCustNoA4FinInc.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
                }
            }
            finally
            {
                objCmd = null;
                con.Close();
            }
        }

        protected void OnEdit_A4FinInc(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab9";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetAuthFinInclusion(recId, "CDMA_AUTH_FINANCE_INCLUSION");
            try
            {
                this.rbtA4FinIncSocFinDisadv.SelectedValue = f.SocialFinancialDisadvtage.ToString();
                this.txtA4FinIncSocFinDoc.Text = f.SocialFinancialDocuments;
                this.rbtA4FinIncEnjoyKYC.SelectedValue = f.EnjoyedTieredKyc;
                this.ddlA4FinIncRiskCat.SelectedValue = f.RiskCategory;
                this.txtA4FinIncMandRule.Text = f.MandateAuthCombineRule;
                this.cblA4FinIncAcctWithOtherBanks.SelectedValue = f.AccountWithOtherBanks;

                this.lblCustNoA4FinInc.Text = f.ErrorMessage == null ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);//"Record Loaded" : f.ErrorMessage;
            }
            catch (Exception ex)
            {

                lblCustNoA4FinInc.Text = ex.Message;
            }

        }


        private void callClearBOx()
        {
            this.txtCustInfoID.Text = this.txtSurname.Text = this.txtFirstName.Text = this.ddlTitle.Text
 = this.txtOtherName.Text = this.txtNickname.Text = this.ddlSex.SelectedValue = this.txtDateOfBirth.Text = this.txtAge.Text =
this.txtMothersMaidenName.Text = "";
            this.txtPlacefBirth.Text = this.ddlCountryOfBirth.SelectedValue = this.txtCountryofBirth.Text = this.ddlNationality.SelectedValue = this.ddlStateOfOrigin.SelectedValue = this.ddlMaritalStatus.SelectedValue = this.txtMothersMaidenName.Text = "";
            this.txtNoOfChildren.Text = this.ddlReligion.Text = this.txtComplexion.Text = this.ddlDisability.Text = this.ddlCountryofResidence.SelectedValue = this.ddlStateOfResidence.SelectedValue = "";
            this.txtStateOfResidence.Text = this.ddlLGAofResidence.Text = this.txtCityofResidence.Text = this.txtResidentialAddy.Text = this.txtNearestBusStop.Text = this.rbtOwnedorRented.Text = "";
            this.txtZipCode.Text = this.txtMobileNo.Text = this.txtEmail.Text = this.txtMailingAddy.Text = this.ddlIDType.SelectedValue = this.txtIDType.Text = this.txtIDNo.Text = this.txtIDIssueDate.Text = this.txtIDExpiryDate.Text = this.txtPlaceOfIssue.Text = this.txtTINNo.Text = "";

        }



    }
}