using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Shortener.Front.ModelBuilders;
using Shortener.Front.Models;
using Shortener.Front.Sessions;
using Shortener.Storage;

namespace Shortener.Front.Controllers
{
    public class LinksController : ApiController
    {
        private readonly ILinksRepository linksRepository;
        private readonly ILinkModelBuilder linkModelBuilder;

        public LinksController(ILinksRepository linksRepository, ILinkModelBuilder linkModelBuilder)
        {
            this.linksRepository = linksRepository;
            this.linkModelBuilder = linkModelBuilder;
        }

        [HttpGet]
        public Session Test([ModelBinder] Session session)
        {
            return session;
        }

        [HttpPost]
        public string Add([ModelBinder] Session session, [FromBody] string url)
        {
            Uri uriResult;
            if (!(Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                  && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                                                {
                                                    Content = new StringContent("URL is incorrect")
                                                });
            }
            return linksRepository.Create(url, session.UserId).Key;
        }

        [HttpGet]
        public LinkModel[] Get([ModelBinder] Session session)
        {
            return linksRepository.GetLinksByUserId(session.UserId)
                                  .Select(linkModelBuilder.BuildLink)
                                  .ToArray();
        }
    }
}