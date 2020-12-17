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
    using Tomato.StandardLib.MyExtensions;

    public class ModelDetailRepository : DALPageBaseNew<ModelDetailModel, ModelDetailOutputModel, ModelDetailViewModel>, IModelDetailRepository
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public ModelDetailRepository() : base("HealthAuditConn", DatabaseMode.SqlClient)
        {

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
        /// 初始化数据库表明细字段
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void InitDBTable(ModelInputModel model)
        {

            string firstTableSql = @"
                IF EXISTS(SELECT 1 FROM sysobjects WHERE id = OBJECT_ID('[{0}]'))
                drop table {0}

                /*==============================================================*/
                /* Table: {0}                                              */
                /*==============================================================*/
                CREATE TABLE [dbo].[{0}](
 ";

            string desSql = @"
)
declare @CurrentUser sysname
select @CurrentUser = 'dbo'
execute sp_addextendedproperty 'MS_Description', '{1}','user', @CurrentUser, 'table', '{0}'
";


            StringBuilder colSqlSB = new StringBuilder();
            StringBuilder desSqlSB = new StringBuilder();

            List<ModelDetailOutputModel> selModelDetail = this.List(new ModelDetailModel()
            {
                ModelID = model.ModelID
            }).ToList();
            foreach (ModelDetailOutputModel modelDetail in selModelDetail)
            {
                colSqlSB.Append($" {modelDetail.ColName} {modelDetail.ColType} ");
                if (modelDetail.ColName == (model.ModelCode + "ID"))
                {
                    colSqlSB.Append(" NOT NULL PRIMARY KEY ");
                }
                colSqlSB.Append(" , ");
                desSqlSB.Append($" execute sp_addextendedproperty 'MS_Description',  '{modelDetail.ColMemo}' ,'user', @CurrentUser, 'table', '{model.ModelCode}', 'column', '{modelDetail.ColName}'   ");
            }



            string sql = string.Format(firstTableSql, model.ModelCode) + colSqlSB.ToString() + string.Format((desSql + desSqlSB.ToString()), model.ModelCode, model.ModelName);

            base.Internal_DataHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 新增字段列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddDynamicTableCol(ModelDetailViewModel model)
        {
            string sql = $@"alter table {model.ModelCode} add {model.ColName} {model.ColType} null";
            int exeCount = base.DataHelper.ExecuteNonQuery(sql);
            return exeCount > 0;
        }

        /// <summary>
        /// 修改字段列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifyDynamicTableCol(ModelDetailViewModel model)
        {
            string sql = $@"alter table {model.ModelCode} alter column {model.ColName} {model.ColType} null";
            int exeCount = base.DataHelper.ExecuteNonQuery(sql);
            return exeCount > 0;
        }

        /// <summary>
        /// 判断字段列是否有数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ListDynamicTableListByPer(ModelDetailViewModel model)
        {
            string sql = $@"select count(1) from {model.ModelCode} where isnull({model.ColName},0)<>0  and isdelete =0";
            DataTable selDataTable = base.DataHelper.FillDataTable(sql);
            return (selDataTable?.Rows[0][0] + string.Empty).ToInt(0) > 0;
        }

        /// <summary>
        /// 删除数据字段列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DropDynamicTableCol(ModelDetailViewModel model)
        {
            string sql = $@"alter table {model.ModelCode} drop column {model.ColName}";
            int exeCount = base.DataHelper.ExecuteNonQuery(sql);
            return exeCount > 0;
        }
    }

}
