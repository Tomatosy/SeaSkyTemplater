using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Tomato.StandardLib.MyConvert
{
    /// <summary>
    /// DataSetXml序列化
    /// </summary>
    public static class DataSetXMLConvert
    {
        /// <summary> 
        /// 序列化DataTable 
        /// </summary> 
        /// <param name="pDt">包含数据的DataTable</param> 
        /// <returns>序列化的DataTable</returns> 
        public static string SerializeDataTableXml(DataTable pDt)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            serializer.Serialize(writer, pDt);
            writer.Close();
            return sb.ToString();
        }  
        
        /// <summary> 
        /// 反序列化DataTable 
        /// </summary> 
        /// <param name="pXml">序列化的DataTable</param> 
        /// <returns>DataTable</returns> 
        public static DataTable DeserializeDataTable(string pXml)
        {
            StringReader strReader = new StringReader(pXml);
            XmlReader xmlReader = XmlReader.Create(strReader);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            DataTable dt = serializer.Deserialize(xmlReader) as DataTable;
            return dt;
        }

        /// <summary>
        /// 序列化DataSet
        /// </summary>
        /// <param name="xmlDS">包含数据的DataSet</param>
        /// <returns>序列化的DataSet</returns>
        public static string ConvertDataSetToXML(DataSet xmlDS)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            XmlSerializer serializer = new XmlSerializer(typeof(DataSet));
            serializer.Serialize(writer, xmlDS);
            writer.Close();
            return sb.ToString();
        }

        /// <summary>
        /// 反序列化DataSet 
        /// </summary>
        /// <param name="xmlData">序列化的DataSet</param>
        /// <returns>DataSet</returns>
        public static DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader strReader = new StringReader(xmlData);
            XmlReader xmlReader = XmlReader.Create(strReader);
            XmlSerializer serializer = new XmlSerializer(typeof(DataSet));
            DataSet ds = serializer.Deserialize(xmlReader) as DataSet;
            return ds;
        }

    }
}
