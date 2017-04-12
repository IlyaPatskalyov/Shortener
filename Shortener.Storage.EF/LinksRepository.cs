using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Shortener.Datas;

namespace Shortener.Storage.EF
{
    public class LinksRepository : ILinksRepository
    {

        private const int NumberOfAttempts = 5;

        private static int keyLength = 3;

        private readonly IDbContext db;
        private readonly DbSet<Link> set;
        private static readonly Regex KeyRegex = new Regex("[^A-Za-z0-9]+", RegexOptions.Compiled);

        public LinksRepository(IDbContext db)
        {
            this.db = db;
            set = this.db.Set<Link>();
        }

        public Link Create(string url, Guid userId)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("link");
            Link link, current;
            int i = 0;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    do
                    {
                        if (i > NumberOfAttempts)
                        {
                            Interlocked.CompareExchange(ref keyLength, keyLength + 1, keyLength);
                            i = 0;
                        }
                        link = BuildLink(url, userId);
                        current = set.FirstOrDefault(p => p.Key == link.Key);
                        i++;
                    } while (current != null);
                    set.Add(link);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return link;
        }

        private static Link BuildLink(string url, Guid userId)
        {
            var id = Guid.NewGuid();
            var created = DateTime.UtcNow;
            var key = KeyRegex.Replace(Convert.ToBase64String(id.ToByteArray()).Substring(0, keyLength), "");

            var link = new Link()
                       {
                           Id = id,
                           Created = created,
                           Modified = created,
                           CountOfRedirects = 0,
                           UserId = userId.ToString(),
                           Key = key,
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

        public Link[] GetLinksByUserId(Guid userId)
        {
            var user = userId.ToString();
            return set.Where(x => x.UserId == user).ToArray();
        }

        public void Delete(string key, Guid userId)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key");
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var user = userId.ToString();
                    var link = set.FirstOrDefault(p => p.Key == key && p.UserId == user);
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