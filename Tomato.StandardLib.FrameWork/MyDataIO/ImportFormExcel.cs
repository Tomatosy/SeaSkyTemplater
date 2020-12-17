using Tomato.StandardLib.MyModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace Tomato.StandardLib.MyDataIO
{
    /// <summary>
    /// Excel导入
    /// </summary>
    public class ImportFromExcel : ImportBase
    {
        /// <summary>
        /// 获取可选导入表集合，Excel为Sheet，Txt为文件名
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="tables">可选导入表集合</param>
        /// <returns>获取结果</returns>
        public override ResultInfo GetTables(DataImportOrderInfo orderInfo,out List<string> tables)
        {
            tables = new List<string>();
            ResultInfo rst = base.checkFileType(orderInfo);
            if (!rst.Successed)
                return rst;

            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(this.CreateExcelConnStr(orderInfo)))
            {
                con.Open();
                dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables_Info, null);
                con.Close();
            }

            foreach (DataRow dr in dt.Rows)
            {
                var tableName =dr["TABLE_NAME"].ToString();

                //sheet名为数字的时候，获取的TABLE_NAME两边会有单引号，先去单引号--update xyp 2015年7月14日14:34:24
                if (tableName.IndexOf('\'') == 0)
                {
                    tableName = tableName.Trim('\'');
                }

                if (dr["TABLE_NAME"].ToString().Contains("$"))
                {
                    //去除最后一个$符号之后的内容，TABLE_NAME解析出来最后必已$结尾
                    tableName = tableName.Substring(0, tableName.LastIndexOf('$'));
                    tables.Add(tableName);
                }
            }

            return ResultInfo.SuccessResultInfo();
        }

        /// <summary>
        /// 从文件中读取数据摘要，10行数据,重写基类
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="descCol">导入字段描述</param>
        /// <param name="dtSummer">获得的摘要数据</param>
        /// <returns>读取结果</returns>
        protected override ResultInfo LoadFileSummerData(DataImportOrderInfo orderInfo, DataImportDescCol descCol, out DataTable dtSummer)
        {
            return LoadExcelData(orderInfo, 10, out dtSummer);
        }

        /// <summary>
        /// 从文件中读取全部,重写基类
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="descCol">导入字段描述</param>
        /// <param name="dtAllData">获得的全部数据</param>
        /// <returns>读取结果</returns>
        protected override ResultInfo LoadFileAllData(DataImportOrderInfo orderInfo, DataImportDescCol descCol, out DataTable dtAllData)
        {
            return LoadExcelData(orderInfo, 0, out dtAllData);
        }
        
        /// <summary>
        /// 从Excel中读取指定行数
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="LoadLine">要读取的行数，0为全部行</param>
        /// <param name="dtResult">获得的指定行数的数据</param>
        /// <returns>读取结果</returns>
        private ResultInfo LoadExcelData(DataImportOrderInfo orderInfo, int LoadLine, out DataTable dtResult)
        {
            dtResult = new DataTable();
            ResultInfo rst = base.checkFileType(orderInfo);
            if (!rst.Successed)
                return rst;

            string sql = string.Format("select * from [{0}$]", orderInfo.TableName);

            if (LoadLine > 0)
                sql = string.Format("select top {1} * from [{0}$]", orderInfo.TableName, LoadLine - 1);

            using (OleDbConnection con = new OleDbConnection(this.CreateExcelConnStr(orderInfo)))
            {
                OleDbCommand ocm=new OleDbCommand(sql,con);
                OleDbDataReader reader = null;
                con.Open();
                try
                {
                    reader = ocm.ExecuteReader();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        DataColumn dc = new DataColumn();
                        dc.DataType = typeof(string);
                        dc.DefaultValue = string.Empty;
                        dc.ColumnName = reader.GetName(i);
                        dtResult.Columns.Add(dc);
                    }

                    while (reader.Read())
                    {
                        DataRow dr = dtResult.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dr[i] = reader[i].ToString();
                        }
                        dtResult.Rows.Add(dr);
                    }

                    DataRow rows;
                    rows = dtResult.NewRow();
                    for (int i = 0; i < dtResult.Columns.Count; i++)
                    {
                        rows[i] = (dtResult.Columns[i].ColumnName.ToString());
                    }
                    dtResult.Rows.InsertAt(rows, 0);
                }
                catch (Exception ex)
                {
                    return ResultInfo.UnSuccessResultInfo(ex.Message);
                }
                finally
                {
                    if (!reader.IsClosed)
                        reader.Close();
                    
                    con.Close();
                }
            }
            return ResultInfo.SuccessResultInfo();
        }

        /// <summary>
        /// 根据导入命令描述构造导入操作类
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        private string CreateExcelConnStr(DataImportOrderInfo orderInfo)
        {
            string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + orderInfo.FileName + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            return conString;
        }

    }
}
