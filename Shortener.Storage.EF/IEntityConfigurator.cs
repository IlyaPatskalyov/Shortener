using System.Data.Entity;

namespace Shortener.Storage.EF
{
    public interface IEntityConfigurator
    {
        void Configure(DbModelBuilder modelBuilder);
    }
}