using Tomato.StandardLib.MyBaseClass;
using Tomato.StandardLib.MyModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tomato.StandardLib.MyDataIO
{
    /// <summary>
    /// 导出字段对齐方式
    /// </summary>
    public enum EnumExportFieldAlignment
    {
        /// <summary>
        /// 左对齐
        /// </summary>
        左对齐 = 0,

        /// <summary>
        /// 右对齐
        /// </summary>
        右对齐 = 1
    }

    /// <summary>
    /// 导出字段描述
    /// </summary>
    public class DataExportDescInfo
    {
        /// <summary>
        /// 导出分类标记
        /// </summary>
        private string m_ExmportKey;

        /// <summary>
        /// 导出目标列名
        /// </summary>
        private string m_fielddesc_External;

        /// <summary>
        /// 导出来源列名
        /// </summary>
        private string m_fielddesc_Internal;

        /// <summary>
        /// 是否默认值字段
        /// </summary>
        private bool m_isDefaultField = false;

        /// <summary>
        /// 导出默认值
        /// </summary>
        private string m_Defaultvalue = string.Empty;

        /// <summary>
        /// 导出数据类型
        /// </summary>
        private EnumIODateType m_fieldwrithType = EnumIODateType.文本;

        /// <summary>
        /// 导出txt字段长度
        /// </summary>
        private int m_txtFieldLength = 0;

        /// <summary>
        /// 导出txt字段填充字符
        /// </summary>
        private string m_txtPaddingChar = string.Empty;

        /// <summary>
        /// 导出字段格式
        /// </summary>
        private string m_ValueFormat;

        /// <summary>
        /// 导出字段对齐方式
        /// </summary>
        private EnumExportFieldAlignment m_txtFieldAlignment;

        /// <summary>
        /// 获取或设置导出目标列名
        /// </summary>
        public string ExmportKey
        {
            get { return this.m_ExmportKey; }
            set { this.m_ExmportKey = value; }
        }

        /// <summary>
        /// 获取或设置导出目标列名
        /// </summary>
        public string Fielddesc_External
        {
            get { return this.m_fielddesc_External; }
            set { this.m_fielddesc_External = value; }
        }

        /// <summary>
        /// 获取或设置导出来源列名
        /// </summary>
        public string Fielddesc_Internal
        {
            get { return this.m_fielddesc_Internal; }
            set { this.m_fielddesc_Internal = value; }
        }

        /// <summary>
        /// 获取或设置是否默认值列
        /// </summary>
        public bool IsDefaultField
        {
            get { return this.m_isDefaultField; }
            set { this.m_isDefaultField = value; }
        }

        /// <summary>
        /// 获取或设置导出默认值
        /// </summary>
        public string Defaultvalue
        {
            get { return this.m_Defaultvalue; }
            set { this.m_Defaultvalue = value; }
        }

        /// <summary>
        /// 获取或设置导出字段类型
        /// </summary>
        public EnumIODateType FieldWrithType
        {
            get { return this.m_fieldwrithType; }
            set { this.m_fieldwrithType = value; }
        }

        /// <summary>
        /// 获取或设置导出txt字段长度,导出类型为txt才有效
        /// </summary>
        public int TxtFieldLength
        {
            get { return this.m_txtFieldLength; }
            set { this.m_txtFieldLength = value; }
        }

        /// <summary>
        /// 获取或设置导出字段格式
        /// </summary>
        public string ValueFormat
        {
            get { return this.m_ValueFormat; }
            set { this.m_ValueFormat = value; }
        }

        /// <summary>
        /// 获取或设置导出txt字段填充符
        /// </summary>
        public string TxtPaddingChar
        {
            get { return this.m_txtPaddingChar; }
            set { this.m_txtPaddingChar = value; }
        }

        /// <summary>
        /// 获取或设置导出字段对齐方式,导出类型为txt才有效
        /// </summary>
        public EnumExportFieldAlignment TxtFieldAlignment
        {
            get { return this.m_txtFieldAlignment; }
            set { this.m_txtFieldAlignment = value; }
        }

        /// <summary>
        /// 获取导出字段类型对应Excel内部数据类型
        /// </summary>
        public string ExcelValueType
        {
            get
            {
                string type = string.Empty;
                switch (this.m_fieldwrithType)
                {
                    case EnumIODateType.文本:
                        type = "nvarchar(255)";
                        break;
                    case EnumIODateType.整数:
                        type = "Integer";
                        break;
                    case EnumIODateType.小数:
                        type = "Decimal";
                        break;
                    case EnumIODateType.金额://update by xyp 2015年7月16日15:32:08
                        type = "Money";
                        break;
                    case EnumIODateType.日期:
                        type = "Date";
                        break;
                }


                return type;
                //return "nvarchar(255)";
            }
        }

        /// <summary>
        /// 将导出字段值转化为符合设置要求的值,导出类型为Excel时使用
        /// </summary>
        /// <param name="value">数据源值</param>
        /// <param name="convertValue">转化后值</param>
        /// <returns>转化结果</returns>
        public ResultInfo GetExportValue(object value, out object convertValue)
        {
            if (this.IsDefaultField)
            {
                convertValue = this.Defaultvalue;
                return ResultInfo.SuccessResultInfo();
            }

            convertValue = value;
            if (string.IsNullOrEmpty(this.m_ValueFormat))
            {
                return ResultInfo.SuccessResultInfo();
            }

            if (this.m_fieldwrithType == EnumIODateType.文本 || this.m_fieldwrithType == EnumIODateType.整数)
            {
                return ResultInfo.SuccessResultInfo();
            }

            if (this.m_fieldwrithType == EnumIODateType.金额 || this.m_fieldwrithType == EnumIODateType.小数)
            {
                decimal tempDecmal;
                if (!decimal.TryParse(value.ToString(), out tempDecmal))
                {
                    return ResultInfo.SuccessResultInfo();
                }

                //convertValue = tempDecmal.ToString(this.m_ValueFormat);
                convertValue = string.Format(this.m_ValueFormat, tempDecmal);//update by xyp 2015年7月16日15:32:08
                return ResultInfo.SuccessResultInfo();
            }

            if (this.m_fieldwrithType == EnumIODateType.日期)
            {
                DateTime tempDate;
                if (!DateTime.TryParse(value.ToString(), out tempDate))
                {
                    return ResultInfo.SuccessResultInfo();
                }

                //convertValue = tempDate.ToString(this.m_ValueFormat);
                convertValue = string.Format(this.m_ValueFormat, tempDate);//update by xyp 2015年7月16日15:32:08
                return ResultInfo.SuccessResultInfo();
            }

            return ResultInfo.SuccessResultInfo();
        }

        /// <summary>
        /// 将导出字段值转化为符合设置要求的值,导出类型为txt时使用
        /// </summary>
        /// <param name="value">数据源值</param>
        /// <param name="paddingChar">默认空白填充字符</param>
        /// <returns>转化结果</returns>
        public string GetExportValueTxtFormat(object value, char paddingChar)
        {
            string valueStr = value.ToString();
          
            //填充方向
            bool padLeft = true;
            //截取方向，以右边为准
            bool subRight = true;
            if (this.m_txtFieldAlignment == EnumExportFieldAlignment.左对齐)
            {
                padLeft = false;
                subRight = false;
            }
            return this.GetSubString(valueStr, this.m_txtFieldLength, paddingChar, padLeft, subRight);


            ////需填充字符的长度
            //int paddingCount = this.TxtFieldLength - Encoding.Default.GetByteCount(valueStr);
            //// 不需填充
            //if (paddingCount == 0) 
            //{ 
            //    return valueStr;
            //}
            //else if (paddingCount > 0)
            //{
            //    // 需填充
            //    string paddingStr = new string(paddingChar, paddingCount);

            //    if (this.TxtFieldAlignment == EnumExportFieldAlignment.左对齐)
            //    {
            //        valueStr = valueStr + paddingStr;
            //    }
            //    else
            //    {
            //        valueStr = paddingStr + valueStr;
            //    }
            //}
            //else //截取
            //{

            //}
            //return valueStr;
        }

        /// <summary>
        /// 截取字符串，中文算2个长度，不足部分由指定字符串填充
        /// </summary>
        /// <param name="source"></param>
        /// <param name="len"></param>
        /// <param name="c"></param>
        /// <param name="padleft">是否左边填充</param>
        /// <param name="subRight">是否右侧截取</param>
        /// <returns></returns>
        private string GetSubString(string source, int len, char c, bool padleft, bool subRight)
        {
            if (source == null) source = string.Empty;

            int len1 = Encoding.Default.GetBytes(source).Length;
            if (len1 == len) return source;
            if (len1 < len) return padleft ? source.PadLeft(source.Length + len - len1, c) : source.PadRight(source.Length + len - len1, c);
            //截取
            string outstr = "";
            int n = 0;

            for (int i = 0; i < source.Length; i++)
            {
                int j = i;
                if (subRight) j = source.Length - i - 1;
                char ch = source[j];
                n += System.Text.Encoding.Default.GetByteCount(ch.ToString());
                if (n > len)
                    break;
                else
                    if (subRight)
                        outstr = ch + outstr;
                    else
                        outstr += ch;
            }
            if (Encoding.Default.GetBytes(outstr).Length < len) outstr = padleft ? outstr.PadLeft(outstr.Length + 1, c) : outstr.PadRight(outstr.Length + 1, c);
            return outstr;
        }

    }

    /// <summary>
    /// 导出字段描述集合
    /// </summary>
    public class DataExportDescCol : CollectionBase<DataExportDescInfo>
    {
    }

    /// <summary>
    /// 导出命令描述
    /// </summary>
    public class DataExportOrderInfo
    {
        /// <summary>
        /// 导出分类标记
        /// </summary>
        private string m_ExmportKey;

        /// <summary>
        /// 导出文件类型
        /// </summary>
        private EnumIOFileType m_FileType;

        /// <summary>
        /// 导出文件名
        /// </summary>
        private string m_FileName;

        /// <summary>
        /// 导出数据表名
        /// </summary>
        private string m_TableName;

        /// <summary>
        /// 导出txt首行内容
        /// </summary>
        private List<string> m_txtSpecialLineBegin = new List<string>();

        /// <summary>
        /// 导出txt尾行内容
        /// </summary>
        private List<string> m_txtSpecialLineEnd = new List<string>();

        /// <summary>
        /// 导出txt分割符
        /// </summary>
        private string m_txtSplitStr = "\t";

        /// <summary>
        /// 导出是否包含标题行
        /// </summary>
        private bool m_txtExportTitle = true;

        /// <summary>
        /// 导出txt默认填充字符
        /// </summary>
        private char m_txtPaddingChar = ' ';

        /// <summary>
        /// 获取或设置导出分类标记
        /// </summary>
        public string ExmportKey
        {
            get { return this.m_ExmportKey; }
            set { this.m_ExmportKey = value; }
        }

        /// <summary>
        /// 获取或设置导出文件类型
        /// </summary>
        public EnumIOFileType FileType
        {
            get { return this.m_FileType; }
            set { this.m_FileType = value; }
        }

        /// <summary>
        /// 获取导出文件类型的扩展名
        /// </summary>
        public string FileExtension
        {
            get
            {
                string Extension = string.Empty;
                if (this.m_FileType == EnumIOFileType.Excel)
                {
                    Extension = "xls";
                }

                if (this.m_FileType == EnumIOFileType.Txt)
                {
                    Extension = "txt";
                }

                return Extension;
            }
        }

        /// <summary>
        /// 获取或设置导出文件名
        /// </summary>
        public string FileName
        {
            get { return this.m_FileName; }
            set { this.m_FileName = value; }
        }

        /// <summary>
        /// 获取或设置导出数据表名，若为txt格式则无需设置
        /// </summary>
        public string TableName
        {
            get { return this.m_TableName; }
            set { this.m_TableName = value; }
        }

        /// <summary>
        /// 获取或设置导出txt首行内容，导出类型为txt才有效
        /// </summary>
        public List<string> TxtSpecialLineBegin
        {
            get { return this.m_txtSpecialLineBegin; }
            set { this.m_txtSpecialLineBegin = value; }
        }

        /// <summary>
        /// 获取或设置导出txt尾行内容，导出类型为txt才有效
        /// </summary>
        public List<string> TxtSpecialLineEnd
        {
            get { return this.m_txtSpecialLineEnd; }
            set { this.m_txtSpecialLineEnd = value; }
        }

        /// <summary>
        /// 获取或设置导出txt分割符，导出类型为txt才有效
        /// </summary>
        public string TxtSplitStr
        {
            get { return this.m_txtSplitStr; }
            set { this.m_txtSplitStr = value; }
        }

        /// <summary>
        /// 获取或设置导出是否包含标题行
        /// </summary>
        public bool TxtExportTitle
        {
            get { return this.m_txtExportTitle; }
            set { this.m_txtExportTitle = value; }
        }

        /// <summary>
        /// 获取或设置导出txt默认填充字符，导出类型为txt才有效
        /// </summary>
        public char TxtPaddingChar
        {
            get { return this.m_txtPaddingChar; }
            set { this.m_txtPaddingChar = value; }
        }
    }
}
