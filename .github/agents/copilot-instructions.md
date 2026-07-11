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
- 017-wave1-visual-component-remediation: Added C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, and `TuiVision.Drivers.Console`; no new packages
- 016-secure-development-hardening: Implemented the 157-control secure-development baseline, bounded persistence/script/workflow/supply-chain remediation, and local Release/coverage proof; next is `Lastenheft_Wave1-Visual-Component-Remediation.md`.
- 015-didactic-comment-hardening: Implemented selective didactic comments and audit-ready evidence without runtime, API, dependency, or example-scope changes; next is `Lastenheft_Secure-Development-Hardening.md`.


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
- The next prioritized intake is `Lastenheft_03_EditorHelpAndResourcesHardening.md` before Wave-3 visual porting.

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
- Jeder Lauf schützt den akzeptierten Scope, verwendet triggerbasierte Validierung und dokumentiert eine kurze Retrospektive für spätere Runbook-Verfeinerungen.

*Fully delegated Spec-Kit runs follow `docs/spec-kit-autonomous-runbook.md` and use the repository-local `$speckit-autonomous` skill. Determine `LocalImplementation`, `PublishPR`, or `MergeAndSync` from the current user request; general autonomy does not silently grant remote write or merge authority. Create evidence before implementation, iterate optional stages to their defined convergence criteria, prove one representative vertical slice before broad rollout, serialize shared writers, protect accepted scope, use trigger-based validation, and record a short retrospective for later workflow refinement.*
<!-- MANUAL ADDITIONS END -->

## Shared Parent Guidance

- The shared parent file `/Users/thorstenhindermann/RiderProjects/AGENTS.md` intentionally stores only repo-spanning baseline rules.
- Keep repository-specific build, test, workflow, architecture, and feature guidance in this repository's own files; when both layers exist, the repository-local files are the more specific authority.
