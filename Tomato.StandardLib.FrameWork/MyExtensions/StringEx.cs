using System;
using System.ComponentModel;
using System.Text;

namespace Tomato.StandardLib.MyExtensions
{
    public static class StringEx
    {
        public static double ToDouble(this string value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static byte[] ToByte(this string str, Encoding encode)
        {
            return encode.GetBytes(str);
        }
        /// <summary>
        /// 隐藏手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HideTel(this string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 11)
            {
                return input;
            }
            return input.Substring(0, 3) + "*****" + input.Substring(8, 3);

        }
        public static int ToInt(this string str, int defaultValue = int.MinValue)
        {

            if (!int.TryParse(str, out int result))
            {
                if (defaultValue != int.MinValue)
                    result = defaultValue;
                else
                    throw new Exception(str + "无法转换成整数类型！");
            }

            return result;
        }

        /// <summary>
        /// 影藏名称
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HideName(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return "**" + input[input.Length - 1];

        }

        /// <summary>
        /// 字符串处理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            string[] arry = value.Split('_');
            string str = "";
            foreach (string item in arry)
            {
                string newstr = item.Replace("(", "").Replace(".", "").Replace(")", "");
                string firstLetter = newstr.Substring(0, 1);
                string rest = newstr.Substring(1, newstr.Length - 1);
                str += firstLetter.ToUpper() + rest.ToLower();
            }
            return str;
        }

        public static string ToCamel(this string str)
        {
            string[] array = str.Split(new char[1]
            {
                '_'
            });
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i][0].ToString().ToUpper() + array[i].Substring(1, array[i].Length - 1);
            }
            return string.Join("", array);
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

        public static int ToDefault(this string s, int defaultValue)
        {
            if (int.TryParse(s, out int result))
            {
                return result;
            }
            return defaultValue;
        }

        public static Guid ToDefault(this string s, Guid defaultValue)
        {
            try
            {
                return string.IsNullOrEmpty(s) ? defaultValue : new Guid(s);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static long ToLongDefault(this string s, long defaultValue)
        {
            if (long.TryParse(s, out long result))
            {
                return result;
            }
            return defaultValue;
        }

        public static double ToDoubleDefault(this string s, double defaultValue)
        {
            if (double.TryParse(s, out double result))
            {
                return result;
            }
            return defaultValue;
        }

        public static decimal ToDecimalDefault(this string s, decimal defaultValue)
        {
            if (decimal.TryParse(s, out decimal result))
            {
                return result;
            }
            return defaultValue;
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);
        }

        public static bool IsNullOrEmpty(this Guid? s)
        {
            return (s == null || s == Guid.Empty);
        }

        public static bool IsNullOrEmpty(this int? s)
        {
            return (s == null || s == 0);
        }

        public static bool IsNullOrEmpty(this DateTime? s)
        {
            return (s == null || s == DateTime.MinValue);
        }

        public static bool IsNullOrEmpty(this bool? s)
        {
            return (s == null);
        }

        public static string GetDescription(this Enum value)
        {
            System.Reflection.FieldInfo fi = value.GetType().GetField(value.ToString());
            if (fi != null)
            {
                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                        typeof(DescriptionAttribute), false);
                return attributes.Length > 0 ? attributes[0].Description : value.ToString();
            }
            return "";
        }

        public static object CastType(this string str, Type type)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            if (type == null)
            {
                return str;
            }
            if (type.IsArray)
            {
                Type elementType = type.GetElementType();
                string[] array = str.Split(new char[1]
                {
                    ';'
                });
                Array array2 = Array.CreateInstance(elementType, array.Length);
                int i = 0;
                for (int num = array.Length; i < num; i++)
                {
                    array2.SetValue(ConvertSimpleType(array[i], elementType), i);
                }
                return array2;
            }
            return ConvertSimpleType(str, type);
        }

        private static object ConvertSimpleType(object value, Type destinationType)
        {
            if (value == null || destinationType.IsInstanceOfType(value))
            {
                return value;
            }
            string text = value as string;
            if (text != null && text.Length == 0)
            {
                return null;
            }
            TypeConverter converter = TypeDescriptor.GetConverter(destinationType);
            bool flag = converter.CanConvertFrom(value.GetType());
            if (!flag)
            {
                converter = TypeDescriptor.GetConverter(value.GetType());
            }
            if (!flag && !converter.CanConvertTo(destinationType))
            {
                throw new InvalidOperationException("无法转换成类型：" + value.ToString() + "==>" + destinationType);
            }
            try
            {
                return flag ? converter.ConvertFrom(null, null, value) : converter.ConvertTo(null, null, value, destinationType);
            }
            catch (Exception innerException)
            {
                throw new InvalidOperationException("类型转换出错：" + value.ToString() + "==>" + destinationType, innerException);
            }
        }
    }
}
