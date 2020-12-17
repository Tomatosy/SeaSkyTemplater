using Tomato.StandardLib.MyModel;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Reflection;

namespace Tomato.StandardLib.MyAttribute
{
    /// <summary>
    /// Model���ֶθ�������
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class DBFieldInfoAttribute : Attribute
    {
        /// <summary>
        /// ����ǰ׺
        /// </summary>
        private string m_parameterPrefix = "@";

        /// <summary>
        /// �ֶ�ǰ׺
        /// </summary>
        private string m_prefix = "[";

        /// <summary>
        /// �ֶκ�׺
        /// </summary>
        private string m_suffix = "]";

        /// <summary>
        /// ����
        /// </summary>
        private string m_columnName;

        /// <summary>
        /// �Ƿ��Զ�����
        /// </summary>
        private bool m_isIdentity = false;

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        private bool m_isKey = false;

        /// <summary>
        /// ��������
        /// </summary>
        private string m_parameterName;

        /// <summary>
        /// ��������
        /// </summary>
        private int m_parameterSize = -1;

        /// <summary>
        /// SqlServer��������
        /// </summary>
        private SqlDbType m_sqlDbType = SqlDbType.Int;

        /// <summary>
        /// OleDb��������
        /// </summary>
        private OleDbType m_oleDbType = OleDbType.Integer;

        /// <summary>
        /// Oracle,mySql��������
        /// </summary>
        private DbType m_DBType = DbType.Int32;

        /// <summary>
        /// �����Ⱥ�
        /// </summary>
        private int m_orderIndex = -1;

        /// <summary>
        /// ����ʽ
        /// </summary>
        private bool m_orderAsc = true;

        /// <summary>
        /// �Ƿ����У����У�
        /// </summary>
        private bool m_isMainKey = false;

        /// <summary>
        /// �Ƿ���
        /// </summary>
        private bool m_isParentKey = false;

        /// <summary>
        /// �ֶη������
        /// </summary>
        private FieldInfo m_field;

        /// <summary>
        /// �ֶ���
        /// </summary>
        private string m_fieldName;

        /// <summary>
        /// �Ƿ�ʹ��Like�Ƚ�
        /// </summary>
        private EnumLikeMode m_likeEqual = EnumLikeMode.NoLike;

        /// <summary>
        /// �����ֶθ�������
        /// </summary>
        public DBFieldInfoAttribute()
        {
        }

        /// <summary>
        /// ����OleDb�����ֶθ�������
        /// </summary>
        /// <param name="fieldName">�ֶ���</param>
        /// <param name="columnName">��Ӧ���ݿ��ֶ���</param>
        /// <param name="isIdentity">�Ƿ��Զ�������trueΪ�ǣ�falseΪ��</param>
        /// <param name="isKey">�Ƿ�������trueΪ�ǣ�falseΪ��</param>
        /// <param name="oleDbType">OleDb��������</param>
        /// <param name="parameterSize">��������</param>
        /// <param name="orderIndex">�����Ⱥ�-1Ϊ������</param>
        /// <param name="orderAsc">����ʽ��trueΪ����Asc��falseΪ����Desc</param>
        /// <param name="isMainKey">�Ƿ����У����У���trueΪ�ǣ�falseΪ��</param>
        /// <param name="isParentKey">�Ƿ��У�treuΪ�ǣ�falseΪ��</param>
        public DBFieldInfoAttribute(string fieldName, string columnName, bool isIdentity, bool isKey, OleDbType oleDbType, int parameterSize, int orderIndex, bool orderAsc, bool isMainKey, bool isParentKey)
        {
            this.m_fieldName = fieldName;
            this.m_columnName = columnName;
            this.m_isIdentity = this.IsIdentity;
            this.m_isKey = this.IsKey;
            this.m_oleDbType = oleDbType;
            this.m_parameterSize = parameterSize;
            this.m_orderIndex = orderIndex;
            this.m_orderAsc = orderAsc;
            this.m_isMainKey = isMainKey;
            this.m_isParentKey = isParentKey;
        }

        /// <summary>
        /// ����OleDb�����ֶθ�������
        /// </summary>
        /// <param name="fieldName">�ֶ���</param>
        /// <param name="columnName">��Ӧ���ݿ��ֶ���</param>
        /// <param name="isIdentity">�Ƿ��Զ�������trueΪ�ǣ�falseΪ��</param>
        /// <param name="isKey">�Ƿ�������trueΪ�ǣ�falseΪ��</param>
        /// <param name="sqlDbType">SqlServer��������</param>
        /// <param name="parameterSize">��������</param>
        /// <param name="orderIndex">�����Ⱥ�-1Ϊ������</param>
        /// <param name="orderAsc">����ʽ��trueΪ����Asc��falseΪ����Desc</param>
        /// <param name="isMainKey">�Ƿ����У����У���trueΪ�ǣ�falseΪ��</param>
        /// <param name="isParentKey">�Ƿ��У�treuΪ�ǣ�falseΪ��</param>
        public DBFieldInfoAttribute(string fieldName, string columnName, bool isIdentity, bool isKey, SqlDbType sqlDbType, int parameterSize, int orderIndex, bool orderAsc, bool isMainKey, bool isParentKey)
        {
            this.m_fieldName = fieldName;
            this.m_columnName = columnName;
            this.m_isIdentity = this.IsIdentity;
            this.m_isKey = this.IsKey;
            this.m_sqlDbType = sqlDbType;
            this.m_parameterSize = parameterSize;
            this.m_orderIndex = orderIndex;
            this.m_orderAsc = orderAsc;
            this.m_isMainKey = isMainKey;
            this.m_isParentKey = isParentKey;
        }

        ///// <summary>
        ///// ����OleDb�����ֶθ�������
        ///// </summary>
        ///// <param name="fieldName">�ֶ���</param>
        ///// <param name="columnName">��Ӧ���ݿ��ֶ���</param>
        ///// <param name="isIdentity">�Ƿ��Զ�������trueΪ�ǣ�falseΪ��</param>
        ///// <param name="isKey">�Ƿ�������trueΪ�ǣ�falseΪ��</param>
        ///// <param name="oracleType">Oracle��������</param>
        ///// <param name="parameterSize">��������</param>
        ///// <param name="orderIndex">�����Ⱥ�-1Ϊ������</param>
        ///// <param name="orderAsc">����ʽ��trueΪ����Asc��falseΪ����Desc</param>
        ///// <param name="isMainKey">�Ƿ����У����У���trueΪ�ǣ�falseΪ��</param>
        ///// <param name="isParentKey">�Ƿ��У�treuΪ�ǣ�falseΪ��</param>
        //public DBFieldInfoAttribute(string fieldName, string columnName, bool isIdentity, bool isKey, OracleType oracleType, int parameterSize, int orderIndex, bool orderAsc, bool isMainKey, bool isParentKey)
        //{
        //    this.m_fieldName = fieldName;
        //    this.m_columnName = columnName;
        //    this.m_isIdentity = this.IsIdentity;
        //    this.m_isKey = this.IsKey;
        //    this.m_oracleType = oracleType;
        //    this.m_parameterSize = parameterSize;
        //    this.m_orderIndex = orderIndex;
        //    this.m_orderAsc = orderAsc;
        //    this.m_isMainKey = isMainKey;
        //    this.m_isParentKey = isParentKey;
        //}

        /// <summary>
        /// ��ȡ�������Ƿ�ͨ��Like����ѯ
        /// </summary>
        public EnumLikeMode LikeEqual
        {
            get { return this.m_likeEqual; }
            set { this.m_likeEqual = value; }
        }

        /// <summary>
        /// ��ȡ������Ĭ��ֵ
        /// </summary>
        public object DefaultValue
        {
            get;
            set;
        }

        /// <summary>
        /// ��ȡ�����ö�Ӧ���ݿ�����
        /// </summary>
        public string ColumnName
        {
            get 
            {
                if (this.m_columnName == null)
                {
                    this.m_columnName = this.m_field.Name.Substring(1);
                }

                return this.m_columnName; 
            }

            set 
            {
                this.m_columnName = value; 
            }
        }

        /// <summary>
        /// ��ȡ�������Ƿ��Զ�����
        /// </summary>
        public bool IsIdentity
        {
            get { return this.m_isIdentity; }
            set { this.m_isIdentity = value; }
        }

        /// <summary>
        /// ��ȡ�������Ƿ�����
        /// </summary>
        public bool IsKey
        {
            get { return this.m_isKey; }
            set { this.m_isKey = value; }
        }

        /// <summary>
        /// ��ȡ�����������Ⱥ�
        /// </summary>
        public int OrderIndex
        {
            get { return this.m_orderIndex; }
            set { this.m_orderIndex = value; }
        }

        /// <summary>
        /// ��ȡ����������ʽ��trueΪ����Asc��falseΪ����Desc
        /// </summary>
        public bool OrderAsc
        {
            get { return this.m_orderAsc; }
            set { this.m_orderAsc = value; }
        }

        /// <summary>
        /// ��ȡ����ʽSQL����
        /// </summary>
        public string OrderDirection
        {
            get { return this.m_orderAsc ? string.Empty : " DESC"; }
        }

        /// <summary>
        /// ��ȡ�����ò�������
        /// ��ΪNull,��ȡ�Ĳ�������Ϊ����ǰ׺+��Ӧ����������
        /// </summary>
        public string ParameterName
        {
            get { return this.m_parameterName == null ? this.m_parameterPrefix + this.ColumnName : this.m_parameterPrefix + this.m_parameterName; }
            set { this.m_parameterName = value; }
        }

        /// <summary>
        /// ��ȡ�����ò�����С
        /// </summary>
        public int ParameterSize
        {
            get { return this.m_parameterSize; }
            set { this.m_parameterSize = value; }
        }

        /// <summary>
        /// ��ȡ������SqlServer��������
        /// </summary>
        public SqlDbType SqlDbType
        {
            get { return this.m_sqlDbType; }
            set { this.m_sqlDbType = value; }
        }

        /// <summary>
        /// ��ȡ������OleDb��������
        /// </summary>
        public OleDbType OleDbType
        {
            get { return this.m_oleDbType; }
            set { this.m_oleDbType = value; }
        }

        /// <summary>
        /// ��ȡ������Oracle,mySql��������
        /// </summary>
        public DbType DBType
        {
            get { return this.m_DBType; }
            set { this.m_DBType = value; }
        }

        /// <summary>
        /// ��ȡ��Ӧ�ֶ�����SQL�е��������ֶ�ǰ׺+�ֶ���+�ֶκ�׺
        /// </summary>
        public string ColumnNameFix
        {
            get { return this.m_prefix + this.ColumnName + this.m_suffix; }
        }

        /// <summary>
        /// ��ȡ�������ֶ�����
        /// </summary>
        public string FieldName
        {
            get { return this.m_fieldName; }
            set { this.m_fieldName = value; }
        }

        /// <summary>
        /// ��ȡ�������Ƿ����У����У�
        /// </summary>
        public bool IsMainKey
        {
            get { return this.m_isMainKey; }
            set { this.m_isMainKey = value; }
        }

        /// <summary>
        /// ��ȡ�������Ƿ���
        /// </summary>
        public bool IsParentKey
        {
            get { return this.m_isParentKey; }
            set { this.m_isParentKey = value; }
        }

        /// <summary>
        /// ���ò���ǰ׺
        /// </summary>
        internal string ParameterPrefix
        {
            set { this.m_parameterPrefix = value; }
        }

        /// <summary>
        /// �����ֶ�ǰ׺
        /// </summary>
        internal string Prefix
        {
            set { this.m_prefix = value; }
        }

        /// <summary>
        /// �����ֶκ�׺
        /// </summary>
        internal string Suffix
        {
            set { this.m_suffix = value; }
        }

        /// <summary>
        /// ��ȡ�����÷����ֶ�
        /// </summary>
        internal FieldInfo Field
        {
            get { return this.m_field; }
            set { this.m_field = value; }
        }
    }

