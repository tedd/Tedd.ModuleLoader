using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tedd.ModuleLoader
{
    public static class TypeInfoExtensions
    {
        public static T CreateInstance<T>(this TypeInfo type) where T : class
        {
            return (T)Activator.CreateInstance(type);
        }

        public static T CreateInstance<T>(this TypeInfo type, object[] args) where T : class
        {
            return (T)Activator.CreateInstance(type, args);
        }
        public static IList<T> CreateInstances<T>(this IList<TypeInfo> types) where T : class
        {
            return types.Select(t => t.CreateInstance<T>()).ToList();
        }

        public static IList<T> CreateInstances<T>(this IList<TypeInfo> types, object[] args) where T : class
        {
            return types.Select(t => t.CreateInstance<T>(args)).ToList();
        }
    }
}
