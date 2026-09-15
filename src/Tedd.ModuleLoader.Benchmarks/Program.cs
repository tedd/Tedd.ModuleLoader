using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.IO;
using System.Linq;

namespace Tedd.ModuleLoader.Benchmarks
{
    [MemoryDiagnoser]
    public class AssemblyLoaderBenchmarks
    {
        private string _tempDir;

        [GlobalSetup]
        public void Setup()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), "ModuleLoaderBenchmarks");
            Directory.CreateDirectory(_tempDir);

            // Need a real dll to avoid BadImageFormatException. Use our own output dll.
            string realDllPath = typeof(AssemblyLoaderBenchmarks).Assembly.Location;

            for (int i = 0; i < 50; i++)
            {
                File.WriteAllText(Path.Combine(_tempDir, $"test{i}.txt"), "test");
                File.Copy(realDllPath, Path.Combine(_tempDir, $"test{i}.dll"), true);
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            Directory.Delete(_tempDir, true);
        }

        [Benchmark(Baseline = true)]
        public void LoadDirectoryArchive()
        {
            try
            {
                Tedd.ModuleLoader.Archive.AssemblyLoader.LoadDirectory(_tempDir);
            }
            catch { }
        }

        [Benchmark]
        public void LoadDirectoryOptimized()
        {
            try
            {
                Tedd.ModuleLoader.AssemblyLoader.LoadDirectory(_tempDir);
            }
            catch { }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<AssemblyLoaderBenchmarks>();
        }
    }
}
