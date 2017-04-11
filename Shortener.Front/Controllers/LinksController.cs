using System.Web.Http;
using System.Web.Http.ModelBinding;
using Shortener.Datas;
using Shortener.Front.Sessions;
using Shortener.Storage;

namespace Shortener.Front.Controllers
{
    public class LinksController : ApiController
    {
        private readonly ILinksRepository linksRepository;

        public LinksController(ILinksRepository linksRepository)
        {
            this.linksRepository = linksRepository;
        }

        [HttpGet]
        public Session Test([ModelBinder] Session session)
        {
            return session;
        }

        [HttpPost]
        public string Add([ModelBinder] Session session, [FromBody] string url)
        {
            return linksRepository.Create(url, session.UserId).Key;
        }

        [HttpGet]
        public Link[] Get([ModelBinder] Session session)
        {
            return linksRepository.GetLinksByUserId(session.UserId);
        }
    }
}