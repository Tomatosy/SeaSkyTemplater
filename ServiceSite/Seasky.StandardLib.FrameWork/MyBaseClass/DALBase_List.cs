using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace SeaSky.StandardLib.MyBaseClass
{
    public abstract partial class DALBase<Model, OutputModel, ViewModel>
    {
        /// <summary>
        /// 以集合方式返回Model类型所对应数据库表的所有数据
        /// </summary>
        /// <returns>表内所有数据的Model集合</returns>
        public virtual IEnumerable<OutputModel> List()
        {
            return List(null);
        }

        /// <summary>
        /// 根据model的值作为条件查询条件，model里每一个非null字段都会作为查询条件
        /// 如果想查询所有记录，则model为null即可
        /// 支持模糊查询
        /// </summary>
        /// <param name="model">作为条件的model</param>
        /// <returns>符合查询条件的数据集合</returns>
        public virtual IEnumerable<OutputModel> List(Model model)
        {
            ParmCollection parms = this.Table.PrepareConditionParms(model, this.IsLikeMode);
            return List(string.Format(this.Table.Select, parms.WhereSql, this.OrderBy), parms);
        }

        /// <summary>
        /// 自定义sql查询，查询结果以集合形式返回
        /// </summary>
        /// <param name="sql">自定义sql语句</param>
        /// <param name="parms">执行参数集合</param>
        /// <returns>符合查询条件的集合</returns>
        public virtual IEnumerable<OutputModel> List(string sql, Collection<IDataParameter> parms = null)
        {
            if (parms == null)
            {
                parms = new Collection<IDataParameter>();
            }
            using (IDataReader rdr = Internal_DataHelper.ExecuteReader(sql, parms))
            {
                while (rdr.Read())
                {
                    yield return (OutputModel)this.Table.ReadDataReader(rdr, new OutputModel());
                }
            }
        }  
        /// <summary>
        /// 自定义sql查询，查询结果以集合形式返回
        /// </summary>
        /// <param name="sql">自定义sql语句</param>
        /// <param name="parms">执行参数集合</param>
        /// <returns>符合查询条件的集合</returns>
        public virtual IEnumerable<ViewModel> ListView(string sql, Collection<IDataParameter> parms = null)
        {
            if (parms == null)
            {
                parms = new Collection<IDataParameter>();
            }
            using (IDataReader rdr = Internal_DataHelper.ExecuteReader(sql, parms))
            {
                while (rdr.Read())
                {
                    yield return (ViewModel)this.Table.ReadDataReader(rdr, new ViewModel());
                }
            }
        }

        /// <summary>
        /// 以数据表方式返回Model类型所对应数据库表的所有数据
        /// </summary>
        /// <returns>表内所有数据的DataTable</returns>
        public virtual DataTable ListTable()
        {
            DataTable dt = ListTable(null);
            return dt;
        }

        /// <summary>
        /// 根据model的值查询，model里每一个非null字段都会作为查询条件
        /// 如果想查询所有记录，则model为null即可
        /// 支持模糊查询
        /// </summary>
        /// <param name="model">作为查询条件的model</param>
        /// <returns>符合查询条件的数据表</returns>
        public virtual DataTable ListTable(Model model)
        {
            ParmCollection parms = this.Table.PrepareConditionParms(model, this.IsLikeMode);
            DataTable dt = ListTable(
                string.Format(this.Table.Select, parms.WhereSql, this.OrderBy)
                , parms);
            return dt;
        }

        /// <summary>
        /// 自定义sql查询，查询结果以数据表形式返回
        /// </summary>
        /// <param name="sql">自定义sql语句</param>
        /// <param name="parms">执行参数集合</param>
        /// <returns>符合查询条件的数据表</returns>
        public virtual DataTable ListTable(string sql, Collection<IDataParameter> parms = null)
        {
            if (parms == null)
            {
                parms = new Collection<IDataParameter>();
            }
            DataTable dt = Internal_DataHelper.FillDataTable(sql, parms);
            dt.TableName = this.Table.TableName;
            return dt;
        }

        /// <summary>
        /// 自定义sql查询，查询结果以DataSet形式返回
        /// </summary>
        /// <param name="sql">自定义sql语句</param>
        /// <param name="parms">执行参数集合</param>
        /// <returns>符合查询条件的数据表集合</returns>
        public virtual DataSet ListDataSet(string sql, Collection<IDataParameter> parms = null)
        {
            if (parms == null)
            {
                parms = new Collection<IDataParameter>();
            }
            DataSet ds = Internal_DataHelper.FillDataSet(sql, parms);
            return ds;
        }

        /// <summary>
        /// 根据model的值和操作种类进行搜索model集合，model里每一个非null字段都会作为查询条件
        /// 支持模糊查询
        /// </summary>
        /// <param name="model">作为查询条件的model</param>
        /// <param name="or">操作符，true为or，false为and</param>
        /// <returns>符合查询条件的数据集合</returns>
        public virtual IEnumerable<OutputModel> Search(Model model, bool or)
        {
            ParmCollection parms = this.Table.PrepareSearchParms(model, or, this.IsLikeMode);
            return List(string.Format(this.Table.Select, parms.WhereSql, this.OrderBy), parms);
        }
    }
}
