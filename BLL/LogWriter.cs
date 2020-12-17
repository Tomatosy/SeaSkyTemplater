using Tomato.NewTempProject.Model.Enum;
using System;
using System.Web;
using TGLog;

namespace Tomato.NewTempProject.BLL
{
    public class LogWriter
    {
        public static log4net.ILog log = log4net.LogManager.GetLogger("ReflectionLayout");

        public static void WriteLog(EnumLogLevel logLevel, string methedName, string actionName,
            string controllerName, string message, Exception exception)
        {
            var request = HttpContext.Current.Request;
            string user;
            try
            {
                user = ApplicationContext.Current.UserName;
            }
            catch
            {
                user = "login";
            }
            var logMessage = new LogMessage(0, user, 0
                    , string.Format("{3}, 控制器:{0}，动作：{1}，Message：{2}"
                                    , controllerName, actionName, message, methedName)
                    , request.UserHostAddress, request.Browser.Platform
                    , request.Browser.Browser + request.Browser.Version);

            switch (logLevel)
            {
                case EnumLogLevel.Fatal:
                    {
                        log.Fatal(logMessage, exception);
                        break;
                    }
                case EnumLogLevel.Error:
                    {
                        log.Error(logMessage, exception);
                        break;
                    }
                case EnumLogLevel.Warning:
                    {
                        log.Warn(logMessage);
                        break;
                    }
                case EnumLogLevel.Info:
                    {
                        log.Info(logMessage);
                        break;

                    }
                case EnumLogLevel.Debug:
                    {
                        log.Debug(logMessage);
                        break;
                    }
                default:
                    {
                        log.Fatal(new LogMessage(0, user, 0
                    , string.Format("{0},logLevel error", methedName)
                    , request.UserHostAddress, request.Browser.Platform
                    , request.Browser.Browser + request.Browser.Version), exception);
                        break;
                    }
            }
        }
    }
}
