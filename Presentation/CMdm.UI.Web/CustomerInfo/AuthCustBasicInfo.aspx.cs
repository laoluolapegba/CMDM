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
using Oracle.DataAccess.Types;
using System.Globalization;

namespace Cdma.Web.CustomerInfo
{
    public partial class AuthCustBasicInfo : System.Web.UI.Page
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        public OracleCommand cmd = new OracleCommand();
        public OracleDataReader rdr;
        public OracleDataAdapter adapter = new OracleDataAdapter();
        private CustomerRepository r;
        private Audit audit;
        private UtilityClass utility;
       
              
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnAuthorise.Attributes.Add("onClick", "javascript:alert('Saved successfully');");
            LoadDefaults();
        }

        private void LoadDefaults()
        {

            if (!Page.IsPostBack)
            {

            }
            else
            {

            }
        }

        protected void btnAuthorise_Click(object sender, EventArgs e)
        {
            Authorize("Y", "Authorized successfully");
        }

        protected void btnDeny_Click(object sender, EventArgs e)
        {
            Authorize("N", "Denied successfully");
        }

        protected void anni1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (txtType1.SelectedItem.Text == "BUSINESS YEAR END")
            //lblanni1.Text = "Please,select the year of Incorporation";

        }

        private void Authorize(string authorized, string statusMsg)
        {
            audit = new Audit();
            OracleConnection con22 = new OracleConnection();
            con22.ConnectionString = new Connection().ConnectionString;
            con22.Open();
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.BindByName = true;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "pkg_cdms2.update_customer";// 
                cmd.Connection = con22;

                OracleParameter prm = new OracleParameter();
                cmd.Parameters.Add("p_customerid", OracleDbType.Varchar2).Value = this.txtCustID.Text;
                cmd.Parameters.Add("p_customer_name", OracleDbType.Varchar2).Value = this.txtFullName.Text;
                cmd.Parameters.Add("p_customer_code", OracleDbType.Varchar2).Value = this.txtCustCode.Text;
                cmd.Parameters.Add("p_secret_question", OracleDbType.Varchar2).Value = this.txtSecurityQuestn.Text;
                cmd.Parameters.Add("p_secret_question_answer", OracleDbType.Varchar2).Value = this.txtSecretAnswer.Text;
                cmd.Parameters.Add("p_customer_type", OracleDbType.Varchar2).Value = this.txtCustType.Text;
                cmd.Parameters.Add("p_email_address_1", OracleDbType.Varchar2).Value = this.txtEmailAdd.Text;
                cmd.Parameters.Add("p_email_address_1_1", OracleDbType.Varchar2).Value = this.txtAltEmailAdd.Text;
                cmd.Parameters.Add("p_web_address", OracleDbType.Varchar2).Value = this.txtWebAdd.Text;
                cmd.Parameters.Add("p_politically_exposed_person", OracleDbType.Varchar2).Value = this.txtPolExposed.Text;
                cmd.Parameters.Add("p_financially_exposed_person", OracleDbType.Varchar2).Value = this.txtFinExposed.Text;

                //
                cmd.Parameters.Add("p_anniversary_type1", OracleDbType.Varchar2).Value = this.txtType1.Text;
                cmd.Parameters.Add("p_anniversary_date1", OracleDbType.Date).Value = this.txtDate1.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtDate1.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

                cmd.Parameters.Add("p_anniversary_type2", OracleDbType.Varchar2).Value = this.txtType2.Text;
                cmd.Parameters.Add("p_anniversary_date2", OracleDbType.Date).Value = this.txtDate2.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtDate2.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

                cmd.Parameters.Add("p_anniversary_type3", OracleDbType.Varchar2).Value = txtType3.Text;
                cmd.Parameters.Add("p_anniversary_date3", OracleDbType.Date).Value = this.txtDate3.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtDate3.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

                cmd.Parameters.Add("p_anniversary_type4", OracleDbType.Varchar2).Value = txtType4.Text;
                cmd.Parameters.Add("p_anniversary_date4", OracleDbType.Date).Value = this.txtDate4.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtDate4.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

                cmd.Parameters.Add("p_anniversary_type5", OracleDbType.Varchar2).Value = txtType5.Text;
                cmd.Parameters.Add("p_anniversary_date5", OracleDbType.Date).Value = this.txtDate5.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtDate5.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

                cmd.Parameters.Add("p_anniversary_type6", OracleDbType.Varchar2).Value = txtType6.Text;
                cmd.Parameters.Add("p_anniversary_date6", OracleDbType.Date).Value = this.txtDate6.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtDate6.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

                cmd.Parameters.Add("p_anniversary_type7", OracleDbType.Varchar2).Value = this.txtType7.Text;
                cmd.Parameters.Add("p_anniversary_date7", OracleDbType.Date).Value = this.txtDate7.Text == string.Empty ? DBNull.Value : (object)Convert.ToDateTime(this.txtDate7.Text, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                //
                cmd.Parameters.Add("p_address_type", OracleDbType.Varchar2).Value = txtAddressType.Text;
                cmd.Parameters.Add("p_house_identifier", OracleDbType.Varchar2).Value = txtHomeIdentifier.Text;
                cmd.Parameters.Add("p_address_line_1", OracleDbType.Varchar2).Value = txtAddLine1.Text;
                cmd.Parameters.Add("p_address_line_2", OracleDbType.Varchar2).Value = txtAddLine2.Text;
                cmd.Parameters.Add("p_address_line_3", OracleDbType.Varchar2).Value = string.Empty;
                cmd.Parameters.Add("p_administrative_area", OracleDbType.Varchar2).Value = txtAreaCode.Text;
                cmd.Parameters.Add("p_locality", OracleDbType.Varchar2).Value = txtLocality.Text;
                cmd.Parameters.Add("p_location_coordinates", OracleDbType.Varchar2).Value = txtLocCordnts.Text;
                cmd.Parameters.Add("p_post_code", OracleDbType.Varchar2).Value = txtPostalCod.Text;
                //
                cmd.Parameters.Add("p_post_office_box", OracleDbType.Varchar2).Value = txtPOBox.Text;
                cmd.Parameters.Add("p_country", OracleDbType.Varchar2).Value = txtCountry.Text;
                cmd.Parameters.Add("p_state", OracleDbType.Varchar2).Value = txtState.Text;
                cmd.Parameters.Add("p_city", OracleDbType.Varchar2).Value = txtCity.Text;
                cmd.Parameters.Add("p_country_of_residence", OracleDbType.Varchar2).Value = txtCountryOfResidence.Text;
                cmd.Parameters.Add("p_phone_category", OracleDbType.Varchar2).Value = txtPhoneCat.Text;
                cmd.Parameters.Add("p_area_code", OracleDbType.Varchar2).Value = txtAreaCode.Text;
                cmd.Parameters.Add("p_country_code", OracleDbType.Varchar2).Value = txtCountryCod.Text;
                cmd.Parameters.Add("p_phone_number", OracleDbType.Varchar2).Value = txtPhoneNo.Text;
                cmd.Parameters.Add("p_extension_no", OracleDbType.Varchar2).Value = txtExtNo.Text;
                cmd.Parameters.Add("p_phone_type", OracleDbType.Varchar2).Value = txtPhoneType.Text;
                cmd.Parameters.Add("p_channel_supported", OracleDbType.Varchar2).Value = txtChannelSupport.Text;
                cmd.Parameters.Add("p_reachable_hours", OracleDbType.Varchar2).Value = txtReachableHr.Text;
                //
                cmd.Parameters.Add("p_modified_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
                cmd.Parameters.Add("p_authorised", OracleDbType.Varchar2).Value = authorized;
                cmd.Parameters.Add("p_authorised_by", OracleDbType.Varchar2).Value = (String)(Session["UserID"]);
                audit = new Audit();
                cmd.Parameters.Add("p_ip_address", OracleDbType.Varchar2).Value = audit.IPAddress;
                cmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = "CHECKER";

                cmd.ExecuteNonQuery();

                // Close and Dispose OracleConnection object

                lblstatus.Text = MessageFormatter.GetFormattedSuccessMessage(statusMsg);

                callClearBox();
            }
            catch (Exception ex)
            {
                lblstatus.Text = MessageFormatter.GetFormattedErrorMessage(ex.Source + "  " + ex.Message);
                callClearBox();
            }
            finally
            {
                con22.Close();
                con22.Dispose();
                GridView2.DataBind();
            }
        }

        private void callClearBox()
        {
            txtAreaCode.Text = txtCountryCod.Text = txtPhoneNo.Text = txtExtNo.Text = txtPhoneType.Text = txtChannelSupport.Text = txtReachableHr.Text = "";
            txtCustID.Text = txtFullName.Text = txtCustCode.Text = txtSecurityQuestn.Text = txtCustType.Text = "";
            txtEmailAdd.Text = txtAltEmailAdd.Text = txtWebAdd.Text = txtPolExposed.Text = txtFinExposed.Text = "";
            txtDate1.Text = txtDate2.Text = txtDate3.Text = txtDate4.Text = txtDate5.Text = txtDate6.Text = txtDate7.Text = "";
            txtAddressType.Text = txtHomeIdentifier.Text = txtAddLine1.Text = txtAddLine2.Text = txtAreaCode.Text = txtLocality.Text = txtLocCordnts.Text = txtPostalCod.Text = "";
            txtPOBox.Text = txtCountry.Text = txtState.Text = txtCity.Text = txtCountryOfResidence.Text = txtPhoneCat.Text = "";
        }




        protected void btnSearch_Click(object sender, EventArgs e)
        {
            OracleCommand objCmd = new OracleCommand();

            try
            {
                objCmd.Connection = con;

                objCmd.CommandText = "pkg_cdms2.tmp_get_basic_details";

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_cusid", OracleDbType.NVarchar2).Value = txtCustID.Text;
                //
                objCmd.Parameters.Add("custinfo", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            }
            catch (Exception ex)
            {
                this.lblstatus.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }
            if (txtCustID.Text == string.Empty)
            {

                this.lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Enter Customer ID");
                callClearBox();

                return;
            }

            LoadDefaults();

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();//

                OracleDataReader rdr;
                rdr = objCmd.ExecuteReader();

                while (rdr.Read())
                {
                    //lblmsg_ContractType.Text = rdr["CUSTOMER_ID"].ToString();
                    txtFullName.Text = Convert.ToString(rdr["CUSTOMER_NAME"]);
                    txtCustID.Text = rdr["CUSTOMER_ID"].ToString();
                    txtFullName.Text = Convert.ToString(rdr["CUSTOMER_NAME"]);
                    txtCustCode.Text = Convert.ToString(rdr["CUSTOMER_CODE"]);
                    // if (rdr["SECRET_QUESTION"] != null || rdr["SECRET_QUESTION"].ToString() != "" ) 
                    txtSecurityQuestn.Text = Convert.ToString(rdr["SECRET_QUESTION"]);
                    txtSecretAnswer.Text = Convert.ToString(rdr["SECRET_QUESTION_ANSWER"]);

                    txtCustType.Text = Convert.ToString(rdr["CUSTOMER_TYPE"]);
                    txtEmailAdd.Text = Convert.ToString(rdr["EMAIL_ADDRESS_1"]);
                    txtAltEmailAdd.Text = Convert.ToString(rdr["EMAIL_ADDRESS_1_1"]);
                    txtWebAdd.Text = Convert.ToString(rdr["WEB_ADDRESS"]);
                    txtPolExposed.Text = Convert.ToString(rdr["POLITICALLY_EXPOSED_PERSON"]);
                    //txtFinExposed.Text = Convert.ToString(rdr[10]);
                    txtFinExposed.Text = Convert.ToString(rdr["FINANCIALLY_EXPOSED_PERSON"]);

                    txtType1.Text = Convert.ToString(rdr["ANNIVERSARY_TYPE1"]);
                    txtDate1.Text = Convert.ToString(rdr["ANNIVERSARY_DATE1"]);

                    txtType2.Text = Convert.ToString(rdr["ANNIVERSARY_TYPE2"]);
                    txtDate2.Text = Convert.ToString(rdr["ANNIVERSARY_DATE2"]); 
                    
                    if (txtType3.Text.Equals(new ListItem(Convert.ToString(rdr["ANNIVERSARY_TYPE3"]))))
                        txtType3.Text = Convert.ToString(rdr["ANNIVERSARY_TYPE3"]);
                    txtDate3.Text = Convert.ToString(rdr["ANNIVERSARY_DATE3"]);
                    //

                    txtType4.Text = Convert.ToString(rdr["ANNIVERSARY_TYPE4"]);
                    txtDate4.Text = Convert.ToString(rdr["ANNIVERSARY_DATE4"]); 

                    txtType5.Text = Convert.ToString(rdr["ANNIVERSARY_TYPE5"]);
                    txtDate5.Text = Convert.ToString(rdr["ANNIVERSARY_DATE5"]); 

                    txtType6.Text = Convert.ToString(rdr["ANNIVERSARY_TYPE6"]);
                    txtDate6.Text = Convert.ToString(rdr["ANNIVERSARY_DATE6"]); 
                    //

                    txtType7.Text = Convert.ToString(rdr["ANNIVERSARY_TYPE7"]);
                    txtDate7.Text = Convert.ToString(rdr["ANNIVERSARY_DATE7"]);

                    txtAuthorizedBy.Text = Convert.ToString(rdr["AUTHORISED_BY"]);
                    txtAuthorizedDate.Text = Convert.ToString(rdr["AUTHORISED_DATE"]);
                    txtCreatedDate.Text = Convert.ToString(rdr["CREATED_DATE"]);

                    txtCreatedBy.Text = Convert.ToString(rdr["CREATED_BY"]);
                    txtModifiedDate.Text = Convert.ToString(rdr["LAST_MODIFIED_DATE"]);
                    txtModiedBy.Text = Convert.ToString(rdr["LAST_MODIFIED_BY"]);
                    //

                    txtAddressType.Text = Convert.ToString(rdr["ADDRESS_TYPE"]);
                    txtHomeIdentifier.Text = Convert.ToString(rdr["HOUSE_IDENTIFIER"]);
                    txtAddLine1.Text = Convert.ToString(rdr["ADDRESS_LINE_1"]);
                    txtAddLine2.Text = Convert.ToString(rdr["ADDRESS_LINE_2"]);

                    //address3.Text = Convert.ToString(rdr["ADDRESS_LINE_3"]);
                    txtAreaCode.Text = Convert.ToString(rdr["ADMINISTRATIVE_AREA"]);
                    txtLocality.Text = Convert.ToString(rdr["LOCALITY"]);
                    txtLocCordnts.Text = Convert.ToString(rdr["LOCATION_COORDINATES"]);
                    txtPostalCod.Text = Convert.ToString(rdr["POST_CODE"]);
                    txtHomeIdentifier.Text = Convert.ToString(rdr["HOUSE_IDENTIFIER"]);
                    txtPOBox.Text = Convert.ToString(rdr["POST_OFFICE_BOX"]);
                    //var titleCase = new string(.ToArray());
                    //lbl.Text = titleCase;
                    UtilityClass utility = new UtilityClass();
                    txtCountry.Text = utility.TitleCase(Convert.ToString(rdr["COUNTRY"]));
                    txtState.Text = utility.TitleCase(Convert.ToString(rdr["STATE"])).Trim();
                    txtCity.Text = utility.TitleCase(Convert.ToString(rdr["CITY"]));
                    txtCountryOfResidence.Text = utility.TitleCase(Convert.ToString(rdr["COUNTRY_OF_RESIDENCE"]));
                    //
                    txtPhoneCat.Text = (Convert.ToString(rdr["PHONE_CATEGORY"]));
                    txtAreaCode.Text = Convert.ToString(rdr["AREA_CODE"]);
                    txtCountryCod.Text = Convert.ToString(rdr["COUNTRY_CODE"]);
                    txtPhoneNo.Text = Convert.ToString(rdr["PHONE_NUMBER"]);

                    txtExtNo.Text = Convert.ToString(rdr["EXTENSION_NO"]);
                    txtPhoneType.Text = (Convert.ToString(rdr["PHONE_TYPE"]));
                    txtChannelSupport.Text = (Convert.ToString(rdr["CHANNEL_SUPPORTED"]));
                    txtReachableHr.Text = (Convert.ToString(rdr["REACHABLE_HOURS"]));

                    lblstatus.Text = MessageFormatter.GetFormattedSuccessMessage("Record found Successful!");

                }//end of while
                rdr.Close();
            }
            catch (Exception ex)
            {

                lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message + " StackTrace:  " + ex.StackTrace);
                callClearBox();
            }
            finally
            {
                objCmd = null;
                con.Close();

                //ddlSecretQestion.Enabled = false;
                //txtSecretAnswer.Enabled = false; 
            }
        }
        //}

        protected void OnEdit_Customer(object sender, EventArgs e)
        {
           LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            //  lblstatus.Text  =
            OracleCommand objCmd = new OracleCommand();
            con = new OracleConnection(new Connection().ConnectionString);
            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms2.tmp_get_basic_details";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_cusid", OracleDbType.NVarchar2).Value = recId;
            //
            objCmd.Parameters.Add("custinfo", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();//


                OracleDataReader rdr;
                rdr = objCmd.ExecuteReader();

                while (rdr.Read())
                {
                    //lblmsg_ContractType.Text = rdr["CUSTOMER_ID"].ToString();
                    txtFullName.Text = Convert.ToString(rdr["CUSTOMER_NAME"]);
                    txtCustID.Text = rdr["CUSTOMER_ID"].ToString();
                    txtFullName.Text = Convert.ToString(rdr["CUSTOMER_NAME"]);
                    txtCustCode.Text = Convert.ToString(rdr["CUSTOMER_CODE"]);
                    // if (rdr["SECRET_QUESTION"] != null || rdr["SECRET_QUESTION"].ToString() != "" ) 
                    txtSecurityQuestn.Text = Convert.ToString(rdr["SECRET_QUESTION"]);
                    txtSecretAnswer.Text = Convert.ToString(rdr["SECRET_QUESTION_ANSWER"]);

                    txtCustType.Text = Convert.ToString(rdr["CUSTOMER_TYPE"]);
                    txtEmailAdd.Text = Convert.ToString(rdr["EMAIL_ADDRESS_1"]);
                    txtAltEmailAdd.Text = Convert.ToString(rdr["EMAIL_ADDRESS_1_1"]);
                    txtWebAdd.Text = Convert.ToString(rdr["WEB_ADDRESS"]);
                    txtPolExposed.Text = Convert.ToString(rdr["POLITICALLY_EXPOSED_PERSON"]);
                    //txtFinExposed.Text = Convert.ToString(rdr[10]);
                    txtFinExposed.Text = Convert.ToString(rdr["FINANCIALLY_EXPOSED_PERSON"]);

                    txtType1.Text = Convert.ToString(rdr["ANNIVERSARY_TYPE1"]);
                    txtDate1.Text = String.Format("{0:d/M/yyyy}", rdr["ANNIVERSARY_DATE1"]);

                    txtType2.Text = Convert.ToString(rdr["ANNIVERSARY_TYPE2"]);
                    txtDate2.Text = String.Format("{0:d/M/yyyy}", rdr["ANNIVERSARY_DATE2"]); 

                    if (txtType3.Text.Equals(new ListItem(Convert.ToString(rdr["ANNIVERSARY_TYPE3"]))))
                        txtType3.Text = Convert.ToString(rdr["ANNIVERSARY_TYPE3"]);
                    txtDate3.Text = String.Format("{0:d/M/yyyy}", rdr["ANNIVERSARY_DATE3"]);
                        
                    //

                    txtType4.Text = Convert.ToString(rdr["ANNIVERSARY_TYPE4"]);
                    txtDate4.Text = String.Format("{0:d/M/yyyy}", rdr["ANNIVERSARY_DATE4"]);

                    txtType5.Text = Convert.ToString(rdr["ANNIVERSARY_TYPE5"]);
                    txtDate5.Text = String.Format("{0:d/M/yyyy}", rdr["ANNIVERSARY_DATE5"]);

                    txtType6.Text = Convert.ToString(rdr["ANNIVERSARY_TYPE6"]);
                    txtDate6.Text = String.Format("{0:d/M/yyyy}", rdr["ANNIVERSARY_DATE6"]);
                    //

                    txtType7.Text = Convert.ToString(rdr["ANNIVERSARY_TYPE7"]);
                    txtDate7.Text = String.Format("{0:d/M/yyyy}", rdr["ANNIVERSARY_DATE7"]);

                    
                    txtAuthorizedBy.Text = Convert.ToString(rdr["AUTHORISED_BY"]);
                    txtAuthorizedDate.Text = Convert.ToString(rdr["AUTHORISED_DATE"]);
                    txtCreatedDate.Text = Convert.ToString(rdr["CREATED_DATE"]);

                    txtCreatedBy.Text = Convert.ToString(rdr["CREATED_BY"]);
                    txtModifiedDate.Text = Convert.ToString(rdr["LAST_MODIFIED_DATE"]);
                    txtModiedBy.Text = Convert.ToString(rdr["LAST_MODIFIED_BY"]);
                    //

                    txtAddressType.Text = Convert.ToString(rdr["ADDRESS_TYPE"]);
                    txtHomeIdentifier.Text = Convert.ToString(rdr["HOUSE_IDENTIFIER"]);
                    txtAddLine1.Text = Convert.ToString(rdr["ADDRESS_LINE_1"]);
                    txtAddLine2.Text = Convert.ToString(rdr["ADDRESS_LINE_2"]);

                    //address3.Text = Convert.ToString(rdr["ADDRESS_LINE_3"]);
                    txtAreaCode.Text = Convert.ToString(rdr["ADMINISTRATIVE_AREA"]);
                    txtLocality.Text = Convert.ToString(rdr["LOCALITY"]);
                    txtLocCordnts.Text = Convert.ToString(rdr["LOCATION_COORDINATES"]);
                    txtPostalCod.Text = Convert.ToString(rdr["POST_CODE"]);
                    txtHomeIdentifier.Text = Convert.ToString(rdr["HOUSE_IDENTIFIER"]);
                    txtPOBox.Text = Convert.ToString(rdr["POST_OFFICE_BOX"]);
                    //var titleCase = new string(.ToArray());
                    //lbl.Text = titleCase;
                    UtilityClass utility = new UtilityClass();
                    txtCountry.Text = utility.TitleCase(Convert.ToString(rdr["COUNTRY"]));
                    txtState.Text = utility.TitleCase(Convert.ToString(rdr["STATE"])).Trim();
                    txtCity.Text = utility.TitleCase(Convert.ToString(rdr["CITY"]));
                    txtCountryOfResidence.Text = utility.TitleCase(Convert.ToString(rdr["COUNTRY_OF_RESIDENCE"]));
                    //
                    txtPhoneCat.Text = (Convert.ToString(rdr["PHONE_CATEGORY"]));
                    txtAreaCode.Text = Convert.ToString(rdr["AREA_CODE"]);
                    txtCountryCod.Text = Convert.ToString(rdr["COUNTRY_CODE"]);
                    txtPhoneNo.Text = Convert.ToString(rdr["PHONE_NUMBER"]);

                    txtExtNo.Text = Convert.ToString(rdr["EXTENSION_NO"]);
                    txtPhoneType.Text = (Convert.ToString(rdr["PHONE_TYPE"]));
                    txtChannelSupport.Text = (Convert.ToString(rdr["CHANNEL_SUPPORTED"]));
                    txtReachableHr.Text = (Convert.ToString(rdr["REACHABLE_HOURS"]));

                    lblstatus.Text = MessageFormatter.GetFormattedSuccessMessage("Record found Successful!");

                  

                }//end of while
                rdr.Close();
            }
            catch (Exception ex)
            {

                lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
                callClearBox();
               
            }
            finally
            {
                objCmd = null;
                con.Close();
            }

       
        }
    }
}