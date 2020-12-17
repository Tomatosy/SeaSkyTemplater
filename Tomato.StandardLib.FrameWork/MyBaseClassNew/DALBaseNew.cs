using Tomato.StandardLib.DAL.Base;
using Tomato.StandardLib.MyAttribute;
using Tomato.StandardLib.MyModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Tomato.StandardLib.MyBaseClass
{
	public abstract class DALBaseNew : MasterRepositoryBase
	{
		private readonly DatabaseMode m_DatabaseMode;

		private LikeMode m_likeMode;

		private IDbDataHelper m_dataHelper;

		public string Operater => GetOperater();

		internal bool IsLikeMode => m_likeMode != null && m_likeMode.IsLikeMode;

		protected IDbDataHelper Internal_DataHelper
		{
			get
			{
				return m_dataHelper;
			}
			set
			{
				m_dataHelper = value;
			}
		}

		public abstract string GetOperater();

		public DALBaseNew(string connName, DatabaseMode dbMode)
			: base(connName)
		{
			m_DatabaseMode = dbMode;
			InitialDataHelper(connName);
		}

		public DALBaseNew(string connName)
			: base(connName)
		{
			InitialDataHelper(connName);
		}

		public DALBaseNew()
		{
			m_dataHelper = SQLHelper.Create();
		}

		private void InitialDataHelper(string connName)
		{
			if (connName == null)
			{
				m_dataHelper = SQLHelper.Create();
				return;
			}
			DatabaseMode databaseMode = m_DatabaseMode;
			if (databaseMode == DatabaseMode.SqlClient)
			{
				m_dataHelper = SQLHelper.Create(connName);
			}
			else
			{
				m_dataHelper = SQLHelper.Create(connName);
			}
		}

		public virtual LikeMode BeginLikeMode()
		{
			m_likeMode = new LikeMode();
			return m_likeMode;
		}

		public virtual int ExecuteProvidedSql(string sql)
		{
			using (DbCommand command = base.Database.GetSqlStringCommand(sql))
			{
				return base.Database.ExecuteNonQuery(command);
			}
		}

		public virtual int ExecuteProvidedSql(string sql, Collection<IDataParameter> parms = null)
		{
			if (parms == null)
			{
				parms = new Collection<IDataParameter>();
			}
			using (DbCommand command = base.Database.GetSqlStringCommand(sql))
			{
				foreach (IDataParameter parm in parms)
				{
					base.Database.AddInParameter(command, parm.ParameterName, parm.DbType, parm.Value);
				}
				return base.Database.ExecuteNonQuery(command);
			}
		}

		public virtual int ExecuteStoredProc(string storedProcName)
		{
			using (DbCommand command = base.Database.GetStoredProcCommand(storedProcName))
			{
				return base.Database.ExecuteNonQuery(command);
			}
		}

		public virtual int ExecuteStoredProc(string storedProcName, params object[] parms)
		{
			using (DbCommand command = base.Database.GetStoredProcCommand(storedProcName, parms))
			{
				return base.Database.ExecuteNonQuery(command);
			}
		}
	}
	public abstract class DALBaseNew<Model> : DALBaseNew<Model, Model> where Model : SystemModel, new()
	{
		public DALBaseNew()
		{
		}

		public DALBaseNew(string connName)
			: base(connName)
		{
		}

		public DALBaseNew(string connName, DatabaseMode dbMode)
			: base(connName, dbMode)
		{
		}
	}
	public abstract class DALBaseNew<Model, OutputModel> : DALBaseNew where Model : SystemModel, new()where OutputModel : class, Model, new()
	{
		private TableInfo m_table;

		protected TableInfo Table
		{
			get
			{
				return m_table;
			}
			set
			{
				m_table = value;
			}
		}

		protected ModelAttribute ModelAttributes => DbDataHelper.GetAttributes(typeof(Model));

		protected virtual IDbDataHelper DataHelper
		{
			get
			{
				return base.Internal_DataHelper;
			}
			set
			{
				base.Internal_DataHelper = value;
			}
		}

		protected string OrderBy
		{
			get
			{
				return Table.OrderByString;
			}
			set
			{
				Table.OrderByString = value;
			}
		}

		public DALBaseNew()
			: this("BaseConn")
		{
		}

		public DALBaseNew(string connName)
			: this(connName, DatabaseMode.SqlClient)
		{
		}

		public DALBaseNew(string connName, DatabaseMode dbMode)
			: base(connName, dbMode)
		{
			m_table = base.Internal_DataHelper.GetTableInformation(typeof(Model));
			OrderBy = null;
		}

		public virtual DataTable GetSchemaTable()
		{
			using (IDataReader dataReader = base.Internal_DataHelper.ExecuteReaderForKeyInfo(string.Format(Table.Select, " WHERE 1=2", string.Empty)))
			{
				return dataReader.GetSchemaTable();
			}
		}

		protected void Initial(ModelAttribute table)
		{
			m_table = new TableInfo();
			m_table.Initial(table, DataHelper, typeof(Model));
			OrderBy = null;
		}
	}
	public abstract class DALBaseNew<Model, OutputModel, ViewModel> : DALBaseNew where Model : SystemModel, new()where OutputModel : class, Model, new()where ViewModel : class, Model, new()
	{
		private TableInfo m_table;

		private SelectViewMode selectViewMode;

		protected TableInfo Table
		{
			get
			{
				return (selectViewMode?.IsSelectViewMode ?? false) ? selectViewMode.SelViewTable : m_table;
			}
			set
			{
				m_table = value;
			}
		}

		protected ModelAttribute ModelAttributes => DbDataHelper.GetAttributes(typeof(Model));

		protected virtual IDbDataHelper DataHelper
		{
			get
			{
				return base.Internal_DataHelper;
			}
			set
			{
				base.Internal_DataHelper = value;
			}
		}

		protected string OrderBy
		{
			get
			{
				return Table.OrderByString;
			}
			set
			{
				Table.OrderByString = value;
			}
		}

		public DALBaseNew()
			: this("BaseConn")
		{
		}

		public DALBaseNew(string connName)
			: this(connName, DatabaseMode.SqlClient)
		{
		}

		public DALBaseNew(string connName, DatabaseMode dbMode)
			: base(connName, dbMode)
		{
			m_table = base.Internal_DataHelper.GetTableInformation(typeof(Model));
			OrderBy = null;
		}

		public virtual DataTable GetSchemaTable()
		{
			using (IDataReader dataReader = base.Internal_DataHelper.ExecuteReaderForKeyInfo(string.Format(Table.Select, " WHERE 1=2", string.Empty)))
			{
				return dataReader.GetSchemaTable();
			}
		}

		protected void Initial(ModelAttribute table)
		{
			m_table = new TableInfo();
			m_table.Initial(table, DataHelper, typeof(Model));
			OrderBy = null;
		}

		public virtual SelectViewMode BeginSelView()
		{
			selectViewMode = new SelectViewMode();
			selectViewMode.SelViewTable = base.Internal_DataHelper.GetTableInformation(typeof(ViewModel));
			return selectViewMode;
		}

		public virtual int DeleteWithModel(params Model[] models)
		{
			int num = 0;
			foreach (Model o in models)
			{
				ParmCollection parmCollection = Table.PrepareConditionParms(o, base.IsLikeMode);
				num += base.Internal_DataHelper.ExecuteNonQuery(string.Format(Table.Delete, parmCollection.WhereSql), parmCollection);
			}
			return num;
		}

		public virtual int DeleteWithIdentity(params long[] identitys)
		{
			int num = 0;
			foreach (long identity in identitys)
			{
				ParmCollection parmCollection = Table.PrepareIdentityParm(identity);
				num += base.Internal_DataHelper.ExecuteNonQuery(string.Format(Table.Delete, parmCollection.WhereSql), parmCollection);
			}
			return num;
		}

		public virtual int DeleteWithKeys(params Model[] models)
		{
			int num = 0;
			foreach (Model o in models)
			{
				ParmCollection parmCollection = Table.PrepareKeysParms(o);
				num += base.Internal_DataHelper.ExecuteNonQuery(string.Format(Table.Delete, parmCollection.WhereSql), parmCollection);
			}
			return num;
		}

		public virtual int DeleteAll()
		{
			return base.Internal_DataHelper.ExecuteNonQuery(string.Format(Table.Delete, string.Empty));
		}

		public virtual void TruncateTable()
		{
			base.Internal_DataHelper.ExecuteNonQuery($"TRUNCATE TABLE {Table.TableName}");
		}

		public virtual int Insert(Model model)
		{
			model.GmtCreateUser = base.Operater;
			model.GmtCreateDate = DateTime.Now;
			model.GmtUpdateUser = base.Operater;
			model.GmtUpdateDate = DateTime.Now;
			ParmCollection cmdParms = Table.PrepareNotIdentityParms(model);
			return base.Internal_DataHelper.ExecuteNonQuery(Table.Insert, cmdParms);
		}

		public virtual OutputModel InsertAndReturn(Model model)
		{
			model.GmtCreateUser = base.Operater;
			model.GmtCreateDate = DateTime.Now;
			model.GmtUpdateUser = base.Operater;
			model.GmtUpdateDate = DateTime.Now;
			ParmCollection cmdParms = Table.PrepareNotIdentityParms(model);
			using (IDataReader dataReader = base.Internal_DataHelper.ExecuteReader(Table.Insert, cmdParms))
			{
				return dataReader.Read() ? ((OutputModel)Table.ReadDataReader(dataReader, new OutputModel())) : null;
			}
		}

		public virtual int InsertCol(Model[] models)
		{
			int num = 0;
			Collection<StringBuilder> collection = Table.PrepareInsertCol<Model, OutputModel>(models, base.Operater);
			foreach (StringBuilder item in collection)
			{
				num += base.Internal_DataHelper.ExecuteNonQuery(item.ToString());
			}
			return num;
		}

		public virtual IEnumerable<OutputModel> List()
		{
			return List((Model)null);
		}

		public virtual IEnumerable<OutputModel> List(Model model)
		{
			ParmCollection parmCollection = Table.PrepareConditionParms(model, base.IsLikeMode);
			return List(string.Format(Table.Select, parmCollection.WhereSql, OrderBy), parmCollection);
		}

		public virtual IEnumerable<OutputModel> List(string sql, Collection<IDataParameter> parms = null)
		{
			if (parms == null)
			{
				parms = new Collection<IDataParameter>();
			}
			using (IDataReader rdr = base.Internal_DataHelper.ExecuteReader(sql, parms))
			{
				while (rdr.Read())
				{
					yield return (OutputModel)Table.ReadDataReader(rdr, new OutputModel());
				}
			}
		}

		public virtual IEnumerable<ViewModel> ListView(string sql, Collection<IDataParameter> parms = null)
		{
			if (parms == null)
			{
				parms = new Collection<IDataParameter>();
			}
			using (IDataReader rdr = base.Internal_DataHelper.ExecuteReader(sql, parms))
			{
				while (rdr.Read())
				{
					yield return (ViewModel)Table.ReadDataReader(rdr, new ViewModel());
				}
			}
		}

		public virtual DataTable ListTable()
		{
			return ListTable((Model)null);
		}

		public virtual DataTable ListTable(Model model)
		{
			ParmCollection parmCollection = Table.PrepareConditionParms(model, base.IsLikeMode);
			return ListTable(string.Format(Table.Select, parmCollection.WhereSql, OrderBy), parmCollection);
		}

		public virtual DataTable ListTable(string sql, Collection<IDataParameter> parms = null)
		{
			if (parms == null)
			{
				parms = new Collection<IDataParameter>();
			}
			DataTable dataTable = base.Internal_DataHelper.FillDataTable(sql, parms);
			dataTable.TableName = Table.TableName;
			return dataTable;
		}

		public virtual DataSet ListDataSet(string sql, Collection<IDataParameter> parms = null)
		{
			if (parms == null)
			{
				parms = new Collection<IDataParameter>();
			}
			return base.Internal_DataHelper.FillDataSet(sql, parms);
		}

		public virtual IEnumerable<OutputModel> Search(Model model, bool or)
		{
			ParmCollection parmCollection = Table.PrepareSearchParms(model, or, base.IsLikeMode);
			return List(string.Format(Table.Select, parmCollection.WhereSql, OrderBy), parmCollection);
		}

		public virtual OutputModel SelectWithModel(Model model)
		{
			ParmCollection parmCollection = Table.PrepareConditionParms(model, base.IsLikeMode);
			return Select(string.Format(Table.Select, parmCollection.WhereSql, OrderBy), parmCollection);
		}

		public virtual ViewModel SelectWithViewModel(Model model)
		{
			ParmCollection parmCollection = Table.PrepareConditionParms(model, base.IsLikeMode);
			return SelectView(string.Format(Table.Select, parmCollection.WhereSql, OrderBy), parmCollection);
		}

		public virtual OutputModel SelectWithIdentity(long identity)
		{
			ParmCollection parmCollection = Table.PrepareIdentityParm(identity);
			return Select(string.Format(Table.Select, parmCollection.WhereSql, OrderBy), parmCollection);
		}

		public virtual OutputModel SelectWithKeys(Model model)
		{
			ParmCollection parmCollection = Table.PrepareKeysParms(model);
			return Select(string.Format(Table.Select, parmCollection.WhereSql, OrderBy), parmCollection);
		}

		public virtual int GetIdentity()
		{
			return base.Internal_DataHelper.ExecuteForIdentity(Table.TableName);
		}

		public Guid GetGuid()
		{
			string g = base.Internal_DataHelper.ExecuteScalar("select newid()").ToString();
			return new Guid(g);
		}

		public DateTime GetServerTime()
		{
			string s = base.Internal_DataHelper.ExecuteScalar("select getdate()").ToString();
			return DateTime.Parse(s);
		}

		public virtual int GetCount()
		{
			return GetCount(null);
		}

		public virtual int GetCount(Model model)
		{
			ParmCollection parmCollection = Table.PrepareConditionParms(model, base.IsLikeMode);
			object obj = base.Internal_DataHelper.ExecuteScalar(CommandType.Text, $"SELECT COUNT(*) FROM {Table.TableName} {parmCollection.WhereSql}", parmCollection);
			if (obj != null)
			{
				return int.Parse(obj.ToString());
			}
			return 0;
		}

		public virtual OutputModel Select(string sql, Collection<IDataParameter> parms = null)
		{
			if (parms == null)
			{
				parms = new Collection<IDataParameter>();
			}
			using (IDataReader dataReader = base.Internal_DataHelper.ExecuteReader(sql, parms))
			{
				return dataReader.Read() ? ((OutputModel)Table.ReadDataReader(dataReader, new OutputModel())) : null;
			}
		}

		public virtual ViewModel SelectView(string sql, Collection<IDataParameter> parms = null)
		{
			if (parms == null)
			{
				parms = new Collection<IDataParameter>();
			}
			using (IDataReader dataReader = base.Internal_DataHelper.ExecuteReader(sql, parms))
			{
				return dataReader.Read() ? ((ViewModel)Table.ReadDataReader(dataReader, new ViewModel())) : null;
			}
		}

		public virtual int UpdateWithModel(Model value, Model condition)
		{
			setSystemField(value);
			ParmCollection parmCollection = Table.PrepareUpdateParms(value, base.IsLikeMode, condition);
			return base.Internal_DataHelper.ExecuteNonQuery(string.Format(parmCollection.UpdateSql, parmCollection.WhereSql), parmCollection);
		}

		public virtual IEnumerable<OutputModel> UpdateWithModelAndReturn(Model value, Model condition)
		{
			setSystemField(value);
			ParmCollection parmCollection = Table.PrepareUpdateParms(value, base.IsLikeMode, condition);
			return List(string.Format(parmCollection.UpdateSql, parmCollection.WhereSql), parmCollection).ToList();
		}

		public virtual int UpdateWithIdentity(params Model[] models)
		{
			int num = 0;
			foreach (Model val in models)
			{
				setSystemField(val);
				ParmCollection parmCollection = Table.PrepareUpdateParms(val);
				parmCollection.AddRange(Table.PrepareAllParms(val));
				num += base.Internal_DataHelper.ExecuteNonQuery(string.Format(parmCollection.UpdateSql, Table.IdentityParm.WhereSql), parmCollection);
			}
			return num;
		}

		public virtual OutputModel UpdateWithIdentityAndReturn(Model model)
		{
			setSystemField(model);
			ParmCollection parmCollection = Table.PrepareUpdateParms(model);
			parmCollection.AddRange(Table.PrepareAllParms(model));
			return Select(string.Format(parmCollection.UpdateSql, Table.IdentityParm.WhereSql), parmCollection);
		}

		public virtual IEnumerable<OutputModel> UpdateWithIdentityAndReturn(Model[] models)
		{
			List<OutputModel> list = new List<OutputModel>();
			foreach (Model val in models)
			{
				setSystemField(val);
				ParmCollection parmCollection = Table.PrepareUpdateParms(val);
				parmCollection.AddRange(Table.PrepareAllParms(val));
				OutputModel item = Select(string.Format(parmCollection.UpdateSql, Table.IdentityParm.WhereSql), parmCollection);
				list.Add(item);
			}
			return list;
		}

		public virtual int UpdateWithKeys(params Model[] models)
		{
			int num = 0;
			foreach (Model val in models)
			{
				setSystemField(val);
				ParmCollection parmCollection = Table.PrepareUpdateParms(val);
				parmCollection.AddRange(Table.PrepareKeysParms(val));
				num += base.Internal_DataHelper.ExecuteNonQuery(string.Format(parmCollection.UpdateSql, Table.KeysParms.WhereSql), parmCollection);
			}
			return num;
		}

		public virtual OutputModel UpdateWithKeysAndReturn(Model model)
		{
			setSystemField(model);
			ParmCollection parmCollection = Table.PrepareUpdateParms(model);
			parmCollection.AddRange(Table.PrepareKeysParms(model));
			return Select(string.Format(parmCollection.UpdateSql, Table.KeysParms.WhereSql), parmCollection);
		}

		public virtual IEnumerable<OutputModel> UpdateWithKeysAndReturn(Model[] models)
		{
			List<OutputModel> list = new List<OutputModel>();
			foreach (Model val in models)
			{
				setSystemField(val);
				ParmCollection parmCollection = Table.PrepareUpdateParms(val);
				parmCollection.AddRange(Table.PrepareKeysParms(val));
				OutputModel item = Select(string.Format(parmCollection.UpdateSql, Table.KeysParms.WhereSql), parmCollection);
				list.Add(item);
			}
			return list;
		}

		private void setSystemField(Model model)
		{
			model.GmtCreateUser = null;
			model.GmtCreateDate = null;
			model.GmtUpdateUser = base.Operater;
			model.GmtUpdateDate = DateTime.Now;
		}
	}
}
