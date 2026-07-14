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

# Build generated docs plus Playwright + axe accessibility smoke tests
cd tests/web-a11y
npm install
npx playwright install chromium
npm run test:docfx
```

### Linting and Formatting
```bash
# Run code analysis
dotnet build --no-restore --verbosity quiet

# Check for style issues (if configured)
dotnet format --verify-no-changes

# Regenerate docs when API/XML comments change and root config exists
docfx docfx.json

# After every DocFX regeneration, run the matching A11y smoke check
cd tests/web-a11y
npm run test:docfx
```

### Coverage Gate (SC-003)
- **Minimum**: ≥ 70 % Line Coverage jeweils in `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console` (Pflichtenheft §9.4 Nr. 1)
- **Measurement**: Coverlet (`coverlet.collector` package) via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings` from the repository root
- `coverlet.runsettings` is the canonical TuiVision coverage-gate configuration and MUST be kept in sync when gate-relevant assemblies, example assemblies, or test projects are added, renamed, or removed.
- Validate `coverlet.runsettings` with `xmllint --noout coverlet.runsettings` where available before relying on the coverage gate.
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

### JSON Handling
- Use `System.Text.Json` for project-owned JSON parsing and serialization
- Introduce `Newtonsoft.Json` only with documented justification and explicit review approval

### Documentation Guidelines
- Explanatory documentation blocks must be bilingual: German first, English second
- German and English documentation should target CEFR-B2 readability
- Large normative documents such as `Pflichtenheft*.md` and `Lastenheft*.md` may use a synchronized English sidecar with suffix `.EN.md` instead of an oversized inline-bilingual file; the German version remains canonical unless explicitly marked otherwise
- Follow the inclusion principle `Programmierung #include<everyone>`: guides, statistics, examples, and generated API documentation must remain usable in text-first assistive setups such as Braille displays, screen readers, and text browsers
- Generated HTML documentation should target WCAG 2.2 conformance level AA as the accessibility baseline
- Prefer semantic headings, lists, tables, and ASCII/text-first diagrams; do not encode essential meaning only through color, layout, or pointer-only affordances
- Treat bilingual CEFR-B2 delivery and the documented A11Y proof path as formal completion criteria for learner-facing documentation and active requirement artifacts
- Public APIs must include complete XML documentation (`summary`, `param`, `returns`, `exception` where needed)
- New or changed non-trivial logic must be reviewed for didactic inline-comment value when it affects learner understanding or maintainability, especially central framework flows and smoke-test helpers.
- Inline comments explain why a decision, trade-off, constraint, historical deviation, or proof boundary exists; do not add comments that merely restate obvious code.
- Keep inline-comment intensity moderate: normally 1-3 lines before a non-trivial block, with German-first/English-second CEFR-B2 text for didactic explanation blocks.
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
- Builds and tests on Ubuntu and macOS runners; Windows oder WSL-basierte Kompatibilitaetschecks sollen bei relevanten Aenderungen zusaetzlich beruecksichtigt werden
- Uses .NET 10 SDK
- Tests are run using `dotnet test` command with Release configuration
- GitHub Pages is published from `.github/workflows/pages.yml`: build `docfx.json`, run the `tests/web-a11y/` Playwright + axe smoke path, upload `_site/` as a Pages artifact, and keep `_site/` plus generated `api/*.yml` files out of Git
- Build and test validation is mandatory for all code changes

## Branching Convention
- Feature branches use either the agent-prefixed form `codex/<feature-description>` (or another supported agent prefix such as `claude/`, `agy/`, legacy-compatible `gemini/`, `copilot/`, `opencode/`) or the numbered Spec-Kit form `NNN-short-description` when the Spec-Kit workflow creates the branch.
- CI runs on pushes to `main`, `master`, `codex/**`, `claude/**`, `agy/**`, legacy-compatible `gemini/**`, `copilot/**`, and `opencode/**` branches.
- When a dedicated feature branch has implemented the requirements of a Lastenheft, rename that file to `Lastenheft_<Thema>.<feature-branch>.md` so the delivered requirement scope stays traceable in the repository.

## Build Versioning

- Repo-wide assembly version fields live in `Directory.Build.props` and MUST keep `Version`, `AssemblyVersion`, and `FileVersion` aligned for all projects.
- The scheme is `Major.Minor.Patch.Build`.
- `Minor` = current Spec-Kit feature/branch number, interpreted numerically as the canonical PR number for versioning (`007` -> `7`) and used immediately even before a GitHub PR exists.
- `Patch` = current commit count in that feature/PR branch after committing the current change.
- `Build` = manual build counter incremented by the bot before every `dotnet build` or `dotnet test`.
- Before any commit or push on a numbered Spec-Kit branch, the repo-wide version fields in `Directory.Build.props` MUST be aligned to this scheme.

## Example Wave Delivery Pattern

- Groessere verpflichtende Beispielwellen SOLLEN als bewusstes zweistufiges Spec-Kit-Liefermuster geplant werden, wenn Portierungslogik, Framework-Luecken und interaktive Runtime-Politur sonst in einem Feature vermischt wuerden.
- Stufe 1 ist der funktionale Portierungs- und Nachweis-Feature-Lauf: historische Beispielablaeufe portieren, Framework-Voraussetzungen schliessen, deterministische Headless- oder In-Process-Smoke-Pfade bereitstellen, Guides/Evidence ergaenzen und interaktive Runtime-Politur explizit als Follow-up markieren, wenn `dotnet run --project examples/<Name>` noch nicht die finale Demo zeigt.
- Stufe 2 ist der interaktive Showcase-Feature-Lauf: die bewiesenen Funktionen ueber sichtbare Menues, Statuszeilen, Desktop-Controls, Dialoge, Tastaturpfade und skriptbare UI-Event-Smoke-Tests erreichbar machen.
- Eine Beispielwelle gilt erst dann als vollstaendig lern- und reviewtauglich, wenn die interaktive Showcase-Stufe geliefert ist, sofern der Scope nicht ausdruecklich nur einen minimalen nicht-interaktiven Nachweis verlangt.
- Stage-1-PR-Evidence, Specs und Guides muessen den bewusst verschobenen Interaktionsumfang klar nennen; Stage-2-Specs sollen die vorhandenen Nachweisfunktionen wiederverwenden statt Verhalten neu zu portieren.
- Kuenftige Lastenhefte fuer Beispielwellen oder beispielnahe Framework-Vorhaertungen MUESSEN ein Framework-Usage- und Remediation-Gate enthalten. Der spaetere Spec-Kit-Lauf muss pro Beispiel oder Vertragsbereich dokumentieren, welche bestehende TuiVision-Framework-Komponente genutzt wird, ob lokale Sonderlogik existiert und welche Entscheidung gilt: `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation` oder `FollowUpHardening`.
- Wiederverwendbare Logik darf nicht dauerhaft als lokale `examples/`-Sonderloesung verbleiben. Wenn dieselbe lokale Logik in mehreren Beispielen nuetzlich waere oder Framework-Verhalten ersetzt, gehoert sie in einen kleinen Framework-Fix oder in ein eigenes Follow-up-Hardening.

## Historical Source Reference Policy

- Fuer jede Spec-Kit-Feature-Implementierung, die historisch abgeleitetes Turbo-Vision-Verhalten portiert, erweitert, testet, dokumentiert oder korrigiert, muessen die relevanten historischen Implementierungsdateien unter `tv203s/` als Read-only-Referenz geprueft werden. Das betrifft mindestens passende `.c`- und `.cc`-Dateien.
- Falls Typen, Konstanten, Makros, Datenlayout, Vererbung, Funktionssignaturen oder Kontext nur ueber Deklarationen klar werden, muessen auch die relevanten C/C++-Header wie `.h`, `.hpp` oder `.hh` geprueft werden.
- `tv203s/` wird nicht veraendert. Die C#-Portierung bleibt eine moderne, idiomatische Umsetzung der historischen Absicht und keine mechanische 1:1-Uebersetzung.
- `spec.md`, `plan.md`, `tasks.md`, Guides, PR-Evidence oder Architektur-/Security-Nachweise muessen bei relevanten Features festhalten, welcher historische Zweck uebernommen wird und welche wesentlichen nutzer- oder API-sichtbaren Abweichungen bewusst sind.
- Wenn ein Feature keinen historischen `tv203s`-Bezug hat, genuegt ein kurzes `N/A` mit Begruendung in Plan, Aufgaben oder Evidence.

- For every Spec-Kit feature implementation that ports, extends, tests, documents, or fixes historically derived Turbo Vision behavior, review the relevant historical implementation files under `tv203s/` as read-only reference. This includes at least the matching `.c` and `.cc` files.
- When types, constants, macros, data layout, inheritance, function signatures, or context are only clear from declarations, also review the relevant C/C++ headers such as `.h`, `.hpp`, or `.hh`.
- Do not modify `tv203s/`. The C# port remains a modern idiomatic implementation of the historical intent, not a mechanical line-by-line translation.
- For relevant features, `spec.md`, `plan.md`, `tasks.md`, guides, PR evidence, or architecture/security evidence must record which historical intent is followed and which material user-visible or API-visible deviations are intentional.
- If a feature has no historical `tv203s` relevance, a short `N/A` rationale in plan, tasks, or evidence is sufficient.

## Active Feature Context

### 004-editor-file-help-streams
- Current implementation baseline: execute the phase-6 increment from `specs/004-editor-file-help-streams/spec.md` and `specs/004-editor-file-help-streams/plan.md`
- Scope is limited to reusable framework components in `src/TuiVision.Controls` and `src/TuiVision.Serialization`: `TEditor`, `TMemo`, `TFileEditor`, `TEditWindow`, file/dialog/history helpers, help topics/viewers/windows, stream primitives, and named resource containers
- Out of scope for this increment: example applications such as `tvedit`, `bhelp`, and `helpdemo`; driver consolidation; calculator/macros/OS-shell integrations; and unrelated specialized widgets
- Editor flows must cover text editing, insert/overwrite behavior, clipboard-oriented actions, search/replace, modified-state handling, explicit safe-close decisions before unsaved changes are discarded, and distinct overwrite decisions when save conflicts occur
- Integration coverage for this feature must explicitly include event-loop-aware shell interaction, focus transitions, menu execution, and dialog interaction rather than relying on those behaviors only implicitly
- File flows must keep directory navigation, file lists, current file-information metadata, wildcard filtering, manual path entry, and history recall synchronized inside reusable dialogs
- Help flows must support context-based topic lookup, cross-reference navigation, and fallback content for missing contexts
- Stream/resource flows must preserve named lookup semantics and reject malformed persisted input explicitly, including truncated, trailing, unknown-type, and cyclic payload failures
- Planning decisions now fixed for this feature: dedicated runtime help files, shared-reference preservation without cyclic-graph support, exact case-sensitive resource keys, `LF` default for new files, preserved line endings for loaded files, and explicit overwrite decisions after external file changes

### 005-driver-consolidation-m07
- Current planning baseline: execute the Phase-7 increment from `specs/005-driver-consolidation-m07/spec.md` and `specs/005-driver-consolidation-m07/plan.md`
- Scope is limited to the managed driver baseline in `src/TuiVision.Drivers.Console`, the supporting validation in `tests/TuiVision.Drivers.Tests`, and the proof ledger `docs/porting-status.md`
- Out of scope for this increment: mandatory example waves, full closure of the Phase-8 entrance gate, new source modules, native bindings, and any one-to-one recreation of the historical per-OS driver split
- The proof ledger must cover every historical `.cc` implementation file in `tv203s/contrib/tvision/classes` with one mandatory primary target, optional secondary targets, status, evidence, and rationale
- Linux and Windows/WSL compatibility checks are required as reviewable evidence for this phase, but may still be manual or semi-automated rather than mandatory CI gates
- Planning decisions now fixed for this feature: `.cc` files are the formal `M-07` ledger scope, ancillary `.c`/`.h` files may appear only as rationale support, capability buckets replace per-OS lineage as the review model, and Phase 7 remains distinct from the later full Phase-8 gate closure

