using Tomato.StandardLib.DAL.Base;
using Tomato.StandardLib.MyModel;
using System.Collections.ObjectModel;
using System.Data;

namespace Tomato.StandardLib.MyBaseClass
{
    public abstract class DALPageBaseNew<Model, OutputModel> : DALBase<Model, OutputModel> where Model : BasePageModel, new() where OutputModel : class, Model, new()
    {
        public DALPageBaseNew()
        {
        }

        public DALPageBaseNew(string connName)
            : base(connName)
        {
        }

        public DALPageBaseNew(string connName, DatabaseMode dbMode)
            : base(connName, dbMode)
        {
        }

        public virtual PageModel<OutputModel> ListPage(Model model)
        {
            bool flag = typeof(BasePageModel).IsAssignableFrom(model.GetType());
            PageModel<OutputModel> pageModel = new PageModel<OutputModel>();
            ParmCollection parmCollection = base.Table.PrepareConditionParms(model, base.IsLikeMode);
            string requirSQL = string.Format(base.Table.Select, parmCollection.WhereSql, string.Empty);
            PageInfo pageInfo = new PageInfo(requirSQL, base.OrderBy, model.PageSize ?? 10, model.PageNO ?? 1);
            pageModel.ListData = List(pageInfo.PageSQL, parmCollection);
            pageModel.PageNO = (model.PageNO ?? 1);
            pageModel.PageSize = (model.PageSize ?? 10);
            using (IDataReader dataReader = base.Internal_DataHelper.ExecuteReader(pageInfo.PageCountSQL, parmCollection))
            {
                pageModel.DataCount = (dataReader.Read() ? dataReader.GetValue<int>("PAGECOUNT") : 0);
            }
            return pageModel;
        }

        public virtual PageModel<OutputModel> ListPage(string sql, int pageNO = 1, int pageSize = 10, Collection<IDataParameter> parms = null, string orderBy = "", string preSql = "")
        {
            if (parms == null)
            {
                parms = new Collection<IDataParameter>();
            }
            PageModel<OutputModel> pageModel = new PageModel<OutputModel>();
            PageInfo pageInfo = new PageInfo(sql, orderBy, pageSize, pageNO);
            pageModel.ListData = List(pageInfo.PageSQL, parms);
            pageModel.PageNO = pageNO;
            pageModel.PageSize = pageSize;
            using (IDataReader dataReader = base.Internal_DataHelper.ExecuteReader(pageInfo.PageCountSQL, parms))
            {
                pageModel.DataCount = (dataReader.Read() ? dataReader.GetValue<int>("PAGECOUNT") : 0);
            }
            return pageModel;
        }
    }
    public abstract class DALPageBaseNew<Model, OutputModel, ViewModel> : DALBaseNew<Model, OutputModel, ViewModel> where Model : BasePageModel, new() where OutputModel : class, Model, new() where ViewModel : class, Model, new()
    {
        public DALPageBaseNew()
        {
        }

        public DALPageBaseNew(string connName)
            : base(connName)
        {
        }

        public DALPageBaseNew(string connName, DatabaseMode dbMode)
            : base(connName, dbMode)
        {
        }

        public virtual PageModel<OutputModel> ListPage(Model model)
        {
            bool flag = typeof(BasePageModel).IsAssignableFrom(model.GetType());
            PageModel<OutputModel> pageModel = new PageModel<OutputModel>();
            ParmCollection parmCollection = base.Table.PrepareConditionParms(model, base.IsLikeMode);
            string requirSQL = string.Format(base.Table.Select, parmCollection.WhereSql, string.Empty);
            PageInfo pageInfo = new PageInfo(requirSQL, base.OrderBy, model.PageSize ?? 10, model.PageNO ?? 1);
            pageModel.ListData = List(pageInfo.PageSQL, parmCollection);
            pageModel.PageNO = (model.PageNO ?? 1);
            pageModel.PageSize = (model.PageSize ?? 10);
            using (IDataReader dataReader = base.Internal_DataHelper.ExecuteReader(pageInfo.PageCountSQL, parmCollection))
            {
                pageModel.DataCount = (dataReader.Read() ? dataReader.GetValue<int>("PAGECOUNT") : 0);
            }
            return pageModel;
        }

        public virtual PageModel<ViewModel> ListViewPage(ViewModel model)
        {
            bool flag = typeof(BasePageModel).IsAssignableFrom(model.GetType());
            PageModel<ViewModel> pageModel = new PageModel<ViewModel>();
            ParmCollection parmCollection = base.Table.PrepareConditionParms(model, base.IsLikeMode);
            string requirSQL = string.Format(base.Table.Select, parmCollection.WhereSql, string.Empty);
            PageInfo pageInfo = new PageInfo(requirSQL, base.OrderBy, model.PageSize ?? 10, model.PageNO ?? 1);
            pageModel.ListData = ListView(pageInfo.PageSQL, parmCollection);
            pageModel.PageNO = (model.PageNO ?? 1);
            pageModel.PageSize = (model.PageSize ?? 10);
            using (IDataReader dataReader = base.Internal_DataHelper.ExecuteReader(pageInfo.PageCountSQL, parmCollection))
            {
                pageModel.DataCount = (dataReader.Read() ? dataReader.GetValue<int>("PAGECOUNT") : 0);
            }
            return pageModel;
        }

        public virtual PageModel<OutputModel> ListPage(string sql, int pageNO = 1, int pageSize = 10, Collection<IDataParameter> parms = null, string orderBy = "", string preSql = "")
        {
            if (parms == null)
            {
                parms = new Collection<IDataParameter>();
            }
            PageModel<OutputModel> pageModel = new PageModel<OutputModel>();
            PageInfo pageInfo = new PageInfo(sql, orderBy, pageSize, pageNO);
            pageModel.ListData = List(pageInfo.PageSQL, parms);
            pageModel.PageNO = pageNO;
            pageModel.PageSize = pageSize;
            using (IDataReader dataReader = base.Internal_DataHelper.ExecuteReader(pageInfo.PageCountSQL, parms))
            {
                pageModel.DataCount = (dataReader.Read() ? dataReader.GetValue<int>("PAGECOUNT") : 0);
            }
            return pageModel;
        }
    }
}
