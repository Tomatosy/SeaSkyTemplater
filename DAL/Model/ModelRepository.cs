namespace Tomato.NewTempProject.DAL
{
    using Microsoft.Practices.Unity;
    using Tomato.StandardLib.MyBaseClass;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Tomato.StandardLib.DAL.Base;
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data.Common;
    using Tomato.StandardLib.MyModel;
    using Tomato.NewTempProject.Model;
    using System.Collections.ObjectModel;

    public class ModelRepository : DALPageBaseNew<ModelModel, ModelOutputModel, ModelViewModel>, IModelRepository
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public ModelRepository() : base("HealthAuditConn", DatabaseMode.SqlClient)
        {

        }

        /// <summary>
        /// 获取动态连接表数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetDynamicJoinTableData(TableSelModel model)
        {
            string joinTableID = model.JoinMasterTableName + "ID";
            if (model.JoinMasterTableName.ToLower() == "tb_projectsmanage")
            {
                joinTableID = "ProjectsManageID";
            }
            string sql = @"select * from  " + model.TableName + " TB left join " + model.JoinMasterTableName + " joinTB on TB.dynamicID=joinTB." + joinTableID + "  where TB.isdelete=0 and joinTB.isdelete=0 {0} ";
            Collection<IDataParameter> parms = new Collection<IDataParameter>();

            StringBuilder sqlWhere = new StringBuilder();

            foreach (TableColSelModel item in model.WhereSel)
            {
                sqlWhere.Append($@" AND TB.{item.ColName} like @{item.ColName}");
                parms.Add(new SqlParameter($"@{item.ColName}", "%" + item.ColValue + "%"));
            }
            return base.DataHelper.FillDataTable(string.Format(sql, sqlWhere), parms);
        }

        /// <summary>
        /// 获取动态表数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetDynamicTableData(TableSelModel model)
        {
            string sql = @"select * from  " + model.TableName + " where isdelete=0  {0} ";
            Collection<IDataParameter> parms = new Collection<IDataParameter>();

            StringBuilder sqlWhere = new StringBuilder();

            foreach (TableColSelModel item in model.WhereSel)
            {
                sqlWhere.Append($@" AND {item.ColName} like @{item.ColName}");
                parms.Add(new SqlParameter($"@{item.ColName}", "%" + item.ColValue + "%"));
            }
            return base.DataHelper.FillDataTable(string.Format(sql, sqlWhere), parms);
        }

        /// <summary>
        /// 获取数据库操作人
        /// </summary>
        /// <returns>操作人</returns>
        public override string GetOperater()
        {
            return ServiceContext.Current.UserName;
        }


        /// <summary>
        /// 批量删除动态表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<int> DelDynamicTableList(List<TableSelModel> inputModel)
        {
            int execCount = 0;
            string sql = @"
update [dbo].[{0}]
set isdelete=1
 where 1=1 {1}";
            Collection<IDataParameter> parms = new Collection<IDataParameter>();
            StringBuilder sqlWhere = new StringBuilder();
            foreach (TableSelModel model in inputModel)
            {
                parms = new Collection<IDataParameter>();
                sqlWhere = new StringBuilder();
                foreach (TableColSelModel col in model.WhereSel)
                {
                    sqlWhere.Append("and " + col.ColName + " = @" + col.ColName + ",");
                    parms.Add(new SqlParameter("@" + col.ColName, col.ColValue));
                }
                string sqlWhereStr = sqlWhere.ToString().Substring(0, sqlWhere.Length - 1);
                sql = string.Format(sql, model.TableName, sqlWhereStr);
                execCount += base.DataHelper.ExecuteNonQuery(sql, parms);
            }
            return new SuccessResultModel<int>(execCount);
        }

        /// <summary>
        /// 多行修改动态模块表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<int> MultiLineUpdateDynamicModel(List<TableSelModel> inputModel)
        {
            int execCount = 0;
            int ranNum = 0;
            foreach (TableSelModel model in inputModel)
            {
                string sql = @"
UPDATE [dbo].[{0}]
   SET {1} where 1=1 {2}";
                Collection<IDataParameter> parms = new Collection<IDataParameter>();
                StringBuilder sqlCol = new StringBuilder();
                StringBuilder sqlWhere = new StringBuilder();
                foreach (TableColSelModel col in model.ColSel)
                {
                    ranNum++;
                    sqlCol.Append(col.ColName + " = @" + col.ColName + ranNum + ",");
                    parms.Add(new SqlParameter("@" + col.ColName + ranNum, col.ColValue));
                }
                foreach (TableColSelModel col in model.WhereSel)
                {
                    ranNum++;
                    sqlWhere.Append("and " + col.ColName + " = @" + col.ColName + ranNum + ",");
                    parms.Add(new SqlParameter("@" + col.ColName + ranNum, col.ColValue));
                }

                string sqlColStr = sqlCol.ToString().Substring(0, sqlCol.Length - 1);
                string sqlWhereStr = sqlWhere.ToString().Substring(0, sqlWhere.Length - 1);
                sql = string.Format(sql, model.TableName, sqlColStr, sqlWhereStr);
                execCount += base.DataHelper.ExecuteNonQuery(sql, parms);
            }

            return new SuccessResultModel<int>(execCount);
        }

        /// <summary>
        /// 新增动态模块表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResultModel<int> InsertDynamicModel(TableSelModel model)
        {
            string sql = @"
INSERT INTO [dbo].[{0}]({1} isDelete)
     VALUES ({2} 0)";
            Collection<IDataParameter> parms = new Collection<IDataParameter>();

            StringBuilder sqlCol = new StringBuilder();
            StringBuilder sqlColValue = new StringBuilder();

            foreach (TableColSelModel col in model.ColSel)
            {
                sqlCol.Append(col.ColName + "  , ");
                sqlColValue.Append("@" + col.ColName + "  , ");
                parms.Add(new SqlParameter("@" + col.ColName, col.ColValue));
            }
            sql = string.Format(sql, model.TableName, sqlCol, sqlColValue);

            return new SuccessResultModel<int>(base.DataHelper.ExecuteNonQuery(sql, parms));
        }

        /// <summary>
        /// 删除数据库表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void DelDBTable(ModelInputModel model)
        {
            string sql = @"
                IF EXISTS(SELECT 1 FROM sysobjects WHERE id = OBJECT_ID('[{0}]'))
                drop table {0}
";
            sql = string.Format(sql, model.ModelCode);
            base.Internal_DataHelper.ExecuteNonQuery(sql);

        }


    }

}
