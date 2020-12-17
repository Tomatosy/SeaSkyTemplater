using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Tomato.StandardLib.MyExtensions
{
    public static class DataTableEx
    {
        public static List<T> ToList<T>(this DataTable dt, bool isStoreDB = true) where T : class, new()
        {
            List<T> list = new List<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (DataRow row in dt.Rows)
            {
                T val = new T();
                PropertyInfo[] array = properties;
                foreach (PropertyInfo propertyInfo in array)
                {
                    if (dt.Columns.Contains(propertyInfo.Name) && row[propertyInfo.Name] != null && row[propertyInfo.Name] != DBNull.Value && (!isStoreDB || !(propertyInfo.PropertyType == typeof(DateTime)) || !(Convert.ToDateTime(row[propertyInfo.Name]) < Convert.ToDateTime("1753-01-01"))))
                    {
                        Type type = propertyInfo.PropertyType;
                        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            type = type.GetGenericArguments()[0];
                        }
                        object value = Convert.ChangeType(row[propertyInfo.Name], type);
                        propertyInfo.SetValue(val, value, null);
                    }
                }
                list.Add(val);
            }
            return list;
        }

        public static T ToEntity<T>(this DataTable dt, int rowindex = 0, bool isNeedDealDatetime = true) where T : class, new()
        {
            Type typeFromHandle = typeof(T);
            T val = new T();
            if (dt == null)
            {
                return val;
            }
            DataRow dataRow = dt.Rows[rowindex];
            PropertyInfo[] properties = typeFromHandle.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (dt.Columns.Contains(propertyInfo.Name) && dataRow[propertyInfo.Name] != null && dataRow[propertyInfo.Name] != DBNull.Value && (!isNeedDealDatetime || !(propertyInfo.PropertyType == typeof(DateTime)) || !(Convert.ToDateTime(dataRow[propertyInfo.Name]) < Convert.ToDateTime("1753-01-02"))))
                {
                    Type type = propertyInfo.PropertyType;
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        type = type.GetGenericArguments()[0];
                    }
                    object value = Convert.ChangeType(dataRow[propertyInfo.Name], type);
                    propertyInfo.SetValue(val, value, null);
                }
            }
            return val;
        }
    }
}
