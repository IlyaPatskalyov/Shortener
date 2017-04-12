using System.Web.Mvc;
using Shortener.Front.Settings;
using Shortener.Storage;

namespace Shortener.Front.Controllers
{
    public class MainController : Controller
    {
        private readonly ILinksRepository linksRepository;
        private readonly IApplicationSettings applicationSettings;

        public MainController(ILinksRepository linksRepository, IApplicationSettings applicationSettings)
        {
            this.linksRepository = linksRepository;
            this.applicationSettings = applicationSettings;
        }

        public ActionResult Index()
        {
            return View(applicationSettings.FrontendMode);
        }

        public ActionResult Link(string key)
        {
            var link = linksRepository.GetByKeyAndIncrement(key);
            if (link != null)
                return Redirect(link.Url);
            return View("NotFound");
        }
    }
}