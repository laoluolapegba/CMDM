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
    public partial class CorporateCustInfo : System.Web.UI.Page
    {

        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        public OracleCommand cmd = new OracleCommand();
        public OracleDataReader rdr;
        public OracleDataAdapter adapter = new OracleDataAdapter();
        private CustomerRepository customer;
        private Audit audit = new Audit();
        
        public CustomMembershipProvider mp = new CustomMembershipProvider();
        //
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //// ddlStateOfResidence.Items.Clear();
                //ddlStateOfResidence.Items.Add(new ListItem("--Select State--", ""));
                ////  ddlLGAofResidence.Items.Clear();
                //ddlLGAofResidence.Items.Add(new ListItem("--Select LGA--", ""));

                ////this.ddlCountryofResidence.AppendDataBoundItems = true;
                //this.ddlCountryOfBirth.AppendDataBoundItems = true;
                //String strConnString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
                String strQuery = "SELECT COUNTRY_NAME,COUNTRY_ABBREVIATION FROM CDMS.CDMA_COUNTRIES";
                OracleCommand objCmd = new OracleCommand();
                //OracleCommand objCmd2 = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strQuery;
                objCmd.Connection = con;
                try
                {
                    con.Open();
                    ddlCompDJurOfInc.DataSource = objCmd.ExecuteReader();
                    ddlCompDJurOfInc.DataTextField = "COUNTRY_NAME";
                    ddlCompDJurOfInc.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlCompDJurOfInc.DataBind();
                    ddlCompDJurOfInc.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));

                    ////con.Open();
                    ddlCompDCountry1.DataSource = objCmd.ExecuteReader();
                    ddlCompDCountry1.DataTextField = "COUNTRY_NAME";
                    ddlCompDCountry1.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlCompDCountry1.DataBind();
                    ddlCompDCountry1.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));

                    ddlCompDCountry2.DataSource = objCmd.ExecuteReader();
                    ddlCompDCountry2.DataTextField = "COUNTRY_NAME";
                    ddlCompDCountry2.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlCompDCountry2.DataBind();
                    ddlCompDCountry2.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));

                    ddlAddInfoCountry.DataSource = objCmd.ExecuteReader();
                    ddlAddInfoCountry.DataTextField = "COUNTRY_NAME";
                    ddlAddInfoCountry.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlAddInfoCountry.DataBind();
                    ddlAddInfoCountry.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));


                    ddlPOACountry.DataSource = objCmd.ExecuteReader();
                    ddlPOACountry.DataTextField = "COUNTRY_NAME";
                    ddlPOACountry.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlPOACountry.DataBind();
                    ddlPOACountry.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));


                    ddlPOANationality.DataSource = objCmd.ExecuteReader();
                    ddlPOANationality.DataTextField = "COUNTRY_NAME";
                    ddlPOANationality.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlPOANationality.DataBind();
                    ddlPOANationality.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));


                    ddlDIRNationality.DataSource = objCmd.ExecuteReader();
                    ddlDIRNationality.DataTextField = "COUNTRY_NAME";
                    ddlDIRNationality.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlDIRNationality.DataBind();
                    ddlDIRNationality.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));

                    ddlDIRAddyCountry.DataSource = objCmd.ExecuteReader();
                    ddlDIRAddyCountry.DataTextField = "COUNTRY_NAME";
                    ddlDIRAddyCountry.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlDIRAddyCountry.DataBind();
                    ddlDIRAddyCountry.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));

                    ddlDIRFNationality.DataSource = objCmd.ExecuteReader();
                    ddlDIRFNationality.DataTextField = "COUNTRY_NAME";
                    ddlDIRFNationality.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlDIRFNationality.DataBind();
                    ddlDIRFNationality.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));

                    ddlDIRMCountry.DataSource = objCmd.ExecuteReader();
                    ddlDIRMCountry.DataTextField = "COUNTRY_NAME";
                    ddlDIRMCountry.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlDIRMCountry.DataBind();
                    ddlDIRMCountry.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));

                    ddlDIRNOKCountry.DataSource = objCmd.ExecuteReader();
                    ddlDIRNOKCountry.DataTextField = "COUNTRY_NAME";
                    ddlDIRNOKCountry.DataValueField = "COUNTRY_ABBREVIATION";
                    ddlDIRNOKCountry.DataBind();
                    ddlDIRNOKCountry.Items.Insert(0, new ListItem("--SELECT COUNTRY--", ""));
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
                String strQuery13 = "SELECT TYPE_ID,CUSTOMER_TYPE FROM CDMS.CDMA_CUSTOMER_TYPE";

                //OracleCommand objCmd = new OracleCommand();
                OracleCommand objCmd2 = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd2.CommandType = CommandType.Text;
                objCmd2.CommandText = strQuery13;
                objCmd2.Connection = con;
                try
                {
                    con.Open();
                    ddlCompanyType.DataSource = objCmd2.ExecuteReader();
                    ddlCompanyType.DataTextField = "CUSTOMER_TYPE";
                    ddlCompanyType.DataValueField = "CUSTOMER_TYPE";
                    ddlCompanyType.DataBind();
                    ddlCompanyType.Items.Insert(0, new ListItem("--SELECT TYPE--", ""));
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
                String strQuery8 = "SELECT BUSINESS_CODE,BUSINESS FROM CDMS.CDMA_NATURE_OF_BUSINESS";

                //OracleCommand objCmd = new OracleCommand();
                OracleCommand objCmd3 = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd3.CommandType = CommandType.Text;
                objCmd3.CommandText = strQuery8;
                objCmd3.Connection = con;
                try
                {
                    con.Open();
                    ddlCatOfBiz.DataSource = objCmd3.ExecuteReader();
                    ddlCatOfBiz.DataTextField = "BUSINESS";
                    ddlCatOfBiz.DataValueField = "BUSINESS";
                    ddlCatOfBiz.DataBind();
                    ddlCatOfBiz.Items.Insert(0, new ListItem("--SELECT BUSINESS--", ""));

                    ddlCompDOpBiz1.DataSource = objCmd3.ExecuteReader();
                    ddlCompDOpBiz1.DataTextField = "BUSINESS";
                    ddlCompDOpBiz1.DataValueField = "BUSINESS";
                    ddlCompDOpBiz1.DataBind();
                    ddlCompDOpBiz1.Items.Insert(0, new ListItem("--SELECT BUSINESS--", ""));

                    ddlCompDOpBiz2.DataSource = objCmd3.ExecuteReader();
                    ddlCompDOpBiz2.DataTextField = "BUSINESS";
                    ddlCompDOpBiz2.DataValueField = "BUSINESS";
                    ddlCompDOpBiz2.DataBind();
                    ddlCompDOpBiz2.Items.Insert(0, new ListItem("--SELECT BUSINESS--", ""));


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
                String strQuery7 = "SELECT SUBSECTOR_CODE,SUBSECTOR_NAME FROM CDMS.CDMA_INDUSTRY_SUBSECTOR";

                //OracleCommand objCmd = new OracleCommand();
                OracleCommand objCmd4 = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd4.CommandType = CommandType.Text;
                objCmd4.CommandText = strQuery7;
                objCmd4.Connection = con;
                try
                {
                    con.Open();
                    ddlCompDSector.DataSource = objCmd4.ExecuteReader();
                    ddlCompDSector.DataTextField = "SUBSECTOR_NAME";
                    ddlCompDSector.DataValueField = "SUBSECTOR_NAME";
                    ddlCompDSector.DataBind();
                    ddlCompDSector.Items.Insert(0, new ListItem("--SELECT SUBSECTOR--", ""));


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
                String strQuery11 = "SELECT BRANCH_CODE,BRANCH_NAME FROM CDMS.VW_CDMA_BRANCH ORDER BY BRANCH_NAME ASC";

                //OracleCommand objCmd = new OracleCommand();
                OracleCommand objCmd5 = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd5.CommandType = CommandType.Text;
                objCmd5.CommandText = strQuery11;
                objCmd5.Connection = con;
                try
                {
                    con.Open();
                    ddlAcctInfoDomBranch.DataSource = objCmd5.ExecuteReader();
                    ddlAcctInfoDomBranch.DataTextField = "BRANCH_NAME";
                    ddlAcctInfoDomBranch.DataValueField = "BRANCH_NAME";
                    ddlAcctInfoDomBranch.DataBind();
                    ddlAcctInfoDomBranch.Items.Insert(0, new ListItem("--SELECT BRANCH--", ""));

                    //ddlCustAIOriginatingBranch.DataSource = objCmd11.ExecuteReader();
                    //ddlCustAIOriginatingBranch.DataTextField = "BRANCH_NAME";
                    //ddlCustAIOriginatingBranch.DataValueField = "BRANCH_NAME";
                    //ddlCustAIOriginatingBranch.DataBind();
                    //ddlCustAIOriginatingBranch.Items.Insert(0, new ListItem("--SELECT BRANCH--", ""));


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
                String strQuery12 = "SELECT STOCK_EXCHANGE_ID,COUNTRY_NAME,STOCK_EXCHANGE_NAME FROM CDMS.CDMA_STOCK_EXCHANGE";

                //OracleCommand objCmd = new OracleCommand();
                OracleCommand objCmd7 = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd7.CommandType = CommandType.Text;
                objCmd7.CommandText = strQuery12;
                objCmd7.Connection = con;
                try
                {
                    con.Open();
                    ddlCompDStkExhange.DataSource = objCmd7.ExecuteReader();
                    ddlCompDStkExhange.DataTextField = "STOCK_EXCHANGE_NAME";
                    ddlCompDStkExhange.DataValueField = "STOCK_EXCHANGE_NAME";
                    ddlCompDStkExhange.DataBind();
                    ddlCompDStkExhange.Items.Insert(0, new ListItem("--SELECT STOCK EXCHANGE--", ""));



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
                String strQuery15 = "SELECT ACCOUNT_CLASS,DESCRIPTION FROM CDMS.VW_CDMA_ACCOUNT_PRODUCT_TYPE";

                //OracleCommand objCmd = new OracleCommand();
                OracleCommand objCmd8 = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd8.CommandType = CommandType.Text;
                objCmd8.CommandText = strQuery15;
                objCmd8.Connection = con;
                try
                {
                    con.Open();
                    ddlAcctInfoAccttype.DataSource = objCmd8.ExecuteReader();
                    ddlAcctInfoAccttype.DataTextField = "DESCRIPTION";
                    ddlAcctInfoAccttype.DataValueField = "DESCRIPTION";
                    ddlAcctInfoAccttype.DataBind();
                    ddlAcctInfoAccttype.Items.Insert(0, new ListItem("--SELECT TYPE--", ""));

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

        
            this.txtDIRAddyState.Visible = false;
            this.txtDIRAddyLGA.Visible = false;
            this.txtDIRIDTypeOfID.Visible = false;
            this.txtDIRNOKState.Visible = false;
            this.txtDIRNOKLGA.Visible = false;

            //this.rbtAWOB.SelectedValue = "N";

            this.txtAWOBBankName.Enabled = false;
            this.txtAWOBBankAddy.Enabled = false;
            this.txtAWOBAcctNo.Enabled = false;
            this.txtAWOBAcctName.Enabled = false;
            this.txtAWOBStatus.Enabled = false;

            
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.txtDIRMFTelephoneNo.Enabled = false;
            this.txtDIRMCity.Enabled = false;
            this.txtDIRMResPermitNo.Enabled = false;
            this.ddlDIRMCountry.Enabled = false;
            this.txtDIRMForeignAddy.Enabled = false;
            this.txtDIRMZipCode.Enabled = false;

            this.ddlDIRFNationality.Enabled = false;
            this.txtDIRFPIssueDate.Enabled = false;
            this.txtDIRFPermitNo.Enabled = false;
            this.txtDIRFPExpiryDate.Enabled = false;

            this.txtPOACustNo.Enabled = false;
            this.txtPOAAccountNo.Enabled = false;
            this.txtPOAHolderName.Enabled = false;
            this.txtPOAAddy.Enabled = false;
            this.ddlPOACountry.Enabled = false;
            this.ddlPOANationality.Enabled = false;
            this.txtPOAPhoneNo.Enabled = false;

        }

        protected void rbtAWOB_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab5";
            if (rbtAWOB.SelectedValue == "N")
            {
                this.txtAWOBBankName.Enabled = false;
                this.txtAWOBBankAddy.Enabled = false;
                this.txtAWOBAcctNo.Enabled = false;
                this.txtAWOBAcctName.Enabled = false;
                this.txtAWOBStatus.Enabled = false;
            }
            else
            {
                this.txtAWOBBankName.Enabled = true;
                this.txtAWOBBankAddy.Enabled = true;
                this.txtAWOBAcctNo.Enabled = true;
                this.txtAWOBAcctName.Enabled = true;
                this.txtAWOBStatus.Enabled = true;
            }
        }

        protected void rblPOAttorney_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab4";
            if (rblPOAttorney.SelectedValue == "N")
            {

                this.txtPOACustNo.Enabled = false;
                this.txtPOAAccountNo.Enabled = false;
                this.txtPOAHolderName.Enabled = false;
                this.txtPOAAddy.Enabled = false;
                this.ddlPOACountry.Enabled = false;
                this.ddlPOANationality.Enabled = false;
                this.txtPOAPhoneNo.Enabled = false;
            }
            else
            {

                this.txtPOACustNo.Enabled = true;
                this.txtPOAAccountNo.Enabled = true;
                this.txtPOAHolderName.Enabled = true;
                this.txtPOAAddy.Enabled = true;
                this.ddlPOACountry.Enabled = true;
                this.ddlPOANationality.Enabled = true;
                this.txtPOAPhoneNo.Enabled = true;
            }
        }

        protected void rbtDIRMultipleCitizenship_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab8";
            if (rbtDIRMultipleCitizenship.SelectedValue == "N")
            {
                this.txtDIRMFTelephoneNo.Enabled = false;
                this.txtDIRMCity.Enabled = false;
                this.txtDIRMResPermitNo.Enabled = false;
                this.ddlDIRMCountry.Enabled = false;
                this.txtDIRMForeignAddy.Enabled = false;
                this.txtDIRMZipCode.Enabled = false;
            }
            else
            {
                this.txtDIRMFTelephoneNo.Enabled = true;
                this.txtDIRMCity.Enabled = true;
                this.txtDIRMResPermitNo.Enabled = true;
                this.ddlDIRMCountry.Enabled = true;
                this.txtDIRMForeignAddy.Enabled = true;
                this.txtDIRMZipCode.Enabled = true;
            }
        }


        protected void rbtDIRForeigner_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab8";
            if (rbtDIRForeigner.SelectedValue == "N")
            {
                this.ddlDIRFNationality.Enabled = false;
                this.txtDIRFPIssueDate.Enabled = false;
                this.txtDIRFPermitNo.Enabled = false;
                this.txtDIRFPExpiryDate.Enabled = false;

                this.txtDIRMFTelephoneNo.Enabled = false;
                this.txtDIRMCity.Enabled = false;
                this.txtDIRMResPermitNo.Enabled = false;
                this.ddlDIRMCountry.Enabled = false;
                this.txtDIRMForeignAddy.Enabled = false;
                this.txtDIRMZipCode.Enabled = false;
                rbtDIRMultipleCitizenship.SelectedValue = "N";
            }
            else
            {
                this.ddlDIRFNationality.Enabled = true;
                this.txtDIRFPIssueDate.Enabled = true;
                this.txtDIRFPermitNo.Enabled = true;
                this.txtDIRFPExpiryDate.Enabled = true;

                
            }
        }


        protected void ddlDIRAddyCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab11";
            if (ddlDIRAddyCountry.SelectedValue.Trim() == "NGA")
            {
                //ddlLGAofResidence.Items.Clear();
                this.ddlDIRAddyState.AppendDataBoundItems = true;
                String strQuery = "SELECT STATE_NAME,STATE_ID FROM CDMS.SRC_CDMA_STATE ORDER BY STATE_NAME ASC";
                OracleCommand objCmd = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strQuery;
                objCmd.Connection = con;
                try
                {
                    con.Open();
                    ddlDIRAddyState.DataSource = objCmd.ExecuteReader();
                    ddlDIRAddyState.DataTextField = "STATE_NAME";
                    ddlDIRAddyState.DataValueField = "STATE_NAME";
                    ddlDIRAddyState.DataBind();
                    ddlDIRAddyState.Items.Insert(0, new ListItem("--SELECT STATE--", ""));

                    //this.btnUpdateDQIParam.Visible = false;
                    //this.GridView1.Visible = false;
                    //this.GridView1.Visible = true;
                    this.txtDIRAddyState.Visible = false;
                    this.ddlDIRAddyState.Visible = true;

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
                this.txtDIRAddyState.Visible = true;
                this.ddlDIRAddyState.Visible = false;
            }
        }

        protected void ddlDIRNOKCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab9";
            if (ddlDIRNOKCountry.SelectedValue.Trim() == "NGA")
            {
                //ddlLGAofResidence.Items.Clear();
                this.ddlDIRNOKState.AppendDataBoundItems = true;
                String strQuery = "SELECT STATE_NAME,STATE_ID FROM CDMS.SRC_CDMA_STATE ORDER BY STATE_NAME ASC";
                OracleCommand objCmd = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strQuery;
                objCmd.Connection = con;
                try
                {
                    con.Open();
                    ddlDIRNOKState.DataSource = objCmd.ExecuteReader();
                    ddlDIRNOKState.DataTextField = "STATE_NAME";
                    ddlDIRNOKState.DataValueField = "STATE_NAME";
                    ddlDIRNOKState.DataBind();
                    ddlDIRNOKState.Items.Insert(0, new ListItem("--SELECT STATE--", ""));

                    //this.btnUpdateDQIParam.Visible = false;
                    //this.GridView1.Visible = false;
                    //this.GridView1.Visible = true;
                    this.txtDIRNOKState.Visible = false;
                    this.ddlDIRNOKState.Visible = true;

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
                this.txtDIRNOKState.Visible = true;
                this.ddlDIRNOKState.Visible = false;
            }
        }


        protected void ddlDIRNOKState_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab9";
            if (ddlDIRNOKCountry.SelectedValue.Trim() == "NGA")
            {
                ddlDIRNOKLGA.AppendDataBoundItems = true;
                // int StateOfRes = Convert.ToInt16(this.ddlDIRAddyState.SelectedItem.Value);
                String strQuery = "SELECT LGA_NAME FROM CDMS.SRC_CDMA_LGA ORDER BY LGA_NAME ASC";//WHERE STATE_ID =:P_STATE_ID";
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
                    ddlDIRNOKLGA.DataSource = objCmd.ExecuteReader();
                    ddlDIRNOKLGA.DataTextField = "LGA_NAME";
                    ddlDIRNOKLGA.DataValueField = "LGA_NAME";
                    ddlDIRNOKLGA.DataBind();
                    ddlDIRNOKLGA.Items.Insert(0, new ListItem("--SELECT LGA--", ""));

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
                this.txtDIRNOKLGA.Visible = true;
                this.ddlDIRNOKLGA.Visible = false;
            }
        }

        protected void ddlDIRIDTypeOfID_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab7";
            if (ddlDIRIDTypeOfID.SelectedValue == "OTHERS")
            {
                this.txtDIRIDTypeOfID.Visible = true;
                //this.ddlCustNOKIdType.Enabled = false;
            }
            else
            {
                this.txtDIRIDTypeOfID.Visible = false;
                this.txtDIRIDTypeOfID.Text = string.Empty;
            }


        }

        protected void ddlDIRAddyState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDIRAddyCountry.SelectedValue.Trim() == "NGA")
            {
                ddlDIRAddyLGA.AppendDataBoundItems = true;
                // int StateOfRes = Convert.ToInt16(this.ddlDIRAddyState.SelectedItem.Value);
                String strQuery = "SELECT LGA_NAME FROM CDMS.SRC_CDMA_LGA ORDER BY LGA_NAME ASC";//WHERE STATE_ID =:P_STATE_ID";
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
                    ddlDIRAddyLGA.DataSource = objCmd.ExecuteReader();
                    ddlDIRAddyLGA.DataTextField = "LGA_NAME";
                    ddlDIRAddyLGA.DataValueField = "LGA_NAME";
                    ddlDIRAddyLGA.DataBind();
                    ddlDIRAddyLGA.Items.Insert(0, new ListItem("--SELECT LGA--", ""));

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
                this.txtDIRAddyLGA.Visible = true;
                this.ddlDIRAddyLGA.Visible = false;
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

        //protected void OnSave_Director(object sender, EventArgs e)
        //{
        //    this.OnAddNew_Director(sender, e);
        //}


        protected void OnEdit_CompInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetCompanyInfo(recId, "CDMA_COMPANY_INFORMATION");

            txtCustomerNo.Text = f.CustomerNo;
            txtCopmanyname.Text = f.CompanyName;
            txtIncopDate.Text = Convert.ToDateTime(f.DateOfIncorpRegistration).ToString("dd/MM/yyyy");
            ddlCompanyType.Text = f.CustomerType;
            txtaddress.Text = f.RegisteredAddress;
            ddlCatOfBiz.SelectedValue = f.BizCategory;

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
        protected void OnEdit_AccountInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";
            LinkButton lnk = sender as LinkButton;

            try
            {
                string recId = lnk.Attributes["RecId"];

                customer = new CustomerRepository();
                var inc = customer.GetAccountInformation(recId, "CDMA_CORP_ACCT_SERV_REQUIRED");

                txtCustNoAcctInfo.Text = inc.CustomerNo;
                ddlAcctInfoAccttype.SelectedValue = inc.AccountType;
                ddlAcctInfoDomBranch.SelectedValue = inc.DomicileBranch;
                txtAcctInfoReferralCode.Text = inc.ReferralCode;
                txtAcctInfoAcctNo.Text = inc.AccountNo;
                txtAcctInfoAcctName.Text = inc.AccountName;
                rbtAcctInfoCrdPref.SelectedValue = inc.CardPreference;
                rbtAcctInfoEBankingPref.SelectedValue = inc.ElectronicBankingPrefer;
                rbtAcctInfoStatmntPref.Text = inc.StatementPreferences;
                rbtTranxAlertPref.SelectedValue = inc.TransactionAlertPreference;
                rbtAcctInfoStatmntFreq.SelectedValue = inc.StatementPreferences;
                rbtAcctInfoChequeConfmtnReq.Text = inc.ChequeConfirmation;
                rbtAcctInfoChequeConfmtn.SelectedValue = inc.ChequeConfirmation;
                rbtAcctInfoChequeConfmtnThreshold.SelectedValue = inc.ChequeConfirmThreshold;


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
        protected void OnEdit_CompDetail(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab2";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            try
            {
                var f = customer.GetCompanyDetails(recId, "CDMA_COMPANY_DETAILS");

                txtCustomerNoCompDetails.Text = f.CustomerNo.ToString();
                txtCompDRegNo.Text = f.CertOfIncorpRegNo;
                ddlCompDJurOfInc.SelectedValue = f.JurisdictionOfIncorpReg;
                txtCompDSCUMLNo.Text = f.ScumlNo;
                rbtCompDGender.SelectedValue = f.GenderControlling51Perc;
                ddlCompDSector.Text = f.SectorOrIndustry.ToString();
                ddlCompDOpBiz1.SelectedValue = f.OperatingBusiness1;
                txtCompDCity1.Text = f.City1.ToString();
                ddlCompDCountry1.Text = f.Country1;
                txtCompDZipCode1.Text = f.ZipCode1;
                txtCompDBizAddy1.Text = f.BizAddressRegOffice1;
                ddlCompDOpBiz1.SelectedValue = f.OperatingBusiness2;
                txtCompDCity1.Text = f.City2;
                ddlCompDCountry2.SelectedValue = f.Country2;
                txtCompDZipCode2.Text = f.ZipCode2;
                txtCompDBizAddy2.Text = f.BizAddressRegOffice2;
                txtCompDCompEmailAddy.Text = f.CompanyEmailAddress;
                txtCompDWebsite.Text = f.Website;
                txtCompDOfficeNo.Text = f.OfficeNumber.ToString();
                txtCompDMobineNo.Text = f.MobileNumber.ToString();
                txtCompDTIN.Text = f.Tin;
                txtCompDBorrwerCode.Text = f.CrmbNoBorrowerCode;
                txtCompDAnnTurnover.Text = f.ExpectedAnnualTurnover;
                rbtCompDOnStckExnge.SelectedValue = f.IsCompanyOnStockExch;
                ddlCompDStkExhange.Text = f.StockExchangeName;


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

        protected void OnEdit_DIRAddressInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab11";
            LinkButton lnk = sender as LinkButton;

            string[] arguments = lnk.CommandArgument.Split(';');
            string customer_no = arguments[0];
            string Mngt_no = arguments[1];
            //string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetCorpAddressInfo(customer_no.Trim(), Mngt_no.Trim(), "CDMA_MANAGEMENT_ADDRESS");

            txtDIRAddyCustNo.Text = f.CustomerNo;
            txtDIRAddyManID.Text = Convert.ToString(f.ManagementNo);
            txtDIRAddyHouseNo.Text = f.HomeNo;
            txtDIRAddyStreetName.Text = f.StreetName;
            txtDIRAddyBStop.Text = f.NearestBstop;
            ddlDIRAddyLGA.SelectedValue = f.LGA.ToUpper();
            txtDIRAddyCity.Text = f.City;
            ddlDIRAddyState.SelectedValue = f.State.ToUpper();
            ddlDIRAddyCountry.SelectedValue = f.Country.ToUpper();
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

        protected void btnSave_DIRAddressInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab11";

            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_management_address";


            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtDIRAddyCustNo.Text;
            objCmd.Parameters.Add("p_management_id", OracleDbType.Varchar2).Value = txtDIRAddyManID.Text;
            objCmd.Parameters.Add("p_house_number", OracleDbType.Varchar2).Value = txtDIRAddyHouseNo.Text;
            objCmd.Parameters.Add("p_street_name", OracleDbType.Varchar2).Value = txtDIRAddyStreetName.Text;
            objCmd.Parameters.Add("p_nearest_bus_stop_landmark", OracleDbType.Varchar2).Value = txtDIRAddyBStop.Text;
            objCmd.Parameters.Add("p_lga_residential", OracleDbType.Varchar2).Value = this.ddlDIRAddyCountry.SelectedValue == "NGR" ? ddlDIRAddyLGA.SelectedValue : txtDIRAddyLGA.Text;
            objCmd.Parameters.Add("p_city_or_town", OracleDbType.Varchar2).Value = txtDIRAddyCity.Text;
            objCmd.Parameters.Add("p_state", OracleDbType.Varchar2).Value = this.ddlDIRAddyCountry.SelectedValue == "NGR" ? ddlDIRAddyState.SelectedValue : txtDIRAddyState.Text;
            objCmd.Parameters.Add("p_country", OracleDbType.Varchar2).Value = ddlDIRAddyCountry.SelectedValue;
            objCmd.Parameters.Add("p_office_number", OracleDbType.Varchar2).Value = txtDIRAddyOfficeNo.Text;
            objCmd.Parameters.Add("p_mobile_number", OracleDbType.Varchar2).Value = txtDIRAddyMobileNo.Text;
            objCmd.Parameters.Add("p_email_address", OracleDbType.Varchar2).Value = txtDIRAddyEmailAddy.Text;


            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";
            //
            if (txtDIRAddyCustNo.Text == string.Empty)
            {
                lblDIRAddress.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }

            getCustID rst = new getCustID();

            try
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(txtDIRAddyCustNo.Text) == 0)
                {
                    lblDIRAddress.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");


                    return;

                }
                int rstCmd = objCmd.ExecuteNonQuery();

                if (rstCmd == -1)
                {

                    lblDIRAddress.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Record Added Successful");

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));

                    txtDIRAddyCustNo.Text = txtDIRAddyManID.Text = txtDIRAddyHouseNo.Text = txtDIRAddyStreetName.Text = txtDIRAddyBStop.Text = ddlDIRAddyLGA.SelectedValue = txtDIRAddyCity.Text = ddlDIRAddyState.SelectedValue = ddlDIRAddyCountry.SelectedValue =
                                       txtDIRAddyOfficeNo.Text = txtDIRAddyMobileNo.Text = txtDIRAddyEmailAddy.Text = "";

                    GridView11.DataBind();
                }
                else
                {

                    lblDIRAddress.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

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
            }
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
                var f = customer.GetCorpBiodataInfo(customer_no.Trim(), Mngt_no.Trim(), "CDMA_MANAGEMENT_BIO_DATA");

                txtDIRCustNo.Text = f.CustomerNo;
                //populate all other manager tables with a specific managementID
                txtDIRMngtNo.Text = txtDIRFMngtID.Text = txtDIRDIManID.Text = txtDIRNOKMngtID.Text = txtDIRAddyManID.Text = Convert.ToString(f.ManagementNo);
                ddlDIRType.SelectedValue = f.ManagementType;
                ddlDIRTitle.SelectedValue = f.Title;
                ddlDIRMStatus.SelectedValue = f.MaritalStatus;
                txtDIRSurname.Text = f.Surname;
                txtDIRFirstName.Text = f.FirstName;
                txtDIROtherName.Text = f.Othernames;
                txtDIRDOB.Text = f.DOB;
                txtDIRPlaceOfBirth.Text = f.POB;
                ddlDIRSex.SelectedValue = f.Sex;
                ddlDIRNationality.SelectedValue = f.Nationality;
                txtDIRMMName.Text = f.MothersMaidenName;
                txtDIROccupation.Text = f.Occupation;
                txtDIRJobTitle.Text = f.JobTitle;
                ddlDIRClassOfSign.SelectedValue = f.ClassOfSignitory;

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
                lblDIRMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }
        }

        protected void btnSave_DIRBioInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab6";

            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_management_bio_data";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtDIRCustNo.Text;
            objCmd.Parameters.Add("p_management_id", OracleDbType.Varchar2).Value = txtDIRMngtNo.Text;
            objCmd.Parameters.Add("p_management_type", OracleDbType.Varchar2).Value = ddlDIRType.SelectedValue;
            objCmd.Parameters.Add("p_title", OracleDbType.Varchar2).Value = ddlDIRTitle.SelectedValue;
            objCmd.Parameters.Add("p_marital_status", OracleDbType.Varchar2).Value = ddlDIRMStatus.SelectedValue;
            objCmd.Parameters.Add("p_surname", OracleDbType.Varchar2).Value = txtDIRSurname.Text;
            objCmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = txtDIRFirstName.Text;
            objCmd.Parameters.Add("p_other_names", OracleDbType.Varchar2).Value = txtDIROtherName.Text;
            objCmd.Parameters.Add("p_date_of_birth", OracleDbType.Date).Value = txtDIRDOB.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtDIRDOB.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat); //Convert.ToDateTime(txtDIRDOB.Text);
            objCmd.Parameters.Add("p_place_of_birth", OracleDbType.Varchar2).Value = txtDIRPlaceOfBirth.Text;
            objCmd.Parameters.Add("p_sex", OracleDbType.Varchar2).Value = ddlDIRSex.SelectedValue;
            objCmd.Parameters.Add("p_nationality", OracleDbType.Varchar2).Value = ddlDIRNationality.SelectedValue;
            objCmd.Parameters.Add("p_mothers_maiden_name", OracleDbType.Varchar2).Value = txtDIRMMName.Text;
            objCmd.Parameters.Add("p_occupation", OracleDbType.Varchar2).Value = txtDIROccupation.Text;
            objCmd.Parameters.Add("p_status_or_job_title", OracleDbType.Varchar2).Value = txtDIRJobTitle.Text;
            objCmd.Parameters.Add("p_class_of_signatory", OracleDbType.Varchar2).Value = ddlDIRClassOfSign.SelectedValue;

            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";
            //
            if (txtDIRCustNo.Text == string.Empty)
            {
                lblDIRMsg.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }
            if (txtDIRFirstName.Text == string.Empty)
            {
                lblDIRMsg.Text = MessageFormatter.GetFormattedErrorMessage("Customer First  Name Required");

                return;
            }

            getCustID rst = new getCustID();

            try
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(txtDIRCustNo.Text) == 0)
                {
                    lblDIRMsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");

                    return;

                }
                int rstCmd = objCmd.ExecuteNonQuery();

                if (rstCmd == -1)
                {

                    lblDIRMsg.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Record Added Successful");

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));

                    txtDIRCustNo.Text = txtDIRMngtNo.Text = ddlDIRType.SelectedValue = ddlDIRTitle.SelectedValue = ddlDIRMStatus.SelectedValue = txtDIRSurname.Text = txtDIRFirstName.Text = txtDIROtherName.Text = txtDIRDOB.Text = txtDIRPlaceOfBirth.Text = ddlDIRSex.SelectedValue = ddlDIRNationality.SelectedValue = txtDIRMMName.Text = txtDIROccupation.Text = txtDIRJobTitle.Text = ddlDIRClassOfSign.SelectedValue = "";

                    GridView6.DataBind();
                }
                else
                {

                    lblDIRMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

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
            }
        }

        protected void OnEdit_CompAddInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab10";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetCorpAdditionalInfo(recId, "CDMA_CORP_ADDITIONAL_DETAILS");

            txtAddInfoCustID.Text = f.CustomerNo;
            txtAddInfoAffltdCompName.Text = f.AffliliatedCompBody;
            ddlAddInfoCountry.Text = f.ParentCompanyIncCountry;


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

        protected void btnSave_CompAddInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab10";

            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_corp_additional_details";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtAddInfoCustID.Text;
            objCmd.Parameters.Add("p_affiliate_company_body", OracleDbType.Varchar2).Value = txtAddInfoAffltdCompName.Text;
            objCmd.Parameters.Add("p_parent_company_ctry_incorp", OracleDbType.Varchar2).Value = ddlAddInfoCountry.Text;

            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";
            //
            if (txtAddInfoCustID.Text == string.Empty)
            {
                lblCompAddInfoMsg.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }

            getCustID rst = new getCustID();

            try
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(txtAddInfoCustID.Text) == 0)
                {
                    lblCompAddInfoMsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");


                    return;

                }
                int rstCmd = objCmd.ExecuteNonQuery();

                if (rstCmd == -1)
                {

                    lblCompAddInfoMsg.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Record Added Successful");

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));

                    txtAddInfoCustID.Text = txtAddInfoAffltdCompName.Text = ddlAddInfoCountry.Text = "";

                    GridView10.DataBind();
                }
                else
                {

                    lblCompAddInfoMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

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
            }
        }

        protected void OnEdit_DIRIDInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab7";
            LinkButton lnk = sender as LinkButton;

            string[] arguments = lnk.CommandArgument.Split(';');
            string customer_no = arguments[0];
            string Mngt_no = arguments[1];

            //string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetCorpIDInfo(customer_no.Trim(), Mngt_no.Trim(), "CDMA_MANAGEMENT_IDENTIFIER");

            txtDIRDICustNo.Text = f.CustomerNo;
            txtDIRDIManID.Text = Convert.ToString(f.ManagementNo);
            ddlDIRIDTypeOfID.Text = f.TypeOfID;
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

        protected void btnSave_DIRIDInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab7";

            OracleCommand objCmd = new OracleCommand();
            if (this.txtDIRIDIssueDate.Text != string.Empty || this.txtDIRIDExpiryDate.Text != string.Empty)
            {
             DateTime IssueDate = DateTime.ParseExact(this.txtDIRIDIssueDate.Text, "dd/MM/yyyy", null);
            DateTime ExpiryDate = DateTime.ParseExact(this.txtDIRIDExpiryDate.Text, "dd/MM/yyyy", null);
            if (mp.checkDateDiff(Convert.ToDateTime(IssueDate, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat), Convert.ToDateTime(ExpiryDate, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat)) == false)
            {

                lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Incorrect: Issue date can not be ahead of Expiry date.");
                return;
            }
            }

            objCmd.Connection = con;
            objCmd.CommandText = "pkg_cdms_maker.prc_management_identifier";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtDIRDICustNo.Text;
            objCmd.Parameters.Add("p_management_id", OracleDbType.Varchar2).Value = txtDIRDIManID.Text;
            objCmd.Parameters.Add("p_type_of_identification", OracleDbType.Varchar2).Value = ddlDIRIDTypeOfID.SelectedValue;
            objCmd.Parameters.Add("p_id_no", OracleDbType.Varchar2).Value = txtDIRIDNo.Text;
            objCmd.Parameters.Add("p_id_issue_date", OracleDbType.Date).Value = txtDIRIDIssueDate.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtDIRIDIssueDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            objCmd.Parameters.Add("p_id_expiry_date", OracleDbType.Date).Value = txtDIRIDExpiryDate.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtDIRIDExpiryDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            objCmd.Parameters.Add("p_bvn_id", OracleDbType.Varchar2).Value = txtDIRIDBVNID.Text;
            objCmd.Parameters.Add("p_tin", OracleDbType.Varchar2).Value = txtDIRIDTIN.Text;

            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";
            //
            if (txtDIRDICustNo.Text == string.Empty)
            {
                lblDIRIDMsg.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }

            getCustID rst = new getCustID();

            try
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(txtDIRDICustNo.Text) == 0)
                {
                    lblDIRIDMsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");
                    return;

                }
                int rstCmd = objCmd.ExecuteNonQuery();

                if (rstCmd == -1)
                {

                    lblDIRIDMsg.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Record Added Successful");

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));

                    txtDIRDICustNo.Text = txtDIRDIManID.Text = ddlDIRIDTypeOfID.SelectedValue = txtDIRIDNo.Text = txtDIRIDIssueDate.Text = txtDIRIDExpiryDate.Text = txtDIRIDBVNID.Text = txtDIRIDTIN.Text = "";

                    GridView7.DataBind();
                }
                else
                {

                    lblDIRIDMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: Operation failed");

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
            }
        }

        protected void OnEdit_AWOB(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab5";
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetCorpAWOBInfo(recId, "CDMA_ACCT_HELD_WITH_OTHER_BANK");

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

        protected void btnSave_AWOBInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab5";

            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_acct_held_with_other_bank";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtAWOBCustNo.Text;
            objCmd.Parameters.Add("p_have_other_bank_acct", OracleDbType.Varchar2).Value = rbtAWOB.SelectedValue;
            objCmd.Parameters.Add("p_bank_name", OracleDbType.Varchar2).Value = txtAWOBBankName.Text;
            objCmd.Parameters.Add("p_bank_address_or_branch", OracleDbType.Varchar2).Value = txtAWOBBankAddy.Text;
            objCmd.Parameters.Add("p_account_name", OracleDbType.Varchar2).Value = txtAWOBAcctName.Text;
            objCmd.Parameters.Add("p_account_number", OracleDbType.Varchar2).Value = txtAWOBAcctNo.Text;
            objCmd.Parameters.Add("p_status", OracleDbType.Varchar2).Value = txtAWOBStatus.Text;

            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";
            //
            if (txtAWOBCustNo.Text == string.Empty)
            {
                lblAWOBmsg.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }

            getCustID rst = new getCustID();

            try
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(txtAWOBCustNo.Text) == 0)
                {
                    lblAWOBmsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");


                    return;

                }
                int rstCmd = objCmd.ExecuteNonQuery();

                if (rstCmd == -1)
                {

                    lblAWOBmsg.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Record Added Successful");

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));

                    // txtAWOBCustNo.Text = 
                    txtAWOBAcctNo.Text = txtAWOBAcctName.Text = txtAWOBBankName.Text = txtAWOBBankAddy.Text = txtAWOBStatus.Text = "";

                    GridView5.DataBind();
                }
                else
                {

                    lblAWOBmsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

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
            }
        }

        protected void OnEdit_ForeignerInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab8";
            LinkButton lnk = sender as LinkButton;

            string[] arguments = lnk.CommandArgument.Split(';');
            string customer_no = arguments[0];
            string Mngt_no = arguments[1];
            //string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetCorpForeignerInfo(customer_no.Trim(), Mngt_no.Trim(), "CDMA_MANAGEMENT_FOREIGNER");

            txtDIRForeginCustNo.Text = f.CustomerNo;
            txtDIRFMngtID.Text = Convert.ToString(f.ManagementNo);
            rbtDIRForeigner.Text = f.isForeigner;
            txtDIRFPermitNo.Text = f.ResidentPermitNo;
            ddlDIRFNationality.SelectedValue = f.Nationality;
            txtDIRFPIssueDate.Text = Convert.ToString(f.PermitIssueDate);
            txtDIRFPExpiryDate.Text = Convert.ToString(f.PermitExpiryDate);
            txtDIRMFTelephoneNo.Text = f.ForeignTelNo;
            txtDIRMResPermitNo.Text = f.PassportResidentPermitNo;
            txtDIRMForeignAddy.Text = f.ForeignAddy;
            txtDIRMCity.Text = f.City;
            ddlDIRMCountry.SelectedValue = f.Country;
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

        protected void btnSave_ForeignerInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab8";

            if (this.txtDIRFPIssueDate.Text != string.Empty || this.txtDIRFPExpiryDate.Text != string.Empty)
            {

                DateTime IssueDate = DateTime.ParseExact(this.txtDIRFPIssueDate.Text, "dd/MM/yyyy", null);
                DateTime ExpiryDate = DateTime.ParseExact(this.txtDIRFPExpiryDate.Text, "dd/MM/yyyy", null);
                if (mp.checkDateDiff(Convert.ToDateTime(IssueDate, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat), Convert.ToDateTime(ExpiryDate, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat)) == false)
                {

                    lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Incorrect: Issue date can not be ahead of Expiry date.");
                    return;

                }
            }
            OracleCommand objCmd = new OracleCommand();


            objCmd.Connection = con;
            objCmd.CommandText = "pkg_cdms_maker.prc_management_foreigner";


            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtDIRForeginCustNo.Text;
            objCmd.Parameters.Add("p_management_id", OracleDbType.Varchar2).Value = txtDIRFMngtID.Text;
            objCmd.Parameters.Add("p_foreigner", OracleDbType.Varchar2).Value = rbtDIRForeigner.SelectedValue;
            objCmd.Parameters.Add("p_multiple_citizenship", OracleDbType.Varchar2).Value = rbtDIRMultipleCitizenship.SelectedValue;
            objCmd.Parameters.Add("p_residence_permit_number", OracleDbType.Varchar2).Value = ddlDIRFNationality.SelectedValue;
            objCmd.Parameters.Add("p_permit_issue_date", OracleDbType.Date).Value = txtDIRFPIssueDate.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtDIRFPIssueDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            objCmd.Parameters.Add("p_permit_expiry_date", OracleDbType.Date).Value = txtDIRFPExpiryDate.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtDIRFPExpiryDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            objCmd.Parameters.Add("p_foreign_tel_number", OracleDbType.Varchar2).Value = txtDIRMFTelephoneNo.Text;
            objCmd.Parameters.Add("p_passport_resident_permit_no", OracleDbType.Varchar2).Value = txtDIRMResPermitNo.Text;
            objCmd.Parameters.Add("p_foreign_address", OracleDbType.Varchar2).Value = txtDIRMForeignAddy.Text;
            objCmd.Parameters.Add("p_city", OracleDbType.Varchar2).Value = txtDIRMCity.Text;
            objCmd.Parameters.Add("p_country", OracleDbType.Varchar2).Value = ddlDIRMCountry.SelectedValue;
            objCmd.Parameters.Add("p_status", OracleDbType.Varchar2).Value = txtDIRMZipCode.Text;

            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";
            //
            if (txtDIRForeginCustNo.Text == string.Empty)
            {
                lblDIRForeignerMsg.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }

            getCustID rst = new getCustID();

            try
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(txtDIRForeginCustNo.Text) == 0)
                {
                    lblDIRForeignerMsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");
                    return;

                }
                int rstCmd = objCmd.ExecuteNonQuery();

                if (rstCmd == -1)
                {

                    lblDIRForeignerMsg.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Record Added Successful");

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));

                    txtDIRForeginCustNo.Text = txtDIRFMngtID.Text = rbtDIRForeigner.SelectedValue = rbtDIRMultipleCitizenship.SelectedValue = ddlDIRFNationality.SelectedValue = txtDIRFPIssueDate.Text = txtDIRFPExpiryDate.Text = txtDIRMFTelephoneNo.Text = txtDIRMResPermitNo.Text = txtDIRMForeignAddy.Text = txtDIRMCity.Text = ddlDIRMCountry.Text = txtDIRMZipCode.Text = "";

                    GridView8.DataBind();
                }
                else
                {

                    lblDIRForeignerMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("SelectedValue which is invalid"))
                {
                    this.lblDIRForeignerMsg.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Record Added Successfully");

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));
                }
                else
                {
                    this.lblDIRForeignerMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
                }
                

            }
            finally
            {
                objCmd = null;
                con.Close();
            }
        }

        protected void OnEdit_DIRNOKInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab9";
            LinkButton lnk = sender as LinkButton;

            string[] arguments = lnk.CommandArgument.Split(';');
            string customer_no = arguments[0];
            string Mngt_no = arguments[1];

            //string recId = lnk.Attributes["RecId"];
            customer = new CustomerRepository();
            var f = customer.GetCorpNOKInfo(customer_no.Trim(), Mngt_no.Trim(), "CDMA_MANAGEMENT_NEXT_OF_KIN");

            txtDIRNOKCustNo.Text = f.CustomerNo;
            txtDIRNOKMngtID.Text = Convert.ToString(f.ManagementNo);
            ddlDIRNOKTitle.SelectedValue = f.Title;
            txtDIRNOKSurame.Text = f.Surname;
            txtDIRNOKFirstName.Text = f.FirstName;
            txtDIRNOKOtherName.Text = Convert.ToString(f.Othernames);
            txtDIRNOKDOB.Text = Convert.ToString(f.DOB);
            ddlDIRNOKSex.SelectedValue = f.Sex;
            ddlDIRNOKRelationship.SelectedValue = f.Relationship;
            txtDIRNOKOfficeNo.Text = f.OfficeNo;
            txtDIRNOKMobileNo.Text = f.MobileNo;
            txtDIRNOKEmailAddy.Text = f.Email;
            txtDIRNOKHouseNo.Text = f.HouseNo;
            txtDIRNOKStrName.Text = f.StreetName;
            txtDIRNOKBusStop.Text = f.NearestBStop;
            txtDIRNOKCity.Text = f.City;
            ddlDIRNOKLGA.Text = f.LGA;
            txtDIRNOKZip.Text = f.ZipPostalCode;
            ddlDIRNOKState.SelectedValue = f.State;
            ddlDIRNOKCountry.SelectedValue = f.Country;


            getCustID rst = new getCustID();
            //

            if (con.State == ConnectionState.Closed)
                con.Open();//

            //
            if (rst.get_corp_customer_id(this.txtDIRNOKCustNo.Text) > 0)
            {

                lblDIRNOKmsg.Text = f.ErrorMessage == string.Empty ? MessageFormatter.GetFormattedSuccessMessage("Record Loaded") : MessageFormatter.GetFormattedErrorMessage(f.ErrorMessage);
            }
        }

        protected void btnSave_DIRNOKInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab9";

            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_management_next_of_kin";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtDIRNOKCustNo.Text;
            objCmd.Parameters.Add("p_management_id", OracleDbType.Varchar2).Value = txtDIRNOKMngtID.Text;
            objCmd.Parameters.Add("p_title", OracleDbType.Varchar2).Value = ddlDIRNOKTitle.SelectedValue;
            objCmd.Parameters.Add("p_surname", OracleDbType.Varchar2).Value = txtDIRNOKSurame.Text;
            objCmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = txtDIRNOKFirstName.Text;
            objCmd.Parameters.Add("p_other_name", OracleDbType.Varchar2).Value = txtDIRNOKOtherName.Text;
            objCmd.Parameters.Add("p_date_of_birth", OracleDbType.Date).Value = txtDIRNOKDOB.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtDIRNOKDOB.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);//= txtDIRNOKDOB.Text;
            objCmd.Parameters.Add("p_sex", OracleDbType.Varchar2).Value = ddlDIRNOKSex.SelectedValue;
            objCmd.Parameters.Add("p_relationship", OracleDbType.Varchar2).Value = ddlDIRNOKRelationship.SelectedValue;
            objCmd.Parameters.Add("p_office_no", OracleDbType.Varchar2).Value = txtDIRNOKOfficeNo.Text;
            objCmd.Parameters.Add("p_mobile_no", OracleDbType.Varchar2).Value = txtDIRNOKMobileNo.Text;
            objCmd.Parameters.Add("p_email_address", OracleDbType.Varchar2).Value = txtDIRNOKEmailAddy.Text;
            objCmd.Parameters.Add("p_house_number", OracleDbType.Varchar2).Value = txtDIRNOKHouseNo.Text;
            objCmd.Parameters.Add("p_street_name", OracleDbType.Varchar2).Value = txtDIRNOKStrName.Text;
            objCmd.Parameters.Add("p_nearest_bus_stop_landmark", OracleDbType.Varchar2).Value = txtDIRNOKBusStop.Text;
            objCmd.Parameters.Add("p_city_town", OracleDbType.Varchar2).Value = txtDIRNOKCity.Text;
            objCmd.Parameters.Add("p_lga", OracleDbType.Varchar2).Value = this.ddlDIRNOKCountry.SelectedValue == "NGR" ? this.ddlDIRNOKLGA.SelectedValue : this.txtDIRNOKLGA.Text;
            objCmd.Parameters.Add("p_zip_postal_code", OracleDbType.Varchar2).Value = txtDIRNOKZip.Text;
            objCmd.Parameters.Add("p_state", OracleDbType.Varchar2).Value = this.ddlDIRNOKCountry.SelectedValue == "NGR" ? this.ddlDIRNOKState.SelectedValue : this.txtDIRNOKState.Text;

            objCmd.Parameters.Add("p_country", OracleDbType.Varchar2).Value = ddlDIRNOKCountry.SelectedValue;

            //
            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";
            //
            if (txtDIRNOKCustNo.Text == string.Empty)
            {
                lblDIRNOKmsg.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }

            getCustID rst = new getCustID();

            try
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(txtDIRNOKCustNo.Text) == 0)
                {
                    lblDIRNOKmsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");


                    return;

                }
                int rstCmd = objCmd.ExecuteNonQuery();

                if (rstCmd == -1)
                {

                    lblDIRNOKmsg.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Record Added Successful");

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));

                    txtDIRNOKCustNo.Text = txtDIRNOKMngtID.Text = ddlDIRNOKTitle.SelectedValue = txtDIRNOKSurame.Text = txtDIRNOKFirstName.Text = txtDIRNOKOtherName.Text = txtDIRNOKDOB.Text = ddlDIRNOKSex.SelectedValue = ddlDIRNOKRelationship.SelectedValue = txtDIRNOKOfficeNo.Text = txtDIRNOKMobileNo.Text = txtDIRNOKEmailAddy.Text = txtDIRNOKHouseNo.Text = txtDIRNOKStrName.Text = txtDIRNOKBusStop.Text = txtDIRNOKCity.Text = ddlDIRNOKLGA.SelectedValue = txtDIRNOKZip.Text = ddlDIRNOKCountry.SelectedValue = "";

                    GridView9.DataBind();
                }
                else
                {

                    lblDIRNOKmsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

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
            }
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
            var f = customer.GetCorpPOAInfo(customer_no.Trim(), account_no.Trim(),"CDMA_POWER_OF_ATTORNEY");

            txtPOACustNo.Text = f.CustomerNo;
            txtPOAAccountNo.Text = f.AccountNo;
            txtPOAHolderName.Text = f.HoldersNAme;
            txtPOAAddy.Text = f.Address;
            ddlPOACountry.Text = f.Country;
            ddlPOANationality.Text = f.Nationality;
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

        protected void btnSave_POAInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab4";

            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_power_of_attorney";


            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtPOACustNo.Text;
            objCmd.Parameters.Add("p_account_number", OracleDbType.Varchar2).Value = txtPOAAccountNo.Text;
            objCmd.Parameters.Add("p_attorney_y_n", OracleDbType.Varchar2).Value = this.rblPOAttorney.SelectedValue;
            objCmd.Parameters.Add("p_holder_name", OracleDbType.Varchar2).Value = txtPOAHolderName.Text;
            objCmd.Parameters.Add("p_address", OracleDbType.Varchar2).Value = txtPOAAddy.Text;
            objCmd.Parameters.Add("p_country", OracleDbType.Varchar2).Value = ddlPOACountry.Text;
            objCmd.Parameters.Add("p_nationality", OracleDbType.Varchar2).Value = ddlPOANationality.Text;
            objCmd.Parameters.Add("p_telephone_number", OracleDbType.Varchar2).Value = txtPOAPhoneNo.Text;

            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";
            //
            if (txtPOACustNo.Text == string.Empty)
            {
                lblPOAMsg.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }

            getCustID rst = new getCustID();

            try
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(txtPOACustNo.Text) == 0)
                {
                    lblPOAMsg.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");


                    return;

                }
                int rstCmd = objCmd.ExecuteNonQuery();

                if (rstCmd == -1)
                {

                    lblPOAMsg.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Record Added Successful");

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));

                    txtPOACustNo.Text =txtPOAAccountNo.Text = txtPOAHolderName.Text = txtPOAAddy.Text = ddlPOACountry.SelectedValue = ddlPOANationality.SelectedValue = txtPOAPhoneNo.Text = "";

                    GridView2.DataBind();
                }
                else
                {

                    lblPOAMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

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
            }
        }

        protected void btnSave_CompDetail(object sender, EventArgs e)
        {
            //OracleConnection con = new OracleConnection(new Connection().ConnectionString);
            hidTAB.Value = "#tab2";
            con.Open();

            try
            {

                OracleCommand cmd = new OracleCommand();
                // Set the command text on an OracleCommand object
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;
                cmd.CommandText = "pkg_cdms_maker.prc_company_details";//

                OracleParameter prm = new OracleParameter();
                cmd.Parameters.Add("p_customer_no", OracleDbType.NVarchar2).Value = this.txtCustomerNoCompDetails.Text;

                cmd.Parameters.Add("p_cert_of_incorp_reg_no", OracleDbType.NVarchar2).Value = txtCompDRegNo.Text;

                cmd.Parameters.Add("p_jurisdiction_of_incorp_reg", OracleDbType.NVarchar2).Value = ddlCompDJurOfInc.SelectedValue;
                cmd.Parameters.Add("p_scuml_no", OracleDbType.NVarchar2).Value = txtCompDSCUMLNo.Text;
                cmd.Parameters.Add("p_gender_controlling_51_perc", OracleDbType.NVarchar2).Value = rbtCompDGender.SelectedValue;
                cmd.Parameters.Add("p_sector_or_industry", OracleDbType.NVarchar2).Value = ddlCompDSector.SelectedValue;

                cmd.Parameters.Add("p_operating_business_1", OracleDbType.NVarchar2).Value = ddlCompDOpBiz1.SelectedValue;
                cmd.Parameters.Add("p_city_1", OracleDbType.NVarchar2).Value = txtCompDCity1.Text;
                cmd.Parameters.Add("p_country_1", OracleDbType.NVarchar2).Value = ddlCompDCountry1.SelectedValue;
                cmd.Parameters.Add("p_zip_code_1", OracleDbType.NVarchar2).Value = txtCompDZipCode1.Text;
                cmd.Parameters.Add("p_biz_address_reg_office_1", OracleDbType.NVarchar2).Value = txtCompDBizAddy1.Text;

                cmd.Parameters.Add("p_operating_business_2", OracleDbType.NVarchar2).Value = ddlCompDOpBiz2.SelectedValue;
                cmd.Parameters.Add("p_city_2", OracleDbType.NVarchar2).Value = txtCompDCity2.Text;
                cmd.Parameters.Add("p_country_2", OracleDbType.NVarchar2).Value = ddlCompDCountry2.SelectedValue;
                cmd.Parameters.Add("p_zip_code_2", OracleDbType.NVarchar2).Value = txtCompDZipCode2.Text;
                cmd.Parameters.Add("p_biz_address_reg_office_2", OracleDbType.NVarchar2).Value = txtCompDBizAddy2.Text;

                cmd.Parameters.Add("p_company_email_address", OracleDbType.NVarchar2).Value = txtCompDCompEmailAddy.Text;
                cmd.Parameters.Add("p_website", OracleDbType.NVarchar2).Value = txtCompDWebsite.Text;
                cmd.Parameters.Add("p_office_number", OracleDbType.NVarchar2).Value = txtCompDOfficeNo.Text;
                cmd.Parameters.Add("p_mobile_number", OracleDbType.NVarchar2).Value = txtCompDMobineNo.Text;
                cmd.Parameters.Add("p_tin", OracleDbType.NVarchar2).Value = txtCompDTIN.Text;
                cmd.Parameters.Add("p_crmb_no_borrower_code", OracleDbType.NVarchar2).Value = txtCompDBorrwerCode.Text;
                cmd.Parameters.Add("p_expected_annual_turnover", OracleDbType.NVarchar2).Value = txtCompDAnnTurnover.Text;
                cmd.Parameters.Add("p_is_company_on_stock_exch", OracleDbType.NVarchar2).Value = rbtCompDOnStckExnge.SelectedValue;
                cmd.Parameters.Add("p_stock_exchange_name", OracleDbType.NVarchar2).Value = ddlCompDStkExhange.SelectedValue;


                cmd.Parameters.Add("p_last_modified_by", OracleDbType.NVarchar2).Value = (String)(Session["UserID"]);
                cmd.Parameters.Add("p_authorized", OracleDbType.NVarchar2).Value = "N";
                cmd.Parameters.Add("p_ip_address", OracleDbType.NVarchar2).Value = audit.IPAddress;
                cmd.Parameters.Add("p_role", OracleDbType.NVarchar2).Value = "MAKER";

                // Execute the command
                getCustID rst1 = new getCustID();

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst1.get_corp_customer_id(txtCustomerNoCompDetails.Text) == 0)
                {
                    lblCompDetails.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");


                    return;

                }

                var rst = cmd.ExecuteNonQuery();
                if (rst == -1)
                {

                    this.lblCompDetails.Text = MessageFormatter.GetFormattedSuccessMessage("Company Details Successfully Updated ");

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));
                    //
                    txtCompDRegNo.Text = ddlCompDJurOfInc.SelectedValue = txtCompDSCUMLNo.Text = rbtCompDGender.Text = ddlCompDSector.SelectedValue = ddlCompDOpBiz1.SelectedValue = ddlCompDCountry1.SelectedValue = txtCompDCity1.Text = "";
                    txtCompDZipCode1.Text = ddlCompDOpBiz2.SelectedValue = ddlCompDCountry2.SelectedValue = txtCompDBizAddy1.Text = txtCompDCity2.Text = "";
                    txtCompDZipCode2.Text = txtCompDBizAddy2.Text = "";
                    txtCompDCompEmailAddy.Text = txtCompDWebsite.Text = rbtCompDOnStckExnge.SelectedValue = txtCompDOfficeNo.Text = txtCompDMobineNo.Text = txtCompDTIN.Text = ddlCompDStkExhange.SelectedValue = "";

                }
                else
                {
                    lblCompDetails.Text = MessageFormatter.GetFormattedErrorMessage("Error: Director Information Not Successfully Updated ");
                }
            }
            catch (Exception ex)
            {
                lblCompDetails.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }

            // Close and Dispose OracleConnection object
            con.Close();
            con.Dispose();

            //bind data to gridview
            GridView1.DataBind();
            GridView4.DataBind();
            //end the creation of audit profile
        }
        protected void btnSave_CompInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";


            if (txtCustomerNo.Text == string.Empty)
            {
                lblmsg_CompanyInfo.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }

            con.Open();
            OracleCommand cmd = new OracleCommand();

            //cmd.Connection = con;

            cmd.BindByName = true;
            try
            {
                cmd.CommandText = "pkg_cdms_maker.prc_company_information";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                OracleParameter prm = new OracleParameter();
                cmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = this.txtCustomerNo.Text;
                cmd.Parameters.Add("p_company_name", OracleDbType.Varchar2).Value = txtCopmanyname.Text;
                cmd.Parameters.Add("p_date_of_incorp_registration", OracleDbType.Date).Value = txtIncopDate.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtIncopDate.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                cmd.Parameters.Add("p_customer_type", OracleDbType.Varchar2).Value = ddlCompanyType.SelectedValue;
                cmd.Parameters.Add("p_registered_address", OracleDbType.Varchar2).Value = txtaddress.Text;
                cmd.Parameters.Add("p_category_of_business", OracleDbType.Varchar2).Value = ddlCatOfBiz.SelectedValue;

                cmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = (String)(Session["UserID"]);
                cmd.Parameters.Add("p_authorized", OracleDbType.Varchar2).Value = "N";
                cmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
                cmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";

                getCustID rst = new getCustID();

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(txtCustomerNo.Text) == 0)
                {
                    lblmsg_CompanyInfo.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");


                    return;

                }


                cmd.ExecuteNonQuery();

                lblmsg_CompanyInfo.Text = MessageFormatter.GetFormattedSuccessMessage(txtCustomerNo.Text + "record updated successfully");
                // Close and Dispose OracleConnection object

                EmailHelper Emailer = new EmailHelper();
                Emailer.NotificationMailSender((String)(Session["UserID"]));

                con.Close();
                con.Dispose();

                txtCustomerNo.Text = txtCopmanyname.Text = txtIncopDate.Text = ddlCompanyType.Text = txtaddress.Text = ddlCatOfBiz.SelectedValue = "";
            }
            catch (Exception ex)
            {
                lblmsg_CompanyInfo.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }


        }

        protected void btnSearchCompInfo_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            try
            {

                using (OracleConnection conn = new OracleConnection(connString))
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {

                        cmd.Connection = conn;
                        cmd.CommandText = "select * from CDMA_COMPANY_INFORMATION where CUSTOMER_NO=:customerNo";
                        OracleParameter param = new OracleParameter("customerNo", OracleDbType.Varchar2);
                        param.Direction = ParameterDirection.Input;
                        param.Value = txtCustomerNo.Text.Trim();


                        cmd.Parameters.Add(param);
                        OracleDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            txtCustomerNo.Text = Convert.ToString(rdr[0]);
                            txtCopmanyname.Text = Convert.ToString(rdr[1]);
                            txtIncopDate.Text = Convert.ToDateTime(rdr[2]).ToString("dd/MM/yyyy");
                            ddlCompanyType.SelectedValue = Convert.ToString(rdr[3]);
                            txtaddress.Text = Convert.ToString(rdr[4]);

                            ddlCatOfBiz.SelectedValue = Convert.ToString(rdr[5]);


                        }


                        rdr.Dispose();
                        cmd.Dispose();
                    }



                    conn.Dispose();
                }


                //
                getCustID rst = new getCustID();
                //

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(this.txtCustomerNo.Text.Trim()) == 0)
                {
                    this.lblmsg_CompanyInfo.Text = MessageFormatter.GetFormattedErrorMessage("NOT A CORPORATE CUSTOMER!");
                    txtCustomerNoCompDetails.Text = txtAddInfoCustID.Text = txtCustNoAcctInfo.Text = txtPOACustNo.Text = txtAWOBCustNo.Text = txtDIRCustNo.Text = txtDIRAddyCustNo.Text = txtDIRDICustNo.Text = txtDIRForeginCustNo.Text = txtDIRNOKCustNo.Text = "";

                    txtCopmanyname.Text = txtIncopDate.Text = ddlCompanyType.SelectedValue = txtaddress.Text = ddlCatOfBiz.SelectedValue = "";
                    return;

                }
                else
                {

                    txtCustomerNoCompDetails.Text = txtAddInfoCustID.Text = txtCustNoAcctInfo.Text = txtPOACustNo.Text = txtAWOBCustNo.Text = txtDIRCustNo.Text = txtDIRAddyCustNo.Text = txtDIRDICustNo.Text = txtDIRForeginCustNo.Text = txtDIRNOKCustNo.Text = txtCustomerNo.Text;
                    lblmsg_CompanyInfo.Text = MessageFormatter.GetFormattedSuccessMessage("Record Successfully Found!");
                }


                GridView1.DataBind();
                GridView3.DataBind();


            }
            catch (Exception ex)
            {
                lblmsg_CompanyInfo.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message + ex.StackTrace);

            }

        }
        protected void btnSave_AccountInfo(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab3";

            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.prc_corp_acct_service_require";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = txtCustNoAcctInfo.Text;
            objCmd.Parameters.Add("p_account_number", OracleDbType.Varchar2).Value = txtAcctInfoAcctNo.Text;
            objCmd.Parameters.Add("p_account_name", OracleDbType.Varchar2).Value = txtAcctInfoAcctName.Text;
            objCmd.Parameters.Add("p_account_type", OracleDbType.Varchar2).Value = ddlAcctInfoAccttype.SelectedValue;
            objCmd.Parameters.Add("p_domicile_branch", OracleDbType.Varchar2).Value = ddlAcctInfoDomBranch.SelectedValue;
            objCmd.Parameters.Add("p_referral_code", OracleDbType.Varchar2).Value = txtAcctInfoReferralCode.Text;    
            objCmd.Parameters.Add("p_card_preference", OracleDbType.Varchar2).Value = rbtAcctInfoCrdPref.SelectedValue;
            objCmd.Parameters.Add("p_electronic_banking_prefer", OracleDbType.Varchar2).Value = rbtAcctInfoEBankingPref.SelectedValue;
            objCmd.Parameters.Add("p_statement_preferences", OracleDbType.Varchar2).Value = rbtAcctInfoStatmntPref.SelectedValue;
            objCmd.Parameters.Add("p_transaction_alert_preference", OracleDbType.Varchar2).Value = rbtTranxAlertPref.SelectedValue;
            objCmd.Parameters.Add("p_statement_frequency", OracleDbType.Varchar2).Value = rbtAcctInfoStatmntFreq.Text;
            objCmd.Parameters.Add("p_cheque_book_requisition", OracleDbType.Varchar2).Value = rbtAcctInfoChequeConfmtnReq.SelectedValue;
            objCmd.Parameters.Add("p_cheque_confirmation", OracleDbType.Varchar2).Value = rbtAcctInfoChequeConfmtn.SelectedValue;
            objCmd.Parameters.Add("p_cheque_confirm_threshold", OracleDbType.Varchar2).Value = rbtAcctInfoChequeConfmtnThreshold.SelectedValue;


            //
            objCmd.Parameters.Add("p_last_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
            objCmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = "N";
            objCmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
            objCmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "MAKER";
            //
            if (txtCustNoAcctInfo.Text == string.Empty)
            {
                lblAccountInfo.Text = MessageFormatter.GetFormattedErrorMessage("Customer No Required");

                return;
            }

            getCustID rst = new getCustID();

            try
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();//

                //
                if (rst.get_corp_customer_id(txtCustNoAcctInfo.Text) == 0)
                {
                    lblAccountInfo.Text = MessageFormatter.GetFormattedErrorMessage("Invalid Customer No");


                    return;

                }
                int rstCmd = objCmd.ExecuteNonQuery();

                if (rstCmd == -1)
                {

                    lblAccountInfo.Text = MessageFormatter.GetFormattedSuccessMessage("Customer Account Record Added Successful");

                    EmailHelper Emailer = new EmailHelper();
                    Emailer.NotificationMailSender((String)(Session["UserID"]));

                    txtCustNoAcctInfo.Text = ddlAcctInfoDomBranch.SelectedValue = ddlAcctInfoAccttype.SelectedValue = txtAcctInfoReferralCode.Text = txtAcctInfoAcctNo.Text = txtAcctInfoAcctName.Text = rbtAcctInfoCrdPref.SelectedValue = rbtAcctInfoEBankingPref.SelectedValue = rbtAcctInfoStatmntPref.SelectedValue = rbtTranxAlertPref.SelectedValue = rbtAcctInfoStatmntFreq.Text = rbtAcctInfoChequeConfmtnReq.SelectedValue = rbtAcctInfoChequeConfmtn.SelectedValue = rbtAcctInfoChequeConfmtnThreshold.SelectedValue = "";
                    GridView3.DataBind();
                }
                else
                {

                    lblAccountInfo.Text = MessageFormatter.GetFormattedErrorMessage("Error: operation failed");

                }

            }
            catch (Exception ex)
            {

                lblAccountInfo.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);

            }
            finally
            {
                objCmd = null;
                con.Close();
            }

        }
    }
}