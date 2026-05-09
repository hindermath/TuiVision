# Research: Interactive Wave 2 Demos

**Feature**: `012-interactive-wave2-demos`
**Spec**: [spec.md](/Users/thorstenhindermann/RiderProjects/TuiVision/specs/012-interactive-wave2-demos/spec.md)
**Date**: 2026-05-09

## Decision 1: Use `examples/Demo` as the first vertical slice

**Decision**: Implement the first complete interactive path in `examples/Demo`, then reuse the proven pattern for the other ten examples.

**Rationale**: `Demo` covers the widest Wave-2 surface: broad controls, standard dialogs, file/path metadata, cancel/invalid decisions, color/display selection, and visible status updates. If the runtime menu/status/event pattern works there, the remaining examples can follow a smaller variant without inventing separate mechanics for each project.

**Alternatives considered**:
- Start with the smallest example. Rejected because it would not prove dialog, file/path, and multi-command runtime behavior.
- Implement all examples independently. Rejected because it would create avoidable duplication and inconsistent smoke paths.

## Decision 2: Each example gets visible command-driven operation paths

**Decision**: Every Wave-2 example must expose primary behavior through a menu, keyboard, status, or command path that updates visible text/state in the running application.

**Rationale**: The purpose of 012 is to move beyond 011's direct function proof. A user running `dotnet run --project examples/<Example>` must see what the example is for and must be able to trigger meaningful behavior without reading test code.

**Alternatives considered**:
- Keep direct methods and print only a summary. Rejected because it does not demonstrate the framework interactively.
- Add only guide documentation. Rejected because the runtime application would still appear incomplete.

## Decision 3: Keep shared helpers small and example-focused

**Decision**: Use small source-level helpers only where they reduce repeated command/status/event wiring. Prefer example-local code for behavior that is historically specific to one demo. Do not add a new framework package or public framework API unless implementation proves it unavoidable.

**Rationale**: The feature is about demos, not a framework redesign. Shared helper code is useful for consistent command labels, status updates, and smoke-event injection, but a new abstraction would need architecture evidence and would increase maintenance cost.

**Alternatives considered**:
- Add a reusable interactive-demo framework layer. Rejected for this feature because the existing controls can already host menus/status/desktop feedback.
- Copy all wiring independently. Rejected if it produces repeated, inconsistent command definitions and event helpers.

## Decision 4: Primary smoke tests must drive the application loop

**Decision**: Smoke tests must execute `app.Run()` or the equivalent real application loop with injected `TEvent`, command, or key events. Existing direct methods may remain for setup, state inspection, or supplemental assertions only.

**Rationale**: The clarified spec requires proof through the visible app route. This catches missing menu/status wiring, broken command dispatch, focus issues, and quit-path errors that direct helper calls cannot detect.

**Alternatives considered**:
- Continue testing direct methods only. Rejected by the spec and by the user-visible problem that examples start as basic apps.
- Launch external processes for every smoke path. Rejected because the current smoke infrastructure is in-process, deterministic, and faster.

## Decision 5: File/path and dialog-designer demonstrations stay read-only

**Decision**: `Demo` and `DlgDsn` use source-controlled fixtures, repository metadata, or test temporary directories. They must not read arbitrary user file contents as proof and must not persist user data during normal operation.

**Rationale**: The examples should demonstrate dialog behavior, validation, metadata, and rendering without adding privacy, safety, or cleanup concerns. This also keeps smokes deterministic.

**Alternatives considered**:
- Use the user's current working directory as live content. Rejected because it is non-deterministic and could expose arbitrary user data.
- Write persistent example state. Rejected because no storage requirement exists for this feature.

## Decision 6: Documentation must explain the visible runtime path

**Decision**: Update the affected guide pages, `examples/README.md`, and `specs/012-interactive-wave2-demos/pr-evidence.md` so each example's runtime operation path, validation command, and intentional omissions are reviewable.

**Rationale**: The examples are educational artifacts. Users and reviewers need to know what a running example is expected to show and how the smoke path proves it.

**Alternatives considered**:
- Keep documentation unchanged because the code is self-explanatory. Rejected because learners may run the examples without reading the implementation.
- Put all evidence only in the PR description. Rejected because repository-local evidence must remain after the PR is merged.

## Decision 7: Accessibility is text-first and keyboard-first

**Decision**: Runtime paths must be operable by keyboard/command injection and must expose feedback as text-readable UI state. Guide and DocFX validation should use the existing text-first and Playwright/axe proof path when documentation content changes.

**Rationale**: TuiVision's accessibility policy applies to examples, documentation, and generated HTML. Visible operation must not rely only on color, pointer-only gestures, or layout that cannot be read by assistive tooling.

**Alternatives considered**:
- Treat terminal examples as outside A11Y scope. Rejected by repository governance.
- Add mouse-only proof. Rejected because keyboard and command paths are required.

## Decision 8: Security and governance evidence stays proportional

**Decision**: Record NIST SSDF/CWE review posture and confirm that ASVS, CAPEC, Zero Trust, SBOM, and VEX do not gain new obligations unless implementation adds a web/API/auth surface, threat boundary, dependency, or release artifact.

**Rationale**: The feature changes local example behavior, not external trust boundaries. The evidence should be explicit but not inflated.

**Alternatives considered**:
- Skip security evidence because examples are local. Rejected because repository governance requires the review statement.
- Add heavyweight threat modeling. Rejected unless implementation introduces new external input boundaries.

## Decision 9: Validation includes examples, full suite, coverage, format, and docs

**Decision**: Completion evidence should include `dotnet test tests/TuiVision.Examples.SmokeTests/`, full `dotnet test`, coverage gate with `coverlet.runsettings`, `dotnet format --verify-no-changes`, and DocFX plus `tests/web-a11y` smoke validation when documentation changes are included.

**Rationale**: The spec requires example smokes and full tests; repository governance also requires the coverage gate and style/docs evidence before merge.

**Alternatives considered**:
- Run only example smoke tests. Rejected because shared framework/runtime behavior may regress.
- Skip DocFX validation after guide changes. Rejected by the repository DocFX/A11Y policy.

## Decision 10: Do not start later example waves

**Decision**: 012 remains limited to the Wave-2 examples already delivered by 011.

**Rationale**: Starting Wave 3 or Wave 4 would blur acceptance, expand the validation matrix, and weaken the intended staged Spec-Kit pattern: first functional proof, then interactive demo polish.

**Alternatives considered**:
- Combine interactive polish with the next example wave. Rejected because the current examples need to become usable before additional examples are added.

## Decision 11: Compare every example with its historical source before wiring interactivity

**Decision**: The later task list must include an explicit read-only source review for each Wave-2 example against the relevant `.c`/`.cc` files and any important matching headers under `tv203s/`. The review records the original demo intent, the interaction path that should be visible in the C# example, and any intentional deviation that belongs in guide or PR evidence.

**Rationale**: The 011 implementation created the functional base. The 012 implementation must make that base educationally useful. Checking the historical sources keeps the visible runtime behavior tied to the original Turbo Vision examples without forcing a mechanical translation.

**Alternatives considered**:
- Rely only on the 011 C# helper methods. Rejected because those methods may prove behavior but not necessarily the original demo flow.
- Translate historical C/C++ code line by line. Rejected because TuiVision is a modern C# port and should use the existing managed framework abstractions.
