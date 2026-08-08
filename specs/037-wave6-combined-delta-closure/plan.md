# Implementation Plan: Wave-6 Combined Delta Closure

**Branch**: `037-wave6-combined-delta-closure`
**Date**: 2026-08-08
**Spec**: `specs/037-wave6-combined-delta-closure/spec.md`
**Input**: `requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md`
**Delivery mode**: `MergeAndSync` (explicitly renewed after the T128 hard gate)

## Summary

Feature 037 is an independent, read-only audit of the product union delivered
by Feature 035 and Feature 036. It records exact Git and content provenance,
joins ten functional areas to ten showcase areas, validates all 24 historical
TVFM sources, and decides whether the local candidate is ready for later
delivery. A deterministic MSTest validator rejects malformed or incomplete
closure data. No runtime, API, dependency, project, example, or historical
source changes are allowed.

## Technical Context

| Concern | Decision |
|---|---|
| Language/runtime | C# on .NET 10 for the test-only validator; Markdown and JSON for evidence |
| Existing test stack | MSTest in `TuiVision.Examples.SmokeTests` |
| Storage | Source-controlled JSON and Markdown only; no database or service |
| Product surface | None; predecessor example code is read-only |
| Historical scope | Exactly 24 files directly under `TVFM/`; relevant `tv203s/` files are contextual read-only references |
| Functional scope | Exactly `W6-001` through `W6-010` from Feature 035 |
| Showcase scope | Exactly `W6S-001` through `W6S-010` from Feature 036 |
| Entry point | Exactly `Tp7FileManager` |
| Delivery | Exact candidate commit, push, PR, provider/review convergence, merge commit, causal closeout when required, and clean synchronized `main` |

## Constitution Check

| Principle | Plan evidence | Status |
|---|---|---|
| Clear requirement artifacts | Binding intake, clarified spec, closed vocabularies and cardinalities | Pass |
| Historical intent, modern C# | TVFM and relevant `tv203s/` are read-only; style differences alone are not findings | Pass |
| Testable closure | Test-first deterministic validator plus negative mutations | Pass |
| Text-first accessibility | German-first/English-second CEFR-B2 Markdown and semantic tables | Pass |
| Security and evidence integrity | Exact commits, hashes, controlled paths, fail-closed validation and secret scan | Pass |
| Cross-platform honesty | CRLF-neutral text hashes; provider-only gates are not claimed locally | Pass |
| Coverage | Full canonical five-assembly gate remains mandatory | Pass |
| Versioning | `1.37.0.<build>`; increment once before each explicit build/test command | Pass |

No constitution exception is required.

## Scope Firewall

Allowed writes are limited to:

- `.specify/feature.json`;
- `Directory.Build.props` for required branch/build versioning;
- `specs/037-wave6-combined-delta-closure/`;
- one test-only validator in `tests/TuiVision.Examples.SmokeTests/`.
- the narrowly authorized requirements-intake alignment validator and its
  test fixtures;
- causal post-merge intake/status/evidence surfaces only after the reviewed
  feature head is merged.

Explicitly forbidden writes include `src/`, `examples/`, project/package files,
`TVFM/`, `TVDEMOS/`, `tv203s/`, generated DocFX output and product-facing
agent guidance. Requirement-series state may change only in the causal
closeout that records the actual Feature-037 merge and unlocks, but does not
start, the portfolio audit.

## Provenance Model

The authoritative PR roles are:

| PR | Role | Base | Head | Merge |
|---:|---|---|---|---|
| #101 | `FunctionalProduct` | `4b32762dfc60e18655de35d816ff1d4ede0185eb` | `207e807ee8835779b9b8641f91868a6a5e80f938` | `52f77facc518e3084f897148b44ec19e62b3dde6` |
| #102 | `FunctionalCloseout` | `52f77facc518e3084f897148b44ec19e62b3dde6` | `e6d5b07ef91ac8770ab03a1c4b9830a17bf334ad` | `b0d99052b66f3f575f8343fa291761ec3f65779d` |
| #103 | `PromptMetadata` | `b0d99052b66f3f575f8343fa291761ec3f65779d` | `3d1ee66b6eb7c54e8663430a57945d47a8d63845` | `42a842fb63a0695a618a0f87ffec543e9bc3b6c8` |
| #104 | `ShowcaseProduct` | `42a842fb63a0695a618a0f87ffec543e9bc3b6c8` | `a0d506297c101104fd0e15911a7d21e1c5a21caa` | `559bffbfbb94699a33cfe1ad8b01d5ac9b86641d` |
| #105 | `ShowcaseCloseout` | `559bffbfbb94699a33cfe1ad8b01d5ac9b86641d` | `50a8f4dfab64de6a042555f8e304f01ac6b8596f` | `371af97ff1741313ab808c87c2827655073cff2c` |

