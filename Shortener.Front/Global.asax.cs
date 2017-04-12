using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Serilog;
using Serilog.Events;
using SerilogWeb.Classic;
using SerilogWeb.Classic.Enrichers;
using Shortener.Front.Settings;
using Shortener.Storage.EF;
using Shortener.Storage.SQLite;

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

            builder.RegisterType<SQLiteDbContext>().As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Loader.LoadFromBinDirectory("Shortener*.dll")).AsImplementedInterfaces();

            var container = builder.Build();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(container.Resolve<IApplicationSettings>().LogPath)
                .Enrich.With<HttpRequestIdEnricher>()
                .Enrich.With<HttpRequestTraceIdEnricher>()
                .Enrich.With<UserNameEnricher>()
                .CreateLogger();

            ApplicationLifecycleModule.RequestLoggingLevel = LogEventLevel.Debug;
            ApplicationLifecycleModule.LogPostedFormData = LogPostedFormDataOption.OnlyOnError;
            ApplicationLifecycleModule.FilterPasswordsInFormData = false;
        }

        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }
    }
}