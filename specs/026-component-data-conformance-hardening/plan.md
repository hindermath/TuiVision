# Implementation Plan: Component and Data Conformance Hardening

**Branch**: `026-component-data-conformance-hardening` | **Date**: 2026-07-13 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `specs/026-component-data-conformance-hardening/spec.md`

## Summary

Feature 026 closes audit findings `F010` through `F013` with four bounded,
additive framework slices. It restricts dialog completion to explicit commands,
adds hierarchical acceptance validation and a phase-aware `TInputLine` validator
bridge, introduces a closed file-dialog outcome contract with mode-specific
rejection, and extends named resources with allowlisted menu and status-line
description records alongside the existing dialog description path. All behavior
is proven test-first through production paths and reuses Feature-025 focus,
modal, lifecycle, and command contracts.

## Technical Context

**Language/Version**: C# with latest language version on .NET 10
**Primary Dependencies**: Existing `TuiVision.Core`, `TuiVision.Controls`, and `TuiVision.Serialization`; no new package or runtime dependency
**Storage**: Existing bounded binary archive and `TResourceFile`; controlled temporary filesystem metadata for file-dialog proofs
**Testing**: MSTest, Coverlet collector, DocFX, Playwright/Axe, repository shell and governance checks
**Target Platform**: macOS, Linux, and Windows/WSL through the existing cross-platform .NET and CI baseline
**Project Type**: Multi-project reusable terminal UI framework library
**Performance Goals**: Dialog validation is linear in the bounded view tree; file classification performs metadata checks only; resource parse and validation are linear in bounded record size
**Constraints**: No destructive file I/O, no arbitrary reflection or polymorphic deserialization, no historical source writes, no breaking API, no new dependency, exact case-sensitive resource keys, atomic failure
**Scale/Scope**: Four findings; Controls and Serialization production paths plus focused tests, audit/evidence, documentation, and triggered governance surfaces

## Constitution Check

*GATE: Passed before research and re-checked after design.*

- **Level-2 environment**: TuiVision uses the registered C#/.NET Level-2 row: .NET 10 build/test, DocFX plus Playwright/Axe for public docs, `docs/project-statistics.md`, and all maintained agent surfaces.
- **Memory-safe languages**: C#/.NET is the only implementation language and is on the MSL allow-list. C/C++ and Pascal remain read-only evidence.
- **Secure code generation**: New parsing and path code uses explicit bounds, ordinal identifiers, typed results, narrow exception handling, no reflection activation, and no internal mutable-state exposure.
- **Secure software architecture**: Dialog completion, validation, filesystem metadata, and persisted resources are explicit trust boundaries. Fail-safe defaults, full-before-publish validation, least capability, and separate Controls/Serialization responsibilities apply.
- **Security documentation**: STRIDE/CIA/CAPEC and security quality scenarios are recorded in `pr-evidence.md`; shared `docs/security/` files change only if implementation opens a repository-wide trigger. No S-ADR or arc42 update is planned because the design stays additive and within existing project boundaries.
- **Security standards applicability**: NIST SSDF and CWE Top 25 are applicable. OWASP Proactive Controls and Cheat Sheets inform input validation. ASVS is `N/A` without Web/Auth. Supply-chain, OpenSSF, SLSA, SBOM, VEX, AI-SBOM, Zero Trust, SAMM, BSI C3A/C5, and regulation remain trigger-based `N/A` unless actual scope changes.
- **AI-SBOM applicability**: AI is development tooling only; no model, dataset, AI service, inference infrastructure, or runtime AI component ships, so AI-SBOM is `N/A`.
- **Release / supply-chain evidence**: No dependency or distribution change is planned. Feature evidence records all `N/A` rationales and re-evaluation triggers; existing repository supply-chain evidence remains authoritative.
- **Default evidence files**: Feature-local `pr-evidence.md` is the proportional equivalent for finding and run evidence; existing `docs/security/` files are referenced rather than duplicated unless a shared fact changes.
- **Spec-Kit presets**: Security v0.6.0, Architecture v0.5.0, iSAQB v0.2.0, A11Y v0.4.0, Cross-Platform v0.2.0, Agent Parity v0.3.0, and optional Autonomous Run Governance v0.1.2 all apply as recorded in the spec.
- **Security-first**: No credential, agent state, history database, log, cache, generated DocFX output, or test result is tracked.
- **Inclusion/A11Y**: Validation rejection, focus restoration, keyboard completion, XML/API docs, text-first proof, and generated docs use WCAG 2.2 AA and `Programmierung #include<everyone>` boundaries.
- **Bilingual delivery**: Public XML and learner-facing explanatory blocks are German-first, English-second at CEFR-B2. Pure implementation identifiers remain technical English.
- **Statistics**: `docs/project-statistics.md` is updated at closeout using the Thorsten-Solo manual baseline and final `## Gesamtstatistik` convention.
- **Agent guidance parity**: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and the generated agent template are reviewed together. They change only if shared guidance changes; the Spec-Kit plan marker update is handled by the standard agent-context script.

