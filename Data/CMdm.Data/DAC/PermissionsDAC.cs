using CMdm.Data.Rbac;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Data.DAC
{
    public class PermissionsDAC
    {
        #region Ctor
        //private AppDbContext db;// = new AppDbContext();
        #endregion
        /// <summary>
        /// Inserts a new row in the CM_PERMISSIONS table.
        /// </summary>
        /// <param name="mdmque">A CM_PERMISSIONS object.</param>
        /// <returns>An updated CM_PERMISSIONS object.</returns>
        public CM_PERMISSIONS Insert(CM_PERMISSIONS permission)
        {
            using (var db = new AppDbContext())
            {
                db.Set<CM_PERMISSIONS>().Add(permission);
                db.SaveChanges();

                return permission;
            }
        }

        /// <summary>
        /// Updates an existing row in the userprofile table.
        /// </summary>
        /// <param name="permission">A userprofile entity object.</param>
        public void Update(CM_PERMISSIONS permission)
        {
            using (var db = new AppDbContext())
            {
                var entry = db.Entry<CM_PERMISSIONS>(permission);

                // Re-attach the entity.
                entry.State = EntityState.Modified;

                db.SaveChanges();
            }
        }
        /// <summary>
        /// Returns a row from the PERMISSION table.
        /// </summary>
        /// <param name="recordId">A recordId value.</param>
        /// <returns>A Permission object with data populated from the database.</returns>
        public CM_PERMISSIONS SelectById(int recordId)
        {
            using (var db = new AppDbContext())
            {
                //db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                //db.Database.Log = Console.Write;
                return db.Set<CM_PERMISSIONS>().Find(recordId);

                //return db.CM_PERMISSIONS.Select(q => q).Include(a => a.CM_ROLE_PERM_XREF)  -- table or view does not exist
                //.Include(a=>a.CM_USER_ROLES).FirstOrDefault(a=>a.PERMISSION_ID == recordId); 

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
        public List<CM_PERMISSIONS> Select( int startRowIndex, int maximumRows, string sortExpression, string permdesc = null)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                //IQueryable<CM_USER_PROFILE> query = db.Set<CM_USER_PROFILE>();
                var query = db.CM_PERMISSIONS.Select(q => q); //.Include(a => a.CM_USER_ROLES); //.Include(a => a.CM_USER_ROLES).Include(b=>b.CM_BRANCH);
                    //query = query.Where(u => userRoleIds.Contains(u.ROLE_ID));
                //query = query.Where(c => c.CM_USER_ROLES.Select(cr => cr.ROLE_ID).Intersect(userRoleIds).Any());
                if (!String.IsNullOrWhiteSpace(permdesc))
                    query = query.Where(c => c.PERMISSIONDESCRIPTION.ToUpper().Contains(permdesc.ToUpper()));
                // Append filters.
                //query = AppendFilters(query, name);

                // Sort and page.
                query = query.OrderBy(a => a.PERMISSIONDESCRIPTION);//    //OrderBy(a => a.CREATED_DATE)  //
                        //.Skip(startRowIndex).Take(maximumRows);

                // Return result.
                return query.ToList();
            }
        }
        public List<CM_PERMISSIONS> SelectRoles(string name, int startRowIndex, int maximumRows, string sortExpression)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                
                var query = db.CM_PERMISSIONS.Select(q => q);

                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(v => v.PERMISSIONDESCRIPTION.Contains(name));
                // Append filters.
                //query = AppendFilters(query, name);

                // Sort and page.
                query = query.OrderBy(a => a.PERMISSIONDESCRIPTION);//    //OrderBy(a => a.CREATED_DATE)  //
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
                IQueryable<CM_PERMISSIONS> query = db.Set<CM_PERMISSIONS>();

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
        private static IQueryable<CM_PERMISSIONS> AppendFilters(IQueryable<CM_PERMISSIONS> query,
            string name)
        {
            // Filter name.
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(v => v.PERMISSIONDESCRIPTION.Contains(name));
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
       
    }
}
