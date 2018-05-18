namespace CMdm.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.Entity.Validation;
    using CMdm.Data.Rbac;
    using System.Configuration;
    using Entities.Domain.Mdm;
    using Entities.Domain.Dqi;
    using Entities.Domain.Entity;
    using Entities.Domain.Customer;
    using System.Data.Entity.Infrastructure;
    using Entities.Domain.Audit;
    using System.Data.Entity.Core.Objects;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    //using Entities.Domain.Courses;

    public class AppDbContext : DbContext
    {
        // Your context has been configured to use a 'TECiDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CMdm.Data.AppDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AppDbContext' 
        // connection string in the application configuration file.
        public AppDbContext()
            : base("name=AppDbContext")
        {
        }
        public int SaveChanges(string userId, string custID, bool updateFlag, object originalEntity)
        {
            try
            {
                //var modifiedEntities = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList();
                
                // Get all Added/Deleted/Modified entities (not Unmodified or Detached)
                foreach (var ent in this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
                {
                    // For each changed record, get the audit record entries and add them
                    foreach (CDMA_CHANGE_LOG x in GetAuditRecordsForChange(ent, userId, custID, updateFlag, originalEntity))
                    {
                        this.CDMA_CHANGE_LOGS.Add(x);
                    }
                }

                return base.SaveChanges();
              /*
              db.ChangeTracker.DetectChanges();
                // Had to add using System.Data.Entity.Infrastructure; for this:
                var modifiedEntries = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Modified);

                foreach (var stateChangeEntry in modifiedEntries)
                {
                    for (int i = 0; i < stateChangeEntry.CurrentValues.FieldCount; i++)
                    {
                        var fieldName = stateChangeEntry.OriginalValues.GetName(i);
                        var changedPropertyName = stateChangeEntry.CurrentValues.GetName(i);

                        if (fieldName != changedPropertyName)
                            continue;

                        var originalValue = stateChangeEntry.OriginalValues.GetValue(i).ToString();
                        var changedValue = stateChangeEntry.CurrentValues.GetValue(i).ToString();
                        if (originalValue != changedValue)
                        {
                            // do stuff
                            var foo = originalValue;
                            var bar = changedValue;
                        }

                    }
                }*/
                
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }

        //public void RollBack()
        //{
        //    var context = DataContextFactory.GetDataContext();
        //    var changedEntries = context.ChangeTracker.Entries()
        //        .Where(x => x.State != EntityState.Unchanged).ToList();

        //    foreach (var entry in changedEntries)
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Modified:
        //                entry.CurrentValues.SetValues(entry.OriginalValues);
        //                entry.State = EntityState.Unchanged;
        //                break;
                    
        //        }
        //    }
        //}

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ent"></param>
            /// <param name="userId"></param>
            /// <param name="primaryKeyId"></param>
            /// updateflag determines if the actual update should be performed on the DB
            /// <param name="updateFlag"></param>
            /// <returns></returns>
        private IEnumerable<CDMA_CHANGE_LOG> GetAuditRecordsForChange(DbEntityEntry ent, string userId, string primaryKeyId, bool updateFlag, object originalEntity)
        {
            List<CDMA_CHANGE_LOG> result = new List<CDMA_CHANGE_LOG>();
            string changeId = Guid.NewGuid().ToString();
            //var entityName = ObjectContext.GetObjectType(change.Entity.GetType()).Name;
            // Get the Table() attribute, if one exists
            TableAttribute tableAttr = ent.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;

            // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
            string entityName = tableAttr != null ? tableAttr.Name : ObjectContext.GetObjectType(ent.Entity.GetType()).Name;
            //change.Entity.GetType().Name;

            
            var changeTime = DateTime.UtcNow;
            if (ent.State == EntityState.Added)
            {
                // For Inserts, just add the whole record
                // If the entity implements IDescribableEntity, use the description from Describe(), otherwise use ToString()
                result.Add(new CDMA_CHANGE_LOG()
                {
                    USERID = userId,
                    DATECHANGED = changeTime,
                    EVENTTYPE = "M",    // Modified
                    ENTITYNAME = entityName,
                    PRIMARYKEYVALUE = primaryKeyId.ToString(),
                    PROPERTYNAME = "*ALL",
                    NEWVALUE = "",
                    CHANGEID = changeId

                });
            }
            else if (ent.State == EntityState.Deleted)
            {
                // Same with deletes, do the whole record, and use either the description from Describe() or ToString()
                result.Add(new CDMA_CHANGE_LOG()
                {
                    USERID = userId,
                    DATECHANGED = changeTime,
                    EVENTTYPE = "D",    // Modified
                    ENTITYNAME = entityName,
                    PRIMARYKEYVALUE = primaryKeyId.ToString(),
                    PROPERTYNAME = "*ALL",
                    NEWVALUE = "",
                    CHANGEID = changeId

                });
            }
            else if (ent.State == EntityState.Modified)
            {
                
                foreach (var prop in ent.OriginalValues.PropertyNames)
                {
                    //var manager = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager;
                    //manager.
                    //var originalValue = this.Entry(ent.Entity).Property(prop).OriginalValue ==null ? "" : this.Entry(ent.Entity).Property(prop).OriginalValue.ToString();

                    //var originalValue = manager.GetObjectStateEntry(ent).OriginalValues[prop].ToString();
                    //we cant use this because getdatabasevalues will error out when there are 2 rows in the db for this primarykey (U,A)
                    //var originalValue = ent.GetDatabaseValues().GetValue<object>(prop) == null ? "" : ent.GetDatabaseValues().GetValue<object>(prop).ToString();
                    //we cant use this because when u attach entity and set entitystate to modified in controller, the original values are reset.
                    //var originalValue = ent.OriginalValues.GetValue<object>(prop) == null ? null : ent.OriginalValues.GetValue<object>(prop).ToString();

                    /*
                    //System.Reflection.PropertyInfo pi = originalEntity.GetType().GetProperty(prop);
                    //var originalValue = (string)(pi.GetValue(originalEntity, null));

                    //var originalValues = Convert.ChangeType(originalEntity, ent.GetType());
                    */
                    string propertyName = prop;

                    var parameter = Expression.Parameter(typeof(object));
                    var cast = Expression.Convert(parameter, originalEntity.GetType());
                    var propertyGetter = Expression.Property(cast, propertyName);
                    var castResult = Expression.Convert(propertyGetter, typeof(object));//for boxing

                    var propertyRetriver = Expression.Lambda<Func<object, object>>(castResult, parameter).Compile();

                    var originalValue = propertyRetriver(originalEntity);
                    

                    var currentValue = ent.CurrentValues[prop] == null ? "" : ent.CurrentValues[prop].ToString();
                    // For updates, we only want to capture the columns that actually changed
                    var primaryKey = GetPrimaryKeyValue(ent);
                    originalValue = originalValue == null ? "" : originalValue.ToString();
                    if ((string)originalValue != currentValue)
                    {
                        result.Add(new CDMA_CHANGE_LOG()
                        {
                            USERID = userId,
                            DATECHANGED = changeTime,
                            EVENTTYPE = "M",    // Modified
                            ENTITYNAME = entityName,
                            PRIMARYKEYVALUE = primaryKey.ToString(),
                            PROPERTYNAME = prop,
                            OLDVALUE = originalValue.ToString(),
                            NEWVALUE = currentValue,
                            CHANGEID = changeId
                            //OriginalValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString(),
                            //NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString()
                        });
                    }

                        
                    
                    /*    
                    //var originalValue = change.OriginalValues[prop] == null ? "" :  change.OriginalValues[prop].ToString();
                    var originalValue = ent.GetDatabaseValues().GetValue<object>(prop) == null ? "" : ent.GetDatabaseValues().GetValue<object>(prop).ToString();
                    var currentValue = ent.CurrentValues[prop] == null ? "" : ent.CurrentValues[prop].ToString();
                    if (originalValue != currentValue)
                    {
                        CDMA_CHANGE_LOG log = new CDMA_CHANGE_LOG()
                        {
                            ENTITYNAME = entityName,
                            PRIMARYKEYVALUE = primaryKey.ToString(),
                            PROPERTYNAME = prop,
                            OLDVALUE = originalValue,
                            NEWVALUE = currentValue,
                            DATECHANGED = now
                        };
                        CDMA_CHANGE_LOGS.Add(log);
                    }
                    */
                }
                //Do not update we just need it for audit purpose
                //We do not want to track update from the authorizer
                if (updateFlag)
                    ent.State = EntityState.Modified;
                else
                    ent.State = EntityState.Unchanged; //roll
            }
            return result;
        }

        private IEnumerable<CDMA_CHANGE_LOG> GetAuditRecords(DbEntityEntry ent, string userId, string primaryKeyId, bool updateFlag, object originalEntity)
        {
            List<CDMA_CHANGE_LOG> result = new List<CDMA_CHANGE_LOG>();
            string changeId = Guid.NewGuid().ToString();
            TableAttribute tableAttr = ent.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
            // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
            string entityName = tableAttr != null ? tableAttr.Name : ObjectContext.GetObjectType(ent.Entity.GetType()).Name;
            var changeTime = DateTime.UtcNow;
            if (ent.State == EntityState.Modified)
            {

                foreach (var prop in ent.OriginalValues.PropertyNames)
                {                    
                     //we cant use this because getdatabasevalues will error out when there are 2 rows in the db for this primarykey (U,A)
                    var originalValue = ent.GetDatabaseValues().GetValue<object>(prop) == null ? "" : ent.GetDatabaseValues().GetValue<object>(prop).ToString();
                    var currentValue = ent.CurrentValues[prop] == null ? "" : ent.CurrentValues[prop].ToString();
                    // For updates, we only want to capture the columns that actually changed
                    var primaryKey = GetPrimaryKeyValue(ent);
                    if (originalValue != currentValue)
                    {
                        result.Add(new CDMA_CHANGE_LOG()
                        {
                            USERID = userId,
                            DATECHANGED = changeTime,
                            EVENTTYPE = "M",    // Modified
                            ENTITYNAME = entityName,
                            PRIMARYKEYVALUE = primaryKey.ToString(),
                            PROPERTYNAME = prop,
                            OLDVALUE = originalValue,
                            NEWVALUE = currentValue,
                            CHANGEID = changeId
                        });
                    }
                }
                
            }
            return result;
        }

        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string schemaName = ConfigurationManager.AppSettings["SchemaName"];
            modelBuilder.HasDefaultSchema(schemaName);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            //precision of decimal is 28 significant figures any thing more gives oracle dataaccess specified cast is not valid
            //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/decimal
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(28, 28));

            modelBuilder.Entity<MdmDQQue>().HasRequired(e => e.MdmDQQueStatuses).WithMany(t => t.MdmDQQues).HasForeignKey(e => e.QUE_STATUS).WillCascadeOnDelete(false);
            modelBuilder.Entity<MdmDQQue>().HasRequired(e => e.MdmDQImpacts).WithMany(t => t.MdmDQQues).HasForeignKey(e => e.IMPACT_LEVEL).WillCascadeOnDelete(false);
            modelBuilder.Entity<MdmDQQue>().HasRequired(e => e.MdmDQPriorities).WithMany(t => t.MdmDQQues).HasForeignKey(e => e.PRIORITY).WillCascadeOnDelete(false);

            modelBuilder.Entity<MdmDqRule>().HasRequired(e => e.MdmDQDataSources).WithMany(t => t.MdmDqRules).HasForeignKey(e => e.DATA_SOURCE_ID).WillCascadeOnDelete(false);

            modelBuilder.Entity<MdmDqRule>().HasRequired(e => e.MdmDqRunSchedules).WithMany(t => t.MdmDqRules).HasForeignKey(e => e.RUN_SCHEDULE).WillCascadeOnDelete(false);
            modelBuilder.Entity<MdmDqRule>().HasRequired(e => e.MdmAggrDimensions).WithMany(t => t.MdmDqRules).HasForeignKey(e => e.DIMENSION).WillCascadeOnDelete(false);
            modelBuilder.Entity<MdmDqRule>().HasRequired(e => e.MdmDQPriorities).WithMany(t => t.MdmDqRules).HasForeignKey(e => e.SEVERITY).WillCascadeOnDelete(false);
            modelBuilder.Entity<MdmDqRule>().HasRequired(e => e.MdmCatalogs).WithMany(t => t.MdmDqRules).HasForeignKey(e => e.CATALOG_ID).WillCascadeOnDelete(false);

            modelBuilder.Entity<MdmDqCatalog>().HasRequired(e => e.MdmWeights).WithMany(t => t.MdmDqiCatalogs).HasForeignKey(e => e.COLUMN_WEIGHT).WillCascadeOnDelete(false);
            modelBuilder.Entity<MdmDqCatalog>().HasRequired(e => e.MdmRegex).WithMany(t => t.MdmDqiCatalogs).HasForeignKey(e => e.REGEX).WillCascadeOnDelete(false);


            modelBuilder.Entity<MdmDQDataSource>().HasRequired(e => e.MdmDQDsTypes).WithMany(t => t.MdmDQDataSources).HasForeignKey(e => e.DS_TYPE).WillCascadeOnDelete(false);
            modelBuilder.Entity<MdmDQQue>().HasRequired(e => e.MdmDqRules).WithMany(t => t.MdmDQQues).HasForeignKey(e => e.RULE_ID).WillCascadeOnDelete(false);

            modelBuilder.Entity<MdmDqRunException>().HasRequired(e => e.MdmDQQueStatuses).WithMany(t => t.MdmDqRunExceptions).HasForeignKey(e => e.ISSUE_STATUS).WillCascadeOnDelete(false);
            modelBuilder.Entity<MdmDqRunException>().HasRequired(e => e.MdmDQPriorities).WithMany(t => t.MdmDqRunExceptions).HasForeignKey(e => e.ISSUE_PRIORITY).WillCascadeOnDelete(false);

            modelBuilder.Entity<MdmEntityDetails>().HasRequired(e => e.MDM_WEIGHTS).WithMany(t => t.MDM_ENTITY_DETAILS).HasForeignKey(e => e.WEIGHT_ID).WillCascadeOnDelete(false);

            modelBuilder.Entity<MdmEntityDetails>().HasRequired(e => e.MdmCatalog).WithMany(t => t.MDM_ENTITY_DETAILS).HasForeignKey(e => e.CATALOG_ID).WillCascadeOnDelete(false);

            modelBuilder.Entity<MdmEntityDetails>().HasRequired(e => e.MdmRegex).WithMany(t => t.EntityDetails).HasForeignKey(e => e.REGEX).WillCascadeOnDelete(false);
            modelBuilder.Entity<MdmEntityDetails>().HasRequired(e => e.MdmAggrDimensions).WithMany(t => t.MDM_ENTITY_DETAILS).HasForeignKey(e => e.DQ_DIMENSION).WillCascadeOnDelete(false);
            modelBuilder.Entity<MdmEntityDetails>().HasRequired(e => e.EntityMast).WithMany(t => t.EntityDetails).HasForeignKey(e => e.ENTITY_ID).WillCascadeOnDelete(false);

            modelBuilder.Entity<CM_ROLE_PERM_XREF>().HasRequired(e => e.CM_USER_ROLES).WithMany(t => t.CM_ROLE_PERM_XREF).HasForeignKey(e => e.ROLE_ID).WillCascadeOnDelete(false);
            modelBuilder.Entity<CM_ROLE_PERM_XREF>().HasRequired(e => e.CM_PERMISSIONS).WithMany(t => t.CM_ROLE_PERM_XREF).HasForeignKey(e => e.PERMISSION_ID).WillCascadeOnDelete(false);

            //modelBuilder.Entity<MdmCatalog>().HasRequired(e => e.CREATED_BY).WithMany(t => t.EntityDetails).HasForeignKey(e => e.ENTITY_ID).WillCascadeOnDelete(false);

            modelBuilder.Entity<CDMA_INDIVIDUAL_NEXT_OF_KIN>().HasOptional(e => e.IdTypes).WithMany(t => t.CdmaNextOfKins).HasForeignKey(e => e.IDENTIFICATION_TYPE).WillCascadeOnDelete(false);
            modelBuilder.Entity<CDMA_INDIVIDUAL_NEXT_OF_KIN>().HasOptional(e => e.RelationshipTypes).WithMany(t => t.CdmaNextOfKins).HasForeignKey(e => e.RELATIONSHIP).WillCascadeOnDelete(false);
            modelBuilder.Entity<CDMA_INDIVIDUAL_NEXT_OF_KIN>().HasOptional(e => e.TitleTypes).WithMany(t => t.CdmaNextOfKins).HasForeignKey(e => e.TITLE).WillCascadeOnDelete(false);
            modelBuilder.Entity<CDMA_INDIVIDUAL_NEXT_OF_KIN>().HasOptional(e => e.Countries).WithMany(t => t.CdmaNextOfKins).HasForeignKey(e => e.COUNTRY).WillCascadeOnDelete(false);
            modelBuilder.Entity<CDMA_INDIVIDUAL_NEXT_OF_KIN>().HasOptional(e => e.States).WithMany(t => t.CdmaNextOfKins).HasForeignKey(e => e.STATE).WillCascadeOnDelete(false);
            modelBuilder.Entity<CDMA_INDIVIDUAL_NEXT_OF_KIN>().HasOptional(e => e.LocalGovts).WithMany(t => t.CdmaNextOfKins).HasForeignKey(e => e.LGA).WillCascadeOnDelete(false);
            modelBuilder.Entity<CDMA_FOREIGN_DETAILS>().HasOptional(e => e.Countries).WithMany(t => t.CdmaForeigner).HasForeignKey(e => e.COUNTRY).WillCascadeOnDelete(false);
            
            modelBuilder.Entity<MdmUnauthException>().HasRequired(e => e.MdmDQQueStatuses).WithMany(t => t.MdmUnauthExceptions).HasForeignKey(e => e.ISSUE_STATUS).WillCascadeOnDelete(false);
            modelBuilder.Entity<MdmUnauthException>().HasRequired(e => e.MdmDQPriorities).WithMany(t => t.MdmUnauthExceptions).HasForeignKey(e => e.ISSUE_PRIORITY).WillCascadeOnDelete(false);
 
  
             modelBuilder.Entity<CDMA_EMPLOYMENT_DETAILS>().HasOptional(e => e.Occupationtype).WithMany(t => t.OccupationList).HasForeignKey(e => e.SECTOR_CLASS).WillCascadeOnDelete(false);
 
            modelBuilder.Entity<CDMA_EMPLOYMENT_DETAILS>().HasOptional(e => e.Occupationtype).WithMany(t => t.OccupationList).HasForeignKey(e => e.SECTOR_CLASS).WillCascadeOnDelete(false);
 
 
             modelBuilder.Entity<CDMA_EMPLOYMENT_DETAILS>().HasOptional(e => e.Occupationtype).WithMany(t => t.OccupationList).HasForeignKey(e => e.SECTOR_CLASS).WillCascadeOnDelete(false);
            modelBuilder.Entity<CDMA_EMPLOYMENT_DETAILS>().HasOptional(e => e.Occupationtype).WithMany(t => t.OccupationList).HasForeignKey(e => e.SECTOR_CLASS).WillCascadeOnDelete(false);
             modelBuilder.Entity<CDMA_EMPLOYMENT_DETAILS>().HasOptional(e => e.Subsectortype).WithMany(t => t.Subsectortype).HasForeignKey(e => e.SUB_SECTOR).WillCascadeOnDelete(false);
 
 
            modelBuilder.Entity<CDMA_EMPLOYMENT_DETAILS>().HasOptional(e => e.Occupationtype).WithMany(t => t.OccupationList).HasForeignKey(e => e.SECTOR_CLASS).WillCascadeOnDelete(false);
 
            modelBuilder.Entity<CDMA_EMPLOYMENT_DETAILS>().HasOptional(e => e.Occupationtype).WithMany(t => t.OccupationList).HasForeignKey(e => e.SECTOR_CLASS).WillCascadeOnDelete(false);
            modelBuilder.Entity<CDMA_EMPLOYMENT_DETAILS>().HasOptional(e => e.Occupationtype).WithMany(t => t.OccupationList).HasForeignKey(e => e.SECTOR_CLASS).WillCascadeOnDelete(false);
            modelBuilder.Entity<CDMA_EMPLOYMENT_DETAILS>().HasOptional(e => e.Occupationtype).WithMany(t => t.OccupationList).HasForeignKey(e => e.SECTOR_CLASS).WillCascadeOnDelete(false);
            modelBuilder.Entity<CDMA_EMPLOYMENT_DETAILS>().HasOptional(e => e.Subsectortype).WithMany(t => t.Subsectortype).HasForeignKey(e => e.SUB_SECTOR).WillCascadeOnDelete(false);
 
            modelBuilder.Entity<CDMA_EMPLOYMENT_DETAILS>().HasOptional(e => e.Businessnature).WithMany(t => t.Businessnature).HasForeignKey(e => e.NATURE_OF_BUSINESS_OCCUPATION).WillCascadeOnDelete(false);
            modelBuilder.Entity<CDMA_EMPLOYMENT_DETAILS>().HasOptional(e => e.Indsegment).WithMany(t => t.Indsegment).HasForeignKey(e => e.INDUSTRY_SEGMENT).WillCascadeOnDelete(false);

            
        }
        #region Utilities

        /// <summary>
        /// Get full error
        /// </summary>
        /// <param name="exc">Exception</param>
        /// <returns>Error</returns>
        protected string GetFullErrorText(DbEntityValidationException exc)
        {
            var msg = string.Empty;
            foreach (var validationErrors in exc.EntityValidationErrors)
                foreach (var error in validationErrors.ValidationErrors)
                    msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
            return msg;
        }

        #endregion
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        //public virtual  DbSet<Course> Courses { get; set; }
        public virtual DbSet<CM_PERMISSIONS> CM_PERMISSIONS { get; set; }
        public virtual DbSet<CM_ROLE_PERM_XREF> CM_ROLE_PERM_XREF { get; set; }

        public virtual DbSet<CM_MAKER_CHECKER_XREF> CM_MAKER_CHECKER_XREF { get; set; }
        public virtual DbSet<CM_USER_PROFILE> CM_USER_PROFILE { get; set; }
        public virtual DbSet<CM_USER_ROLE_XREF> CM_USER_ROLE_XREF { get; set; }
        public virtual DbSet<CM_USER_ROLES> CM_USER_ROLES { get; set; }
        public virtual DbSet<MDM_DQI_SETTING> MDM_DQI_SETTING { get; set; }
        public virtual DbSet<CM_BRANCH> CM_BRANCH { get; set; }
        public virtual DbSet<CM_NOTIFICATION> Notifications { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }

        public virtual DbSet<MdmAggrDimensions> MDM_AGGR_DIMENSION { get; set; }
        public virtual DbSet<DqiAggrTransactions> MDM_DQI_AGGR_TRANSACTIONS { get; set; }
        public virtual DbSet<DqiTransactions> MDM_DQI_RECORD_TRANSACTIONS { get; set; }
        public virtual DbSet<MdmEntityDetails> EntityDetails { get; set; }
        public virtual DbSet<EntityMast> EntityMast { get; set; }
        public virtual DbSet<CMdm.Entities.Domain.Mdm.MdmMetrics> MDM_METRICS { get; set; }
        public virtual DbSet<MdmWeights> MDM_WEIGHTS { get; set; }
        //public virtual DbSet<MdmCatalog> MDM_CATALOGS { get; set; }

        public virtual DbSet<MdmDQImpact> MdmDQImpacts { get; set; }
        public virtual DbSet<MdmDQPriority> MdmDQPriorities { get; set; }

        public System.Data.Entity.DbSet<MdmDQQue> MdmDQQues { get; set; }
        public virtual DbSet<MdmDQQueStatus> MdmDQQueStatuses { get; set; }


        public System.Data.Entity.DbSet<MdmDqRule> MdmDqRules { get; set; }

        public System.Data.Entity.DbSet<MdmDQDataSource> MdmDQDataSources { get; set; }

        public System.Data.Entity.DbSet<MdmDqRunSchedule> MdmDqRunSchedules { get; set; }
        public System.Data.Entity.DbSet<MdmDQDsType> MdmDQDsTypes { get; set; }
        public System.Data.Entity.DbSet<MdmCatalog> MdmCatalogs { get; set; }
        public DbSet<MdmDqRunException> MdmDqRunExceptions { get; set; }
        public DbSet<MdmUnauthException> MdmUnauthExceptions { get; set; }
        public DbSet<MdmDqCatalog> MdmDqiParams { get; set; }
        public DbSet<MdmRegex> MdmRegex { get; set; }
        public DbSet<CDMA_COUNTRIES> CDMA_COUNTRIES { get; set; }
        public DbSet<CDMA_RELIGION> CDMA_RELIGION { get; set; }
        public DbSet<SRC_CDMA_STATE> SRC_CDMA_STATE { get; set; }
        public DbSet<SRC_CDMA_LGA> SRC_CDMA_LGA { get; set; }
        public DbSet<CDMA_IDENTIFICATION_TYPE> CDMA_IDENTIFICATION_TYPE { get; set; }
        public DbSet<CDMA_CUSTOMER_INCOME_LOG> CDMA_CUSTOMER_INCOME_LOG { get; set; }
        




        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDIVIDUAL_BIO_DATA> CDMA_INDIVIDUAL_BIO_DATA { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDIVIDUAL_ADDRESS_DETAIL> CDMA_INDIVIDUAL_ADDRESS_DETAIL { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDIVIDUAL_CONTACT_DETAIL> CDMA_INDIVIDUAL_CONTACT_DETAIL { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDIVIDUAL_IDENTIFICATION> CDMA_INDIVIDUAL_IDENTIFICATION { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDIVIDUAL_OTHER_DETAILS> CDMA_INDIVIDUAL_OTHER_DETAILS { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDIVIDUAL_BIO_DATA_LOG> CDMA_INDIVIDUAL_BIO_DATA_LOG { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG> CDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDIVIDUAL_CONTACT_DETAIL_LOG> CDMA_INDIVIDUAL_CONTACT_DETAIL_LOG { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDIVIDUAL_IDENTIFICATION_LOG> CDMA_INDIVIDUAL_IDENTIFICATION_LOG { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDIVIDUAL_OTHER_DETAILS_LOG> CDMA_INDIVIDUAL_OTHER_DETAILS_LOG { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDIVIDUAL_PROFILE_LOG> CDMA_INDIVIDUAL_PROFILE_LOG { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDIVIDUAL_BIO_LOG> CDMA_INDIVIDUAL_BIO_LOG { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDIVIDUAL_CONTACT_LOG> CDMA_INDIVIDUAL_CONTACT_LOG { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_ACCOUNT_INFO> CDMA_ACCOUNT_INFO { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_ACCT_SERVICES_REQUIRED> CDMA_ACCT_SERVICES_REQUIRED { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_BRANCH_CLASS> CDMA_BRANCH_CLASS { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_CUSTOMER_SEGMENT> CDMA_CUSTOMER_SEGMENT { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_CUSTOMER_TYPE> CDMA_CUSTOMER_TYPE { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CM_BACK_JOBS> CM_BACK_JOBS { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.Limit_Range> Limit_Range { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_BUSINESS_SIZE> CDMA_BUSINESS_SIZE { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_ACCOUNT_TYPE> CDMA_ACCOUNT_TYPE { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_BUSINESS_DIVISION> CDMA_BUSINESS_DIVISION { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_ONLINE_TRANSFER_LIMIT> CDMA_ONLINE_TRANSFER_LIMIT { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_CHEQUE_CONFIRM_THRESHOLD> CDMA_CHEQUE_CONFIRM_THRESHOLD { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_BUSINESS_SEGMENT> CDMA_BUSINESS_SEGMENT { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_ACCOUNT_INFO_LOG> CDMA_ACCOUNT_INFO_LOG { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_ACCOUNT_RECORD_LOG> CDMA_ACCOUNT_RECORD_LOG { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_ACCT_SERVICES_LOG> CDMA_ACCT_SERVICES_LOG { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CMDM_PHONEVALIDATION_RESULTS> CMDM_PHONEVALIDATION_RESULTS { get; set; }


        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Dqi.BranchDqiSummary> BranchDqiSummaries { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Kpi.BrnKpi> BrnKpis { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Configuration.Setting> Settings { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_CUSTOMER_INCOME> CDMA_CUSTOMER_INCOME { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INCOME_BAND> CDMA_INCOME_BAND { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INITIAL_DEPOSIT_RANGE> CDMA_INITIAL_DEPOSIT_RANGE { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_MARITALSTATUS> CDMA_MARITALSTATUS { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.GoldenRecord.CdmaGoldenRecord> CdmaGoldenRecords { get; set; }
        

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.CustomModule.Fcmb.OutStandingDoc> OutStandingDocs { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.CustomModule.Fcmb.AccountOfficer> AccountOfficers { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.CustomModule.Fcmb.WrongSchemeCode> WrongSchemeCodes { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.CustomModule.Fcmb.CustSegment> CustSegment { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.CustomModule.Fcmb.MultipleRefCode> MultipleRefCode { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.CustomModule.Fcmb.EmailPhone> EmailPhone { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.CustomModule.Fcmb.WrongSegment> WrongSegment { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDIVIDUAL_NEXT_OF_KIN> CDMA_INDIVIDUAL_NEXT_OF_KIN { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_CUST_REL_TYPE> CDMA_CUST_REL_TYPE { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_CUST_TITLE> CDMA_CUST_TITLE { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Audit.CDMA_CHANGE_LOG> CDMA_CHANGE_LOGS { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_EMPLOYMENT_DETAILS> CDMA_EMPLOYMENT_DETAILS { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_OCCUPATION_LIST> CDMA_OCCUPATION_LIST { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDUSTRY_SUBSECTOR> CDMA_INDUSTRY_SUBSECTOR { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_NATURE_OF_BUSINESS> CDMA_NATURE_OF_BUSINESS { get; set; }
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_INDUSTRY_SEGMENT> CDMA_INDUSTRY_SEGMENT { get; set; }
 
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_FOREIGN_DETAILS> CDMA_FOREIGN_DETAILS { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_TRUSTS_CLIENT_ACCOUNTS> CDMA_TRUSTS_CLIENT_ACCOUNTS { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_AUTH_FINANCE_INCLUSION> CDMA_AUTH_FINANCE_INCLUSION { get; set; }
 
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_JURAT> CDMA_JURAT { get; set; }
   
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CDMA_ADDITIONAL_INFORMATION> CDMA_ADDITIONAL_INFORMATION { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Logging.Log> LOG { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.TMP_ACCOUNTOFFICER_EXCEPTIONS> TMP_ACCOUNTOFFICER_EXCEPTIONS { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Dqi.CDMA_DQI_PROCESSING_RESULT> CDMA_DQI_PROCESSING_RESULT { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.GoldenRecord.GoldenRecordRules> GoldenRecordRules { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Report.CmReportDefn> RptDefinition { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.TMP_SEC_SUB3> TMP_SEC_SUB3 { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CMDM_WRONGSCHCODECLASS> CMDM_WRONGSCHCODECLASS { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CMDM_MULTIPLE_REF_CODE> CMDM_MULTIPLE_REF_CODE { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CMDM_CMMN_EMAIL_PHONENO> CMDM_CMMN_EMAIL_PHONENO { get; set; }

        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CMDM_WRONG_SEGMENTSUBSEGMENT> CMDM_WRONG_SEGMENTSUBSEGMENT { get; set; }

        //  public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.CMDM_SRC_BRANCH> CMDM_SRC_BRANCH { get; set; }


    }

}