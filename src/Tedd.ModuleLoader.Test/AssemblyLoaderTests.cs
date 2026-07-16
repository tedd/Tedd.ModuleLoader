using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;
using Tedd.ModuleLoader;

namespace Tedd.ModuleLoader.Test
{
    public class AssemblyLoaderTests
    {
        [Fact]
        public void LoadDirectory_WithEmptyDirectory_ReturnsEmptyList()
        {
            string emptyDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(emptyDir);
            try
            {
                var result = AssemblyLoader.LoadDirectory(emptyDir);
                Assert.Empty(result);
            }
            finally
            {
                Directory.Delete(emptyDir);
            }
        }

        [Fact]
        public void LoadFiles_WithEmptyList_ReturnsEmptyList()
        {
            var emptyFiles = new List<string>();
            var result = AssemblyLoader.LoadFiles(emptyFiles);
            Assert.Empty(result);
        }
    }
}
