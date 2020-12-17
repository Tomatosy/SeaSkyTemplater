namespace Tomato.StandardLib.MyEncrypt
{
    /// <summary>
    /// 简单加密解密类
    /// </summary>
    public class SimpleMethod
    {
        /// <summary>
        /// 位移加密及解密
        /// </summary>
        /// <param name="oriString">待加密或解密的字符串</param>
        /// <returns>加密或解密后的字符串</returns>
        public static string getConfusedString(string oriString)
        {
            string newString = "";
            for (int i = 0; i < oriString.Length; i++)
            {
                newString += (char)(oriString[i] ^ 26);
            }
            return newString;
        }
    }
}
