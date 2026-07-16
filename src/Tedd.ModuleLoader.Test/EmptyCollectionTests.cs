using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Tedd.ModuleLoader;
using Tedd.ModuleLoader.Test.Common;

namespace Tedd.ModuleLoader.Test
{
    public class EmptyCollectionTests
    {
        public static IEnumerable<object[]> GetArgs()
        {
            yield return new object[] { new object[] { 1, 2 } };
            yield return new object[] { new object[] { } };
            yield return new object[] { null };
        }

        [Fact]
        public void CreateInstances_EmptyList_ReturnsEmptyList()
        {
            var emptyList = new List<TypeInfo>();
            var result = emptyList.CreateInstances<ICalcModule>();
            Assert.Empty(result);
        }

        [Theory]
        [MemberData(nameof(GetArgs))]
        public void CreateInstancesWithArgs_EmptyList_ReturnsEmptyList(object[] args)
        {
            var emptyList = new List<TypeInfo>();
            var result = emptyList.CreateInstances<ICalcModule>(args);
            Assert.Empty(result);
        }

        [Fact]
        public void CreateInstancesWithAssemblies_EmptyList_ReturnsEmptyList()
        {
            var emptyList = new List<Assembly>();
            var result = emptyList.CreateInstances<ICalcModule>();
            Assert.Empty(result);
        }

        [Theory]
        [MemberData(nameof(GetArgs))]
        public void CreateInstancesWithAssembliesAndArgs_EmptyList_ReturnsEmptyList(object[] args)
        {
            var emptyList = new List<Assembly>();
            var result = emptyList.CreateInstances<ICalcModule>(args);
            Assert.Empty(result);
        }

        [Fact]
        public void CreateInstancesWithAssembliesAndArgs_WithValidTypes_CreatesInstances()
        {
            var assemblies = AssemblyLoader.LoadDirectory(".");
            var result = assemblies.CreateInstances<ICtorArgsModule>(new object[] { "Test", 123 });
            Assert.NotEmpty(result);
            foreach (var r in result)
            {
                Assert.Equal("Test", r.Arg1);
                Assert.Equal(123, r.Arg2);
            }
        }
    }
}
