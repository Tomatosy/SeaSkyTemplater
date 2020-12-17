using System;
using System.Collections.Generic;
using System.Text;

namespace Tomato.StandardLib.DynamicPage
{
    public static class Ex
    {
        public static object ToObjectValue(this string str, string typeName)
        {
            if (typeName == typeof(string).FullName)
            {
                return str;
            }
            return typeof(int).Assembly.GetType(typeName).GetMethod("Parse", new Type[1]
            {
        typeof(string)
            }).Invoke(null, new object[1]
            {
        str
            });
        }


        public static string ToCSharpTypeStr(this string str)
        {
            string empty = string.Empty;
            switch (str.ToLower())
            {
                case "int":
                    return "System.Int32";
                case "text":
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "varchar":
                case "char":
                    return "System.String";
                case "bigint":
                    return "System.Int64";
                case "binary":
                    return "System.Byte[]";
                case "bit":
                    return "System.Boolean";
                case "datetime":
                case "datetime2":
                    return "System.DateTime";
                case "decimal":
                case "money":
                case "numeric":
                case "smallmoney":
                    return "System.Decimal";
                case "float":
                    return "System.Double";
                case "image":
                    return "System.Byte[]";
                case "real":
                    return "System.Single";
                case "smalldatetime":
                    return "System.DateTime";
                case "smallint":
                    return "System.Int16";
                case "timestamp":
                    return "System.DateTime";
                case "tinyint":
                    return "System.Byte";
                case "uniqueidentifier":
                    return "System.Guid";
                case "varbinary":
                    return "System.Byte[]";
                case "Variant":
                    return "System.Object";
                default:
                    return "System.String";
            }
        }

    }
}
