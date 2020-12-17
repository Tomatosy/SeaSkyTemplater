namespace Tomato.NewTempProject.BLL
{
    using System;
    using System.Collections.Generic;
    using Tomato.StandardLib.MyModel;
using Tomato.NewTempProject.Model;

    public interface IModelDetailService 
    {

        #region 基础方法
        /// <summary>
        /// 获取模块明细表视图列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<PageModel<ModelDetailViewModel>> ListViewPageModelDetail(ModelDetailViewModel model);

        /// <summary>
        /// 获取模块明细表列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<PageModel<ModelDetailOutputModel>> ListPageModelDetail(ModelDetailInputModel model);

        /// <summary>
        /// 新增、修改模块明细表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<ModelDetailOutputModel> ModifyModelDetail(ModelDetailInputModel model);

        /// <summary>
        /// 删除模块明细表 (逻辑删除)
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        BaseResultModel<int> DeleteModelDetail(List<Guid?> IDs);

        /// <summary>
        /// 获取单个模块明细表
        /// </summary>
        BaseResultModel<ModelDetailViewModel> GetModelDetail(Guid? ID);

        #endregion  
  }


}
