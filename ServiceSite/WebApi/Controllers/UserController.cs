using Microsoft.Practices.Unity;
using SeaSky.StandardLib.MyModel;
using System.Collections.Generic;
using System.Web.Http;
using SeaSky.SyTemplater.BLL;
using SeaSky.SyTemplater.Model;
using System;
using SeaSky.SyTemplater.WebApi.Log;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.ModelBinding;
using System.Text;

namespace SeaSky.SyTemplater.WebApi.Controllers
{

    public class UserController : ApiController
    {

        private IUserService UserService = ApplicationContext.Current.UnityContainer.Resolve<IUserService>();

        /// <summary>
        /// 获取用户列表分页
        /// </summary>
        /// <param name="model">用户Model</param>
        /// <returns>用户ViewModel</returns>
        public BaseResultModel<PageModel<UserViewModel>> TestModelVail(UserModel model)
        {
            try
            {

                StringBuilder errorMes = new StringBuilder();

                List<ValidationResult> erorrResults = new List<ValidationResult>();
                ValidationContext context = new ValidationContext(model, null, null);
                bool isValid = Validator.TryValidateObject(model, context, erorrResults, true);
                if (!isValid)
                {
                    foreach (ValidationResult items in erorrResults)
                    {
                        foreach (string item in items.MemberNames)
                        {
                            errorMes.Append(items.ErrorMessage + ";");
                        }
                    }
                }



                foreach (ModelState value in ModelState.Values)
                {
                    foreach (ModelError error in value.Errors)
                    {
                        errorMes.Append(error.ErrorMessage + ";");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        /// <summary>
        /// 获取用户列表分页
        /// </summary>
        /// <param name="viewModel">用户ViewModel</param>
        /// <returns>用户ViewModel</returns>
        public BaseResultModel<PageModel<UserViewModel>> ListViewPageUser(UserViewModel viewModel)
        {
            return UserService.ListViewPageUser(viewModel);
        }

        /// <summary>
        /// 新增、修改用户
        /// </summary>
        /// <param name="model">用户Model</param>
        /// <returns>返回新增、修改过的用户</returns>
        public BaseResultModel<UserOutputModel> ModifyUser(UserInputModel model)
        {
            if (model == null)
            {
                return new ErrorResultModel<UserOutputModel>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            ModelAttrEx arrtEx = new ModelAttrEx();
            string modelErrorMes = "";


            modelErrorMes += arrtEx.EditAttrVaild<UserInputModel>(ModelState, model);


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
        /// <param name="IDs">用户Guid-->List</param>
        /// <returns>返回删除的行数</returns>
        [HttpPost]
        public BaseResultModel<int> DeleteUser(List<Guid?> IDs)
        {
            if (IDs == null || IDs.Count == 0)
            {
                return new ErrorResultModel<int>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            return UserService.DeleteUser(IDs);
        }

        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="ID">用户Guid-->List</param>
        /// <returns>单个用户Model</returns>
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
