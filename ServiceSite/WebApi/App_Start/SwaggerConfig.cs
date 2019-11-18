using System.Web.Http;
using WebActivatorEx;
using SeaSky.SyTemplater.WebApi;
using Swashbuckle.Application;
using System.Linq;
using System.Reflection;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace SeaSky.SyTemplater.WebApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            Assembly thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {

                        c.SingleApiVersion("v2", "SeaSky.SyTemplater.WebApi");
                        //Ìí¼ÓÏÂÊö´úÂë                        
                        string xmlFile = string.Format("{0}/bin/SeaSky.SyTemplater.WebApi.XML", System.AppDomain.CurrentDomain.BaseDirectory);
                        if (System.IO.File.Exists(xmlFile))
                        {
                            c.IncludeXmlComments(xmlFile);
                        }
                        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                        c.CustomProvider((defaultProvider) => new SwaggerControllerDescProvider(defaultProvider, xmlFile));
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.InjectJavaScript(Assembly.GetExecutingAssembly(), "SeaSky.SyTemplater.WebApi.Scripts.SwaggerConfig.js");

                    });
        }
    }
}
