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
    public class AssemblyExtensionsTests
    {
        [Theory]
        [InlineData(".")]
        public void GetTypeInfoOf_ShouldFindTypes(string directory)
        {
            var assemblies = AssemblyLoader.LoadDirectory(directory);
            var types = assemblies.GetTypeInfoOf<ICalcModule>();
            Assert.True(types.Count > 0);
        }

        [Theory]
        [InlineData(".")]
        public void CreateInstances_ShouldCreateExpectedInstances(string directory)
        {
            var assemblies = AssemblyLoader.LoadDirectory(directory);
            var instances = assemblies.CreateInstances<ICalcModule>();
            Assert.True(instances.Count > 0);
            foreach (var instance in instances)
            {
                Assert.Equal(10, instance.Add(5, 5));
            }
        }

        [Theory]
        [InlineData(".", "Test", 123)]
        public void CreateInstances_WithArgs_ShouldCreateExpectedInstances(string directory, string arg1, int arg2)
        {
            var assemblies = AssemblyLoader.LoadDirectory(directory);
            var instances = assemblies.CreateInstances<ICtorArgsModule>(new object[] { arg1, arg2 });
            Assert.True(instances.Count > 0);
            foreach (var instance in instances)
            {
                Assert.Equal(arg1, instance.Arg1);
                Assert.Equal(arg2, instance.Arg2);
            }
        }
    }

}
