using System;
using Tedd.ModuleLoader.Test.Common;
using Xunit;

namespace Tedd.ModuleLoader.Test
{
    public class UnitTest1
    {
        [Fact]
        public void ExecuteInstances()
        {
            var modules = AssemblyLoader
                .LoadDirectory(".")
                .CreateInstances<ICalcModule>();

            Assert.True(modules.Count > 1);

            foreach (var module in modules)
            {
                Assert.True(module.Add(5, 5) == 10);
            }
        }

        [Fact]
        public void CountAssemblies()
        {
            Assert.True(AssemblyLoader.LoadDirectory(".").Count > 1);
        }

        [Fact]
        public void CountInterfaces()
        {
            Assert.True(AssemblyLoader.LoadDirectory(".").GetTypeInfoOf<ICalcModule>().Count > 1);
        }

              [Fact]
        public void AssemblyInstancesWithArgs()
        {

            var modules = AssemblyLoader
                .LoadDirectory(".")
                .CreateInstances<ICtorArgsModule>(new object[] { "Test", 123 });

            Assert.True(modules.Count == 1);

            foreach (var module in modules)
            {
                Assert.True(module.Arg1 == "Test");
                Assert.True(module.Arg2 == 123);
            }
        }
        [Fact]
        public void TypeInfoInstances()
        {

            var modules = AssemblyLoader
                .LoadDirectory(".")
                .GetTypeInfoOf<ICalcModule>()
                .CreateInstances<ICalcModule>();

            Assert.True(modules.Count >1);

            foreach (var module in modules)
            {
                Assert.True(module.Add(1, 2) == 3);
            }
        }
        [Fact]
        public void TypeInfoInstancesWithArgs()
        {

            var modules = AssemblyLoader
                .LoadDirectory(".")
                .GetTypeInfoOf<ICtorArgsModule>()
                .CreateInstances<ICtorArgsModule>(new object[] { "Test", 123 });

            Assert.True(modules.Count == 1);

            foreach (var module in modules)
            {
                Assert.True(module.Arg1 == "Test");
                Assert.True(module.Arg2 == 123);
            }
        }
    }

}
