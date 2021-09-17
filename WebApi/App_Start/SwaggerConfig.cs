using System.Web.Http;
using WebActivatorEx;
using Tomato.NewTempProject.WebApi;
using Swashbuckle.Application;
using System.Linq;
using System.Reflection;
using System;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Tomato.NewTempProject.WebApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            Assembly thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    //c.SingleApiVersion("v1.1", "SSky.NewTempProject.WebApi");
                    c.MultipleApiVersions(
                    (apiDesc, targetApiVersion) => targetApiVersion.Equals("default", StringComparison.InvariantCultureIgnoreCase) ||                     // Include everything by default
                          apiDesc.Route.RouteTemplate.StartsWith(targetApiVersion, StringComparison.InvariantCultureIgnoreCase), // Only include matching routes for other versions
                         (vc) =>
                         {
                             vc.Version("default", "Tomato.NewTempProject.WebApi");
                         });

                    //c.SingleApiVersion("v1.1", "Tomato.NewTempProject.WebApi");

                    string xmlFile1 = string.Format("{0}/App_Data/Tomato.NewTempProject.WebApi.XML", System.AppDomain.CurrentDomain.BaseDirectory);
                    string xmlFile2 = string.Format("{0}/App_Data/Tomato.NewTempProject.Model.XML", System.AppDomain.CurrentDomain.BaseDirectory);
                    if (System.IO.File.Exists(xmlFile1))
                    {
                        c.IncludeXmlComments(xmlFile1);
                    }
                    if (System.IO.File.Exists(xmlFile2))
                    {
                        c.IncludeXmlComments(xmlFile2);
                    }
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    c.CustomProvider((defaultProvider) => new SwaggerControllerDescProvider(defaultProvider, xmlFile1));
                })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(Assembly.GetExecutingAssembly(), "Tomato.NewTempProject.WebApi.Scripts.SwaggerConfig.js");

                });
        }
    }
}
