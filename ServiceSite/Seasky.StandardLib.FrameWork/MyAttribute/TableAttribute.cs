using SeaSky.StandardLib.MyModel;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Reflection;

namespace SeaSky.StandardLib.MyAttribute
{
    /// <summary>
    /// Model类字段辅助属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class DBFieldInfoAttribute : Attribute
    {
        /// <summary>
        /// 参数前缀
        /// </summary>
        private string m_parameterPrefix = "@";

        /// <summary>
        /// 字段前缀
        /// </summary>
        private string m_prefix = "[";

        /// <summary>
        /// 字段后缀
        /// </summary>
        private string m_suffix = "]";

        /// <summary>
        /// 列名
        /// </summary>
        private string m_columnName;

        /// <summary>
        /// 是否自动增量
        /// </summary>
        private bool m_isIdentity = false;

        /// <summary>
        /// 是否主键
        /// </summary>
        private bool m_isKey = false;

        /// <summary>
        /// 参数名称
        /// </summary>
        private string m_parameterName;

        /// <summary>
        /// 参数长度
        /// </summary>
        private int m_parameterSize = -1;

        /// <summary>
        /// SqlServer数据类型
        /// </summary>
        private SqlDbType m_sqlDbType = SqlDbType.Int;

        /// <summary>
        /// OleDb数据类型
        /// </summary>
        private OleDbType m_oleDbType = OleDbType.Integer;

        /// <summary>
        /// Oracle,mySql数据类型
        /// </summary>
        private DbType m_DBType = DbType.Int32;

        /// <summary>
        /// 排序先后
        /// </summary>
        private int m_orderIndex = -1;

        /// <summary>
        /// 排序方式
        /// </summary>
        private bool m_orderAsc = true;

        /// <summary>
        /// 是否主列（子列）
        /// </summary>
        private bool m_isMainKey = false;

        /// <summary>
        /// 是否父列
        /// </summary>
        private bool m_isParentKey = false;

        /// <summary>
        /// 字段反射对象
        /// </summary>
        private FieldInfo m_field;

        /// <summary>
        /// 字段名
        /// </summary>
        private string m_fieldName;

        /// <summary>
        /// 是否使用Like比较
        /// </summary>
        private EnumLikeMode m_likeEqual = EnumLikeMode.NoLike;

        /// <summary>
        /// 构造字段辅助属性
        /// </summary>
        public DBFieldInfoAttribute()
        {
        }

        /// <summary>
        /// 构造OleDb类型字段辅助属性
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="columnName">对应数据库字段名</param>
        /// <param name="isIdentity">是否自动增量，true为是，false为否</param>
        /// <param name="isKey">是否主键，true为是，false为否</param>
        /// <param name="oleDbType">OleDb数据类型</param>
        /// <param name="parameterSize">参数长度</param>
        /// <param name="orderIndex">排序先后，-1为不排序</param>
        /// <param name="orderAsc">排序方式，true为降序Asc，false为升序Desc</param>
        /// <param name="isMainKey">是否主列（子列），true为是，false为否</param>
        /// <param name="isParentKey">是否父列，treu为是，false为否</param>
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
        /// 构造OleDb类型字段辅助属性
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="columnName">对应数据库字段名</param>
        /// <param name="isIdentity">是否自动增量，true为是，false为否</param>
        /// <param name="isKey">是否主键，true为是，false为否</param>
        /// <param name="sqlDbType">SqlServer数据类型</param>
        /// <param name="parameterSize">参数长度</param>
        /// <param name="orderIndex">排序先后，-1为不排序</param>
        /// <param name="orderAsc">排序方式，true为降序Asc，false为升序Desc</param>
        /// <param name="isMainKey">是否主列（子列），true为是，false为否</param>
        /// <param name="isParentKey">是否父列，treu为是，false为否</param>
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
        ///// 构造OleDb类型字段辅助属性
        ///// </summary>
        ///// <param name="fieldName">字段名</param>
        ///// <param name="columnName">对应数据库字段名</param>
        ///// <param name="isIdentity">是否自动增量，true为是，false为否</param>
        ///// <param name="isKey">是否主键，true为是，false为否</param>
        ///// <param name="oracleType">Oracle数据类型</param>
        ///// <param name="parameterSize">参数长度</param>
        ///// <param name="orderIndex">排序先后，-1为不排序</param>
        ///// <param name="orderAsc">排序方式，true为降序Asc，false为升序Desc</param>
        ///// <param name="isMainKey">是否主列（子列），true为是，false为否</param>
        ///// <param name="isParentKey">是否父列，treu为是，false为否</param>
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
        /// 获取或设置是否通过Like来查询
        /// </summary>
        public EnumLikeMode LikeEqual
        {
            get { return this.m_likeEqual; }
            set { this.m_likeEqual = value; }
        }

        /// <summary>
        /// 获取或设置默认值
        /// </summary>
        public object DefaultValue
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置对应数据库列名
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
        /// 获取或设置是否自动增量
        /// </summary>
        public bool IsIdentity
        {
            get { return this.m_isIdentity; }
            set { this.m_isIdentity = value; }
        }

        /// <summary>
        /// 获取或设置是否主键
        /// </summary>
        public bool IsKey
        {
            get { return this.m_isKey; }
            set { this.m_isKey = value; }
        }

        /// <summary>
        /// 获取或设置排序先后
        /// </summary>
        public int OrderIndex
        {
            get { return this.m_orderIndex; }
            set { this.m_orderIndex = value; }
        }

        /// <summary>
        /// 获取或设置排序方式，true为降序Asc，false为升序Desc
        /// </summary>
        public bool OrderAsc
        {
            get { return this.m_orderAsc; }
            set { this.m_orderAsc = value; }
        }

        /// <summary>
        /// 获取排序方式SQL描述
        /// </summary>
        public string OrderDirection
        {
            get { return this.m_orderAsc ? string.Empty : " DESC"; }
        }

        /// <summary>
        /// 获取或设置参数名称
        /// 若为Null,获取的参数名称为参数前缀+对应数据列列名
        /// </summary>
        public string ParameterName
        {
            get { return this.m_parameterName == null ? this.m_parameterPrefix + this.ColumnName : this.m_parameterPrefix + this.m_parameterName; }
            set { this.m_parameterName = value; }
        }

        /// <summary>
        /// 获取或设置参数大小
        /// </summary>
        public int ParameterSize
        {
            get { return this.m_parameterSize; }
            set { this.m_parameterSize = value; }
        }

        /// <summary>
        /// 获取或设置SqlServer数据类型
        /// </summary>
        public SqlDbType SqlDbType
        {
            get { return this.m_sqlDbType; }
            set { this.m_sqlDbType = value; }
        }

        /// <summary>
        /// 获取或设置OleDb数据类型
        /// </summary>
        public OleDbType OleDbType
        {
            get { return this.m_oleDbType; }
            set { this.m_oleDbType = value; }
        }

        /// <summary>
        /// 获取或设置Oracle,mySql数据类型
        /// </summary>
        public DbType DBType
        {
            get { return this.m_DBType; }
            set { this.m_DBType = value; }
        }

        /// <summary>
        /// 获取对应字段名在SQL中的描述，字段前缀+字段名+字段后缀
        /// </summary>
        public string ColumnNameFix
        {
            get { return this.m_prefix + this.ColumnName + this.m_suffix; }
        }

        /// <summary>
        /// 获取或设置字段名称
        /// </summary>
        public string FieldName
        {
            get { return this.m_fieldName; }
            set { this.m_fieldName = value; }
        }

        /// <summary>
        /// 获取或设置是否主列（子列）
        /// </summary>
        public bool IsMainKey
        {
            get { return this.m_isMainKey; }
            set { this.m_isMainKey = value; }
        }

        /// <summary>
        /// 获取或设置是否父列
        /// </summary>
        public bool IsParentKey
        {
            get { return this.m_isParentKey; }
            set { this.m_isParentKey = value; }
        }

        /// <summary>
        /// 设置参数前缀
        /// </summary>
        internal string ParameterPrefix
        {
            set { this.m_parameterPrefix = value; }
        }

        /// <summary>
        /// 设置字段前缀
        /// </summary>
        internal string Prefix
        {
            set { this.m_prefix = value; }
        }

        /// <summary>
        /// 设置字段后缀
        /// </summary>
        internal string Suffix
        {
            set { this.m_suffix = value; }
        }

        /// <summary>
        /// 获取或设置反射字段
        /// </summary>
        internal FieldInfo Field
        {
            get { return this.m_field; }
            set { this.m_field = value; }
        }
    }

