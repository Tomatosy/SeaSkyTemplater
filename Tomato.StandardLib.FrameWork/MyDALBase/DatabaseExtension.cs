using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Tomato.StandardLib.DAL.Base
{
    /// <summary>
	/// Extends <see cref="Database"/>.
	/// </summary>
	public static class DatabaseExtension
    {
        /// <summary>
        /// Executes the command and returns the first column 
        /// of the first row in the result set returned by the query.
        /// Extra columns or rows are ignored.
        /// </summary>
        /// <typeparam name="T">Type of the scalar.</typeparam>
        /// <param name="database">The <see cref="Database"/> that executes the command.</param>
        /// <param name="cmd">Command to execute.</param>
        /// <returns>The value of the first column of the first row in the result set.</returns>
        public static T ExecuteScalar<T>(this Database database, DbCommand cmd)
        {
            object value = database.ExecuteScalar(cmd);

            return ConvertValue<T>(value);
        }

        internal static IList<T> ExecuteList<T>(this Database database, DbCommand cmd, Func<IDataRecord, T> converter)
        {
            var result = new List<T>();
            using (var reader = database.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    result.Add(converter(reader));
                }
            }
            return result;
        }

        internal static T ExecuteEntity<T>(this Database database, DbCommand cmd, Func<IDataRecord, T> converter)
        {
            using (var reader = database.ExecuteReader(cmd))
            {
                return reader.Read() ? converter(reader) : default(T);
            }
        }

        internal static T ConvertValue<T>(object value)
        {
            return (T)ConvertUtils.ConvertTo(typeof(T), value == DBNull.Value ? null : value);
        }
    }
}
