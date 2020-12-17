namespace Tomato.NewTempProject.BLL
{
    using System;
    using System.Collections.Generic;
    using Tomato.StandardLib.MyModel;
using Tomato.NewTempProject.Model;

    public interface IAppraisalsService 
    {

        #region 基础方法
        /// <summary>
        /// 获取考核项目表视图列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<PageModel<AppraisalsViewModel>> ListViewPageAppraisals(AppraisalsViewModel model);

        /// <summary>
        /// 获取考核项目表列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<PageModel<AppraisalsOutputModel>> ListPageAppraisals(AppraisalsInputModel model);

        /// <summary>
        /// 新增、修改考核项目表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<AppraisalsOutputModel> ModifyAppraisals(AppraisalsInputModel model);

        /// <summary>
        /// 删除考核项目表 (逻辑删除)
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        BaseResultModel<int> DeleteAppraisals(List<Guid?> IDs);

        /// <summary>
        /// 获取单个考核项目表
        /// </summary>
        BaseResultModel<AppraisalsViewModel> GetAppraisals(Guid? ID);

        #endregion  
  }


}