    /// <summary>
    /// Model，类级别辅助属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DBTableInfoAttribute : Attribute
    {
        /// <summary>
        /// 对应数据表名
        /// </summary>
        private string m_tableName;

        /// <summary>
        /// 对应数据表描述
        /// </summary>
        private string m_tableDesc;

        /// <summary>
        /// 是否只处理定义的对象
        /// </summary>
        private bool m_declaredOnly = false;

        /// <summary>
        /// 构造类属性
        /// </summary>
        /// <param name="tableName">对应数据表名</param>
        public DBTableInfoAttribute(string tableName)
        {
            this.m_tableName = tableName;
            this.m_tableDesc = string.Empty;
        }

        /// <summary>
        /// 构造类属性
        /// </summary>
        /// <param name="tableName">对应数据表名</param>
        /// <param name="tableDesc">对应数据表描述</param>
        public DBTableInfoAttribute(string tableName, string tableDesc)
        {
            this.m_tableName = tableName;
            this.m_tableDesc = tableDesc;
        }

        /// <summary>
        /// 获取或设置对应数据描述
        /// </summary>
        public string TableDesc
        {
            get { return this.m_tableDesc; }
            set { this.m_tableDesc = value; }
        }

        /// <summary>
        /// 获取或设置对应数据表名
        /// </summary>
        public string TableName
        {
            get { return this.m_tableName; }
            set { this.m_tableName = value; }
        }

        /// <summary>
        /// 获取或设置是否只处理定义的对象
        /// </summary>
        public bool DeclaredOnly
        {
            get { return this.m_declaredOnly; }
            set { this.m_declaredOnly = value; }
        }
    }

    /// <summary>
    /// Model属性汇总集合
    /// 包含字段属性集合，类属性
    /// </summary>
    public class ModelAttribute : Collection<DBFieldInfoAttribute>
    {
        /// <summary>
        /// Model类级别的辅助属性
        /// </summary>
        private DBTableInfoAttribute m_attrTable;

        /// <summary>
        /// 获取或设置Model类级别的辅助属性
        /// </summary>
        public DBTableInfoAttribute AttrTable
        {
            get { return this.m_attrTable; }
            set { this.m_attrTable = value; }
        }

        /// <summary>
        /// 获取字段属性集合生成OrderBy语句
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
        /// 对字段的辅助属性根据OrderIndex排序，用户生成OrderBy语句
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
