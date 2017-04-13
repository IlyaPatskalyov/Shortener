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
        private static readonly Regex keyRegex = new Regex("[^A-Za-z0-9]+", RegexOptions.Compiled);

        public LinksRepository(IDbContext db)
        {
            this.db = db;
            set = this.db.Set<Link>();
        }

        public Link Create(string url, Guid userId)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            return DoTransaction(() =>
                                 {
                                     var attempt = 0;
                                     Link link, current;
                                     do
                                     {
                                         if (attempt > NumberOfAttempts)
                                         {
                                             Interlocked.CompareExchange(ref keyLength, keyLength + 1, keyLength);
                                             attempt = 0;
                                         }
                                         link = BuildLink(url, userId);
                                         var key = link.Key;
                                         current = set.FirstOrDefault(p => p.Key == key);
                                         attempt++;
                                     } while (current != null);
                                     set.Add(link);
                                     db.SaveChanges();
                                     return link;
                                 });
        }

        private static Link BuildLink(string url, Guid userId)
        {
            var id = Guid.NewGuid();
            var created = DateTime.UtcNow;
            var key = keyRegex.Replace(Convert.ToBase64String(id.ToByteArray()), "").Substring(0, keyLength);

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
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            return set.FirstOrDefault(p => p.Key == key);
        }

        public Link GetByKeyAndIncrement(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            return DoTransaction(() =>
                                 {
                                     var link = set.FirstOrDefault(p => p.Key == key);
                                     if (link != null)
                                     {
                                         link.CountOfRedirects++;
                                         db.Entry(link).State = EntityState.Modified;
                                         db.SaveChanges();
                                     }
                                     return link;
                                 });
        }

        public Link[] GetLinksByUserId(Guid userId)
        {
            var user = userId.ToString();
            return set.Where(x => x.UserId == user).ToArray();
        }


        public void Delete(string key, Guid userId)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key");
            DoTransaction(() =>
                          {
                              var user = userId.ToString();
                              var link = set.FirstOrDefault(p => p.Key == key && p.UserId == user);
                              if (link != null)
                              {
                                  set.Remove(link);
                                  db.SaveChanges();
                              }
                              return link;
                          });
        }

        private T DoTransaction<T>(Func<T> action)
        {
            T result;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    result = action();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return result;
        }
    }
}