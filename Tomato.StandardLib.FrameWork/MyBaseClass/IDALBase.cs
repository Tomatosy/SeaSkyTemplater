using Tomato.StandardLib.MyModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Tomato.StandardLib.MyBaseClass
{
    /// <summary>
    /// DAL基（数据库Model）
    /// </summary>
    /// <typeparam name="T">数据库表对应的Model类型</typeparam>
    public interface IDALBase<T> : IDALBase<T, T>
        where T : SystemModel, new()
    {

    }

    /// <summary>
    /// DAL基（数据库Model,返回OutputModel）
    /// </summary>
    /// <typeparam name="T">数据库表对应的Model类型</typeparam>
    /// <typeparam name="OutputT">返回的OutputModel类型</typeparam>
    public interface IDALBase<T, OutputT>
        where T : SystemModel, new()
        where OutputT : class, T, new()
    {
        /// <summary>
        /// 获取数据库操作人(需要子类实现)
        /// </summary>
        /// <returns>操作人</returns>
        string GetOperater();

        /// <summary>
        /// 开启模糊查询
        /// </summary>
        /// <returns>模糊查询状态</returns>
        LikeMode BeginLikeMode();

        /// <summary>
        /// 删除全部记录
        /// </summary>
        /// <returns>影响的记录数</returns>
        int DeleteAll();

        /// <summary>
        /// 根据自增字段作为删除条件
        /// </summary>
        /// <param name="identitys">自动增量值集合</param>
        /// <returns>影响的记录数</returns>
        int DeleteWithIdentity(params long[] identitys);

        /// <summary>
        /// 根据Key字段作为删除条件，model里的其他任何值均不会作为删除条件，只有标识为Key的字段才会作为删除条件
        /// </summary>
        /// <param name="models">作为条件的model集合</param>
        /// <returns>影响的记录数</returns>
        int DeleteWithKeys(params T[] models);

        /// <summary>
        /// 根据model的值作为条件删除，model里每一个非null字段都会作为删除条件
        /// 支持模糊查询条件
        /// </summary>
        /// <param name="models">作为条件的model集合</param>
        /// <returns>影响的记录数</returns>
        int DeleteWithModel(params T[] models);

        /// <summary>
        /// 快速清空表内全部数据
        /// 不经过日志处理，Truncate Table 慎用
        /// </summary>
        void TruncateTable();

        /// <summary>
        /// 获取Model对应的数据库表内所有记录数合计
        /// </summary>
        /// <returns>记录数合计</returns>
        int GetCount();

        /// <summary>
        /// 根据model的值作为查询条件,model里每一个非null字段都会作为查询条件，
        /// 获取Model对应的数据库表内符合条件的记录数合计
        /// 支持模糊查询
        /// </summary>
        /// <param name="model">作为条件的model</param>
        /// <returns>记录数合计</returns>
        int GetCount(T model);

        /// <summary>
        /// 通过SQL生成Guid，与Model无关
        /// </summary>
        /// <returns>Guid</returns>
        Guid GetGuid();

        /// <summary>
        /// 获取Model对应的数据库表内最新生成的自动增量值
        /// </summary>
        /// <returns>最新生成的自动增量值</returns>
        int GetIdentity();

        /// <summary>
        /// 获取数据库表结构信息
        /// </summary>
        /// <returns>数据库表结构信息</returns>
        DataTable GetSchemaTable();

        /// <summary>
        /// 通过SQL获取数据库服务器的当前时间，与Model无关
        /// </summary>
        /// <returns>数据库服务器的当前时间</returns>
        DateTime GetServerTime();

        /// <summary>
        /// 把model里的除了标识为Identity,Timestamp之外的所有值插入数据库
        /// </summary>
        /// <param name="model">需要插入数据库的model</param>
        /// <returns>影响的记录数</returns>
        int Insert(T model);

        /// <summary>
        /// 把model里的除了标识为Identity,Timestamp之外的所有值插入数据库
        /// </summary>
        /// <param name="model">需要插入数据库的model</param>
        /// <returns>插入后的结果</returns>
        OutputT InsertAndReturn(T model);

        /// <summary>
        /// 把model集合里的model每个除了标识为Identity,Timestamp之外的所有值插入数据库
        /// </summary>
        /// <param name="models">需要插入数据库的</param>
        /// <returns>影响的记录数</returns>
        int InsertCol(T[] models);

        /// <summary>
        /// 以集合方式返回Model类型所对应数据库表的所有数据
        /// </summary>
        /// <returns>表内所有数据的Model集合</returns>
        IEnumerable<OutputT> List();

        /// <summary>
        /// 根据model的值作为条件查询条件，model里每一个非null字段都会作为查询条件
        /// 如果想查询所有记录，则model为null即可
        /// 支持模糊查询
        /// </summary>
        /// <param name="model">作为条件的model</param>
        /// <returns>符合查询条件的数据集合</returns>
        IEnumerable<OutputT> List(T model);

        /// <summary>
        /// 自定义sql查询，查询结果以集合形式返回
        /// 支持模糊查询
        /// </summary>
        /// <param name="sql">自定义sql语句</param>
        /// <param name="parms">执行参数集合</param>
        /// <returns>符合查询条件的集合</returns>
        IEnumerable<OutputT> List(string sql, Collection<IDataParameter> parms = null);

        /// <summary>
        /// 根据model的值查询，model里每一个非null字段都会作为查询条件
        /// 如果想查询所有记录，则model为null即可
        /// 支持模糊查询
        /// </summary>
        /// <param name="model">作为查询条件的model</param>
        /// <returns>符合查询条件的数据表</returns>
        DataTable ListTable(T model);

        /// <summary>
        /// 以数据表方式返回Model类型所对应数据库表的所有数据
        /// </summary>
        /// <returns>表内所有数据的DataTable</returns>
        DataTable ListTable();

        /// <summary>
        /// 自定义sql查询，查询结果以数据表形式返回
        /// </summary>
        /// <param name="sql">自定义sql语句</param>
        /// <param name="parms">执行参数集合</param>
        /// <returns>符合查询条件的数据表</returns>
        DataTable ListTable(string sql, Collection<IDataParameter> parms = null);

        /// <summary>
        /// 自定义sql查询，查询结果以DataSet形式返回
        /// </summary>
        /// <param name="sql">自定义sql语句</param>
        /// <param name="parms">执行参数集合</param>
        /// <returns>符合查询条件的数据表集合</returns>
        DataSet ListDataSet(string sql, Collection<IDataParameter> parms = null);

        /// <summary>
        /// 根据model的值和操作种类进行搜索model集合，model里每一个非null字段都会作为查询条件
        /// </summary>
        /// <param name="model">作为查询条件的model</param>
        /// <param name="or">操作符，true为or，false为and</param>
        /// <returns>符合查询条件的数据集合</returns>
        IEnumerable<OutputT> Search(T model, bool or);

        /// <summary>
        /// 以自动增量字段为查询条件，获取符合条件model
        /// </summary>
        /// <param name="identity">作为条件的自动增量</param>
        /// <returns>符合条件model</returns>
        OutputT SelectWithIdentity(long identity);

        /// <summary>
        /// 以主键字段为查询条件，model中的非主键字段均不作为查询条件，获取符合条件model
        /// </summary>
        /// <param name="model">作为条件的model</param>
        /// <returns>符合条件model</returns>
        OutputT SelectWithKeys(T model);

        /// <summary>
        /// 根据model的值作为查询条件,model里每一个非null字段都会作为查询条件
        /// 返回符合条件的第一个model
        /// 支持模糊查询
        /// </summary>
        /// <param name="model">作为条件的model</param>
        /// <returns>符合查询条件的第一个model</returns>
        OutputT SelectWithModel(T model);

        /// <summary>
        /// 自定义sql查询，返回满足条件的第一个Model
        /// </summary>
        /// <param name="sql">自定义sql</param>
        /// <param name="parms">执行参数集合</param>
        /// <returns>符合查询条件的model</returns>
        OutputT Select(string sql, Collection<IDataParameter> parms = null);

        /// <summary>
        /// 根据自增字段值更新，model里的其他任何值均不会作为更新条件
        /// 而model中除自动增量字段以外的所有不为null的字段将会被更新
        /// </summary>
        /// <param name="models">要更新的值集合</param>
        /// <returns>影响的记录数</returns>
        int UpdateWithIdentity(params T[] models);

        /// <summary>
        /// 根据自增字段值更新，model里的其他任何值均不会作为更新条件
        /// 而model中除自动增量字段以外的所有不为null的字段将会被更新
        /// </summary>
        /// <param name="model">要更新的值</param>
        /// <returns>影响的记录数</returns>
        OutputT UpdateWithIdentityAndReturn(T model);

        /// <summary>
        /// 根据自增字段值更新，model里的其他任何值均不会作为更新条件
        /// 而model集合中除自动增量字段以外的所有不为null的字段将会被更新
        /// </summary>
        /// <param name="models">要更新的值集合</param>
        /// <returns>更新后的结果</returns>
        IEnumerable<OutputT> UpdateWithIdentityAndReturn(T[] models);

        /// <summary>
        /// 根据主键字段值更新，model里的其他任何值均不会作为更新条件
        /// 而model中除主键字段以外的所有不为null的字段将会被更新
        /// </summary>
        /// <param name="models">要更新的值集合</param>
        /// <returns>影响的记录数</returns>
        int UpdateWithKeys(params T[] models);

        /// <summary>
        /// 根据主键字段值更新，model里的其他任何值均不会作为更新条件
        /// 而model中除主键字段以外的所有不为null的字段将会被更新
        /// </summary>
        /// <param name="model">要更新的值</param>
        /// <returns>更新后的值</returns>
        OutputT UpdateWithKeysAndReturn(T model);

        /// <summary>
        /// 根据主键字段值更新，model里的其他任何值均不会作为更新条件
        /// 而model中除主键字段以外的所有不为null的字段将会被更新
        /// </summary>
        /// <param name="models">要更新的值集合</param>
        /// <returns>更新后的值</returns>
        IEnumerable<OutputT> UpdateWithKeysAndReturn(T[] models);

        /// <summary>
        /// 根据condition把value里的值更新，condition中所有不为null的字段都将作为查询条件
        /// 而value中的所有不为null的字段将会被更新
        /// 支持模糊查询
        /// </summary>
        /// <param name="value">需要更新的值</param>
        /// <param name="condition">更新时的条件</param>
        /// <returns>影响的记录数</returns>
        int UpdateWithModel(T value, T condition);

        /// <summary>
        /// 根据condition把value里的值更新，condition中所有不为null的字段都将作为查询条件
        /// 而value中的所有不为null的字段将会被更新
        /// 支持模糊查询
        /// </summary>
        /// <param name="value">需要更新的值</param>
        /// <param name="condition">更新时的条件</param>
        /// <returns>更新后的值集合</returns>
        IEnumerable<OutputT> UpdateWithModelAndReturn(T value, T condition);

        /// <summary>
        /// 执行提供的自定义sql
        /// </summary>
        /// <param name="sql">自定义sql</param>
        /// <returns>返回影响行数</returns>
        int ExecuteProvidedSql(string sql);

        /// <summary>
        /// 执行提供的自定义sql
        /// </summary>
        /// <param name="sql">自定义sql</param>
        /// <param name="parms">参数</param>
        /// <returns>返回影响行数</returns>
        int ExecuteProvidedSql(string sql, Collection<IDataParameter> parms = null);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <returns>返回影响行数</returns>
        int ExecuteStoredProc(string storedProcName);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parms">存储过程参数</param>
        /// <returns>返回影响行数</returns>
        int ExecuteStoredProc(string storedProcName, params object[] parms);

        /// <summary>
        /// 使用outputModel的视图查询
        /// </summary>
        SelectViewMode BeginSelView();
    }
}
