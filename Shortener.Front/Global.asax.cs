using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Serilog;
using Serilog.Events;
using SerilogWeb.Classic;
using SerilogWeb.Classic.Enrichers;

namespace Shortener.Front
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            var frontAssembly = typeof(Global).Assembly;

            builder.RegisterControllers(frontAssembly);
            builder.RegisterApiControllers(frontAssembly);
            builder.RegisterAssemblyTypes(frontAssembly).AsImplementedInterfaces();

            var container = builder.Build();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(Path.Combine(HttpRuntime.AppDomainAppPath, @"..\logs\log-{Date}.txt"))
                .Enrich.With<HttpRequestIdEnricher>()
                .Enrich.With<HttpRequestTraceIdEnricher>()
                .Enrich.With<UserNameEnricher>()
                .CreateLogger();

            ApplicationLifecycleModule.RequestLoggingLevel = LogEventLevel.Debug;
            ApplicationLifecycleModule.LogPostedFormData = LogPostedFormDataOption.OnlyOnError;
            ApplicationLifecycleModule.FilterPasswordsInFormData = false;
        }
    }

    public class AssembliesLoader
    {
        public static string GetBinPath()
        {
            var relativeSearchPath = AppDomain.CurrentDomain.RelativeSearchPath;
            if (String.IsNullOrEmpty(relativeSearchPath))
                return AppDomain.CurrentDomain.BaseDirectory;
            if (relativeSearchPath.Contains("ReSharperPlatform"))
                return AppDomain.CurrentDomain.BaseDirectory;
            return AppDomain.CurrentDomain.RelativeSearchPath;
        }

        public static Assembly[] LoadFromBinDirectory()
        {
            string binPath = GetBinPath();

            var locations = new List<string>();
            locations.AddRange(Directory.GetFiles(binPath, "*.dll", SearchOption.TopDirectoryOnly));
            locations.AddRange(Directory.GetFiles(binPath, "*.exe", SearchOption.TopDirectoryOnly));


            var result = new List<Assembly>();
            foreach (string dllFile in locations)
            {
                try
                {
                    var assembly = Assembly.Load(AssemblyName.GetAssemblyName(dllFile));
                    result.Add(assembly);
                }
                catch (BadImageFormatException)
                {
                    //   logger.InfoFormat("Can't load file {0}", dllFile);
                    continue;
                }
                //logger.InfoFormat("Assembly {0} loaded", assemblyName);
            }
            return result.ToArray();
        }
    }
}