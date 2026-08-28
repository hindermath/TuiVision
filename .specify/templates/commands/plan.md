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

<!-- source-reference-policy:begin -->
6. Apply the source-reference policy when history or modernization is material:
   current TuiVision contracts are normative; inspect `magiblot/tvision` first
   at commit `57b6f56b38e0ee75240a80a10ee0e11470c24693`, tree
   `96dd03873955689ff0a79f6c8107a8148fe1ebd6`, as a non-normative modern
   design reference; then inspect historical and material consumer sources.
   Record exactly `AdoptModernization`, `PreserveHistoricalIntent`,
   `IntentionalTuiVisionDeviation`, or `N/A`. Source rank alone never resolves
   conflicts. Keep external sources uncopied and record
   `MultipartNotRepositoryWideMIT`. Apply this `Prospective`; re-evaluate only
   for a changed contract, new approved pin, or materially new consumer
   evidence. Moving branches are not evidence.
<!-- source-reference-policy:end -->

## Validation Checklist

- No gate is left unresolved without rationale.
- Test, coverage, dependency, and documentation impacts are planned before implementation.
- Architecture evidence and justified `N/A` decisions are planned before implementation.
- Evidence exists before implementation and each iterative stage has a measurable stop condition.
