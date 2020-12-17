using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Tomato.StandardLib.MyConvert
{
    /// <summary>
    /// Object序列化
    /// </summary>
    public static class ObjectSerializeConvert
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>byteString</returns>
        public static string BinarySerialize(object obj)
        {
            BinaryFormatter ser = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();
            ser.Serialize(mStream, obj);
            byte[] buf = mStream.ToArray();
            mStream.Close();

            return ByteToStr(buf);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="byteString">byteString</param>
        /// <returns>object</returns>
        public static object DeSerialize(string byteString)
        {
            byte[] binary = strToByte(byteString);
            BinaryFormatter ser = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream(binary);
            object obj = new object();
            obj = (object)ser.Deserialize(mStream);
            mStream.Close();
            return obj;
        }

        /// <summary>
        /// byte[]转string
        /// </summary>
        /// <param name="inputBytes"></param>
        /// <returns></returns>
        public static string ByteToStr(byte[] inputBytes)
        {
            UnicodeEncoding converter = new UnicodeEncoding();
            string result = System.Convert.ToBase64String(inputBytes);

            return result;
        }

        /// <summary>
        /// string转byte[]
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static byte[] strToByte(string inputString)
        {
            UnicodeEncoding converter = new UnicodeEncoding();

            byte[] inputBytes = System.Convert.FromBase64String(inputString);
            return inputBytes;
        }
    }
}
