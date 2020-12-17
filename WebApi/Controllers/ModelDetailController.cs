using Microsoft.Practices.Unity;
using Tomato.StandardLib.MyModel;
using System.Collections.Generic;
using System.Web.Http;
using Tomato.NewTempProject.BLL;
using Tomato.NewTempProject.Model;
using System;
using Tomato.NewTempProject.WebApi.Log;
using System.Net.Http;
using Tomato.StandardLib.MyExtensions;
namespace Tomato.NewTempProject.WebApi.Controllers
{

    public class ModelDetailController : ApiController
    {

        private IModelDetailService ModelDetailService = ApplicationContext.Current.UnityContainer.Resolve<IModelDetailService>();


        #region 基础方法

        /// <summary>
        /// 获取模块明细表视图列表分页
        /// </summary>
        /// <param name="model">ViewModel</param>
        /// <returns>ViewModel</returns>
        public BaseResultModel<PageModel<ModelDetailViewModel>> ListViewPageModelDetail(ModelDetailViewModel model)
        {
            return ModelDetailService.ListViewPageModelDetail(model);
        }

        /// <summary>
        /// 获取模块明细表列表分页
        /// </summary>
        /// <param name="model">InputModel</param>
        /// <returns>OutputModel</returns>
        public BaseResultModel<PageModel<ModelDetailOutputModel>> ListPageModelDetail(ModelDetailInputModel model)
        {
            return ModelDetailService.ListPageModelDetail(model);
        }

        /// <summary>
        /// 新增、修改模块明细表
        /// </summary>
        /// <param name="model">OutputModel</param>
        /// <returns>OutputModel</returns>
        public BaseResultModel<ModelDetailOutputModel> ModifyModelDetail(ModelDetailInputModel model)
        {
            if (model == null)
            {
                return new ErrorResultModel<ModelDetailOutputModel>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            ModelAttrEx arrtEx = new ModelAttrEx();
            string modelErrorMes = "";
            if (model.ModelDetailID.IsNullOrEmpty())
            {
                modelErrorMes += arrtEx.AddAttrVaild<ModelDetailInputModel>(ModelState, model);
            }
            else
            {
                modelErrorMes += arrtEx.EditAttrVaild<ModelDetailInputModel>(ModelState, model);
            }
            if (!modelErrorMes.IsNullOrEmpty())
            {
                return new ErrorResultModel<ModelDetailOutputModel>(EnumErrorCode.请求参数错误, modelErrorMes);
            }
            return ModelDetailService.ModifyModelDetail(model);
        }

        /// <summary>
        /// 删除模块明细表 (逻辑删除)
        /// </summary>
        /// <param name="IDs">List-->Guid?</param>
        /// <returns>受影响行数</returns>
        [HttpPost]
        public BaseResultModel<int> DeleteModelDetail(List<Guid?> IDs)
        {
            if (IDs == null || IDs.Count == 0)
            {
                return new ErrorResultModel<int>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            return ModelDetailService.DeleteModelDetail(IDs);
        }

        /// <summary>
        /// 获取单个模块明细表
        /// </summary>
        /// <param name="ID">Guid?</param>
        /// <returns>ViewModel</returns>
        [HttpPost]
        public BaseResultModel<ModelDetailViewModel> GetModelDetail(Guid? ID)
        {
            if (ID.IsNullOrEmpty())
            {
                return new ErrorResultModel<ModelDetailViewModel>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            return ModelDetailService.GetModelDetail(ID);
        }
        #endregion
    }


}
