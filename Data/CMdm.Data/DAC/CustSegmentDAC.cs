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
    public class CustSegmentDAC
    {
        #region CustSegment
        /// <summary>
        /// Inserts a new row in the MdmDQQue table.
        /// </summary>
        /// <param name="mdmque">A MdmDQQue object.</param>
        /// <returns>An updated MdmDQQue object.</returns>

        public CustSegment InsertCustSegment(CustSegment mdmdque)
        {
            using (var db = new AppDbContext())
            {
                db.Set<CustSegment>().Add(mdmdque);
                db.SaveChanges();

                return mdmdque;
            }
        }

        /// <summary>
        /// Updates an existing row in the mdmdque table.
        /// </summary>
        /// <param name="mdmdque">A mdmdque entity object.</param>
        public void UpdateCustSegment(CustSegment mdmdque)
        {
            using (var db = new AppDbContext())
            {
                var entry = db.Entry<CustSegment>(mdmdque);

                // Re-attach the entity.
                entry.State = EntityState.Modified;

                db.SaveChanges();
            }
        }
        public virtual IList<CustSegment> SelectByIds(int[] recordIds)
        {
            if (recordIds == null || recordIds.Length == 0)
                return new List<CustSegment>();

            using (var db = new AppDbContext())
            {
                var query = from c in db.CustSegment
                            where recordIds.Contains(c.ID)
                            select c;
                var goldenrecords = query.ToList();
                //sort by passed identifiers
                var sortedCustomers = new List<CustSegment>();
                foreach (int id in recordIds)
                {
                    var goldenrecord = goldenrecords.Find(x => x.ID == id);
                    if (goldenrecord != null)
                        sortedCustomers.Add(goldenrecord);
                }
                return sortedCustomers;
            }

        }
        public CustSegment SelectCustSegmentById(int recordId)
        {
            using (var db = new AppDbContext())
            {
                return db.Set<CustSegment>().Find(recordId);
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
        public List<CustSegment> SelectCustSegment(string custid, string custtype, string accno, string fname, string mname, string lname, string branchCode, string reason,
            int startRowIndex, int maximumRows, string sortExpression)
        {
            using (var db = new AppDbContext())
            {
                var query = db.CustSegment.Select(q => q);

                if (!string.IsNullOrWhiteSpace(custid))
                    query = query.Where(v => v.ORGKEY.Contains(custid));
                if (!string.IsNullOrWhiteSpace(accno))
                    query = query.Where(v => v.ACCOUNT_NO.Contains(accno));
                if (!string.IsNullOrWhiteSpace(custtype) && custtype != "0")
                    query = query.Where(v => v.CUSTOMER_TYPE.ToUpper().Contains(custtype.ToUpper()));
                if (!string.IsNullOrWhiteSpace(fname))
                    query = query.Where(v => v.CUST_FIRST_NAME.ToUpper().Contains(fname.ToUpper()));
                if (!string.IsNullOrWhiteSpace(mname))
                    query = query.Where(v => v.CUST_MIDDLE_NAME.ToUpper().Contains(mname.ToUpper()));
                if (!string.IsNullOrWhiteSpace(lname))
                    query = query.Where(v => v.CUST_LAST_NAME.ToUpper().Contains(lname.ToUpper()));
                if (!string.IsNullOrWhiteSpace(reason) && reason != "0")
                    query = query.Where(v => v.REASON.ToUpper().Contains(reason.ToUpper()));
                if (!string.IsNullOrWhiteSpace(branchCode) && branchCode != "0")
                    query = query.Where(v => v.PRIMARY_SOL_ID.Contains(branchCode));
                // Append filters.
                //query = AppendFilters(query, name);

                // Sort and page.
                query = query.OrderByDescending(a => a.CUST_DOB);//    //OrderBy(a => a.CREATED_DATE)  //
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
        public int CountAccountOfficer(string name)
        {
            using (var db = new AppDbContext())
            {
                // Store the query.
                IQueryable<CustSegment> query = db.Set<CustSegment>();

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
        private static IQueryable<CustSegment> AppendFilters(IQueryable<CustSegment> query,
            string fname)
        {
            // Filter name.
            if (!string.IsNullOrWhiteSpace(fname))
                query = query.Where(v => v.CUST_FIRST_NAME.Contains(fname));
            //query = query.Where(l => l.Employee == employee);

            // Filter category.
            //if (category != null)
            //    query = query.Where(l => l.Category == category);

            //// Filter status.
            //if (status != null)
            //    query = query.Where(l => l.Status == status);
            return query;
        }
        #endregion CustSegment
        //
    }
}
