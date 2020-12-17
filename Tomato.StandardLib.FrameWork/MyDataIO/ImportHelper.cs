using Tomato.StandardLib.MyModel;
using System;
using System.Collections.Generic;
using System.Data;

namespace Tomato.StandardLib.MyDataIO
{
    /// <summary>
    /// 数据导入接口
    /// </summary>
    public interface IImportBase
    {
        /// <summary>
        /// 获取可选导入表集合，Excel为Sheet，Txt为文件名
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="tables">可选导入表集合</param>
        /// <returns>获取结果</returns>
        ResultInfo GetTables(DataImportOrderInfo orderInfo,out List<string> tables);

        /// <summary>
        /// 10行数据内分析导入文件的字段信息与设置的字段描述匹配程度
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="descCol">设置的字段导入描述</param>
        /// <param name="noMarchCol">未匹配到的字段信息</param>
        /// <returns>匹配结果</returns>
        ResultInfo AnalysisField(ref DataImportOrderInfo orderInfo, ref DataImportDescCol descCol, out Dictionary<string, int> noMarchCol);

        /// <summary>
        /// 将数据导入到DataTable
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="descCol">设置的字段导入描述</param>
        /// <param name="dtResult">获取到数据的DataTable</param>
        /// <returns>导入结果</returns>
        ResultInfo LoadDataToDataTable(DataImportOrderInfo orderInfo, DataImportDescCol descCol,out DataTable dtResult);
    }

    /// <summary>
    /// 数据导入基础类
    /// </summary>
    public abstract class ImportBase : IImportBase
    {
        /// <summary>
        /// 获取可选导入表集合，Excel为Sheet，Txt为文件名
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="tables">可选导入表集合</param>
        /// <returns>获取结果</returns>
        public abstract ResultInfo GetTables(DataImportOrderInfo orderInfo,out List<string> tables);

        /// <summary>
        /// 从文件中读取数据摘要，10行数据
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="descCol">导入字段描述</param>
        /// <param name="dtSummer">获得的摘要数据</param>
        /// <returns>读取结果</returns>
        protected abstract ResultInfo LoadFileSummerData(DataImportOrderInfo orderInfo,DataImportDescCol descCol, out DataTable dtSummer);

        /// <summary>
        /// 从文件中读取全部
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="descCol">导入字段描述</param>
        /// <param name="dtAllDate">获得的全部数据</param>
        /// <returns>读取结果</returns>
        protected abstract ResultInfo LoadFileAllData(DataImportOrderInfo orderInfo,DataImportDescCol descCol, out DataTable dtAllDate);

        /// <summary>
        /// 10行数据内分析导入文件的字段信息与设置的字段描述匹配程度
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="descCol">设置的字段导入描述</param>
        /// <param name="noMarchCol">未匹配到的字段信息</param>
        /// <returns>匹配结果</returns>
        public ResultInfo AnalysisField(ref DataImportOrderInfo orderInfo, ref DataImportDescCol descCol, out Dictionary<string, int> noMarchCol)
        {
            ResultInfo res = new ResultInfo();
            noMarchCol = new Dictionary<string, int>();

            // 获取文件数据摘要
            DataTable dtSummer;
            res = this.LoadFileSummerData(orderInfo, descCol,out dtSummer);
            if (!res.Successed)
                return res;

            if (orderInfo.HasTitle)
            {
                // 进入有标题行字段匹配过程
                res = this.AnalysisField_hasTitle(dtSummer, ref orderInfo, ref descCol, out noMarchCol);
            }
            else
            {
                // 进入无标题行字段匹配过程
                res = this.AnalysisField_noTitle(dtSummer, ref orderInfo, ref descCol, out noMarchCol);
            }

            return res;
        }

