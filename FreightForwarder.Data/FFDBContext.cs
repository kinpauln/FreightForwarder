using FreightForwarder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.Data
{
    public class FFDBContext : DbContext
    {
        public FFDBContext()
            : base("name=FFDBContext")
        {
            Database.SetInitializer<FFDBContext>(new MigrateDatabaseToLatestVersion<FFDBContext, ReportingDbMigrationsConfiguration>());
            //Database.SetInitializer<PhotoCompContext>(new DropCreateDatabaseIfModelChanges<PhotoCompContext>());
            //Database.SetInitializer<PhotoCompContext>(new DropCreateDatabaseAlways<PhotoCompContext>());
        }

        //public PhotoCompContext(string configString)
        //    : base(configString)
        //{
        //    Database.SetInitializer<PhotoCompContext>(new MigrateDatabaseToLatestVersion<PhotoCompContext, ReportingDbMigrationsConfiguration>());
        //    //Database.SetInitializer<PhotoCompContext>(new DropCreateDatabaseIfModelChanges<PhotoCompContext>());
        //    //Database.SetInitializer<PhotoCompContext>(new DropCreateDatabaseAlways<PhotoCompContext>());
        //}

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<RouteInformationItem> RouteItems { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<RegisterCode> RegisterCodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////Fluent API
            //modelBuilder.Entity<Entry>().HasKey(d => d.Id);
            //base.OnModelCreating(modelBuilder);

            // table name is the same as class name
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // dont crate table EdmMetadata
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        }

        internal sealed class ReportingDbMigrationsConfiguration : DbMigrationsConfiguration<FFDBContext>
        {
            public ReportingDbMigrationsConfiguration()
            {
                //update DB regardless of changes of any Model Class
                AutomaticMigrationsEnabled = true;
                AutomaticMigrationDataLossAllowed = true;
            }
        }
    }
}
