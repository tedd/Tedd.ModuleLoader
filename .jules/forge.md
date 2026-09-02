## 2026-06-02 - NuGet Dependency and Framework Modernization

**Observation:** The package uses outdated target frameworks (`netcoreapp2.0`, `netcoreapp2.1`, `netcoreapp2.2`) which are out of support and contain vulnerabilities in transitive dependencies (Microsoft.NETCore.App 2.x). The dependencies `Microsoft.Extensions.DependencyInjection.Abstractions` and `Microsoft.Extensions.Hosting.Abstractions` are at version 2.2.0. Upgrading these dependencies unconditionally to 9.0.0 is not compatible with `netcoreapp2.x` or `netstandard2.0`. Tests and examples were targeting `netcoreapp2.2`.

**Strategic Action:**
1. Modernized `Tedd.ModuleLoader` target frameworks to `<TargetFrameworks>netstandard2.0;net8.0;net9.0</TargetFrameworks>` to maintain broad compatibility while supporting the latest .NET versions. `netstandard2.0` serves as a stable, widely supported baseline that preserves compatibility for legacy applications (including replacing `netcoreapp2.0`, `netcoreapp2.1`, `netcoreapp2.2` which effectively run on `netstandard2.0`).
2. Updated NuGet dependencies conditionally. For `netstandard2.0`, retained `2.2.0` versions. For `net8.0` and `net9.0`, upgraded to `9.0.0` versions.
3. Updated code to use preprocessor directives (`#if NET8_0_OR_GREATER`) to utilize `System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath` for modern frameworks, and `Assembly.LoadFile` for legacy `netstandard2.0`.
4. Modernized test projects and examples to target `net8.0` and updated test dependencies (e.g., `Microsoft.NET.Test.Sdk`, `xunit`, `xunit.runner.visualstudio`) to latest stable versions to enable testing and avoid old SDK vulnerabilities.
5. Updated package metadata to include the `README.md` file in `Tedd.ModuleLoader.csproj` as prompted by `dotnet build`.

## 2024-03-24 - Forge: NuGet Dependency and Framework Modernization

**Observation:**
Project `Tedd.ModuleLoader` has outdated dependencies for `net8.0` and `net9.0`. Project `Tedd.ModuleLoader.Test` has outdated dependencies for `net8.0` including `Microsoft.Extensions.*`, `xunit`, `Microsoft.NET.Test.Sdk`, and `xunit.runner.visualstudio`. The `netstandard2.0` target in `Tedd.ModuleLoader` retains `2.2.0` to preserve legacy compatibility and is not modified.
In `Tedd.ModuleLoader.Test`, `xunit` is deprecated and `xunit.v3` is the alternative. The current xunit is 2.7.0.

**Strategic Action:**
1. Update `Tedd.ModuleLoader` dependencies:
   - `net8.0`: `Microsoft.Extensions.DependencyInjection.Abstractions` and `Microsoft.Extensions.Hosting.Abstractions` to `8.0.2` and `8.0.1` respectively.
   - `net9.0`: `Microsoft.Extensions.DependencyInjection.Abstractions` and `Microsoft.Extensions.Hosting.Abstractions` to `9.0.2`.
2. Update `Tedd.ModuleLoader.Test` dependencies to the latest available stable 9.0.x versions for Microsoft.Extensions.*, and update test dependencies to xunit 2.9.3, xunit.runner.visualstudio 3.0.2, and Microsoft.NET.Test.Sdk 17.13.0.
