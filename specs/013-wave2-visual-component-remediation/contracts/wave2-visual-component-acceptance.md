# Contract: Wave 2 Visual Component Acceptance

**Feature**: `013-wave2-visual-component-remediation`
**Date**: 2026-05-22

This contract defines the observable runtime, smoke-test, documentation, and governance obligations for the Wave-2 visual-component remediation.

## 1. Common Runtime Contract

Each scoped Wave-2 example MUST:

- start as a meaningful terminal UI demo through `dotnet run --project` for each scoped example project under `examples/`;
- show a visible main component or stable visual runtime state that represents the historical visual idea;
- include a real `TStatusLine` for short dynamic feedback unless an equivalent status area is documented as a deviation;
- provide the canonical `Help -> Description` runtime path;
- remain keyboard/command operable without requiring mouse-only input;
- use in-memory runtime state except for source-controlled fixtures or test temporary directories allowed by the spec;
- keep short 012 status sentences as supporting status feedback where useful.

An example MUST NOT be considered remediated if:

- the primary proof is only `VisibleText`, `VisibleHistory`, a log, or a direct helper result;
- the visual component exists only in source code and is not visible in the rendered runtime state;
- `Help -> Description` is missing or reachable only through a mouse-only path;
- a status message replaces the main component instead of supporting it;
- the proof depends on arbitrary user files, persistent user history, external services, or external proof paths.

## 2. Primary Smoke Contract

Primary smoke tests MUST:

- execute `app.Run()` or the equivalent real application loop;
- inject `TEvent`, command, or key input through the same dispatch path used by runtime users;
- assert concrete visible state such as control presence, dialog state, focus target, selection, scroll position, input value, history state, progress state, rejection state, abort state, or cancel state;
- include a stable rendered visibility proof with both view-tree proof and buffer/cell snapshot proof;
- verify control-specific content at the expected position or region;
- include a deterministic quit path;
- classify direct helper use as `SetupOnly` or `SupplementalAssertion` when used.

Primary smoke tests SHOULD:

- reuse bounded helper code for event scripts and buffer/cell visibility snapshots;
- keep assertions text-first and independent of color alone;
- avoid sleeps, external process timing, arbitrary terminal sizes, network calls, and unbounded filesystem scans.

## 3. Per-Example Acceptance Matrix

| Example | Runtime target | Required smoke evidence |
|---|---|---|
| `Clipboard` | Visible text/input component before and after copy, cut, paste, plus unavailable-clipboard state | App-loop command path; text/input state assertions; rendered content snapshot; status feedback |
| `Demo` | `Dialog/Control`, `File/Path metadata`, and `Display/Color/Gadget` visible flow families | Three distinct app-loop flows; concrete dialog/control, metadata, and display/color/gadget assertions; rendered snapshots |
| `DlgDsn` | Dialog or control tree for valid dialog descriptions, plus visible rejection for invalid controlled fixtures | Valid render path; invalid rejection path; fixture boundary proof; rendered dialog/control snapshot |
| `DynTxt` | Dynamic text view with changed, clipped, aligned, or narrow-width content | State-changing command/key path; rendered dynamic text snapshot; status feedback |
| `InpLis` | Dialog composition with list, input, history or boundary behavior | Focus/selection/input/history or boundary assertion; rendered dialog/list/input snapshot |
| `ListVi` | List viewer/list box with selected item, boundary, empty-state, scrollbar, or focus indication as applicable | Selection/boundary/empty assertion; rendered list region snapshot |
| `ProgBa` | Progress-bar state through completion | Progress start/increment/complete assertions; rendered progress snapshot |
| `Sdlg` | Scroll-dialog or scroll-group state with content outside the initial visible area | Focus or scroll offset assertion; rendered viewport snapshot before/after movement |
| `Sdlg2` | Two-axis scroll-dialog or scroll-group behavior | Horizontal and vertical offset/focus assertions; rendered two-axis viewport snapshots |
| `TCombo` | Input-plus-combo or selection composition with displayed value and boundary/empty behavior | Selection/value/boundary assertion; rendered combo/input snapshot |
| `TProgB` | Progress dialog or window with partial progress, abort, and cancelled states | Separate partial, abort, and cancel assertions; rendered dialog/progress snapshots |

