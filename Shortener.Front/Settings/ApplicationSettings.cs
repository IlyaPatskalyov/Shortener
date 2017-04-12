using System.IO;
using System.Web;
using Shortener.Front.Models;
using Shortener.Storage.EF;

namespace Shortener.Front.Settings
{
    public interface IApplicationSettings
    {
        string LogPath { get; }
        FrontendMode FrontendMode { get; }
    }

    public class ApplicationSettings : IDbSettings, IApplicationSettings
    {
        public string ConnectionString { get; private set;  }

        public string LogPath { get; private set; }

        public FrontendMode FrontendMode { get; private set;  }

        public ApplicationSettings()
        {
            ConnectionString = @"Data Source=D:\RiderProjects\WebApplication.db;Foreign keys=True;Version=3";
            FrontendMode = FrontendMode.Deployed;
            LogPath = Path.Combine(HttpRuntime.AppDomainAppPath, @"..\logs\log-{Date}.txt");
        }


    }
}