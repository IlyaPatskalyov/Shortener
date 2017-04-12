using System;
using Shortener.Datas;

namespace Shortener.Storage
{
    public interface ILinksRepository
    {
        Link Create(string url, Guid userId);

        Link GetByKey(string key);

        Link GetByKeyAndIncrement(string key);

        Link[] GetLinksByUserId(Guid userId);

        void Delete(string key, Guid userId);
    }
}