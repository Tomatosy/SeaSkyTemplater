using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Tomato.StandardLib.DynamicPage
{
    public static class AssemblyEx
    {
        public static List<Type> GetAllChildType(this Assembly assembly, Type parentType)
        {
            List<Type> list = new List<Type>();
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.BaseType == parentType)
                {
                    list.Add(type);
                }
            }
            return list;
        }

        public static IEnumerable<MethodInfo> GetExtensionMethods(this Assembly assembly, string name)
        {
            return from type in assembly.GetTypes().Where(delegate (Type type)
            {
                if (!type.IsGenericType)
                {
                    return !type.IsNested;
                }
                return false;
            })
                   from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                   where method.IsDefined(typeof(ExtensionAttribute), inherit: false)
                   where method.Name == name
                   select method;
        }
    }
}