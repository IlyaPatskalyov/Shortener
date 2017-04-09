using System.Web.Mvc;
using Serilog;
using Shortener.Front.Services;

namespace Shortener.Front.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestService testService;

        public HomeController(ITestService testService)
        {
            this.testService = testService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var count = 456;
            Log.Information("Retrieved {Count} records", count);

            var fruit = new[] { "Apple", "Pear", "Orange" };
            Log.Information("In my bowl I have {Fruit}", fruit);

            ViewBag.Message = "Your application description page. " + string.Join(", ", testService.Get());

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}