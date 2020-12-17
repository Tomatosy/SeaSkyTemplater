using Tomato.StandardLib.MyModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;

namespace Tomato.StandardLib.MyDataIO
{
    /// <summary>
    /// 将数据导出至Excel
    /// </summary>
    public class ExportToExcel : IExportBase
    {
        /// <summary>
        /// Excel文件名
        /// </summary>
        private string m_FileName;

        /// <summary>
        /// Excel文件Sheet名
        /// </summary>
        private string m_TableName;

        /// <summary>
        /// 根据导出命令描述构造Excel导出操作类
        /// </summary>
        /// <param name="orderInfo">导出命令描述</param>
        public ExportToExcel(DataExportOrderInfo orderInfo)
        {
            this.m_FileName = orderInfo.FileName;
            this.m_TableName = orderInfo.TableName;
        }

        #region IExportBase 成员

        /// <summary>
        /// 将数据导出到Excel文件
        /// </summary>
        /// <param name="descCol">导出列信息描述</param>
        /// <param name="dataSource">导出数据源</param>
        /// <returns>导出结果</returns>
        public ResultInfo Export(DataExportDescCol descCol, DataTable dataSource)
        {
            return this.Export(descCol, dataSource, Encoding.Default);
        }
        /// <summary>
        /// 将数据导出到Excel文件
        /// </summary>
        /// <param name="descCol">导出列信息描述</param>
        /// <param name="dataSource">导出数据源</param>
        /// <param name="Encoding">Encoding</param>
        /// <returns>导出结果</returns>
        public ResultInfo Export(DataExportDescCol descCol, DataTable dataSource, Encoding Encoding)
        {
            string tableName = this.m_TableName;

            // 'HDR=Yes;IMEX=1";
            string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + m_FileName + ";Extended Properties=\"Excel 8.0;HDR=YES\"";

            if (System.IO.File.Exists(this.m_FileName))
            {
                System.IO.File.Delete(this.m_FileName);
            }

            string create = "Create Table [" + tableName + "] (";

            string excelColumn = string.Empty;

            // string excelPara = string.Empty;
            foreach (DataExportDescInfo exp in descCol)
            {
                create += "[";
                create += exp.Fielddesc_External;
                create += "]";

                excelColumn += "[" + exp.Fielddesc_External + "],";

                // excelPara += "@" + exp.Fielddesc_External + ",";
                create += string.Format(" {0},", exp.ExcelValueType);
            }

            create = create.TrimEnd(',');
            excelColumn = excelColumn.TrimEnd(',');

            // excelPara = excelPara.TrimEnd(',');
            create += ")";

            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                conn.Open();
                using (OleDbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // create table
                        this.ExecuteNonQuery(trans, create);

                        // string insertSql = "Insert Into [" + tableName + "] (" + excelColumn + ") Values (" + excelPara + ")";
                        string insertSql = "Insert Into [" + tableName + "] (" + excelColumn + ") Values (";

                        List<OleDbParameter> paras = new List<OleDbParameter>();
                        foreach (DataRow dr in dataSource.Rows)
                        {
                            // paras.Clear();
                            string valuesql = string.Empty;
                            foreach (DataExportDescInfo expd in descCol)
                            {
                                object exportValue;
                                object value = string.Empty;
                                if (!expd.IsDefaultField)
                                {
                                    DataColumn dc = dataSource.Columns[expd.Fielddesc_Internal];
                                    if (dc == null)
                                    {
                                        continue;
                                    }

                                    value = dr[expd.Fielddesc_Internal];
                                }

                                expd.GetExportValue(value, out exportValue);
                                if (expd.FieldWrithType != EnumIODateType.文本 && expd.FieldWrithType != EnumIODateType.日期 && string.IsNullOrEmpty(exportValue.ToString()))
                                {
                                    valuesql += "null,";
                                }
                                else
                                {
                                    //duxuecheng  2015年10月13日 12:29:34 添加 .Replace("'", "''") 
                                    //原因：当字段中包含了单引号的时候将会报错
                                    valuesql += "'" + exportValue.ToString().Replace("'", "''") + "',";

                                }

                                // paras.Add(new OleDbParameter("@" + expd.Fielddesc_External, ex_value));
                            }

                            valuesql = valuesql.TrimEnd(',');

                            // ExecuteNonQuery(trans, insertSql, paras.ToArray());
                            this.ExecuteNonQuery(trans, insertSql + valuesql + ")");
                        }

                        trans.Commit();

                        return ResultInfo.SuccessResultInfo();
                    }
                    catch (Exception ex)
                    {
                        string s = ex.Message;
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 将数据插入Excel，返回影响行数
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdText">执行的SQL命令</param>
        /// <returns>影响行数</returns>
        private int ExecuteNonQuery(OleDbTransaction trans, string cmdText)
        {
            using (OleDbCommand cmd = new OleDbCommand())
            {
                cmd.Connection = trans.Connection;
                cmd.CommandText = cmdText;
                cmd.Transaction = trans;
                cmd.CommandType = CommandType.Text;
                return cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}
