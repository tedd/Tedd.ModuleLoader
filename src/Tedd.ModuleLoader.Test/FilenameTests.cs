using Xunit;

namespace Tedd.ModuleLoader.Test
{
    public class FilenameTests
    {
        [Theory]
        [InlineData("*.Module1.dll", 1)]
        [InlineData("XXX", 0)]
        public void FilenameFilter_Test(string pattern, int expectedCount)
        {
            Assert.Equal(expectedCount, AssemblyLoader.LoadDirectory(".", pattern).Count);
        }

        [Theory]
        [InlineData(@"Test.*Module1\.dll$", 1)]
        [InlineData("test.*module1XXX", 0)]
        public void FilenameFilterRegex_Test(string pattern, int expectedCount)
        {
            Assert.Equal(expectedCount, AssemblyLoader.LoadDirectory(".", pattern, false).Count);
        }

        [Theory]
        [InlineData(@"test.*module1.*\.dll$", 1)]
        [InlineData("test.*module1XXX", 0)]
        public void FilenameFilterRegexIgnoreCase_Test(string pattern, int expectedCount)
        {
            Assert.Equal(expectedCount, AssemblyLoader.LoadDirectory(".", pattern, true).Count);
        }
    }
}
