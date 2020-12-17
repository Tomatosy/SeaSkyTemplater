using Tomato.StandardLib.DAL.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Tomato.StandardLib.MyConvert
{
    /// <summary>
    /// 对象映射器
    /// </summary>
    public static class ObjectMapper
    {
        /// <summary>
        /// 映射
        /// </summary>
        /// <typeparam name="TFrom">映射前类型</typeparam>
        /// <typeparam name="TTo">映射后类型</typeparam>
        /// <param name="from">映射前的对象</param>
        /// <param name="to">映射后的对象</param>
        /// <returns>映射后的对象</returns>
        public static TTo Map<TFrom, TTo>(TFrom from, TTo to)
        {
            return Map(from, to, new Dictionary<string, object>());
        }

        /// <summary>
        /// 映射
        /// </summary>
        /// <typeparam name="TFrom">映射前类型</typeparam>
        /// <typeparam name="TTo">映射后类型</typeparam>
        /// <param name="from">映射前的对象</param>
        /// <param name="to">映射后的对象</param>
        /// <param name="ignoreUnmatchedValue">忽略不匹配对象</param>
        /// <returns>映射后的对象</returns>
        public static TTo Map<TFrom, TTo>(TFrom from, TTo to, bool ignoreUnmatchedValue)
        {
            return Map(from, to, ignoreUnmatchedValue, new Dictionary<string, object>());
        }

        /// <summary>
        /// Maps properties of <paramref name="from"/> object into <paramref name="to"/> object.
        /// </summary>
        /// <typeparam name="TFrom">The type of <paramref name="from"/>.</typeparam>
        /// <typeparam name="TTo">The type of <paramref name="to"/>.</typeparam>
        /// <param name="from">An object mapped from.</param>
        /// <param name="to">An object mapped to.</param>
        /// <param name="explicitMappings">Explicit mappings.</param>
        /// <returns>Mapped <paramref name="to"/> object.</returns>
        /// <remarks>
        /// This method does not work under partially trusted environment.
        /// </remarks>
        public static TTo Map<TFrom, TTo>(TFrom from, TTo to, object explicitMappings)
        {
            return Map(from, to, GetExplicitMappings(explicitMappings));
        }

        /// <summary>
        /// Maps properties of <paramref name="from"/> object into <paramref name="to"/> object.
        /// </summary>
        /// <typeparam name="TFrom">The type of <paramref name="from"/>.</typeparam>
        /// <typeparam name="TTo">The type of <paramref name="to"/>.</typeparam>
        /// <param name="from">An object mapped from.</param>
        /// <param name="to">An object mapped to.</param>
        /// <param name="ignoreUnmatchedValue">Ignores unmatched value</param>
        /// <param name="explicitMappings">Explicit mappings.</param>
        /// <returns>Mapped <paramref name="to"/> object.</returns>
        /// <remarks>
        /// This method does not work under partially trusted environment.
        /// </remarks>
        public static TTo Map<TFrom, TTo>(TFrom from, TTo to, bool ignoreUnmatchedValue, object explicitMappings)
        {
            return Map(from, to, ignoreUnmatchedValue, GetExplicitMappings(explicitMappings));
        }

        /// <summary>
        /// Maps properties of <paramref name="from"/> object into <paramref name="to"/> object.
        /// </summary>
        /// <typeparam name="TFrom">The type of <paramref name="from"/>.</typeparam>
        /// <typeparam name="TTo">The type of <paramref name="to"/>.</typeparam>
        /// <param name="from">An object mapped from.</param>
        /// <param name="to">An object mapped to.</param>
        /// <param name="explicitMappings">Explicit mappings.</param>
        /// <returns>Mapped <paramref name="to"/> object.</returns>
        public static TTo Map<TFrom, TTo>(TFrom from, TTo to, IDictionary<string, object> explicitMappings)
        {
            return Map(from, to, false, explicitMappings);
        }

        /// <summary>
        /// Maps properties of <paramref name="from"/> object into <paramref name="to"/> object.
        /// </summary>
        /// <typeparam name="TFrom">The type of <paramref name="from"/>.</typeparam>
        /// <typeparam name="TTo">The type of <paramref name="to"/>.</typeparam>
        /// <param name="from">An object mapped from.</param>
        /// <param name="to">An object mapped to.</param>
        /// <param name="ignoreUnmatchedValue">Ignores unmatched value</param>
        /// <param name="explicitMappings">Explicit mappings.</param>
        /// <returns>Mapped <paramref name="to"/> object.</returns>
        public static TTo Map<TFrom, TTo>(TFrom from, TTo to, bool ignoreUnmatchedValue, IDictionary<string, object> explicitMappings)
        {
            var sourceProps = from.GetType().GetProperties();

            foreach (var destinationProp in to.GetType().GetProperties())
            {
                if (!destinationProp.CanWrite)
                    continue;

                object destinationValue;

                // Apply implicit mapping if there is no explicit mapping.
                if (!explicitMappings.TryGetValue(destinationProp.Name, out destinationValue))
                {
                    var sourceProp = sourceProps.FirstOrDefault(p => p.Name == destinationProp.Name && p.GetType() == destinationProp.GetType());

                    // Ignore if there is no such property in the source type.
                    if (sourceProp == null)
                        continue;

                    var sourceBaseType = sourceProp.PropertyType;
                    var destinationBaseType = destinationProp.PropertyType;

                    // Get the source value.
                    destinationValue = sourceProp.GetValue(from, null);
                    if (destinationValue == null)
                    {
                        if (destinationProp.PropertyType.IsNullable())
                        {
                            destinationProp.SetValue(to, destinationValue, null);
                            continue;
                        }
                    }
                    else
                    {
                        sourceBaseType = sourceProp.PropertyType.IsNullableValueType() ? sourceProp.PropertyType.GetGenericArguments()[0] : sourceProp.PropertyType;
                        destinationBaseType = destinationProp.PropertyType.IsNullableValueType() ? destinationProp.PropertyType.GetGenericArguments()[0] : destinationProp.PropertyType;
                    }

                    // Apply implicit type conversion.
                    if (sourceBaseType != destinationBaseType)
                    {

                        if (ConvertUtils.CanConvertTo(destinationBaseType, destinationValue))
                            destinationValue = ConvertUtils.ConvertTo(destinationBaseType, destinationValue);
                        else if (destinationBaseType == typeof(DateTime) && destinationValue is DateTime)
                            destinationValue = ((DateTime)destinationValue);
                        else
                            if (destinationBaseType == typeof(int))
                            destinationValue = Convert.ToInt32(destinationValue, CultureInfo.InvariantCulture);
                        else if (destinationBaseType == typeof(float))
                            destinationValue = Convert.ToSingle(destinationValue, CultureInfo.InvariantCulture);
                        else if (destinationBaseType == typeof(double))
                            destinationValue = Convert.ToDouble(destinationValue, CultureInfo.InvariantCulture);
                        else if (destinationBaseType == typeof(decimal))
                            destinationValue = Convert.ToDecimal(destinationValue, CultureInfo.InvariantCulture);
                        else if (destinationBaseType == typeof(DateTime))
                            destinationValue = Convert.ToDateTime(destinationValue, CultureInfo.InvariantCulture);
                        else if (ignoreUnmatchedValue)
                            continue;
                    }
                }
                destinationProp.SetValue(to, destinationValue, null);
            }

            return to;
        }

        private static IDictionary<string, object> GetExplicitMappings(object explicitMappings)
        {
            if (explicitMappings == null)
                return new Dictionary<string, object>();
            else
            {
                var explicitMappingsDictionary = explicitMappings as IDictionary<string, object>;
                if (explicitMappingsDictionary != null)
                    return explicitMappingsDictionary;
            }


            var mappings = new Dictionary<string, object>();

            var props = explicitMappings.GetType().GetProperties();

            foreach (var prop in props)
            {
                object val = prop.GetValue(explicitMappings, null);

                mappings.Add(prop.Name, val);
            }

            return mappings;
        }
    }
}
