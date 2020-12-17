using Tomato.StandardLib.MyAttribute;
using Tomato.StandardLib.MyModel;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace Tomato.StandardLib.MyBaseClass
{
    /// <summary>
    /// 存放从Model的辅助属性中读取的数据库相关表结构信息
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// model名称
        /// </summary>
        private string m_modelName;

        /// <summary>
        /// 对应的数据库名称
        /// </summary>
        private string m_tableName;

        /// <summary>
        /// 预生成的Selsect语句
        /// </summary>
        private string m_select;

        /// <summary>
        /// 预生成的Update语句
        /// </summary>
        private string m_update;

        /// <summary>
        /// 预生成的Insert语句
        /// </summary>
        private string m_insert;

        /// <summary>
        /// 预生成的Insert语句（批量插入）
        /// </summary>
        private string m_insertCol;

        /// <summary>
        /// 树结构主列字段名
        /// </summary>
        private string m_treeMainKeyColName;

        /// <summary>
        /// 树结构父列字段名
        /// </summary>
        private string m_treeParentKeyColName;

        /// <summary>
        /// 自动增量参数集合
        /// </summary>
        private ParmCollection m_identityParm;

        /// <summary>
        /// 主键参数集合
        /// </summary>
        private ParmCollection m_keysParms = new ParmCollection();

        /// <summary>
        /// 参数集合
        /// </summary>
        private Collection<IDataParameter> m_parameters;

        /// <summary>
        ///  这是临时设置的OrderBy语句，如果有此语句，则从Model获得OrderBy定义不用
        /// </summary>
        private string m_orderByString;

        /// <summary>
        /// Model类属性集合
        /// </summary>
        private ModelAttribute m_fields = new ModelAttribute();

        /// <summary>
        /// 获取或设置树结构父列字段名
        /// </summary>
        public string TreeParentKeyColName
        {
            get { return this.m_treeParentKeyColName; }
        }

        /// <summary>
        /// 获取或设置树结构主列字段名
        /// </summary>
        public string TreeMainKeyColName
        {
            get { return this.m_treeMainKeyColName; }
        }

        /// <summary>
        /// 获取或设置model名称
        /// </summary>
        public virtual string ModelName
        {
            get { return this.m_modelName; }
            set { this.m_modelName = value; }
        }

        /// <summary>
        /// 获取或设置对应的数据库名称
        /// </summary>
        public virtual string TableName
        {
            get { return this.m_tableName; }
            set { this.m_tableName = value; }
        }

        /// <summary>
        /// 获取或设置参数集合
        /// </summary>
        public Collection<IDataParameter> Parameters
        {
            get { return this.m_parameters; }
            set { this.m_parameters = value; }
        }

        /// <summary>
        /// 获取或设置Model类属性集合
        /// </summary>
        public virtual ModelAttribute Fields
        {
            get { return this.m_fields; }
            set { this.m_fields = value; }
        }

        /// <summary>
        /// 获取或设置自动增量参数集合
        /// </summary>
        public virtual ParmCollection IdentityParm
        {
            get { return this.m_identityParm; }
            set { this.m_identityParm = value; }
        }

        /// <summary>
        /// 获取或设置主键参数集合
        /// </summary>
        public virtual ParmCollection KeysParms
        {
            get { return this.m_keysParms; }
            set { this.m_keysParms = value; }
        }

        /// <summary>
        /// 获取或设置预生成的Selsect语句
        /// </summary>
        public virtual string Select
        {
            get { return string.Format(this.m_select, this.TableName, "{0}", "{1}"); }
            set { this.m_select = value; }
        }

        /// <summary>
        /// 获取或设置预生成的Update语句
        /// </summary>
        public virtual string Update
        {
            get { return string.Format(this.m_update, this.TableName, "{0}"); }
            set { this.m_update = value; }
        }

        /// <summary>
        /// 获取或设置预生成的Insert语句
        /// </summary>
        public virtual string Insert
        {
            get { return string.Format(this.m_insert, this.TableName); }
            set { this.m_insert = value; }
        }

        /// <summary>
        /// 获取或设置预生成的Delete语句
        /// </summary>
        public virtual string Delete
        {
            get { return string.Format("DELETE FROM {0} {1}", this.TableName, "{0}"); }
        }

        /// <summary>
        /// 获取或设置预生成的排序语句
        /// </summary>
        public virtual string OrderByString
        {
            get
            {
                if (this.m_orderByString == null)
                {
                    this.m_orderByString = this.Fields.OrderBy;
                }

                return this.m_orderByString;
            }

            set
            {
                this.m_orderByString = value;
            }
        }

        /// <summary>
        /// 根据具体Model对象生成Where参数及子句
        /// 对象中属性为null值的不作为生成条件
        /// </summary>
        /// <param name="o">Model对象</param>
        /// <param name="isLikeMode">是否模糊查询</param>
        /// <returns>Where参数及子句</returns>
        public virtual ParmCollection PrepareConditionParms(object o, bool isLikeMode)
        {
            ParmCollection temp = new ParmCollection();
            if (o == null)
            {
                return temp;
            }

            string where = string.Empty;
            foreach (IDataParameter p in this.Parameters)
            {
                foreach (DBFieldInfoAttribute f in this.Fields)
                {
                    object val = f.Field.GetValue(o);
                    if (f.ColumnName == p.SourceColumn && val != null && val.ToString() != string.Empty)
                    {
                        if (f.Field.FieldType == typeof(byte[]))
                        {//字段为byte[0]时不做查询条件
                            var byteVal = (byte[])val;
                            if (byteVal.Length == 0)
                            {
                                break;
                            }
                        }
                        string link;
                        if (f.Field.FieldType == typeof(string) && isLikeMode)
                        {
                            val = val.ToString().Replace("[", "[[]").Replace("%", "[%]").Replace("^", "[^]").Replace("_", "[_]");
                            link = "LIKE";
                            if (f.LikeEqual == EnumLikeMode.NoLike)
                            {
                                link = "=";
                            }
                            if (f.LikeEqual == EnumLikeMode.AllLike)
                            {
                                val = "%" + val + "%";
                            }
                            if (f.LikeEqual == EnumLikeMode.BackwardLike)
                            {
                                val = val + "%";
                            }
                            if (f.LikeEqual == EnumLikeMode.ForwardLike)
                            {
                                val = "%" + val;
                            }
                        }
                        else
                        {
                            val = f.Field.GetValue(o);
                            link = "=";
                        }
                        p.Value = val;
                        IDataParameter DP = new SqlParameter(p.ParameterName, p.Value);
                        where += string.Format("{0} {1} {2} AND ", f.ColumnNameFix, link, f.ParameterName);
                        temp.Add(DP);
                        break;
                    }
                }
            }

            if (where.Length > 0)
            {
                where = string.Format("{0}{1}", " WHERE ", where.Substring(0, where.Length - 5));
                temp.WhereSql = where;
            }

            return temp;
        }

        /// <summary>
        /// 根据具体Model对象生成Where参数及子句
        /// 对象中属性为null值的不作为生成条件
        /// </summary>
        /// <param name="o">Model对象</param>
        /// <param name="or">条件连接是否使用or，true为使用or，false为使用and</param>
        /// <param name="isLikeMode">是否模糊查询</param>
        /// <returns>Where参数及子句</returns>
        public virtual ParmCollection PrepareSearchParms(object o, bool or, bool isLikeMode)
        {
            ParmCollection temp = new ParmCollection();
            if (o == null)
            {
                return temp;
            }

            string where = string.Empty;
            foreach (IDataParameter p in this.Parameters)
            {
                foreach (DBFieldInfoAttribute f in this.Fields)
                {
                    object val = f.Field.GetValue(o);
                    if (f.ColumnName == p.SourceColumn && val != null && val.ToString() != string.Empty)
                    {
                        if (f.Field.FieldType == typeof(byte[]))
                        {//字段为byte[0]时不做查询条件
                            var byteVal = (byte[])val;
                            if (byteVal.Length == 0)
                            {
                                break;
                            }
                        }
                        string link;
                        if (f.Field.FieldType == typeof(string) && isLikeMode)
                        {
                            val = val.ToString().Replace("[", "[[]").Replace("%", "[%]").Replace("^", "[^]").Replace("_", "[_]");
                            link = "LIKE";
                            if (f.LikeEqual == EnumLikeMode.NoLike)
                            {
                                link = "=";
                            }
                            if (f.LikeEqual == EnumLikeMode.AllLike)
                            {
                                val = "%" + val + "%";
                            }
                            if (f.LikeEqual == EnumLikeMode.BackwardLike)
                            {
                                val = val + "%";
                            }
                            if (f.LikeEqual == EnumLikeMode.ForwardLike)
                            {
                                val = "%" + val;
                            }
                        }
                        else
                        {
                            val = f.Field.GetValue(o);
                            link = "=";
                        }

                        p.Value = val;
                        where += string.Format("{0} {1} {2}   {3} ", f.ColumnNameFix, link, f.ParameterName, or ? "OR" : "AND");
                        temp.Add(p);
                        break;
                    }
                }
            }

            if (where.Length > 0)
            {
                where = string.Format("{0}{1}", " WHERE ", where.Substring(0, where.Length - 5));
                temp.WhereSql = where;
            }

            return temp;
        }

        /// <summary>
        /// 根据具体Model对象生成Update参数及子句
        /// </summary>
        /// <param name="value">更新Model对象，用于生成Update部分，属性为null值的不作为生成条件</param>
        /// <param name="condition">条件Model对象，用于生成Where部分，属性为null值的不作为生成条件</param>
        /// <param name="isLikeMode">是否模糊查询</param>
        /// <returns>Update参数及子句</returns>
        public virtual ParmCollection PrepareUpdateParms(object value, bool isLikeMode = false, object condition = null)
        {
            ParmCollection temp = new ParmCollection();

            string update = string.Empty;
            foreach (IDataParameter p in this.Parameters)
            {
                foreach (DBFieldInfoAttribute f in this.Fields)
                {
                    if (f.ColumnName == p.SourceColumn && f.Field.GetValue(value) != null && f.SqlDbType != SqlDbType.Timestamp)
                    {
                        p.Value = f.Field.GetValue(value);
                        update += string.Format("{0}={1}U, ", f.ColumnNameFix, f.ParameterName);
                        IDataParameter pp = this.CloneParameter(p);
                        pp.ParameterName = f.ParameterName + "U";
                        temp.Add(pp);
                        break;
                    }
                }
            }

            if (update.Length == 0)
            {
                return temp;
            }

            ParmCollection where = this.PrepareConditionParms(condition, isLikeMode);

            update = string.Format("UPDATE {0} SET {1}  OUTPUT INSERTED.*  {2}", this.TableName, update.Substring(0, update.Length - 2), "{0}");
            where.UpdateSql = update;

            where.AddRange(temp);
            return where;
        }

        /// <summary>
        /// 根据具体Model对象获取非自动增量的参数集合
        /// </summary>
        /// <param name="o">Model对象</param>
        /// <returns>非自动增量的参数集合</returns>
        public virtual ParmCollection PrepareNotIdentityParms(object o)
        {
            ParmCollection temp = new ParmCollection();
            foreach (IDataParameter p in this.Parameters)
            {
                foreach (DBFieldInfoAttribute f in this.Fields)
                {
                    if (!f.IsIdentity && f.ColumnName == p.SourceColumn)
                    {
                        object v = f.Field.GetValue(o);
                        if (f.IsKey && v == null)
                        {
                            v = Guid.NewGuid();
                        }
                        if (f.DefaultValue != null)
                        {
                            if (f.DefaultValue.GetType().IsEnum)
                            {
                                f.DefaultValue = (int)f.DefaultValue;
                            }
                        }
                        p.Value = v == null ? (f.DefaultValue == null ? DBNull.Value : f.DefaultValue) : v;
                        p.DbType = v == null ? (f.DefaultValue == null ? p.DbType : DbType.String) : p.DbType;
                        temp.Add(p);
                        break;
                    }
                }
            }

            return temp;
        }

        /// <summary>
        /// 根据具体Model对象获取自动增量的参数集合
        /// </summary>
        /// <param name="identity">Model对象</param>
        /// <returns>自动增量的参数集合</returns>
        public virtual ParmCollection PrepareIdentityParm(long identity)
        {
            if (this.IdentityParm == null || this.IdentityParm.OnlyParameter == null)
            {
                throw new MyModel.MyException("Model中没有设置任何Identity字段。");
            }

            foreach (DBFieldInfoAttribute f in this.Fields)
            {
                if (f.ColumnName == this.IdentityParm.OnlyParameter.SourceColumn)
                {
                    this.IdentityParm.OnlyParameter.Value = identity;
                    break;
                }
            }

            return this.IdentityParm;
        }

        /// <summary>
        /// 根据具体Model对象获取主键的参数集合
        /// </summary>
        /// <param name="o">Model对象</param>
        /// <returns>主键的参数集合</returns>
        public virtual ParmCollection PrepareKeysParms(object o)
        {
            if (this.KeysParms.Count == 0)
            {
                throw new MyModel.MyException("Model中没有设置任何Key字段。");
            }

            foreach (IDataParameter p in this.KeysParms)
            {
                foreach (DBFieldInfoAttribute f in this.Fields)
                {
                    if (f.ColumnName == p.SourceColumn)
                    {
                        object v = f.Field.GetValue(o);
                        p.Value = v == null ? DBNull.Value : v;
                        break;
                    }
                }
            }

            return this.KeysParms;
        }

        /// <summary>
        /// 根据具体Model对象获取所有的参数集合
        /// </summary>
        /// <param name="o">Model对象</param>
        /// <returns>所有参数集合</returns>
        public virtual Collection<IDataParameter> PrepareAllParms(object o)
        {
            foreach (IDataParameter p in this.Parameters)
            {
                foreach (DBFieldInfoAttribute f in this.Fields)
                {
                    if (f.ColumnName == p.SourceColumn)
                    {
                        object v = f.Field.GetValue(o);
                        p.Value = v == null ? DBNull.Value : v;
                        break;
                    }
                }
            }

            return this.Parameters;
        }

        /// <summary>
        /// 根据Model生成批量Insert语句
        /// </summary>
        /// <typeparam name="Model">数据库Model类型</typeparam>
        /// <typeparam name="OutputModel">数据库返回Model集合类型</typeparam>
        /// <param name="models">Model对象集合</param>
        /// <param name="operater">数据库操作人</param>
        /// <returns>批量Insert语句</returns>
        public virtual Collection<StringBuilder> PrepareInsertCol<Model, OutputModel>(Model[] models, string operater)
            where Model : SystemModel, new()
            where OutputModel : Model, new()
        {
            Collection<StringBuilder> sc = new Collection<StringBuilder>();
            if (models == null)
            {
                return sc;
            }

            foreach (DBFieldInfoAttribute ff in this.Fields)
            {
                if (ff.Field.FieldType == typeof(byte[]) && ff.SqlDbType != SqlDbType.Timestamp)
                {
                    throw new Exception("有byte[]不允许执行InsertCol。");
                }
            }

            StringBuilder s = new StringBuilder();

            int columnNum = (int)(30000 / this.Fields.Count);

            for (int i = 0; i < models.Length; i++)
            {
                string val = string.Empty;
                foreach (DBFieldInfoAttribute f in this.Fields)
                {
                    if (f.IsIdentity || f.SqlDbType == SqlDbType.Timestamp)
                    {
                        continue;
                    }
                    object v = f.Field.GetValue(models[i]);
                    if (f.DefaultValue != null)
                    {
                        if (f.DefaultValue.GetType().IsEnum)
                        {
                            f.DefaultValue = (int)f.DefaultValue;
                        }
                    }
                    v = v == null ? (f.DefaultValue == null ? DBNull.Value : f.DefaultValue) : v;
                    //杨永山 2015年5月8日 11:29:10 添加字段是否为Guid判断
                    if (f.Field.FieldType == typeof(string) || f.Field.FieldType == typeof(Guid?))
                    {
                        if (f.IsKey && (v == null || string.IsNullOrEmpty(v.ToString())))
                        {
                            v = Guid.NewGuid();
                        }
                        if (f.Field.Name == "gmtCreateUser"
                           || f.Field.Name == "gmtUpdateUser"
                           )
                        {
                            v = operater;
                        }
                        val += string.Format("'{0}',", this.ConvertString(v));
                    }
                    else if (f.Field.FieldType == typeof(DateTime?))
                    {
                        if (f.Field.Name == "gmtCreateDate"
                            || f.Field.Name == "gmtUpdateDate"
                            )
                        {
                            v = DateTime.Now;
                        }
                        val += string.Format("'{0:yyyy-MM-dd HH:mm:ss}',", v);
                    }
                    else if (f.Field.FieldType == typeof(bool?))
                    {
                        bool? b = (bool?)v;

                        val += b == true ? "1," : "0,";
                    }
                    else
                    {
                        val += string.Format("{0},", v);
                    }
                }

                val = val.Substring(0, val.Length - 1);
                string tmp = string.Format(this.m_insertCol, this.TableName, val);
                tmp += "\r\n";

                s.Append(tmp);
                if ((i + 1) % columnNum == 0)
                {
                    sc.Add(s);
                    s = new StringBuilder();
                }
            }

            if (s.Length > 0)
            {
                sc.Add(s);
            }

            return sc;
        }

        /// <summary>
        /// 读取DataReader里的数据到传进来的object中，DataReader必须已经rdr.Read()过
        /// </summary>
        /// <param name="rdr">要读取的DataReader</param>
        /// <param name="o">读取到这个object中</param>
        /// <returns>返回读取好的object</returns>
        public virtual object ReadDataReader(IDataReader rdr, object o)
        {
            int fieldCount = rdr.FieldCount;
            for (int i = 0; i < fieldCount; i++)
            {
                bool isMatch = false;
                foreach (DBFieldInfoAttribute field in this.Fields)
                {
                    if (field.ColumnName == rdr.GetName(i))
                    {
                        // 必须保证类型是可空类型或者保证数据库为不可为空
                        field.Field.SetValue(o, rdr.IsDBNull(i) ? null : rdr.GetValue(i));
                        isMatch = true;
                        break;
                    }
                }

                if (!isMatch)
                {
                    PropertyInfo pi = o.GetType().GetProperty(rdr.GetName(i), BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    if (pi != null && pi.CanWrite && rdr[i] != DBNull.Value)
                    {
                        //如果是int?、bool?、double?等这种可空类型，获取其实际类型，如int?的实际类型是int
                        Type baseType = Nullable.GetUnderlyingType(pi.PropertyType);
                        if (baseType != null)
                            pi.SetValue(o, Convert.ChangeType(rdr[i], baseType), null);
                        else
                            pi.SetValue(o, Convert.ChangeType(rdr[i], pi.PropertyType), null);//设置对象值
                    }
                }
            }

            return o;
        }

        /// <summary>
        /// 根据Model数据表属性，预生成Select，Update，Delete，Insert语句主干
        /// </summary>
        /// <param name="table">Model数据表属性</param>
        /// <param name="dataHelper">数据库连接对象</param>
        /// <param name="t">Model类型</param>
        internal void Initial(ModelAttribute table, IDbDataHelper dataHelper, Type t)
        {
            string selectColumns = string.Empty;
            string updateColumns = string.Empty;
            string insertColumns = string.Empty;
            string identityColumn = string.Empty;
            string insertselectColumns = string.Empty;
            string keysColumns = string.Empty;
            this.m_parameters = new Collection<IDataParameter>();
            foreach (DBFieldInfoAttribute f in table)
            {
                if (f.Field == null)
                {
                    FieldInfo field = t.GetField(f.FieldName);
                    if (field == null)
                    {
                        throw new MyModel.MyException(string.Format("Model[{0}]中没有字段名为[{1}]的private或protected的字段。", t, f.FieldName));
                    }

                    f.Field = field;
                }

                f.ParameterPrefix = dataHelper.ParameterPrefix;
                f.Prefix = dataHelper.Prefix;
                f.Suffix = dataHelper.Suffix;

                IDataParameter parm = dataHelper.Parameter(f.ParameterName, dataHelper.DbType(f), f.ColumnName);
                this.m_parameters.Add(parm);

                // 自增字段
                if (f.IsIdentity)
                {
                    this.m_identityParm = new ParmCollection(parm);
                    this.m_identityParm.WhereSql = string.Format(" WHERE {0}={1} ", f.ColumnNameFix, f.ParameterName);
                    identityColumn = f.ColumnNameFix;
                }
                else
                {
                    if (f.SqlDbType != SqlDbType.Timestamp)
                    {
                        updateColumns += string.Format("{0}={1}, ", f.ColumnNameFix, f.ParameterName);
                        insertColumns += string.Format("{0}, ", f.ParameterName);
                        insertselectColumns += string.Format("{0}, ", f.ColumnNameFix);
                    }
                    selectColumns += string.Format("{0}, ", f.ColumnNameFix);
                }

                if (f.IsKey)
                {
                    this.m_keysParms.Add(parm);
                    keysColumns += string.Format("{0}={1} AND ", f.ColumnNameFix, f.ParameterName);
                }

                if (f.IsMainKey)
                {
                    this.m_treeMainKeyColName = f.ColumnName;
                }

                if (f.IsParentKey)
                {
                    this.m_treeParentKeyColName = f.ColumnName;
                }
            }

            if (identityColumn == string.Empty && selectColumns == string.Empty)
            {
                throw new MyModel.MyException(t.ToString() + " 类中没有一个Field应用DbFieldInfoAttribute属性。");
            }

            string select = selectColumns.Substring(0, selectColumns.Length - 2);
            string insertselect = insertselectColumns.Substring(0, insertselectColumns.Length - 2);

            this.m_fields = table;
            this.m_modelName = t.ToString();
            this.m_tableName = table.AttrTable.TableName;
            this.m_select = string.Format("SELECT {0}{1} FROM {2} {3} {4}", identityColumn == string.Empty ? string.Empty : (identityColumn + ", "), select, "{0}", "{1}", "{2}");
            this.m_update = string.Format("UPDATE {0} SET {1} {2}", "{0}", updateColumns.Substring(0, updateColumns.Length - 2), "{1}");
            this.m_insert = string.Format("INSERT INTO {0}({1}) OUTPUT INSERTED.* VALUES({2})", "{0}", insertselect, insertColumns.Substring(0, insertColumns.Length - 2));
            this.m_insertCol = string.Format("INSERT INTO {0}({1}) VALUES({2})", "{0}", insertselect, "{1}");
            this.m_keysParms.WhereSql = keysColumns.Length > 0 ? string.Format(" WHERE {0}", keysColumns.Substring(0, keysColumns.Length - 4)) : string.Empty;
        }

        /// <summary>
        /// 将对象转化为字符串，将一个单引号，转化为两个单引号
        /// </summary>
        /// <param name="o">待转化对象</param>
        /// <returns>转化后的字符串</returns>
        private string ConvertString(object o)
        {
            if (o == null || o == DBNull.Value)
            {
                return string.Empty;
            }

            return o.ToString().Replace("'", "''");
        }

        /// <summary>
        /// 复制参数集合
        /// </summary>
        /// <param name="parm">待复制的参数集合</param>
        /// <returns>复制后的参数集合</returns>
        private IDataParameter CloneParameter(IDataParameter parm)
        {
            SqlParameter p = (SqlParameter)parm;
            SqlParameter pp = new SqlParameter(p.ParameterName, p.SqlDbType, p.Size, p.SourceColumn);
            pp.Value = p.Value;
            return pp;
        }
    }

    /// <summary>
    /// 存放从Model的辅助属性中读取的数据库相关表结构信息，多个数据表
    /// </summary>
    public class TableCollection : Collection<TableInfo>
    {
        /// <summary>
        /// 获取指定Model名的数据库表结构信息
        /// </summary>
        /// <param name="modelName">Model名称</param>
        /// <returns>数据库表结构信息</returns>
        public TableInfo this[string modelName]
        {
            get
            {
                foreach (TableInfo t in Items)
                {
                    if (t.ModelName == modelName)
                    {
                        return t;
                    }
                }

                return null;
            }
        }
    }

    /// <summary>
    /// SQL语句中的参数集合及对应子句
    /// </summary>
    public class ParmCollection : Collection<IDataParameter>
    {
        /// <summary>
        /// Where子句
        /// </summary>
        private string m_whereSql = string.Empty;

        /// <summary>
        /// Update子句
        /// </summary>
        private string m_updateSql = string.Empty;

        /// <summary>
        /// Insert子句
        /// </summary>
        private string m_insertSql = string.Empty;

        /// <summary>
        /// 构造参数集合及子句
        /// </summary>
        public ParmCollection()
        {
        }

        /// <summary>
        /// 构造参数集合及子句
        /// </summary>
        /// <param name="parm">指定参数集合</param>
        public ParmCollection(IDataParameter parm)
        {
            Items.Add(parm);
        }

        /// <summary>
        /// 获取或设置Where子句
        /// </summary>
        public string WhereSql
        {
            get { return this.m_whereSql; }
            set { this.m_whereSql = value; }
        }

        /// <summary>
        /// 获取或设置Update子句
        /// </summary>
        public string UpdateSql
        {
            get { return this.m_updateSql; }
            set { this.m_updateSql = value; }
        }

        /// <summary>
        /// 获取参数集合
        /// </summary>
        public IDataParameter OnlyParameter
        {
            get
            {
                if (Items.Count > 0)
                {
                    return this[0];
                }

                return null;
            }
        }

        /// <summary>
        /// 添加到参数集合
        /// </summary>
        /// <param name="parms">参数集合</param>
        public void AddRange(Collection<IDataParameter> parms)
        {
            foreach (IDataParameter p in parms)
            {
                if (!this.Contains(p.ParameterName))
                {
                    Items.Add(p);
                }
            }
        }

        /// <summary>
        /// 判断是否包含指定参数名的参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <returns>true为包含，false为不包含</returns>
        public bool Contains(string parameterName)
        {
            foreach (IDataParameter p in Items)
            {
                if (p.ParameterName == parameterName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
