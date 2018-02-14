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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string schemaName = ConfigurationManager.AppSettings["SchemaName"];
            modelBuilder.HasDefaultSchema(schemaName);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(38, 40));

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
        public virtual DbSet<EntityDetails> EntityDetails { get; set; }
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
        public DbSet<MdmDqCatalog> MdmDqiParams { get; set; }
        public DbSet<MdmRegex> MdmRegex { get; set; }
        public DbSet<CDMA_COUNTRIES> CDMA_COUNTRIES { get; set; }
        public DbSet<CDMA_RELIGION> CDMA_RELIGION { get; set; }
        public DbSet<SRC_CDMA_STATE> SRC_CDMA_STATE { get; set; }
        public DbSet<SRC_CDMA_LGA> SRC_CDMA_LGA { get; set; }
        public DbSet<CDMA_IDENTIFICATION_TYPE> CDMA_IDENTIFICATION_TYPE { get; set; }

<<<<<<< HEAD
        


        
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

        //

=======
        public System.Data.Entity.DbSet<CMdm.Entities.Domain.Customer.IndCustomerBioData> IndCustomerBioDatas { get; set; }
>>>>>>> 225a5f4d1805c10ab00050b33331e6248ab8fa7d
    }

}