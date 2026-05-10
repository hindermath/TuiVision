# Contract: Interactive Wave 2 Demo Acceptance

**Feature**: `012-interactive-wave2-demos`
**Date**: 2026-05-09

This contract defines the observable runtime, smoke-test, documentation, and evidence obligations for the Wave-2 interactive demo feature.

## 1. Common Runtime Contract

Each Wave-2 example MUST:

- start as a meaningful interactive demo when invoked with `dotnet run --project examples/<Example>`;
- show first-screen text or controls that identify the demonstrated behavior;
- expose at least one primary operation through menu, keyboard, status, or command routing;
- update visible feedback after each primary operation;
- provide a deterministic quit path for automated smoke tests;
- remain keyboard/command operable without requiring mouse-only interaction;
- keep runtime state in memory unless the spec explicitly allows a source-controlled fixture or test temporary path.

An example MUST NOT be considered interactive if:

- the demonstrated behavior is reachable only by a direct test helper method;
- the runtime shows only an empty or generic base application;
- the visible output is only a precomputed string unrelated to command dispatch;
- the smoke test bypasses the application loop for the primary path.

## 2. Smoke Event Contract

Primary smoke tests MUST:

- construct the example in a deterministic test mode where necessary;
- execute `app.Run()` or the equivalent real application loop;
- inject `TEvent`, command, or key events through the same dispatch path used by the runtime app;
- assert visible feedback state after the event sequence;
- include a deterministic quit event;
- use direct helper methods only for setup or supplemental assertions.

Primary smoke tests SHOULD:

- reuse common event-script helper code where this improves consistency;
- keep assertions text-first and stable;
- avoid wall-clock sleeps, external process timing, or dependence on terminal dimensions beyond fixed test bounds.

## 3. Per-Example Runtime Contract

Before implementing or accepting each per-example runtime path, the relevant historical `.c`/`.cc` source files and any important matching headers under `tv203s/` MUST be reviewed as read-only reference. The implementation MUST either reflect the original demo intent in the visible C# interaction or document the intentional deviation in guide or PR evidence.

### Clipboard

MUST demonstrate copy, cut, paste, and unavailable-clipboard feedback through visible command paths.

### Demo

MUST be the P1 vertical slice and demonstrate at least three visible behaviors across broad controls/dialogs, standard file/path metadata, cancel/invalid handling, and color/display selection.

### DlgDsn

MUST load/render a source-controlled dialog description, allow at least one visible runtime change, and visibly reject malformed or invalid descriptions.

### DynTxt

MUST demonstrate short, long, and constrained-width dynamic text states through visible runtime operations.

### InpLis

MUST demonstrate editable input, list selection changes, any history or recall behavior that exists in the 011 proof baseline, and boundary/empty feedback.

### ListVi

MUST demonstrate list selection, boundary navigation, and empty or unavailable-state feedback.

### ProgBa

MUST demonstrate a visible progress path that reaches completion.

### Sdlg

MUST demonstrate scrolling or focus movement to content outside the initial viewport on the primary axis.

### Sdlg2

MUST demonstrate scrolling or focus movement outside the initial viewport on both horizontal and vertical axes.

### TCombo

MUST demonstrate combo selection, visible value change, and boundary or empty-state feedback.

### TProgB

MUST demonstrate partial progress, abort, and cancelled states as separate visible outcomes.

## 4. Read-Only File and Fixture Contract

File/path and dialog-designer examples MUST:

- use source-controlled fixtures, fixed repository paths, or test temporary directories;
- demonstrate metadata, validation, rendering, or rejection rather than arbitrary user content reads;
- avoid persistent user-data writes during normal operation;
- clean up test-created temporary paths;
- show visible invalid/cancel/unavailable feedback when a path cannot be used.

## 5. Documentation and Evidence Contract

Implementation MUST update or confirm:

- a per-example historical source review against the relevant read-only `.c`/`.cc` files and any important matching headers under `tv203s/`;
- affected guide pages under `docs/guides/examples/` with German-first/English-second runtime instructions;
- `examples/README.md` when the visible operation model changes;
- `specs/012-interactive-wave2-demos/pr-evidence.md` with per-example smoke/evidence status;
- architecture evidence under `docs/architecture/` for runtime behavior impact;
- security evidence under `docs/security/` or a clear unchanged statement for NIST SSDF/CWE posture;
- `docs/project-statistics.md` after the completed implementation phase;
- `Pflichtenheft.md` next-step and feature-progress markers if this feature changes prioritized work status.
- a clear unchanged or N/A rationale for any expected proof surface that does not need content changes.

Generated `_site/`, generated `api/*.yml`, and transient DocFX files MUST NOT be committed.

## 6. Validation Contract

The feature is complete only after the implementation records evidence for:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/
dotnet test
dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
docfx docfx.json
cd tests/web-a11y && npm run test:docfx
```

Because this feature requires guide/documentation updates, DocFX and web A11Y validation are expected. If a validation command cannot be run in the local environment, the reason and equivalent CI/manual evidence MUST be recorded in `pr-evidence.md`.

## 7. Out-of-Scope Contract

The feature MUST NOT:

- start Wave 3 or Wave 4 example implementation;
- introduce mandatory mouse-only operation;
- add unrelated framework redesign;
- modify historical reference files under `tv203s/`;
- introduce a database, external service, or persistent user history;
- change the DocFX publishing model or commit generated DocFX output.
