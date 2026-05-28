# Research: Wave 2 Visual Component Remediation

**Feature**: `013-wave2-visual-component-remediation`
**Spec**: [spec.md](./spec.md)
**Date**: 2026-05-22

## Decision 1: Treat visible UI composition as the primary parity proof

**Decision**: The primary acceptance proof for each example is a real visible TuiVision component, dialog, window, view group, scroll group, progress display, input/list/combo composition, or stable visual runtime state.

**Rationale**: The clarified specification explicitly rejects text-only proof as the primary parity proof. `VisibleText`, `VisibleHistory`, and direct helper output remain useful for supporting evidence, but they do not prove the historical visual intent by themselves.

**Alternatives considered**:
- Keep 012 text-first app-loop feedback as sufficient. Rejected because it preserves the current gap.
- Require pixel-perfect historical reproduction. Rejected because TuiVision is a modern C# port and the spec allows documented intentional deviations.

## Decision 2: Use the three-layer runtime model everywhere

**Decision**: Every example uses a visible main component, a real `TStatusLine` for short dynamic feedback, and a canonical keyboard-reachable `Help -> Description` path.

**Rationale**: This preserves the useful 012 status sentences without allowing them to replace the visible component. It also gives learners, screen-reader users, and reviewers a consistent route to understand what the visual UI is demonstrating.

**Alternatives considered**:
- Put all explanation into guides only. Rejected because the explanation path must exist at runtime.
- Allow each app to choose unrelated help names. Rejected because the clarified spec chose `Help -> Description` as the canonical path.
- Use an equivalent status area by default. Rejected because the clarified spec makes a real `TStatusLine` primary; equivalent status areas require documented deviation evidence.

## Decision 3: Prove visibility with view-tree plus buffer/cell snapshots

**Decision**: Primary smoke tests combine a view-tree proof with a stable buffer/cell snapshot that contains control-specific content at the expected position or region.

**Rationale**: A view-tree assertion proves that the control or dialog exists in the runtime composition. A buffer/cell snapshot proves that the user-visible render path exposes meaningful content. The combination catches regressions that text logs or helper methods would miss.

**Alternatives considered**:
- Assert only view instances. Rejected because a control may exist but not draw useful content.
- Assert only rendered text. Rejected because text can appear outside the intended control or from a fallback status path.
- Use full terminal screenshot comparison. Rejected for now because the existing in-process smoke infrastructure can provide more stable semantic buffer assertions.

## Decision 4: Keep `Demo` as the P1 vertical slice

**Decision**: Implement and prove `Demo` first, covering the three required flow families: `Dialog/Control`, `File/Path metadata`, and `Display/Color/Gadget`.

**Rationale**: `Demo` is the broadest Wave-2 example and exercises the most framework surfaces. Proving the visible composition pattern there reduces risk before applying the same pattern to narrower examples.

**Alternatives considered**:
- Start with the smallest example. Rejected because it would not prove dialog, file/path, color/display, and gadget interactions.
- Implement all eleven independently. Rejected because it would create inconsistent proof style and avoidable duplication.

## Decision 5: Group the remaining examples by visual behavior

**Decision**: After `Demo`, implement examples in behavior families: clipboard text/input, dynamic text, input/list/combo, progress, dialog-designer, and scroll-dialog behavior.

**Rationale**: The examples have different historical purposes but similar test needs within each family. Grouping keeps task generation coherent while preserving per-example acceptance.

**Alternatives considered**:
- Treat all examples as one large task. Rejected because it hides per-example proof gaps.
- Create a separate framework feature for every missing helper. Rejected because 013 permits only the smallest necessary shared seams.

## Decision 6: Keep fixtures controlled and local

**Decision**: File/path, dialog-designer, and clipboard-adjacent proof uses source-controlled fixtures or test temporary directories. Arbitrary user files, persistent user history, external proof paths, and network dependencies are excluded.

