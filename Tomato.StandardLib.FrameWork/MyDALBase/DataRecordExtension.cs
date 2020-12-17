using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Tomato.StandardLib.DAL.Base
{
    /// <summary>
    /// Extends <see cref="System.Data.IDataRecord"/>.
    /// </summary>
    public static class DataRecordExtension
    {
        /// <summary>
        /// Gets the value of the specified field.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the field.
        /// Should be a reference type or a nullable value type 
        /// if it is possible that the field contains <see cref="DBNull"/>.
        /// </typeparam>
        /// <param name="record"></param>
        /// <param name="name">The name of the field to find.</param>
        /// <returns>
        /// The value of the specified field; <see langword="null"/> if the field contains <see cref="DBNull"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">This exception occurs when the value conversion failed.</exception>
        public static T GetValue<T>(this IDataRecord record, string name)
        {
            try
            {
                object value = record[name];

                return DatabaseExtension.ConvertValue<T>(value);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(string.Format("The value conversion failed. (column name : {0})", name), e);
            }
        }

        /// <summary>
        /// Gets the value of the specified field.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the field.
        /// Should be a reference type or a nullable value type 
        /// if it is possible that the field contains <see cref="DBNull"/>.
        /// </typeparam>
        /// <param name="record"></param>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The value of the specified field; <see langword="null"/> if the field contains <see cref="DBNull"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">This exception occurs when the value conversion failed.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static T GetValue<T>(this IDataRecord record, int i)
        {
            try
            {
                object value = record[i];

                return DatabaseExtension.ConvertValue<T>(value);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(string.Format("The value conversion failed. (column index : {0})", i), e);
            }
        }
    }
}
