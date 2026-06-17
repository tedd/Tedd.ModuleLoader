# Tedd.ModuleLoader

A deterministic framework for dynamic assembly resolution and instantiation within modern .NET environments.

## Architectural Execution Flow

The `Tedd.ModuleLoader` architecture focuses on deterministic assembly loading via `AssemblyLoadContext.Default` and reflection-based type discovery/instantiation.

### Established Framework Capabilities

The functional core provides immediate, validated mechanics for assembly integration:
- **Dynamic Assembly Loading:** Loads external `.dll` files using `AssemblyLoadContext.Default.LoadFromAssemblyPath` from explicit or filtered file paths.
- **Generic Type Discovery:** Employs advanced reflection abstractions (`GetTypeInfoOf<T>`) to rapidly isolate concrete implementations of defined contracts (interfaces or base classes).
- **Dependency Injection Parity:** Seamless integration with standard `IServiceCollection` constructs, natively supporting scoped, transient, and singleton lifecycles (`AddScoped<T>`, `AddTransient<T>`, `AddSingleton<T>`, `AddHostedService<T>`).

### Planned Future Enhancements (Hypotheses)

The following architectural paradigms are currently under development and should not be considered functional in the existing production deployment:
- **Hierarchical Data Binding:** A structured context propagation system intended to establish reactive, cascading variable assignments across dynamically instantiated module boundaries.
- **Routed Event Infrastructure:** A decoupled messaging topology designed to propagate localized, cross-module events systematically through a hierarchical domain model.

### Integrated Structural Components
- **Retro-Computing Contextual Controls:** Newly integrated structural components for legacy compatibility abstractions seamlessly interpreting and rendering DOS-era UI controls within a contemporary, generic binding context.

## Dependencies

- References `Microsoft.Extensions.DependencyInjection.Abstractions` for `AddScoped<T>`, `AddTransient<T>`, and `AddSingleton<T>`.
- References `Microsoft.Extensions.Hosting.Abstractions` for integration with `IHostedService` via `AddHostedService<T>`.

## Operational Examples

### Direct Instantiation

Load all candidate assemblies within a directory (default filter `*.dll`) and synthesize instances for all exported types inheriting from the `ICalcModule` contract:

```csharp
var modules = AssemblyLoader.LoadDirectory(".").CreateInstances<ICalcModule>();
```

Constructor arguments can be explicitly injected:

```csharp
var modules = AssemblyLoader.LoadDirectory(".").CreateInstances<ICtorArgsModule>(new object[] { "Test", 123 });
```

### Type Introspection

Isolate structural type definitions (`TypeInfo`) for all candidate classes inheriting from `ICalcModule`:

```csharp
var types = AssemblyLoader.LoadDirectory(".").GetTypeInfoOf<ICalcModule>();
```

Synthesize instances directly from the collected `TypeInfo` references:

```csharp
var modules = types.CreateInstances<ICalcModule>();
```

### Dependency Injection Integration

Bind discovered module implementations seamlessly into the standard service collection:

```csharp
// Scoped Lifecycle
services.AddScoped<ICalcModule>(AssemblyLoader.LoadDirectory("."));

// Transient Lifecycle
services.AddTransient<ICalcModule>(AssemblyLoader.LoadDirectory("."));

// Singleton Lifecycle
services.AddSingleton<ICalcModule>(AssemblyLoader.LoadDirectory("."));
```

### Assembly Resolution Filtering

Assemblies can be explicitly filtered during the load context phase using standard `.NET` wildcards, complex Regular Expressions, or predefined explicit enumerations:

- **Standard .NET Globbing:**
  ```csharp
  var assemblies = AssemblyLoader.LoadDirectory(".", "*.Module1.dll");
  ```
- **Regex Filter (Case Insensitive):**
  ```csharp
  var assemblies = AssemblyLoader.LoadDirectory(".", @"Test.*Module1\.dll$", false);
  ```
- **Regex Filter (Case Sensitive):**
  ```csharp
  var assemblies = AssemblyLoader.LoadDirectory(".", @"Test.*Module1\.dll$", true);
  ```
- **Explicit Enumeration:**
  ```csharp
  var files = new List<string> { "Module1.dll", "Module2.dll" };
  var assemblies = AssemblyLoader.LoadFiles(files);
  ```