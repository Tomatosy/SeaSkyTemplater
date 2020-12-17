using System;
using System.ComponentModel;

namespace Tomato.StandardLib.DAL.Base
{
    /// <summary>
    /// 转换工具
    /// </summary>
    public static class ConvertUtils
    {
        /// <summary>
        /// 能否转化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="value">需要转化的对象</param>
        /// <returns></returns>
        public static bool CanConvertTo(Type type, object value)
        {
            var baseType = type.IsNullableValueType() ? type.GetGenericArguments()[0] : type;
            var valueType = value == null ? null : value.GetType();

            if (value == null)
            {
                return type.IsNullable();
            }
            else if (baseType.IsAssignableFrom(valueType))
            {
                return true;
            }
            else
            {
                var converterTo = TypeDescriptor.GetConverter(baseType);
                if (converterTo.CanConvertFrom(valueType))
                {
                    return true;
                }

                var converterFrom = TypeDescriptor.GetConverter(valueType);
                return converterFrom.CanConvertTo(baseType);
            }
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="value">需要转换的对象</param>
        /// <returns>转换后的对象</returns>
        public static object ConvertTo(Type type, object value)
        {
            var baseType = type.IsNullableValueType() ? type.GetGenericArguments()[0] : type;
            var valueType = value == null ? null : value.GetType();

            if (value == null)
            {
                return null;
            }
            else if (baseType.IsAssignableFrom(valueType))
            {
                return value;
            }
            else
            {
                var converterTo = TypeDescriptor.GetConverter(baseType);
                if (converterTo.CanConvertFrom(valueType))
                {
                    return converterTo.ConvertFrom(value);
                }

                var converterFrom = TypeDescriptor.GetConverter(valueType);
                return converterFrom.ConvertTo(value, baseType);
            }
        }
    }
}
