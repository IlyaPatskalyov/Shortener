using System;
using System.Configuration;
using System.IO;
using System.Web;
using Shortener.Front.Models;

namespace Shortener.Front.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        public string ConnectionString { get; private set; }

        public string LogPath { get; private set; }

        public FrontendMode FrontendMode { get; private set; }

        public ApplicationSettings()
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            FrontendMode frontendMode;
            FrontendMode = Enum.TryParse(ConfigurationManager.AppSettings["FrontendMode"], out frontendMode) ? frontendMode : FrontendMode.Deployed;
            LogPath = Path.Combine(HttpRuntime.AppDomainAppPath, ConfigurationManager.AppSettings["LogPath"]);
        }
    }
}