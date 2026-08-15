using BenchmarkDotNet.Attributes;
using System.IO;
using System.Linq;

namespace Tedd.ModuleLoader.Benchmarks
{
    [MemoryDiagnoser]
    public class AssemblyLoaderBenchmarks
    {
        private string[] _files;

        [GlobalSetup]
        public void Setup()
        {
            _files = new string[1000];
            for (int i = 0; i < 900; i++)
            {
                _files[i] = $"/path/to/some/directory/file_{i}.dll";
            }
            for (int i = 900; i < 1000; i++)
            {
                _files[i] = $"/path/to/some/directory/file_{i}.txt";
            }
        }

        [Benchmark(Baseline = true)]
        public int LegacySubstring()
        {
            return _files.Where(f => f.Substring(f.Length - 4, 4).ToLower() == ".dll").Count();
        }

        [Benchmark]
        public int OptimizedEndsWith()
        {
            return _files.Where(f => f.EndsWith(".dll", System.StringComparison.OrdinalIgnoreCase)).Count();
        }
    }
}