### Post-Design Gate Re-check

The design adds no dependency, service, cloud boundary, script, dynamic type
activation, or architecture layer. Public contracts are additive, serialized
types use a closed registry and fixed bounds, filesystem behavior is metadata-
only, and every user-visible rejection has keyboard, focus, and text evidence.
All Constitution gates remain passed with no justified violation.

## Autonomous Execution Contract

**Delivery mode**: `MergeAndSync`
**Authority source**: Current user instruction to execute the recommended sequence and then run Feature 026 autonomously
**Evidence path**: `specs/026-component-data-conformance-hardening/pr-evidence.md`
**Representative vertical slice**: `F010` dialog command classification plus child acceptance validation, with Red/Green real `HandleEvent` proof, focus target, visible rejection, and historical/Free-Vision evidence
**Convergence gates**: Clarify has no material remaining question; every checklist item passes; Analyze ends with no Critical/High and disposed Medium findings; implementation ends with all tasks/evidence and triggered validations green; remote review ends with actual required checks green and no actionable thread
**Shared single-writer files**: `pr-evidence.md`, Feature-024 audit artifacts, `Pflichtenheft.md`, agent guidance, `docs/project-statistics.md`, `Directory.Build.props`, Lastenheft archive path, and closeout evidence
**Validation triggers**: Always run diff/style/secret/governance checks; run targeted and full Release plus canonical coverage for executable shared logic; run DocFX, Playwright/Axe, and text-first review for public API/XML/guide changes; map cross-platform evidence to actual workflow/job/runner semantics
**Scope firewall**: Runtime, design, parity, application, destructive-file, format-compatibility, or proof discoveries outside `F010`–`F013` become `FollowUpHardening`; a breaking or destructive product decision becomes `ProductDecision` and stops that change
**Remote closeout**: Validate the exact staged candidate, align `1.26.<patch>.<build>`, commit/push, create PR, map required checks to actual scopes, resolve review threads, use the authorized narrow Human-Approval bypass only after technical gates are green, merge, delete branch, and prove clean synchronized local `main`

Create `pr-evidence.md` from the autonomous evidence template before the first
implementation edit. Increment the manual build counter before every explicit
`dotnet build` or `dotnet test`; batched commands may contain only one such
invocation. Before commit/push, align version fields without incrementing unless
another build/test is run.

## Technical Design

### Slice A - Dialog completion and hierarchical validation (`F010`)

- Add a small `TValidationPhase` and immutable `TValidationResult` contract in
  Controls. `TView` accepts by default; `TGroup` validates a stable snapshot in
  owner order and returns the first rejection target.
- `TDialog.IsCompletionCommand` is a protected virtual classifier whose default
  set is `cmOK`, `cmCancel`, `cmYes`, and `cmNo`. Only classified commands are
  consumed and routed to `CloseDialog`.
- `TDialog.Valid` bypasses content validation only for `cmCancel`; every other
  completion validates the child tree. The first rejected target is focused
  through the existing Feature-025 focus path and exposes its validation text.
- Existing derived `Valid` overrides remain source-compatible. The classifier
  is the explicit extension point for additional domain completion commands.

### Slice B - Phase-aware input validation (`F011`)

- Keep `TValidator.IsValid(string)` as the compatibility contract and add a
  virtual phase-aware method that defaults to permissive edit checks and final
  `IsValid` checks for focus loss and acceptance.
- `TInputLine` receives an optional validator, an observable last result, and a
  state snapshot boundary. Focus loss uses `CanReleaseFocus`; dialog acceptance
  uses hierarchical validation; edit validation evaluates a candidate before
  committing it when a validator opts into edit rejection.
- `TInputLine` exposes a bounded selection range through read-only start/end
  positions and an explicit range setter. A normal cursor move collapses the
  range; an edit replaces the selected range before applying insert/overwrite.
- Rejection preserves text, cursor, viewport, insert mode, and the complete
  non-empty or collapsed selection range. It provides a text message and never
  mutates focus before the existing focus-veto result is known.

### Slice C - Mode-aware file outcomes (`F012`)

- Preserve `TFileDecisionResult`, `LastDecision`, and existing confirmation APIs
  as compatibility projections, adding only a `Rejected` enum outcome so an
  existing confirmation API never returns stale success. Add a new closed `TFileDialogOutcome` contract
  for navigation, filter, accepted Open, accepted Save, caller overwrite
  decision, rejection, and cancel.
- A single classifier uses `TFileDialogMode`, resolved path, metadata snapshot,
  and wildcard state. Missing Open targets, directory/file mismatches, invalid
  paths, and missing Save parents reject without history commit or dialog close.
