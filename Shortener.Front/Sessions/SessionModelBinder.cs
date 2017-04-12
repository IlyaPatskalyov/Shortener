using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Shortener.Front.Sessions
{
    public class SessionModelBinder : IModelBinder
    {
        private const string CookieName = "UserId";

        private static HttpContextWrapper GetHttpContextWrapper(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
                return ((HttpContextWrapper) request.Properties["MS_HttpContext"]);
            if (HttpContext.Current != null)
                return new HttpContextWrapper(HttpContext.Current);
            return null;
        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(Session))
                return false;

            Guid userId;
            if (!TryGetUserId(actionContext, out userId))
            {
                userId = Guid.NewGuid();
                var response = GetHttpContextWrapper(actionContext.Request).Response;
                response.Cookies.Add(new HttpCookie(CookieName, userId.ToString())
                                     {
                                         HttpOnly = true,
                                         Expires = DateTime.MaxValue
                                     });
            }

            bindingContext.Model = new Session
                                   {
                                       UserId = userId
                                   };

            return true;
        }

        private static bool TryGetUserId(HttpActionContext actionContext, out Guid userId)
        {
            var userIdCookie = actionContext.Request.Headers.GetCookies(CookieName).FirstOrDefault();
            if (userIdCookie != null)
                return Guid.TryParse(userIdCookie[CookieName].Value, out userId);

            userId = Guid.Empty;
            return false;
        }
    }
}