        /// <summary>
        /// 分析所需字段与导入文件内字段的匹配关系，有标题行
        /// </summary>
        /// <param name="dtSummer">10行摘要数据</param>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="descCol">导入字段描述</param>
        /// <param name="noMarchCol">未匹配到的字段</param>
        /// <returns>匹配结果</returns>
        private ResultInfo AnalysisField_hasTitle(DataTable dtSummer, ref DataImportOrderInfo orderInfo, ref DataImportDescCol descCol, out Dictionary<string, int> noMarchCol)
        {
            //未匹配字段集合
            noMarchCol = new Dictionary<string, int>();

            // 初始化字段匹配集合，清空导入字段的匹配信息，加载到待匹配字段（方便匹配字段名时，可以直接通过key来查找）
            Dictionary<string, DataImportDescInfo> dicDescDiction = new Dictionary<string, DataImportDescInfo>();
            foreach (DataImportDescInfo descInfo in descCol)
            {
                descInfo.Fielddesc_External = string.Empty;
                descInfo.Fielddesc_External_Index = -1;
                dicDescDiction.Add(descInfo.Fielddesc_Internal_Desc, descInfo);
            }

            int titleRowIndex = -1;
            // 在摘要数据中定位标题行，只要有一个字段匹配到即为标题行
            for (int row = 0; row < dtSummer.Rows.Count; row++)
            {
                for (int col = 0; col < dtSummer.Columns.Count; col++)
                {
                    string drCell = dtSummer.Rows[row][col].ToString();
                    if (dicDescDiction.ContainsKey(drCell))
                    {
                        titleRowIndex = row;
                        break;
                    }
                }
                if (titleRowIndex >= 0) break;
            }

            // 若未发现标题行，则定位首行非空数据为标题行，只要有一个字段非空即为标题行
            if (titleRowIndex < 0)
            {
                for (int row = 0; row < dtSummer.Rows.Count; row++)
                {
                    for (int col = 0; col < dtSummer.Columns.Count; col++)
                    {
                        string drCell = dtSummer.Rows[row][col].ToString();
                        if (!string.IsNullOrEmpty(drCell))
                        {
                            titleRowIndex = row;
                            break;
                        }
                    }
                    if (titleRowIndex >= 0) break;
                }
            }
            // 若未发现非空行，则报错退出
            if(titleRowIndex < 0)
            {
                 return ResultInfo.UnSuccessResultInfo("10行内未找到标题行");
            }

            // 针对标题行，进行字段匹配
            DataRow drTitle = dtSummer.Rows[titleRowIndex];
            for (int i = 0; i < drTitle.Table.Columns.Count; i++)
            {
                string drCell = drTitle[i].ToString();
                if (dicDescDiction.ContainsKey(drCell))
                {
                    dicDescDiction[drCell].Fielddesc_External = string.Format("{0} 第{1}列",drCell,i + 1);
                    dicDescDiction[drCell].Fielddesc_External_Index = i;
                }
                else
                {
                    noMarchCol.Add(string.Format("{0} 第{1}列", drCell, i + 1), i);
                }
            }

            // 设置有效数据启始行号
            orderInfo.StartReadIndex = titleRowIndex + 1;
            return ResultInfo.SuccessResultInfo();
        }

        /// <summary>
        /// 分析所需字段与导入文件内字段的匹配关系，无标题行
        /// </summary>
        /// <param name="dtSummer">10行摘要数据</param>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="descCol">导入字段描述</param>
        /// <param name="noMarchCol">未匹配到的字段</param>
        /// <returns>匹配结果</returns>
        private ResultInfo AnalysisField_noTitle(DataTable dtSummer, ref DataImportOrderInfo orderInfo, ref DataImportDescCol descCol, out Dictionary<string, int> noMarchCol)
        {
            // 未匹配字段集合
            noMarchCol = new Dictionary<string, int>();

            // 初始化字段匹配集合，清空导入字段的匹配信息
            foreach (DataImportDescInfo descInfo in descCol)
            {
                descInfo.Fielddesc_External = string.Empty;
                descInfo.Fielddesc_External_Index = -1;
            }

            int titleRowIndex = -1;
            // 若指定了读入行，则该行为标题行
            if (orderInfo.StartReadIndex > 0)
            {
                titleRowIndex = orderInfo.StartReadIndex;
            }

            // 若未发现标题行，则定位首行非空数据为标题行，只要有一个字段非空即为标题行
            if (titleRowIndex < 0)
            {
                for (int row = 0; row < dtSummer.Rows.Count; row++)
                {
                    for (int col = 0; col < dtSummer.Columns.Count; col++)
                    {
                        string drCell = dtSummer.Rows[row][col].ToString();
                        if (!string.IsNullOrEmpty(drCell))
                        {
                            titleRowIndex = row;
                            break;
                        }
                    }

                    if (titleRowIndex >= 0) break;
                }
            }

            // 若未发现非空行，则报错退出
            if(titleRowIndex < 0)
            {
                 return ResultInfo.UnSuccessResultInfo("10行内未找到标题行");
            }

            // 针对标题行，进行字段匹配
            DataRow drTitle = dtSummer.Rows[titleRowIndex];
            for (int i = 0; i < drTitle.Table.Columns.Count; i++)
            {
                string drCell = drTitle[i].ToString();
                if (i < descCol.Count)
                {
                    descCol[i].Fielddesc_External = string.Format("第{0}列 首行样例：{1}", i + 1, drCell);
                    descCol[i].Fielddesc_External_Index = i;
                }
                else
                {
                    noMarchCol.Add(string.Format("第{0}列 首行样例：{1}", i + 1, drCell), i);
                }
            }

            // 设置有效数据启始行号
            orderInfo.StartReadIndex = titleRowIndex;

            return ResultInfo.SuccessResultInfo();
        }

