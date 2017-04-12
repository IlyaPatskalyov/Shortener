using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using Shortener.Storage.EF;
using SQLite.CodeFirst;

namespace Shortener.Storage.SQLite
{
    [DbConfigurationType(typeof(SQLiteDbConfiguration))]
    public class SQLiteDbContext : DbContext, IDbContext
    {
        private readonly IEntityConfigurator[] entityConfigurators;

        public SQLiteDbContext(IDbSettings settings, IEntityConfigurator[] entityConfigurators)
            : base(new SQLiteConnection()
                   {
                       ConnectionString = settings.ConnectionString
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