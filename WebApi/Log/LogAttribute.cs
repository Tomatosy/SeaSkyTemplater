using Tomato.NewTempProject.BLL;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TGLog;

namespace Tomato.NewTempProject.WebApi.Log
{
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            if (!SkipLogging(filterContext))
            {
                //获取action名称
                string actionName = filterContext.ActionDescriptor.ActionName;
                //获取Controller 名称
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                //获取触发当前方法的Action方法的所有参数 
                var paramss = filterContext.ActionArguments;
                string Content = Newtonsoft.Json.JsonConvert.SerializeObject(paramss);

                var request = HttpContext.Current.Request;

                string user;
                try
                {
                    user = ApplicationContext.Current.UserName;
                }
                catch
                {
                    user = "UnName";
                }

                LogWriter.log.Info(new LogMessage(0, user, 0
                    , string.Format("OnActionExecuting, 控制器:{0}，动作：{1}，参数：{2}"
                                    , controllerName, actionName, Content)
                    , request.UserHostAddress, request.Browser.Platform
                    , request.Browser.Browser + request.Browser.Version));
            }
            base.OnActionExecuting(filterContext);
        }


        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (!SkipLogging(actionExecutedContext))
            {
                //获取action名称
                string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
                //获取Controller 名称
                string controllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                //获取触发当前方法的Action方法的所有参数 
                var exception = actionExecutedContext.Exception;
                var request = HttpContext.Current.Request;

                string Content = "";
                if (exception == null)
                {
                    Content = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;
                }

                string user;
                try
                {
                    user = ApplicationContext.Current.UserName;
                }
                catch
                {
                    user = "UnName";
                }

                var logMessage = new LogMessage(0, user, 0
                        , string.Format("OnActionExecuted, 控制器:{0}，动作：{1}，结果：{2}"
                                        , controllerName, actionName, Content, exception)
                        , request.UserHostAddress, request.Browser.Platform
                        , request.Browser.Browser + request.Browser.Version);

                if (exception != null)
                {
                    LogWriter.log.Fatal(logMessage, exception);
                }
                else
                {
                    LogWriter.log.Info(logMessage);
                }
            }
            base.OnActionExecuted(actionExecutedContext);
        }


        /// <summary>
        /// 判断控制器和Action是否要进行拦截
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private static bool SkipLogging(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<NoLogAttribute>().Any() || actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<NoLogAttribute>().Any();
        }

        /// <summary>
        /// 判断控制器和Action是否要进行拦截
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private static bool SkipLogging(HttpActionExecutedContext actionContext)
        {
            return actionContext.ActionContext.ActionDescriptor.GetCustomAttributes<NoLogAttribute>().Any() || actionContext.ActionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<NoLogAttribute>().Any();
        }
    }
}