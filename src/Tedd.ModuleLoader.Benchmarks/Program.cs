using BenchmarkDotNet.Running;

namespace Tedd.ModuleLoader.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<AssemblyLoaderBenchmark>();
        }
    }
}
