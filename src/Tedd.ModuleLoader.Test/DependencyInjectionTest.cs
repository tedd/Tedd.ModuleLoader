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
        public enum RegistrationType
        {
            Scoped,
            Transient,
            Singleton
        }

        [Theory]
        [InlineData(RegistrationType.Scoped)]
        [InlineData(RegistrationType.Transient)]
        [InlineData(RegistrationType.Singleton)]
        public void RegisterTypes(RegistrationType type)
        {
            var builder = new HostBuilder()
           .ConfigureServices((hostingContext, services) =>
           {
               if (type == RegistrationType.Scoped)
               {
                   services.AddScoped<ICalcModule>(AssemblyLoader.LoadDirectory("."));
               }
               else if (type == RegistrationType.Transient)
               {
                   services.AddTransient<ICalcModule>(AssemblyLoader.LoadDirectory("."));
               }
               else if (type == RegistrationType.Singleton)
               {
                   services.AddSingleton<ICalcModule>(AssemblyLoader.LoadDirectory("."));
               }

               var sp = services.BuildServiceProvider();
               var module = sp.GetService<ICalcModule>();
               Assert.True(module.Add(2, 3) == 5);
           });

            var b = builder.Build();
        }

        [Fact]
        public void RegisterHostedService()
        {
            var builder = new HostBuilder()
           .ConfigureServices((hostingContext, services) =>
           {
               services.AddHostedService<DummyHostedService>(AssemblyLoader.LoadDirectory("."));
               var sp = services.BuildServiceProvider();
               var hostedServices = sp.GetServices<IHostedService>();
               Assert.True(hostedServices.Count() > 0);
               Assert.Contains(hostedServices, s => s is DummyHostedService);
           });

            var b = builder.Build();
        }
    }
}
