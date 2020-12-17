using Tomato.StandardLib.DAL.Base;
using Tomato.StandardLib.MyAttribute;
using Tomato.StandardLib.MyModel;
using System.Collections.ObjectModel;
using System.Data;

namespace Tomato.StandardLib.MyBaseClass
{
    /// <summary>
    /// DAL����    
    /// </summary>
    public abstract class DALBase : MasterRepositoryBase
    {
        /// <summary>
        /// ���ݿ����ӷ�ʽ
        /// </summary>
        private DatabaseMode m_DatabaseMode;
        /// <summary>
        /// sql likeģʽ
        /// </summary>
        private LikeMode m_likeMode;

        /// <summary>
        /// ���ݿ������
        /// </summary>
        public string Operater
        {
            get
            {
                return GetOperater();
            }

        }
        /// <summary>
        /// ��ȡ���ݿ������(��Ҫ����ʵ��)
        /// </summary>
        /// <returns>������</returns>
        public abstract string GetOperater();

        /// <summary>
        /// �Ƿ���like
        /// </summary>
        internal bool IsLikeMode
        {
            get { return m_likeMode == null ? false : m_likeMode.IsLikeMode; }
        }

        /// <summary>
        /// ���ݿ���ʶ���
        /// </summary>
        private IDbDataHelper m_dataHelper;


        /// <summary>
        /// ����DAL����
        /// </summary>
        /// <param name="connName">���ݿ�������</param>
        /// <param name="dbMode">���ݿ�����</param>
        public DALBase(string connName, DatabaseMode dbMode) : base(connName)
        {
            this.m_DatabaseMode = dbMode;
            this.InitialDataHelper(connName);
        }

        /// <summary>
        /// ����DAL����
        /// </summary>
        /// <param name="connName">���ݿ�������</param>
        public DALBase(string connName) : base(connName)
        {
            this.InitialDataHelper(connName);
        }

        /// <summary>
        /// ����DAL����
        /// </summary>
        public DALBase()
        {
            this.m_dataHelper = SQLHelper.Create();
        }

        /// <summary>
        /// ��ȡ���������ݿ����Ӷ���
        /// </summary>
        protected IDbDataHelper Internal_DataHelper
        {
            get { return this.m_dataHelper; }
            set { this.m_dataHelper = value; }
        }

        /// <summary>
        /// ���������ַ����������ݿ����Ӷ���
        /// </summary>
        /// <param name="connName">���ݿ�������</param>
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
        /// ����ģ����ѯ
        /// </summary>
        /// <returns>ģ����ѯģʽ</returns>
        public virtual LikeMode BeginLikeMode()
        {
            m_likeMode = new LikeMode();
            return m_likeMode;
        }

