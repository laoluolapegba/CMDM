using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//using Cdma.Web.Properties;
using System.Net;
using System.Net.Mail;
//using CMdm.Data;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace CMdm.UI.Web.BLL
{
    public class EmailHelper
    {

        //public Cdma.External.CDMA_Model db = new Cdma.External.CDMA_Model();

        private static string logs = "";
        private Customer custObj;
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        // public OracleCommand cmd = new OracleCommand();
        public OracleDataReader rdr;
        public OracleDataAdapter adapter = new OracleDataAdapter();

        public void NotificationMailSender(string UserName)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                //////////////////////////////////////////////////////////////////////////////////////
                // Create the mail message
                MailMessage mail = new MailMessage();
                // Set the host, 
                SmtpClient smtp = new SmtpClient();
                // Set the from address and to address
                mail.From = new MailAddress(ConfigurationManager.AppSettings["Account"]);
                mail.To.Add(new MailAddress(getUserEmailwithUserID(UserName)));//n

                //StringBuilder sb = new StringBuilder();
                // Set the subject and body
                mail.Subject = "CDMA Customer record update notification!!!";

                mail.Body = FetchMailBody(UserName);//n
                mail.BodyEncoding = System.Text.Encoding.ASCII;
                mail.IsBodyHtml = true;

                smtp.UseDefaultCredentials = false;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Account"], ConfigurationManager.AppSettings["Password"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                smtp.Host = ConfigurationManager.AppSettings["SmtpHost"];//"smtp.office365.com";
                ///Settings.Default.Save();
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;

                // ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate,X509Chain chain, SslPolicyErrors sslPolicyErrors){ return true; };

                smtp.Send(mail);

                sb.AppendLine();
                // sb.Append(" " + Util.getUserName(n) +" ("+ getEmpEmailwithID(Convert.ToInt32(n)).ToString() + ") , ");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void C2MNotificationMailSender(string CheckerName, string MakerName, string customerNo, string status, string rejectedTab,string checkersComment)
        {
            
            StringBuilder sb = new StringBuilder();
            string MakerEmail = getEmailwithUserID(MakerName);
            try
            {
                //////////////////////////////////////////////////////////////////////////////////////
                // Create the mail message
                MailMessage mail = new MailMessage();
                // Set the host, 
                SmtpClient smtp = new SmtpClient();
                // Set the from address and to address
                mail.From = new MailAddress(ConfigurationManager.AppSettings["Account"]);
                mail.To.Add(new MailAddress(MakerEmail));//maker email

                //StringBuilder sb = new StringBuilder();
                // Set the subject and body
                mail.Subject = "CDMA Customer record Approval/Rejection notification!!! -- for " + MakerName;

                mail.Body = FetchC2MMailBody(CheckerName, MakerEmail, customerNo, status, rejectedTab, MakerName, checkersComment);//n
                mail.BodyEncoding = System.Text.Encoding.ASCII;
                mail.IsBodyHtml = true;

                smtp.UseDefaultCredentials = false;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Account"], ConfigurationManager.AppSettings["Password"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings[".Port"]);
                smtp.Host = ConfigurationManager.AppSettings["SmtpHost"];//"smtp.office365.com";
                //Settings.Default.Save();
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;

                // ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate,X509Chain chain, SslPolicyErrors sslPolicyErrors){ return true; };

                smtp.Send(mail);

                sb.AppendLine();
                // sb.Append(" " + Util.getUserName(n) +" ("+ getEmpEmailwithID(Convert.ToInt32(n)).ToString() + ") , ");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        public void UserCreationMailSender(string UserEmail, string Role)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                //////////////////////////////////////////////////////////////////////////////////////
                // Create the mail message
                MailMessage mail = new MailMessage();
                // Set the host, 
                SmtpClient smtp = new SmtpClient();
                // Set the from address and to address
                mail.From = new MailAddress(ConfigurationManager.AppSettings["Account"]);
                mail.To.Add(new MailAddress(UserEmail.Trim()));//n

                //StringBuilder sb = new StringBuilder();
                // Set the subject and body
                mail.Subject = "CDMA User Creation!!!";

                mail.Body = FetchMailBodyUserCreation(UserEmail.Trim(), Role);//n
                mail.IsBodyHtml = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Account"], ConfigurationManager.AppSettings["Password"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                smtp.Host = ConfigurationManager.AppSettings["SmtpHost"];//"smtp.office365.com";
                //Settings.Default.Save();
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;

                smtp.Send(mail);

                sb.AppendLine();
                // sb.Append(" " + Util.getUserName(n) +" ("+ getEmpEmailwithID(Convert.ToInt32(n)).ToString() + ") , ");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        

        public void UserAssignmentMailSender(string MakerID, string CheckerID)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                //////////////////////////////////////////////////////////////////////////////////////
                // Create the mail message
                MailMessage mail = new MailMessage();
                // Set the host, 
                SmtpClient smtp = new SmtpClient();
                // Set the from address and to address
                mail.From = new MailAddress(ConfigurationManager.AppSettings["Account"]);
                mail.To.Add(new MailAddress(getEmailwithProfID(MakerID)));//n

                //StringBuilder sb = new StringBuilder();
                // Set the subject and body
                mail.Subject = "CDMA User Assigning!!!";

                mail.Body = FetchMappingMailBody(getEmailwithProfID(MakerID), getEmailwithProfID(CheckerID));//n
                mail.IsBodyHtml = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Account"], ConfigurationManager.AppSettings["Password"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                smtp.Host = ConfigurationManager.AppSettings["SmtpHost"];//"smtp.office365.com";
                //Settings.Default.Save();
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;

                smtp.Send(mail);


                sb.AppendLine();
                // sb.Append(" " + Util.getUserName(n) +" ("+ getEmpEmailwithID(Convert.ToInt32(n)).ToString() + ") , ");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private string FetchMappingMailBody(string makerEmail, string checkerEmail)
        {
            //string email = getUserEmailwithUserID(name);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.Append("<p>Hello " + makerEmail.Substring(0, makerEmail.IndexOf('@')) + ",</P>");//get the username from email` 
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("<p>Your account on CDMA has been assigned to " + checkerEmail.Substring(0, checkerEmail.IndexOf('@')) + " for the purpose of Customer data review.</P>");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("<p>You can login here http://10.0.5.153/CDMA/login.aspx with your Active Directory Username and Password</P>");
            sb.AppendLine();
            sb.Append("<p>Please contact the administrator if you have any question(s).</P>");

            sb.AppendLine();
            sb.AppendLine();

            sb.Append("<p>Kind Regards,</P>");
            sb.AppendLine();
            sb.Append("<p>CDMA Team.</P>");

            return sb.ToString();
        }

        private string getEmailwithProfID(string UserID)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            //string Email;
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = con;

            objCmd.CommandText = "select EMAIL_ADDRESS from cm_user_profile where profile_id = :p_profid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_profid", OracleDbType.NVarchar2).Value = UserID;

            {
                OracleDataReader rdr;
                rdr = objCmd.ExecuteReader();

                if (rdr.Read())
                {
                    return rdr.GetString(0);
                }
                return null;
            }
        }

        private string getEmailwithUserID(string UserID)// getting email of maker/checker with ID
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            //string Email;
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = con;

            objCmd.CommandText = "select EMAIL_ADDRESS from cm_user_profile where user_id = :p_user_id";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_user_id", OracleDbType.NVarchar2).Value = UserID;

            {
                OracleDataReader rdr;
                rdr = objCmd.ExecuteReader();

                if (rdr.Read())
                {
                    return rdr.GetString(0);
                }
                return null;
            }
        }

        public string getIndivMakerwithCustNo(string CustNo,string tableName)// getting email of maker/checker with ID
        {
            

            if (con.State == ConnectionState.Closed)
                con.Open();
            //string Email;
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = con;

            //string query = "";
            //if (CustAcctNo == "")
            //{
             //   query = "select CREATED_BY,LAST_MODIFIED_BY from " + tableName + " where CUSTOMER_NO = :p_cust_no";
            //}
            //else
            //{
            //    query = "select CREATED_BY,LAST_MODIFIED_BY from " + tableName + " where CUSTOMER_NO = :p_cust_no and ACCOUNT_NUMBER = :p_acctNo";
            //}
            objCmd.CommandText = "select distinct CREATED_BY,LAST_MODIFIED_BY from " + tableName + " where CUSTOMER_NO = :p_cust_no and rownum <= 1";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_cust_no", OracleDbType.NVarchar2).Value = CustNo;
            //objCmd.Parameters.Add("p_acctNo", OracleDbType.NVarchar2).Value = CustAcctNo;

            {
                OracleDataReader rdr;
                rdr = objCmd.ExecuteReader();

                string createdBy = string.Empty;
                string lastestModBy = string.Empty;
                string makerName = string.Empty;

                if (rdr.Read())
                {
                    createdBy = rdr.GetString(0);
                    lastestModBy = rdr.GetString(1);

                    if (createdBy != string.Empty || lastestModBy != string.Empty)
                        //    // {
                    return makerName = lastestModBy == string.Empty ? createdBy : lastestModBy;
                }
                return null;
            }
        }


        public string getUserEmailwithUserID(string userid)//using maker ID to get checker email.
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            //string Email;
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = con;

            objCmd.CommandText = "select EMAIL_ADDRESS from cm_user_profile where profile_id =(select m.checker_id from cm_user_profile u,CM_MAKER_CHECKER_XREF m where u.profile_id = m.maker_id and u.user_id = :p_userid)";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_userid", OracleDbType.NVarchar2).Value = userid;//maker ID

            {
                OracleDataReader rdr;
                rdr = objCmd.ExecuteReader();

                if (rdr.Read())
                {
                    return rdr.GetString(0);
                }
                return null;
            }
        }

        public string FetchMailBodyUserCreation(string email, string Role)
        {
            //string email = getUserEmailwithUserID(name);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.Append("<p>Hello " + email.Substring(0, email.IndexOf('@')) + ",</p>");//get the username from email` 
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("<p>An account has been created for you with a " + Role.ToUpper() + " Role on CDMA Application.</p>");
            sb.AppendLine();

            sb.AppendLine();
            sb.Append("<p>Please login here http://10.0.5.153/CDMA/login.aspx with your Active Directory Username and Password</p>");

            sb.AppendLine();
            sb.AppendLine();

            sb.Append("<p>Kind Regards,</p>");
            sb.AppendLine();
            sb.Append("<p>CDMA Team.</p>");

            return sb.ToString();
        }

        public string FetchMailBody(string name)
        {
            string Checkeremail = getUserEmailwithUserID(name);
            string checkerName = getCheckerNamewithEmail(Checkeremail);
            StringBuilder sb = new StringBuilder();
//Checkeremail.Substring(0, Checkeremail.IndexOf('@'))
            sb.AppendLine();
            sb.Append("<p>Hello " + checkerName + ",</p>");//get the username from email` 
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("<p>Customer data was updated by " + name + " and requires your attention.</p>");
            sb.AppendLine();

            sb.AppendLine();
            sb.Append("<p>Please login here http://10.0.5.153/CDMA/login.aspx</p>");

            sb.AppendLine();
            sb.AppendLine();

            sb.Append("<p>Kind Regards,</p>");
            sb.AppendLine();
            sb.Append("<p>CDMA Team.</p>");

            return sb.ToString();
        }

        private string getCheckerNamewithEmail(string Checkeremail)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            //string Email;
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = con;

            objCmd.CommandText = "select USER_ID from cm_user_profile where EMAIL_ADDRESS = :p_Checkeremail";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_Checkeremail", OracleDbType.NVarchar2).Value = Checkeremail;//maker ID

            {
                OracleDataReader rdr;
                rdr = objCmd.ExecuteReader();

                if (rdr.Read())
                {
                    return rdr.GetString(0);
                }
                return null;
            }
        }


        public string FetchC2MMailBody(string checkername, string makerEmail, string CustomerNo, string status, string RejectedTab, string MakerName,string CheckersComment)
        {

            string customerName = getCustNameWithCustNo(CustomerNo);
            StringBuilder sb = new StringBuilder();

            if (status == "Y")
            {//makerEmail.Substring(0, makerEmail.IndexOf('@'))
                sb.AppendLine();
                sb.Append("<p>Hello " + MakerName + ",</p>");//get the username from email` 
                sb.AppendLine();
                sb.AppendLine();
                sb.Append("<p>Customer " + customerName + "'s (" + CustomerNo + ") data has been successfully approved by " + checkername + ".</p>");
                sb.AppendLine();

                sb.AppendLine();
                sb.Append("<p>Please login here http://10.0.5.153/CDMA/login.aspx</p>");

                sb.AppendLine();
                sb.AppendLine();

                sb.Append("<p>Kind Regards,</p>");
                sb.AppendLine();
                sb.Append("<p>CDMA Team.</p>");
            }
            else
            {//makerEmail.Substring(0, makerEmail.IndexOf('@'))
                sb.AppendLine();
                sb.Append("<p>Hello " + MakerName + ",</p>");//get the username from email` 
                sb.AppendLine();
                sb.AppendLine();
                sb.Append("<p>Customer " + customerName + "'s (" + CustomerNo + ")  data has been rejected by " + checkername + ", the Tab rejected is " + RejectedTab + ", please review and send back for approval.</p>");
                sb.AppendLine();
                sb.Append("<p><b>Checker's Comment:  " + CheckersComment + "</b></p>");
                sb.AppendLine();
                sb.AppendLine();
                sb.Append("<p>You can login here http://10.0.5.153/CDMA/login.aspx</p>");

                sb.AppendLine();
                sb.AppendLine();

                sb.Append("<p>Kind Regards,</p>");
                sb.AppendLine();
                sb.Append("<p>CDMA Team.</p>");

            }
            return sb.ToString();
        }


        public string getCustNameWithCustNo(string CustomerNo)
        {
            String customerName = "";

            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms_maker.get_indiv_details";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.Varchar2).Value = CustomerNo;
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

                    if (CustomerNo == reader["customer_no"].ToString())
                    {
                        customerName = reader["first_name"] == DBNull.Value ? reader["surname"].ToString() : reader["surname"].ToString() + " " + reader["first_name"].ToString();

                    }
                }//end of while

            }
            catch (Exception ex)
            {
                //lblstatus.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message + ex.InnerException);

            }
            finally
            {
                //reader.Close();
                objCmd = null;
                con.Close();
            }
            return customerName;
        }
    

 
    }

}

