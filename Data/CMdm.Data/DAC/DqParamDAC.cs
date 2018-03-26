using CMdm.Entities.Domain.Dqi;
using CMdm.Entities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Data.DAC
{
    public class DqParamDAC
    {
        #region Ctor
        //private AppDbContext db;// = new AppDbContext();
        #endregion
        /// <summary>
        /// Inserts a new row in the MdmDQQue table.
        /// </summary>
        /// <param name="que">A MdmDQQue object.</param>
        /// <returns>An updated MdmDQQue object.</returns>
        public MdmEntityDetails Insert(MdmEntityDetails mdmdque)
        {
            using (var db = new AppDbContext())
            {
                db.Set<MdmEntityDetails>().Add(mdmdque);
                db.SaveChanges();

                return mdmdque;
            }
        }

        /// <summary>
        /// Updates an existing row in the mdmdque table.
        /// </summary>
        /// <param name="mdmdque">A mdmdque entity object.</param>
        public void Update(MdmEntityDetails mdmdque)
        {
            using (var db = new AppDbContext())
            {
                var entry = db.Entry<MdmEntityDetails>(mdmdque);

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
        public MdmEntityDetails SelectById(int recordId)
        {
            using (var db = new AppDbContext())
            {
                return db.Set<MdmEntityDetails>().Find(recordId);
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
        public List<MdmEntityDetails> Select(string name, int startRowIndex, int maximumRows, string sortExpression, int? dimId = null, int? entityId = null, int? catalogId =null)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                //IQueryable<MdmDqRule> query = db.Set<MdmDqRule>();
                var query = db.EntityDetails.Select(q => q).Include(m => m.MdmAggrDimensions).Include(m => m.MdmCatalog).Include(m => m.MdmRegex).Include(m => m.MdmAggrDimensions).Include(m=>m.MDM_WEIGHTS);

                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(v => v.ENTITY_COL_NAME.ToUpper().Contains(name.ToUpper()));
                //if (createdOnFrom.HasValue)
                //    query = query.Where(al => createdOnFrom.Value <= al.RUN_DATE);
                //if (createdOnTo.HasValue)
                //    query = query.Where(al => createdOnTo.Value >= al.RUN_DATE);
                if (dimId.HasValue && dimId > 0)
                {
                    int rule = (int)dimId.Value;
                    query = query.Where(d => d.DQ_DIMENSION == rule);
                }
                if (entityId.HasValue && entityId > 0)
                {
                    int brnId = (int)entityId.Value;
                    query = query.Where(d => d.ENTITY_ID == brnId);
                }
                if (catalogId.HasValue && catalogId > 0)
                {
                    int stat = (int)catalogId.Value;
                    query = query.Where(d => d.CATALOG_ID == stat);
                }

                // Append filters.
                //query = AppendFilters(query, name);

                // Sort and page.
                query = query.OrderBy(a => a.CREATED_DATE);//    //OrderBy(a => a.CREATED_DATE)  //
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
                IQueryable<MdmEntityDetails> query = db.Set<MdmEntityDetails>();

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
        private static IQueryable<MdmEntityDetails> AppendFilters(IQueryable<MdmEntityDetails> query,
            string name)
        {
            // Filter name.
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(v => v.ENTITY_COL_NAME.Contains(name));
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
