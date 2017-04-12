namespace Shortener.Storage.EF
{
    public interface IDbSettings
    {
        string ConnectionString { get; }
    }
}