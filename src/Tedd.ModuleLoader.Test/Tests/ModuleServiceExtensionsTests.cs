using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tedd.ModuleLoader.Test.Common;
using Xunit;

namespace Tedd.ModuleLoader.Test
{
    public class ModuleServiceExtensionsTests
    {
        [Theory]
        [InlineData(".")]
        public void RegisterScoped_ShouldResolveInstance(string directory)
        {
            var assemblies = AssemblyLoader.LoadDirectory(directory);
            var services = new ServiceCollection();
            services.AddScoped<ICalcModule>(assemblies);
            var sp = services.BuildServiceProvider();
            var module = sp.GetService<ICalcModule>();
            Assert.NotNull(module);
            Assert.Equal(5, module.Add(2, 3));
        }

        [Theory]
        [InlineData(".")]
        public void RegisterTransient_ShouldResolveInstance(string directory)
        {
            var assemblies = AssemblyLoader.LoadDirectory(directory);
            var services = new ServiceCollection();
            services.AddTransient<ICalcModule>(assemblies);
            var sp = services.BuildServiceProvider();
            var module = sp.GetService<ICalcModule>();
            Assert.NotNull(module);
            Assert.Equal(5, module.Add(2, 3));
        }

        [Theory]
        [InlineData(".")]
        public void RegisterSingleton_ShouldResolveInstance(string directory)
        {
            var assemblies = AssemblyLoader.LoadDirectory(directory);
            var services = new ServiceCollection();
            services.AddSingleton<ICalcModule>(assemblies);
            var sp = services.BuildServiceProvider();
            var module = sp.GetService<ICalcModule>();
            Assert.NotNull(module);
            Assert.Equal(5, module.Add(2, 3));
        }

        [Theory]
        [InlineData(".")]
        public void RegisterHostedService_ShouldResolveInstance(string directory)
        {
            var assemblies = AssemblyLoader.LoadDirectory(directory);
            var services = new ServiceCollection();
            services.AddHostedService<DummyHostedService>(assemblies);
            var sp = services.BuildServiceProvider();
            var servicesResolved = sp.GetServices<IHostedService>().ToList();
            Assert.True(servicesResolved.Count > 0);
        }
    }

    public class DummyHostedService : IHostedService
    {
        public System.Threading.Tasks.Task StartAsync(System.Threading.CancellationToken cancellationToken) => System.Threading.Tasks.Task.CompletedTask;
        public System.Threading.Tasks.Task StopAsync(System.Threading.CancellationToken cancellationToken) => System.Threading.Tasks.Task.CompletedTask;
    }
}
