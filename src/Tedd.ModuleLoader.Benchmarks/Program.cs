using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;

namespace Tedd.ModuleLoader.Benchmarks
{
    [MemoryDiagnoser]
    public class AssemblyExtensionsBenchmarks
    {
        private IList<Assembly> _assemblies;

        [GlobalSetup]
        public void Setup()
        {
            _assemblies = new List<Assembly> { typeof(Tedd.ModuleLoader.Tests.ClassB).Assembly };
        }

        [Benchmark(Baseline = true)]
        public IList<TypeInfo> GetTypeInfoOf_Archive()
        {
            return Tedd.ModuleLoader.Archive.AssemblyExtensions.GetTypeInfoOf<Tedd.ModuleLoader.Tests.ClassA>(_assemblies);
        }

        [Benchmark]
        public IList<TypeInfo> GetTypeInfoOf_Optimized()
        {
            return Tedd.ModuleLoader.AssemblyExtensions.GetTypeInfoOf<Tedd.ModuleLoader.Tests.ClassA>(_assemblies);
        }

        [Benchmark]
        public IList<Tedd.ModuleLoader.Tests.ClassA> CreateInstances_Archive()
        {
            return Tedd.ModuleLoader.Archive.AssemblyExtensions.CreateInstances<Tedd.ModuleLoader.Tests.ClassA>(_assemblies);
        }

        [Benchmark]
        public IList<Tedd.ModuleLoader.Tests.ClassA> CreateInstances_Optimized()
        {
            return Tedd.ModuleLoader.AssemblyExtensions.CreateInstances<Tedd.ModuleLoader.Tests.ClassA>(_assemblies);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<AssemblyExtensionsBenchmarks>();
        }
    }
}
