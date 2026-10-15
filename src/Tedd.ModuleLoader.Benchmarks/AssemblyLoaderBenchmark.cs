using System;
using System.IO;
using BenchmarkDotNet.Attributes;

namespace Tedd.ModuleLoader.Benchmarks
{
    [MemoryDiagnoser]
    public class AssemblyLoaderBenchmark
    {
        private string _tempDirectory;
        private string _dummyDllPath;

        [GlobalSetup]
        public void Setup()
        {
            _tempDirectory = Path.Combine(Path.GetTempPath(), "Tedd.ModuleLoader.Benchmarks", Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDirectory);

            // Create 1 valid dummy .dll file
            _dummyDllPath = Path.Combine(_tempDirectory, "ValidModule.dll");
            File.WriteAllBytes(_dummyDllPath, new byte[] { 0x4D, 0x5A }); // MZ header for minimal valid structure

            // Create 1000 dummy .txt files to simulate heavily populated deployment folder
            for (int i = 0; i < 1000; i++)
            {
                File.WriteAllText(Path.Combine(_tempDirectory, $"DummyFile_{i}.txt"), "dummy");
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(_tempDirectory))
            {
                try { Directory.Delete(_tempDirectory, true); } catch { }
            }
        }

        [Benchmark(Baseline = true)]
        public void Archive_LoadDirectory()
        {
            try
            {
                Tedd.ModuleLoader.Archive.LegacyAssemblyLoader.LoadDirectory(_tempDirectory);
            }
            catch (BadImageFormatException)
            {
                // Expected because the dummy dll is not a real .NET assembly
            }
        }

        [Benchmark]
        public void Main_LoadDirectory()
        {
            try
            {
                Tedd.ModuleLoader.AssemblyLoader.LoadDirectory(_tempDirectory);
            }
            catch (BadImageFormatException)
            {
                // Expected because the dummy dll is not a real .NET assembly
            }
        }
    }
}
