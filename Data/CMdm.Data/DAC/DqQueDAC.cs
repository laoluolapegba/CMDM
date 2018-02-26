using CMdm.Entities.Domain.Dqi;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Data.DAC
{
    public class DqQueDAC
    {
        #region Ctor
        //private AppDbContext db;// = new AppDbContext();
        #endregion
        /// <summary>
        /// Inserts a new row in the MdmDQQue table.
        /// </summary>
        /// <param name="mdmque">A MdmDQQue object.</param>
        /// <returns>An updated MdmDQQue object.</returns>
        public MdmDQQue Insert(MdmDQQue mdmdque)
        {
            using (var db = new AppDbContext())
            {
                db.Set<MdmDQQue>().Add(mdmdque);
                db.SaveChanges();

                return mdmdque;
            }
        }

        /// <summary>
        /// Updates an existing row in the mdmdque table.
        /// </summary>
        /// <param name="mdmdque">A mdmdque entity object.</param>
        public void Update(MdmDQQue mdmdque)
        {
            using (var db = new AppDbContext())
            {
                var entry = db.Entry<MdmDQQue>(mdmdque);

                // Re-attach the entity.
                entry.State = EntityState.Modified;
                
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Returns a row from the MdmDQQue table.
        /// </summary>
        /// <param name="recordId">A recordId value.</param>
        /// <returns>A DQQUe object with data populated from the database.</returns>
        public MdmDQQue SelectById(int recordId)
        {
            using (var db = new AppDbContext())
            {
                return db.Set<MdmDQQue>().Find(recordId);
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
        public List<MdmDQQue> Select(string name, int startRowIndex, int maximumRows, string sortExpression)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                //IQueryable<MdmDQQue> query = db.Set<MdmDQQue>();
                var query = db.MdmDQQues.Select(q => q).Include(a => a.MdmDQPriorities);

                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(v => v.ERROR_DESC.Contains(name));
                // Append filters.
                //query = AppendFilters(query, name);

                // Sort and page.
                query = query.OrderBy(a => a.CREATED_DATE);//    //OrderBy(a => a.CREATED_DATE)  //
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
        public int Count(string name)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                IQueryable<MdmDQQue> query = db.Set<MdmDQQue>();

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
        private static IQueryable<MdmDQQue> AppendFilters(IQueryable<MdmDQQue> query,
            string name)
        {
            // Filter name.
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(v => v.ERROR_DESC.Contains(name));
            //query = query.Where(l => l.Employee == employee);

            // Filter category.
            //if (category != null)
            //    query = query.Where(l => l.Category == category);

            //// Filter status.
            //if (status != null)
            //    query = query.Where(l => l.Status == status);
            return query;
        }
        //
        public List<MdmDqRunException> SelectBrnIssues(string name,  int startRowIndex, int maximumRows, string sortExpression, int? ruleId = null, string BranchId = null, IssueStatus? status = null , int? priority = null)
        {
            //DateTime? createdOnFrom = null,            DateTime? createdOnTo = null,
            using (var db = new AppDbContext())
            {
                // Store the query.
                //IQueryable<MdmDQQue> query = db.Set<MdmDQQue>();
                var query = db.MdmDqRunExceptions.Select(q => q).Include(a=>a.MdmDQPriorities).Include(a=>a.MdmDQQueStatuses);

                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(v => v.RULE_NAME.Contains(name));
                //if (createdOnFrom.HasValue)
                //    query = query.Where(al => createdOnFrom.Value <= al.RUN_DATE);
                //if (createdOnTo.HasValue)
                //    query = query.Where(al => createdOnTo.Value >= al.RUN_DATE);
                if (ruleId.HasValue && ruleId > 0)
                {
                    int rule = (int)ruleId.Value;
                    query = query.Where(d => d.RULE_ID == rule);
                }
                if (!string.IsNullOrWhiteSpace(BranchId))
                {
                    //string brnId = (string)BranchId.Value;
                    query = query.Where(d => d.BRANCH_CODE == BranchId);
                }
                if (status.HasValue) // && status>0
                {
                    int stat = (int)status.Value;
                    query = query.Where(d => d.ISSUE_STATUS == stat);
                }
                if (priority.HasValue && priority > 0)
                {
                    int prior = (int)priority.Value;
                    query = query.Where(d => d.ISSUE_PRIORITY == prior);
                    
                }
                // Append filters.
                //query = AppendFilters(query, name);

                // Sort and page.
                query = query.OrderBy(a => a.RUN_DATE); //    //OrderBy(a => a.CREATED_DATE)  //
                        //.Skip(startRowIndex).Take(maximumRows);

                // Return result.
                return query.ToList();
            }
        }
    }
}
