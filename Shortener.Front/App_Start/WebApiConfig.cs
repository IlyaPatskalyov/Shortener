using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Routing;
using Newtonsoft.Json;
using Shortener.Front.Sessions;

namespace Shortener.Front
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ConfigureFormatters(config);
            ConfigureRoutes(config);
            ConfigureServices(config);
        }

        private static void ConfigureServices(HttpConfiguration config)
        {
            config.Services.Insert(typeof(ModelBinderProvider), 0,
                                   new SimpleModelBinderProvider(typeof(Session), new SessionModelBinder()));
        }

        private static void ConfigureRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "ApiGetRoute", "api/{controller}/{action}", new {action = "Get"},
                constraints: new {httpMethod = new HttpMethodConstraint(HttpMethod.Get)}
            );
            config.Routes.MapHttpRoute(
                "ApiPostRoute", "api/{controller}/{action}", new {action = "Post"},
                constraints: new {httpMethod = new HttpMethodConstraint(HttpMethod.Post, HttpMethod.Put)}
            );
        }

        private static void ConfigureFormatters(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
                                                                 {
                                                                     Formatting = Formatting.Indented,
                                                                 };
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
                new Newtonsoft.Json.Converters.StringEnumConverter {CamelCaseText = true});

            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}