using System;
using System.Collections.ObjectModel;
using System.Data;

namespace SeaSky.StandardLib.MyBaseClass
{
    public abstract partial class DALBase<Model, OutputModel, ViewModel>
    {
        /// <summary>
        /// 根据model的值作为查询条件,model里每一个非null字段都会作为查询条件
        /// 返回符合条件的第一个model
        /// 支持模糊查询
        /// </summary>
        /// <param name="model">作为条件的model</param>
        /// <returns>符合查询条件的第一个model</returns>
        public virtual OutputModel SelectWithModel(Model model)
        {
            ParmCollection parms = this.Table.PrepareConditionParms(model, this.IsLikeMode);
            return this.Select(string.Format(this.Table.Select, parms.WhereSql, this.OrderBy), parms);
        }

        /// <summary>
        /// 以自动增量字段为查询条件，获取符合条件model
        /// </summary>
        /// <param name="identity">作为条件的自动增量</param>
        /// <returns>符合条件model</returns>
        public virtual OutputModel SelectWithIdentity(long identity)
        {
            ParmCollection parms = this.Table.PrepareIdentityParm(identity);
            return this.Select(string.Format(this.Table.Select, parms.WhereSql, this.OrderBy), parms);
        }

        /// <summary>
        /// 以主键字段为查询条件，model中的非主键字段均不作为查询条件，获取符合条件model
        /// </summary>
        /// <param name="model">作为条件的model</param>
        /// <returns>符合条件model</returns>
        public virtual OutputModel SelectWithKeys(Model model)
        {
            ParmCollection parms = this.Table.PrepareKeysParms(model);
            return this.Select(string.Format(this.Table.Select, parms.WhereSql, this.OrderBy), parms);
        }

        /// <summary>
        /// 获取Model对应的数据库表内最新生成的自动增量值
        /// </summary>
        /// <returns>最新生成的自动增量值</returns>
        public virtual int GetIdentity()
        {
            return this.Internal_DataHelper.ExecuteForIdentity(this.Table.TableName);
        }

        /// <summary>
        /// 通过SQL生成Guid，与Model无关
        /// </summary>
        /// <returns>Guid</returns>
        public Guid GetGuid()
        {
            string guid_str = this.Internal_DataHelper.ExecuteScalar("select newid()").ToString();
            return new Guid(guid_str);
        }

        /// <summary>
        /// 通过SQL获取数据库服务器的当前时间，与Model无关
        /// </summary>
        /// <returns>数据库服务器的当前时间</returns>
        public DateTime GetServerTime()
        {
            string time_str = this.Internal_DataHelper.ExecuteScalar("select getdate()").ToString();
            return DateTime.Parse(time_str);
        }

        /// <summary>
        /// 获取Model对应的数据库表内所有记录数合计
        /// </summary>
        /// <returns>记录数合计</returns>
        public virtual int GetCount()
        {
            return GetCount(null);
        }

        /// <summary>
        /// 根据model的值作为查询条件,model里每一个非null字段都会作为查询条件，
        /// 获取Model对应的数据库表内符合条件的记录数合计
        /// 支持模糊查询
        /// </summary>
        /// <param name="model">作为条件的model</param>
        /// <returns>记录数合计</returns>
        public virtual int GetCount(Model model)
        {
            ParmCollection parms = this.Table.PrepareConditionParms(model, this.IsLikeMode);
            object o = Internal_DataHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(*) FROM {0} {1}", this.Table.TableName, parms.WhereSql), parms);
            if (o != null)
            {
                return int.Parse(o.ToString());
            }

            return 0;
        }

        /// <summary>
        /// 自定义sql查询，返回满足条件的第一个Model
        /// </summary>
        /// <param name="sql">自定义sql</param>
        /// <param name="parms">执行参数集合</param>
        /// <returns>符合查询条件的model</returns>
        public virtual OutputModel Select(string sql, Collection<IDataParameter> parms = null)
        {
            if (parms == null)
            {
                parms = new Collection<IDataParameter>();
            }
            using (IDataReader rdr = this.Internal_DataHelper.ExecuteReader(sql, parms))
            {
                return rdr.Read() ? (OutputModel)this.Table.ReadDataReader(rdr, new OutputModel()) : null;
            }
        }
    }
}
