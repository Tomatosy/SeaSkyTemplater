using SeaSky.StandardLib.DAL.Base;
using SeaSky.StandardLib.MyAttribute;
using SeaSky.StandardLib.MyModel;
using System.Collections.ObjectModel;
using System.Data;

namespace SeaSky.StandardLib.MyBaseClass
{
    /// <summary>
    /// DAL基类    
    /// </summary>
    public abstract class DALBase : MasterRepositoryBase
    {
        /// <summary>
        /// 数据库连接方式
        /// </summary>
        private readonly DatabaseMode m_DatabaseMode;
        /// <summary>
        /// sql like模式
        /// </summary>
        private LikeMode m_likeMode;

        /// <summary>
        /// 数据库操作人
        /// </summary>
        public string Operater
        {
            get
            {
                return GetOperater();
            }

        }
        /// <summary>
        /// 获取数据库操作人(需要子类实现)
        /// </summary>
        /// <returns>操作人</returns>
        public abstract string GetOperater();

        /// <summary>
        /// 是否开启like
        /// </summary>
        internal bool IsLikeMode
        {
            get { return m_likeMode == null ? false : m_likeMode.IsLikeMode; }
        }

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        private IDbDataHelper m_dataHelper;


        /// <summary>
        /// 构造DAL基类
        /// </summary>
        /// <param name="connName">数据库连接名</param>
        /// <param name="dbMode">数据库类型</param>
        public DALBase(string connName, DatabaseMode dbMode) : base(connName)
        {
            this.m_DatabaseMode = dbMode;
            this.InitialDataHelper(connName);
        }

        /// <summary>
        /// 构造DAL基类
        /// </summary>
        /// <param name="connName">数据库连接名</param>
        public DALBase(string connName) : base(connName)
        {
            this.InitialDataHelper(connName);
        }

        /// <summary>
        /// 构建DAL基类
        /// </summary>
        public DALBase()
        {
            this.m_dataHelper = SQLHelper.Create();
        }

        /// <summary>
        /// 获取或设置数据库连接对象
        /// </summary>
        protected IDbDataHelper Internal_DataHelper
        {
            get { return this.m_dataHelper; }
            set { this.m_dataHelper = value; }
        }

        /// <summary>
        /// 根据链接字符串创建数据库连接对象
        /// </summary>
        /// <param name="connName">数据库连接名</param>
        private void InitialDataHelper(string connName)
        {
            if (connName == null)
            {
                this.m_dataHelper = SQLHelper.Create();
            }
            else
            {
                switch (m_DatabaseMode)
                {
                    case DatabaseMode.SqlClient:
                        {
                            this.m_dataHelper = SQLHelper.Create(connName);
                            break;
                        }
                    default:
                        {
                            this.m_dataHelper = SQLHelper.Create(connName);
                            break;
                        }

                }
            }
        }

        /// <summary>
        /// 开启模糊查询
        /// </summary>
        /// <returns>模糊查询模式</returns>
        public virtual LikeMode BeginLikeMode()
        {
            m_likeMode = new LikeMode();
            return m_likeMode;
        }

