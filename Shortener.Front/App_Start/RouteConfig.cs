using System.Web.Mvc;
using System.Web.Routing;

namespace Shortener.Front
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                "Default",
                "",
                new {controller = "Main", action = "Index", id = UrlParameter.Optional}
            );
            routes.MapRoute("Key", "{key}", new {controller = "Main", action = "Link", key = UrlParameter.Optional});
            routes.MapRoute(
                "DefaultController",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}