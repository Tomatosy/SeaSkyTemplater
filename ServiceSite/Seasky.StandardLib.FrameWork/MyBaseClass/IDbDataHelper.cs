// ===============================================================================
// This file is based on the Microsoft Data Access Application Block for .NET
// For more information please go to 
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
// ===============================================================================

using System;
using System.Collections.ObjectModel;
using System.Data;

namespace SeaSky.StandardLib.MyBaseClass
{
    /// <summary>
    /// 数据库操作对象（接口）
    /// 具体的数据库操作类必须继承此接口
    /// </summary>
    public interface IDbDataHelper
    {
        /// <summary>
        /// 获取SQL参数前缀，默认为@
        /// </summary>
        string ParameterPrefix { get; }

        /// <summary>
        /// 获取SQL字段前缀，默认为[
        /// </summary>
        string Prefix { get; }

        /// <summary>
        /// 获取SQL字段后缀，默认为]
        /// </summary>
        string Suffix { get; }

        /// <summary>
        /// 根据Model类返回对应的数据库表结构信息
        /// </summary>
        /// <param name="t">Model类的类型</param>
        /// <returns>对应的数据库表结构信息</returns>
        TableInfo GetTableInformation(Type t);

        /// <summary>
        /// 获取指定数据表内新的自动增量
        /// </summary>
        /// <param name="tableName">数据表名</param>
        /// <returns>自动增量</returns>
        int ExecuteForIdentity(string tableName);

        /// <summary>
        /// 执行SQL命令返回影响记录行数
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后影响记录行数</returns>
        int ExecuteNonQuery(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// 执行SQL命令返回影响记录行数
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后影响记录行数</returns>
        int ExecuteNonQuery(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// 执行SQL命令返回影响记录行数
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后影响记录行数</returns>
        int ExecuteNonQuery(string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// 执行SQL命令返回影响记录行数
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后影响记录行数</returns>
        int ExecuteNonQuery(string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// 执行SQL命令返回数据表
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的数据表</returns>
        DataTable FillDataTable(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// 执行SQL命令返回数据表
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的数据表</returns>
        DataTable FillDataTable(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// 执行SQL命令返回数据表
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的数据表</returns>
        DataTable FillDataTable(string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// 执行SQL命令返回数据表
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的数据表</returns>
        DataTable FillDataTable(string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// 执行SQL命令返回数据集
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的数据集</returns>
        DataSet FillDataSet(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// 执行SQL命令返回数据集
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的数据集</returns>
        DataSet FillDataSet(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// 执行SQL命令返回数据集
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的数据集</returns>
        DataSet FillDataSet(string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// 执行SQL命令返回数据集
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的数据集</returns>
        DataSet FillDataSet(string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// 执行SQL命令返回DataReader
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的DataReader</returns>
        IDataReader ExecuteReader(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// 执行SQL命令返回DataReader
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的DataReader</returns>
        IDataReader ExecuteReader(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// 执行SQL命令返回DataReader
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的DataReader</returns>
        IDataReader ExecuteReader(string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// 执行SQL命令返回DataReader
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的DataReader</returns>
        IDataReader ExecuteReader(string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// 执行SQL命令返回DataReader,(返回列和主键信息)
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的DataReader(列和主键信息)</returns>
        IDataReader ExecuteReaderForKeyInfo(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// 执行SQL命令返回DataReader,(返回列和主键信息)
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的DataReader(列和主键信息)</returns>
        IDataReader ExecuteReaderForKeyInfo(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// 执行SQL命令返回DataReader,(返回列和主键信息)
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的DataReader(列和主键信息)</returns>
        IDataReader ExecuteReaderForKeyInfo(string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// 执行SQL命令返回DataReader,(返回列和主键信息)
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的DataReader(列和主键信息)</returns>
        IDataReader ExecuteReaderForKeyInfo(string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// 执行SQL命令返回首行首列对象，集合为空时返回null
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的首行首列对象</returns>
        object ExecuteScalar(CommandType cmdType, string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// 执行SQL命令返回首行首列对象，集合为空时返回null
        /// </summary>
        /// <param name="cmdType">执行命令类型</param>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的首行首列对象</returns>
        object ExecuteScalar(CommandType cmdType, string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// 执行SQL命令返回首行首列对象，集合为空时返回null
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,数组方式</param>
        /// <returns>执行后返回的首行首列对象</returns>
        object ExecuteScalar(string cmdText, params IDataParameter[] cmdParms);

        /// <summary>
        /// 执行SQL命令返回首行首列对象，集合为空时返回null
        /// CommandType类型为CommandType.Text
        /// </summary>
        /// <param name="cmdText">执行SQL命令</param>
        /// <param name="cmdParms">执行SQL参数,集合方式</param>
        /// <returns>执行后返回的首行首列对象</returns>
        object ExecuteScalar(string cmdText, Collection<IDataParameter> cmdParms);

        /// <summary>
        /// 创建SQL参数类型
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="p_dbType">数据库数据类型</param>
        /// <param name="srcColumn">数据库列名</param>
        /// <returns>SQL参数类型</returns>
        IDataParameter Parameter(string parameterName, object p_dbType, string srcColumn);

        /// <summary>
        /// 根据Model类中的字段属性获取对应的数据库数据类型
        /// </summary>
        /// <param name="f">Model类中的字段属性</param>
        /// <returns>对应的数据库数据类型</returns>
        object DbType(MyAttribute.DBFieldInfoAttribute f);
    }
}