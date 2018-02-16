using CMdm.Data;
using Elmah;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CMdm.UI.Web.BLL
{
    public class DQQueBiz
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        private OracleConnection conn = new OracleConnection(connString);
        public AppDbContext _db = new AppDbContext();

        public int GetDQQuesCountbyBrn(string BranchCode)
        {
            //var entity = from n in _db.MdmDQQues
            //             where n.BRANCH_CODE == BranchCode
            //             select new { n. };
            //int count = entity.Count();
            int count = 0;
            return count = _db.MdmDQQues.Where(a=>a.BRANCH_CODE == BranchCode).Count();
        }
    }
}