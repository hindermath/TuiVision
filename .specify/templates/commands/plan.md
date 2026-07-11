# Command Template: `/speckit.plan`

Use this command to produce an implementation plan from an approved specification.

## Required Actions

1. Populate technical context with real stack details.
2. Execute the Constitution Check gates explicitly:
   - branching and PR flow
   - .NET 10 + C# 14.0 toolchain alignment
   - architecture/layer boundaries
   - iSAQB/arc42 architecture evidence under `docs/architecture/`
   - bilingual CEFR B2 documentation scope
   - XML documentation + DocFX regeneration scope
   - Red-Green-Refactor testing scope
   - coverage gate (`>=70%` minimum, `>=80%` target)
   - NuGet dependency currency and pinning exceptions
   - serialization/data conventions
3. Document concrete project structure for this feature.
4. Record justified exceptions in Complexity Tracking.
5. Complete the Autonomous Execution Contract with delivery authority,
   evidence-first setup, one representative vertical slice, convergence gates,
   shared single-writer files, trigger-based validation, scope firewall, and
   remote closeout when delegated.

## Validation Checklist

- No gate is left unresolved without rationale.
- Test, coverage, dependency, and documentation impacts are planned before implementation.
- Architecture evidence and justified `N/A` decisions are planned before implementation.
- Evidence exists before implementation and each iterative stage has a measurable stop condition.
