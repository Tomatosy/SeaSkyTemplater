using Tomato.StandardLib.MyModel;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Tomato.StandardLib.MyDataIO
{
    /// <summary>
    /// 将数据导出至Excel
    /// </summary>
    public class ExportToTxt : IExportBase
    {
        /// <summary>
        /// Txt文件名
        /// </summary>
        private string m_FileName;

        /// <summary>
        /// 起始行数据
        /// </summary>
        private List<string> m_SpecialLineBegin;

        /// <summary>
        /// 结尾行数据
        /// </summary>
        private List<string> m_SpecialLineEnd;

        /// <summary>
        /// 字段分割符
        /// </summary>
        private string m_SplitStr;

        /// <summary>
        /// 是否导出标题
        /// </summary>
        private bool m_ExportTitle;

        /// <summary>
        /// 默认空白填充字符
        /// </summary>
        private char m_paddingChar;

        /// <summary>
        /// 根据导出命令描述构造Txt导出操作类
        /// </summary>
        /// <param name="orderInfo">导出命令描述</param>
        public ExportToTxt(DataExportOrderInfo orderInfo)
        {
            this.m_FileName = orderInfo.FileName;
            this.m_SpecialLineBegin = orderInfo.TxtSpecialLineBegin;
            this.m_SpecialLineEnd = orderInfo.TxtSpecialLineEnd;
            this.m_SplitStr = orderInfo.TxtSplitStr;
            this.m_ExportTitle = orderInfo.TxtExportTitle;
            this.m_paddingChar = orderInfo.TxtPaddingChar;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="descCol">导出列信息描述</param>
        /// <param name="dataSource">导出数据源</param>
        /// <returns>导出结果</returns>
        public ResultInfo Export(DataExportDescCol descCol, DataTable dataSource)
        {
            return this.Export(descCol, dataSource, Encoding.Default);
        }
        /// <summary>
        /// 将数据导出到Txt文件
        /// </summary>
        /// <param name="descCol">导出列信息描述</param>
        /// <param name="dataSource">导出数据源</param>
        /// <param name="Encoding">Encoding</param>
        /// <returns>导出结果</returns>
        public ResultInfo Export(DataExportDescCol descCol, DataTable dataSource,Encoding Encoding)
        {

            using (TextWriter writer = new StreamWriter(this.m_FileName, false, Encoding))
            {
                foreach (string s_beg in this.m_SpecialLineBegin)
                {
                    if (!string.IsNullOrEmpty(s_beg))
                    {
                        writer.WriteLine(s_beg);
                    }
                }

                if (this.m_ExportTitle)
                {
                    string s_title = string.Empty;
                    foreach (DataExportDescInfo expd in descCol)
                    {
                        string value = expd.Fielddesc_External;
                        if (expd.TxtFieldLength > 0 && !string.IsNullOrEmpty(this.m_paddingChar.ToString()))
                            value = expd.GetExportValueTxtFormat(value, this.m_paddingChar);

                        s_title += value;
                        s_title += this.m_SplitStr;
                    }

                    writer.WriteLine(s_title);
                }

                foreach (DataRow dr in dataSource.Rows)
                {
                    string s_data = string.Empty;
                    for (int i = 0; i < descCol.Count; i++)
                    {
                        DataExportDescInfo expd = descCol[i];
                        object exportvalue;
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

                        expd.GetExportValue(value, out exportvalue);

                        if (expd.TxtFieldLength > 0 && !string.IsNullOrEmpty(expd.TxtPaddingChar))
                            exportvalue = expd.GetExportValueTxtFormat(exportvalue, expd.TxtPaddingChar.ToCharArray()[0]);

                        s_data += exportvalue;
                        if (i != (descCol.Count - 1))
                            s_data += this.m_SplitStr;
                    }

                    writer.WriteLine(s_data);
                }

                foreach (string s_end in this.m_SpecialLineEnd)
                {
                    if (!string.IsNullOrEmpty(s_end))
                    {
                        writer.WriteLine(s_end);
                    }
                }
            }

            return ResultInfo.SuccessResultInfo();
           
        }
    }
}
