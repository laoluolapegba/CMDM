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
        public MdmDqRunException SelectExceptionById(int recordId)
        {
            using (var db = new AppDbContext())
            {
                return db.Set<MdmDqRunException>().Find(recordId);
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

        public void DisApproveExceptionQues (List<MdmDqRunException> modifiedrecords, string comments)
        {
            using (var db = new AppDbContext())
            {
                foreach (var item in modifiedrecords)
                {   
                    /*
                    switch (item.CATALOG_TABLE_NAME)
                    {
                        case "CDMA_INDIVIDUAL_BIO_DATA":
                            var entry = db.CDMA_INDIVIDUAL_BIO_DATA.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            if (entry != null)
                            {
                                entry.AUTHORISED = "A";
                                db.CDMA_INDIVIDUAL_BIO_DATA.Attach(entry);
                                db.Entry(entry).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            break;
                        case "CDMA_CUSTOMER_INCOME":
                            var entry1 = db.CDMA_CUSTOMER_INCOME.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            if (entry1 != null)
                            {
                                entry1.AUTHORISED = "A";
                                db.CDMA_CUSTOMER_INCOME.Attach(entry1);
                                db.Entry(entry1).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            break;
                        case "CDMA_INDIVIDUAL_NEXT_OF_KIN":
                            var entry2 = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            if (entry2 != null)
                            {
                                entry2.AUTHORISED = "A";
                                db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Attach(entry2);
                                db.Entry(entry2).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            break;
                        case "CDMA_FOREIGN_DETAILS":
                            var entry3 = db.CDMA_FOREIGN_DETAILS.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            if (entry3 != null)
                            {
                                entry3.AUTHORISED = "A";
                                db.CDMA_FOREIGN_DETAILS.Attach(entry3);
                                db.Entry(entry3).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            break;
                        case "CDMA_JURAT":
                            var entry4 = db.CDMA_JURAT.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            if (entry4 != null)
                            {
                                entry4.AUTHORISED = "A";
                                db.CDMA_JURAT.Attach(entry4);
                                db.Entry(entry4).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            break;
                        case "CDMA_EMPLOYMENT_DETAILS":
                            var entry5 = db.CDMA_EMPLOYMENT_DETAILS.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            if (entry5 != null)
                            {
                                entry5.AUTHORISED = "A";
                                db.CDMA_EMPLOYMENT_DETAILS.Attach(entry5);
                                db.Entry(entry5).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            break;
                        case "CDMA_TRUSTS_CLIENT_ACCOUNTS":
                            var entry6 = db.CDMA_TRUSTS_CLIENT_ACCOUNTS.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            if (entry6 != null)
                            {
                                entry6.AUTHORISED = "A";
                                db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Attach(entry6);
                                db.Entry(entry6).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            break;
                        case "CDMA_AUTH_FINANCE_INCLUSION":
                            var entry7 = db.CDMA_AUTH_FINANCE_INCLUSION.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            if (entry7 != null)
                            {
                                entry7.AUTHORISED = "A";
                                db.CDMA_AUTH_FINANCE_INCLUSION.Attach(entry7);
                                db.Entry(entry7).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            break;
                        case "CDMA_ADDITIONAL_INFORMATION":
                            var entry8 = db.CDMA_ADDITIONAL_INFORMATION.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            if (entry8 != null)
                            {
                                entry8.AUTHORISED = "A";
                                db.CDMA_ADDITIONAL_INFORMATION.Attach(entry8);
                                db.Entry(entry8).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            break;
                        default:
                            break;

                    }
                    */
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
        public void ApproveExceptionQues(List<MdmDqRunException> modifiedrecords, int userId)
        {
            using (var db = new AppDbContext())
            {
                //The property 'AUTHORISED' is part of the object's key information and cannot be modified.
                //entry2.AUTHORISED = "A";
                //db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Attach(entry2);
                //db.Entry(entry2).State = System.Data.Entity.EntityState.Modified;
                int noOfRowUpdated = 0;
                foreach (var item in modifiedrecords)
                {
                    var queitem = db.MdmDqRunExceptions.FirstOrDefault(a => a.EXCEPTION_ID == item.EXCEPTION_ID);
                    if (queitem != null)
                    {
                        queitem.ISSUE_STATUS = (int)IssueStatus.Closed;
                        db.MdmDqRunExceptions.Attach(queitem);
                        db.Entry(queitem).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    switch (item.CATALOG_TABLE_NAME)
                    {
                        #region Individual
                        case "CDMA_INDIVIDUAL_BIO_DATA":
                            var entry = db.CDMA_INDIVIDUAL_BIO_DATA.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            var oldrecord = db.CDMA_INDIVIDUAL_BIO_DATA.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "A");
                            if (entry != null)
                            {
                                if (oldrecord != null)
                                {
                                    db.CDMA_INDIVIDUAL_BIO_DATA.Remove(oldrecord);
                                }

                                db.SaveChanges();
                                noOfRowUpdated = db.Database.ExecuteSqlCommand("Update CDMA_INDIVIDUAL_BIO_DATA set AUTHORISED='A', AUTHORISED_BY = " + userId + ", AUTHORISED_DATE=sysdate  where customer_no = '" + item.CUST_ID + "' and AUTHORISED ='U' ");


                            }
                            var addressentry = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            var addressrecord = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "A");
                            if (addressentry != null)
                            {
                                if (addressrecord != null)
                                {
                                    db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Remove(addressrecord);
                                }

                                db.SaveChanges();
                                noOfRowUpdated = db.Database.ExecuteSqlCommand("Update CDMA_INDIVIDUAL_ADDRESS_DETAIL set AUTHORISED='A', AUTHORISED_BY = " + userId + ", AUTHORISED_DATE=sysdate  where customer_no = '" + item.CUST_ID + "' and AUTHORISED ='U' ");


                            }
                            var identry = db.CDMA_INDIVIDUAL_IDENTIFICATION.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            var idrecord = db.CDMA_INDIVIDUAL_IDENTIFICATION.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "A");
                            if (identry != null)
                            {
                                if (addressrecord != null)
                                {
                                    db.CDMA_INDIVIDUAL_IDENTIFICATION.Remove(idrecord);
                                }

                                db.SaveChanges();
                                noOfRowUpdated = db.Database.ExecuteSqlCommand("Update CDMA_INDIVIDUAL_IDENTIFICATION set AUTHORISED='A', AUTHORISED_BY = " + userId + ", AUTHORISED_DATE=sysdate  where customer_no = '" + item.CUST_ID + "' and AUTHORISED ='U' ");


                            }
                            var otherentry = db.CDMA_INDIVIDUAL_OTHER_DETAILS.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            var otherrecord = db.CDMA_INDIVIDUAL_OTHER_DETAILS.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "A");
                            if (identry != null)
                            {
                                if (addressrecord != null)
                                {
                                    db.CDMA_INDIVIDUAL_OTHER_DETAILS.Remove(otherrecord);
                                }

                                db.SaveChanges();
                                noOfRowUpdated = db.Database.ExecuteSqlCommand("Update CDMA_INDIVIDUAL_OTHER_DETAILS set AUTHORISED='A', AUTHORISED_BY = " + userId + ", AUTHORISED_DATE=sysdate  where customer_no = '" + item.CUST_ID + "' and AUTHORISED ='U' ");


                            }
                            var contactentry = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            var contactrecord = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "A");
                            if (identry != null)
                            {
                                if (contactrecord != null)
                                {
                                    db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Remove(contactrecord);
                                }

                                db.SaveChanges();
                                noOfRowUpdated = db.Database.ExecuteSqlCommand("Update CDMA_INDIVIDUAL_CONTACT_DETAIL set AUTHORISED='A', AUTHORISED_BY = " + userId + ", AUTHORISED_DATE=sysdate  where customer_no = '" + item.CUST_ID + "' and AUTHORISED ='U' ");


                            }
                            break;
                        #endregion
                        case "CDMA_CUSTOMER_INCOME":
                            var entry1 = db.CDMA_CUSTOMER_INCOME.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            var oldrecord1 = db.CDMA_CUSTOMER_INCOME.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "A");
                            if (entry1 != null)
                            {
                                if (oldrecord1 != null)
                                {
                                    db.CDMA_CUSTOMER_INCOME.Remove(oldrecord1);
                                }

                                db.SaveChanges();
                                noOfRowUpdated = db.Database.ExecuteSqlCommand("Update CDMA_CUSTOMER_INCOME set AUTHORISED='A', AUTHORISED_BY = " + userId + ", AUTHORISED_DATE=sysdate  where customer_no = '" + item.CUST_ID + "' and AUTHORISED ='U' ");

                            }
                            break;
                        case "CDMA_INDIVIDUAL_NEXT_OF_KIN":
                            var entry2 = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            var oldrecord2 = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "A");
                            if (entry2 != null)
                            {
                                if (oldrecord2 != null)
                                {
                                    db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Remove(oldrecord2);
                                }

                                db.SaveChanges();
                                noOfRowUpdated = db.Database.ExecuteSqlCommand("Update CDMA_INDIVIDUAL_NEXT_OF_KIN set AUTHORISED='A', AUTHORISED_BY = " + userId + ", AUTHORISED_DATE=sysdate  where customer_no = '" + item.CUST_ID + "' and AUTHORISED ='U' ");

                            }
                            break;
                        case "CDMA_FOREIGN_DETAILS":
                            var entry3 = db.CDMA_FOREIGN_DETAILS.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            var oldrecord3 = db.CDMA_FOREIGN_DETAILS.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "A");
                            if (entry3 != null)
                            {
                                if (oldrecord3 != null)
                                {
                                    db.CDMA_FOREIGN_DETAILS.Remove(oldrecord3);
                                }

                                db.SaveChanges();
                                noOfRowUpdated = db.Database.ExecuteSqlCommand("Update CDMA_FOREIGN_DETAILS set AUTHORISED='A', AUTHORISED_BY = " + userId + ", AUTHORISED_DATE=sysdate  where customer_no = '" + item.CUST_ID + "' and AUTHORISED ='U' ");

                            }
                            break;
                        case "CDMA_JURAT":
                            var entry4 = db.CDMA_JURAT.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            var oldrecord4 = db.CDMA_JURAT.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "A");
                            if (entry4 != null)
                            {
                                if (oldrecord4 != null)
                                {
                                    db.CDMA_JURAT.Remove(oldrecord4);
                                }

                                db.SaveChanges();
                                noOfRowUpdated = db.Database.ExecuteSqlCommand("Update CDMA_JURAT set AUTHORISED='A', AUTHORISED_BY = " + userId + ", AUTHORISED_DATE=sysdate  where customer_no = '" + item.CUST_ID + "' and AUTHORISED ='U' ");

                            }
                            break;
                        case "CDMA_EMPLOYMENT_DETAILS":
                            var entry5 = db.CDMA_EMPLOYMENT_DETAILS.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            var oldrecord5 = db.CDMA_EMPLOYMENT_DETAILS.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "A");
                            if (entry5 != null)
                            {
                                if(oldrecord5 != null)
                                {
                                    db.CDMA_EMPLOYMENT_DETAILS.Remove(oldrecord5);
                                }

                                db.SaveChanges();
                                noOfRowUpdated = db.Database.ExecuteSqlCommand("Update CDMA_EMPLOYMENT_DETAILS set AUTHORISED='A', AUTHORISED_BY = " + userId + ", AUTHORISED_DATE=sysdate  where customer_no = '" + item.CUST_ID + "' and AUTHORISED ='U' ");

                            }
                            break;
                        case "CDMA_TRUSTS_CLIENT_ACCOUNTS":
                            var entry6 = db.CDMA_TRUSTS_CLIENT_ACCOUNTS.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            var oldrecord6 = db.CDMA_TRUSTS_CLIENT_ACCOUNTS.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "A");
                            if (entry6 != null)
                            {
                                if (oldrecord6 != null)
                                {
                                    db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Remove(oldrecord6);
                                }

                                db.SaveChanges();
                                noOfRowUpdated = db.Database.ExecuteSqlCommand("Update CDMA_TRUSTS_CLIENT_ACCOUNTS set AUTHORISED='A', AUTHORISED_BY = " + userId + ", AUTHORISED_DATE=sysdate  where customer_no = '" + item.CUST_ID + "' and AUTHORISED ='U' ");

                            }
                            break;
                        case "CDMA_AUTH_FINANCE_INCLUSION":
                            var entry7 = db.CDMA_AUTH_FINANCE_INCLUSION.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            var oldrecord7 = db.CDMA_AUTH_FINANCE_INCLUSION.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "A");
                            if (entry7 != null)
                            {
                                if (oldrecord7 != null)
                                {
                                    db.CDMA_AUTH_FINANCE_INCLUSION.Remove(oldrecord7);
                                }

                                db.SaveChanges();
                                noOfRowUpdated = db.Database.ExecuteSqlCommand("Update CDMA_AUTH_FINANCE_INCLUSION set AUTHORISED='A', AUTHORISED_BY = " + userId + ", AUTHORISED_DATE=sysdate  where customer_no = '" + item.CUST_ID + "' and AUTHORISED ='U' ");

                            }
                            break;
                        case "CDMA_ADDITIONAL_INFORMATION":
                            var entry8 = db.CDMA_ADDITIONAL_INFORMATION.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "U");
                            var oldrecord8 = db.CDMA_ADDITIONAL_INFORMATION.FirstOrDefault(a => a.CUSTOMER_NO == item.CUST_ID && a.AUTHORISED == "A");
                            if (entry8 != null)
                            {
                                if (oldrecord8 != null)
                                {
                                    db.CDMA_ADDITIONAL_INFORMATION.Remove(oldrecord8);
                                }

                                db.SaveChanges();
                                noOfRowUpdated = db.Database.ExecuteSqlCommand("Update CDMA_ADDITIONAL_INFORMATION set AUTHORISED='A', AUTHORISED_BY = " + userId + ", AUTHORISED_DATE=sysdate  where customer_no = '" + item.CUST_ID + "' and AUTHORISED ='U' ");

                            }
                            break;
                        default:
                            break;

                    }

                    


                }

            }
        }

        public List<CustExceptionsModel> SelectBrnUnauthIssues(string name, int startRowIndex, int maximumRows, string sortExpression, string customerId = null, int? ruleId = null, int? catalogId = null, string BranchId = null, IssueStatus? status = null, int? priority = null)
        {
            //DateTime? createdOnFrom = null,            DateTime? createdOnTo = null,
            //var db2 = new AppDbContext();
            #region old
            //using (var db = new AppDbContext()) 
            //{
            //    // Store the query.
            //    string authStatus = "U";
            //    //int issueStatus = (int)IssueStatus.Open;
            //    var data = db.MdmDqRunExceptions
            //        .Join(db.CDMA_INDIVIDUAL_BIO_DATA,
            //        e => e.CUST_ID, c => c.CUSTOMER_NO,
            //        (e, c) => new { Excp = e, Cust = c }).Include(e => e.Excp.MdmDQPriorities).Include(a => a.Excp.MdmDQQueStatuses)
            //        .Where(x => x.Cust.AUTHORISED == authStatus);
            //        //.Where(x=> x.Excp.ISSUE_STATUS == issueStatus);

            //    var query = data.Select(o => new CustExceptionsModel
            //    {
            //        EXCEPTION_ID = o.Excp.EXCEPTION_ID,
            //        RULE_ID = o.Excp.RULE_ID,
            //        RULE_NAME = o.Excp.RULE_NAME,
            //        CUST_ID = o.Excp.CUST_ID,
            //        BRANCH_CODE = o.Excp.BRANCH_CODE,
            //        BRANCH_NAME = o.Excp.BRANCH_NAME,
            //        ISSUE_PRIORITY_DESC = o.Excp.MdmDQPriorities.PRIORITY_DESCRIPTION,
            //        ISSUE_STATUS_DESC = o.Excp.MdmDQQueStatuses.STATUS_DESCRIPTION,
            //        RUN_DATE = o.Excp.RUN_DATE,
            //        LAST_MODIFIED_DATE = o.Cust.LAST_MODIFIED_DATE,
            //        LAST_MODIFIED_BY = o.Cust.LAST_MODIFIED_BY,
            //        STATUS_CODE = o.Excp.ISSUE_STATUS,
            //        PRIORITY_CODE = o.Excp.ISSUE_PRIORITY,
            //        REASON = o.Excp.REASON,
            //        CATALOG_TABLE_NAME = o.Excp.CATALOG_TABLE_NAME,
            //        CATALOG_ID = o.Excp.CATALOG_ID,
            //        SURNAME = o.Cust.SURNAME,
            //        OTHERNAME = o.Cust.OTHER_NAME,
            //        FIRST_NAME = o.Cust.FIRST_NAME,


            //    });
            #endregion
            using (var db = new AppDbContext())
            {
                // Store the query.
                string authStatus = "U";
                //int issueStatus = (int)IssueStatus.Open;
                //var data = db.MdmUnauthExceptions
                //    .Join(db.CDMA_INDIVIDUAL_BIO_DATA,
                //    e => e.CUST_ID, c => c.CUSTOMER_NO,
                //    (e, c) => new { Excp = e, Cust = c }).Include(e => e.Excp.MdmDQPriorities).Include(a => a.Excp.MdmDQQueStatuses)
                //    .Where(x => x.Cust.AUTHORISED == authStatus);
                //.Where(x=> x.Excp.ISSUE_STATUS == issueStatus);

                var query = db.MdmUnauthExceptions.Select(o => new CustExceptionsModel
                {
                    EXCEPTION_ID = o.EXCEPTION_ID,
                    RULE_ID = o.RULE_ID,
                    RULE_NAME = o.RULE_NAME,
                    CUST_ID = o.CUST_ID,
                    BRANCH_CODE = o.BRANCH_CODE,
                    BRANCH_NAME = o.BRANCH_NAME,
                    ISSUE_PRIORITY_DESC = o.MdmDQPriorities.PRIORITY_DESCRIPTION,
                    ISSUE_STATUS_DESC = o.MdmDQQueStatuses.STATUS_DESCRIPTION,
                    RUN_DATE = o.RUN_DATE,
                    LAST_MODIFIED_DATE = o.LAST_MODIFIED_DATE,
                    LAST_MODIFIED_BY = o.LAST_MODIFIED_BY,
                    STATUS_CODE = o.ISSUE_STATUS,
                    PRIORITY_CODE = o.ISSUE_PRIORITY,
                    REASON = o.REASON,
                    CATALOG_TABLE_NAME = o.CATALOG_TABLE_NAME,
                    CATALOG_ID = o.CATALOG_ID,
                    SURNAME = o.SURNAME,
                    OTHERNAME = o.OTHER_NAME,
                    FIRST_NAME = o.FIRST_NAME,


                });
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
