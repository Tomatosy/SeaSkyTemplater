using Microsoft.Practices.Unity;
using Tomato.StandardLib.MyModel;
using System.Collections.Generic;
using System.Web.Http;
using Tomato.NewTempProject.BLL;
using Tomato.NewTempProject.Model;
using System;
using Tomato.NewTempProject.WebApi.Log;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Reflection;

namespace Tomato.NewTempProject.WebApi.Controllers
{

    public class MenuController : ApiController
    {

        private IMenuService MenuService = ApplicationContext.Current.UnityContainer.Resolve<IMenuService>();

        /// <summary>
        /// 获取菜单列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<PageModel<MenuOutputModel>> ListPageMenu(MenuOutputModel model)
        {
            return MenuService.ListPageMenu(model);
        }

        /// <summary>
        /// 新增、修改菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<MenuOutputModel> ModifyMenu(MenuInputModel model)
        {
            if (model == null)
            {
                return new ErrorResultModel<MenuOutputModel>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            ModelAttrEx arrtEx = new ModelAttrEx();
            string modelErrorMes = "";
            if (model.MenuID.IsNullOrEmpty())
            {
                modelErrorMes += arrtEx.AddAttrVaild<MenuInputModel>(ModelState, model);
            }
            else
            {
                modelErrorMes += arrtEx.EditAttrVaild<MenuInputModel>(ModelState, model);
            }
            if (!modelErrorMes.IsNullOrEmpty())
            {
                return new ErrorResultModel<MenuOutputModel>(EnumErrorCode.请求参数错误, modelErrorMes);
            }
            return MenuService.ModifyMenu(model);
        }

        /// <summary>
        /// 删除菜单 (逻辑删除)
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResultModel<int> DeleteMenu(List<Guid?> IDs)
        {
            if (IDs == null || IDs.Count == 0)
            {
                return new ErrorResultModel<int>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            return MenuService.DeleteMenu(IDs);
        }

        /// <summary>
        /// 获取单个项目管理
        /// </summary>
        [HttpPost]
        public BaseResultModel<MenuOutputModel> GetMenu(Guid? ID)
        {
            if (ID.IsNullOrEmpty())
            {
                return new ErrorResultModel<MenuOutputModel>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            return MenuService.GetMenu(ID);
        }

        /// <summary>
        /// 查询所有有效的菜单树
        /// </summary>
        /// <returns></returns>
        public BaseResultModel<List<MenuOutputModel>> ListAllMenuTree()
        {
            return MenuService.ListAllMenuTree();
        }



        [HttpPost]

        public string test(MenuInputModel t)
        {

            string versionNo = AssemblyName.GetAssemblyName("").Version.ToString();
            //using BaseUtil = Tomato.FinanceMonitor.DLL.BaseUtil;

            HttpContext.Current.Server.MapPath("");
            //C:\inetpub\wwwroot\

            var asdf = System.AppDomain.CurrentDomain.BaseDirectory;
            //d:\项目

            //BaseResultModel<string> validResult = ValidationData.ValidationModel(model);
            //BaseResultModel<string> validResult = ValidationData.ValidationField(model, "AuditTypeID1", "AuditFormID1", "ProjectsManageDesc");
            string result = "";
            if (!ModelState.IsValid)
            {
                foreach (System.Web.Http.ModelBinding.ModelState value in ModelState.Values)
                {
                    foreach (System.Web.Http.ModelBinding.ModelError error in value.Errors)
                    {
                        result += error.ErrorMessage;
                    }
                }
            }

            List<ValidationResult> erorrResults = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(t, null, null);
            bool isValid = Validator.TryValidateObject(t, context, erorrResults, true);

            return result;
        }
    }


}
