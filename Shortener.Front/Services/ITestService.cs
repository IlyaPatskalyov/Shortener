namespace Shortener.Front.Services
{
    public interface ITestService
    {
        string[] Get();
    }

    public interface ITestService2
    {
        string Get();
    }


    public class TestService : ITestService
    {
        public string[] Get()
        {
            return new[] {"gsdfgsdf", "sdfgdsf"};
        }
    }

    public class TestService2a : ITestService2
    {
        public string Get()
        {
            return "a";
        }
    }

    public class TestService2b : ITestService2
    {
        public string Get()
        {
            return "b";
        }
    }
}