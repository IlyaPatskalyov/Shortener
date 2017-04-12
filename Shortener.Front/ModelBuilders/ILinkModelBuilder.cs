using Shortener.Datas;
using Shortener.Front.Models;

namespace Shortener.Front.ModelBuilders
{
    public interface ILinkModelBuilder
    {
        LinkModel BuildLink(Link link);
    }
}