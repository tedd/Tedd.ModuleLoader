using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
#if NET8_0_OR_GREATER
using System.Runtime.Loader;
#endif
using System.Text;
using System.Text.RegularExpressions;

namespace Tedd.ModuleLoader
{
    public static class AssemblyLoader
    {
        // Time Complexity: O(n) where n is the number of files in the directory.
        // Space Complexity: O(k) where k is the number of DLL files successfully matched.
        public static IList<Assembly> LoadDirectory(string directory)
        {
            return AssemblyLoader.LoadFiles(Directory.EnumerateFiles(directory, "*.dll", SearchOption.TopDirectoryOnly));
        }

        // Time Complexity: O(n) where n is the number of files matching the search pattern.
        // Space Complexity: O(k) where k is the number of files successfully matched.
        public static IList<Assembly> LoadDirectory(string directory, string searchPattern)
        {
            return AssemblyLoader.LoadFiles(Directory.EnumerateFiles(directory, searchPattern));
        }

        // Time Complexity: O(n * m) where n is the number of files and m is regex matching cost.
        // Space Complexity: O(k) where k is the number of files successfully matched.
        public static IList<Assembly> LoadDirectory(string directory, string regexSearchPattern, bool ignoreCase)
        {
            var options = ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;
            var regex = new Regex(regexSearchPattern, options);
            return AssemblyLoader.LoadFiles(Directory.EnumerateFiles(directory).Where(f => regex.IsMatch(f)));
        }

        // Time Complexity: O(n) where n is the number of files in the enumeration.
        // Space Complexity: O(n) where n is the number of files added to the assembly list.
        public static IList<Assembly> LoadFiles(IEnumerable<string> files)
        {
            var list = new List<Assembly>();
            foreach (string fileName in files)
            {
                var fullName = Path.GetFullPath(fileName);
#if NET8_0_OR_GREATER
                var item = AssemblyLoadContext.Default.LoadFromAssemblyPath(fullName);
#else
                var item = Assembly.LoadFile(fullName);
#endif
                list.Add(item);
            }
            return list;
        }
    }
}
