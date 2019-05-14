# Dependency
References `Microsoft.Extensions.DependencyInjection.Abstractions` for AddScoped<T>, AddTransient<T> and AddSingleton<T>.
References `Microsoft.Extensions.Hosting.Abstractions` to reference `IHostedService` in `AddHostedService<T>`.

# Examples

## Create instances directly
Load all files in directory (with default filter of *.dll). Create an instance of all classes that inherit from ICalcModule.

`var modules = AssemblyLoader.LoadDirectory(".").CreateInstances<ICalcModule>();`

or if ctor requires arguments

`var modules = AssemblyLoader.LoadDirectory(".").CreateInstances<ICtorArgsModule>(new object[] { "Test", 123 });`

## Get TypeInfo
Load all files in directory (with default filter of *.dll). Find TypeInfo for all classes that inherit from ICalcModule.

`var types = AssemblyLoader.LoadDirectory(".").GetTypeInfoOf<ICalcModule>();`

## Create instance from TypeInfo

`types.CreateInstances<ICalcModule>()`

## Dependency injection
`services.AddScoped<ICalcModule>(AssemblyLoader.LoadDirectory("."));`

or

`services.AddTransient<ICalcModule>(AssemblyLoader.LoadDirectory("."));`

or

`services.AddSingleton<ICalcModule>(AssemblyLoader.LoadDirectory("."));`

## Filter what files to load

* .Net Directory.GetFiles filter:
`AssemblyLoader.LoadDirectory(".", "*.Module1.dll")`
* Regex filter, case insensitive: `AssemblyLoader.LoadDirectory(".", @"Test.*Module1\.dll$", false)`
* Regex filter, case sensitive: `AssemblyLoader.LoadDirectory(".", @"Test.*Module1\.dll$", true)`
* Custom list of files: `AssemblyLoader.LoadDirectory(files)` where files is an IEnumerable (i.e. List, Array or LINQ result) of string.