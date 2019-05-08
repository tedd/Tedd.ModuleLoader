using Xunit;

namespace Tedd.ModuleLoader.Test
{
    public class FilenameTests
    {
        [Fact]
        public void FilenameFilter()
        {
            Assert.True(AssemblyLoader.LoadDirectory(".", "*.Module1.dll").Count == 1);
        }
        [Fact]
        public void FilenameFilterNoMatch()
        {
            Assert.True(AssemblyLoader.LoadDirectory(".", "XXX").Count == 0);
        }
        [Fact]
        public void FilenameFilterRegex()
        {
            Assert.True(AssemblyLoader.LoadDirectory(".", @"Test.*Module1\.dll$", false).Count == 1);
        }
        [Fact]
        public void FilenameFilterRegexNoMatch()
        {
            Assert.True(AssemblyLoader.LoadDirectory(".", "test.*module1XXX", false).Count == 0);
        }
        [Fact]
        public void FilenameFilterRegexIgnoreCase()
        {
            Assert.True(AssemblyLoader.LoadDirectory(".", @"test.*module1.*\.dll$", true).Count == 1);
        }
        [Fact]
        public void FilenameFilterRegexIgnoreCaseNoMatch()
        {
            Assert.True(AssemblyLoader.LoadDirectory(".", "test.*module1XXX", true).Count == 0);
        }
    }
}
