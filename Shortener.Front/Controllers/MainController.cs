using System.Web.Mvc;

namespace Shortener.Front.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}