using CMdm.Data.Rbac;
using CMdm.Entities.Domain.Kpi;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using (OracleConnection connection = new OracleConnection(connString))
            {
                //BrnKpi kpirow = new BrnKpi();                
                    connection.Open();

                    OracleCommand command = new OracleCommand("cmdm_kpi.prc_get_brn_kpi", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add("var_p_branch_code", OracleDbType.Varchar2).Value = BranchCode;
                    //command.Parameters.Add("var_p_result", OracleDbType.RefCursor);


                    OracleParameter cursor = command.Parameters.Add(
                        new OracleParameter
                        {
                            ParameterName = "var_p_result",
                            Direction = ParameterDirection.Output,
                            OracleDbType = OracleDbType.RefCursor
                        }
                    );

                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    using (OracleDataReader reader = ((OracleRefCursor)cursor.Value).GetDataReader())
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
                
                
                return allKpis;
            }
            #endregion

        }

        public List<BrnKpi> GetMultipleBrnKPI(DateTime trandate, string[] BranchCodes)
        {
            // decimal openingBal = 0;
            string connString = ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
            List<BrnKpi> allKpis = new List<BrnKpi>();
            #region New
            using (OracleConnection connection = new OracleConnection(connString))
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
                "FROM cmdm_common_kpi WHERE BRANCH_CODE IN ("+allBranches+") " +
                "GROUP BY TIER ORDER BY TIER ASC";

                OracleCommand command = new OracleCommand(sqlSelect, connection);
                OracleDataReader rdr = command.ExecuteReader();
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
            using (OracleConnection connection = new OracleConnection(connString))
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

                OracleCommand command = new OracleCommand(sqlSelect, connection);
                OracleDataReader rdr = command.ExecuteReader();
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
