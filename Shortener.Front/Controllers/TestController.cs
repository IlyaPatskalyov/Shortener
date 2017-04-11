using System.Web.Http;
using Shortener.Front.Services;
using Shortener.Storage;

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