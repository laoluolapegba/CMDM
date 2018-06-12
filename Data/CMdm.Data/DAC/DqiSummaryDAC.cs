using CMdm.Entities.Domain.CustomModule.Fcmb;
using CMdm.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Data.DAC
{
    public class DqiSummaryDAC
    {
        #region Ctor
        //private AppDbContext db;// = new AppDbContext();
        #endregion
        #region DQISUmmary
        /// <summary>
        /// Inserts a new row in the MdmDQQue table.
        /// </summary>
        /// <param name="mdmque">A MdmDQQue object.</param>
        /// <returns>An updated MdmDQQue object.</returns>
        public DqiSummary InsertDqiSummary(DqiSummary mdmdque)
        {
            using (var db = new AppDbContext())
            {
                db.Set<DqiSummary>().Add(mdmdque);
                db.SaveChanges();

                return mdmdque;
            }
        }

        /// <summary>
        /// Updates an existing row in the mdmdque table.
        /// </summary>
        /// <param name="mdmdque">A mdmdque entity object.</param>
        public void UpdateDqiSummary(DqiSummary mdmdque)
        {
            using (var db = new AppDbContext())
            {
                var entry = db.Entry<DqiSummary>(mdmdque);

                // Re-attach the entity.
                entry.State = EntityState.Modified;

                db.SaveChanges();
            }
        }
        public virtual IList<DqiSummary> SelectByIds(int[] recordIds)
        {
            if (recordIds == null || recordIds.Length == 0)
                return new List<DqiSummary>();

            using (var db = new AppDbContext())
            {
                var query = from c in db.DqiSummary
                            where recordIds.Contains(c.ID)
                            select c;
                var goldenrecords = query.ToList();
                //sort by passed identifiers
                var sortedCustomers = new List<DqiSummary>();
                foreach (int id in recordIds)
                {
                    var goldenrecord = goldenrecords.Find(x => x.ID == id);
                    if (goldenrecord != null)
                        sortedCustomers.Add(goldenrecord);
                }
                return sortedCustomers;
            }

        }
        public DqiSummary SelectDqiSummaryById(int recordId)
        {
            using (var db = new AppDbContext())
            {
                return db.Set<DqiSummary>().Find(recordId);
            }
        }

        /// <summary>
        /// Conditionally retrieves one or more rows from the Leaves table with paging and a sort expression.
        /// </summary>
        /// <param name="maximumRows">The maximum number of rows to return.</param>
        /// <param name="startRowIndex">The starting row index.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="name">A name value.</param>
        /// <returns>A collection of  objects.</returns>		
        public List<DqiSummary> SelectDqiSummary(string branchCode, int startRowIndex, int maximumRows, string sortExpression)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                //IQueryable<MdmDQQue> query = db.Set<MdmDQQue>();
                var query = db.DqiSummary.Select(q => q);

                if (!string.IsNullOrWhiteSpace(branchCode) && branchCode != "0")
                    query = query.Where(v => v.BRANCH_CODE.Contains(branchCode));
                // Append filters.
                //query = AppendFilters(query, name);

                // Sort and page.
                query = query.OrderByDescending(a => a.DAT_LAST_RUN);//    //OrderBy(a => a.CREATED_DATE)  //
                                                                 //  .Skip(startRowIndex).Take(maximumRows);

                // Return result.
                return query.ToList();
            }
        }

        /// <summary>
        /// Returns a count based on the condition.
        /// </summary>
        /// <param name="name">A employee value.</param>
        /// <returns>The record count.</returns>		
        public int CountDqiSummary(string name)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                IQueryable<DqiSummary> query = db.Set<DqiSummary>();

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
        private static IQueryable<DqiSummary> AppendFilters(IQueryable<DqiSummary> query,
            string name)
        {
            // Filter name.
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(v => v.BRANCH_CODE.Contains(name));
            //query = query.Where(l => l.Employee == employee);

            // Filter category.
            //if (category != null)
            //    query = query.Where(l => l.Category == category);

            //// Filter status.
            //if (status != null)
            //    query = query.Where(l => l.Status == status);
            return query;
        }
        #endregion DQISUmmary
        //
    }
}
