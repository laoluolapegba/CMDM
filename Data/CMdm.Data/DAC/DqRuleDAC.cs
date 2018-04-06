using CMdm.Entities.Domain.Dqi;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Data.DAC
{
    public class DqRuleDAC
    {
        #region Ctor
        //private AppDbContext db;// = new AppDbContext();
        #endregion
        /// <summary>
        /// Inserts a new row in the MdmDQQue table.
        /// </summary>
        /// <param name="leave">A MdmDQQue object.</param>
        /// <returns>An updated MdmDQQue object.</returns>
        public MdmDqRule Insert(MdmDqRule mdmdque)
        {
            using (var db = new AppDbContext())
            {
                db.Set<MdmDqRule>().Add(mdmdque);
                db.SaveChanges();

                return mdmdque;
            }
        }

        /// <summary>
        /// Updates an existing row in the mdmdque table.
        /// </summary>
        /// <param name="mdmdque">A mdmdque entity object.</param>
        public void Update(MdmDqRule mdmdque)
        {
            using (var db = new AppDbContext())
            {
                var entry = db.Entry<MdmDqRule>(mdmdque);

                // Re-attach the entity.
                entry.State = EntityState.Modified;
                
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Returns a row from the MdmDqRule table.
        /// </summary>
        /// <param name="recordId">A recordId value.</param>
        /// <returns>A DQQUe object with data populated from the database.</returns>
        public MdmDqRule SelectById(int recordId)
        {
            using (var db = new AppDbContext())
            {
                return db.Set<MdmDqRule>().Find(recordId);
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
        public List<MdmDqRule> Select(string name, int startRowIndex, int maximumRows, string sortExpression, int? dimensionId = null)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                //IQueryable<MdmDqRule> query = db.Set<MdmDqRule>();
                var query = db.MdmDqRules.Select(q => q).Include(m => m.MdmAggrDimensions).Include(m => m.MdmDQDataSources).Include(m => m.MdmDQPriorities).Include(m => m.MdmDqRunSchedules);

                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(v => v.RULE_NAME.ToUpper().Contains(name.ToUpper()));
                // Append filters.
                //query = AppendFilters(query, name);
                if(dimensionId.HasValue && dimensionId.Value > 0)
                {
                    int dimension = (int)dimensionId.Value;
                    query = query.Where(d => d.DIMENSION == dimension);
                }
                // Sort and page.
                query = query.OrderBy(a => a.LAST_RUN);
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
                IQueryable<MdmDqRule> query = db.Set<MdmDqRule>();

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
        private static IQueryable<MdmDqRule> AppendFilters(IQueryable<MdmDqRule> query,
            string name)
        {
            // Filter name.
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(v => v.RULE_NAME.Contains(name));
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
