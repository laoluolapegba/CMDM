using CMdm.Data.Rbac;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Data.DAC
{
    public class UserAdminDAC
    {
        #region Ctor
        //private AppDbContext db;// = new AppDbContext();
        #endregion
        /// <summary>
        /// Inserts a new row in the CM_USER_PROFILE table.
        /// </summary>
        /// <param name="mdmque">A CM_USER_PROFILE object.</param>
        /// <returns>An updated CM_USER_PROFILE object.</returns>
        public CM_USER_PROFILE Insert(CM_USER_PROFILE userprofile)
        {
            using (var db = new AppDbContext())
            {
                db.Set<CM_USER_PROFILE>().Add(userprofile);
                db.SaveChanges();

                return userprofile;
            }
        }

        /// <summary>
        /// Updates an existing row in the userprofile table.
        /// </summary>
        /// <param name="userprofile">A userprofile entity object.</param>
        public void Update(CM_USER_PROFILE userprofile)
        {
            using (var db = new AppDbContext())
            {
                var entry = db.Entry<CM_USER_PROFILE>(userprofile);

                // Re-attach the entity.
                entry.State = EntityState.Modified;

                db.SaveChanges();
            }
        }
        /// <summary>
        /// Returns a row from the CM_USER_PROFILE table.
        /// </summary>
        /// <param name="recordId">A recordId value.</param>
        /// <returns>A DQQUe object with data populated from the database.</returns>
        public CM_USER_PROFILE SelectById(int recordId)
        {
            using (var db = new AppDbContext())
            {
                return db.Set<CM_USER_PROFILE>().Find(recordId);
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
        public List<CM_USER_PROFILE> Select( int startRowIndex, int maximumRows, string sortExpression, int[] userRoleIds = null, string email = null, string username = null,
            string firstName = null, string lastName = null)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                //IQueryable<CM_USER_PROFILE> query = db.Set<CM_USER_PROFILE>();
                var query = db.CM_USER_PROFILE.Select(q => q).Include(a => a.CM_USER_ROLES).Include(b=>b.CM_BRANCH);
                if (userRoleIds != null && userRoleIds.Length > 0)
                    query = query.Where(u => userRoleIds.Contains(u.ROLE_ID));
                //query = query.Where(c => c.CM_USER_ROLES.Select(cr => cr.ROLE_ID).Intersect(userRoleIds).Any());
                if (!String.IsNullOrWhiteSpace(email))
                    query = query.Where(c => c.EMAIL_ADDRESS.ToUpper().Contains(email.ToUpper()));
                if (!String.IsNullOrWhiteSpace(username))
                    query = query.Where(c => c.USER_ID.ToUpper().Contains(username.ToUpper()));
                if (!String.IsNullOrWhiteSpace(firstName))
                    query = query.Where(c => c.FIRSTNAME.ToUpper().Contains(firstName.ToUpper()));
                if (!String.IsNullOrWhiteSpace(lastName))
                    query = query.Where(c => c.LASTNAME.ToUpper().Contains(lastName.ToUpper()));
                // Append filters.
                //query = AppendFilters(query, name);

                // Sort and page.
                query = query.OrderBy(a => a.CREATED_DATE);//    //OrderBy(a => a.CREATED_DATE)  //
                        //.Skip(startRowIndex).Take(maximumRows);

                // Return result.
                return query.ToList();
            }
        }
        public List<CM_USER_ROLES> SelectRoles(string name, int startRowIndex, int maximumRows, string sortExpression)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                
                var query = db.CM_USER_ROLES.Select(q => q);

                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(v => v.ROLE_NAME.Contains(name));
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
                IQueryable<CM_USER_PROFILE> query = db.Set<CM_USER_PROFILE>();

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
        private static IQueryable<CM_USER_PROFILE> AppendFilters(IQueryable<CM_USER_PROFILE> query,
            string name)
        {
            // Filter name.
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(v => v.USER_ID.Contains(name));
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
