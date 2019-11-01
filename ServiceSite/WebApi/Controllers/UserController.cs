using Microsoft.Practices.Unity;
using SeaSky.StandardLib.MyModel;
using System.Collections.Generic;
using System.Web.Http;
using SeaSky.SyTemplater.BLL;
using SeaSky.SyTemplater.Model;
using System;
using SeaSky.SyTemplater.WebApi.Log;
using System.Net.Http;
namespace SeaSky.SyTemplater.WebApi.Controllers
{

    public class UserController : ApiController
    {

        private IUserService UserService = ApplicationContext.Current.UnityContainer.Resolve<IUserService>();

        /// <summary>
        /// 获取用户列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<PageModel<UserOutputModel>> ListPageUser(UserOutputModel model)
        {
            return UserService.ListPageUser(model);
        }

        /// <summary>
        /// 新增、修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<UserOutputModel> ModifyUser(UserInputModel model)
        {
            if (model == null)
            {
                return new ErrorResultModel<UserOutputModel>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            ModelAttrEx arrtEx = new ModelAttrEx();
            string modelErrorMes = "";
            if (model.UserID.IsNullOrEmpty())
            {
                modelErrorMes += arrtEx.AddAttrVaild<UserInputModel>(ModelState, model);
            }
            else
            {
                modelErrorMes += arrtEx.EditAttrVaild<UserInputModel>(ModelState, model);
            }
            if (!modelErrorMes.IsNullOrEmpty())
            {
                return new ErrorResultModel<UserOutputModel>(EnumErrorCode.请求参数错误, modelErrorMes);
            }
            return UserService.ModifyUser(model);
        }

        /// <summary>
        /// 删除用户 (逻辑删除)
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultModel<int> DeleteUser(List<Guid?> IDs)
        {
            if (IDs==null||IDs.Count == 0)
            {
                return new ErrorResultModel<int>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            return UserService.DeleteUser(IDs);
        }

        /// <summary>
        /// 获取单个用户
        /// </summary>
        [HttpPost]
        public BaseResultModel<UserOutputModel> GetUser(Guid? ID)
        {
            if (ID.IsNullOrEmpty())
            {
                return new ErrorResultModel<UserOutputModel>(EnumErrorCode.请求参数错误, "删除项不能为空");
            }
            return UserService.GetUser(ID);
        }
    }


}