### 006-close-phase8-gate
- Current planning baseline: execute the Phase-8 entrance-gate closure from `specs/006-close-phase8-gate/spec.md` and `specs/006-close-phase8-gate/plan.md`
- Scope is limited to closing the remaining `M-07` proof and gate evidence in `docs/porting-status.md`, `Pflichtenheft.md`, the existing module test projects plus any required Compatibility-focused validation additions, coverage evidence, formatting evidence, and API-documentation validation for `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console`
- Out of scope for this increment: starting any of the 25 mandatory example waves, introducing substitute follow-on example scope from `TVDEMOS/` or `TVFM/`, reopening Phase-7 capability-bucket decisions except where pending proof rows require final status updates, and unrelated new framework features
- Every historical `.cc` row in `docs/porting-status.md` must end in a final proof state of `portiert + getestet` or `bewusst ausgelassen + Begruendung`; no `portiert + Test ausstehend` rows may remain once the gate is claimed closed
- Gate closure must provide explicit build, full-test, coverage, formatting, and conditional API-doc evidence, and must keep the mandatory example waves blocked until the closure is formally recorded
- Planning decisions now fixed for this feature: `docs/porting-status.md` remains the authoritative M-07 ledger; `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` must each satisfy the explicit 70 % line-coverage gate with assembly-specific evidence; `tests/TuiVision.Compatibility.Tests/` is the planned dedicated Compatibility fallback suite when shared tests are insufficient; placeholder-only or no-op-only modules cannot satisfy that gate; gate-scope removals must update the proof surfaces in the same change; skipped or ignored gate-scoped tests require recorded tracked-issue references; unresolved local-versus-CI coverage conflicts block closure; and the final Phase-8 decision requires a dedicated closure marker or commit reference

### 012-interactive-wave2-demos
- Current implementation status: interactive Wave-2 demo polish is implemented on branch `012-interactive-wave2-demos`; final validation evidence is tracked in `specs/012-interactive-wave2-demos/pr-evidence.md`.
- Delivered scope is limited to making the eleven Wave-2 examples visibly operable at normal runtime: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`; matching event-loop smoke tests in `tests/TuiVision.Examples.SmokeTests/`; guides, README, `pr-evidence.md`, and proportional architecture/security/A11Y/statistics evidence.
- Before wiring or accepting each example, review the relevant historical `.c`/`.cc` source and any important matching headers under `tv203s/` as read-only reference, capture the original demo intent, and document intentional user-visible deviations in guide or PR evidence.
- Every example must show first-screen purpose text, expose primary behavior through menu, keyboard, status, or command paths, and update visible text-first feedback after each demonstrated operation.
- Primary smoke proof must run `app.Run()` or the equivalent real application loop with injected `TEvent`, command, or key events. Direct helper methods may support setup or supplemental assertions only.
- `examples/Demo` is the P1 vertical slice and must prove at least three visible behaviors before the pattern is spread across the rest of the examples.
- File/path and dialog-designer flows stay read-only toward user data: use source-controlled fixtures, fixed repository paths, or test temporary directories; do not read arbitrary user file contents or persist user history as proof.
- Wave 3 and Wave 4 examples, mandatory mouse-only operation, broad framework redesign, new runtime dependencies, databases, external services, and DocFX publishing-model changes are out of scope.
- Next open mandatory example scope after this feature is Wave 3: editor, file, help, and stream demos such as `tvedit`, `bhelp`, and `helpdemo`.

### 013-wave2-visual-component-remediation
- Current implementation status: visual component remediation is implemented on branch `013-wave2-visual-component-remediation`; final validation evidence is tracked in `specs/013-wave2-visual-component-remediation/pr-evidence.md`.
- Scope is limited to the eleven Wave-2 examples: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`.
- Shared runtime support for the remediated examples lives in `examples/Shared/Wave2Runtime.cs` and is linked into the eleven scoped example projects.
- The primary parity proof is the visible UI composition itself: controls, dialogs, windows, view groups, scroll groups, progress displays, input/list/combo composition, or another stable visible runtime state.
- Each example must use the three-layer model: visible main component, real `TStatusLine` feedback, and keyboard-reachable `Help -> Description`.
- Primary smokes must drive the real app loop and combine concrete state assertions with view-tree proof plus buffer/cell rendered visibility proof at expected positions or regions.
- Historical C/C++ sources under `tv203s/` remain read-only intent references; intentional user-visible deviations must be documented in plan, tasks, guides, feature evidence, or PR evidence.
- AI-SBOM is `N/A` for this feature while AI is only development/agent tooling; re-evaluate if runtime/product AI, models, datasets, AI infrastructure, or delivered AI components enter scope.
- Wave 3/4 functionality, broad framework redesign, mandatory mouse-only operation, arbitrary user-file proof, external proof paths, persistent user history, databases, external services, and new runtime dependencies are out of scope.

### 014-wave1-functional-hardening
- Current implementation status: Wave-1 functional hardening is implemented on branch `014-wave1-functional-hardening`; final validation evidence is tracked in `specs/014-wave1-functional-hardening/pr-evidence.md`.
- Scope is limited to `Desklogo`, `MsgCls`, `Tutorial` steps `tvguid01` through `tvguid16`, and `Videomode`.
- The primary proof surface is `specs/014-wave1-functional-hardening/pr-evidence.md`, which records historical source, C# behavior, proof method, helper classification, negative/fallback proof, missing-core decisions, intentional deviations, validation, and the archived Lastenheft path.
- Managed runtime behavior is proven by executable smoke tests; evidence-only proof remains allowed only for explicitly documented no-runtime-target deviations.
- Helper or headless paths may be `PrimaryProof` only when they execute real example or application logic through public commands, events, application methods, or stable public state with concrete assertions; 014 added `PrimaryProof`, `SupplementalProof`, `SetupOnly`, and `LegacyOrTemporary` helper taxonomy.
- Historical C/C++ sources under `tv203s/` remain read-only intent references. `set-logo.cc` and `tv_logo.cc` are Desklogo asset/generator boundary context only.
- Validation baseline: targeted Wave-1 smokes 38/38 passed, full example smokes 91/91 passed, full Release tests 496/496 passed, coverage gate exceeded 70% for all required assemblies, `docfx docfx.json` passed with 0 warnings/errors, and Playwright/axe DocFX smoke passed 2/2 via explicit local-server workaround when sandboxed webserver startup is blocked.
- Feature 015 completed the didactic comment pass; the next open cross-cutting step is `Lastenheft_Secure-Development-Hardening.md` before Wave-1 visual remediation.
- Wave-1 visual remediation, Wave 2/3/4 behavior, broad framework redesign, mouse-only operation, arbitrary user-file proof, external proof paths, persistent user history, databases, external services, new runtime dependencies, and runtime/product AI are out of scope.

### 015-didactic-comment-hardening
- Current implementation status: the selective didactic inline-code-comment hardening is implemented; final evidence is in `specs/015-didactic-comment-hardening/pr-evidence.md`.
- Scope is limited to selective didactic inline, block, file, or module comments, feature evidence in `specs/015-didactic-comment-hardening/pr-evidence.md`, and affected guidance/evidence surfaces.
- Review must cover central framework flows and relevant smoke-test helpers: event/command/dispatch, focus transitions, view hierarchy, StatusLine, Help/Description, dialog state, validation/rejection, buffer/cell proof, rendering snapshots, terminal fallbacks, historical Turbo Vision deviations, and proof helpers.
- Each reviewed area must receive exactly one decision: `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`, `UpdateExistingComment`, or `FollowUpHardening`.
- Comments remain moderate and reason-focused: explain why, trade-off, constraint, historical deviation, or proof boundary; avoid restating obvious code; use German-first/English-second CEFR-B2 text for didactic explanation blocks.
- XML comments remain the API/DocFX surface; pure `//` or `/* */` hardening does not trigger DocFX. XML/API/generated docs/navigation/guides changes trigger the normal DocFX plus A11Y path.
- No runtime behavior change, API change, new dependency, new example porting, broad framework revision, Wave-1 visual remediation, or runtime/product AI is in scope.

