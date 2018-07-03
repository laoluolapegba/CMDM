using CMdm.Data.Rbac;
using CMdm.Entities.Domain.Kpi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CMdm.Data.DAC
{
    public class KPIDAC
    {
        public List<BrnKpi> GetBrnKPI(DateTime trandate, string BranchCode)
        {
            // decimal openingBal = 0;
            string connString = ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
            List<BrnKpi> allKpis = new List<BrnKpi>();
            #region New
            using (SqlConnection connection = new SqlConnection(connString))
            {             
                    connection.Open();
                StringBuilder sbd = new StringBuilder();
                sbd.Append("SELECT fci.TRAN_DATE, fci.BRANCH_CODE, fci.BRN_CUST_COUNT, fci.BRN_DQI, fci.BRN_OPEN_EXCEPTIONS, fci.BRN_PCT_CONTRIB, fci.BRN_RECURRING_ERRORS, ");
                sbd.Append("   fci.BRN_RESOLVED_ERRORS, fci.BANK_CUST_COUNT  FROM ");
                sbd.Append("  ( ");
                sbd.Append("      SELECT");
                sbd.Append("        CMDM_COMMON_KPI.TRAN_DATE, ");
                sbd.Append("        CMDM_COMMON_KPI.BRANCH_CODE, ");
                sbd.Append("        CMDM_COMMON_KPI.BRN_CUST_COUNT, ");
                sbd.Append("        CMDM_COMMON_KPI.BRN_DQI, ");
                sbd.Append("        CMDM_COMMON_KPI.BRN_OPEN_EXCEPTIONS, ");
                sbd.Append("       CMDM_COMMON_KPI.BRN_PCT_CONTRIB, ");
                sbd.Append("       CMDM_COMMON_KPI.BRN_RECURRING_ERRORS, ");
                sbd.Append("      CMDM_COMMON_KPI.BRN_RESOLVED_ERRORS, ");
                sbd.Append("     CMDM_COMMON_KPI.BANK_CUST_COUNT, ");
                sbd.Append("     row_number() OVER( ");
                sbd.Append("        ORDER BY CMDM_COMMON_KPI.TRAN_DATE DESC) AS RN ");
                sbd.Append("    FROM CDMACONV.CMDM_COMMON_KPI ");
                sbd.Append("    WHERE CMDM_COMMON_KPI.BRANCH_CODE = @var_p_branch_code ");
                sbd.Append(" )  AS fci ");
                sbd.Append("WHERE fci.RN = 1 ");
                SqlCommand command = new SqlCommand(sbd.ToString(), connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("var_p_branch_code", SqlDbType.VarChar).Value = BranchCode;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BrnKpi kpirow = new BrnKpi
                        {
                            BANK_CUST_COUNT = reader.GetInt32(reader.GetOrdinal("BANK_CUST_COUNT")),
                            BRN_CUST_COUNT = reader.GetInt32(reader.GetOrdinal("BRN_CUST_COUNT")),
                            BRN_DQI = reader.GetDecimal(reader.GetOrdinal("BRN_DQI")),
                            BRN_OPEN_EXCEPTIONS = reader.GetInt32(reader.GetOrdinal("BRN_OPEN_EXCEPTIONS")),
                            BRN_PCT_CONTRIB = reader.GetDecimal(reader.GetOrdinal("BRN_PCT_CONTRIB")),
                            BRN_RECURRING_ERRORS = reader.GetInt32(reader.GetOrdinal("BRN_RECURRING_ERRORS")),
                            BRN_RESOLVED_ERRORS = reader.GetInt32(reader.GetOrdinal("BRN_RESOLVED_ERRORS"))
                        };
                        allKpis.Add(kpirow);
                    }
                }
                        /*
                            SqlCommand command = new SqlCommand("cmdm_kpi.prc_get_brn_kpi", connection);
                            command.CommandType = System.Data.CommandType.StoredProcedure;

                            command.Parameters.Add("var_p_branch_code", SqlDbType.VarChar).Value = BranchCode;
                            //command.Parameters.Add("var_p_result", SqlDbType.RefCursor);


                            SqlParameter cursor = command.Parameters.Add(
                                new SqlParameter
                                {
                                    ParameterName = "var_p_result",
                                    Direction = ParameterDirection.Output,
                                    SqlDbType = SqlDbType.RefCursor
                                }
                            );

                            command.CommandType = CommandType.StoredProcedure;
                            command.ExecuteNonQuery();
                            using (SqlDataReader reader = ((SqlRefCursor)cursor.Value).GetDataReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        BrnKpi kpirow = new BrnKpi
                                        {
                                            BANK_CUST_COUNT = reader.GetInt32(reader.GetOrdinal("BANK_CUST_COUNT")),
                                            BRN_CUST_COUNT = reader.GetInt32(reader.GetOrdinal("BRN_CUST_COUNT")),
                                            BRN_DQI = reader.GetDecimal(reader.GetOrdinal("BRN_DQI")),
                                            BRN_OPEN_EXCEPTIONS = reader.GetInt32(reader.GetOrdinal("BRN_OPEN_EXCEPTIONS")),
                                            BRN_PCT_CONTRIB = reader.GetDecimal(reader.GetOrdinal("BRN_PCT_CONTRIB")),
                                            BRN_RECURRING_ERRORS = reader.GetInt32(reader.GetOrdinal("BRN_RECURRING_ERRORS")),
                                            BRN_RESOLVED_ERRORS = reader.GetInt32(reader.GetOrdinal("BRN_RESOLVED_ERRORS"))
                                        };

                                        //kpirow.BANK_CUST_COUNT = reader.GetInt32(reader.GetOrdinal("BANK_CUST_COUNT"));
                                        //kpirow.BRN_CUST_COUNT = reader.GetInt32(reader.GetOrdinal("BRN_CUST_COUNT"));
                                        //kpirow.BRN_DQI = reader.GetDecimal(reader.GetOrdinal("BRN_DQI"));
                                        //kpirow.BRN_OPEN_EXCEPTIONS = reader.GetInt32(reader.GetOrdinal("BRN_OPEN_EXCEPTIONS"));
                                        //kpirow.BRN_PCT_CONTRIB = reader.GetDecimal(reader.GetOrdinal("BRN_PCT_CONTRIB"));
                                        //kpirow.BRN_RECURRING_ERRORS = reader.GetInt32(reader.GetOrdinal("BRN_RECURRING_ERRORS"));
                                        //kpirow.BRN_RESOLVED_ERRORS = reader.GetInt32(reader.GetOrdinal("BRN_RESOLVED_ERRORS"));
                                        allKpis.Add(kpirow);

                                        //Iterate the result set
                                    }
                                }

                            }

                        */
                        return allKpis;
            }
            #endregion

        }

        public List<BrnKpi> GetMultipleBrnKPI(DateTime trandate, string[] BranchCodes)
        {
            // decimal openingBal = 0;
            string connString = ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
            string schemaname = ConfigurationManager.AppSettings["SchemaName"].ToString();
            List<BrnKpi> allKpis = new List<BrnKpi>();
            #region New
            using (SqlConnection connection = new SqlConnection(connString))
            {
                string allBranches = "";
                foreach(string branch in BranchCodes)
                {
                    if (branch == BranchCodes.Last())
                        allBranches += "\'"+ branch +"\'";
                    else
                        allBranches += "\'"+ branch+ "\',";
                }
                //BrnKpi kpirow = new BrnKpi();                
                connection.Open();

                string sqlSelect = "SELECT  SUM(BRN_CUST_COUNT) AS BRN_CUST_COUNT, ROUND(AVG(BRN_DQI),2) AS BRN_DQI, SUM(BRN_OPEN_EXCEPTIONS) AS BRN_OPEN_EXCEPTIONS, " +
                "ROUND(AVG(BRN_PCT_CONTRIB),2) AS BRN_PCT_CONTRIB, SUM(BRN_RECURRING_ERRORS) AS BRN_RECURRING_ERRORS, SUM(BRN_RESOLVED_ERRORS) AS BRN_RESOLVED_ERRORS, " +
                "SUM(BANK_CUST_COUNT) AS BANK_CUST_COUNT, TIER " +
                "FROM " + schemaname + "." +  "cmdm_common_kpi WHERE BRANCH_CODE IN ("+allBranches+") " +
                "GROUP BY TIER ORDER BY TIER ASC";

                SqlCommand command = new SqlCommand(sqlSelect, connection);
                SqlDataReader rdr = command.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        BrnKpi kpirow = new BrnKpi
                        {
                            BANK_CUST_COUNT = rdr.GetInt32(rdr.GetOrdinal("BANK_CUST_COUNT")),
                            BRN_CUST_COUNT = rdr.GetInt32(rdr.GetOrdinal("BRN_CUST_COUNT")),
                            BRN_DQI = rdr.GetDecimal(rdr.GetOrdinal("BRN_DQI")),
                            BRN_OPEN_EXCEPTIONS = rdr.GetInt32(rdr.GetOrdinal("BRN_OPEN_EXCEPTIONS")),
                            BRN_PCT_CONTRIB = rdr.GetDecimal(rdr.GetOrdinal("BRN_PCT_CONTRIB")),
                            BRN_RECURRING_ERRORS = rdr.GetInt32(rdr.GetOrdinal("BRN_RECURRING_ERRORS")),
                            BRN_RESOLVED_ERRORS = rdr.GetInt32(rdr.GetOrdinal("BRN_RESOLVED_ERRORS"))
                        };
                        allKpis.Add(kpirow);
                    }
                }

                return allKpis;
            }
            #endregion

        }

        public List<CorpKpi> GetCorpKPI(DateTime trandate, string[] BranchCodes)
        {
            // decimal openingBal = 0;
            string connString = ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
            List<CorpKpi> allKpis = new List<CorpKpi>();
            #region Corp
            using (SqlConnection connection = new SqlConnection(connString))
            {
                string allBranches = "";
                foreach (string branch in BranchCodes)
                {
                    if (branch == BranchCodes.Last())
                        allBranches += "\'" + branch + "\'";
                    else
                        allBranches += "\'" + branch + "\',";
                }
                //BrnKpi kpirow = new BrnKpi();                
                connection.Open();

                string sqlSelect = "SELECT  SUM(BRN_CUST_COUNT) AS BRN_CUST_COUNT, ROUND(AVG(BRN_DQI),2) AS BRN_DQI, SUM(BRN_OPEN_EXCEPTIONS) AS BRN_OPEN_EXCEPTIONS, " +
                "ROUND(AVG(BRN_PCT_CONTRIB),2) AS BRN_PCT_CONTRIB, SUM(BRN_RECURRING_ERRORS) AS BRN_RECURRING_ERRORS, SUM(BRN_RESOLVED_ERRORS) AS BRN_RESOLVED_ERRORS, " +
                "SUM(BANK_CUST_COUNT) AS BANK_CUST_COUNT " +
                "FROM cmdm_corp_common_kpi WHERE BRANCH_CODE IN (" + allBranches + ") ";

                SqlCommand command = new SqlCommand(sqlSelect, connection);
                SqlDataReader rdr = command.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        CorpKpi kpirow = new CorpKpi
                        {
                            BANK_CUST_COUNT = rdr.GetInt32(rdr.GetOrdinal("BANK_CUST_COUNT")),
                            BRN_CUST_COUNT = rdr.GetInt32(rdr.GetOrdinal("BRN_CUST_COUNT")),
                            BRN_DQI = rdr.GetDecimal(rdr.GetOrdinal("BRN_DQI")),
                            BRN_OPEN_EXCEPTIONS = rdr.GetInt32(rdr.GetOrdinal("BRN_OPEN_EXCEPTIONS")),
                            BRN_PCT_CONTRIB = rdr.GetDecimal(rdr.GetOrdinal("BRN_PCT_CONTRIB")),
                            BRN_RECURRING_ERRORS = rdr.GetInt32(rdr.GetOrdinal("BRN_RECURRING_ERRORS")),
                            BRN_RESOLVED_ERRORS = rdr.GetInt32(rdr.GetOrdinal("BRN_RESOLVED_ERRORS"))
                        };
                        allKpis.Add(kpirow);
                    }
                }

                return allKpis;
            }
            #endregion Corp
        }
    }
}
