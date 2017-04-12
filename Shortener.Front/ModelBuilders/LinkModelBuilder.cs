using Shortener.Datas;
using Shortener.Front.Models;

namespace Shortener.Front.ModelBuilders
{
    public class LinkModelBuilder : ILinkModelBuilder
    {
        public LinkModel BuildLink(Link link)
        {
            return new LinkModel
                   {
                       Key = link.Key,
                       Url = link.Url,
                       Created = link.Created,
                       CountOfRedirects = link.CountOfRedirects
                   };
        }
    }
}