### 016-secure-development-hardening
- Current implementation status: the project-wide secure-development hardening is implemented; final evidence is in `specs/016-secure-development-hardening/pr-evidence.md`.
- The durable control matrix covers all 157 `CL-XX-NN` controls with `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, or `FollowUp` and complete ownership, risk, evidence, and re-evaluation fields.
- Delivered remediation is bounded to explicit malformed persistence rejection, immutable workflow dependencies, supply-chain automation, root disclosure guidance, and safe Bash/PowerShell archive-script parity.
- Local acceptance is 498/498 Release tests plus line coverage above 70% for Core, Controls, Serialization, Compatibility, and Drivers.Console; DocFX/axe and remote OS/CI proof remain delivery gates.
- Human legal, provider, organization, and agent-platform decisions remain `Open`; release provenance, reproducible-build/lock maturity, and RFC 9116 remain named follow-ups rather than implicit claims.
- The next open prioritized intake is `Lastenheft_03_EditorHelpAndResourcesHardening.md` before Wave-3 visual porting.

### 017-wave1-visual-component-remediation
- Current implementation status: Wave-1 visual component remediation is implemented; final evidence is in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- `Desklogo`, `MsgCls`, all 16 Tutorial tokens, and `Videomode` now use the three-layer model: visible main state, real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, view-tree identity, and rendered buffer/cell evidence. The acceptance matrix contains four app rows plus 16 unique Tutorial rows; direct helpers are not primary proof.
- Shared composition in `examples/Shared/Wave1Runtime.cs` uses existing framework controls. Desklogo and MsgCls are `UseExistingFramework`; Tutorial and Videomode are bounded `IntentionalDeviation` decisions.
- Historical sources under `tv203s/` remain read-only. No functional re-port, Wave-2/3/4 behavior, broad framework redesign, new dependency, persistence, external service, or runtime/product AI entered scope.
- The complete example-smoke suite passes 101/101 locally; final repository, coverage, DocFX, A11Y, and remote checks remain delivery gates until recorded in feature evidence.
- Feature 018 closes the editor/help/resources intake; the next prioritized intake is `Lastenheft_Wave3-Visual-Component-Porting.md`.

### 018-editor-help-resources-hardening
- Current implementation status: Wave-3 editor/help/resources hardening is implemented; final evidence is in `specs/018-editor-help-resources-hardening/pr-evidence.md`.
- Existing `TEditor`, `TFileEditor`, `TEditWindow`, `THelpViewer`, and `THelpWindow` flows are retained and proven as coherent open/edit/search/replace/save, safe-close/conflict, persisted navigation, back, and fallback paths.
- `THelpSourceCompiler` provides a bounded `.topic` and `{text[:alias]}` source contract with strict UTF-8, deterministic contexts, forward-reference resolution, stable diagnostics, and atomic no-partial-model failure.
- `TLocalizedResourceLookup` uses explicit exact-language, caller-ordered fallback, then neutral keys over case-sensitive `TResourceFile` storage without ambient locale, gettext, codepage, or new dependency scope.
- Persisted resources reject duplicate keys and negative payload lengths; persisted help rejects negative counts and unresolved or invalid reference ranges before presentation.
- Framework decisions are `UseExistingFramework` for editor/file, and bounded `SmallFrameworkFix` for help graph validation, compiler, resources, and i18n. Historical sources remain read-only.
- Wave-3 examples, mouse, terminal/charset/font work, TP7, broad redesign, services, and dependencies remain outside 018.
- The next prioritized intake is `Lastenheft_Wave3-Visual-Component-Porting.md` for `bhelp`, `helpdemo`, `i18n`, `tvedit`, and `tvhc`.

### 019-wave3-visual-component-porting
- Current implementation status: Wave-3 visual component porting is implemented locally; final evidence is in `specs/019-wave3-visual-component-porting/pr-evidence.md`.
- `BHelp`, `HelpDemo`, `I18n`, `TvEdit`, and `TvHc` use visible main components, a real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, view-tree identity, rendered buffer/cell evidence, status, and description. The Wave-3 matrix passes 14/14 locally, including five constrained `48x16` layouts.
- Framework decisions are `UseExistingFramework` for TvEdit, HelpDemo, I18n, and TvHc, and bounded `IntentionalDeviation` for BHelp because the proprietary unchecked Borland `.tch` decoder is omitted.
- Embedded/source-controlled learning content and test-owned temporary paths are the only data boundaries. Historical sources remain read-only.
- Mouse interaction, terminal/charset/font work, Wave 4, broad redesign, services, new dependencies, and runtime/product AI remain outside 019.
- The next prioritized intake is `Lastenheft_04_MouseSupportAndInteraction.md`.

### 020-mouse-support-interaction
- Current implementation status: bounded mouse support and interaction hardening is implemented locally; final evidence is in `specs/020-mouse-support-interaction/pr-evidence.md`.
- `ConsoleMouseIngress` accepts only complete bounded SGR 1006 left press, pressed move, and release reports and publishes zero or one existing `TEvent`; malformed syntax, range, button, capability, and phase input is rejected atomically.
- `TGroup` routes mouse down to one topmost visible target, transfers focus only to selectable targets, and preserves existing exactly-once control commands. Nested mouse coordinates traverse the full owner chain.
- The only mouse drag contract is moving a `TWindow` from its title row. Owner bounds, release, Escape, capability loss, disable, removal, shutdown, and the existing `Ctrl+F5` keyboard fallback are proven.
- Interactive macOS/Linux terminals and WSL use the SGR capability contract; native Windows Console and redirected/headless I/O remain honest `Unsupported` boundaries. Wheel, hover, touch, extra buttons, full protocol parity, and additional drag targets remain out of scope.
- Primary proof runs through `TProgram.GetEvent` and `app.Run()` and combines concrete focus/command/drag state, target identity, visible text, and rendered buffer/cell assertions. Historical sources remain read-only.
- The next prioritized intake is `Lastenheft_05_TerminalCharsetAndEmulation.md`.

### 021-terminal-charset-hardening
- Current implementation status: terminal and charset hardening is implemented locally; final evidence is in `specs/021-terminal-charset-hardening/pr-evidence.md`.
- `TerminalSession` provides a bounded in-process transcript, cursor, 16-color attributes, 4,096-cell FIFO history, deterministic resize/reset/lifecycle, and atomic C0/CSI subset recovery without a process, shell, or PTY.
- `TerminalCharsetMapper` uses Unicode plus one fixed KOI8-R table and U+FFFD replacement. `BitmapFontFixture` validates only raw 8x16/256/4,096-byte metadata; no host codec, font installation, or historical generator is used.
- `TerminalProfile` uses a closed `System.Text.Json` schema. Invalid schema is rejected as a whole; unavailable font/host capability uses an observable safe default and `Unsupported` status.
- `TTerminalView` reuses the Driver-owned session and existing Controls app loop. Primary proof combines session/profile state, concrete view identity, text status, cursor, rendered cells, controlled keyboard input, and deterministic quit.
- Historical terminal, Cyrillic, font, Eterm, and XTerm sources remain read-only intent. Full ANSI/VT/XTerm parity, visible Wave-4 examples, host mutation, new dependencies, services, persistence, and runtime/product AI remain outside 021.
- The next prioritized intake is `Lastenheft_Wave4-Visual-Component-Porting.md`.

### 022-wave4-visual-component-porting
- Current implementation status: Wave-4 visual component porting is implemented locally; final evidence is in `specs/022-wave4-visual-component-porting/pr-evidence.md`.
- `Terminal`, `Cyrillic`, `Fonts`, `ETerm`, and `XTerm` provide a visible main component, real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, exact view identity, rendered buffer/cell evidence, controlled operation, fallback, description, and deterministic host classification. Narrow viewports preserve example identity and status meaning.
- Terminal, Cyrillic, and Fonts are `UseExistingFramework`; ETerm and XTerm are bounded `IntentionalDeviation` immutable manifests that do not parse or execute historical configuration.
- The copied raw 8x16 fixture is byte-identical to historical `font.016`; historical sources remain read-only. No process, shell, PTY, host font/codepage/locale/keyboard mutation, X resource database, terminfo, external command, or new dependency entered scope.
- Physical host observation remains separate from deterministic in-process and remote-CI evidence. Unsupported capabilities use text-first fallbacks and never claim unavailable native behavior.
- The next prioritized intake is `Lastenheft_06_A11Y_Framework.md`.

### 023-a11y-framework
- Current implementation baseline: deliver the managed accessibility layer from `specs/023-a11y-framework/` and `Lastenheft_06_A11Y_Framework.md`.
- Scope is limited to opt-in `IAccessibleWidget` text, typed `cmFocusChanged` announcements, immutable shortcut queries for `TMenuBar`/`TStatusLine`, explicit `TColorScheme.HighContrast`, keyboard coverage inventory, the `A11yFramework` reference app, and DocFX/Axe evidence.
- Focus announcements reuse one existing broadcast and propagate descendant focus to the shell; non-migrated views remain compatible without fabricated labels.
- Keyboard acceptance requires behavioral proof or a concrete `N/A` for Tab, Shift+Tab, arrows, Enter, and direct shortcuts across every inventoried selectable control family.
- Native AT-SPI, NSAccessibility, UI Automation, speech, full-control migration, terminal-wide WCAG claims, Wave 1-4 remediation, and Feature 024 are out of scope.
- Historical Turbo Vision has no direct equivalent for the modern semantic A11Y contracts; relevant focus/menu/status sources are read-only intent context only.
- The former post-023 Wave-5 statement is historical; Features 024-028 supersede it, and the current next intake is Feature 028 followed by the mandatory Feature-029 Terminal.GUI audit.

### 024-tv203-freevision-conformance-audit
- Current planning baseline: the original audit and merge remain complete, but consumer-review Revision 2 in `specs/024-tv203-freevision-conformance-audit/consumer-readiness-review.md` supersedes the zero-finding forward decision.
- Borland documentation and `tv203s/` remain primary; official Free Vision commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` is external secondary evidence only. `TVDEMOS/` and `TVFM/` are read-only consumer evidence.
- The inventory remains 151 historical `.cc` rows, 119 maintained production `.cs` files, 176 exported public types, 16 domains, and 48 contracts.
- Revision-2 decisions are 7 `Aligned`, 27 `IntentionalModernization`, 1 `ConsciouslyOmitted`, 8 `BehavioralDrift`, and 5 `EvidenceGap`.
- The 13 accepted findings route exactly nine items `F001`-`F009` to `Core025` and four items `F010`-`F013` to `ComponentData026`; both are closed, while Wave 5 and Wave 6 remain blocked through Feature 028, Feature 029, all finding-derived hardening, and the new independent closure.
- Revision 2 changes audit data, validation, evidence, requirements, and ordering only; it does not implement runtime behavior or port examples.

### 027-pre-wave5-conformance-closure
- Feature 027 remains valid historical closure evidence: feature merge `35414af` and causal closeout PR #67 merge `1da2b211e84221db87ab9f959b7b40d3ae2b01f0` are complete.
- Its original 13/34/1/0/0 decision set and zero-finding Wave-5 release statement are superseded for forward planning by Feature-024 Revision 2, not rewritten retroactively.
- Final evidence is in `specs/027-pre-wave5-conformance-closure/closure-evidence.md`; all 90 historical tasks are complete.

