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
    public class AssemblyLoaderTests
    {
        [Theory]
        [InlineData(".", 1)]
        [InlineData("NotADirectory", 0)]
        public void LoadDirectory_ShouldHandleValidAndInvalidPaths(string directory, int expectedMinAssemblies)
        {
            if (expectedMinAssemblies == 0)
            {
                Assert.Throws<DirectoryNotFoundException>(() => AssemblyLoader.LoadDirectory(directory));
            }
            else
            {
                var assemblies = AssemblyLoader.LoadDirectory(directory);
                Assert.True(assemblies.Count >= expectedMinAssemblies);
            }
        }

        [Theory]
        [InlineData(".", "*.Module1.dll", 1)]
        [InlineData(".", "XXX", 0)]
        public void LoadDirectory_WithSearchPattern_ShouldReturnExpectedCount(string directory, string searchPattern, int expectedCount)
        {
            var assemblies = AssemblyLoader.LoadDirectory(directory, searchPattern);
            Assert.Equal(expectedCount, assemblies.Count);
        }

        [Theory]
        [InlineData(".", @"Test.*Module1\.dll$", false, 1)]
        [InlineData(".", "test.*module1XXX", false, 0)]
        [InlineData(".", @"test.*module1.*\.dll$", true, 1)]
        [InlineData(".", "test.*module1XXX", true, 0)]
        public void LoadDirectory_WithRegex_ShouldReturnExpectedCount(string directory, string regexPattern, bool ignoreCase, int expectedCount)
        {
            var assemblies = AssemblyLoader.LoadDirectory(directory, regexPattern, ignoreCase);
            Assert.Equal(expectedCount, assemblies.Count);
        }
    }

}
