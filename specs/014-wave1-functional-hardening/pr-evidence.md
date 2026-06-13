# PR Evidence: 014-wave1-functional-hardening

## Setup Evidence

Deutsch: Diese Datei ist die primaere Beweismatrix fuer die funktionale
Haertung der Wave-1-Beispiele. Sie ersetzt nicht die Guides, aber sie ist die
verbindliche Review-Flaeche fuer historische Quellen, Proof-Pfade, Helper-
Klassifikation, Fallbacks, fehlende Kernfunktionen und Validierung.

English: This file is the primary proof matrix for Wave-1 functional
hardening. It does not replace the guides, but it is the binding review surface
for historical sources, proof paths, helper classification, fallbacks, missing
core functions, and validation.

- Branch: `014-wave1-functional-hardening`
- Prerequisites: `.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks`
- Result: `FEATURE_DIR=/Users/thorstenhindermann/RiderProjects/TuiVision/specs/014-wave1-functional-hardening`; docs `research.md`, `data-model.md`, `contracts/`, `quickstart.md`, `tasks.md`
- Checklists: `requirements.md` 16/16 PASS; `plan-quality.md` 36/36 PASS
- Optional before/after implement hook: `speckit.git.commit`; not auto-executed in this implementation run
- `specify check`: PASS; Specify CLI ready, Git/Claude/Codex/Gemini/Junie/opencode/Qwen/VS Code available, other optional agents absent or IDE-only
- `specify preset list`: PASS; installed presets include `security-governance` v0.5.0, `architecture-governance` v0.4.0, `isaqb-architecture-governance` v0.1.0, `a11y-governance` v0.2.0, `cross-platform-governance` v0.1.0, `agent-parity-governance` v0.2.0
- `specify preset info agent-parity-governance`: PASS; v0.2.0, enabled, 9 templates/commands, MIT license
- Baseline inventory: `examples/Desklogo/`, `examples/MsgCls/`, `examples/Tutorial/`, `examples/Videomode/`, `examples/README.md`, and `tests/TuiVision.Examples.SmokeTests/`
- Dependency baseline: no new runtime dependency, no database, no service, no network proof path, no persistent user history, no runtime/product AI; example projects and smoke project continue to reference existing TuiVision modules and MSTest only
- Out of scope: Wave-1 visual remediation, Wave 2/3/4 behavior, broad framework redesign, mouse-only operation, arbitrary user-file proof, persistent user history, databases, external services, new runtime dependencies, runtime/product AI

## Historical Source Reviews

| Area | Historical source | Reviewed purpose | Decision |
|---|---|---|---|
| Desklogo | `tv203s/contrib/tvision/examples/desklogo/desklogo.cc` | Custom `TDeskTop`/background pattern and About menu | Ported as `DesklogoApp` plus `DesklogoDesktop`; About menu remains learner exercise/follow-up, not required for current functional purpose |
| Desklogo | `set-logo.cc`, `tv_logo.cc` | Asset/generator boundary only | Replaced by embedded `LogoLines`; no runtime generator dependency |
| MsgCls | `msgcls/testdyn.cpp` | Application command path posts demo info/messages | Ported as `MsgClsApp` command plus public `PostMessage()` |
| MsgCls | `msgcls/tlnmsg.cpp`, `tlnmsg.h` | Message/info windows, broadcast commands, list insertion | Ported as focused `MsgClsWindow` accumulating string messages; info-window variant is follow-up/out of scope |
| Tutorial | `tutorial/tvguid01.cc` through `tvguid16.cc` | 16 ordered learning steps from minimal app to dialog data restore | Ported as one `ITutorialStep` per token plus `TutorialStepCatalog` and `TutorialApp` launcher |
| Videomode | `videomode/test.cc` | Terminal/window size capability, mode commands, fallback messages | Ported as `DisplayModeCoordinator`, `VideomodeView`, and startup transition attempt; shell and broad menu matrix are follow-up/out of scope |

Additional header/declaration review: `msgcls/tlnmsg.h` was required for command constants and class roles. No additional `tv203s/` headers were required for Desklogo, Tutorial, or Videomode beyond the listed implementation files.

## Tutorial Step Reviews

