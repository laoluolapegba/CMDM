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
    public class EmailPhoneDAC
    {
        #region Ctor
        //private AppDbContext db;// = new AppDbContext();
        #endregion
        #region EmailPhone
        /// <summary>
        /// Inserts a new row in the MdmDQQue table.
        /// </summary>
        /// <param name="mdmque">A MdmDQQue object.</param>
        /// <returns>An updated MdmDQQue object.</returns>
        public EmailPhone InsertEmailPhone(EmailPhone mdmdque)
        {
            using (var db = new AppDbContext())
            {
                db.Set<EmailPhone>().Add(mdmdque);
                db.SaveChanges();

                return mdmdque;
            }
        }

        /// <summary>
        /// Updates an existing row in the mdmdque table.
        /// </summary>
        /// <param name="mdmdque">A mdmdque entity object.</param>
        public void UpdateEmailPhone(EmailPhone mdmdque)
        {
            using (var db = new AppDbContext())
            {
                var entry = db.Entry<EmailPhone>(mdmdque);

                // Re-attach the entity.
                entry.State = EntityState.Modified;

                db.SaveChanges();
            }
        }
        public virtual IList<EmailPhone> SelectByIds(int[] recordIds)
        {
            if (recordIds == null || recordIds.Length == 0)
                return new List<EmailPhone>();

            using (var db = new AppDbContext())
            {
                var query = from c in db.EmailPhone
                            where recordIds.Contains(c.ID)
                            select c;
                var goldenrecords = query.ToList();
                //sort by passed identifiers
                var sortedCustomers = new List<EmailPhone>();
                foreach (int id in recordIds)
                {
                    var goldenrecord = goldenrecords.Find(x => x.ID == id);
                    if (goldenrecord != null)
                        sortedCustomers.Add(goldenrecord);
                }
                return sortedCustomers;
            }

        }
        public EmailPhone SelectEmailPhoneById(int recordId)
        {
            using (var db = new AppDbContext())
            {
                return db.Set<EmailPhone>().Find(recordId);
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
        public List<EmailPhone> SelectEmailPhone(string custid, string phone, string email, string firstname, string midname, string lastname, string branchCode, int startRowIndex, int maximumRows, string sortExpression)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                //IQueryable<MdmDQQue> query = db.Set<MdmDQQue>();
                var query = db.EmailPhone.Select(q => q);

                if (!string.IsNullOrWhiteSpace(custid))
                    query = query.Where(v => v.ORGKEY.Contains(custid));

                if (!string.IsNullOrWhiteSpace(phone))
                    query = query.Where(v => v.PREFERREDPHONE.Contains(phone));

                if (!string.IsNullOrWhiteSpace(email))
                    query = query.Where(v => v.EMAIL.ToUpper().Contains(email.ToUpper()));

                if (!string.IsNullOrWhiteSpace(firstname))
                    query = query.Where(v => v.CUST_FIRST_NAME.ToUpper().Contains(firstname.ToUpper()));

                if (!string.IsNullOrWhiteSpace(midname))
                    query = query.Where(v => v.CUST_MIDDLE_NAME.ToUpper().Contains(midname.ToUpper()));

                if (!string.IsNullOrWhiteSpace(lastname))
                    query = query.Where(v => v.CUST_LAST_NAME.ToUpper().Contains(lastname.ToUpper()));

                if (!string.IsNullOrWhiteSpace(branchCode) && branchCode != "0")
                    query = query.Where(v => v.BRANCH_CODE.Contains(branchCode));
                // Append filters.
                //query = AppendFilters(query, name);

                // Sort and page.
                query = query.OrderBy(a => a.DUPLICATE_ID);//    //OrderBy(a => a.CREATED_DATE)  //
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
        public int CountEmailPhone(string name)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                IQueryable<EmailPhone> query = db.Set<EmailPhone>();

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
        private static IQueryable<EmailPhone> AppendFilters(IQueryable<EmailPhone> query,
            string name)
        {
            // Filter name.
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(v => v.ORGKEY.Contains(name));
            //query = query.Where(l => l.Employee == employee);

            // Filter category.
            //if (category != null)
            //    query = query.Where(l => l.Category == category);

            //// Filter status.
            //if (status != null)
            //    query = query.Where(l => l.Status == status);
            return query;
        }
        #endregion EmailPhone
        //
    }
}