## 4. Historical Source Contract

Before implementation acceptance, each example MUST have a historical-source review that records:

- relevant `.c`/`.cc` source files under `tv203s/`;
- important matching headers (`.h`, `.hpp`, `.hh`) when declarations, constants, data layout, inheritance, macros, or signatures are needed;
- the historical visual intent;
- the C# visible target state;
- intentional user-visible deviations and their rationale.

`tv203s/` is read-only and MUST NOT be modified.

## 5. Description and A11Y Contract

Each `Help -> Description` path MUST:

- be consistently named and keyboard-reachable;
- explain the visible component and operation path;
- summarize the historical intent and any intentional simplification;
- describe how a text-first reviewer can verify the behavior;
- provide German-first and English-second content at roughly CEFR-B2 for learner-facing text;
- be verified by a primary or supplemental smoke test for reachability and content.

`About` MAY provide supplemental context, but it MUST NOT replace the
canonical `Help -> Description` runtime path.

Visible behavior MUST NOT rely on color, layout, or pointer-only affordances alone.

## 6. Fixture and Data Contract

Allowed proof data:

- source-controlled fixtures;
- fixed repository paths used for metadata or validation only;
- test temporary directories;
- deterministic clipboard test doubles or unavailable-clipboard states where needed.

Disallowed proof data:

- arbitrary user file contents;
- persistent user history;
- external proof paths;
- external services or network access;
- database storage.

## 7. Documentation and Evidence Contract

Implementation MUST update or explicitly mark unchanged/N/A:

- affected guide pages under `docs/guides/examples/`;
- `examples/README.md`;
- feature or PR evidence under `specs/013-wave2-visual-component-remediation/`, normally `pr-evidence.md` once implementation evidence is collected;
- architecture evidence under `docs/architecture/` or the feature evidence path;
- security evidence under `docs/security/` or unchanged-risk rationale, naming `security-governance` v0.4.0 as the active baseline;
- supply-chain, SBOM/VEX/SLSA, and AI-SBOM applicability rationale;
- confirmation that the v0.4.0 Rust/Go/Swift/Java/Kotlin/Python/TypeScript/JavaScript secure-coding profiles do not apply to this C#/.NET implementation;
- A11Y evidence for terminal UI and generated HTML where changed;
- `docs/project-statistics.md`;
- `Pflichtenheft.md` next-step and progress markers where implementation changes the prioritized work state;
- shared agent guidance surfaces (`AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`) when implementation changes active feature context, technologies, project structure, or shared workflow rules.

Generated `_site/`, generated `api/*.yml`, build output, test output, and transient local caches MUST NOT be committed.

## 8. Validation Contract

Formal completion evidence MUST include:

```bash
dotnet build --configuration Release
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
dotnet test --configuration Release
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
```

When guides, DocFX content, documentation navigation, or API documentation are affected, completion evidence MUST also include:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Before each build/test command on the numbered branch, `Directory.Build.props` MUST be aligned to `1.13.<patch>.<build>` and the manual build counter MUST be incremented according to repository versioning rules.

If a validation command cannot run locally, the reason and equivalent CI/manual evidence MUST be recorded in feature or PR evidence.

## 9. Out-of-Scope Contract

The feature MUST NOT:

- start Wave 3 or Wave 4 example implementation;
- introduce mandatory mouse-only operation;
- perform broad framework redesign;
- modify historical reference files under `tv203s/`;
- introduce a database, external service, network dependency, or persistent user history;
- add runtime/product AI or AI infrastructure without re-opening the AI-SBOM decision;
- change the DocFX publishing model or commit generated DocFX output.

## Deutsch / English

Deutsch: Dieser Vertrag beschreibt, woran die Umsetzung spaeter gemessen wird: sichtbare Hauptkomponente, Statuszeile, `Help -> Description`, historische Quellenpruefung und strengere Smoke-Nachweise.

English: This contract describes how the later implementation will be judged: visible main component, status line, `Help -> Description`, historical source review, and stricter smoke evidence.