### 025-core-runtime-conformance-hardening
- Current implementation status: all nine `Core025` findings are implemented and proven; final evidence is in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`.
- `F001`-`F009` close concrete event kinds, focus and group state, idle lifecycle, desktop stack, modal/close, shared command state, real keyboard ingress, and bounded generic drag through real-path red/green proof.
- Feature-024 resolution metadata records exactly 13 non-documentation-only closures from Features 025 and 026 while preserving the Feature-028 and Wave-5/Wave-6 gates.
- The next prioritized intake after Feature 026 merges and `main` is synchronized is `Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.md` for Feature 028.

### 026-component-data-conformance-hardening
- Current implementation status: all four `ComponentData026` findings are implemented and proven; final evidence is in `specs/026-component-data-conformance-hardening/pr-evidence.md`.
- `F010`-`F013` close dialog completion/child validation, phase-aware input validation, mode-aware typed file outcomes, and allowlisted named UI-resource composition through real-path red/green proof.
- Feature-024 metadata now contains 139 maintained source files, 211 exported public types, and exactly 13 closed finding resolutions; Wave 5 and Wave 6 remain blocked through the new Terminal.GUI-derived closure after Feature 029.

### 028-pre-wave5-wave6-conformance-closure
- This evidence-only closure starts only after Features 025 and 026 merge, using `Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.md`.
- It independently revalidates all 13 findings and both consumer families; it fixes no product finding and starts no example wave.
- A full pass closes only the existing TV203/Free Vision gate as `ReadyForTerminalGuiAudit`; both waves remain `BlockedPendingTerminalGuiAudit`, and Feature 029 is the mandatory next intake.

### 029-tv203-freevision-terminalgui-conformance-audit
- This read-only audit starts only after Feature 028 merges and uses `Lastenheft_13_TV203-FreeVision-TerminalGUI-Conformance-Audit.md`.
- Turbo Vision 2.0.3 remains authoritative; pinned Free Vision remains secondary evidence, and Terminal.GUI v1.9.0 at commit `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3` is an additional modern C# implementation opinion.
- Review every existing contract `C001`-`C048`; create `C049+` only for a material uncovered consumer responsibility and `TG001+` only for a reproducible TuiVision contract, consumer, safety, A11Y, platform, or real-path proof gap.
- The audit changes no runtime, API, dependency, example, historical, consumer, Free Vision, or Terminal.GUI source. It creates only non-empty finding-derived hardening Lastenhefte followed by one mandatory independent closure.
- After every autonomous run, promote only reproducible provider-neutral preset learning through a Home-Baseline patch release and exact tag-ZIP adoption before the next run; record `NoPromotion` without an empty branch or PR. Open one consolidated upstream preset issue only immediately before Wave 5.


### Autonomous Red-Proof Completeness
- Before the first red test batch, review imports, public XML docs, harness helpers, focus/ownership assertions, and linked-source assembly identity.
- Group independent negative cases only as a bounded project-local red matrix with explicit failure boundaries and shared ownership.
- When source is linked into multiple assemblies, cross-project proof uses public contracts or state delegates and does not assume one CLR type identity.
- Keep a causal closeout evidence-only and single-commit-capable: do not require its own PR URL, reviewed-head result, or merge commit inside that same repository file; verify terminal facts externally without recursive closeout.
- When push and pull-request events create equivalent workflow sets, use pull-request-context checks as the gate and record duplicate runs as noise; cancel them only under an explicit safe workflow/concurrency contract.

## Agent File Synchronization Policy

- When active feature context, implementation plans, or project-wide agent guidance changes, the following AI-agent files MUST be reviewed and updated together in the same work item if they are affected:
  - `AGENTS.md`
  - `CLAUDE.md`
  - `GEMINI.md`
  - `.github/copilot-instructions.md`
  - `.github/agents/copilot-instructions.md`
- Partial synchronization is not acceptable when shared guidance has changed.
- If one file intentionally diverges for agent-specific reasons, that divergence MUST be explicit and documented in the same change.

## Agentische Skriptausfuehrung / Agentic Script Execution

- Vor jeder Automationsaufgabe zuerst das Betriebssystem pruefen. Wenn ein passendes PowerShell-7-Skript oder Cmdlet vorhanden ist und `pwsh` verfuegbar ist, diese Variante bevorzugen. Fuer strukturierte lokale Automationen ist C# ueber `.NET` oder `mono` ein zulaessiger zweiter Weg, wenn Typisierung, Dateiformate oder Wiederverwendbarkeit dadurch klar besser werden. Erst wenn PowerShell oder C# nicht sinnvoll passen, die OS-nahe vorhandene Repo-Variante nutzen, auf macOS/Linux typischerweise Bash. Keine neue Sprache nur aus Bequemlichkeit einfuehren, wenn ein bestehendes Repo-Skript denselben Zweck erfuellt.
- Detect the operating system before each automation task. If a matching PowerShell 7 script or cmdlet exists and `pwsh` is available, prefer that variant. For structured local automation, C# via `.NET` or `mono` is an acceptable second option when type safety, file formats, or reuse clearly benefit from it. Only when PowerShell or C# is not a good fit, use the existing OS-native repository variant, typically Bash on macOS/Linux. Do not introduce a new language merely for convenience when an existing repository script already solves the task.

## Project Statistics

- Maintain `docs/project-statistics.md` as the living statistics ledger for the repository.
- Update the file after each completed Spec-Kit implementation phase, after each agent-driven repository change, or when a refresh is explicitly requested.
- Within the `## Fortschreibungsprotokoll` table, keep entries in strict chronological order: oldest entry at the top, newest and most recently added entry at the bottom; entries with the same date keep their insertion order.
- Keep a final top-level `## Gesamtstatistik` block as the last section of `docs/project-statistics.md`; do not place any later top-level section after it.
- Inside that final `## Gesamtstatistik` block, maintain compact ASCII-only trend diagrams that visualize at least the current artifact mix, the documented branch/phase development curves, the documented acceleration factors from agentic-AI plus Spec-Kit/SDD support, and a direct comparison between experienced-developer effort, Thorsten-solo effort, and the visible AI-assisted delivery window; keep those diagrams directly below the textual overall summary and refresh them together with the ledger.
- Keep each short CEFR-B2 explanation directly adjacent to its matching ASCII diagram group, ideally immediately before or after it, so apprentices do not have to scroll between explanation and diagram.
- When the data benefits from progression across an X-axis, add simple ASCII X/Y charts as a second visualization layer; keep them approximate, readable in plain Markdown, and explained in CEFR-B2 language.
- ASCII X/Y charts must use fixed-width X slots: every documented phase keeps its slot, missing values stay blank, and overly wide future series are split into labelled blocks such as `0..15`, `16..31`, and `32..47`, each with its own axis line and X labels.
- Keep the statistics section plain-text friendly for Braille displays, screen readers, and text browsers; the ASCII diagrams and their explanations must stay understandable without relying on color or visual layout alone.
- When DocFX content, documentation navigation, or API presentation changes, validate representative `_site/` pages through a text-oriented review path, preferably with a local Playwright accessibility snapshot.
- Keep the Playwright + `@axe-core/playwright` smoke tests in `tests/web-a11y/` runnable and aligned with the current DocFX structure; use `lynx` as an additional text-browser spot check when available.
- Treat every successful `docfx docfx.json` regeneration as requiring the matching `tests/web-a11y/` A11y smoke check in the same work item.
- Use WCAG 2.2 AA as the concrete review baseline for generated HTML documentation, especially for page language, bypass blocks, keyboard focus visibility, non-text contrast, and readable landmark structure.
- Each update must record the relevant branch/phase, observable work window, production/test/documentation line counts, main work packages, the conservative manual baseline of 80 code lines per day for an experienced developer, and the repo-specific Thorsten-Solo comparison baseline of 125 lines per workday for this Pascal/Turbo-Vision-derived port.
- When reporting acceleration, compare both manual references against visible Git active days and label the result as a blended repository speedup rather than a stopwatch measurement.
- When hour values are shown, convert the day-based estimates with the TVoeD working-day baseline of `7.8 hours` (`7h 48m`) per day.

## Workflow Platforms

- The Multi-Mac setup on `MacBook Air M2` and `Mac mini M4 Pro` is the primary development and day-to-day test workflow.
- Keep `gh`, `specify`, `codex`, `claude`, `copilot`, and `agy` installed on both Macs; before Spec-Kit work or Spec-Kit updates, run `specify check` to confirm the required toolchain is available.
- After every `/speckit-plan` run or equivalent plan refresh that changes active technologies, project structure, or agent context, run `.specify/scripts/bash/update-agent-context.sh` for `codex`, `claude`, `agy`, and `copilot` in the same work item by default. This repository treats that multi-agent context refresh as pre-approved maintenance and does not require a separate user prompt.
- Linux and Windows are additional compatibility-validation environments; on Windows, prefer WSL with a current Ubuntu release, currently `Ubuntu 24.04`.
- When changes affect runtime behavior, build reliability, terminal behavior, or portability, include Linux and Windows/WSL compatibility checks where practical and reflect them in CI or equivalent validation evidence when feasible.

## Pflichtenheft Next-Step Marker

- Maintain a prominent `>>> NAECHSTER SCHRITT <<<` marker in `Pflichtenheft.md`.
- The marker MUST point to the currently highest-priority open work item in the prioritized rest-work section and MUST be moved whenever progress changes the effective next step.


## Gemeinsame Governance-Ergaenzung / Shared Governance Addendum

- Alle nutzerseitigen Artefakte muessen barrierefrei gedacht und geprueft werden: CLI-Ausgaben, Dokumentation, HTML, UI und generierte Templates; WCAG 2.2 Level AA ist die Standard-Basis, sobald die Kriterien auf das Artefakt anwendbar sind.
- All user-facing artefacts must be designed and reviewed for accessibility: CLI output, documentation, HTML, UI, and generated templates; WCAG 2.2 Level AA is the default baseline wherever the criteria apply.

- Fuer C#/.NET-Repositories gilt standardmaessig eine Thorsten-Solo-Basis von `125` Zeilen/Arbeitstag, sofern das Repo keinen abweichenden, begruendeten Wert dokumentiert.
- The default Thorsten-solo baseline for C#/.NET repositories is `125` lines/workday unless the repository documents a justified deviation.

