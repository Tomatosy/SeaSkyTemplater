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

    public interface IModelDetailRepository : IDALBaseNew<ModelDetailModel, ModelDetailOutputModel, ModelDetailViewModel>, IDALPageBaseNew<ModelDetailModel, ModelDetailOutputModel, ModelDetailViewModel>
    {
        /// <summary>
        /// 初始化数据库表明细字段
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void InitDBTable(ModelInputModel model);

        /// <summary>
        /// 新增字段列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddDynamicTableCol(ModelDetailViewModel model);

        /// <summary>
        /// 修改字段列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ModifyDynamicTableCol(ModelDetailViewModel model);

        /// <summary>
        /// 判断字段列是否有数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ListDynamicTableListByPer(ModelDetailViewModel model);
        /// <summary>
        /// 删除数据字段列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool DropDynamicTableCol(ModelDetailViewModel model);

    }


}