    /// <summary>
    /// Model���༶��������
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DBTableInfoAttribute : Attribute
    {
        /// <summary>
        /// ��Ӧ���ݱ���
        /// </summary>
        private string m_tableName;

        /// <summary>
        /// ��Ӧ���ݱ�����
        /// </summary>
        private string m_tableDesc;

        /// <summary>
        /// �Ƿ�ֻ��������Ķ���
        /// </summary>
        private bool m_declaredOnly = false;

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="tableName">��Ӧ���ݱ���</param>
        public DBTableInfoAttribute(string tableName)
        {
            this.m_tableName = tableName;
            this.m_tableDesc = string.Empty;
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="tableName">��Ӧ���ݱ���</param>
        /// <param name="tableDesc">��Ӧ���ݱ�����</param>
        public DBTableInfoAttribute(string tableName, string tableDesc)
        {
            this.m_tableName = tableName;
            this.m_tableDesc = tableDesc;
        }

        /// <summary>
        /// ��ȡ�����ö�Ӧ��������
        /// </summary>
        public string TableDesc
        {
            get { return this.m_tableDesc; }
            set { this.m_tableDesc = value; }
        }

        /// <summary>
        /// ��ȡ�����ö�Ӧ���ݱ���
        /// </summary>
        public string TableName
        {
            get { return this.m_tableName; }
            set { this.m_tableName = value; }
        }

        /// <summary>
        /// ��ȡ�������Ƿ�ֻ��������Ķ���
        /// </summary>
        public bool DeclaredOnly
        {
            get { return this.m_declaredOnly; }
            set { this.m_declaredOnly = value; }
        }
    }

    /// <summary>
    /// Model���Ի��ܼ���
    /// �����ֶ����Լ��ϣ�������
    /// </summary>
    public class ModelAttribute : Collection<DBFieldInfoAttribute>
    {
        /// <summary>
        /// Model�༶��ĸ�������
        /// </summary>
        private DBTableInfoAttribute m_attrTable;

        /// <summary>
        /// ��ȡ������Model�༶��ĸ�������
        /// </summary>
        public DBTableInfoAttribute AttrTable
        {
            get { return this.m_attrTable; }
            set { this.m_attrTable = value; }
        }

        /// <summary>
        /// ��ȡ�ֶ����Լ�������OrderBy���
        /// </summary>
        public string OrderBy
        {
            get
            {
                ModelAttribute temp = new ModelAttribute();
                foreach (DBFieldInfoAttribute f in Items)
                {
                    if (f.OrderIndex != -1)
                    {
                        temp.Add(f);
                    }
                }

                temp.Sort();

                string orderBy = string.Empty;
                foreach (MyAttribute.DBFieldInfoAttribute f in temp)
                {
                    orderBy += string.Format("{0}{1}, ", f.ColumnNameFix, f.OrderDirection);
                }

                if (orderBy != string.Empty)
                {
                    orderBy = " ORDER BY " + orderBy.Substring(0, orderBy.Length - 2);
                }

                return orderBy;
            }
        }

        /// <summary>
        /// ���ֶεĸ������Ը���OrderIndex�����û�����OrderBy���
        /// </summary>
        private void Sort()
        {
            int n = Count - 1;
            for (int i = 1; i <= n; i++)
            {
                bool flag = false;

                for (int j = 0; j <= n - i; j++)
                {
                    if (Items[j].OrderIndex > Items[j + 1].OrderIndex)
                    {
                        DBFieldInfoAttribute f = Items[j];
                        Items[j] = Items[j + 1];
                        Items[j + 1] = f;
                        flag = true;
                    }
                }

                if (!flag)
                {
                    break;
                }
            }
        }
    }
}
