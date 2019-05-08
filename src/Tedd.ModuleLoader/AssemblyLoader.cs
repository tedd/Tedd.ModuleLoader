using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.RegularExpressions;

namespace Tedd.ModuleLoader
{
    public static class AssemblyLoader
    {
        public static IList<Assembly> LoadDirectory(string directory)
        {
            return AssemblyLoader.LoadFiles(Directory.GetFiles(directory).Where(f => f.Substring(f.Length - 4, 4).ToLower() == ".dll"));
        }

        public static IList<Assembly> LoadDirectory(string directory, string searchPattern)
        {
            return AssemblyLoader.LoadFiles(Directory.GetFiles(directory, searchPattern));
        }

        public static IList<Assembly> LoadDirectory(string directory, string regexSearchPattern, bool ignoreCase)
        {
            var options = ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;
            var regex = new Regex(regexSearchPattern, options);
            return AssemblyLoader.LoadFiles(Directory.GetFiles(directory).Where(f => regex.IsMatch(f)));
        }

        public static IList<Assembly> LoadFiles(IEnumerable<string> files)
        {
            //return files.Select(f => AssemblyLoadContext.Default.LoadFromAssemblyPath(new FileInfo(f).FullName)).ToList();

            var list = new List<Assembly>();
            foreach (string fileName in files)
            {
                var fullName = new FileInfo(fileName).FullName;
                var item = AssemblyLoadContext.Default.LoadFromAssemblyPath(fullName);
                list.Add(item);
            }
            return list;
        }
    }
}
