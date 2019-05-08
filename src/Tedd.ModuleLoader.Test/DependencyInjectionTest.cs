using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tedd.ModuleLoader.Test.Common;
using Xunit;

namespace Tedd.ModuleLoader.Test
{
    public class DependencyInjectionTest
    {
        [Fact]
        public void RegisterScoped()
        {
            var builder = new HostBuilder()
           .ConfigureServices((hostingContext, services) =>
           {
               services.AddScoped<ICalcModule>(AssemblyLoader.LoadDirectory("."));
               // Build an intermediate service provider
               var sp = services.BuildServiceProvider();
               var module = sp.GetService<ICalcModule>();
               Assert.True(module.Add(2, 3) == 5);
           });

            var b=builder.Build();
            //b.Run();
        }

        [Fact]
        public void RegisterTransient()
        {
            var builder = new Microsoft.Extensions.Hosting.HostBuilder()
           .ConfigureServices((hostingContext, services) =>
           {
               services.AddTransient<ICalcModule>(AssemblyLoader.LoadDirectory("."));
               // Build an intermediate service provider
               var sp = services.BuildServiceProvider();
               var module = sp.GetService<ICalcModule>();
               Assert.True(module.Add(2, 3) == 5);
           });

            var b = builder.Build();
            //b.Run();
        }

        [Fact]
        public void RegisterSingleton()
        {
            var builder = new Microsoft.Extensions.Hosting.HostBuilder()
           .ConfigureServices((hostingContext, services) =>
           {
               services.AddSingleton<ICalcModule>(AssemblyLoader.LoadDirectory("."));
               // Build an intermediate service provider
               var sp = services.BuildServiceProvider();
               var module = sp.GetService<ICalcModule>();
               Assert.True(module.Add(2, 3) == 5);
           });

            var b = builder.Build();
            //b.Run();
        }
    }
}
