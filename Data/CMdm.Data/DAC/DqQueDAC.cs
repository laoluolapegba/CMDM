using CMdm.Entities.Domain.Dqi;
using CMdm.Entities.ViewModels;
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

        public void UpdateExceptionQue(MdmDqRunException mdmdque)
        {
            using (var db = new AppDbContext())
            {
                var entry = db.Entry<MdmDqRunException>(mdmdque);

                // Re-attach the entity.
                entry.State = EntityState.Modified;

                db.SaveChanges();
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
        /// /// <summary>
        /// Get customers by identifiers
        /// </summary>
        /// <param name="customerIds">Customer identifiers</param>
        /// <returns>Customers</returns>
        public virtual IList<MdmDqRunException> SelectByIds(int[] recordIds)
        {
            if (recordIds == null || recordIds.Length == 0)
                return new List<MdmDqRunException>();

            using (var db = new AppDbContext())
            {
                var query = from c in db.MdmDqRunExceptions
                            where recordIds.Contains(c.EXCEPTION_ID)
                            select c;
                var goldenrecords = query.ToList();
                //sort by passed identifiers
                var sortedCustomers = new List<MdmDqRunException>();
                foreach (int id in recordIds)
                {
                    var goldenrecord = goldenrecords.Find(x => x.EXCEPTION_ID == id);
                    if (goldenrecord != null)
                        sortedCustomers.Add(goldenrecord);
                }
                return sortedCustomers;
            }

        }
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
                    query = query.Where(v => v.DQ_PROCESS_NAME.ToUpper().Contains(name.ToUpper()));
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
        public List<MdmDqRunException> SelectBrnIssues(string name,  int startRowIndex, int maximumRows, string sortExpression, string customerID = null, int? ruleId = null, int? catalogId =null, string BranchId = null, IssueStatus? status = null , int? priority = null)
        {
            //DateTime? createdOnFrom = null,            DateTime? createdOnTo = null,
            using (var db = new AppDbContext())
            {
                // Store the query.
                //IQueryable<MdmDQQue> query = db.Set<MdmDQQue>();
                var query = db.MdmDqRunExceptions.Select(q => q).Include(a=>a.MdmDQPriorities).Include(a=>a.MdmDQQueStatuses);

                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(v => v.REASON.Contains(name));
                //if (createdOnFrom.HasValue)
                //    query = query.Where(al => createdOnFrom.Value <= al.RUN_DATE);
                //if (createdOnTo.HasValue)
                //    query = query.Where(al => createdOnTo.Value >= al.RUN_DATE);
                if (ruleId.HasValue && ruleId > 0)
                {
                    int rule = (int)ruleId.Value;
                    query = query.Where(d => d.RULE_ID == rule);
                }
                if(!string.IsNullOrWhiteSpace(customerID))
                {
                    query = query.Where(d => d.CUST_ID == customerID);
                }
                if (catalogId.HasValue && catalogId > 0)
                {
                    int catalog = (int)catalogId.Value;
                    query = query.Where(d => d.CATALOG_ID == catalog);
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

        public void ApproveExceptionQues(List<MdmDqRunException> modifiedrecords)
        {
            using (var db = new AppDbContext())
            {
                foreach (var item in modifiedrecords)
                {
                    var entry = db.CDMA_INDIVIDUAL_BIO_DATA.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                    if (entry != null)
                    {
                        entry.AUTHORISED = "A";
                        db.CDMA_INDIVIDUAL_BIO_DATA.Attach(entry);
                        db.Entry(entry).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    var queitem = db.MdmDqRunExceptions.FirstOrDefault(a => a.EXCEPTION_ID == item.EXCEPTION_ID);
                    if (queitem != null)
                    {
                        queitem.ISSUE_STATUS = (int)IssueStatus.Closed;
                        db.MdmDqRunExceptions.Attach(queitem);
                        db.Entry(queitem).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
        }
        public void DisApproveExceptionQues(List<MdmDqRunException> modifiedrecords, string comments)
        {
            using (var db = new AppDbContext())
            {
                foreach (var item in modifiedrecords)
                {
                    var entry = db.CDMA_INDIVIDUAL_BIO_DATA.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                    if (entry != null)
                    {
                        entry.AUTHORISED = "N";
                        db.CDMA_INDIVIDUAL_BIO_DATA.Attach(entry);
                        db.Entry(entry).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    var queitem = db.MdmDqRunExceptions.FirstOrDefault(a => a.EXCEPTION_ID == item.EXCEPTION_ID);
                    if (queitem != null)
                    {
                        queitem.ISSUE_STATUS = (int)IssueStatus.Rejected;
                        //Add reject reason here
                        queitem.AUTH_REJECT_REASON = comments;
                        db.MdmDqRunExceptions.Attach(queitem);
                        db.Entry(queitem).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }


                }

            }
        }

        public List<CustExceptionsModel> SelectBrnUnauthIssues(string name, int startRowIndex, int maximumRows, string sortExpression, string customerId = null, int? ruleId = null, int? catalogId = null, string BranchId = null, IssueStatus? status = null, int? priority = null)
        {
            //DateTime? createdOnFrom = null,            DateTime? createdOnTo = null,
            //var db2 = new AppDbContext();
            
            using (var db = new AppDbContext()) 
            {
                // Store the query.
                //IQueryable<MdmDQQue> query = db.Set<MdmDQQue>();
                //var data = northwind.CM_DISTRIBUTION_SCHEDULE.Join(northwind.CM_BRANCH,
                //c => c.BRANCH_ID, o => o.BRANCH_ID, (o, c) => new { Sched = o, Branch = c }).ToList();
                string authStatus = "U";
                //int issueStatus = (int)IssueStatus.Open;
                var data = db.MdmDqRunExceptions
                    .Join(db.CDMA_INDIVIDUAL_BIO_DATA,
                    e => e.CUST_ID, c => c.CUSTOMER_NO,
                    (e, c) => new { Excp = e, Cust = c }).Include(e => e.Excp.MdmDQPriorities).Include(a => a.Excp.MdmDQQueStatuses)
                    .Where(x => x.Cust.AUTHORISED == authStatus);
                    //.Where(x=> x.Excp.ISSUE_STATUS == issueStatus);

                var query = data.Select(o => new CustExceptionsModel
                {
                    EXCEPTION_ID = o.Excp.EXCEPTION_ID,
                    RULE_ID = o.Excp.RULE_ID,
                    RULE_NAME = o.Excp.RULE_NAME,
                    CUST_ID = o.Excp.CUST_ID,
                    BRANCH_CODE = o.Excp.BRANCH_CODE,
                    BRANCH_NAME = o.Excp.BRANCH_NAME,
                    ISSUE_PRIORITY_DESC = o.Excp.MdmDQPriorities.PRIORITY_DESCRIPTION,
                    ISSUE_STATUS_DESC = o.Excp.MdmDQQueStatuses.STATUS_DESCRIPTION,
                    RUN_DATE = o.Excp.RUN_DATE,
                    LAST_MODIFIED_DATE = o.Cust.LAST_MODIFIED_DATE,
                    LAST_MODIFIED_BY = o.Cust.LAST_MODIFIED_BY,
                    STATUS_CODE = o.Excp.ISSUE_STATUS,
                    PRIORITY_CODE = o.Excp.ISSUE_PRIORITY,
                    REASON = o.Excp.REASON,
                    CATALOG_TABLE_NAME = o.Excp.CATALOG_TABLE_NAME,
                    CATALOG_ID = o.Excp.CATALOG_ID,
                    SURNAME = o.Cust.SURNAME,
                    OTHERNAME = o.Cust.OTHER_NAME,
                    FIRST_NAME = o.Cust.FIRST_NAME,


                });
                //    q => q.Excp).Include(a => a.MdmDQPriorities).Include(a => a.MdmDQQueStatuses);
                    
                //q => q).Include(a => Excp.MdmDQPriorities).Include(a => a.MdmDQQueStatuses);

                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(v => v.CUST_ID == name);
                //if (createdOnFrom.HasValue)
                //    query = query.Where(al => createdOnFrom.Value <= al.RUN_DATE);
                //if (createdOnTo.HasValue)
                //    query = query.Where(al => createdOnTo.Value >= al.RUN_DATE);
                if (ruleId.HasValue && ruleId > 0)
                {
                    int rule = (int)ruleId.Value;
                    query = query.Where(d => d.RULE_ID == rule);
                }
                if(!string.IsNullOrWhiteSpace(customerId))
                {
                    query = query.Where(d => d.CUST_ID == customerId);
                }
                if (catalogId.HasValue && catalogId > 0)
                {
                    int catalog = (int)catalogId.Value;
                    query = query.Where(d => d.CATALOG_ID == catalog);
                }
                if (!string.IsNullOrWhiteSpace(BranchId))
                {
                    //string brnId = (string)BranchId.Value;
                    query = query.Where(d => d.BRANCH_CODE == BranchId);
                }
                if (status.HasValue) // && status>0
                {
                    int stat = (int)status.Value;
                    query = query.Where(d => d.STATUS_CODE == stat);
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
