using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tomato.StandardLib.MyModel;
using Tomato.NewTempProject.BLL;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;

namespace Tomato.NewTempProject.WebApi
{
    /// <summary>
    /// 需要登入
    /// </summary>
    public class RequestLoginAttribute : AuthorizeAttribute
    {
        string ErrorCode = "";
        string ErrorMessage = "";

        /// <summary>
        /// 需要登入
        /// </summary>
        public RequestLoginAttribute()
        {
        }

        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                string userName = ApplicationContext.Current.UserName;
                Guid userId = ApplicationContext.Current.UserId;

                if (userName != null && userId != Guid.Empty)
                {
                    //权限通过
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    //权限未通过
                    this.ErrorCode = EnumErrorCode.未登入;
                    this.ErrorMessage = "请重新登入";
                    HandleUnauthorizedRequest(actionContext);
                }

            }
            catch
            {
                //登入信息不存在
                this.ErrorCode = EnumErrorCode.未登入;
                this.ErrorMessage = "请重新登入";
                HandleUnauthorizedRequest(actionContext);
            }
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);

            var response = actionContext.Response = actionContext.Response ?? new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.Forbidden;//Http 403
            var content = new ErrorResultModel<string>();
            content.Data = "Server denied access: you have no permission or be offline";
            content.ErrorCode = this.ErrorCode;
            content.ErrorMessage = this.ErrorMessage;
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Content = new StringContent(JsonConvert.SerializeObject(content, settings), Encoding.UTF8, "application/json");
        }
    }
}