        /// <summary>
        /// ִ���ṩ���Զ���sql
        /// </summary>
        /// <param name="sql">�Զ���sql</param>
        /// <returns>����Ӱ������</returns>
        public virtual int ExecuteProvidedSql(string sql)
        {
            using (var cmd = this.Database.GetSqlStringCommand(sql))
            {
                return this.Database.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// ִ���ṩ���Զ���sql
        /// </summary>
        /// <param name="sql">�Զ���sql</param>
        /// <param name="parms">����</param>
        /// <returns>����Ӱ������</returns>
        public virtual int ExecuteProvidedSql(string sql, Collection<IDataParameter> parms = null)
        {
            if (parms == null)
            {
                parms = new Collection<IDataParameter>();
            }
            using (var cmd = this.Database.GetSqlStringCommand(sql))
            {
                foreach (var parm in parms)
                {
                    this.Database.AddInParameter(cmd, parm.ParameterName, parm.DbType, parm.Value);
                }
                return this.Database.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <returns>����Ӱ������</returns>
        public virtual int ExecuteStoredProc(string storedProcName)
        {
            using (var cmd = this.Database.GetStoredProcCommand(storedProcName))
            {
                return this.Database.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parms">�洢���̲���</param>
        /// <returns>����Ӱ������</returns>
        public virtual int ExecuteStoredProc(string storedProcName, params object[] parms)
        {
            using (var cmd = this.Database.GetStoredProcCommand(storedProcName, parms))
            {
                return this.Database.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// ȥ��like�ַ�
        /// </summary>
        /// <param name="parmsValue"></param>
        /// <returns></returns>
        public string KillLikeKeywords(string parmsValue)
        {
            return parmsValue.Replace("[", "[[]").Replace("%", "[%]").Replace("^", "[^]").Replace("_", "[_]");
        }
    }

    /// <summary>
    /// DAL�������ݿ�Model��
    /// </summary>
    /// <typeparam name="Model">���ݿ����Ӧ��Model����</typeparam>
    public abstract class DALBase<Model> : DALBase<Model, Model>
        where Model : SystemModel, new()
    {
        /// <summary>
        /// ����DAL����
        /// </summary>
        public DALBase()
            : base()
        {
        }

        /// <summary>
        /// ���������ַ�������DAL����
        /// </summary>
        /// <param name="connName">������</param>
        public DALBase(string connName)
            : base(connName)
        {
        }

        /// <summary>
        /// ����DAL����
        /// </summary>
        /// <param name="connName">���ݿ�������</param>
        /// <param name="dbMode">���ݿ�����</param>
        public DALBase(string connName, DatabaseMode dbMode) : base(connName, dbMode)
        {
        }
    }

    /// <summary>
    /// DAL�������ݿ�Model,����OutputModel��
    /// </summary>
    /// <typeparam name="Model">���ݿ����Ӧ��Model����</typeparam>
    /// <typeparam name="OutputModel">���ص�OutputModel����</typeparam>
    public abstract partial class DALBase<Model, OutputModel> : DALBase
        where Model : SystemModel, new()
        where OutputModel : class, Model, new()
    {
        /// <summary>
        /// ��Ŷ�Ӧ���ݿ���ṹ��Ϣ
        /// </summary>
        private TableInfo m_table;

        /// <summary>
        /// ����DAL����
        /// </summary>
        public DALBase()
            : this("BaseConn")
        {
        }

        /// <summary>
        /// �������ݿ������ַ�������DAL����
        /// </summary>
        /// <param name="connName">���ݿ�������</param>
        public DALBase(string connName)
            : this(connName, DatabaseMode.SqlClient)
        {
        }

        /// <summary>
        /// ��ͼ��ѯģʽ
        /// </summary>
        private SelectViewMode selectViewMode;

        /// <summary>
        /// ��ȡ�����ö�Ӧ���ݿ���ṹ��Ϣ
        /// </summary>
        protected TableInfo Table
        {
            get { return this.selectViewMode?.IsSelectViewMode ?? false ? this.selectViewMode.SelViewTable : this.m_table; }
            set { this.m_table = value; }
        }

        /// <summary>
        /// ����DAL����
        /// </summary>
        /// <param name="connName">���ݿ�������</param>
        /// <param name="dbMode">���ݿ�����</param>
        public DALBase(string connName, DatabaseMode dbMode) : base(connName, dbMode)
        {
            this.m_table = this.Internal_DataHelper.GetTableInformation(typeof(Model));
            this.OrderBy = null;
        }

        /// <summary>
        /// ��ȡModel�������Լ���
        /// </summary>
        protected ModelAttribute ModelAttributes
        {
            get { return DbDataHelper.GetAttributes(typeof(Model)); }
        }

        /// <summary>
        /// ��ȡ���������ݿ����Ӷ���
        /// </summary>
        protected virtual IDbDataHelper DataHelper
        {
            get { return this.Internal_DataHelper; }
            set { this.Internal_DataHelper = value; }
        }

        /// <summary>
        /// ��ȡ�����������Ӿ�
        /// </summary>
        protected string OrderBy
        {
            get { return this.Table.OrderByString; }
            set { this.Table.OrderByString = value; }
        }

        /// <summary>
        /// ��ȡ���ݿ���ṹ��Ϣ
        /// </summary>
        /// <returns>���ݿ���ṹ��Ϣ</returns>
        public virtual DataTable GetSchemaTable()
        {
            using (IDataReader rdr = this.Internal_DataHelper.ExecuteReaderForKeyInfo(string.Format(this.Table.Select, " WHERE 1=2", string.Empty)))
            {
                return rdr.GetSchemaTable();
            }
        }

        /// <summary>
        /// �������ʹ�ô�Model��õ����ԣ���ʹ���Զ�������Գ�ʼ��
        /// </summary>
        /// <param name="table">�Զ�������</param>
        protected void Initial(ModelAttribute table)
        {
            this.m_table = new TableInfo();
            this.m_table.Initial(table, this.DataHelper, typeof(Model));
            this.OrderBy = null;
        }

        /// <summary>
        /// ������ѯoutModel�������ͼ
        /// </summary>
        public virtual SelectViewMode BeginSelView()
        {
            this.selectViewMode = new SelectViewMode();
            this.selectViewMode.SelViewTable = this.Internal_DataHelper.GetTableInformation(typeof(OutputModel));
            return selectViewMode;
        }
    }
}
