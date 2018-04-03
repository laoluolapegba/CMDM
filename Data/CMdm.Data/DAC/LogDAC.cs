using CMdm.Entities.Domain.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Data.DAC
{
    public class LogDAC
    {
        public Log Insert(Log log)
        {
            using (var db = new AppDbContext())
            {
                db.Set<Log>().Add(log);
                db.SaveChanges();

                return log;
            }
        }
        public List<Log> Select(DateTime? fromUtc = null, DateTime? toUtc = null,
            string message = "", LogLevel? logLevel = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                var query = db.LOG.Select(q => q);
                if (fromUtc.HasValue)
                    query = query.Where(l => fromUtc.Value <= l.CREATEDDATE);
                if (toUtc.HasValue)
                    query = query.Where(l => toUtc.Value >= l.CREATEDDATE);
                if (logLevel.HasValue)
                {
                    var logLevelId = (int)logLevel.Value;
                    query = query.Where(l => logLevelId == l.LOGLEVELID);
                }
                if (!String.IsNullOrEmpty(message))
                    query = query.Where(l => l.SHORTMESSAGE.Contains(message) || l.FULLMESSAGE.Contains(message));
                query = query.OrderByDescending(l => l.CREATEDDATE);

                // Return result.
                return query.ToList();
            }
        }
        public Log SelectById(int recordId)
        {
            using (var db = new AppDbContext())
            {
                return db.Set<Log>().Find(recordId);
            }
        }
        public virtual IList<Log> SelectByIds(int[] recordIds)
        {
            if (recordIds == null || recordIds.Length == 0)
                return new List<Log>();

            using (var db = new AppDbContext())
            {
                var query = from c in db.LOG
                            where recordIds.Contains(c.ID)
                            select c;
                var records = query.ToList();
                //sort by passed identifiers
                var sortedCustomers = new List<Log>();
                foreach (int id in recordIds)
                {
                    var record = records.Find(x => x.ID == id);
                    if (record != null)
                        sortedCustomers.Add(record);
                }
                return sortedCustomers;
            }

        }
    }
}
