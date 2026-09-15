💡 **Hypothesis:**
The mechanical inefficiency identified in `AssemblyLoader.LoadDirectory` was the eager loading of all files within a directory and subsequently allocating string arrays for both all file paths and filtered paths using `Directory.GetFiles()` combined with LINQ `Where`. Furthermore, `new FileInfo(fileName).FullName` allocated unnecessary `FileInfo` instances just to resolve the full path. Replacing eager loading with `Directory.EnumerateFiles(..., "*.dll", SearchOption.TopDirectoryOnly)` and avoiding intermediate heap allocations via `Path.GetFullPath()` should reduce memory consumption.

🎯 **Execution:**
- Substituted `Directory.GetFiles` with `Directory.EnumerateFiles(directory, "*.dll", SearchOption.TopDirectoryOnly)` to leverage OS-level file filtering and eliminate the allocation of large, intermediate string arrays before applying the LINQ `Where` clause.
- Replaced the instantiation of `new FileInfo(fileName).FullName` with the static and highly optimized `Path.GetFullPath(fileName)` method to eliminate extraneous GC pressure.
- Documented Big O complexity in comments for transparency on time/space expectations.

📊 **Empirical Impact:**
```text
BenchmarkDotNet v0.15.8, Linux Ubuntu 24.04.4 LTS (Noble Numbat)
Intel Xeon Processor 2.30GHz, 1 CPU, 4 logical and 4 physical cores
.NET SDK 10.0.103
  [Host]     : .NET 8.0.24 (8.0.24, 8.0.2426.7010), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 8.0.24 (8.0.24, 8.0.2426.7010), X64 RyuJIT x86-64-v3

| Method                 | Mean     | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------------- |---------:|----------:|----------:|------:|--------:|----------:|------------:|
| LoadDirectoryArchive   | 2.124 ms | 0.0370 ms | 0.0346 ms |  1.00 |    0.02 |  26.91 KB |        1.00 |
| LoadDirectoryOptimized | 2.136 ms | 0.0335 ms | 0.0313 ms |  1.01 |    0.02 |   6.45 KB |        0.24 |
```
_Note: Processing speed is virtually identical for 50 assemblies (as I/O dictates the baseline), but memory allocations are reduced by ~76% per execution cycle._

🔬 **Verification Protocol:**
1. Open the project in the `.NET SDK 8.0` environment.
2. Navigate to `src/Tedd.ModuleLoader.Benchmarks/`.
3. Execute `dotnet run -c Release`.
4. Validate the Memory allocation comparison between `Archive` and `Optimized` implementations.
5. Execute `dotnet test src/Tedd.ModuleLoader.sln` to guarantee structural parity.
