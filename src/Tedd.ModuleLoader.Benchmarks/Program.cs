using BenchmarkDotNet.Running;

namespace Tedd.ModuleLoader.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<AssemblyLoaderBenchmarks>();
        }
    }
}