## Active Technologies
- C# latest (C# 14) / .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest, Coverlet, docfx (004-editor-file-help-streams)
- Real local file-system interaction plus persisted binary help/resource files; no database layer in this increment (004-editor-file-help-streams)
- C# `latest` on .NET 10 (`net10.0`) + Existing modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, `TuiVision.Compatibility`; MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; docfx for API documentation validation; GitHub Actions for existing CI (005-driver-consolidation-m07)
- Source-controlled Markdown evidence in `docs/porting-status.md`; no database storage; compatibility evidence may include repository notes and command output references (005-driver-consolidation-m07)
- C# `latest` on .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, existing MSTest suites plus any required Compatibility-focused validation additions, Coverlet coverage evidence, `dotnet format`, docfx, `Pflichtenheft.md`, and `docs/porting-status.md` for the formal Phase-8 entrance-gate proof (006-close-phase8-gate)
- Repository-visible proof artifacts only; no database layer, and no example-application delivery in this increment (006-close-phase8-gate)
- Source-controlled example projects under `examples/`; wave-1 examples (`desklogo`, `msgcls`, `tutorial`, `videomode`) delivered; 41 smoke tests green; next: Wave 2 Controls and Dialogs (007-port-wave1-examples)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions for existing CI validation (008-controls-revision)
- In-memory UI state only in production; source-controlled planning, tests, and proof artifacts in `specs/`, `tests/`, and `docs/`; no database or external service storage (008-controls-revision)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell and widget foundation from `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`, `TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions plus existing example-smoke infrastructure for downstream wave-2 readiness (009-controls-widgets-and-collections)
- In-memory UI state only in production; source-controlled planning, tests, proof artifacts, and already delivered downstream examples in `specs/`, `tests/`, `docs/`, and `examples/`; no database or external service storage (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation delivered in `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`) plus existing widget/input primitives (`TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, current `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions and existing `tests/TuiVision.Examples.SmokeTests/` infrastructure as downstream contex (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell, dialog, file, color, history, and widget types (`TDialog`, `TFileDialog`, `TFileList`, `TDirListBox`, `TFileInfo`, `TFileInputLine`, `THistory`, `TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup`, `TColorDisplay`, `TComboBox`, `TProgressBar`, `TParamText`); existing `TuiVision.Serialization` archive/resource foundation (`TRecordRegistry`, `TRecordSerializer`, `TBinaryArchiveReader`, `TBinaryArchiveWriter`, `TResourceFile`, `TResourceCollection`, `pstream` family); MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (010-standard-dialogs-designer)
- In-memory dialog state and session-only history; real local file-system metadata for file-listing/validation only; source-controlled tests/proof artifacts; minimal persisted dialog-description fixture through existing serialization/resource primitives; no database or external service storage (010-standard-dialogs-designer)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing framework modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; new wave-2 example projects under `examples/`; existing `tests/TuiVision.Examples.SmokeTests/`; MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (011-port-wave2-examples)
- Runtime example state is in memory; standard-dialog file flows use real local file-system metadata only; `dlgdsn` may use source-controlled dialog-description fixtures through existing Serialization/resource primitives; no database, external service, persisted user history, or new dependency planned (011-port-wave2-examples)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet test stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned. (012-interactive-wave2-demos)
- Runtime example state is in memory. Dialog-designer and file/path demonstrations use source-controlled fixtures, fixed repository paths, or test temporary directories. The examples must not persist user history, write user data as part of normal demonstration, read arbitrary user file contents as proof, or add a database/external service. (012-interactive-wave2-demos)
- Runtime example state remains in memory. Controlled examples may use source-controlled fixtures, fixed repository paths, or test temporary directories for metadata, rendering, validation, or rejection proof. The feature must not add a database, external service, network dependency, persistent user history, or arbitrary user-file content reads. (013-wave2-visual-component-remediation)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned. (014-wave1-functional-hardening)
- Runtime example state remains in memory. Proof data is limited to existing source-controlled files, controlled example fixtures if needed, or test temporary directories. No database, external service, network dependency, persistent user history, arbitrary user-file content reads, or runtime/product AI storage is planned. (014-wave1-functional-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest/Coverlet validation; existing DocFX plus Playwright/axe web A11Y tooling when documentation triggers apply. No new runtime NuGet dependency is planned. (015-didactic-comment-hardening)
- Source-controlled Markdown evidence and guidance files only. Production code state and tests keep their current storage model. No database, external service, network dependency, persistent user history, runtime/product AI storage, or arbitrary user-file proof path is planned. (015-didactic-comment-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`); Bash and PowerShell 7 for repository tooling + Existing TuiVision projects, MSTest, Coverlet, DocFX, Playwright/axe, GitHub Actions, Gitleaks, and CycloneDX for .NET 6.2.0 as a repository-local tool. No new runtime package is planned. (016-secure-development-hardening)
- Source-controlled Markdown, YAML, shell/PowerShell scripts, a local .NET tool manifest, and test fixtures. Generated evidence is written to temporary or ignored directories. No database, service, credential, runtime AI, or user-data store is introduced. (016-secure-development-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing TuiVision modules, shared Wave-1 example composition, MSTest/Coverlet, DocFX, and Playwright/axe; no new package (017-wave1-visual-component-remediation)
- Runtime example state remains in-process and session-only; proof and governance use source-controlled Markdown, with no database, external service, persistent user history, arbitrary user-file proof, or runtime/product AI (017-wave1-visual-component-remediation)
- C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, and `TuiVision.Drivers.Console`; no new packages (019-wave3-visual-component-porting)
- Embedded/source-controlled learning content and test-owned temporary files only (019-wave3-visual-component-porting)
- C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Drivers.Console`, `TuiVision.Controls`, and reviewed `TuiVision.Compatibility` key translation; no new packages (021-terminal-charset-hardening)
- In-memory session/history state plus source-controlled JSON and raw 8x16 fixtures (021-terminal-charset-hardening)
- C# / .NET 10 for durable test-only validation; Markdown and JSON for evidence + existing framework assemblies, MSTest, `System.Text.Json`, Git, official pinned FPC source checkout (024-tv203-freevision-conformance-audit)
- repository-owned JSON and Markdown evidence; external Free Vision worktree under `/tmp`, never tracked (024-tv203-freevision-conformance-audit)
- C# / .NET 10 + existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, MSTest; no new package (025-core-runtime-conformance-hardening)
- in-process state only; audit JSON and Markdown evidence are repository-owned documentation (025-core-runtime-conformance-hardening)
- C# with latest language version on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, and `TuiVision.Serialization`; no new package or runtime dependency (026-component-data-conformance-hardening)
- Existing bounded binary archive and `TResourceFile`; controlled temporary filesystem metadata for file-dialog proofs (026-component-data-conformance-hardening)

### 007-port-wave1-examples
- Current status: Wave 1 delivered (2026-03-28). `desklogo`, `msgcls`, `tutorial` (16 steps), `videomode` are ported, smoke-tested, and guide-documented.
- Wave 1 scope: `examples/Desklogo/`, `examples/MsgCls/`, `examples/Tutorial/`, `examples/Videomode/`; shared smoke-test infrastructure in `tests/TuiVision.Examples.SmokeTests/`; guides in `docs/guides/examples/`.
- Next open scope: Wave 2 – Controls and Dialogs (requires Controls/Dialog layer as prerequisite before planning starts).
- Planning decisions now fixed: headless smoke seam via `bool headless` constructor parameter + `GetEvent()` override; in-process MSTest execution without external process spawning; bilingual German-first/English-second XML docs and comments at CEFR-B2; `DisplayModeCoordinator.ProbeResizeSupport()` cross-platform probe with CA1416 suppressed.

## Recent Changes
- 014-wave1-functional-hardening: Added plan artifacts for Wave-1 functional hardening, including historical proof matrix, smoke-proof, helper-classification, fallback, missing-core, documentation, and governance planning.
- 013-wave2-visual-component-remediation: Implemented visible main components or stable visual runtime states, real `TStatusLine` feedback, `Help -> Description`, shared `examples/Shared/Wave2Runtime.cs`, stricter app-loop rendered-visibility smokes, guides, README, architecture/security evidence, statistics, and PR evidence for all eleven Wave-2 examples.
- 004-editor-file-help-streams: Added the phase-6 specification and requirements checklist for editor, file, help, stream, and resource components.
- 004-editor-file-help-streams: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/public-api.md`; synchronized shared agent guidance to the post-plan baseline.
- 004-editor-file-help-streams: Applied plan-review clarifications for safe-close vs. overwrite handling, wildcard-filtered file dialogs, explicit malformed-stream cases, and non-functional scope boundaries.
- 004-editor-file-help-streams: Added explicit coverage of insert/overwrite plus clipboard editor actions, synchronized file-information state in dialogs, shell menu/status routing, and the full Core/Controls/Serialization coverage gate.
- 004-editor-file-help-streams: Tightened the remaining integration-test expectations so event-loop dispatch, focus transitions, menu execution, and explicit dialog interaction are named directly in the feature artifacts.
- 005-driver-consolidation-m07: Added the phase-7 specification and clarification set for managed driver consolidation, `M-07` proof coverage, primary versus secondary ledger targets, and required Linux/Windows/WSL compatibility evidence.
- 005-driver-consolidation-m07: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/phase-7-proof-contract.md` to define the capability-based driver consolidation approach, the formal `.cc` ledger scope, and the review contract for `docs/porting-status.md`.
- 005-driver-consolidation-m07: Implemented Phase-7 consolidation: created `DriverCapabilityMap.cs` with 5 capability buckets, built `docs/porting-status.md` covering all 151 historical `.cc` files, added 5 new driver test files (30 tests passing), updated `docs/guides/multi-mac-workflow.md` with compatibility evidence, created `checklists/phase-8-gate-review.md`, updated `Pflichtenheft.md` marker to Phase-8 gate closure.
- 006-close-phase8-gate: Added the Phase-8 entrance-gate specification and requirements checklist for fully closing `M-07`, resolving every pending ledger row to a final proof state, and packaging the remaining build/test/coverage/format/API-doc evidence before mandatory example work may begin.
- 006-close-phase8-gate: Synchronized the hard coverage rule to require `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` each to reach at least 70 % line coverage across the shared agent guidance and gate-tracking artifacts.
- 006-close-phase8-gate: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/phase-8-gate-contract.md` to define the final `M-07` closure workflow, the 5x-70%-coverage gate, the full-suite validation package, and the dedicated closure-commit contract.
- 006-close-phase8-gate: Refined the planning baseline so the five-module coverage gate now requires assembly-specific evidence and forbids placeholder-only or no-op-only modules from satisfying Phase-8 closure.
- 006-close-phase8-gate: Analyse-Remediation nach `tasks.md`-Pruefung eingearbeitet: `gate-docs.md` im Planartefakt-Satz verankert, `tests/TuiVision.Compatibility.Tests/` als explizite Plan-Baseline nachgezogen und die Pflicht auf dokumentierte Tracking-Issues fuer Skip/Ignore-Faelle in Plan, Quickstart und Aufgaben sichtbar gemacht.
- 007-port-wave1-examples: Wave 1 portiert: `desklogo` (minimale Desktop-App), `msgcls` (Broadcast-Nachrichtenrouting), `tutorial` (16 token-basierte Schritte), `videomode` (Faehigkeitserkennung + Fallback). 41 Smoke-Tests gruen, Release-Build sauber, `dotnet format --verify-no-changes` bestanden. `Pflichtenheft.md` Welle-1-Checkliste abgehakt, `>>> NAECHSTER SCHRITT <<<` auf Welle 2 vorgeschoben.
- 008-controls-revision: Controls-Revision implementiert: `TSubMenu`, `TStatusDef`, `WindowFlags` neu hinzugefuegt; `TMenuBar`, `TStatusLine`, `TWindow`, `TDialog`, `TMenuItem`, `TView` erweitert; 338 Tests gruen, `TuiVision.Controls`-Abdeckung 84,02 %, Format-Gate bestanden; `docs/porting-status.md`, `Pflichtenheft.md` und `docs/project-statistics.md` nachgezogen; `Lastenheft_ControlsRevision.md` umbenannt.
- 010-standard-dialogs-designer: Framework-Readiness fuer Standarddialoge und Dialog-Designer implementiert: explizite Datei-/Verzeichnisentscheidungen ohne Dateiinhalt-I/O, Color-/Display-/symbolische Charset-Auswahl, validierte Dialogbeschreibung, persistierter Dialogbeschreibungs-Roundtrip und malformed-input rejection; fokussierte Controls- und Serialization-Tests gruen.
- 011-port-wave2-examples: Planartefakte fuer Welle 2 erstellt: 11 neue Beispielprojekte, dedizierte Example-Smoke-Tests, DE-first/EN-second Guides, Architektur-/Security-/A11Y-Nachweise und klarer `sdlg`/`sdlg2`-Scope als historische ScrollDialog/ScrollGroup-Beispiele.
- 012-interactive-wave2-demos: Interaktive Showcase-Stufe fuer Welle 2 implementiert: alle elf Wave-2-Beispiele besitzen sichtbare normale Runtime-Pfade, app-loop-basierte Smoke-Nachweise, aktualisierte Guides, README-, Architektur-/Security-/A11Y- und PR-Evidence.

## Shared Parent Guidance

- The shared parent file `/Users/thorstenhindermann/RiderProjects/AGENTS.md` intentionally stores only repo-spanning baseline rules.
- Keep repository-specific build, test, workflow, architecture, and feature guidance in this repository's own files; when both layers exist, the repository-local files are the more specific authority.

---

## Level-2-Umgebungsregister / Level-2 Environment Registry

- Die zentrale `constitution.md` enthält das verbindliche Level-2 Project Environment Registry.
- Spec-Kit-Pläne und Agentenarbeit in Level-2-Projekten müssen die passende Registry-Zeile als verbindlichen Kontext für Runtime, Build/Test, A11Y, Statistik und Agentenflächen verwenden.
- Änderungen an einer Level-2-Runtime, Toolchain oder Statistik-Basis müssen `constitution.md`, `.specify/memory/constitution.md` und betroffene KI-Agenten-Dateien gemeinsam prüfen.

*The central `constitution.md` contains the binding Level-2 Project Environment Registry. Spec-Kit plans and agent work in Level-2 projects must use the matching registry row as binding context for runtime, build/test, A11Y, statistics, and agent surfaces. Changes to Level-2 runtime, toolchain, or statistics baselines require a joint review of `constitution.md`, `.specify/memory/constitution.md`, and affected AI-agent files.*
## Memory-Safe Languages (MSL) / Speichersichere Sprachen

- Level-2-Projekte SOLLEN eine speichersichere Sprache (Memory-Safe Language, MSL) als primäre Laufzeit verwenden, wenn die Zielplattform es erlaubt.
- Verbindliche MSL-Erlaubnisliste, Regeln und Begründungspflicht: siehe `constitution.md`, Prinzip XI.
- MSL-Kurzliste: Rust, Swift, C#, F#, Java, Kotlin, Scala, Go, Dart, Python, Ruby, JavaScript, TypeScript, Haskell, OCaml, Erlang, Elixir, Ada, SPARK.
- **Nicht** MSL (Begründung im Level-2-`constitution.md` erforderlich): C, C++, klassisches Objective-C, Assembly, `cc65`-C89, Zig (pre-1.0), Nim (manual), D ohne GC.
- In Nicht-MSL-Repositories (z. B. `C64Projects/cc65`) die im Level-2-`constitution.md` hinterlegte Begründung im Plan- und Task-Kontext erwähnen.
- `speckit.constitution` und `speckit.specify` SOLLEN bei Nicht-MSL-Primärsprache einen **nicht blockierenden** Hinweis ausgeben (Tooling-Aufgabe, separate Umsetzung).
- Änderungen an dieser Empfehlung erfordern ein gemeinsames Update in `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md`.

*Level-2 projects SHOULD use a memory-safe language (MSL) as their primary runtime when the target platform allows. Authoritative rules: `constitution.md`, Principle XI. MSL short list: Rust, Swift, C#/F#, Java/Kotlin/Scala, Go, Dart, Python, Ruby, JavaScript/TypeScript, Haskell, OCaml, Erlang/Elixir, Ada/SPARK. Non-MSL languages (C, C++, Assembly, `cc65`, Zig pre-1.0, …) require a documented justification in the Level-2 `constitution.md`. In non-MSL repositories (e.g. `C64Projects/cc65`), surface the documented justification in plans and tasks. `speckit.constitution` and `speckit.specify` SHOULD emit a non-blocking advisory warning when the primary language is not an MSL — tracked as a separate tooling task. Changes to this recommendation require a joint update across `constitution.md`, `.specify/memory/constitution.md`, and all four agent guidance files.*
## Sichere Code-Erzeugung / Secure Code Generation (ISO 27001/27002 A.8.28)

- KI-generierter und menschlich geschriebener Code MUSS den etablierten Secure-Coding-Best-Practices der Zielsprache und des Frameworks folgen. LLMs erzeugen nicht zuverlässig sicheren Code; explizite Durchsetzung ist erforderlich.
- Verbindliche Regeln und sprachspezifische Anforderungen: siehe `constitution.md`, Prinzip XII.
- Sprachspezifische Kurzregeln (Detailprofil: `.specify/templates/secure-coding-language-rules-template.md`):
  - **C / C89**: Bounds-Checking, kein `gets()`, kein ungeprueftes `sprintf()`/`strcpy()`, CERT C.
  - **C# / .NET**: parametrisierte Queries, Output-Encoding gegen XSS, Anti-Forgery-Tokens, sichere Deserialisierung, Microsoft Secure Coding Guidelines.
  - **Rust**: `unsafe` isolieren und begruenden, keine Panic-Pfade aus nicht vertrauenswuerdigem Input, Deserialisierung validieren, `cargo audit` oder gleichwertig verwenden.
  - **Go**: HTTP-/Client-Timeouts setzen, `context` propagieren, SSRF pruefen, `crypto/rand` nutzen, `govulncheck` oder gleichwertig verwenden.
  - **Swift**: keine Force-Unwraps auf nicht vertrauenswuerdigen Daten, dekodierte Eingaben validieren, Keychain/CryptoKit/TLS-Defaults nutzen, Datei-URLs einschraenken.
  - **Java / Kotlin**: DTOs validieren, Persistence-Zugriffe parametrisieren, Deserialisierung beschraenken, Auth/CSRF/CORS/Session-Defaults pruefen.
  - **Python**: Boundary-Input validieren, keine unsichere Deserialisierung oder dynamische Ausfuehrung, `subprocess`/Dateipfade einschraenken, Dependency-Audit nutzen.
  - **TypeScript / JavaScript**: Runtime-Input validieren, XSS/Prototype-Pollution/SSRF pruefen, keine dynamische Code-Ausfuehrung, Lockfiles auditieren.
  - **SQL**: nur parametrisierte Statements, kein dynamisches SQL aus nicht vertrauenswuerdigem Input.
  - **Bash**: Variable in Anfuehrungszeichen (`"$var"`), kein `eval` auf nicht vertrauenswuerdigem Input, `--` End-of-Options.
  - **PowerShell**: `Set-StrictMode -Version Latest`, validierte Parameter, kein `Invoke-Expression` auf nicht vertrauenswuerdigem Input.
- Kryptografie: aktuelle Algorithmen (AES-256, RSA >= 3072, SHA-256+, Ed25519); veraltete (MD5, SHA-1 für Signaturen, DES, RC4) nur mit expliziter Risikobegründung.
- Fehlerbehandlung darf keine internen Zustände, Stack-Traces oder Verbindungszeichenketten an Endbenutzer preisgeben.
- Hinzugefügte Abhängigkeiten müssen aktiv gepflegt sein und dürfen keine bekannten kritischen CVEs aufweisen.
- Code-Reviews MÜSSEN eine Sicherheitsperspektive für Eingabeverarbeitung, Authentifizierung, Autorisierung, Kryptografie und Datei-/Netzwerk-I/O enthalten.
- Änderungen an dieser Regel erfordern ein gemeinsames Update in `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md`.

*AI-generated and human-written code MUST follow the secure-coding best practices of the target language and framework. Authoritative rules: `constitution.md`, Principle XII, and `.specify/templates/secure-coding-language-rules-template.md`. Language-specific short rules cover C/C89, C#/.NET, Rust, Go, Swift, Java/Kotlin, Python, TypeScript/JavaScript, SQL, Bash, and PowerShell. MSL status does not replace secure API, I/O, auth, SQL, crypto, logging, or dependency review. Cryptography: use current algorithms (AES-256, SHA-256+, Ed25519); deprecated (MD5, SHA-1 for signatures, DES, RC4) only with explicit risk acknowledgement. Error handling must not expose internals. Dependencies must have no known critical CVEs. Code reviews must include a security perspective for input handling, auth, crypto, and I/O. Changes require a joint update across `constitution.md`, `.specify/memory/constitution.md`, and all four agent guidance files.*
## Sichere Software-Architektur / Secure Software Architecture (ISO 27001/27002 A.8.27)

- KI-generierte und menschlich geschriebene Software-Architektur MUSS etablierten sicheren Architekturprinzipien folgen. Sicherer Code (Prinzip XII) ohne sichere Architektur reicht nicht aus — beide Ebenen müssen zusammenwirken.
- Verbindliche Regeln und sprachspezifische Architekturvorgaben: siehe `constitution.md`, Prinzip XIII.
- Verbindliche Architekturprinzipien:
  - **Trust Boundaries**: Explizite Vertrauensgrenzen definieren; alle Eingaben an Vertrauensgrenzen validieren und bereinigen.
  - **Defense in Depth**: Mindestens zwei unabhängige Sicherheitsschichten für kritische Assets.
  - **Least Privilege**: Jede Komponente, jeder Dienst und Prozess arbeitet mit minimalen Berechtigungen.
  - **Fail-Safe Defaults**: Zugriff standardmäßig verweigern, explizit gewähren; Fehlerpfade fallen in sicheren Zustand zurück.
  - **Angriffsfläche reduzieren**: Ungenutzte Endpunkte, Dienste und Debug-Funktionen deaktivieren oder entfernen.
  - **Separation of Concerns**: Authentifizierung, Autorisierung, Logging und Eingabevalidierung als Cross-Cutting Concerns implementieren, nicht ad-hoc verstreuen.
  - **Sichere Konfiguration**: Secrets in plattformgeeigneten Secret-Stores (z. B. Azure Key Vault, macOS Keychain), nie im Quellcode oder in Git-tracked Config-Dateien.
  - **Supply-Chain-Sicherheit**: Abhängigkeiten aus verifizierten Registries; Lock-Files committen; verwundbare Abhängigkeiten vor Release ersetzen.
- Änderungen an dieser Regel erfordern ein gemeinsames Update in `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md`.

*AI-generated and human-written software architecture MUST follow secure-architecture principles. Authoritative rules: `constitution.md`, Principle XIII. Core principles: trust boundaries (validate all input at system boundaries), defense in depth (at least two independent security layers), least privilege (minimum required permissions), fail-safe defaults (deny by default), attack surface reduction (disable unused features), separation of concerns (auth/logging/validation as cross-cutting concerns), secure configuration (secrets in secret stores, never in code or Git), supply-chain security (verified registries, lock files, no known-vulnerable dependencies). Principles XII + XIII together form the complete secure-development approach: XII = tactical code-level security, XIII = strategic architecture-level security. Changes require a joint update across `constitution.md`, `.specify/memory/constitution.md`, and all four agent guidance files.*
## Allgemeine Architektur-Governance / General Architecture Governance (iSAQB / arc42)

- Architektur MUSS als explizites Arbeitsergebnis behandelt werden, wenn Struktur, Schnittstellen, Qualitätsattribute, Laufzeitverhalten, Deployment, Wartbarkeit oder technische Schulden betroffen sind.
- Verbindliche Regeln: siehe `constitution.md`, Prinzip XX; Architektur-Evidenz liegt standardmaessig unter `docs/architecture/`.
- Relevante Artefakte: Architecture Vision, Context View, Building-Block View, Runtime View, Deployment View, Quality Scenarios, Architecture Decision Records und Architecture Risks/Technical Debt.
- In `spec.md`, `plan.md` und `tasks.md` immer festhalten, ob Architektur-Evidenz erforderlich ist; `N/A` nur mit kurzer Begruendung.
- Bei sicherheitsrelevanter Architektur zusaetzlich die Secure-Architecture- und Security-Evidenz aus `docs/security/` anwenden.

*Architecture MUST be treated as an explicit work product when structure, interfaces, quality attributes, runtime behavior, deployment, maintainability, or technical debt are affected. Authoritative rules: `constitution.md`, Principle XX. Default evidence lives under `docs/architecture/`: architecture vision, context, building blocks, runtime, deployment, quality scenarios, ADRs, and architecture risks/technical debt. `spec.md`, `plan.md`, and `tasks.md` must state whether this evidence applies; `N/A` requires rationale. Security-relevant architecture also uses the secure-architecture evidence under `docs/security/`.*
## Sicherheitsdokumentation / Security Documentation (XII–XVIII Extensions)

- Jedes Level-2-Projekt MUSS die folgenden Sicherheitsdokumente pflegen, basierend auf den Templates in `.specify/templates/`:
  - **Bedrohungsmodell / Threat Model** (`threat-model-template.md`) — STRIDE-Methodik, Trust Boundaries, Risikobewertung, CAPEC-Referenzen (Prinzip XIII + XVII)
  - **Security Architecture Decision Records (S-ADR)** (`adr-template.md`) — architektonische Sicherheitsentscheidungen mit Compliance-Nachweis (Prinzip XIII)
  - **arc42 Section 8 Sicherheits-Querschnittskonzepte** (`arc42-security-template.md`) — Authentifizierung, Autorisierung, Verschlüsselung, Eingabevalidierung, Fehlerbehandlung, Logging, Abhängigkeiten, Deployment (Prinzip XIII)
  - **Sicherheits-Checkliste / Security Checklist** (`security-checklist-template.md`) — sprachspezifische Code-Review-Checkliste (Prinzip XII)
  - **Abhängigkeits-Audit / Dependency Audit** (`dependency-audit-template.md`) — CVE-Tracking, Lizenz-Compliance, Supply-Chain-Sicherheit (Prinzip XII)
  - **Sicherheits-Qualitätsszenarien / Security Quality Scenarios** (`security-quality-scenarios-template.md`) — iSAQB CPSA-F Qualitätsszenario-Methodik (Prinzip XII + XIII, SHOULD)
  - **ASVS-Verifikation / ASVS Verification** (`asvs-verification-template.md`) — OWASP ASVS Level, Scope und Evidenz (Prinzip XV, Web-/API-Projekte MUST)
  - **Supply-Chain-Evidenz / Supply Chain Evidence** (`supply-chain-evidence-template.md`) — SBOM, AI-SBOM, VEX, SLSA, OpenSSF Scorecard (Prinzip XVI, releasefähige Projekte MUST; AI-SBOM nur bei KI-Runtime-/Produktkomponenten)
  - **Zero-Trust-Anwendbarkeit / Zero Trust Applicability** (`zero-trust-applicability-template.md`) — NIST SP 800-207-Bewertung (Prinzip XVIII, verteilte Systeme SHOULD)
  - **SAMM-Bewertung / SAMM Assessment** (`samm-assessment-template.md`) — OWASP SAMM Reifegrad und Verbesserungsplan (Prinzip XVIII, langlebige Projekte SHOULD)
- Projektspezifische Instanzen werden in `docs/security/` gepflegt; S-ADRs als einzelne Dateien in `docs/security/adr/`.

*Every Level-2 project MUST maintain security documents based on templates in `.specify/templates/`: threat model (STRIDE+CAPEC), S-ADRs, arc42 Section 8 security concepts, security checklist, dependency audit, security quality scenarios (SHOULD), ASVS verification (web/API MUST), supply-chain evidence (release-capable MUST; AI-SBOM when AI runtime/product components apply), Zero Trust applicability note (distributed systems SHOULD), and SAMM assessment (long-lived projects SHOULD). Project-specific instances live in `docs/security/`; S-ADRs in `docs/security/adr/`. See `constitution.md`, Principles XII–XVIII for authoritative requirements.*

## Sicherheitsstandards & Anwendbarkeit / Security Standards & Applicability

- Vor jeder Level-2-Aufgabe die anwendbaren Sicherheitsstandards aus `constitution.md`, Prinzipien XIV-XVIII bestimmen und explizit benennen.
- `NIST SSDF` und `CWE Top 25` gelten immer für Level-2-Arbeit.
- `OWASP ASVS` gilt für Web-, API-, HTTP- und authentifizierte Dienste; der gewählte ASVS-Level muss benannt werden.
- `SBOM` gilt für releasefähige oder verteilbare Artefakte; `VEX`, wenn bekannte Schwachstellen in ausgelieferten oder geprüften Komponenten bewertet werden müssen.
- `AI-SBOM` gilt projektartabhängig bei KI-Modellen, KI-Diensten, Trainings-/Embedding-Daten, Inferenz-Infrastruktur oder KI-Runtime-Komponenten im ausgelieferten oder betriebenen System; reine Entwicklungswerkzeug-Nutzung wird als `N/A` mit Toolchain-Begründung dokumentiert.
- `SLSA` gilt als Soll-Vorgabe für CI/CD- oder veröffentlichte Artefakte; `Zero Trust` ist für verteilte, servicebasierte, cloudnahe oder remote-verwaltete Systeme explizit zu prüfen.
- `CAPEC` soll in Bedrohungsmodellen für die risikoreichsten Angriffswege verwendet werden; `OWASP SAMM` soll für langlebige Projekte/Workspaces in Verbesserungspläne einfließen.
- `OWASP Cheat Sheet Series`, `OWASP Proactive Controls` und bei öffentlichen OSS-Repositories oder kritischen Abhängigkeiten `OpenSSF Scorecard` sind als ergänzende Referenzen zu berücksichtigen.
- Nichtanwendbarkeit immer als `N/A` mit kurzer Begründung dokumentieren; keine stillschweigende Auslassung.

*At the start of every Level-2 task, determine and name the applicable security standards from `constitution.md`, Principles XIV-XVIII. `NIST SSDF` and `CWE Top 25` always apply. `OWASP ASVS` applies to web/API/HTTP/auth-bearing services; `SBOM` applies to releasable or distributable artefacts; `AI-SBOM` applies when AI models, AI services, datasets, inference infrastructure, or AI runtime components are part of the released or operated system; `VEX` applies when known vulnerabilities in shipped/evaluated components need a disposition statement. `SLSA` is the target model for CI/CD and published artefacts; `Zero Trust` must be explicitly evaluated for distributed, service-based, cloud, or remotely managed systems. `CAPEC`, `OWASP SAMM`, `OWASP Cheat Sheet Series`, `OWASP Proactive Controls`, and `OpenSSF Scorecard` are supporting references where relevant. Record non-applicability as `N/A` with justification rather than omitting it silently.*

## Agentischer Security-Workflow / Agentic Security Workflow

- In `spec.md`, `plan.md` und `tasks.md` die anwendbaren Standards samt Evidenzpfad festhalten.
- Bei Bedrohungsmodellen `STRIDE` als Basis und bei risikoreichen Flows zusätzlich relevante `CAPEC`-Patterns verwenden.
- Bei Web/API-Features den `ASVS`-Level und den Verifikationsumfang in `docs/security/` oder gleichwertiger Projektdokumentation ablegen.
- KI-Nutzung explizit klassifizieren: Entwicklungswerkzeug, keine KI im ausgelieferten/betriebenen System, oder KI-Runtime-/Produktkomponente; `AI-SBOM` entsprechend als `N/A` begründen oder in der Supply-Chain-Evidenz dokumentieren.
- Bei Release-/Artefakt-Arbeit `SBOM`, `AI-SBOM`, `VEX`, Provenance/SLSA-Nachweise und gegebenenfalls `OpenSSF Scorecard` in Release- oder Sicherheitsdokumentation einplanen.
- Bei Architekturänderungen `Zero Trust`-Anwendbarkeit und bei langlebigen Projekten `SAMM`-Folgeaktionen prüfen.
- Default-Evidenzpfad: `docs/security/asvs-verification.md`, `docs/security/supply-chain-evidence.md`, `docs/security/zero-trust-applicability.md`, `docs/security/samm-assessment.md`; Abweichungen nur mit lokal dokumentierter Begründung.

*Capture the applicable standards and the evidence path in `spec.md`, `plan.md`, and `tasks.md`. Use `STRIDE` as the base for threat modeling and add relevant `CAPEC` patterns for the highest-risk flows. For web/API work, record the chosen `ASVS` level and verification scope in `docs/security/` or equivalent project documentation. Classify AI usage as development tooling, absent from the released/operated system, or AI runtime/product component; document `AI-SBOM` as `N/A` or as supply-chain evidence accordingly. For release and artefact work, plan `SBOM`, `AI-SBOM`, `VEX`, provenance/SLSA evidence, and `OpenSSF Scorecard` review where applicable. For architectural changes, evaluate `Zero Trust`; for long-lived projects, consider `OWASP SAMM` follow-up actions. The default evidence path is `docs/security/asvs-verification.md`, `docs/security/supply-chain-evidence.md`, `docs/security/zero-trust-applicability.md`, and `docs/security/samm-assessment.md`, unless the repository documents a justified equivalent location.*

## GitHub/GitLab CLI First / GitHub/GitLab CLI zuerst

Für GitHub-Repositories zuerst die authentifizierte `gh` CLI für mögliche Schreibaktionen und Live-Repository-Operationen verwenden, einschließlich PR-/Issue-Kommentaren, PR-Statusprüfungen, Review-Follow-up, Workflow-Prüfung und Merge-/Statusabfragen. GitHub-Connector-Tools hauptsächlich für strukturierte Read-only-Inspektion oder Fälle nutzen, in denen die CLI nicht geeignet ist.

Für GitLab-Repositories die authentifizierte `glab` CLI zuerst für gleichwertige Aktionen verwenden. Bekanntermaßen fehlschlagende Connector-Schreibwege nicht wiederholt versuchen, wenn `gh`/`glab` die Aufgabe direkt erledigen kann.

For GitHub repositories, use the authenticated `gh` CLI first for feasible write actions and live repository operations, including PR/issue comments, PR status checks, review follow-up, workflow inspection, and merge/status queries. Use GitHub connector tools mainly for structured read-only inspection or when the CLI is not suitable.

For GitLab repositories, use the authenticated `glab` CLI first for equivalent actions. Do not repeatedly try connector write paths that are known to fail when `gh`/`glab` can perform the task directly.


## Spec-Kit-Modell-Routing / Spec Kit Model Routing

- Modellwahl ist operative Agenten-Routing-Guidance, keine Feature-Anforderung. Modellnamen nicht in `spec.md`, `plan.md`, `tasks.md` oder einzelne Feature-Specs schreiben; diese Artefakte muessen reproduzierbar bleiben, auch wenn Modellnamen wechseln oder ein anderer KI-Agent verwendet wird.
- Der jeweilige Agent soll diese Empfehlungen auf seine aktuell verfuegbaren Modelle abbilden; keine feste Anbieter- oder Modellbindung ableiten.
- Fuer Spec-Kit-Spezifikation, Klaerung, Planung, Tasks und Analyse (`/speckit-specify`, `/speckit-clarify`, `/speckit-plan`, `/speckit-tasks`, `/speckit-analyze`; je nach Agent auch `/speckit.specify` usw.) das staerkste verfuegbare Frontier-Reasoning-/Coding-Modell bevorzugen.
- Fuer vollstaendige, lang laufende `/speckit-implement`-Laeufe das staerkste verfuegbare Long-Running-Agent-Modell bevorzugen; das Frontier-Modell nutzen, wenn maximale Urteilsguete wichtiger ist als Laufzeitstabilitaet.
- Fuer fokussierte Reviews oder CI-Fixes ein coding-optimiertes Modell bevorzugen.
- Fuer triviale Bereinigung, Formatierung oder risikoarme mechanische Edits ist ein schnelles kleines Coding-Modell akzeptabel.

*Model choice is operational agent-routing guidance, not a feature requirement. Do not pin model names in `spec.md`, `plan.md`, `tasks.md`, or individual feature specs; those artifacts must stay reproducible even when model names change or another AI agent is used. Each agent should map these recommendations to its currently available models; do not derive a fixed vendor or model requirement. For Spec-Kit specification, clarification, planning, task generation, and analysis (`/speckit-specify`, `/speckit-clarify`, `/speckit-plan`, `/speckit-tasks`, `/speckit-analyze`; or `/speckit.specify` etc. depending on the agent surface), prefer the strongest available frontier reasoning/coding model. For complete long-running `/speckit-implement` runs, prefer the strongest available long-running agent model; use the frontier model when maximum judgment quality is more important than runtime stability. For focused review or CI fixes, prefer a coding-optimized model. For trivial cleanup, formatting, or low-risk mechanical edits, a fast small coding model is acceptable.*

## Autonome Spec-Kit-Läufe / Autonomous Spec-Kit Runs

- Vollständig delegierte Spec-Kit-Läufe folgen `docs/spec-kit-autonomous-runbook.md` und verwenden den projektgebundenen Skill `$speckit-autonomous`.
- Vor dem Start muss der Delivery-Modus `LocalImplementation`, `PublishPR` oder `MergeAndSync` aus dem aktuellen Benutzerauftrag bestimmt werden. Allgemeine Autonomie erteilt keine stillschweigende Remote-Schreib- oder Merge-Berechtigung.
- Evidence wird vor der ersten Implementierungsänderung angelegt. Clarify, Checklists, Analyze, Implement und Remote Review werden bis zu den im Runbook definierten Konvergenzkriterien ausgeführt, nicht nach einer festen Wiederholungszahl.
- Ein repräsentativer vertikaler Slice mit Test und Proof kommt vor der breiten Wiederholung. Gemeinsame Evidence-, Versions-, Statistik-, Workflow- und Agent-Dateien bleiben Single-writer-Flächen.
- Jede Remote- oder Delivery-Task nennt den konkreten Repository-Evidence-Pfad für ihr Abnahmeergebnis; implizite Evidence-Verweise reichen für Analyze und Resume nicht aus.
- Aktuelle Check-/Review-Fakten werden vor dem Merge geprüft, aber in genau einen benannten Closeout-Evidence-Pfad verschoben, wenn ihr Commit den geprüften Feature-Head und damit die Aussage selbst entwerten würde.
- Jede manuelle Build-Zählererhöhung gilt für genau einen expliziten `dotnet build`- oder `dotnet test`-Aufruf; mehrere Aufrufe dürfen nicht hinter einer Erhöhung verkettet werden.
- Repository-Prüfhelfer erhalten den Repository-Root explizit. Ein Pass benötigt erwarteten Exitcode und einen Fehlerkanal ohne PowerShell-ErrorRecord, `command not found` oder gleichwertige fatale Signatur.
- Vor einem Commit wird der beabsichtigte Lieferkandidat separat gestaged und mit `git diff --cached --check` sowie einem Pfadvergleich gegen den Repository-Status geprüft; `git diff --check` allein erfasst keine ungetrackten Dateien, und fremde Änderungen bleiben ausgeschlossen.
- Vor einem Merge wird jedes Acceptance-Gate dem tatsächlichen Workflow, Job, Betriebssystem und ausgeführten Command zugeordnet; ein grüner Aggregatstatus oder ein Plattformname gilt nicht als Proof, wenn der Job den geforderten Runtime-, Plattform-, Dokumentations- oder Security-Nachweis nicht ausgeführt hat.
- Jeder Lauf schützt den akzeptierten Scope, verwendet triggerbasierte Validierung und dokumentiert eine kurze Retrospektive für spätere Runbook-Verfeinerungen.

*Fully delegated Spec-Kit runs follow `docs/spec-kit-autonomous-runbook.md` and use the repository-local `$speckit-autonomous` skill. Determine `LocalImplementation`, `PublishPR`, or `MergeAndSync` from the current user request; general autonomy does not silently grant remote write or merge authority. Create evidence before implementation, iterate optional stages to their defined convergence criteria, prove one representative vertical slice before broad rollout, serialize shared writers, require each remote or delivery task to name its exact repository evidence path, route self-invalidating reviewed-head facts to one named closeout path after verifying them before merge, validate the exact staged candidate with `git diff --cached --check` so new files are included without unrelated changes, map each acceptance gate to the actual workflow, job, platform, and executed command before merge, protect accepted scope, use trigger-based validation, and record a short retrospective for later workflow refinement.*

## Spec-Kit-Preset-Pflege / Spec Kit Preset Maintenance

- Standard-Preset-Set: `security-governance` v0.6.0 prio 10, `architecture-governance` v0.5.0 prio 20, `isaqb-architecture-governance` v0.2.0 prio 30, `a11y-governance` v0.4.0 prio 40, `cross-platform-governance` v0.2.0 prio 50, `agent-parity-governance` v0.3.0 prio 60.
- Optional installiert: `autonomous-run-governance` v0.2.0 prio 70 aus dem öffentlichen Tag-ZIP. Der projektgebundene Codex-Skill `$speckit-autonomous` bleibt am selben eindeutigen Pfad als lokaler Override bestehen, weil er TuiVision-spezifische Nummerierungs-, Build-Zähler-, DocFX-/A11Y- und historische Source-Verträge ergänzt; Preset-Command, Retrospektiv-Skill, Runbook und Adoption-Evidence bleiben die portablen beziehungsweise gemeinsamen Nachweisflächen. Version 0.2.0 verlangt vor der Implementierung deklarierte Acceptance-Gates und prüft vor dem Merge temporäre Evidence für den exakten HEAD. Der Validator gleicht Requirements-Hash, HEAD, Command-/Runner-Tokens und genau einen `Primary`-Nachweis mit echten Workflow-Definitionen oder Job-Logs ab; grüne Namen, Validator und Bypass ersetzen weder technischen Nachweis noch Remote- oder Merge-Berechtigung. Version 0.2.0 ergänzt `speckit.autonomous-status`, den kooperativen `speckit.autonomous-stop`, den geschützten `speckit.autonomous-resume` und validierten feature-lokalen Laufzustand; `PausedByUser` wird nie stillschweigend fortgesetzt und unsicher beendete Operationen bleiben `NeedsRevalidation`.

*Optional installed preset: `autonomous-run-governance` v0.2.0 at priority 70 comes from the public tag ZIP. The project-owned Codex `$speckit-autonomous` skill stays at its single path as a local override because it adds TuiVision-specific numbering, build-counter, DocFX/A11Y, and historical-source contracts. The preset command, retrospective skill, runbook, and adoption evidence remain the portable or shared proof surfaces. Version 0.2.0 requires acceptance gates to be declared before implementation and validates temporary exact-HEAD evidence before merge. The validator checks the requirements hash, HEAD, command and runner tokens, and exactly one `Primary` proof against actual workflow definitions or job logs; green names, the validator, and bypass grant neither technical proof nor remote or merge authority. Version 0.2.0 adds `speckit.autonomous-status`, cooperative `speckit.autonomous-stop`, protected `speckit.autonomous-resume`, and validated feature-local run state; `PausedByUser` is never resumed silently, and uncertain operations remain `NeedsRevalidation`.*
- Aktive Google-Agentenoberfläche ist Antigravity CLI mit Befehl `agy` und Spec-Kit-Integration `agy`. `GEMINI.md` und `~/.gemini/antigravity-cli/` bleiben Antigravity-kompatible Oberflächen; direkte `gemini`-Befehle sind nur historische oder ausdrücklich benötigte Enterprise-/API-Kompatibilität und keine lokale Pflicht.

*The active Google agent surface is Antigravity CLI through the `agy` command and Spec Kit `agy` integration. `GEMINI.md` and `~/.gemini/antigravity-cli/` remain Antigravity-compatible surfaces; direct `gemini` commands are historical or explicitly required enterprise/API compatibility, not a local requirement.*
- `a11y-governance` v0.4.0 ergaenzt didaktische Inline-Code-Kommentar-Governance fuer neue oder geaenderte nicht-triviale Logik.
- `security-governance` v0.6.0 fuehrt `AI-SBOM` weiter als bedingt anwendbare Supply-Chain-Evidenz, ergaenzt sprachspezifische Secure-Coding-Profile und ergaenzt regulatorische Anwendbarkeit fuer NIS2, CRA, EU AI Act und DORA. Reine Entwicklungswerkzeug-Nutzung bleibt `N/A`; KI-Runtime-/Produktkomponenten benoetigen Evidenz nach G7/BSI AI-SBOM-Clustern; private Ausbildungsprojekte dokumentieren regulatorische Nichtanwendbarkeit mit kurzer Begruendung.
- `architecture-governance` v0.5.0 ergaenzt `BSI C3A` als bedingte Cloud-Autonomie-Evidenz und `BSI C5` als bedingte Cloud-Compliance-Assurance-Evidenz fuer Cloud-Service-Auswahl, Provider-Abhaengigkeiten, Audit-/Nachweisstand, Shared Responsibility und Betriebsnachweise.
- Alle sechs Presets enthalten ab diesem Release-Block audit-ready Spec-Kit-Run-Evidenz: `Applicable` / `N/A` / `Open`, Begruendung, Evidenzpfad, Reviewer, Restrisiko und Follow-up muessen im aktuellen Spec-Kit-Lauf dokumentiert werden.
- Alle sechs Presets sind seit 2026-05-04 im `github/spec-kit` Community-Katalog enthalten und liegen zusätzlich als veröffentlichte Repos unter `https://github.com/hindermath/spec-kit-preset-*`.
- Neue Level-2-Projekte SOLLEN bei der Spec-Kit-Initialisierung die passende Preset-Teilmenge installieren; C#/.NET-Level-2-Projekte verwenden standardmäßig alle sechs Presets, sofern keine begründete Ausnahme dokumentiert ist.
- Referenz-Rollout für alle sechs Presets: `RiderProjects/TinyPl0`, `RiderProjects/TinyCalc`, `RiderProjects/TuiVision`, `RiderProjects/InventarWorkerService`.
- Installation bevorzugt über den Community-Katalog, wenn `specify` das unterstützt; für reproduzierbare Pins die versionierten GitHub-ZIP-URLs aus `constitution.md`/`README.md` verwenden.
- `.specify/presets/` und generierte Agenten-/Command-Dateien committen, wenn Presets Projekt-Policy sind; `.specify/presets/.cache/` nie committen.
- Nach Installation oder Update prüfen: `specify preset list`, mindestens ein `specify preset info <id>`, bei Template-Fragen zusätzlich `specify preset resolve <template>`.
- Die lokale Arbeitskopie der veröffentlichten Preset-Repos liegt unter `~/SpecKitPresetProjects/`; kanonische Scaffolds in diesem Repo liegen unter `specs/spec-kit-presets/` und `specs/spec-kit-preset-repos/`.
- Verbesserungen an Presets zuerst im `home-baseline`-Scaffold einarbeiten, dann in die passenden Repos unter `~/SpecKitPresetProjects/` übertragen, committen, pushen und mit GitHub-ZIP-URL smoke-testen.
- Bei Änderungen an Preset-Regeln immer prüfen, ob `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` und `scripts/templates/*` ebenfalls aktualisiert werden müssen.
- Bei jeder Preset-Version oder Prioritätsänderung die kompakte Preset-Tabelle und ZIP-Installationsbefehle in `README.md`, die Matrix in `constitution.md`/`.specify/memory/constitution.md`, die vier Agenten-Dateien, `scripts/templates/speckit-workflow-section.md` und die Agenten-Templates gemeinsam aktualisieren.
- Community-/Katalog-Abstimmung läuft über `github/spec-kit#2362`.

*Standard preset set: `security-governance` v0.6.0 prio 10, `architecture-governance` v0.5.0 prio 20, `isaqb-architecture-governance` v0.2.0 prio 30, `a11y-governance` v0.4.0 prio 40, `cross-platform-governance` v0.2.0 prio 50, and `agent-parity-governance` v0.3.0 prio 60. `a11y-governance` v0.4.0 adds didactic inline-code-comment governance for new or changed non-trivial logic. `architecture-governance` v0.5.0 adds conditional `BSI C3A` cloud-autonomy evidence and `BSI C5` cloud-compliance assurance evidence for cloud-service selection, provider dependencies, audit/assurance status, shared responsibility, and operational evidence. `security-governance` v0.6.0 keeps conditional `AI-SBOM` evidence, language-specific secure-coding profiles, and regulatory applicability screening for NIS2, CRA, EU AI Act, and DORA: development-tool-only AI usage is `N/A`, AI runtime/product components require G7/BSI AI-SBOM cluster evidence, and private training projects record regulatory `N/A` when no regulated scope exists. All six presets now include audit-ready Spec-Kit run evidence: `Applicable` / `N/A` / `Open`, rationale, evidence path, reviewer, residual risk, and follow-up must be documented for the current Spec-Kit run. All six presets are in the `github/spec-kit` community catalog as of 2026-05-04 and are also published under `https://github.com/hindermath/spec-kit-preset-*`. New Level-2 projects should install the applicable subset; C#/.NET Level-2 projects default to all six unless a justified exception is documented. Commit `.specify/presets/` and generated agent command updates when presets are project policy, but never commit `.specify/presets/.cache/`. Verify installs with `specify preset list`, `specify preset info`, and where relevant `specify preset resolve`. Improve presets in the home-baseline scaffold first, propagate to standalone preset repos, then commit, push, and smoke-test via GitHub ZIP URL. Preset-rule changes and preset version/priority changes require reviewing constitution, README tables/install snippets, all agent guidance files, and relevant templates together. Community/catalog coordination happens in `github/spec-kit#2362`.*

<!-- SPECKIT START -->
For the next feature context, read the binding intake
`Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.md` and the Feature-024
pre-wave gate before starting Feature 028.
<!-- SPECKIT END -->

## Hinweise / Notes

- Diese Datei bleibt bewusst kompakt und ergänzt die projektspezifische Dokumentation.
- This file intentionally stays compact and complements the project-specific documentation.
