namespace Tomato.NewTempProject.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Tomato.StandardLib.MyBaseClass;
    using Tomato.StandardLib.MyModel;
    using Tomato.NewTempProject.Model;
    using System.Data;

    public interface IModelRepository : IDALBaseNew<ModelModel, ModelOutputModel, ModelViewModel>, IDALPageBaseNew<ModelModel, ModelOutputModel, ModelViewModel>
    {
        /// <summary>
        /// 获取动态连接表数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        DataTable GetDynamicJoinTableData(TableSelModel model);

        /// <summary>
        /// 获取动态表数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        DataTable GetDynamicTableData(TableSelModel model);

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

        /// <summary>
        /// 删除数据库表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void DelDBTable(ModelInputModel model);
    }


}

