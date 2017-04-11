using Shortener.Datas;

namespace Shortener.Storage
{
    public interface ILinksRepository
    {
        Link Create(string url);

        Link GetByKey(string key);
        Link GetByKeyAndIncrement(string key);

        void Delete(string key);
    }
}