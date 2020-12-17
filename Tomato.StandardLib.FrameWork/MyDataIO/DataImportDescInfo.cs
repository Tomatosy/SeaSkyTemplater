using Tomato.StandardLib.MyBaseClass;
using Tomato.StandardLib.MyModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Tomato.StandardLib.MyDataIO
{
    /// <summary>
    /// 导入数据过滤方式
    /// </summary>
    public enum EnumFilterWay
    {
        /// <summary>
        /// 无过滤
        /// </summary>
        无过滤 = 1,

        /// <summary>
        /// 只处理
        /// </summary>
        只处理 = 2,

        /// <summary>
        /// 不处理
        /// </summary>
        不处理 = 3
    }

    /// <summary>
    /// 导入或导出的文件类型
    /// </summary>
    public enum EnumIOFileType
    {
        /// <summary>
        /// Excel
        /// </summary>
        Excel = 1,

        /// <summary>
        /// Txt
        /// </summary>
        Txt = 2,
    }

    /// <summary>
    /// 导入或导出的字段类型
    /// </summary>
    public enum EnumIODateType
    {
        /// <summary>
        /// 文本
        /// </summary>
        文本 = 1,

        /// <summary>
        /// 整数
        /// </summary>
        整数 = 2,

        /// <summary>
        /// 小数
        /// </summary>
        小数 = 3,

        /// <summary>
        /// 日期
        /// </summary>
        日期 = 4,

        /// <summary>
        /// 金额
        /// </summary>
        金额 = 5
    }

    ///// <summary>
    ///// 导入对象的数据库操作类
    ///// </summary>
    //public class DBHelper : DALBase<DataImportDescInfo> 
    //{
    //}

    /// <summary>
    /// 导入字段描述
    /// </summary>
    [Serializable]
    public class DataImportDescInfo
    {
        /// <summary>
        /// 导入分类标记
        /// </summary>
        private string m_ImportKey;

        /// <summary>
        /// 导入内部字段名
        /// </summary>
        private string m_fielddesc_Internal;

        /// <summary>
        /// 导入内部字段名描述
        /// </summary>
        private string m_fielddesc_Internal_Desc;

        /// <summary>
        /// 导入字段类型
        /// </summary>
        private EnumIODateType m_fieldreadType = EnumIODateType.文本;

        /// <summary>
        /// 导入字段长度
        /// </summary>
        private int m_FieldLength = 200;

        /// <summary>
        /// 导入字段是否必须字段
        /// </summary>
        private bool m_isNecessary = false;

        /// <summary>
        /// 导入数据源字段名
        /// </summary>
        private string m_fielddesc_External;

        /// <summary>
        /// 导入数据源字段序号
        /// </summary>
        private int m_fielddesc_External_Index = -1;

        /// <summary>
        /// 导入字段描述
        /// </summary>
        private string m_fielddesc_Remark;

        /// <summary>
        /// 导入字段分组
        /// </summary>
        private int m_bindingGroupIndex = -1;

        /// <summary>
        /// 导入txt行起始位置
        /// </summary>
        private int m_txtFieldStartIndex = 0;

        /// <summary>
        /// 导入txt行读取字符数
        /// </summary>
        private int m_txtFieldReadLength = 0;

        /// <summary>
        /// 导入过滤字符
        /// </summary>
        private string m_FilterValue = string.Empty;

        /// <summary>
        /// 导入过滤处理方式
        /// </summary>
        private EnumFilterWay m_FilterWay = EnumFilterWay.不处理;

        /// <summary>
        /// 获取或设置导入分类标记
        /// </summary>
        public string ImportKey
        {
            get { return this.m_ImportKey; }
            set { this.m_ImportKey = value; }
        }

        /// <summary>
        /// 获取或设置导入内部字段名，DataTable或数据库列名
        /// 或为空则为导入内部字段描述
        /// </summary>
        public string Fielddesc_Internal
        {
            get 
            {
                if (string.IsNullOrEmpty(this.m_fielddesc_Internal))
                {
                    return this.m_fielddesc_Internal_Desc;
                }

                return this.m_fielddesc_Internal;
            }

            set 
            {
                this.m_fielddesc_Internal = value; 
            }
        }

        /// <summary>
        /// 获取或设置导入内部字段名描述，用于显示
        /// </summary>
        public string Fielddesc_Internal_Desc
        {
            get { return this.m_fielddesc_Internal_Desc; }
            set { this.m_fielddesc_Internal_Desc = value; }
        }

        /// <summary>
        /// 获取或设置导入字段类型
        /// </summary>
        public EnumIODateType FieldreadType
        {
            get { return this.m_fieldreadType; }
            set { this.m_fieldreadType = value; }
        }

        /// <summary>
        /// 获取或设置导入字段长度
        /// 若字段类型为小数或金额，长度默认为18
        /// </summary>
        public int FieldLength
        {
            get { return this.m_FieldLength; }
            set { this.m_FieldLength = value; }
        }

        /// <summary>
        /// 获取或设置是否必须字段
        /// </summary>
        public bool IsNecessary
        {
            get { return this.m_isNecessary; }
            set { this.m_isNecessary = value; }
        }

        /// <summary>
        /// 获取或设置导入数据源字段名
        /// </summary>
        public string Fielddesc_External
        {
            get { return this.m_fielddesc_External; }
            set { this.m_fielddesc_External = value; }
        }

        /// <summary>
        /// 获取或设置导入数据源字段序号
        /// </summary>
        public int Fielddesc_External_Index
        {
            get { return this.m_fielddesc_External_Index; }
            set { this.m_fielddesc_External_Index = value; }
        }

        /// <summary>
        /// 获取或设置导入字段描述
        /// </summary>
        public string Fielddesc_Remark
        {
            get { return this.m_fielddesc_Remark; }
            set { this.m_fielddesc_Remark = value; }
        }

        /// <summary>
        /// 获取或设置导入字段分组，同一组内必须要么同时导入，要么同时不导入
        /// 分组值未-1时，为不分组
        /// </summary>
        public int BindingGroupIndex
        {
            get { return this.m_bindingGroupIndex; }
            set { this.m_bindingGroupIndex = value; }
        }

        /// <summary>
        /// 获取或设置导入txt行起始位置，只对导入类型为txt有效
        /// </summary>
        public int TxtFieldStartIndex
        {
            get { return this.m_txtFieldStartIndex; }
            set { this.m_txtFieldStartIndex = value; }
        }

        /// <summary>
        /// 获取或设置导入txt行读取字符数，只对导入类型为txt有效
        /// </summary>
        public int TxtFieldReadLength
        {
            get { return this.m_txtFieldReadLength; }
            set { this.m_txtFieldReadLength = value; }
        }

        /// <summary>
        /// 获取或设置导入过滤字符
        /// </summary>
        public string FilterValue
        {
            get { return this.m_FilterValue; }
            set { this.m_FilterValue = value; }
        }

        /// <summary>
        /// 获取或设置导入过滤处理方式，默认为无过滤
        /// </summary>
        public EnumFilterWay FilterWay
        {
            get { return this.m_FilterWay; }
            set { this.m_FilterWay = value; }
        }

        /// <summary>
        /// 将导入字段值转化为符合设置要求的值
        /// </summary>
        /// <param name="value">导入字段值</param>
        /// <param name="convertValue">转化后的值</param>
        /// <param name="isIgnore">是否被过滤</param>
        /// <returns>转化结果</returns>
        public ResultInfo GetImportValue(string value, out object convertValue, out bool isIgnore)
        {
            // 若字段值过长则自动截断
            convertValue = value.Substring(0, value.Length < this.m_FieldLength ? value.Length : this.m_FieldLength);

            // 判断值是否匹配过滤策略
            isIgnore = false;
            if (this.m_FilterWay != EnumFilterWay.无过滤)
            {
                if (this.m_FilterValue == null)
                    this.m_FilterValue = string.Empty;

                string[] filterSplit = this.m_FilterValue.Split('&');
                List<string> listFilter = new List<string>(filterSplit);

                if (this.m_FilterWay == EnumFilterWay.只处理)
                {
                    string s = convertValue.ToString();
                    isIgnore = string.IsNullOrEmpty(listFilter.Find(delegate(string m) { return m == s; }));
                }

                if (this.m_FilterWay == EnumFilterWay.不处理)
                {
                    string s = convertValue.ToString();
                    isIgnore = !string.IsNullOrEmpty(listFilter.Find(delegate(string m) { return m == s; }));
                }
            }

            if (value.Length > this.m_FieldLength)
                return ResultInfo.UnSuccessResultInfo("字段太长系统自动截断");
            else
                return ResultInfo.SuccessResultInfo();
        }
    }

    /// <summary>
    /// 导入字段描述集合
    /// </summary>
    public class DataImportDescCol : CollectionBase<DataImportDescInfo>
    {
        /// <summary>
        /// 生成准备存放数据的DataTable结构，导入数据到DataTable使用
        /// </summary>
        /// <returns>创建的只有结构但没有数据的DataTable</returns>
        public DataTable CreateDataTableByDescInfo()
        {
            using (DataTable dt = new DataTable())
            {
                foreach (DataImportDescInfo descInfo in this)
                {
                    if (descInfo.Fielddesc_External_Index < 0)
                    {
                        continue;
                    }

                    using (DataColumn dc = new DataColumn(descInfo.Fielddesc_Internal))
                    {
                        dc.DataType = typeof(string);
                        dt.Columns.Add(dc);
                    }
                }
                DataColumn dcError = new DataColumn("Error");
                dcError.DataType = typeof(string);
                dt.Columns.Add(dcError);

                return dt;
            }
        }

        /// <summary>
        /// 获取已经匹配到的需要导入字段的内部列名
        /// </summary>
        /// <returns>已经匹配到的需要导入字段的内部列名</returns>
        public List<string> GetMatchedInternalCols()
        {
            List<string> Cols = new List<string>();

            foreach (DataImportDescInfo descInfo in this)
            {
                if (descInfo.Fielddesc_External_Index < 0)
                {
                    continue;
                }

                Cols.Add(descInfo.Fielddesc_Internal);
            }

            return Cols;
        }

        /// <summary>
        /// 获取已经匹配到的需要导入字段的列名与描述关系
        /// </summary>
        /// <returns>已经匹配到的需要导入字段的列名与描述关系,内部列名为key，描述为value</returns>
        public Dictionary<string,string> GetMatchedInternalRelation()
        {
            Dictionary<string, string> Cols = new Dictionary<string, string>();

            foreach (DataImportDescInfo descInfo in this)
            {
                if (descInfo.Fielddesc_External_Index < 0)
                {
                    continue;
                }

                Cols.Add(descInfo.Fielddesc_Internal,descInfo.Fielddesc_Internal_Desc);
            }

            return Cols;
        }
    }

    /// <summary>
    /// 导入命令描述
    /// </summary>
    public class DataImportOrderInfo
    {
        /// <summary>
        /// 导入分类标记
        /// </summary>
        private string m_ImportKey;

        /// <summary>
        /// 导入文件类型
        /// </summary>
        private EnumIOFileType m_FileType;

        /// <summary>
        /// 导入文件名
        /// </summary>
        private string m_FileName;

        /// <summary>
        /// 导入数据表名
        /// </summary>
        private string m_TableName;

        /// <summary>
        /// 导入目标是否含有标题
        /// </summary>
        private bool m_hasTitle;

        /// <summary>
        /// 导入文件开始行位置
        /// </summary>
        private int m_StartReadIndex;

        /// <summary>
        /// 导入文件结束行位置
        /// </summary>
        private int m_EndReadIndex;

        /// <summary>
        /// 导入txt文件分割符
        /// </summary>
        private string m_txtSplitStr;

        /// <summary>
        /// 获取或设置导入分类标记
        /// </summary>
        public string ImportKey
        {
            get { return this.m_ImportKey; }
            set { this.m_ImportKey = value; }
        }

        /// <summary>
        /// 获取或设置导入文件类型
        /// </summary>
        public EnumIOFileType FileType
        {
            get { return this.m_FileType; }
            set { this.m_FileType = value; }
        }

        /// <summary>
        /// 获取或设置导入文件名
        /// </summary>
        public string FileName
        {
            get { return this.m_FileName; }
            set { this.m_FileName = value; }
        }

        /// <summary>
        /// 获取或设置导入数据表名，若导入类型为txt则无需设置
        /// </summary>
        public string TableName
        {
            get { return this.m_TableName; }
            set { this.m_TableName = value; }
        }

        /// <summary>
        /// 获取或设置导入目标是否含有标题
        /// </summary>
        public bool HasTitle
        {
            get { return this.m_hasTitle; }
            set { this.m_hasTitle = value; }
        }

        /// <summary>
        /// 获取或设置导入文件开始行位置
        /// </summary>
        public int StartReadIndex
        {
            get { return this.m_StartReadIndex; }
            set { this.m_StartReadIndex = value; }
        }

        /// <summary>
        /// 获取或设置导入文件结束行位置
        /// 此值为倒数，如设置为2，则为倒数第二行，倒数第二行和第一行将不被导入
        /// </summary>
        public int EndReadIndex
        {
            get { return this.m_EndReadIndex; }
            set { this.m_EndReadIndex = value; }
        }

        /// <summary>
        /// 获取或设置导入txt文件分割符，只对导入类型为txt有效
        /// </summary>
        public string TxtSplitStr
        {
            get { return this.m_txtSplitStr; }
            set { this.m_txtSplitStr = value; }
        }

        /// <summary>
        /// 校验导入文件是否符合设置要求
        /// </summary>
        /// <returns>校验结果</returns>
        public bool CheckExtensionAvailable()
        {
            bool Available = true;
            string filetype = Path.GetExtension(this.FileName);
            if (this.m_FileType == EnumIOFileType.Excel)
            {
                if (filetype.ToLower() != ".xls" && filetype.ToLower() != ".xlsx")
                {
                    Available = false;
                }
            }

            if (this.m_FileType == EnumIOFileType.Txt)
            {
                if (filetype != ".txt")
                {
                    Available = false;
                }
            }

            return Available;
        } 
    }
}
