# TuiVision Development Guidelines

Auto-generated from all feature plans. Last updated: 2026-07-11

## Active Technologies
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions for existing CI validation (008-controls-revision)
- In-memory UI state only in production; source-controlled planning, tests, and proof artifacts in `specs/`, `tests/`, and `docs/`; no database or external service storage (008-controls-revision)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation delivered in `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`) plus existing widget/input primitives (`TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, current `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions and existing `tests/TuiVision.Examples.SmokeTests/` infrastructure as downstream contex (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell, dialog, file, color, history, and widget types (`TDialog`, `TFileDialog`, `TFileList`, `TDirListBox`, `TFileInfo`, `TFileInputLine`, `THistory`, `TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup`, `TColorDisplay`, `TComboBox`, `TProgressBar`, `TParamText`); existing `TuiVision.Serialization` archive/resource foundation (`TRecordRegistry`, `TRecordSerializer`, `TBinaryArchiveReader`, `TBinaryArchiveWriter`, `TResourceFile`, `TResourceCollection`, `pstream` family); MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (010-standard-dialogs-designer)
- In-memory dialog state and session-only history; real local file-system metadata for file-listing/validation only; source-controlled tests/proof artifacts; minimal persisted dialog-description fixture through existing serialization/resource primitives; no database or external service storage (010-standard-dialogs-designer)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing framework modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; new wave-2 example projects under `examples/`; existing `tests/TuiVision.Examples.SmokeTests/`; MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (011-port-wave2-examples)
- Runtime example state is in memory; standard-dialog file flows use real local file-system metadata only; `dlgdsn` may use source-controlled dialog-description fixtures through existing Serialization/resource primitives; no database, external service, persisted user history, or new dependency planned (011-port-wave2-examples)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet test stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned. (012-interactive-wave2-demos)
- Runtime example state is in memory. Dialog-designer and file/path demonstrations use source-controlled fixtures, fixed repository paths, or test temporary directories. The examples must not persist user history, write user data as part of normal demonstration, read arbitrary user file contents as proof, or add a database/external service. (012-interactive-wave2-demos)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only; shared example support in `examples/Shared/Wave2Runtime.cs`; existing MSTest/Coverlet, DocFX, and Playwright/axe tooling. No new runtime NuGet dependency was added. (013-wave2-visual-component-remediation)
- Runtime example state remains in memory. Controlled examples may use source-controlled fixtures, fixed repository paths, or test temporary directories for metadata, rendering, validation, or rejection proof. The feature must not add a database, external service, network dependency, persistent user history, or arbitrary user-file content reads. (013-wave2-visual-component-remediation)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency was added. (014-wave1-functional-hardening implemented)
- Runtime example state remains in memory. Proof data is limited to existing source-controlled files, controlled example fixtures if needed, or test temporary directories. No database, external service, network dependency, persistent user history, arbitrary user-file content reads, or runtime/product AI storage was added. (014-wave1-functional-hardening implemented)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest/Coverlet validation; existing DocFX plus Playwright/axe web A11Y tooling when documentation triggers apply. No new runtime NuGet dependency is planned. (015-didactic-comment-hardening)
- Source-controlled Markdown evidence and guidance files only. Production code state and tests keep their current storage model. No database, external service, network dependency, persistent user history, runtime/product AI storage, or arbitrary user-file proof path is planned. (015-didactic-comment-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`); Bash and PowerShell 7 for repository tooling + Existing TuiVision projects, MSTest, Coverlet, DocFX, Playwright/axe, GitHub Actions, Gitleaks, and CycloneDX for .NET 6.2.0 as a repository-local tool. No new runtime package is planned. (016-secure-development-hardening)
- Source-controlled Markdown, YAML, shell/PowerShell scripts, a local .NET tool manifest, and test fixtures. Generated evidence is written to temporary or ignored directories. No database, service, credential, runtime AI, or user-data store is introduced. (016-secure-development-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing TuiVision modules, shared Wave-1 example composition, MSTest/Coverlet, DocFX, and Playwright/axe; no new package (017-wave1-visual-component-remediation)
- Runtime example state remains in-process and session-only; proof and governance use source-controlled Markdown, with no database, external service, persistent user history, arbitrary user-file proof, or runtime/product AI (017-wave1-visual-component-remediation)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing Controls/Serialization modules, MSTest/Coverlet, DocFX, and Playwright/axe; no new package (018-editor-help-resources-hardening)
- Runtime state is bounded to managed editor/help/resource models and deterministic temporary test files; no example port, database, external service, ambient locale dependency, arbitrary user-file proof, or runtime/product AI (018-editor-help-resources-hardening)
- C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, and `TuiVision.Drivers.Console`; no new packages (019-wave3-visual-component-porting)
- Embedded/source-controlled learning content and test-owned temporary files only (019-wave3-visual-component-porting)
- C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Drivers.Console`, `TuiVision.Controls`, and reviewed `TuiVision.Compatibility` key translation; no new packages (021-terminal-charset-hardening)
- In-memory session/history state plus source-controlled JSON and raw 8x16 fixtures (021-terminal-charset-hardening)
- C# / .NET 10 for durable test-only validation; Markdown and JSON for evidence + existing framework assemblies, MSTest, `System.Text.Json`, Git, official pinned FPC source checkout (024-tv203-freevision-conformance-audit)
- repository-owned JSON and Markdown evidence; external Free Vision worktree under `/tmp`, never tracked (024-tv203-freevision-conformance-audit)

- C# `latest` on .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, `TuiVision.Compatibility`; MSTest; Coverlet; docfx; wave-1 example projects under `examples/` (007-port-wave1-examples — Wave 1 delivered)

## Project Structure

```text
src/
tests/
examples/
docs/guides/examples/
```

## Commands

```bash
dotnet restore
dotnet build --configuration Release
dotnet test
dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
dotnet test tests/TuiVision.Examples.SmokeTests/
cd tests/web-a11y && npm install && npx playwright install chromium && npm run test:docfx
docfx docfx.json && cd tests/web-a11y && npm run test:docfx
```

## Code Style

C# `latest` on .NET 10 (`net10.0`): Follow standard conventions. All XML docs and explanatory comments must be bilingual: German first, English second, CEFR-B2 readability. Large normative documents such as `Pflichtenheft*.md` and `Lastenheft*.md` may use a synchronized English sidecar with suffix `.EN.md` instead of an oversized inline-bilingual file; the German version remains canonical unless explicitly marked otherwise. Follow `Programmierung #include<everyone>`: guides, statistics, examples, and generated API docs must stay usable in text-first assistive setups such as Braille displays, screen readers, and text browsers. Generated HTML documentation should target WCAG 2.2 conformance level AA. Keep the Playwright + `@axe-core/playwright` smoke checks in `tests/web-a11y/` aligned with the current DocFX structure and use `lynx` as an extra text-browser spot check when available. Every successful `docfx docfx.json` regeneration must be followed by the matching `tests/web-a11y/` A11y smoke check in the same work item. Treat bilingual CEFR-B2 delivery plus the documented A11Y proof path as formal completion criteria for learner-facing documentation and active requirement artifacts. New or changed non-trivial logic must be reviewed for didactic inline-comment value when it affects learner understanding or maintainability, especially central framework flows and smoke-test helpers. Inline comments explain why a decision, trade-off, constraint, historical deviation, or proof boundary exists; do not add comments that merely restate obvious code. Keep inline-comment intensity moderate: normally 1-3 lines before a non-trivial block, with German-first/English-second CEFR-B2 text for didactic explanation blocks.

GitHub Pages is published from `.github/workflows/pages.yml`: build root `docfx.json`, run the `tests/web-a11y/` Playwright + axe smoke path, upload `_site/` as a Pages artifact, and keep `_site/` plus generated `api/*.yml` files out of Git.

## Recent Changes
- 024-tv203-freevision-conformance-audit: Added C# / .NET 10 for durable test-only validation; Markdown and JSON for evidence + existing framework assemblies, MSTest, `System.Text.Json`, Git, official pinned FPC source checkout
- 021-terminal-charset-hardening: Added C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Drivers.Console`, `TuiVision.Controls`, and reviewed `TuiVision.Compatibility` key translation; no new packages
- 019-wave3-visual-component-porting: Added C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, and `TuiVision.Drivers.Console`; no new packages


<!-- MANUAL ADDITIONS START -->
## 016 Secure Development Delivery Context

- Final evidence is in `specs/016-secure-development-hardening/pr-evidence.md`; the durable matrix records all 157 controls with complete status, ownership, risk, evidence, and re-evaluation fields.
- Remediation is bounded to malformed persistence rejection, immutable workflow dependencies, supply-chain automation, root disclosure guidance, and Bash/PowerShell archive-script parity.
- Local acceptance is 498/498 Release tests and coverage above 70% for all five required assemblies; DocFX/axe and remote OS/CI proof remain delivery gates.
- Human legal, provider, organization, and agent-platform decisions remain `Open`; release provenance, reproducible-build/lock maturity, and RFC 9116 remain named follow-ups.
- The next open prioritized intake is `Lastenheft_03_EditorHelpAndResourcesHardening.md` before Wave-3 visual porting.

## 017 Wave-1 Visual Component Remediation Delivery Context

- The implementation is complete; final evidence is in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- `Desklogo`, `MsgCls`, all 16 Tutorial tokens, and `Videomode` use a visible main state, real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` with concrete state, view-tree identity, and rendered buffer/cell evidence. The matrix contains four app rows and 16 unique Tutorial rows.
- `examples/Shared/Wave1Runtime.cs` composes existing controls. Desklogo and MsgCls use `UseExistingFramework`; Tutorial and Videomode use bounded `IntentionalDeviation` decisions.
- Historical `tv203s/` sources remain read-only. No cross-wave behavior, broad framework redesign, new dependency, persistence, external service, or runtime/product AI entered scope.
- The complete example-smoke suite passes 101/101 locally; repository, coverage, DocFX, A11Y, and remote checks remain delivery gates until recorded.
- Feature 018 closes the editor/help/resources intake; the next prioritized intake is `Lastenheft_Wave3-Visual-Component-Porting.md`.

## 018 Editor, Help, and Resources Hardening Delivery Context

- Final evidence is in `specs/018-editor-help-resources-hardening/pr-evidence.md`.
- Existing editor/file and runtime-help controls are retained and proven through coherent application paths.
- `THelpSourceCompiler` supports bounded `.topic` source, strict UTF-8, deterministic symbols, forward references, stable diagnostics, and atomic failure.
- `TLocalizedResourceLookup` uses exact language, caller-ordered fallbacks, and neutral exact keys without ambient locale or gettext dependencies.
- Resource and Help deserialization reject duplicate/negative structures and invalid reference graphs before publication.
- Historical `tv203s/` sources remain read-only; Wave-3 examples, mouse, terminal/charset, broad redesign, and dependencies remain out of scope.
- The next prioritized intake is `Lastenheft_Wave3-Visual-Component-Porting.md`.

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
- After Feature 023 delivery, Wave 5 remains the next fachliche intake; do not start Feature 024 as part of this run.

### 024-tv203-freevision-conformance-audit
- Current implementation status: audit-only Feature 024 is complete; final evidence is in `specs/024-tv203-freevision-conformance-audit/pr-evidence.md`.
- Borland documentation and `tv203s/` remain primary; official Free Vision commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` is external secondary evidence only.
- The exact inventory is 151 historical `.cc` rows, 119 maintained production `.cs` files, 176 exported public types, 16 domains, and 48 contracts.
- Decisions are 13 `Aligned`, 34 `IntentionalModernization`, 1 `ConsciouslyOmitted`, 0 `BehavioralDrift`, and 0 `EvidenceGap`; Free Vision relations are recorded independently.
- `Core025` and `ComponentData026` owner sets are empty, so no Feature 025 or 026 branch or PR may be created. `027-pre-wave5-conformance-closure` is the mandatory next intake and Wave 5 remains blocked.
- No runtime, API, dependency, package, example, generated-output, or historical-source change entered 024; the external Free Vision worktree remains outside Git.

### 027-pre-wave5-conformance-closure
- Current implementation status: the mandatory local closure is complete; final evidence is in `specs/027-pre-wave5-conformance-closure/closure-evidence.md`.
- The merged Feature-024 baseline revalidated exactly: 16 domains, 48 contracts, inventories 151/119/176, 15 external source records, 94 proof references, decisions 13/34/1/0/0, and zero findings.
- Features 025 and 026 remain suppressed because their accepted owner sets are empty. Any later drift or evidence gap requires a reviewed audit revision rather than an implicit runtime repair.
- Focused audit, 698/698 full Release tests, all five coverage gates, format, DocFX/Axe/Lynx, secret, and protected-scope checks passed locally.
- No runtime, public API, dependency, package, example behavior, historical source, or Wave-5 implementation change entered 027. Wave 5 is the next fachliche intake after the reviewed 027 merge.


### Autonomous Red-Proof Completeness
- Before the first red test batch, review imports, public XML docs, harness helpers, focus/ownership assertions, and linked-source assembly identity.
- Group independent negative cases only as a bounded project-local red matrix with explicit failure boundaries and shared ownership.
- When source is linked into multiple assemblies, cross-project proof uses public contracts or state delegates and does not assume one CLR type identity.
- Keep a causal closeout evidence-only and single-commit-capable: do not require its own PR URL, reviewed-head result, or merge commit inside that same repository file; verify terminal facts externally without recursive closeout.
- When push and pull-request events create equivalent workflow sets, use pull-request-context checks as the gate and record duplicate runs as noise; cancel them only under an explicit safe workflow/concurrency contract.

  Groessere verpflichtende Beispielwellen sollen als zweistufiges Spec-Kit-
  Liefermuster geplant werden, wenn funktionale Portierung und interaktive
  Runtime-Politur sonst vermischt wuerden. Stufe 1 portiert das Verhalten,
  schliesst Framework-Voraussetzungen, liefert deterministische Headless- oder
  In-Process-Smoke-Pfade und markiert bewusst verschobene Interaktion als
  Follow-up. Stufe 2 haengt dieselben Funktionen an sichtbare Menues,
  Statuszeilen, Desktop-Controls, Dialoge, Tastaturpfade und skriptbare
  UI-Event-Smoke-Tests. Eine Beispielwelle ist erst nach Stufe 2 voll
  lern- und reviewtauglich, sofern der Scope nicht ausdruecklich nur einen
  minimalen nicht-interaktiven Nachweis verlangt.
  Larger mandatory example waves should use a two-stage Spec-Kit delivery
  pattern when functional porting and interactive runtime polish would
  otherwise be mixed. Stage 1 ports behavior, closes framework prerequisites,
  provides deterministic headless or in-process smoke paths, and marks deferred
  interactivity as follow-up. Stage 2 wires the same proven functions into
  visible menus, status lines, desktop controls, dialogs, keyboard paths, and
  scripted UI-event smoke tests. A wave is fully learner- and review-ready only
  after Stage 2 unless the scope explicitly asks for a minimal non-interactive
  proof.
  Kuenftige Lastenhefte fuer Beispielwellen oder beispielnahe Framework-
  Vorhaertungen muessen ein Framework-Usage- und Remediation-Gate enthalten.
  Der spaetere Spec-Kit-Lauf muss pro Beispiel oder Vertragsbereich
  dokumentieren, welche bestehende TuiVision-Framework-Komponente genutzt wird,
  ob lokale Sonderlogik existiert und welche Entscheidung gilt:
  `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation` oder
  `FollowUpHardening`. Wiederverwendbare Logik darf nicht dauerhaft als lokale
  `examples/`-Sonderloesung verbleiben; wiederholte oder Framework-Verhalten
  ersetzende Logik gehoert in einen kleinen Framework-Fix oder in ein eigenes
  Follow-up-Hardening.
  Future Lastenhefte for example waves or example-adjacent framework hardening
  must include a framework-usage and remediation gate. The later Spec-Kit run
  must document, per example or contract area, which existing TuiVision
  framework component is used, whether local special logic exists, and which
  decision applies: `UseExistingFramework`, `SmallFrameworkFix`,
  `IntentionalDeviation`, or `FollowUpHardening`. Reusable logic must not
  remain permanently as a local `examples/` special solution; move repeated or
  framework-replacing logic into a small framework fix or a dedicated follow-up
  hardening item.
  Fuer jede Spec-Kit-Feature-Implementierung mit historisch abgeleitetem
  Turbo-Vision-Verhalten muessen die relevanten Implementierungsdateien unter
  `tv203s/` (`.c`, `.cc`) als Read-only-Referenz geprueft werden. Wenn
  Deklarationen, Konstanten, Makros, Datenlayout, Vererbung oder Signaturen
  wichtig sind, muessen auch passende C/C++-Header (`.h`, `.hpp`, `.hh`)
  einbezogen werden. Wesentliche nutzer- oder API-sichtbare Abweichungen werden
  in Spec, Plan, Tasks, Guide, PR-Evidence oder Architektur-/Security-Nachweis
  dokumentiert; ohne historischen Bezug genuegt ein kurzes `N/A`.
  For every Spec-Kit feature implementation with historically derived Turbo
  Vision behavior, review the relevant implementation files under `tv203s/`
  (`.c`, `.cc`) as read-only reference. When declarations, constants, macros,
  data layout, inheritance, or signatures matter, also include matching C/C++
  headers (`.h`, `.hpp`, `.hh`). Document material user-visible or API-visible
  deviations in spec, plan, tasks, guide, PR evidence, or architecture/security
  evidence; if there is no historical relevance, a short `N/A` is sufficient.
  Schnittstellen, Qualitätsattribute, Laufzeitverhalten, Deployment,
  Wartbarkeit oder technische Schulden betrifft, muss `spec.md`, `plan.md`
  oder `tasks.md` festhalten, ob Evidenz unter `docs/architecture/`
  erforderlich ist. `N/A` braucht eine kurze Begruendung.
  quality attributes, runtime behavior, deployment, maintainability, or
  technical debt, `spec.md`, `plan.md`, or `tasks.md` must state whether
  evidence under `docs/architecture/` is required. `N/A` needs a short
  rationale.
  TuiVision-Coverage-Gate-Konfiguration. Coverage-Gates muessen aus dem
  Repository-Root mit `dotnet test --collect:"XPlat Code Coverage" --settings
  coverlet.runsettings` gemessen werden; die Datei muss bei Aenderungen an
  gate-relevanten Assemblies, Beispiel-Assemblies oder Testprojekten gepflegt
  und nach Moeglichkeit mit `xmllint --noout coverlet.runsettings` validiert
  werden.
  configuration. Coverage gates must be measured from the repository root with
  `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`;
  keep the file in sync when gate-relevant assemblies, example assemblies, or
  test projects change, and validate it with `xmllint --noout
  coverlet.runsettings` where available.

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
- Optional installiert: `autonomous-run-governance` v0.1.0 prio 70 aus dem öffentlichen Tag-ZIP. Der projektgebundene Codex-Skill `$speckit-autonomous` bleibt am selben eindeutigen Pfad als lokaler Override bestehen, weil er TuiVision-spezifische Nummerierungs-, Build-Zähler-, DocFX-/A11Y- und historische Source-Verträge ergänzt; Preset-Command, Retrospektiv-Skill, Runbook und Adoption-Evidence bleiben die portablen beziehungsweise gemeinsamen Nachweisflächen.
- Jeder Lauf schützt den akzeptierten Scope, verwendet triggerbasierte Validierung und dokumentiert eine kurze Retrospektive für spätere Runbook-Verfeinerungen.

*Fully delegated Spec-Kit runs follow `docs/spec-kit-autonomous-runbook.md` and use the repository-local `$speckit-autonomous` skill. Determine `LocalImplementation`, `PublishPR`, or `MergeAndSync` from the current user request; general autonomy does not silently grant remote write or merge authority. Create evidence before implementation, iterate optional stages to their defined convergence criteria, prove one representative vertical slice before broad rollout, serialize shared writers, require each remote or delivery task to name its exact repository evidence path, route self-invalidating reviewed-head facts to one named closeout path after verifying them before merge, protect accepted scope, use trigger-based validation, and record a short retrospective for later workflow refinement.*

*Optional installed preset: `autonomous-run-governance` v0.1.0 at priority 70 comes from the public tag ZIP. The project-owned Codex `$speckit-autonomous` skill stays at its single path as a local override because it adds TuiVision-specific numbering, build-counter, DocFX/A11Y, and historical-source contracts. The preset command, retrospective skill, runbook, and adoption evidence remain the portable or shared proof surfaces.*
- Aktive Google-Agentenoberfläche ist Antigravity CLI mit Befehl `agy` und Spec-Kit-Integration `agy`. `GEMINI.md` und `~/.gemini/antigravity-cli/` bleiben Antigravity-kompatible Oberflächen; direkte `gemini`-Befehle sind nur historische oder ausdrücklich benötigte Enterprise-/API-Kompatibilität und keine lokale Pflicht.

*The active Google agent surface is Antigravity CLI through the `agy` command and Spec Kit `agy` integration. `GEMINI.md` and `~/.gemini/antigravity-cli/` remain Antigravity-compatible surfaces; direct `gemini` commands are historical or explicitly required enterprise/API compatibility, not a local requirement.*
<!-- MANUAL ADDITIONS END -->

## Shared Parent Guidance

- The shared parent file `/Users/thorstenhindermann/RiderProjects/AGENTS.md` intentionally stores only repo-spanning baseline rules.
- Keep repository-specific build, test, workflow, architecture, and feature guidance in this repository's own files; when both layers exist, the repository-local files are the more specific authority.
