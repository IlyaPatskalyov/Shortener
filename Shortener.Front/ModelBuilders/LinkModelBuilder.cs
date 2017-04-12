using Shortener.Datas;
using Shortener.Front.Models;

namespace Shortener.Front.ModelBuilders
{
    public class LinkModelBuilder : ILinkModelBuilder
    {
        public LinkModel BuildLink(Link l)
        {
            return new LinkModel
                   {
                       Key = l.Key,
                       Url = l.Url,
                       Created = l.Created,
                       CountOfRedirects = l.CountOfRedirects
                   };
        }
    }
}