namespace Tomato.NewTempProject.BLL
{
    using System;
    using System.Collections.Generic;
    using Tomato.StandardLib.MyModel;
    using Tomato.NewTempProject.Model;

    public interface IModelService
    {

        /// <summary>
        /// 查询动态连接表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<ResultModel> ListPageDynamicJoinModelList(TableSelModel model);

        /// <summary>
        /// 查询动态表视图分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<ResultModel> ListPageDynamicModelList(TableSelModel model);

        /// <summary>
        /// 批量删除动态表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<int> DelDynamicTableList(List<TableSelModel> inputModel);

        /// <summary>
        /// 新增动态模块表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<int> InsertDynamicModel(TableSelModel model);

        /// <summary>
        /// 多行修改动态模块表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<int> MultiLineUpdateDynamicModel(List<TableSelModel> model);
        #region 基础方法
        /// <summary>
        /// 获取模块表视图列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<PageModel<ModelViewModel>> ListViewPageModel(ModelViewModel model);

        /// <summary>
        /// 获取模块表列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<PageModel<ModelOutputModel>> ListPageModel(ModelInputModel model);

        /// <summary>
        /// 新增、修改模块表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultModel<ModelOutputModel> ModifyModel(ModelInputModel model);

        /// <summary>
        /// 删除模块表 (逻辑删除)
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        BaseResultModel<int> DeleteModel(List<Guid?> IDs);

        /// <summary>
        /// 获取单个模块表
        /// </summary>
        BaseResultModel<ModelViewModel> GetModel(Guid? ID);

        #endregion
    }


}