| Token | Historical source | Managed step path | Learning target / behavior proof | Sequence | Deviation decision |
|---|---|---|---|---:|---|
| `tvguid01` | `tutorial/tvguid01.cc` | `TvGuid01Step.cs` | Minimal `TApplication` | 1 | Executable smoke plus title/description proof |
| `tvguid02` | `tutorial/tvguid02.cc` | `TvGuid02Step.cs` | Status/menu shell basics | 2 | Executable smoke plus title/description proof |
| `tvguid03` | `tutorial/tvguid03.cc` | `TvGuid03Step.cs` | Menu command handling | 3 | Executable smoke plus title/description proof |
| `tvguid04` | `tutorial/tvguid04.cc` | `TvGuid04Step.cs` | Opening a `TWindow` | 4 | Executable smoke plus title/description proof |
| `tvguid05` | `tutorial/tvguid05.cc` | `TvGuid05Step.cs` | Drawing window content | 5 | Executable smoke plus title/description proof |
| `tvguid06` | `tutorial/tvguid06.cc` | `TvGuid06Step.cs` | Vertical scroll bar | 6 | Executable smoke plus title/description proof |
| `tvguid07` | `tutorial/tvguid07.cc` | `TvGuid07Step.cs` | Horizontal and vertical scroll bars | 7 | Executable smoke plus title/description proof |
| `tvguid08` | `tutorial/tvguid08.cc` | `TvGuid08Step.cs` | Scroll bars and delta point | 8 | Executable smoke plus title/description proof |
| `tvguid09` | `tutorial/tvguid09.cc` | `TvGuid09Step.cs` | Multiple windows | 9 | Executable smoke plus title/description proof |
| `tvguid10` | `tutorial/tvguid10.cc` | `TvGuid10Step.cs` | Opening a `TDialog` | 10 | Executable smoke plus title/description proof |
| `tvguid11` | `tutorial/tvguid11.cc` | `TvGuid11Step.cs` | Dialog buttons | 11 | Executable smoke plus title/description proof |
| `tvguid12` | `tutorial/tvguid12.cc` | `TvGuid12Step.cs` | Dialog input line | 12 | Executable smoke plus title/description proof |
| `tvguid13` | `tutorial/tvguid13.cc` | `TvGuid13Step.cs` | Two dialog buttons | 13 | Executable smoke plus title/description proof |
| `tvguid14` | `tutorial/tvguid14.cc` | `TvGuid14Step.cs` | Check boxes and radio buttons | 14 | Executable smoke plus title/description proof |
| `tvguid15` | `tutorial/tvguid15.cc` | `TvGuid15Step.cs` | Saving dialog data | 15 | Executable smoke plus title/description proof |
| `tvguid16` | `tutorial/tvguid16.cc` | `TvGuid16Step.cs` | Save and restore dialog data | 16 | Executable smoke plus title/description proof |

SC-001 / SC-002 reviewer checklist: four of four Wave-1 areas are represented, and 16 of 16 tutorial steps are represented individually.

## Smoke Proof Matrix

| Area | Smoke method(s) | Proof method | Concrete assertion | Helper classification | Executable status |
|---|---|---|---|---|---|
| Desklogo | `Desklogo_DesktopHasLogoPattern`, `Desklogo_UndersizedTerminal_NoException`, `Desklogo_StartsAndExitsCleanly` | Public `DesklogoApp.Run()` plus `DesklogoDesktop.Draw()` and stable public render metrics | Wide logo pattern, full-width default rendering, controlled clipping on 16-column terminal, clean exit | `PrimaryProof`, `SupplementalProof` | Executable smoke |
| MsgCls | `MsgCls_CommandEventRoutesLoremIpsumToWindow`, `MsgCls_HeadlessMessageRoutedToWindow`, `MsgCls_RepeatedMessageTriggerIsStable` | Public command event and public `PostMessage()` route through real app/window event handling | Command adds Lorem Ipsum, headless init routes test message, three repeated messages preserve order | `PrimaryProof`, `SetupOnly` | Executable smoke |
| Tutorial | `Tutorial_TvGuid01_StartsAndExitsCleanly` through `Tutorial_TvGuid16_StartsAndExitsCleanly`, catalog tests, unknown-token fallback | Public `TutorialApp.Run()`, `TutorialStepCatalog`, and launcher proof state | Each token maps to exact sequence, title fragment, bilingual description, `LastRunStepToken`; unknown token uses fallback | `PrimaryProof`, `SupplementalProof` | Executable smoke |
| Videomode | `Videomode_InitialTransitionOutcomeIsVisible`, `Videomode_TransitionReturnsDefinedOutcome`, `Videomode_PostTransitionUsability`, fallback tests | Public coordinator transition and view outcome state | `LastOutcome` is defined, view shows the same outcome, fallback message is text-first, app remains runnable after retry | `PrimaryProof`, `SetupOnly`, `SupplementalProof` | Executable smoke |

Current smoke audit result: previous startup-only and static metadata checks were not sufficient for 014. The hardened smokes now assert historical core behavior, public state, fallback, and helper classification.

## Helper Classification Inventory