**Rationale**: Controlled data keeps smokes deterministic, protects user privacy, and avoids adding storage or external trust boundaries.

**Alternatives considered**:
- Use the current working directory or live user files. Rejected because it is non-deterministic and can expose private data.
- Persist example history to prove behavior. Rejected because no persistence requirement exists and persistent user data would expand the feature risk.

## Decision 7: Review historical sources before accepting each example

**Decision**: Each example requires a read-only review of relevant historical `.c`/`.cc` files and important headers under `tv203s/` where declarations are needed.

**Rationale**: The feature is specifically about historical visual parity. The original sources are the best intent reference, but they must not be modified and must not force a mechanical line-by-line translation.

**Alternatives considered**:
- Rely only on current C# 012 behavior. Rejected because 012 may already have missed the visible component intent.
- Translate historical sources exactly. Rejected because the modern C# framework should express the same intent idiomatically.

## Decision 8: Keep shared seams small and documented

**Decision**: Add shared control, status, or test seams only when they are required to expose and test the visible Wave-2 composition. Larger framework gaps become documented deviations outside 013.

**Rationale**: The user requested a targeted remediation, not a broad framework revision. This keeps scope aligned with Wave 2 and avoids pulling Wave 3/4 or infrastructure redesign into the branch.

**Alternatives considered**:
- Solve every framework gap encountered. Rejected because it would break the feature boundary.
- Avoid all shared code. Rejected if it would duplicate fragile smoke or status logic across eleven examples.

## Decision 9: Record proportional governance evidence

**Decision**: NIST SSDF and CWE Top 25 remain applicable baselines. The installed `security-governance` baseline is v0.4.0. ASVS, CAPEC, Zero Trust, SBOM/VEX/SLSA changes, and AI-SBOM evidence are `N/A` unless implementation changes their trigger conditions. AI-SBOM is currently `N/A` because AI is used only as development/agent tooling and no runtime/product AI is delivered. The v0.4.0 language-specific secure-coding profiles for Rust, Go, Swift, Java/Kotlin, Python, and TypeScript/JavaScript create no new obligation for this C#/.NET feature; existing C#/.NET secure-coding and TuiVision rules continue to apply.

**Rationale**: The feature changes local terminal examples and documentation. It does not add web/API/auth, external services, AI runtime, non-C# implementation languages, new dependencies, or release artifacts.

**Alternatives considered**:
- Skip governance evidence entirely. Rejected because Level-2 governance requires explicit applicability decisions.
- Create full AI-SBOM evidence. Rejected because no delivered AI component exists.
- Re-run Specify/Clarify because of `security-governance` v0.4.0. Rejected because the feature requirements and clarifications remain unchanged; only the plan/evidence baseline needs synchronization.

## Decision 10: Validation follows the stricter completion gate

**Decision**: Formal completion evidence includes Release build, fast Example-Smoke suite, full Release test run, Coverlet coverage gate, `dotnet format --verify-no-changes`, and DocFX plus web-a11y when documentation output or navigation changes.

**Rationale**: The clarified spec and repository governance require both runtime proof and repository-wide quality gates. Documentation changes are learner-facing and therefore require the existing DocFX/A11Y path when output or navigation is affected.

**Alternatives considered**:
- Run only example smokes. Rejected because shared runtime behavior and coverage gates may regress.
- Skip DocFX/A11Y after guide changes. Rejected by repository A11Y and documentation policy when generated documentation is affected.

## Entscheidung / Decision Summary

Deutsch: Die Planung haelt 013 eng: echte sichtbare Komponenten sind der primaere Nachweis, Statuszeilen bleiben als text-first Unterstuetzung erhalten, und `Help -> Description` erklaert den sichtbaren Ablauf direkt in der App.

English: The plan keeps 013 narrow: real visible components are the primary proof, status lines remain as text-first support, and `Help -> Description` explains the visible flow inside the app.
