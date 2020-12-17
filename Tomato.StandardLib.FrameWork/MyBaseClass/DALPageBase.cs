using Tomato.StandardLib.DAL.Base;
using Tomato.StandardLib.MyModel;
using System.Collections.ObjectModel;
using System.Data;

namespace Tomato.StandardLib.MyBaseClass
{
    /// <summary>
    /// 用于分页的DAL基
    /// </summary>
    /// <typeparam name="Model">继承于BasePageModel数据库Model</typeparam>
    /// <typeparam name="OutputModel">返回的OutputModel</typeparam>
    public abstract class DALPageBase<Model, OutputModel> : DALBase<Model, OutputModel>
        where Model : BasePageModel, new()
        where OutputModel : class, Model, new()
    {
        /// <summary>
        /// 构造DAL基类
        /// </summary>
        public DALPageBase()
            : base()
        {
        }

        /// <summary>
        /// 根据数据库连接字符串构建DAL基类
        /// </summary>
        /// <param name="connName">数据库连接名</param>
        public DALPageBase(string connName)
                : base(connName)
        {

        }

        /// <summary>
        /// 构造DAL基类
        /// </summary>
        /// <param name="connName">数据库连接名</param>
        /// <param name="dbMode">数据库类型</param>
        public DALPageBase(string connName, DatabaseMode dbMode) : base(connName, dbMode)
        {
        }

        /// <summary>
        /// 根据model的值作为查询条件,model里每一个非null字段都会作为查询条件
        /// 获取分页的数据
        /// 支持模糊查询
        /// </summary>
        /// <param name="model">作为条件的model</param>
        /// <returns>符合查询条件的分页数据集合</returns>
        public virtual PageModel<OutputModel> ListPage(Model model)
        {
            bool isIMyInterface = typeof(BasePageModel).IsAssignableFrom(model.GetType());

            var result = new PageModel<OutputModel>();
            ParmCollection parms = this.Table.PrepareConditionParms(model, this.IsLikeMode);
            var requirSql = string.Format(this.Table.Select, parms.WhereSql, string.Empty);
            PageInfo pageInfo = new PageInfo(requirSql, this.OrderBy, model.PageSize ?? 10, model.PageNO ?? 1);
            result.ListData = List(pageInfo.PageSQL, parms);
            result.PageNO = model.PageNO ?? 1;
            result.PageSize = model.PageSize ?? 10;
            using (IDataReader rdr = Internal_DataHelper.ExecuteReader(pageInfo.PageCountSQL, parms))
            {
                result.DataCount = rdr.Read() ? rdr.GetValue<int>("PAGECOUNT") : 0;
            }
            return result;
        }

        /// <summary>
        /// 自定义sql查询，查询结果以分页形式返回
        /// </summary>
        /// <param name="sql">自定义原始sql语句</param>
        /// <param name="parms">参数</param>
        /// <param name="orderBy">排序语句,例: "ORDER BY GmtUpdateDate DESC"</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="pageNO">页数</param>
        /// <param name="preSql">前置sql</param>
        /// <returns></returns>
        public virtual PageModel<OutputModel> ListPage(string sql, int pageNO = 1, int pageSize = 10, Collection<IDataParameter> parms = null, string orderBy = "", string preSql = "")
        {
            if (parms == null)
            {
                parms = new Collection<IDataParameter>();
            }
            var result = new PageModel<OutputModel>();
            PageInfo pageInfo = new PageInfo(sql, orderBy, pageSize, pageNO);
            result.ListData = List(pageInfo.PageSQL, parms);
            result.PageNO = pageNO;
            result.PageSize = pageSize;
            using (IDataReader rdr = Internal_DataHelper.ExecuteReader(pageInfo.PageCountSQL, parms))
            {
                result.DataCount = rdr.Read() ? rdr.GetValue<int>("PAGECOUNT") : 0;
            }
            return result;
        }
    }
}
