using Tomato.StandardLib.MyModel;
using Tomato.StandardLib.MyTool;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tomato.StandardLib.MyDataIO
{
    /// <summary>
    /// 从Txt导入数据
    /// </summary>
    public class ImportFromTxt : ImportBase
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

            tables.Add(Path.GetFileNameWithoutExtension(orderInfo.FileName));
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
            return this.LoadTxtData(orderInfo, descCol, 10, out dtSummer);
        }

        /// <summary>
        /// 从文件中读取全部,重写基类
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="descCol">导入字段描述</param>
        /// <param name="dtAllDate">获得的全部数据</param>
        /// <returns>读取结果</returns>
        protected override ResultInfo LoadFileAllData(DataImportOrderInfo orderInfo, DataImportDescCol descCol, out DataTable dtAllDate)
        {
            return this.LoadTxtData(orderInfo, descCol, 0, out dtAllDate);
        }

        /// <summary>
        /// 从Txt中读取指定行数
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="descCol">导入字段描述</param>
        /// <param name="LoadLine">要读取的行数，0为全部行</param>
        /// <param name="dtResult">获得的指定行数的数据</param>
        /// <returns>读取结果</returns>
        private ResultInfo LoadTxtData(DataImportOrderInfo orderInfo,DataImportDescCol descCol, int LoadLine, out DataTable dtResult)
        {
            dtResult = new DataTable();

            ResultInfo rst = base.checkFileType(orderInfo);
            if (!rst.Successed)
                return rst;

            // 若txt文件不按间隔符分割，则对字段开始位置排序读取
            List<DataImportDescInfo> listDataCol = new List<DataImportDescInfo>();
            if (string.IsNullOrEmpty(orderInfo.TxtSplitStr))
            {
                listDataCol = descCol.OrderBy(d => d.TxtFieldStartIndex).ToList();
            }

            using (FileStream fs = new FileStream(orderInfo.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
                string strLine;
                // 在CSV文件中寻找标题行，并匹配字段匹配集合
                int readLineIndex = 0;
                while ((strLine = sr.ReadLine()) != null)
                {
                    if (LoadLine > 0)
                    {
                        if (readLineIndex == LoadLine)
                            break;
                    }

                    List<string> aryLine;
                    if (!string.IsNullOrEmpty(orderInfo.TxtSplitStr))
                        aryLine = this.AnalysisTxtLine(strLine, orderInfo.TxtSplitStr);
                    else
                        aryLine = this.AnalysisTxtLine(strLine, listDataCol);

                    // 判断dtResult列数是否一致
                    if (dtResult.Columns.Count < aryLine.Count)
                    {
                        int tag = dtResult.Columns.Count;
                        for (int i = 0; i < aryLine.Count - tag; i++)
                        {
                            DataColumn dc = new DataColumn();
                            dc.DataType = typeof(string);
                            dc.DefaultValue = string.Empty;
                            dc.ColumnName = string.Format("Col{0}", tag + i + 1);
                            dtResult.Columns.Add(dc);
                        }
                    }

                    DataRow dr = dtResult.NewRow();
                    dr.ItemArray = aryLine.ToArray();
                    dtResult.Rows.Add(dr);
                    readLineIndex++;
                }
            }

            return ResultInfo.SuccessResultInfo();
        }

        /// <summary>
        /// 根据分隔符，将行数据转化为字段对应的字符串集合
        /// </summary>
        /// <param name="src">txt行数据</param>
        /// <param name="split">字段分割符</param>
        /// <returns>字段对应的字符串集合</returns>
        private List<string> AnalysisTxtLine(string src, string split)
        {
            src = Regex.Replace(src, split, "㊣");
            StringInfo s = new StringInfo(src);
            src = s.ToStringFiltered();
            List<string> sl = new List<string>();
            if (!string.IsNullOrEmpty(src))
            {
                string[] ie = Regex.Split(src, "㊣", RegexOptions.IgnoreCase);
                sl = new List<string>(ie);
            }

            return sl;
        }

        /// <summary>
        /// 根据字段描述信息，将行数据转化为字段对应的字符串集合
        /// 根据字段描述的读入起始位置及读取字符数来读取
        /// </summary>
        /// <param name="src">txt行数据</param>
        /// <param name="descCol">字段分割符</param>
        /// <returns>字段对应的字符串集合</returns>
        private List<string> AnalysisTxtLine(string src, List<DataImportDescInfo> descCol)
        {
            StringInfo s = new StringInfo(src);
            List<string> sl = new List<string>();
            if (!string.IsNullOrEmpty(src))
            {
                foreach (DataImportDescInfo i in descCol)
                {
                   string t = s.SubString_Byte(i.TxtFieldStartIndex, i.TxtFieldReadLength).ToStringFiltered();
                   sl.Add(t);
                }
            }

            return sl;
        }
    }
}
