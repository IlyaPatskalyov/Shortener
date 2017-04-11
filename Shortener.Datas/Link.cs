using System;
using Shortener.Datas.Common;

namespace Shortener.Datas
{
    public class Link : Entity<Guid>
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public string UserId { get; set; }

        public string Key { get; set; }

        public string Url { get; set; }

        public int CountOfRedirects { get; set; }
    }
}