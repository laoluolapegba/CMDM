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

using System.Configuration;
namespace CMdm.UI.Web.BLL
{
    public class get_Biz_id
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        public OracleCommand cmd = new OracleCommand();
        public OracleDataReader rdr;
        public OracleDataAdapter adapter = new OracleDataAdapter();
        //
        public int get_bizCust_id(string custID)
        {
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = con;

            objCmd.CommandText = "pkg_cdms2.verify_custbizid";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_custID", OracleDbType.Varchar2).Value = custID;
            //
            objCmd.Parameters.Add("Bizcid", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            int created = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();//

                OracleDataReader reader;
                reader = objCmd.ExecuteReader();

                while (reader.Read())
                {
                    if (custID == reader["customer_id"].ToString())
                    {
                        created = 1;

                    }
                    else
                        created = 0;

                }//end of while
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
            finally
            {
                objCmd = null;
                con.Close();
            }

            return created;

        }
    }
}