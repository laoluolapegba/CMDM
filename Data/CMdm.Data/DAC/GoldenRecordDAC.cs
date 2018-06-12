using CMdm.Entities.Domain.Dqi;
using CMdm.Entities.Domain.GoldenRecord;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Data.DAC
{
    public class GoldenRecordDAC
    {
        #region Ctor
        //private AppDbContext db;// = new AppDbContext();
        #endregion
        /// <summary>
        /// Inserts a new row in the MdmDQQue table.
        /// </summary>
        /// <param name="que">A MdmDQQue object.</param>
        /// <returns>An updated MdmDQQue object.</returns>
        public CdmaGoldenRecord Insert(CdmaGoldenRecord mdmdque)
        {
            using (var db = new AppDbContext())
            {
                db.Set<CdmaGoldenRecord>().Add(mdmdque);
                db.SaveChanges();

                return mdmdque;
            }
        }

        /// <summary>
        /// Updates an existing row in the goldenrecords table.
        /// </summary>
        /// <param name="mdmdque">A goldenrecord entity object.</param>
        public void Update(CdmaGoldenRecord mdmdque)
        {
            using (var db = new AppDbContext())
            {
                var entry = db.Entry<CdmaGoldenRecord>(mdmdque);

                // Re-attach the entity.
                entry.State = EntityState.Modified;
                
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Get goldenrecords by identifiers
        /// </summary>
        /// <param name="recordIds">goldenrecord identifiers</param>
        /// <returns>Customers</returns>
        public virtual IList<CdmaGoldenRecord> SelectByIds(int[] recordIds)
        {
            if (recordIds == null || recordIds.Length == 0)
                return new List<CdmaGoldenRecord>();

            using (var db = new AppDbContext())
            {
                var query = from c in db.CdmaGoldenRecords
                            where recordIds.Contains(c.RECORD_ID)
                            select c;
                var goldenrecords = query.ToList();
                //sort by passed identifiers
                var sortedCustomers = new List<CdmaGoldenRecord>();
                foreach (int id in recordIds)
                {
                    var goldenrecord = goldenrecords.Find(x => x.RECORD_ID == id);
                    if (goldenrecord != null)
                        sortedCustomers.Add(goldenrecord);
                }
                return sortedCustomers;
            }
            
        }
        
        /// <summary>
        /// Returns a row from the Goldenrecord table.
        /// </summary>
        /// <param name="recordId">A recordId value.</param>
        /// <returns>A Record object with data populated from the database.</returns>
        public CdmaGoldenRecord SelectById(int recordId)
        {
            using (var db = new AppDbContext())
            {
                return db.Set<CdmaGoldenRecord>().Find(recordId);
            }
        }

        /// <summary>
        /// Conditionally retrieves one or more rows from the Leaves table with paging and a sort expression.
        /// </summary>
        /// <param name="maximumRows">The maximum number of rows to return.</param>
        /// <param name="startRowIndex">The starting row index.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="name">A name value.</param>
        /// <returns>A collection of Leave objects.</returns>		
        public List<CdmaGoldenRecord> Select(string name, string custid, string email, string accno, string bvn,
            int startRowIndex, int maximumRows, string sortExpression, int? GoldenrecordId = null, string BranchId = null)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                //IQueryable<MdmDqRule> query = db.Set<MdmDqRule>();
                var query = db.CdmaGoldenRecords.Where(a=>a.RECORD_STATUS == "N").Select(q => q); //.Include(m => m.MdmAggrDimensions).Include(m => m.MdmCatalog).Include(m => m.MdmRegex).Include(m => m.MdmAggrDimensions).Include(m=>m.MDM_WEIGHTS);

                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(v => v.FULL_NAME.ToUpper().Contains(name.ToUpper()));
                if (!string.IsNullOrWhiteSpace(accno))
                    query = query.Where(v => v.ACCOUNT_NO.Contains(accno));
                if (!string.IsNullOrWhiteSpace(bvn))
                    query = query.Where(v => v.BVN.Contains(bvn));
                if (!string.IsNullOrWhiteSpace(custid))
                    query = query.Where(v => v.CUSTOMER_NO.Contains(custid));
                if (!string.IsNullOrWhiteSpace(email))
                    query = query.Where(v => v.EMAIL.ToUpper().Contains(email.ToUpper()));
                //if (createdOnFrom.HasValue)
                //    query = query.Where(al => createdOnFrom.Value <= al.RUN_DATE);
                //if (createdOnTo.HasValue)
                //    query = query.Where(al => createdOnTo.Value >= al.RUN_DATE);
                if (!string.IsNullOrWhiteSpace(BranchId) && BranchId != "0")
                    query = query.Where(v => v.BRANCH_CODE == BranchId);
                if (GoldenrecordId.HasValue && GoldenrecordId > 0)
                {
                    int rec = (int)GoldenrecordId.Value;
                    query = query.Where(d => d.GOLDEN_RECORD == rec);
                }
               

                // Sort and page.
                query = query.OrderBy(a => a.GOLDEN_RECORD);//    //OrderBy(a => a.CREATED_DATE)  //
                        //.Skip(startRowIndex).Take(maximumRows);

                // Return result.
                return query.ToList();
            }
        }

        /// <summary>
        /// Returns a count based on the condition.
        /// </summary>
        /// <param name="name">A employee value.</param>
        /// <returns>The record count.</returns>		
        public int Count(string name)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                IQueryable<CdmaGoldenRecord> query = db.Set<CdmaGoldenRecord>();

                // Append filters.
                query = AppendFilters(query, name);

                // Return result.
                return query.Count();
            }
        }

        /// <summary>
        /// Conditionally appends filters to the query.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <param name="name">The name to filter by.</param>
        /// <returns>A query object.</returns>
        private static IQueryable<CdmaGoldenRecord> AppendFilters(IQueryable<CdmaGoldenRecord> query,
            string name)
        {
            // Filter name.
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(v => v.FULL_NAME.Contains(name));
            //query = query.Where(l => l.Employee == employee);

            // Filter category.
            //if (category != null)
            //    query = query.Where(l => l.Category == category);

            //// Filter status.
            //if (status != null)
            //    query = query.Where(l => l.Status == status);
            return query;
        }
    }
}
