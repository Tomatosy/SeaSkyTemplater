using Tomato.StandardLib.DAL.Base;
using Tomato.StandardLib.MyAttribute;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Tomato.StandardLib.MyBaseClass
{
    /// <summary>
    /// 数据库操作对象（抽象类）
    /// 具体的数据库操作类通过此类派生
    /// </summary>
    public abstract class DbDataHelper : MasterRepositoryBase, IDbDataHelper
    {
        /// <summary>
        /// 构造DbDataHelper
        /// </summary>
        /// <param name="connName">数据库连接名</param>
        public DbDataHelper(string connName) : base(connName)
        {

        }
        /// <summary>
        /// 获取SQL参数前缀，默认为@
        /// </summary>
        public virtual string ParameterPrefix
        {
            get { return "@"; }
        }

        /// <summary>
        /// 获取SQL字段前缀，默认为[
        /// </summary>
        public virtual string Prefix
        {
            get { return "["; }
        }

        /// <summary>
        /// 获取SQL字段后缀，默认为]
        /// </summary>
        public virtual string Suffix
        {
            get { return "]"; }
        }

        /// <summary>
        /// 获取数据库操作对象，需实现
        /// </summary>
        protected abstract IDbDataHelper DataHelper { get; }

        /// <summary>
        /// 获取或设置数据库表结构集合，需实现
        /// </summary>
        protected abstract TableCollection Tables { get; set; }

        /// <summary>
        /// 根据Model类返回对应的数据库表结构信息
        /// </summary>
        /// <param name="t">Model类的类型</param>
        /// <returns>对应的数据库表结构信息</returns>
        public virtual TableInfo GetTableInformation(Type t)
        {
            TableInfo table;

            if (this.Tables == null)
            {
                this.Tables = new TableCollection();
            }
            else
            {
                table = this.Tables[t.ToString()];
                if (table != null)
                {
                    return table;
                }
            }

            table = new TableInfo();
            table.Initial(GetAttributes(t), this.DataHelper, t);
            this.Tables.Add(table);
            return table;
        }

        /// <summary>
        /// 获取指定数据表内新的自动增量
        /// </summary>
        /// <param name="tableName">数据表名</param>
        /// <returns>自动增量</returns>
        public virtual int ExecuteForIdentity(string tableName)
        {
            object o = ExecuteScalar(string.Format("SELECT IDENT_CURRENT('{0}') AS 'NewID'", tableName));
            if (o == null)
            {
                return 0;
            }
            return int.Parse(o.ToString());
        }

        /// <summary>
        /// 执行SQL命令返回影响记录行数
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后影响记录行数</returns>
        public virtual int ExecuteNonQuery(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms)
        {
            using (var cmd = this.Database.GetSqlStringCommand(cmdText))
            {
                cmd.CommandType = cmdType;
                foreach (var parm in cmdParms)
                {
                    this.Database.AddInParameter(cmd, parm.ParameterName, parm.DbType, parm.Value);
                }
                int val = this.Database.ExecuteNonQuery(cmd);
                return val;
            }
        }

        /// <summary>
        /// 执行SQL命令返回影响记录行数
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后影响记录行数</returns>
        public virtual int ExecuteNonQuery(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms)
        {
            using (var cmd = this.Database.GetSqlStringCommand(cmdText))
            {
                cmd.CommandType = cmdType;
                foreach (var parm in cmdParms)
                {
                    this.Database.AddInParameter(cmd, parm.ParameterName, parm.DbType, parm.Value);
                }
                int val = this.Database.ExecuteNonQuery(cmd);
                return val;
            }
        }

        /// <summary>
        /// 执行SQL命令返回影响记录行数
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后影响记录行数</returns>
        public virtual int ExecuteNonQuery(string cmdText, params IDataParameter[] cmdParms)
        {
            return this.ExecuteNonQuery(CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行SQL命令返回影响记录行数
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后影响记录行数</returns>
        public virtual int ExecuteNonQuery(string cmdText, Collection<IDataParameter> cmdParms)
        {
            return this.ExecuteNonQuery(CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行SQL命令返回数据表
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的数据表</returns>
        public virtual DataTable FillDataTable(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms)
        {
            using (var cmd = this.Database.GetSqlStringCommand(cmdText))
            {
                cmd.CommandType = cmdType;
                foreach (var parm in cmdParms)
                {
                    this.Database.AddInParameter(cmd, parm.ParameterName, parm.DbType, parm.Value);
                }

                var dataSet = this.Database.ExecuteDataSet(cmd);
                return dataSet.Tables[0];
            }
        }

        /// <summary>
        /// 执行SQL命令返回数据表
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的数据表</returns>
        public virtual DataTable FillDataTable(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms)
        {
            using (var cmd = this.Database.GetSqlStringCommand(cmdText))
            {
                cmd.CommandType = cmdType;
                foreach (var parm in cmdParms)
                {
                    this.Database.AddInParameter(cmd, parm.ParameterName, parm.DbType, parm.Value);
                }

                var dataSet = this.Database.ExecuteDataSet(cmd);
                return dataSet.Tables[0];
            }
        }

        /// <summary>
        /// 执行SQL命令返回数据表
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的数据表</returns>
        public virtual DataTable FillDataTable(string cmdText, params IDataParameter[] cmdParms)
        {
            return this.FillDataTable(CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行SQL命令返回数据表
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的数据表</returns>
        public virtual DataTable FillDataTable(string cmdText, Collection<IDataParameter> cmdParms)
        {
            return this.FillDataTable(CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行SQL命令返回数据集
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的数据集</returns>
        public virtual DataSet FillDataSet(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms)
        {
            using (var cmd = this.Database.GetSqlStringCommand(cmdText))
            {
                cmd.CommandType = cmdType;
                foreach (var parm in cmdParms)
                {
                    this.Database.AddInParameter(cmd, parm.ParameterName, parm.DbType, parm.Value);
                }

                var dataSet = this.Database.ExecuteDataSet(cmd);
                return dataSet;
            }
        }

        /// <summary>
        /// 执行SQL命令返回数据集
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的数据集</returns>
        public virtual DataSet FillDataSet(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms)
        {
            using (var cmd = this.Database.GetSqlStringCommand(cmdText))
            {
                cmd.CommandType = cmdType;
                foreach (var parm in cmdParms)
                {
                    this.Database.AddInParameter(cmd, parm.ParameterName, parm.DbType, parm.Value);
                }

                var dataSet = this.Database.ExecuteDataSet(cmd);
                return dataSet;
            }
        }

        /// <summary>
        /// 执行SQL命令返回数据集
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的数据集</returns>
        public virtual DataSet FillDataSet(string cmdText, params IDataParameter[] cmdParms)
        {
            return this.FillDataSet(CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行SQL命令返回数据集
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的数据集</returns>
        public virtual DataSet FillDataSet(string cmdText, Collection<IDataParameter> cmdParms)
        {
            return this.FillDataSet(CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行SQL命令返回DataReader
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的DataReader</returns>
        public virtual IDataReader ExecuteReader(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms)
        {
            using (var cmd = this.Database.GetSqlStringCommand(cmdText))
            {
                cmd.CommandType = cmdType;
                foreach (var parm in cmdParms)
                {
                    this.Database.AddInParameter(cmd, parm.ParameterName, parm.DbType, parm.Value);
                }

                var rdr = this.Database.ExecuteReader(cmd);
                return rdr;
            }
        }

        /// <summary>
        /// 执行SQL命令返回DataReader
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的DataReader</returns>
        public virtual IDataReader ExecuteReader(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms)
        {
            using (var cmd = this.Database.GetSqlStringCommand(cmdText))
            {
                cmd.CommandType = cmdType;
                foreach (var parm in cmdParms)
                {
                    this.Database.AddInParameter(cmd, parm.ParameterName, parm.DbType, parm.Value);
                }
                var rdr = this.Database.ExecuteReader(cmd);
                return rdr;
            }
        }

        /// <summary>
        /// 执行SQL命令返回DataReader
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的DataReader</returns>
        public virtual IDataReader ExecuteReader(string cmdText, params IDataParameter[] cmdParms)
        {
            return this.ExecuteReader(CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行SQL命令返回DataReader
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的DataReader</returns>
        public virtual IDataReader ExecuteReader(string cmdText, Collection<IDataParameter> cmdParms)
        {
            return this.ExecuteReader(CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行SQL命令返回DataReader,(返回列和主键信息)
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的DataReader(列和主键信息)</returns>
        public virtual IDataReader ExecuteReaderForKeyInfo(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms)
        {
            using (var cmd = this.Database.GetSqlStringCommand(cmdText))
            {
                cmd.CommandType = cmdType;
                cmd.Parameters.AddRange(cmdParms);
                var rdr = this.Database.ExecuteReader(cmd);
                return rdr;
            }
        }

        /// <summary>
        /// 执行SQL命令返回DataReader,(返回列和主键信息)
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的DataReader(列和主键信息)</returns>
        public virtual IDataReader ExecuteReaderForKeyInfo(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms)
        {
            using (var cmd = this.Database.GetSqlStringCommand(cmdText))
            {
                cmd.CommandType = cmdType;
                foreach (var parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
                var rdr = this.Database.ExecuteReader(cmd);
                return rdr;
            }
        }

        /// <summary>
        /// 执行SQL命令返回DataReader,(返回列和主键信息)
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的DataReader(列和主键信息)</returns>
        public virtual IDataReader ExecuteReaderForKeyInfo(string cmdText, params IDataParameter[] cmdParms)
        {
            return this.ExecuteReaderForKeyInfo(CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行SQL命令返回DataReader,(返回列和主键信息)
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的DataReader(列和主键信息)</returns>
        public virtual IDataReader ExecuteReaderForKeyInfo(string cmdText, Collection<IDataParameter> cmdParms)
        {
            return this.ExecuteReaderForKeyInfo(CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行SQL命令返回首行首列对象，集合为空时返回null
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的首行首列对象</returns>
        public virtual object ExecuteScalar(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms)
        {
            using (var cmd = this.Database.GetSqlStringCommand(cmdText))
            {
                cmd.CommandType = cmdType;
                cmd.Parameters.AddRange(cmdParms);
                object val = this.Database.ExecuteScalar(cmd);
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行SQL命令返回首行首列对象，集合为空时返回null
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的首行首列对象</returns>
        public virtual object ExecuteScalar(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms)
        {
            using (var cmd = this.Database.GetSqlStringCommand(cmdText))
            {
                cmd.CommandType = cmdType;
                foreach (var parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
                object val = this.Database.ExecuteScalar(cmd);
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行SQL命令返回首行首列对象，集合为空时返回null
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的首行首列对象</returns>
        public virtual object ExecuteScalar(string cmdText, params IDataParameter[] cmdParms)
        {
            return this.ExecuteScalar(CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行SQL命令返回首行首列对象，集合为空时返回null
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的首行首列对象</returns>
        public virtual object ExecuteScalar(string cmdText, Collection<IDataParameter> cmdParms)
        {
            return this.ExecuteScalar(CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 创建SQL参数类型
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="p_dbType">数据库数据类型</param>
        /// <param name="srcColumn">数据库列名</param>
        /// <returns>SQL参数类型</returns>
        public abstract IDataParameter Parameter(string parameterName, object p_dbType, string srcColumn);

        /// <summary>
        /// 复制SQL参数类型
        /// </summary>
        /// <param name="parm">要复制的参数对象</param>
        /// <returns>复制后的参数对象</returns>
        public abstract IDataParameter CloneParameter(IDataParameter parm);

        /// <summary>
        /// 根据Model类中的字段属性获取对应的数据库数据类型
        /// </summary>
        /// <param name="f">Model类中的字段属性</param>
        /// <returns>对应的数据库数据类型</returns>
        public abstract object DbType(DBFieldInfoAttribute f);

        /// <summary>
        /// 根据Model类型，获取类级别属性
        /// </summary>
        /// <param name="t">Model类型</param>
        /// <returns>类级别属性</returns>
        internal static ModelAttribute GetAttributes(Type t)
        {
            // get table name
            System.Attribute attr = System.Attribute.GetCustomAttribute(t, typeof(MyAttribute.DBTableInfoAttribute));
            if (attr == null)
            {
                throw new MyModel.MyException(t.ToString() + " 类没有应用DbTableInfoAttribute属性，无法获取数据库表名。");
            }

            MyAttribute.DBTableInfoAttribute attrTable = (MyAttribute.DBTableInfoAttribute)attr;

            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            if (attrTable.DeclaredOnly)
            {
                flags = flags | BindingFlags.DeclaredOnly;
            }

            FieldInfo[] fields = t.GetFields(flags);

            MyAttribute.ModelAttribute attrFields = new MyAttribute.ModelAttribute();
            foreach (FieldInfo field in fields)
            {
                object[] o = field.GetCustomAttributes(typeof(MyAttribute.DBFieldInfoAttribute), false);

                // foreach 肯定只会循环一次
                foreach (MyAttribute.DBFieldInfoAttribute f in o)
                {
                    f.Field = field;
                    attrFields.Add(f);
                }
            }

            attrFields.AttrTable = attrTable;

            return attrFields;
        }

        /// <summary>
        /// 根据Command创建数据库适配对象
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <returns>数据库适配对象</returns>
        protected abstract DbDataAdapter DataAdapter(IDbCommand cmd);
    }
}
