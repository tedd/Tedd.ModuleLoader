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
    public class TypeInfoExtensionsTests
    {
        [Theory]
        [InlineData(".")]
        public void CreateInstances_FromTypeInfo_ShouldCreateInstances(string directory)
        {
            var types = AssemblyLoader.LoadDirectory(directory).GetTypeInfoOf<ICalcModule>();
            var instances = types.CreateInstances<ICalcModule>();
            Assert.True(instances.Count > 0);
            foreach (var instance in instances)
            {
                Assert.Equal(3, instance.Add(1, 2));
            }
        }

        [Theory]
        [InlineData(".", "Test", 123)]
        public void CreateInstances_FromTypeInfoWithArgs_ShouldCreateInstances(string directory, string arg1, int arg2)
        {
            var types = AssemblyLoader.LoadDirectory(directory).GetTypeInfoOf<ICtorArgsModule>();
            var instances = types.CreateInstances<ICtorArgsModule>(new object[] { arg1, arg2 });
            Assert.True(instances.Count > 0);
            foreach (var instance in instances)
            {
                Assert.Equal(arg1, instance.Arg1);
                Assert.Equal(arg2, instance.Arg2);
            }
        }
    }

}
