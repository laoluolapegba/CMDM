using Elmah;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace CMdm.UI.Web.BLL
{
    public class DQIProfileBiz
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        private OracleConnection conn = new OracleConnection(connString);
        public string DQIValue()
        {
            string custCount = string.Empty;

            OracleCommand objCmd = new OracleCommand();
            objCmd.CommandText = "select round(100-avg(column_weight),2) dqi from(select table_name, a.column_name, a.VALIDITY_FAILED_PCT,NEW_WEIGHT weight, a.VALIDITY_FAILED_PCT* NEW_WEIGHT column_weight   from dqi_profile_result23 a, mdm_dqi_parameters b   where a.column_name = b.column_names)";
            objCmd.CommandType = CommandType.Text;


            try
            {
                objCmd.Connection = conn; conn.Open();
                custCount = objCmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                conn.Close();

            }

            return custCount;

        }
        public int CustCount(string tablename)
        {
            int custCount = 0;

            OracleCommand objCmd = new OracleCommand();
            objCmd.CommandText = "select count(1) from " + tablename;
            objCmd.CommandType = CommandType.Text;


            try
            {
                objCmd.Connection = conn; conn.Open();
                custCount = Convert.ToInt32(objCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                conn.Close();

            }

            return custCount;

        }

        public bool RunDQIProfileProc()
        {
            bool retStat = false;
            try
            {
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = conn;
                conn.Open();//
                objCmd.CommandText = "DQI";

                objCmd.CommandType = CommandType.StoredProcedure;

                int i = objCmd.ExecuteNonQuery();
                retStat = true;

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                
            }
            finally
            {
                conn.Close();
            }

            return retStat;
        }
    }
}