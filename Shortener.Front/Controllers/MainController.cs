using System.Linq;
using System.Web.Mvc;
using Serilog;
using Shortener.Storage;

namespace Shortener.Front.Controllers
{
    public class MainController : Controller
    {
        private readonly ILinksRepository linksRepository;

        public MainController(ILinksRepository linksRepository)
        {
            this.linksRepository = linksRepository;
        }

        public ActionResult Index()
        {
            var link = linksRepository.Create("http://stackoverflow.com/questions/21573550/setting-unique-constraint-with-fluent-api");
            Log.Information("Save {@link}", link);
            return View();
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