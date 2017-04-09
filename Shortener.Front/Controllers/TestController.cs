using System.Web.Http;
using Shortener.Front.Services;

namespace Shortener.Front.Controllers
{
    public class TestController : ApiController
    {
        private readonly ITestService testService;


        public TestController(ITestService testService)
        {
            this.testService = testService;
        }

        [HttpGet]
        public string[] Index()
        {
            return testService.Get();
        }
    }
}