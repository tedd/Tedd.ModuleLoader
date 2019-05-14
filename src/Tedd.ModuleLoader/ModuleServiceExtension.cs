using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        public static IServiceCollection AddHostedService<T>(this IServiceCollection services, IList<Assembly> assemblies) where T : class, IHostedService
        {
            // https://github.com/aspnet/Hosting/blob/f9d145887773e0c650e66165e0c61886153bcc0b/src/Microsoft.Extensions.Hosting.Abstractions/ServiceCollectionHostedServiceExtensions.cs
            foreach (var implementationType in assemblies.GetTypeInfoOf<T>())
                services.AddTransient(typeof(IHostedService), implementationType);

            return services;
        }
    }
}
