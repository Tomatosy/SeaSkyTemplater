// ===============================================================================
// This file is based on the Microsoft Data Access Application Block for .NET
// For more information please go to 
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
// ===============================================================================

using Tomato.StandardLib.MyAttribute;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Tomato.StandardLib.MyBaseClass
{
    /// <summary>
    /// SQLServer���ݿ��������
    /// </summary>
    internal class SQLHelper : DbDataHelper, IDbDataHelper
    {
        /// <summary>
        /// ��ȡSQL����ǰ׺��Ĭ��Ϊ@
        /// </summary>
        public override string ParameterPrefix
        {
            get { return "@"; }
        }

        /// <summary>
        /// ��ȡSQL�ֶ�ǰ׺��Ĭ��Ϊ[
        /// </summary>
        public override string Prefix
        {
            get { return "["; }
        }

        /// <summary>
        /// ��ȡSQL�ֶκ�׺��Ĭ��Ϊ]
        /// </summary>
        public override string Suffix
        {
            get { return "]"; }
        }

        /// <summary>
        /// ��̬SQLServer���ݿ��������
        /// </summary>
        private static SQLHelper m_DataHelper;

        /// <summary>
        /// ���Model�������ݱ���Ϣ����
        /// </summary>
        private TableCollection m_tables;

        /// <summary>
        /// ����Ĭ��SQLServer���ݿ��������
        /// </summary>
        private SQLHelper() : base("BaseConn")
        {
        }

        /// <summary>
        /// ����SQLServer���ݿ��������ָ�������ַ���
        /// </summary>
        /// <param name="connString">���ݿ������ַ���</param>
        private SQLHelper(string connString) : base(connString)
        {

        }

        /// <summary>
        /// ��ȡ����DataHelper
        /// </summary>
        protected override IDbDataHelper DataHelper
        {
            get { return this; }
        }

        /// <summary>
        /// ��ȡ������Model�������ݱ���Ϣ����
        /// </summary>
        protected override TableCollection Tables
        {
            get { return this.m_tables; }
            set { this.m_tables = value; }
        }

        /// <summary>
        /// �Ե�����ʽ����Ĭ�Ͼ�̬SQLHelper�������ַ���ΪĬ��
        /// </summary>
        /// <returns>��̬SQLHelper</returns>
        public static SQLHelper Create()
        {
            if (m_DataHelper == null)
            {
                m_DataHelper = new SQLHelper();
            }
            return m_DataHelper;
        }

        /// <summary>
        /// �Ե�����ʽ����Ĭ�Ͼ�̬SQLHelper��ָ�������ַ���
        /// </summary>
        /// <param name="connString">���ݿ������ַ���</param>
        /// <returns>��̬SQLHelper</returns>
        public static SQLHelper Create(string connString)
        {
            return new SQLHelper(connString);
        }

        /// <summary>
        /// ����SQL��������
        /// </summary>
        /// <param name="parameterName">��������</param>
        /// <param name="p_dbType">���ݿ���������</param>
        /// <param name="srcColumn">���ݿ�����</param>
        /// <returns>SQL��������</returns>
        public override IDataParameter Parameter(string parameterName, object p_dbType, string srcColumn)
        {
            SqlParameter p = new SqlParameter(parameterName, (SqlDbType)p_dbType);
            p.SourceColumn = srcColumn;
            return p;
        }

        /// <summary>
        /// ����SQL��������
        /// </summary>
        /// <param name="parm">Ҫ���ƵĲ�������</param>
        /// <returns>���ƺ�Ĳ�������</returns>
        public override IDataParameter CloneParameter(IDataParameter parm)
        {
            SqlParameter p = (SqlParameter)parm;
            SqlParameter pp = new SqlParameter(p.ParameterName, p.SqlDbType, p.Size, p.SourceColumn);
            pp.Value = p.Value;
            return pp;
        }

        /// <summary>
        /// ����Model���е��ֶ����Ի�ȡ��Ӧ�����ݿ���������
        /// </summary>
        /// <param name="f">Model���е��ֶ�����</param>
        /// <returns>��Ӧ�����ݿ���������</returns>
        public override object DbType(DBFieldInfoAttribute f)
        {
            return f.SqlDbType;
        }

        /// <summary>
        /// ��ȡָ�����ݱ����µ��Զ�����
        /// </summary>
        /// <param name="tableName">���ݱ���</param>
        /// <returns>�Զ�����</returns>
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
        /// ����Command�������ݿ��������
        /// </summary>
        /// <param name="cmd">Command����</param>
        /// <returns>���ݿ��������</returns>
        protected override DbDataAdapter DataAdapter(IDbCommand cmd)
        {
            return new SqlDataAdapter((SqlCommand)cmd);
        }
    }
}