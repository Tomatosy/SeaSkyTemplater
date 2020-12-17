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

    public class AppraisalsController : ApiController
    {

        private IAppraisalsService AppraisalsService = ApplicationContext.Current.UnityContainer.Resolve<IAppraisalsService>();


        #region 基础方法
		
        /// <summary>
        /// 获取考核项目表视图列表分页
        /// </summary>
        /// <param name="model">ViewModel</param>
        /// <returns>ViewModel</returns>
        public BaseResultModel<PageModel<AppraisalsViewModel>> ListViewPageAppraisals(AppraisalsViewModel model)
        {
            return AppraisalsService.ListViewPageAppraisals(model);
        }
		
        /// <summary>
        /// 获取考核项目表列表分页
        /// </summary>
        /// <param name="model">InputModel</param>
        /// <returns>OutputModel</returns>
        public BaseResultModel<PageModel<AppraisalsOutputModel>> ListPageAppraisals(AppraisalsInputModel model)
        {
            return AppraisalsService.ListPageAppraisals(model);
        }

        /// <summary>
        /// 新增、修改考核项目表
        /// </summary>
        /// <param name="model">OutputModel</param>
        /// <returns>OutputModel</returns>
        public BaseResultModel<AppraisalsOutputModel> ModifyAppraisals(AppraisalsInputModel model)
        {
            if (model == null)
            {
                return new ErrorResultModel<AppraisalsOutputModel>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            ModelAttrEx arrtEx = new ModelAttrEx();
            string modelErrorMes = "";
            if (model.AppraisalsID.IsNullOrEmpty())
            {
                modelErrorMes += arrtEx.AddAttrVaild<AppraisalsInputModel>(ModelState, model);
            }
            else
            {
                modelErrorMes += arrtEx.EditAttrVaild<AppraisalsInputModel>(ModelState, model);
            }
            if (!modelErrorMes.IsNullOrEmpty())
            {
                return new ErrorResultModel<AppraisalsOutputModel>(EnumErrorCode.请求参数错误, modelErrorMes);
            }
            return AppraisalsService.ModifyAppraisals(model);
        }

        /// <summary>
        /// 删除考核项目表 (逻辑删除)
        /// </summary>
        /// <param name="IDs">List-->Guid?</param>
        /// <returns>受影响行数</returns>
        [HttpPost]
        public BaseResultModel<int> DeleteAppraisals(List<Guid?> IDs)
        {
            if (IDs==null||IDs.Count == 0)
            {
                return new ErrorResultModel<int>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            return AppraisalsService.DeleteAppraisals(IDs);
        }

        /// <summary>
        /// 获取单个考核项目表
        /// </summary>
        /// <param name="ID">Guid?</param>
        /// <returns>ViewModel</returns>
        [HttpPost]
        public BaseResultModel<AppraisalsViewModel> GetAppraisals(Guid? ID)
        {
            if (ID.IsNullOrEmpty())
            {
                return new ErrorResultModel<AppraisalsViewModel>(EnumErrorCode.请求参数错误, "参数不能为空");
            }
            return AppraisalsService.GetAppraisals(ID);
        }
        #endregion
    }


}
