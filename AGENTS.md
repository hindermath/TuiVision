# AGENTS.md

## Build/Lint/Test Commands

### Build and Test
```bash
# Restore, build, and test (full validation cycle)
dotnet restore
dotnet build --configuration Release
dotnet test

# Run tests for a specific project
dotnet test tests/TuiVision.Core.Tests/

# Run a single test method
dotnet test --filter "FullyQualifiedName~TestMethodName"

# Build a specific project
dotnet build src/TuiVision.Core
```

### Linting and Formatting
```bash
# Run code analysis
dotnet build --no-restore --verbosity quiet

# Check for style issues (if configured)
dotnet format --verify-no-changes

# Regenerate docs when API/XML comments change and root config exists
docfx docfx.json
```

### Coverage Gate (SC-003)
- **Minimum**: ≥ 70 % Line Coverage in `TuiVision.Controls` (Pflichtenheft §9.4 Nr. 1)
- **Measurement**: Coverlet (`coverlet.collector` package) via `dotnet test --collect:"XPlat Code Coverage"`
- Merging into `main` without passing the coverage gate is NOT permitted.

## Code Style Guidelines

### Imports and Using Statements
- Use `using` directives at the top of files, grouped logically
- Place `using` directives inside namespace declarations when needed
- Follow C# convention for ordering: System namespaces first, then third-party, then internal

### Formatting and Naming Conventions
- Use PascalCase for all identifiers (classes, methods, properties, etc.)
- Use camelCase for local variables and parameters
- Use `var` for implicit typing when the type is obvious
- Prefer explicit type declarations for readability where needed
- Use `readonly record struct` for immutable value types
- Use `sealed class` for classes not meant to be inherited
- Use `public` keyword explicitly when needed (C# default is internal)
- Use `private` keyword for private members when needed for clarity

### Types and Structs
- Use `readonly record struct` for immutable value types (like TPoint, TRect)
- Use records for value objects that carry data
- Use classes for reference types that need inheritance or mutability
- Use enums for sets of named constants
- Use `enum with Flags` for bitwise combinations

### Error Handling
- Use exceptions for exceptional conditions
- Don't catch exceptions unless you can handle them meaningfully
- Provide meaningful exception messages
- Use `ArgumentNullException` for null parameter validation
- Consider using `ArgumentNullException.ThrowIfNull()` in .NET 6+

### Code Organization
- Group related methods and properties within classes
- Use partial classes for large files when appropriate
- Keep methods small and focused on single responsibilities
- Use meaningful names that describe what the code does
- Avoid magic numbers - use constants or enums instead
- Prefer composition over inheritance where possible
- Use interfaces for contracts and abstractions
- Use `static` for utility methods that don't depend on instance state
- Use `sealed` classes when inheritance is not intended

### Documentation Guidelines
- Explanatory documentation blocks must be bilingual: German first, English second
- German and English documentation should target CEFR-B2 readability
- Public APIs must include complete XML documentation (`summary`, `param`, `returns`, `exception` where needed)
- Update documentation in the same change when API signatures or XML comments change

## Testing Guidelines
- All test files should be in `tests/` directory with corresponding project structure
- Use MSTest attributes for test methods: `[TestMethod]` for tests
- Use descriptive test method names starting with `Test_`
- Organize tests into classes that match the naming of the tested class
- Use `[TestClass]` attribute for test classes
- Tests should be independent and not rely on test execution order
- Use `Assert.AreEqual()` for equality checks
- Use `Assert.IsTrue()` or `Assert.IsFalse()` for boolean assertions
- Use `Assert.ThrowsException<T>()` for expected exception testing
- Test edge cases including boundary conditions and null inputs

## CI/CD Configuration
- Repository uses GitHub Actions for CI
- Builds and tests on Ubuntu and macOS runners
- Uses .NET 10 SDK
- Tests are run using `dotnet test` command with Release configuration
- Build and test validation is mandatory for all code changes

## Active Feature Context

### 002-application-framework
- Current planning target: implement the application shell increment from `specs/002-application-framework/spec.md` and `specs/002-application-framework/plan.md`
- Scope is limited to shell infrastructure in `src/TuiVision.Controls`: `TProgram`, `TApplication`, `TDesktop`, `TMenuBar`, `TStatusLine`, lightweight action items, and shared shell command identifiers
- Out of scope for this increment: concrete dialogs, control widgets, and specialized window types
- `TApplication` must auto-create a default shell with menu bar, desktop workspace, and status line
- Unavailable global actions must remain visible in menu and status line, but be disabled
- Reuse existing `TView`/`TGroup` ownership, focus, and event-dispatch behavior instead of introducing a new shell module
- Add focused MSTest coverage in `tests/TuiVision.Controls.Tests/` for default shell creation, shared command routing, disabled-action visibility, and desktop focus recovery
- Follow visible Red-Green-Refactor TDD sequencing for this feature before production code is added

## Agent File Synchronization Policy

- When active feature context, implementation plans, or project-wide agent guidance changes, the following AI-agent files MUST be reviewed and updated together in the same work item if they are affected:
  - `AGENTS.md`
  - `CLAUDE.md`
  - `GEMINI.md`
  - `.github/copilot-instructions.md`
- Partial synchronization is not acceptable when shared guidance has changed.
- If one file intentionally diverges for agent-specific reasons, that divergence MUST be explicit and documented in the same change.

## Active Technologies
- C# latest (C# 14) / .NET 10 (`net10.0`) + `TuiVision.Core` (TView, TGroup, TEvent, TObject, TPoint, TRect, (003-dialog-control-layer)
- N/A — in-memory UI state only; keine Persistenz in Phase 5 (003-dialog-control-layer)

## Recent Changes
- 003-dialog-control-layer: Added C# latest (C# 14) / .NET 10 (`net10.0`) + `TuiVision.Core` (TView, TGroup, TEvent, TObject, TPoint, TRect,
