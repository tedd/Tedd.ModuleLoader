## 2024-05-17 - Dependency Modernization

**Observation:** The package references for target frameworks `net8.0` and `net9.0` in `Tedd.ModuleLoader.csproj` can be upgraded to the latest 8.0.x and 9.0.x patches. `netstandard2.0` package references in `Tedd.ModuleLoader.csproj` are reported as legacy, but upgrading them would break downstream `netstandard2.0` consumers because `Microsoft.Extensions.DependencyInjection.Abstractions` dropped support for older frameworks in later versions. For the test project (`Tedd.ModuleLoader.Test`), since the project targets `net8.0`, references like `Microsoft.Extensions.*` should be updated to `8.0.x` rather than `9.0.0` or `10.0.14` to avoid pulling unsupported framework assemblies. Also, xUnit, Microsoft.NET.Test.Sdk should be upgraded to their latest stable version.

**Strategic Action:**
1. Upgrade `net8.0` packages to 8.0.2 in `Tedd.ModuleLoader` and 8.0.x for all `Microsoft.Extensions.*` packages in `Tedd.ModuleLoader.Test.csproj`.
2. Upgrade `net9.0` packages to 9.0.22 in `Tedd.ModuleLoader`.
3. Keep `netstandard2.0` packages at 2.2.0 in `Tedd.ModuleLoader` to preserve compatibility.
4. Upgrade test dependencies (`Microsoft.NET.Test.Sdk`, `xunit`, `xunit.runner.visualstudio`) to latest stable versions in `Tedd.ModuleLoader.Test.csproj`.

## 2024-05-17 - Additional Dependency Modernization

**Observation:** The test project's cross-platform post build copy step was modernized, substituting Windows-specific `$(SolutionDir)` patterns with MSBuild native variables. Dependencies for `net8.0` and `net9.0` were safely modernized while correctly preserving `netstandard2.0`.
**Strategic Action:** Maintain backward compatibility for users by avoiding global framework upgrades and explicitly targeting package upgrades to compatible frameworks.
