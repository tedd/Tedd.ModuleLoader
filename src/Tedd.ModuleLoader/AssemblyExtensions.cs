using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tedd.ModuleLoader
{
    public static class AssemblyExtensions
    {
        public static IList<TypeInfo> GetTypeInfoOf<T>(this Assembly assembly) where T : class
        {
            var tti = typeof(T).GetTypeInfo();
            return assembly
                .DefinedTypes
                .Where(type => type.IsClass && type.IsPublic && tti.IsAssignableFrom(type.AsType()))
                .ToList();
        }

        public static IList<TypeInfo> GetTypeInfoOf<T>(this IList<Assembly> assemblies) where T : class
        {
            return assemblies
                .SelectMany((Assembly a) => a.GetTypeInfoOf<T>())
                .ToList();
        }

        public static IList<T> CreateInstances<T>(this IList<Assembly> assemblies) where T : class
        {
            return assemblies
                .SelectMany((Assembly a) => a.GetTypeInfoOf<T>())
                .Select(t => (T)Activator.CreateInstance(t))
                .ToList();
        }

        public static IList<T> CreateInstances<T>(this IList<Assembly> assemblies, object[] args) where T : class
        {
            return assemblies
                .SelectMany((Assembly a) => a.GetTypeInfoOf<T>())
                .Select(t => (T)Activator.CreateInstance(t, args))
                .ToList();
        }
        //public static IList<T> GetInstances<T>(this IList<Assembly> assemblies, params object[] args) where T : class
        //{
        //    return assemblies
        //        .SelectMany((Assembly a) => a.GetTypeInfoOf<T>())
        //        .Select(t => (T)Activator.CreateInstance(t, args))
        //        .ToList();
        //}
    }
}
