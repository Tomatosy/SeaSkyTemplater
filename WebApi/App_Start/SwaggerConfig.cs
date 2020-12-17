using System.Web.Http;
using WebActivatorEx;
using Tomato.NewTempProject.WebApi;
using Swashbuckle.Application;
using System.Linq;
using System.Reflection;

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

                        c.SingleApiVersion("v2", "Tomato.NewTempProject.WebApi");
                        //������������                        
                        string xmlFile = string.Format("{0}/bin/Tomato.NewTempProject.WebApi.XML", System.AppDomain.CurrentDomain.BaseDirectory);
                        if (System.IO.File.Exists(xmlFile))
                        {
                            c.IncludeXmlComments(xmlFile);
                        }
                        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                        c.CustomProvider((defaultProvider) => new SwaggerControllerDescProvider(defaultProvider, xmlFile));
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.InjectJavaScript(Assembly.GetExecutingAssembly(), "Tomato.NewTempProject.WebApi.Scripts.SwaggerConfig.js");

                    });
        }
    }
}
