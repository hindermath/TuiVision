# Command Template: `/speckit.plan`

Use this command to produce an implementation plan from an approved specification.

## Required Actions

1. Populate technical context with real stack details.
2. Execute the Constitution Check gates explicitly:
   - branching and PR flow
   - .NET 10 + C# 14.0 toolchain alignment
   - architecture/layer boundaries
   - bilingual CEFR B2 documentation scope
   - XML documentation + DocFX regeneration scope
   - Red-Green-Refactor testing scope
   - coverage gate (`>=70%` minimum, `>=80%` target)
   - NuGet dependency currency and pinning exceptions
   - serialization/data conventions
3. Document concrete project structure for this feature.
4. Record justified exceptions in Complexity Tracking.

## Validation Checklist

- No gate is left unresolved without rationale.
- Test, coverage, dependency, and documentation impacts are planned before implementation.
