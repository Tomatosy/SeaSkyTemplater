using Tomato.StandardLib.MyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Tomato.NewTempProject.WebApi.Common
{
    public static class ModelStateValid
    {
        public static BaseResultModel<T> ModelValid<T>(this ApiController api)
        {
            var result = "";
            if (api.ModelState.IsValid)
            {
                return new SuccessResultModel<T>();
            }

            foreach (var value in api.ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    result += error.ErrorMessage;
                }
            }
            return new ErrorResultModel<T>(EnumErrorCode.参数校验未通过, result);
        }
    }
}