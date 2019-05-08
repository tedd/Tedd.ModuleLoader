using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Tedd.ModuleLoader
{
    public static class ModuleServiceExtensions
    {
        public static IServiceCollection AddScoped<T>(this IServiceCollection services, IList<Assembly> assemblies) where T : class
        {
            foreach (var implementationType in assemblies.GetTypeInfoOf<T>())
                services.AddScoped(typeof(T), implementationType);

            return services;
        }

        public static IServiceCollection AddTransient<T>(this IServiceCollection services, IList<Assembly> assemblies) where T : class
        {
            foreach (var implementationType in assemblies.GetTypeInfoOf<T>())
                services.AddTransient(typeof(T), implementationType);

            return services;
        }

        public static IServiceCollection AddSingleton<T>(this IServiceCollection services, IList<Assembly> assemblies) where T : class
        {
            foreach (var type in assemblies.GetTypeInfoOf<T>())
                services.AddSingleton(typeof(T), (T)Activator.CreateInstance(type));

            return services;
        }
    }
}
