using System;
using System.Security.Cryptography;
using System.Text;

namespace Tomato.StandardLib.MyEncrypt
{
    /// <summary>
    /// MD5加密类
    /// </summary>
    public class MD5Method
    {
        /// <summary>
        /// (原)MD5加密
        /// </summary>
        /// <param name="strPwd">待加密字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string strPwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bt = Encoding.ASCII.GetBytes(strPwd);
            bt = md5.ComputeHash(bt);
            return Convert.ToBase64String(bt);
        }

        /// <summary>
        /// MD5+Base64标准加密
        /// </summary>
        /// <param name="strPwd">待加密字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string StringToMD5(string strPwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bt = Encoding.UTF8.GetBytes(strPwd);
            bt = md5.ComputeHash(bt);
            StringBuilder md5string = new StringBuilder();
            for (int i = 0; i < bt.Length; i++)
            {
                md5string.AppendFormat("{0:x2}", bt[i]);

            }
            bt = Encoding.UTF8.GetBytes(md5string.ToString());
            return Convert.ToBase64String(bt);
        }

        /// <summary>
        /// MD5Hash加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StringToMd5Hash(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bt = Encoding.UTF8.GetBytes(input);
            bt = md5.ComputeHash(bt);
            StringBuilder md5string = new StringBuilder();
            for (int i = 0; i < bt.Length; i++)
            {
                md5string.AppendFormat("{0:x2}", bt[i]);
            }
            return md5string.ToString();
        }
    }
}
