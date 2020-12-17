using System;

namespace Tomato.StandardLib.DAL.Base
{
    /// <summary>
    /// Extends <see cref="System.Type"/>.
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// Gets a value indicating whether the current type is an instance of <see cref="Nullable{T}"/> type.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the type is a nullable value type;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNullableValueType(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Gets a value indicating whether the current type is nullable.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the type is a reference type or an instance of <see cref="Nullable{T}"/> type;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNullable(this Type type)
        {
            return !type.IsValueType || type.IsNullableValueType();
        }
    }
}
