using Tomato.StandardLib.MyModel;
using System.Collections.ObjectModel;
using System.Data;

namespace Tomato.StandardLib.MyBaseClass
{
    /// <summary>
    /// 带分页的DAL基
    /// </summary>
    /// <typeparam name="T">数据库model</typeparam>
    /// <typeparam name="OutputT">返回值outputModel</typeparam>
    public interface IDALPageBase<T, OutputT>
        where T : BasePageModel, new()
        where OutputT : class, T, new()
    {
        /// <summary>
        /// 根据model的值作为查询条件,model里每一个非null字段都会作为查询条件
        /// 获取分页的数据
        /// 支持模糊查询
        /// </summary>
        /// <param name="model">作为条件的model</param>
        /// <returns>符合查询条件的分页数据集合</returns>
        PageModel<OutputT> ListPage(T model);

        /// <summary>
        /// 自定义sql查询，查询结果以分页形式返回
        /// </summary>
        /// <param name="sql">自定义原始sql语句</param>
        /// <param name="pageNO">页数</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="parms">参数</param>
        /// <param name="orderBy">排序语句,例: "ORDER BY GmtUpdateDate DESC"</param>
        /// <param name="preSql">前置sql语句</param>
        /// <returns></returns>
        PageModel<OutputT> ListPage(string sql, int pageNO = 1, int pageSize = 10, Collection<IDataParameter> parms = null, string orderBy = "", string preSql = "");

    }
}
