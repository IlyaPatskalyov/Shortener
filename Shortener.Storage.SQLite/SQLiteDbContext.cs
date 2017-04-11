using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using Shortener.Storage.EF;
using SQLite.CodeFirst;

namespace Shortener.Storage.SQLite
{
    [DbConfigurationType(typeof(SQLiteConfiguration))]
    public class SQLiteDbContext : DbContext, IDbContext
    {
        private readonly IEntityConfigurator[] entityConfigurators;

        public SQLiteDbContext(IEntityConfigurator[] entityConfigurators)
            : base(new SQLiteConnection()
            {
                ConnectionString =
                    new SQLiteConnectionStringBuilder()
                        {
                            DataSource = @"D:\RiderProjects\WebApplication.db",
                            ForeignKeys = true,
                            Version = 3
                        }
                        .ConnectionString
            }, true)
        {
            this.entityConfigurators = entityConfigurators;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new SqliteDropCreateDatabaseWhenModelChanges<SQLiteDbContext>(modelBuilder));
            foreach (var entityConfigurator in entityConfigurators)
                entityConfigurator.Configure(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
}