        /// <summary>
        /// 将数据导入到DataTable
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <param name="descCol">设置的字段导入描述</param>
        /// <param name="dtResult">获取到数据的DataTable</param>
        /// <returns>导入结果</returns>
        public ResultInfo LoadDataToDataTable(DataImportOrderInfo orderInfo, DataImportDescCol descCol, out DataTable dtResult)
        {
            ResultInfo rst = new ResultInfo();
            // 根据导入字段，构建导入DataTable框架
            dtResult = descCol.CreateDataTableByDescInfo();

            DataTable dtFile;
            rst = this.LoadFileAllData(orderInfo,descCol, out dtFile);
            if (!rst.Successed)
                return rst;

            // 加载导入字段到待导入字典，同时计算已匹配到的最大列号
            Dictionary<int, DataImportDescInfo> dicDescDiction = new Dictionary<int, DataImportDescInfo>();
            foreach (DataImportDescInfo descInfo in descCol)
            {
                if (descInfo.Fielddesc_External_Index >= 0)
                    dicDescDiction.Add(descInfo.Fielddesc_External_Index, descInfo);
            }

            DataTableReader reader = dtFile.CreateDataReader();

            for (int row = 0; row < dtFile.Rows.Count; row++)
            {
                // 跳过有效数据行之前的数据
                if (row < orderInfo.StartReadIndex)
                    continue;

                DataRow drCur = dtFile.Rows[row];
                //若读入空行，则跳过读取
                string s = String.Join("", drCur.ItemArray);
                if (string.IsNullOrEmpty(s))
                    continue;

                string error = string.Empty;
                DataRow dr = dtResult.NewRow();
                bool isIgnore = false;
                
                for (int i = 0; i < drCur.Table.Columns.Count; i++)
                {
                    if (dicDescDiction.ContainsKey(i))
                    {
                        object value;
                        rst = dicDescDiction[i].GetImportValue(drCur[i].ToString(), out value, out isIgnore);
                        
                        //符合过滤策略则跳过
                        if (isIgnore)
                        {
                            break;
                        }

                        dr[dicDescDiction[i].Fielddesc_Internal] = value;

                        if (!rst.Successed)
                        {
                            error += string.Format("[{0}]", rst.FailReasonDesc);
                        }
                    }
                }

                if (!isIgnore)
                {
                    dr["Error"] = error;
                    dtResult.Rows.Add(dr);
                }
            }

            // 处理尾行数据
            if (orderInfo.EndReadIndex > 0)
            {
                for (int i = 0; i < orderInfo.EndReadIndex; i++)
                {
                    dtResult.Rows.RemoveAt(dtResult.Rows.Count - 1 - i);
                }
            }

            return rst;
        }

        /// <summary>
        /// 根据导入文件名，判断文件类型是否符合导入类型
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <returns>判断结果</returns>
        protected ResultInfo checkFileType(DataImportOrderInfo orderInfo)
        {
            if (orderInfo.CheckExtensionAvailable())
                return ResultInfo.SuccessResultInfo();
            else
                return ResultInfo.UnSuccessResultInfo("导入文件类型不匹配");
        }
    }

    /// <summary>
    /// 数据导入接口工厂类
    /// </summary>
    public class ImportHelper
    {
        /// <summary>
        /// 根据导入命令描述，创建数据导入操作类
        /// </summary>
        /// <param name="orderInfo">导入命令描述</param>
        /// <returns>数据导入操作类</returns>
        public static IImportBase CreateImportHelper(DataImportOrderInfo orderInfo)
        {
            IImportBase im = null;
            switch (orderInfo.FileType)
            {
                case EnumIOFileType.Excel:
                    im = new ImportFromExcel();
                    break;
                case EnumIOFileType.Txt:
                    im = new ImportFromTxt();
                    break;
            }

            return im;
        }
    }
}
