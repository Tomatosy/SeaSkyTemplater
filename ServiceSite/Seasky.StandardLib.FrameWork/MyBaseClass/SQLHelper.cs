// ===============================================================================
// This file is based on the Microsoft Data Access Application Block for .NET
// For more information please go to 
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
// ===============================================================================

using SeaSky.StandardLib.MyAttribute;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SeaSky.StandardLib.MyBaseClass
{
    /// <summary>
    /// SQLServer数据库操作对象
    /// </summary>
    internal class SQLHelper : DbDataHelper, IDbDataHelper
    {
        /// <summary>
        /// 获取SQL参数前缀，默认为@
        /// </summary>
        public override string ParameterPrefix
        {
            get { return "@"; }
        }

        /// <summary>
        /// 获取SQL字段前缀，默认为[
        /// </summary>
        public override string Prefix
        {
            get { return "["; }
        }

        /// <summary>
        /// 获取SQL字段后缀，默认为]
        /// </summary>
        public override string Suffix
        {
            get { return "]"; }
        }

        /// <summary>
        /// 静态SQLServer数据库操作对象
        /// </summary>
        private static SQLHelper m_DataHelper;

        /// <summary>
        /// 存放Model类中数据表信息集合
        /// </summary>
        private TableCollection m_tables;

        /// <summary>
        /// 构造默认SQLServer数据库操作对象
        /// </summary>
        private SQLHelper() : base("BaseConn")
        {
        }

        /// <summary>
        /// 构造SQLServer数据库操作对象，指定连接字符串
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        private SQLHelper(string connString) : base(connString)
        {

        }

        /// <summary>
        /// 获取自身DataHelper
        /// </summary>
        protected override IDbDataHelper DataHelper
        {
            get { return this; }
        }

        /// <summary>
        /// 获取或设置Model类中数据表信息集合
        /// </summary>
        protected override TableCollection Tables
        {
            get { return this.m_tables; }
            set { this.m_tables = value; }
        }

        /// <summary>
        /// 以单例方式创建默认静态SQLHelper，连接字符串为默认
        /// </summary>
        /// <returns>静态SQLHelper</returns>
        public static SQLHelper Create()
        {
            if (m_DataHelper == null)
            {
                m_DataHelper = new SQLHelper();
            }
            return m_DataHelper;
        }

        /// <summary>
        /// 以单例方式创建默认静态SQLHelper，指定连接字符串
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>静态SQLHelper</returns>
        public static SQLHelper Create(string connString)
        {
            return new SQLHelper(connString);
        }

        /// <summary>
        /// 创建SQL参数类型
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="p_dbType">数据库数据类型</param>
        /// <param name="srcColumn">数据库列名</param>
        /// <returns>SQL参数类型</returns>
        public override IDataParameter Parameter(string parameterName, object p_dbType, string srcColumn)
        {
            SqlParameter p = new SqlParameter(parameterName, (SqlDbType)p_dbType);
            p.SourceColumn = srcColumn;
            return p;
        }

        /// <summary>
        /// 复制SQL参数类型
        /// </summary>
        /// <param name="parm">要复制的参数对象</param>
        /// <returns>复制后的参数对象</returns>
        public override IDataParameter CloneParameter(IDataParameter parm)
        {
            SqlParameter p = (SqlParameter)parm;
            SqlParameter pp = new SqlParameter(p.ParameterName, p.SqlDbType, p.Size, p.SourceColumn);
            pp.Value = p.Value;
            return pp;
        }

        /// <summary>
        /// 根据Model类中的字段属性获取对应的数据库数据类型
        /// </summary>
        /// <param name="f">Model类中的字段属性</param>
        /// <returns>对应的数据库数据类型</returns>
        public override object DbType(DBFieldInfoAttribute f)
        {
            return f.SqlDbType;
        }

        /// <summary>
        /// 获取指定数据表内新的自动增量
        /// </summary>
        /// <param name="tableName">数据表名</param>
        /// <returns>自动增量</returns>
        public override int ExecuteForIdentity(string tableName)
        {
            object o = ExecuteScalar(string.Format("SELECT IDENT_CURRENT('{0}') AS 'NewID'", tableName));
            if (o == null)
            {
                return 0;
            }
            return int.Parse(o.ToString());
        }


        /// <summary>
        /// 根据Command创建数据库适配对象
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <returns>数据库适配对象</returns>
        protected override DbDataAdapter DataAdapter(IDbCommand cmd)
        {
            return new SqlDataAdapter((SqlCommand)cmd);
        }
    }
}