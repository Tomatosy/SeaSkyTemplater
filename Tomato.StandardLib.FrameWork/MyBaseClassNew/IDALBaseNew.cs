using Tomato.StandardLib.MyModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Tomato.StandardLib.MyBaseClass
{
	public interface IDALBaseNew<T> : IDALBaseNew<T, T, T> where T : SystemModel, new()
	{
	}
	public interface IDALBaseNew<T, OutputT, ViewT> where T : SystemModel, new()where OutputT : class, T, new()where ViewT : class, T, new()
	{
		string GetOperater();

		LikeMode BeginLikeMode();

		int DeleteAll();

		int DeleteWithIdentity(params long[] identitys);

		int DeleteWithKeys(params T[] models);

		int DeleteWithModel(params T[] models);

		void TruncateTable();

		int GetCount();

		int GetCount(T model);

		Guid GetGuid();

		int GetIdentity();

		DataTable GetSchemaTable();

		DateTime GetServerTime();

		int Insert(T model);

		OutputT InsertAndReturn(T model);

		int InsertCol(T[] models);

		IEnumerable<OutputT> List();

		IEnumerable<OutputT> List(T model);

		IEnumerable<OutputT> List(string sql, Collection<IDataParameter> parms = null);

		DataTable ListTable(T model);

		DataTable ListTable();

		DataTable ListTable(string sql, Collection<IDataParameter> parms = null);

		DataSet ListDataSet(string sql, Collection<IDataParameter> parms = null);

		IEnumerable<OutputT> Search(T model, bool or);

		OutputT SelectWithIdentity(long identity);

		OutputT SelectWithKeys(T model);

		OutputT SelectWithModel(T model);

		ViewT SelectWithViewModel(T model);

		OutputT Select(string sql, Collection<IDataParameter> parms = null);

		ViewT SelectView(string sql, Collection<IDataParameter> parms = null);

		int UpdateWithIdentity(params T[] models);

		OutputT UpdateWithIdentityAndReturn(T model);

		IEnumerable<OutputT> UpdateWithIdentityAndReturn(T[] models);

		int UpdateWithKeys(params T[] models);

		OutputT UpdateWithKeysAndReturn(T model);

		IEnumerable<OutputT> UpdateWithKeysAndReturn(T[] models);

		int UpdateWithModel(T value, T condition);

		IEnumerable<OutputT> UpdateWithModelAndReturn(T value, T condition);

		int ExecuteProvidedSql(string sql);

		int ExecuteProvidedSql(string sql, Collection<IDataParameter> parms = null);

		int ExecuteStoredProc(string storedProcName);

		int ExecuteStoredProc(string storedProcName, params object[] parms);

		SelectViewMode BeginSelView();
	}
}
