using CMdm.Data;
using Elmah;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace CMdm.UI.Web.BLL
{
    public class DataCatalogBiz
    {
        public AppDbContext DQIdb = new AppDbContext();
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        private OracleConnection conn = new OracleConnection(connString);
        public bool IsCatalogExist(string Catalogname)
        {
            //bool exists = true;
            var entity = from n in DQIdb.MdmDqiParams
                         where n.TABLE_NAMES == Catalogname
                        select new { n.TABLE_NAMES };
            int count = entity.Count();
            return count > 0;
        }

    }
}