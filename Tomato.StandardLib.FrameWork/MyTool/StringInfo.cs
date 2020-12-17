using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Tomato.StandardLib.MyTool
{
    /// <summary>
    /// 字符串处理类
    /// </summary>
    public class StringInfo
    {
        private string m_Str;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="Str">要处理的字符串</param>
        public StringInfo(string Str)
        {
            this.m_Str = Str;
        }

        internal StringInfo SubString_Byte(int startIndex, int length)
        {
            string outstr = "";
            int n = 0;
            for (int i = 0; i < this.m_Str.Length; i++)
            {
                char ch = this.m_Str[i];
                n += Encoding.Default.GetByteCount(ch.ToString());
                if (n <= startIndex)
                    continue;

                if (n > length + startIndex)
                    break;

                outstr += ch;
            }

            StringInfo s = new StringInfo(outstr);
            return s;
        }

        internal StringInfo RightString_Byte(int length)
        {
            return SubString_Byte(length, true);
        }

        internal StringInfo LeftString_Byte(int length)
        {
            return SubString_Byte(length, false);
        }

        private StringInfo SubString_Byte(int length,bool isRight)
        {
            int len1 = Encoding.Default.GetBytes(this.m_Str).Length;
            if (len1 <= length) return this;

            string outstr = "";
            int n = 0;
            int j = 0;
            for (int i = 0; i < this.m_Str.Length; i++)
            {
                if (isRight)
                    j = this.m_Str.Length - i - 1;
                else
                    j = i;

                char ch = this.m_Str[j];
                n += Encoding.Default.GetByteCount(ch.ToString());
                if (n > length)
                    break;

                if(isRight)
                    outstr = ch + outstr;
                else
                    outstr += ch;
            }

            StringInfo s = new StringInfo(outstr);
            return s;
        }

        internal StringInfo PadLeft_Byte(int length, char c)
        {
            return Pad_Byte(length, c, false);
        }

        internal StringInfo PadRight_Byte(int length, char c)
        {
            return Pad_Byte(length, c, true);
        }

        private StringInfo Pad_Byte(int length, char c, bool isRight)
        {
            int len1 = Encoding.Default.GetBytes(this.m_Str).Length;
            if (len1 > length) return this;

            string p = new string(c, length - len1);
            string outstr =string.Empty;
            if(isRight)
                outstr = this.m_Str + p;
            else
                outstr = p + this.m_Str;

            StringInfo s = new StringInfo(outstr);
            return s;
        }

        internal StringInfo FixedString_Byte(int length, bool subRight, bool padRight,char c)
        {
            StringInfo s;
            if (subRight)
                s = this.RightString_Byte(length);
            else
                s = this.LeftString_Byte(length);

            if (padRight)
                s = s.PadRight_Byte(length, c);
            else
                s = s.PadLeft_Byte(length, c);

            return s;
        }

        /// <summary>
        /// 返回字符串实例
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.m_Str; 
        }

        /// <summary>
        /// 返回去除前后空格和非打印字符后的字符串实例
        /// </summary>
        /// <returns></returns>
        public string ToStringFiltered()
        {
            // 去除前后空格
            string s = this.m_Str.Trim();
            // 去除字符串中非打印字符
            s = Regex.Replace(s, @"\s", string.Empty);
            return s;
        }
    }
}
