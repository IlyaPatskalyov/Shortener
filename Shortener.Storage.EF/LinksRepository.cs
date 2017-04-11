using System;
using System.Data.Entity;
using System.Linq;
using Shortener.Datas;

namespace Shortener.Storage.EF
{
    public class LinksRepository : ILinksRepository
    {
        private readonly IDbContext db;
        private readonly DbSet<Link> set;

        public LinksRepository(IDbContext db)
        {
            this.db = db;
            set = this.db.Set<Link>();
        }

        public Link Create(string url)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("link");
            Link link, current;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    do
                    {
                        link = BuildLink(url);
                        current = set.FirstOrDefault(p => p.Key == link.Key);
                    } while (current != null);
                    set.Add(link);
                    db.SaveChanges();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return link;
        }

        private static Link BuildLink(string url)
        {
            var id = Guid.NewGuid();
            var created = DateTime.UtcNow;

            var link = new Link()
            {
                Id = id,
                Created = created,
                Modified = created,
                CountOfRedirects = 0,
                Key = Convert.ToBase64String(id.ToByteArray()).Substring(0, 10),
                Url = url
            };
            return link;
        }

        public Link GetByKey(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key");
            return set.FirstOrDefault(p => p.Key == key);
        }

        public Link GetByKeyAndIncrement(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key");
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var link = set.FirstOrDefault(p => p.Key == key);
                    if (link != null)
                    {
                        link.CountOfRedirects++;
                        db.Entry(link).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    transaction.Commit();
                    return link;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Delete(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key");
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var link = set.FirstOrDefault(p => p.Key == key);
                    if (link != null)
                    {
                        set.Remove(link);
                        db.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}