| Helper / path | Classification | Runtime logic executed | Public surface used | Replacement responsibility |
|---|---|---|---|---|
| `AssertSmokeRunCompletes(() => app.Run())` for Wave-1 apps | `PrimaryProof` when paired with concrete state assertion | Yes | `TApplication.Run()` / `TutorialApp.Run()` | Keep; later visual remediation can add richer visible paths |
| `DefaultBounds()` | `SetupOnly` | No | Test setup helper | None |
| `DesklogoDesktop.Draw()` and render metrics | `PrimaryProof` / `SupplementalProof` | Yes | Public `Draw()`, `RenderedLineCount`, `LastVisibleLogoColumnCount` | Later visual remediation may add rendered-buffer region proof |
| `MsgClsApp.PostMessage()` | `PrimaryProof` | Yes | Public app method and broadcast route | Keep |
| `TEvent.CreateCommand(MsgClsEvents.cmPostLoremIpsum)` | `PrimaryProof` | Yes | Public command/event path | Keep |
| `TutorialStepCatalog` assertions | `SupplementalProof` | No direct runtime loop alone | Public catalog state | Keep as catalog invariant |
| `TutorialApp.LastRunStepToken` / `LastRunUsedFallback` | `PrimaryProof` when asserted after `Run()` | Yes | Public launcher state | Keep |
| `DisplayModeCoordinator.TryTransition()` | `PrimaryProof` | Yes | Public coordinator method | Keep |
| `VideomodeView.LastShownOutcome` / `LastShownMessage` | `PrimaryProof` | Yes after app constructor/transition | Public view state | Keep |

LegacyOrTemporary classifications: none in this feature. Later Wave-1 visual remediation still owns richer first-screen visual component proof.

## Negative And Fallback Proof

| Area | Path kind | Trigger | Observed fallback / proof boundary |
|---|---|---|---|
| Desklogo | `UndersizedDisplay` | `DefaultBounds(width: 16, height: 5)` | App exits cleanly; render metrics show clipped logo rows and 16 visible columns |
| MsgCls | N/A | No acceptance-relevant invalid input path in 014 | Message routing uses controlled string inputs; malformed payload semantics are outside this example |
| Tutorial | `InvalidInput` | Unknown token `tvguidXX` | Fallback app runs and `LastRunUsedFallback` is true; no step token is reported |
| Videomode | `PlatformLimitation` / `UnavailableCapability` | Terminal cannot support `Console.SetWindowSize()` or retry throws | `VisibleFallback` and text-first fallback message; real transition is environment-dependent |

## Missing-Core Decisions

| Area | Historical core point | Decision | Rationale |
|---|---|---|---|
| Desklogo | Historical About dialog and generator tools | `IntentionalDeviation` | Current functional purpose is desktop/logo rendering; generator files are asset boundary, About remains learner exercise/follow-up |
| MsgCls | Historical non-modal info window (`postInfo`) | `FollowUp` | Current example proves message routing; info-window split would broaden scope beyond current Wave-1 function |
| Tutorial | Full historical interactive behavior inside each step | `FollowUp` | 014 hardens functional traceability; visible interactive remediation remains separate |
| Videomode | Full mode/resolution menu matrix and shell command | `FollowUp` | Current proof covers capability/fallback; broad menu matrix and shell behavior are outside 014 |

## Documentation And A11Y

Updated learner-facing artifacts:

- `docs/guides/examples/desklogo.md`
- `docs/guides/examples/msgcls.md`
- `docs/guides/examples/tutorial.md`
- `docs/guides/examples/videomode.md`
- `examples/README.md`

Deutsch: Die Texte sind Deutsch zuerst und Englisch danach. Sie bleiben
Markdown-/Text-first und erklaeren historische Abweichungen, Fallbacks und
Proof-Grenzen ohne Farbcodierung oder Layout-Abhaengigkeit.

English: The text is German first and English second. It remains Markdown /
text-first and explains historical deviations, fallbacks, and proof boundaries
without depending on color or layout.

DocFX/web-a11y trigger: guides and README changed, so DocFX plus
`tests/web-a11y` validation is required.

## Governance

- Architecture: no architecture-facing runtime boundary changed; only example proof state and smoke tests changed. Architecture docs remain unchanged.
- Security: C#/.NET secure coding baseline applies; NIST SSDF and CWE Top 25 remain baseline context. ASVS, CAPEC, and Zero Trust are `N/A` because no web/API/auth/trust-boundary surface changed.
- Supply chain: no new dependency, no SBOM/VEX/SLSA change.
- AI-SBOM: `N/A`; AI is development tooling only, no runtime/product AI added.
- Governance presets applicable in the local toolchain: `security-governance` v0.5.0, `architecture-governance` v0.4.0, `isaqb-architecture-governance` v0.1.0, `a11y-governance` v0.2.0, `cross-platform-governance` v0.1.0, `agent-parity-governance` v0.2.0. The plan/task baseline named older security/architecture preset versions; implementation evidence records the actual installed versions.
- Agent guidance parity: after implementation, the shared agent files were synchronized from 014 planning state to implemented status and now point to `Lastenheft_07_Didactic-Inline-Code-Comment-Hardening.md` as the next example-adjacent step.
- Pflichtenheft: next-step marker is updated after final 014 validation.
- `Lastenheft_Wave1-Visual-Component-Remediation.md`: remains unrenamed follow-up intake and outside this feature.