- Save never writes. An existing target yields a caller-decision outcome; a new
  target yields accepted Save. Navigation and filter changes remain observable
  but are not successful file operations.

### Slice D - Safe named UI composition (`F013`)

- Reuse `TResourceFile`, `TRecordRegistry`, exact keys, payload isolation, and
  existing `TDialogDescriptionRecord` rather than creating a second resource
  container.
- Add immutable menu and status-line description models in Controls and
  dependency-free persistable records in Serialization. Adapters validate and
  convert between layers; factories create existing `TMenuBar`, `TMenuItem`,
  `TStatusDef`, `TStatusItem`, and `TStatusLine` objects.
- Register dialog, menu, and status-line records through
  `TResourceFile.RegisterBuiltInTypes`; custom callers still receive only the
  explicit registry entries they choose to add.
- Menu records use stable IDs and parent IDs rather than serialized runtime
  pointers. Validation requires unique IDs, valid parents, one bounded acyclic
  forest, non-empty labels, and nonzero commands except separators. Status
  records use ordered context definitions and items with valid ranges/commands.
- Serialization limits are 4,096 resource entries, 4 MiB per payload, 4,096
  description nodes/items, and maximum menu depth 16. Unsupported version,
  unknown registered type, truncation, trailing bytes, duplicate key, invalid
  command/reference, cycle, or limit breach fails before publication.
- `TResourceFile.Load` builds an isolated candidate and returns it only after
  complete stream consumption; no existing resource collection is mutated.
- Persisted record loaders enforce version, bounds, IDs, references, commands,
  cycles, ranges, and depth before a record enters the candidate catalog.
  Controls validators enforce the same invariants for descriptions constructed
  in memory before any factory creates a runtime view. Adapter parity tests keep
  both trust boundaries synchronized without reversing project references.

## Project Structure

### Documentation (this feature)

```text
specs/026-component-data-conformance-hardening/
├── spec.md
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── pr-evidence.md
├── contracts/
│   └── component-data-conformance-acceptance.md
└── checklists/
    ├── requirements.md
    ├── proof-governance.md
    ├── security-a11y.md
    ├── plan-quality.md
    └── plan-review.md
```

### Source Code (repository root)

```text
src/TuiVision.Controls/
├── TView.cs
├── TGroup.cs
├── TDialog.cs
├── TValidationPhase.cs
├── TValidationResult.cs
├── TValidator.cs
├── TInputLine.cs
├── TFileDialog.cs
├── TFileDialogOutcome.cs
├── MenuDescription.cs
├── MenuDescriptionValidator.cs
├── MenuDescriptionFactory.cs
├── StatusLineDescription.cs
├── StatusLineDescriptionValidator.cs
├── StatusLineDescriptionFactory.cs
├── UiDescriptionPersistenceAdapter.cs
├── TDialogDescriptionPersistenceAdapter.cs
└── existing menu/status/dialog/file controls

src/TuiVision.Serialization/
├── TResourceFile.cs
├── TRecordRegistry.cs
├── TDialogDescriptionRecord.cs
├── TMenuDescriptionRecord.cs
└── TStatusLineDescriptionRecord.cs

tests/TuiVision.Controls.Tests/
├── TDialogTests.cs
├── TInputLineTests.cs
├── TFileDialogTests.cs
├── MenuDescriptionTests.cs
├── StatusLineDescriptionTests.cs
└── ComponentDataConformanceTests.cs

tests/TuiVision.Serialization.Tests/
├── TResourceFileTests.cs
└── TUiDescriptionRecordTests.cs
```

**Structure Decision**: Extend the existing Controls and Serialization
projects. Controls owns runtime models, validation, and factories;
Serialization owns dependency-free allowlisted records and binary boundaries.
Adapters in Controls bridge them without reversing project references.

## Validation Strategy

1. Capture the existing failing behavior for each finding in tests without
   changing production code, then record command, failure, and boundary.
2. Complete Slice A before spreading the validation pattern. Run targeted
   Controls tests after one build-counter increment.
3. Complete Slices B and C with the same real dialog/focus path and controlled
   temporary directories.
4. Complete Slice D with Controls reconstruction plus Serialization roundtrip
   and the full malformed-input matrix.
5. Run `git diff --check`, exact-candidate checks, agent/preset parity, gitleaks,
   formatting, full Release tests, canonical coverage, DocFX, Playwright/Axe,
   and text-first Markdown/HTML review. Cross-platform acceptance comes from
   the actual Ubuntu/macOS/Windows jobs mapped in run evidence.
6. Reconcile Feature-024 findings only after Green proof, archive the intake,
   update ordering/statistics/evidence, and verify the final diff firewall.

## Complexity Tracking

No Constitution violation requires an exception. The additive public contracts
are necessary to close accepted reusable-framework findings and avoid local
Wave-application workarounds.
