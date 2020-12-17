using Newtonsoft.Json.Serialization;
using Tomato.NewTempProject.WebApi.Common;
using Tomato.NewTempProject.WebApi.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务 
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling =
                Newtonsoft.Json.DateTimeZoneHandling.Local;

            // 日志
            config.Filters.Add(new LogAttribute());
            // 实现跨域访问
            config.MessageHandlers.Add(new CrosHandler());

            // Web API 路由
            config.MapHttpAttributeRoutes();

#if DEBUG
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "NewTempProject/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
#endif

#if RELEASE
             config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
#endif
        }
    }
}