## Validation Evidence

| Command | Version | Result |
|---|---|---|
| `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release --filter "FullyQualifiedName~Desklogo|FullyQualifiedName~MsgCls|FullyQualifiedName~Tutorial|FullyQualifiedName~Videomode"` | `1.14.19.49` | Sandbox blocked MSBuild IPC with `System.Net.Sockets.SocketException (13): Permission denied`; rerun required outside sandbox |
| `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release --filter "FullyQualifiedName~Desklogo|FullyQualifiedName~MsgCls|FullyQualifiedName~Tutorial|FullyQualifiedName~Videomode"` | `1.14.19.50` | PASS: 38 passed, 0 failed, 0 skipped |
| `dotnet restore` | n/a | PASS: all projects restored or already up to date |
| `dotnet build --configuration Release` | `1.14.19.51` | PASS: 0 warnings, 0 errors |
| `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release` | `1.14.19.53` | PASS: 91 passed, 0 failed, 0 skipped |
| `dotnet test --configuration Release` | `1.14.19.54` | PASS: 496 passed, 0 failed, 0 skipped across all test projects |
| `xmllint --noout coverlet.runsettings` | n/a | PASS |
| `dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings` | `1.14.19.55` | PASS: tests green; gate packages generated Cobertura reports |
| Coverage gate details | `1.14.19.55` | PASS: `TuiVision.Core` 89.78%, `TuiVision.Controls` 84.84%, `TuiVision.Serialization` 87.95%, `TuiVision.Compatibility` 80.55%, `TuiVision.Drivers.Console` 81.70% line coverage |
| `dotnet format --verify-no-changes` | n/a | PASS |
| `docfx docfx.json` | n/a | PASS: build succeeded, 0 warnings, 0 errors |
| `npm run test:docfx` | n/a | BLOCKED in this Codex environment by local HTTP server timeout while Playwright starts `python3 -m http.server` on port 8123 |
| `python3 -m http.server 8123 --bind 127.0.0.1 --directory _site` plus `PLAYWRIGHT_SKIP_WEBSERVER=1 npm test` | n/a | PASS: 2 Playwright/axe tests passed against the generated DocFX site |
| `git diff --check` | n/a | PASS before final Lastenheft archive step |
| `git diff --check` | n/a | PASS after task-list closure and final Lastenheft archive evidence update |

Generated-output hygiene: DocFX regenerated `_site/` and API metadata only as ignored generated output; no generated `_site/` or `api/*.yml` files are staged or intended for Git.

## Final PR Evidence

Changed examples:

- `examples/Desklogo/DesklogoDesktop.cs`: minimal public render metrics for line and visible-column proof.
- `examples/Tutorial/TutorialApp.cs`: public launcher proof state for selected step token and fallback path.

Changed tests:

- `tests/TuiVision.Examples.SmokeTests/ExampleTestBase.cs`: helper taxonomy extended to `PrimaryProof`, `SupplementalProof`, `SetupOnly`, and `LegacyOrTemporary`.
- Wave-1 smoke tests now assert logo/clipping, custom message command routing, tutorial token identity and learning targets, videomode capability/fallback state, and helper classifications.

Changed documentation:

- `docs/guides/examples/desklogo.md`
- `docs/guides/examples/msgcls.md`
- `docs/guides/examples/tutorial.md`
- `docs/guides/examples/videomode.md`
- `examples/README.md`
- `docs/project-statistics.md`
- `Pflichtenheft.md`
- `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`

Security and supply-chain statement: no new dependency, database, external service, network proof path, persistent user history, trust boundary, or runtime/product AI was added. AI-SBOM remains `N/A`.

Scope confirmation: Wave-1 visual remediation, Wave 2/3/4 behavior, broad framework redesign, mouse-only operation, arbitrary user-file proof, and external proof paths remain out of scope. `Lastenheft_Wave1-Visual-Component-Remediation.md` remains the unrenamed follow-up intake.

Final polish step:

- Functional hardening Lastenheft archived via `bash scripts/rename-lastenheft.sh Lastenheft_Wave1-Functional-Hardening.md 014-wave1-functional-hardening`.
- Resulting path: `Lastenheft_Wave1-Functional-Hardening.014-wave1-functional-hardening.md`.
- Script-created commit: `acfa1a5 chore: rename Lastenheft to Lastenheft_Wave1-Functional-Hardening.014-wave1-functional-hardening.md`.

Final task-list status: T001 through T079 are checked in `specs/014-wave1-functional-hardening/tasks.md`.