        /// <summary>
        /// 执行提供的自定义sql
        /// </summary>
        /// <param name="sql">自定义sql</param>
        /// <returns>返回影响行数</returns>
        public virtual int ExecuteProvidedSql(string sql)
        {
            using (System.Data.Common.DbCommand cmd = this.Database.GetSqlStringCommand(sql))
            {
                return this.Database.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 执行提供的自定义sql
        /// </summary>
        /// <param name="sql">自定义sql</param>
        /// <param name="parms">参数</param>
        /// <returns>返回影响行数</returns>
        public virtual int ExecuteProvidedSql(string sql, Collection<IDataParameter> parms = null)
        {
            if (parms == null)
            {
                parms = new Collection<IDataParameter>();
            }
            using (System.Data.Common.DbCommand cmd = this.Database.GetSqlStringCommand(sql))
            {
                foreach (IDataParameter parm in parms)
                {
                    this.Database.AddInParameter(cmd, parm.ParameterName, parm.DbType, parm.Value);
                }
                return this.Database.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <returns>返回影响行数</returns>
        public virtual int ExecuteStoredProc(string storedProcName)
        {
            using (System.Data.Common.DbCommand cmd = this.Database.GetStoredProcCommand(storedProcName))
            {
                return this.Database.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parms">存储过程参数</param>
        /// <returns>返回影响行数</returns>
        public virtual int ExecuteStoredProc(string storedProcName, params object[] parms)
        {
            using (System.Data.Common.DbCommand cmd = this.Database.GetStoredProcCommand(storedProcName, parms))
            {
                return this.Database.ExecuteNonQuery(cmd);
            }
        }
    }

    /// <summary>
    /// DAL基（数据库Model）
    /// </summary>
    /// <typeparam name="Model">数据库表对应的Model类型</typeparam>
    public abstract class DALBase<Model> : DALBase<Model, Model>
        where Model : SystemModel, new()
    {
        /// <summary>
        /// 构建DAL基类
        /// </summary>
        public DALBase()
            : base()
        {
        }

        /// <summary>
        /// 根据连接字符串构建DAL基类
        /// </summary>
        /// <param name="connName">连接名</param>
        public DALBase(string connName)
            : base(connName)
        {
        }

        /// <summary>
        /// 构造DAL基类
        /// </summary>
        /// <param name="connName">数据库连接名</param>
        /// <param name="dbMode">数据库类型</param>
        public DALBase(string connName, DatabaseMode dbMode) : base(connName, dbMode)
        {
        }
    }

    /// <summary>
    /// DAL基（数据库Model,返回OutputModel）
    /// </summary>
    /// <typeparam name="Model">数据库表对应的Model类型</typeparam>
    /// <typeparam name="OutputModel">返回的OutputModel类型</typeparam>
    public abstract partial class DALBase<Model, OutputModel> : DALBase
    where Model : SystemModel, new()
    where OutputModel : class, Model, new()
    {
        /// <summary>
        /// 存放对应数据库表结构信息
        /// </summary>
        private TableInfo m_table;

        /// <summary>
        /// 构造DAL基类
        /// </summary>
        public DALBase()
            : this("BaseConn")
        {
        }

        /// <summary>
        /// 根据数据库连接字符串构建DAL基类
        /// </summary>
        /// <param name="connName">数据库连接名</param>
        public DALBase(string connName)
            : this(connName, DatabaseMode.SqlClient)
        {
        }

        /// <summary>
        /// 获取或设置对应数据库表结构信息
        /// </summary>
        protected TableInfo Table
        {
            get { return this.m_table; }
            set { this.m_table = value; }
        }

        /// <summary>
        /// 构造DAL基类
        /// </summary>
        /// <param name="connName">数据库连接名</param>
        /// <param name="dbMode">数据库类型</param>
        public DALBase(string connName, DatabaseMode dbMode) : base(connName, dbMode)
        {
            this.m_table = this.Internal_DataHelper.GetTableInformation(typeof(Model));
            this.OrderBy = null;
        }

        /// <summary>
        /// 获取Model辅助属性集合
        /// </summary>
        protected ModelAttribute ModelAttributes
        {
            get { return DbDataHelper.GetAttributes(typeof(Model)); }
        }

        /// <summary>
        /// 获取或设置数据库连接对象
        /// </summary>
        protected virtual IDbDataHelper DataHelper
        {
            get { return this.Internal_DataHelper; }
            set { this.Internal_DataHelper = value; }
        }

        /// <summary>
        /// 获取或设置排序子句
        /// </summary>
        protected string OrderBy
        {
            get { return this.Table.OrderByString; }
            set { this.Table.OrderByString = value; }
        }

        /// <summary>
        /// 获取数据库表结构信息
        /// </summary>
        /// <returns>数据库表结构信息</returns>
        public virtual DataTable GetSchemaTable()
        {
            using (IDataReader rdr = this.Internal_DataHelper.ExecuteReaderForKeyInfo(string.Format(this.Table.Select, " WHERE 1=2", string.Empty)))
            {
                return rdr.GetSchemaTable();
            }
        }

        /// <summary>
        /// 如果不想使用从Model获得的属性，则使用自定义的属性初始化
        /// </summary>
        /// <param name="table">自定义属性</param>
        protected void Initial(ModelAttribute table)
        {
            this.m_table = new TableInfo();
            this.m_table.Initial(table, this.DataHelper, typeof(Model));
            this.OrderBy = null;
        }
    }

    /// <summary>
    /// DAL基（数据库Model,ViewModel）
    /// </summary>
    /// <typeparam name="Model">数据库表对应的Model类型</typeparam>
    /// <typeparam name="OutputModel">OutputModel</typeparam>
    /// <typeparam name="ViewModel">ViewModel</typeparam>
    public abstract partial class DALBase<Model, OutputModel, ViewModel> : DALBase
        where Model : SystemModel, new()
        where OutputModel : class, Model, new()
        where ViewModel : class, Model, new()
    {
        /// <summary>
        /// 存放对应数据库表结构信息
        /// </summary>
        private TableInfo m_table;

        /// <summary>
        /// 构造DAL基类
        /// </summary>
        public DALBase()
            : this("BaseConn")
        {
        }

        /// <summary>
        /// 根据数据库连接字符串构建DAL基类
        /// </summary>
        /// <param name="connName">数据库连接名</param>
        public DALBase(string connName)
            : this(connName, DatabaseMode.SqlClient)
        {
        }

        /// <summary>
        /// 视图查询模式
        /// </summary>
        private SelectViewMode selectViewMode;

        /// <summary>
        /// 获取或设置对应数据库表结构信息
        /// </summary>
        protected TableInfo Table
        {
            get { return this.selectViewMode?.IsSelectViewMode ?? false ? this.selectViewMode.SelViewTable : this.m_table; }
            set { this.m_table = value; }
        }

        /// <summary>
        /// 构造DAL基类
        /// </summary>
        /// <param name="connName">数据库连接名</param>
        /// <param name="dbMode">数据库类型</param>
        public DALBase(string connName, DatabaseMode dbMode) : base(connName, dbMode)
        {
            this.m_table = this.Internal_DataHelper.GetTableInformation(typeof(Model));
            this.OrderBy = null;
        }

        /// <summary>
        /// 获取Model辅助属性集合
        /// </summary>
        protected ModelAttribute ModelAttributes
        {
            get { return DbDataHelper.GetAttributes(typeof(Model)); }
        }

        /// <summary>
        /// 获取或设置数据库连接对象
        /// </summary>
        protected virtual IDbDataHelper DataHelper
        {
            get { return this.Internal_DataHelper; }
            set { this.Internal_DataHelper = value; }
        }

        /// <summary>
        /// 获取或设置排序子句
        /// </summary>
        protected string OrderBy
        {
            get { return this.Table.OrderByString; }
            set { this.Table.OrderByString = value; }
        }

        /// <summary>
        /// 获取数据库表结构信息
        /// </summary>
        /// <returns>数据库表结构信息</returns>
        public virtual DataTable GetSchemaTable()
        {
            using (IDataReader rdr = this.Internal_DataHelper.ExecuteReaderForKeyInfo(string.Format(this.Table.Select, " WHERE 1=2", string.Empty)))
            {
                return rdr.GetSchemaTable();
            }
        }

        /// <summary>
        /// 如果不想使用从Model获得的属性，则使用自定义的属性初始化
        /// </summary>
        /// <param name="table">自定义属性</param>
        protected void Initial(ModelAttribute table)
        {
            this.m_table = new TableInfo();
            this.m_table.Initial(table, this.DataHelper, typeof(Model));
            this.OrderBy = null;
        }

        /// <summary>
        /// 开启查询outModel里面的视图
        /// </summary>
        public virtual SelectViewMode BeginSelView()
        {
            this.selectViewMode = new SelectViewMode();
            this.selectViewMode.SelViewTable = this.Internal_DataHelper.GetTableInformation(typeof(ViewModel));
            return selectViewMode;
        }
    }
}