The product delta is only the union of declared product paths from #101 and
#104. Closeout and prompt metadata remain provenance inputs, not product code.

## Combined Review Slices

| Combined ID | Functional | Showcase | Scope |
|---|---|---|---|
| `W6C-001` | `W6-001` | `W6S-009` | app, menu, status and help shell |
| `W6C-002` | `W6-002` | `W6S-001` | controlled navigation and list |
| `W6C-003` | `W6-003` | `W6S-003` | filter, sort, tags and metadata |
| `W6C-004` | `W6-004` | `W6S-002` | bounded text and hex preview |
| `W6C-005` | `W6-005` | `W6S-004` | controlled search and cancellation |
| `W6C-006` | `W6-006` | `W6S-006` | confirmed file operations and dialogs |
| `W6C-007` | `W6-007` | `W6S-007` | drag intent with keyboard parity |
| `W6C-008` | `W6-008` | `W6S-005` | internal association and viewer choice |
| `W6C-009` | `W6-009` | `W6S-010` | progress, failure, cancellation, recovery and layout |
| `W6C-010` | `W6-010` | `W6S-008` | palettes, resources and configuration |

Each functional and showcase ID appears in exactly one combined row. Reciprocal
links and each final primary decision are validated explicitly.

## Implementation Strategy

### Phase 1 - Evidence foundation

Create `pr-evidence.md`, the autonomous gate contract and an empty but
schema-valid closure dataset before adding the validator. Record local
authority, protected paths and current source hashes.

### Phase 2 - Test-first vertical slice

Add the test-only validator with one deliberately incomplete combined row and
run its focused test to prove an expected failure. Complete `W6C-001`, rerun
the focused proof, then extend the same schema to all remaining rows.

### Phase 3 - Complete closure data

Populate exact PR pins, predecessor hashes, 24 source records, ten functional
proofs, ten showcase proofs, ten combined rows, one entry point, governance
rows and validation placeholders. Review example-local code for duplicated
framework behavior without changing it.

### Phase 4 - Validate

Run positive and malformed-data closure tests, targeted Wave-6 tests, bounded
normal and `--smoke` entry-point checks, full Release tests, coverage, format,
DocFX/Axe, secrets, supply chain, agent parity, text and scope checks. Increase
the build counter before every explicit `dotnet build` or `dotnet test`.

### Phase 5 - Local closeout

If and only if no finding or product decision exists, mark the local feature
candidate `ReadyForDelivery`. Do not assert an actual merge, Wave-6 `Closed`,
or portfolio `Eligible`; those are causal delivery facts outside current
authority. Complete tasks and retrospective with terminal local state.

## Test Design

The validator must reject at least:

- wrong PR pin, count or file-set hash;
- changed predecessor or TVFM hash;
- missing, duplicate or unknown source/area/entry relation;
- unknown decision or dimension;
- accepted row containing `Gap`;
- finding without stable ID, evidence, owner or trigger;
- product decision combined with ready/closed state;
- missing app-loop, view, focus, status, Description, keyboard or cell proof;
- non-reciprocal source and area links;
- LF/CRLF-dependent text hashing;
- local evidence that falsely claims provider or post-merge success.

## Governance Applicability

Security, architecture, iSAQB, A11Y, cross-platform, agent parity, intake and
autonomous governance are applicable to evidence integrity and orchestration.
Web ASVS, SBOM/VEX/SLSA release evidence, runtime AI, NIS2/CRA/EU AI Act/DORA,
cloud/identity/Zero-Trust, BSI C3A/C5, script parity and template distribution
remain `N/A` unless their trigger enters the actual diff. Every `N/A` row
includes a rationale and re-evaluation trigger.

## Documentation Impact

Feature-specific Markdown and JSON evidence changes: `Update`. Public API/XML,
DocFX navigation, learner guides, agent guidance and templates: `NoChange`.
DocFX/Axe still run because the binding intake requires a full closure gate,
not because XML/API documentation changed.

## Stop Conditions

Stop immediately on provenance drift, cardinality mismatch, a reproducible
`CandidateFinding`, any `ProductDecision`, protected-path modification, or a
mandatory local gate failure. Do not remediate product behavior in this feature.
