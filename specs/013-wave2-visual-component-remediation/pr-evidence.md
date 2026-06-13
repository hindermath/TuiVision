# PR Evidence: 013 Wave 2 Visual Component Remediation

**Branch**: `013-wave2-visual-component-remediation`
**Evidence start**: 2026-05-30
**Scope**: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, `TProgB`

## 1. Setup Evidence

Deutsch: Die Implementierung startet auf dem richtigen 013-Zweig. Die Spec-Kit-Pruefung zeigt auf den erwarteten Feature-Ordner. Alle Plan- und Requirements-Checklisten sind abgeschlossen. Der optionale `before_implement`-Hook `speckit.git.commit` wurde erkannt, aber nicht ausgefuehrt, weil kein Commit angefordert wurde.

English: Implementation starts on the correct 013 branch. The Spec-Kit prerequisite check points to the expected feature directory. All plan and requirements checklists are complete. The optional `before_implement` hook `speckit.git.commit` was detected but not executed because no commit was requested.

| Check | Result |
|---|---|
| Current branch | `013-wave2-visual-component-remediation` |
| Prerequisites command | `.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks` |
| Feature directory | `/Users/thorstenhindermann/RiderProjects/TuiVision/specs/013-wave2-visual-component-remediation` |
| Available docs | `research.md`, `data-model.md`, `contracts/`, `quickstart.md`, `tasks.md` |
| `requirements.md` checklist | 16 total, 16 complete, 0 incomplete, PASS |
| `plan-quality.md` checklist | 40 total, 40 complete, 0 incomplete, PASS |
| `specify preset list` | `security-governance` v0.4.0, `architecture-governance` v0.2.0, `isaqb-architecture-governance` v0.1.0, `a11y-governance` v0.2.0, `cross-platform-governance` v0.1.0, `agent-parity-governance` v0.2.0 all enabled |
| `dotnet restore` | PASS: all projects up to date |
| New runtime dependencies | None added for setup |

## 2. Evidence Matrix

| Example | Visible target | Status feedback | `Help -> Description` | Rendered proof | Direct-helper role | Evidence link |
|---|---|---|---|---|---|---|
| `Clipboard` | `TInputLine` for copy/cut/paste/unavailable fallback | Real `Wave2StatusLine` (`TStatusLine`) | `Help -> Description` command/menu | PASS: view-tree plus buffer/cell snapshot | Setup/supplemental only | Runtime proof table |
| `Demo` | `TDialog` and `TWindow` families for dialog/control, file/path metadata, display/color/gadget | Real `Wave2StatusLine` (`TStatusLine`) | `Help -> Description` command/menu | PASS: view-tree plus buffer/cell snapshot | Setup/supplemental only | Runtime proof table |
| `DlgDsn` | Runtime `TDialog` tree plus visible rejection dialog | Real `Wave2StatusLine` (`TStatusLine`) | `Help -> Description` command/menu | PASS: view-tree plus buffer/cell snapshot | Setup/supplemental only | Runtime proof table |
| `DynTxt` | Dynamic `TStaticText` view with clipped/constrained content | Real `Wave2StatusLine` (`TStatusLine`) | `Help -> Description` command/menu | PASS: view-tree plus buffer/cell snapshot | Setup/supplemental only | Runtime proof table |
| `InpLis` | `TDialog` composition with `TListBox`, `TInputLine`, history/boundary text | Real `Wave2StatusLine` (`TStatusLine`) | `Help -> Description` command/menu | PASS: view-tree plus buffer/cell snapshot | Setup/supplemental only | Runtime proof table |
| `ListVi` | `TDialog` composition with `TListBox` and empty/boundary text | Real `Wave2StatusLine` (`TStatusLine`) | `Help -> Description` command/menu | PASS: view-tree plus buffer/cell snapshot | Setup/supplemental only | Runtime proof table |
| `ProgBa` | `TProgressBar` through completion | Real `Wave2StatusLine` (`TStatusLine`) | `Help -> Description` command/menu | PASS: view-tree plus buffer/cell snapshot | Setup/supplemental only | Runtime proof table |
| `Sdlg` | Visible vertical `TScrollGroup` | Real `Wave2StatusLine` (`TStatusLine`) | `Help -> Description` command/menu | PASS: view-tree plus buffer/cell snapshot | Setup/supplemental only | Runtime proof table |
| `Sdlg2` | Visible two-axis `TScrollGroup` | Real `Wave2StatusLine` (`TStatusLine`) | `Help -> Description` command/menu | PASS: view-tree plus buffer/cell snapshot | Setup/supplemental only | Runtime proof table |
| `TCombo` | `TDialog` composition with managed `TComboBox`/input value and empty/boundary text | Real `Wave2StatusLine` (`TStatusLine`) | `Help -> Description` command/menu | PASS: view-tree plus buffer/cell snapshot | Setup/supplemental only | Runtime proof table |
| `TProgB` | `TWindow` progress dialog with `TProgressBar` partial/abort/cancel states | Real `Wave2StatusLine` (`TStatusLine`) | `Help -> Description` command/menu | PASS: view-tree plus buffer/cell snapshot | Setup/supplemental only | Runtime proof table |

## 3. Starting Baseline and Gap Scan

Deutsch: Die 012-Basis hat bereits Headless-Konstruktoren, `QueueEvents(...)`, `app.Run()`-basierte Smoke-Pfade und sichtbare Textfolgen. Die primaeren Smokes pruefen jedoch ueberwiegend `VisibleHistory` oder `VisibleText` und damit noch keinen stabilen gerenderten Komponenten-Nachweis. Mehrere Apps erzeugen Framework-Controls (`TProgressBar`, `TComboBox`, `TScrollGroup`) nur als Hilfsobjekte oder interne Gruppen, ohne sie konsequent als primäre gerenderte Zielkomponente in der Beweismatrix zu pruefen. `Help -> Description` und dynamische echte `TStatusLine`-Meldungen fehlen in der 012-Basis.

English: The 012 baseline already has headless constructors, `QueueEvents(...)`, `app.Run()` smoke paths, and visible text histories. The primary smokes still mostly assert `VisibleHistory` or `VisibleText`, so they do not yet provide stable rendered component proof. Several apps create framework controls (`TProgressBar`, `TComboBox`, `TScrollGroup`) only as helper objects or internal groups, without consistently proving them as the primary rendered target component. `Help -> Description` and dynamic real `TStatusLine` messages are missing from the 012 baseline.

Required remediation:

- Add or expose real visible component targets for all eleven examples.
- Keep `VisibleText` and `VisibleHistory` as supporting text-first evidence only.
- Add real `TStatusLine` feedback or record a blocking deviation.
- Add canonical `Help -> Description` command/menu path in every app.
- Extend smoke helpers with view-tree plus buffer/cell rendered-visibility assertions.
- Harden the matrix so every primary smoke records rendered proof and helper classification.

## 4. Shared Smoke Helper Review

Deutsch: `ExampleTestBase.cs` already provides app-loop tracking and direct-helper classification, but it lacks reusable view-tree and buffer/cell assertions. `InteractiveSmokeEventScript.cs` supports command and explicit-event sequences, but it does not yet document quit-path expectations or offer a canonical description-command helper. These are the required shared changes for T018-T020 and T059-T060.

English: `ExampleTestBase.cs` already provides app-loop tracking and direct-helper classification, but it lacks reusable view-tree and buffer/cell assertions. `InteractiveSmokeEventScript.cs` supports command and explicit-event sequences, but it does not yet document quit-path expectations or provide a canonical description-command helper. These are the required shared changes for T018-T020 and T059-T060.

T018-T020 implementation note: `ExampleTestBase.cs` now provides view-tree target assertions, full-buffer rendered assertions, region-based buffer/cell assertions, and buffer-to-text conversion. `InteractiveSmokeEventScript.cs` now composes deterministic event sequences via `ThenCommands(...)` and `ThenEvents(...)` while documenting that the headless app owns the quit path. `Wave2InteractiveSmokeMatrixTests.cs` now has explicit matrix fields for visible target, status feedback, description path, rendered proof status, direct-helper classification, and evidence link for all eleven examples.

## 5. Historical Source Reviews

Deutsch: Alle historischen Dateien wurden nur gelesen. `tv203s/` bleibt unveraendert. Die C#-Zielzustaende uebernehmen die sichtbare Absicht, nicht die alte Implementierung zeilenweise.

English: All historical files were read-only references. `tv203s/` remains unchanged. The C# target states carry the visible intent, not a line-by-line copy of the old implementation.

| Example | Historical sources reviewed | Original visual intent | C# target state | Intentional deviations |
|---|---|---|---|---|
| `Clipboard` | `tv203s/contrib/tvision/examples/clipboard/test.cc`; `tv203s/contrib/tvision/include/tv/osclipboard.h` | Demonstrate OS-independent clipboard copy/paste flows and unavailable-clipboard error dialogs. | Visible text/input component showing copy, cut, paste, and unavailable fallback state. | Uses deterministic managed clipboard/test fallback instead of live OS clipboard integration for smoke proof. |
| `Demo` | `tvdemo1.cc`, `tvdemo2.cc`, `tvdemo3.cc`, `tvdemo.h`, `tvcmds.h`, `gadgets.cc`, `fileview.cc`, `ascii.cc`, `calendar.cc` under `tv203s/contrib/tvision/examples/demo/` | Broad Turbo Vision showcase: desktop windows, dialogs, file/path views, color selection, gadgets, help contexts, ASCII/calendar/calculator/puzzle examples. | Three required visual families: dialog/control, file/path metadata, display/color/gadget state. | Editor/help-stream/terminal/mouse/charset and full puzzle/calendar/calculator behavior remain documented out of scope for Wave 2. |
| `DlgDsn` | `freedsgn.cc`, `dsgobjs.cc`, `propdlgs.cc`, `propedit.cc`, `strmoper.cc`, `dsgdata.h`, `dsgobjs.h` | Interactive dialog designer with editable dialog/control objects, property dialogs, stream persistence, load/save, and generated code. | Controlled dialog-description render path plus visible rejection for malformed/incomplete/duplicate/invalid fixtures. | Uses source-controlled marker fixtures and managed serialization roundtrip; no arbitrary file open/save, no persistent user history, no full designer UI. |
| `DynTxt` | `dyntext.cpp`, `testdyn.cpp`, `dyntext.h` | `DynamicText` view updates rendered text from an input line and demonstrates clipping/alignment. | Dynamic text view with short, clipped long, and constrained-width rendered states. | Uses deterministic commands instead of a live editable modal dialog for primary proof. |
| `InpLis` | `inplist.cpp`, `test.cpp`, `inplist.h` | Input dialog with list box, focused item editing, scrollbar, and value synchronization. | Visible dialog-style composition with list, input value, session-only history, boundary, and empty feedback. | Uses bounded in-memory list/history state and deterministic commands instead of modal item editing. |
| `ListVi` | `lst_view.cpp`, `listbox2.cpp`, `lst_view.h`, `tv203s/contrib/tvision/classes/tlistvie.cc` | Scrolling list viewers/list boxes with focus, selection, scrollbar coordination, and optional input-line synchronization. | Visible list viewer/list box region with selected item, first/last boundary, and empty state. | Uses one list proof surface instead of the full dual-list historical dialog. |
| `ProgBa` | `example.cpp`, `tprogbar.cpp`, `tprogbar.h`, `makerez.cpp`, `readrez.cpp` | Progress-bar control rendered inside dialogs, including streaming/resource examples. | Visible progress bar driven to completion with bounded deterministic state. | Streaming resource creation/loading is not part of 013; Serialization/resource behavior is covered elsewhere. |
| `Sdlg` | `main.cpp`, `scrldlg.cpp`, `scrlgrp.cpp`, `dlg.h` under `examples/sdlg/` | Vertical scroll dialog/group with controls outside the first viewport and focus-driven scrolling. | Visible vertical scroll group state with control outside initial viewport plus focus/boundary proof. | Uses deterministic scroll/focus commands instead of modal keyboard traversal. |
| `Sdlg2` | `main.cpp`, `scrldlg.cpp`, `scrlgrp.cpp`, `dlg.h` under `examples/sdlg2/` | Horizontal and vertical scroll dialog/group with far cells/controls and two-axis movement. | Visible two-axis scroll group state with horizontal and vertical offsets plus focus/boundary proof. | Uses deterministic scroll/focus commands instead of modal keyboard traversal. |
| `TCombo` | `test.cpp`, `tcombobx.cpp`, `tcombobx.h`, `tcmbovwr.cpp`, `tcmbowin.cpp`, `tsinputl.cpp`, `tsinputl.h` | Combo-box extension built from input line, combo trigger, combo window, and list viewer, including static input-line selection. | Visible input-plus-combo composition with loaded choices, selected value, boundary retention, and empty state. | Uses the current managed `TComboBox` instead of recreating the historical popup window implementation. |
| `TProgB` | `calc.cpp`, `tprogbar.cpp`, `tprogbar.h` | Progress dialog with start and cancel buttons; progress updates and closes on completion/cancel. | Visible progress dialog/window state with partial, abort, and cancelled states. | Long-running work is simulated deterministically; no blocking work loop or real-time animation in smoke proof. |

## 5.1 Controlled Fixture and Data Boundaries

Deutsch: `DlgDsn`-Proof verwendet nur `examples/DlgDsn/Fixtures/*.tvdialog`, deren erlaubte Namen in `DlgDsnApp.KnownFixtureNames` begrenzt sind. Clipboard-Proof nutzt `ManagedClipboard` plus einen deterministischen unavailable fallback. Datei-/Pfadbeweise verwenden source-controlled oder Test-Temp-Pfade nur fuer Metadaten; Dateiinhalte werden nicht als Proof gelesen. Es wird keine Nutzer-History persistiert.

English: `DlgDsn` proof uses only `examples/DlgDsn/Fixtures/*.tvdialog`, constrained by `DlgDsnApp.KnownFixtureNames`. Clipboard proof uses `ManagedClipboard` plus a deterministic unavailable fallback. File/path proof uses source-controlled or test-temporary paths for metadata only; file contents are not read as proof. No user history is persisted.

## 6. Runtime and Smoke Proof

Deutsch: Alle primaeren Wave-2-Smokes laufen ueber `app.Run()` mit injizierten `TEvent`-Command-Sequenzen. `VisibleText` und `VisibleHistory` bleiben nur konkrete Zustandsassertionen; der primaere Paritaetsnachweis enthaelt zusaetzlich den aufgezeichneten View-Typ und eine Region im gerenderten BackBuffer. Es gibt keine Status- oder Description-Abweichung: alle elf Beispiele verwenden eine echte `Wave2StatusLine` auf Basis von `TStatusLine` und einen `Help -> Description`-Befehl im Help-Menue.

English: All primary Wave 2 smokes run through `app.Run()` with injected `TEvent` command sequences. `VisibleText` and `VisibleHistory` remain concrete state assertions only; the primary parity proof also includes the recorded view type and a region in the rendered back buffer. There is no status or description deviation: all eleven examples use a real `Wave2StatusLine` based on `TStatusLine` and a `Help -> Description` command in the Help menu.

| Example | Primary smoke method | Concrete state assertions | View-tree proof | Buffer/cell proof | Status/description smoke |
|---|---|---|---|---|---|
| `Clipboard` | `Clipboard_AppLoop_Dispatches_Copy_Cut_Paste_And_Unavailable_Feedback` | copied, cut, pasted, unavailable fallback | `TInputLine` | rendered fallback input contains `isolated fallback` | `Clipboard_AppLoop_Shows_StatusLine_And_HelpDescription` |
| `Demo` | `Demo_AppLoop_Renders_Dialog_Control_Family`, `Demo_AppLoop_Renders_File_Path_Metadata_Family`, `Demo_AppLoop_Renders_Display_Color_Gadget_Family` | controls/dialogs/gadgets, `notes.txt`, `blue-on-black` | `TDialog`, `TWindow` | rendered regions contain family-specific text | `Demo_AppLoop_Shows_StatusLine_And_HelpDescription` |
| `DlgDsn` | `DlgDsn_AppLoop_Loads_Renders_Changes_And_Rejects_Descriptions` | rendered, changed, malformed rejection, invalid-navigation rejection | `TDialog` | rendered rejection region contains `invalid-navigation` | `DlgDsn_AppLoop_Shows_StatusLine_And_HelpDescription` |
| `DynTxt` | `DynTxt_AppLoop_Dispatches_Short_Long_And_Constrained_Text` | short, clipped long, constrained text | `TStaticText` | rendered region contains `const` | `DynTxt_AppLoop_Shows_StatusLine_And_HelpDescription` |
| `InpLis` | `InpLis_AppLoop_Dispatches_Input_Selection_History_Boundary_And_Empty_Feedback` | selection, commit, recall, boundary, empty | `TDialog` | rendered region contains `empty list` | `InpLis_AppLoop_Shows_StatusLine_And_HelpDescription` |
| `ListVi` | `ListVi_AppLoop_Dispatches_Selection_Boundaries_And_Empty_Feedback` | selected last, selected first, empty list | `TDialog` | rendered region contains `empty list` | `ListVi_AppLoop_Shows_StatusLine_And_HelpDescription` |
| `ProgBa` | `ProgBa_AppLoop_Dispatches_Progress_Completion` | completed 10/10 and completed state | `TProgressBar` | rendered progress bar contains `=` | `ProgBa_AppLoop_Shows_StatusLine_And_HelpDescription` |
| `Sdlg` | `Sdlg_AppLoop_Dispatches_Vertical_Scroll_Focus_And_Boundary_Feedback` | Control 19, focused Control 32, Control 40 | `TScrollGroup` | rendered region contains `Control 40` | `Sdlg_AppLoop_Shows_StatusLine_And_HelpDescription` |
| `Sdlg2` | `Sdlg2_AppLoop_Dispatches_TwoAxis_Scroll_Focus_And_Boundary_Feedback` | Cell 12/09, focused Cell 04/14, Cell 29/19 | `TScrollGroup` | rendered region contains `Cell 29/19` | `Sdlg2_AppLoop_Shows_StatusLine_And_HelpDescription` |
| `TCombo` | `TCombo_AppLoop_Dispatches_Selection_Boundary_And_Empty_Feedback` | loaded choices, selected Gamma, retained boundary, empty | `TDialog` | rendered region contains `empty choices` | `TCombo_AppLoop_Shows_StatusLine_And_HelpDescription` |
| `TProgB` | `TProgB_AppLoop_Dispatches_Partial_Abort_And_Cancelled_Feedback` | progress 4/10, abort requested, cancelled | `TWindow` | rendered region contains `cancelled state visible` | `TProgB_AppLoop_Shows_StatusLine_And_HelpDescription` |

Fast smoke evidence:

- `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release --filter "FullyQualifiedName~ClipboardSmokeTests|FullyQualifiedName~DynTxtSmokeTests|FullyQualifiedName~InpLisSmokeTests|FullyQualifiedName~ListViSmokeTests|FullyQualifiedName~TComboSmokeTests|FullyQualifiedName~ProgBaSmokeTests|FullyQualifiedName~TProgBSmokeTests|FullyQualifiedName~DlgDsnSmokeTests|FullyQualifiedName~SdlgSmokeTests|FullyQualifiedName~Sdlg2SmokeTests|FullyQualifiedName~DemoSmokeTests|FullyQualifiedName~Wave2InteractiveSmokeMatrixTests"` -> PASS, 47 passed, 0 failed, 0 skipped.
- `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release --filter "FullyQualifiedName~Wave2InteractiveSmokeMatrixTests"` -> PASS, 4 passed, 0 failed, 0 skipped.
- T061 exact fast smoke command after version alignment to `1.13.10.42`: `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release` -> PASS, 89 passed, 0 failed, 0 skipped.
- Direct-helper-only proof is not primary: `Wave2InteractiveSmokeMatrix_PrimaryProof_Is_Not_TextOnly_Or_DirectHelperOnly` rejects pending rendered proof, missing view-tree proof, missing buffer/cell proof, `VisibleText`/`VisibleHistory` as primary targets, and supplemental direct helper use as primary proof.

## 7. Documentation and Governance Evidence

Deutsch: Die elf Beispiel-Guides wurden German-first/English-second mit
CEFR-B2-nahem, text-first Inhalt ergaenzt. Jeder 013-Abschnitt nennt sichtbare
Hauptkomponente, Bedienpfad, echte `TStatusLine`, `Help -> Description`,
A11Y-Reviewpfad, historische Quelle und bewusste Abweichung. `examples/README.md`
beschreibt den neuen 013-Status fuer die gesamte Welle 2. Die
`wave2-guide-review-notes.md` halten die Cross-Example-Pruefung fest.

English: The eleven example guides were extended German-first/English-second
with CEFR-B2-oriented, text-first content. Each 013 section names the visible
main component, operation path, real `TStatusLine`, `Help -> Description`,
A11Y review path, historical source, and intentional deviation.
`examples/README.md` describes the new 013 status for all Wave 2 examples.
`wave2-guide-review-notes.md` records the cross-example review.

| Example | Guide | Runtime path | Primary smoke trace |
|---|---|---|---|
| `Clipboard` | `docs/guides/examples/clipboard.md` | `TInputLine`, status line, `Help -> Description` | `ClipboardSmokeTests` |
| `Demo` | `docs/guides/examples/demo.md` | `TDialog`/`TWindow`, status line, `Help -> Description` | `DemoSmokeTests` |
| `DlgDsn` | `docs/guides/examples/dlgdsn.md` | Runtime/rejection `TDialog`, status line, `Help -> Description` | `DlgDsnSmokeTests` |
| `DynTxt` | `docs/guides/examples/dyntxt.md` | `TStaticText`, status line, `Help -> Description` | `DynTxtSmokeTests` |
| `InpLis` | `docs/guides/examples/inplis.md` | List/input `TDialog`, status line, `Help -> Description` | `InpLisSmokeTests` |
| `ListVi` | `docs/guides/examples/listvi.md` | List `TDialog`, status line, `Help -> Description` | `ListViSmokeTests` |
| `ProgBa` | `docs/guides/examples/progba.md` | `TProgressBar`, status line, `Help -> Description` | `ProgBaSmokeTests` |
| `Sdlg` | `docs/guides/examples/sdlg.md` | Vertical `TScrollGroup`, status line, `Help -> Description` | `SdlgSmokeTests` |
| `Sdlg2` | `docs/guides/examples/sdlg2.md` | Two-axis `TScrollGroup`, status line, `Help -> Description` | `Sdlg2SmokeTests` |
| `TCombo` | `docs/guides/examples/tcombo.md` | Combo `TDialog`, status line, `Help -> Description` | `TComboSmokeTests` |
| `TProgB` | `docs/guides/examples/tprogb.md` | Progress `TWindow`, status line, `Help -> Description` | `TProgBSmokeTests` |

Governance notes for T077-T082:

- T077 architecture: `docs/architecture/architecture-vision.md`, `docs/architecture/runtime-view.md`, `docs/architecture/quality-scenarios.md`, and `docs/architecture/architecture-risks.md` were reviewed and updated for 013. The architecture impact is intentionally local to Wave-2 example compositions, the shared `Wave2Runtime` helper, and test/evidence surfaces; no new runtime architecture layer, host model, or external service boundary was introduced.
- T078 security posture: `docs/security/security-checklist.md`, `docs/security/threat-model.md`, `docs/security/dependency-audit.md`, and `docs/security/asvs-verification.md` were reviewed against NIST SSDF/CWE and `security-governance` v0.4.0. Risk remains unchanged because the feature adds no network, database, persistence, arbitrary user-file reads, authentication, authorization, or new runtime dependency.
- T079 supply chain and applicability: `docs/security/supply-chain-evidence.md`, `docs/security/zero-trust-applicability.md`, and `docs/security/samm-assessment.md` were reviewed and updated. AI-SBOM remains `N/A` because AI is only development tooling; Rust/Go/Swift/Java/Kotlin/Python/TypeScript secure-coding profiles are not applicable to this C#/.NET feature.
- T080 Pflichtenheft: `Pflichtenheft.md` now records the completed Wave-2 visible-component proof and keeps the next-step marker on the highest-priority open Wave-3 work.
- T081 statistics: `docs/project-statistics.md` records the 013 implementation scope, focused smoke evidence, manual baselines, and refreshed final `## Gesamtstatistik` diagrams while keeping the final top-level section at the end of the file.
- T082 agent guidance: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md` were reviewed together and updated for the changed 013 implementation status plus the new shared `examples/Shared/Wave2Runtime.cs` project-structure surface.

## 8. Validation Evidence

Final validation log:

- T083 version alignment: `Directory.Build.props` set to branch version `1.13.10.43` before the final build/test command sequence.
- T084 `dotnet build --configuration Release` at `1.13.10.43` -> PASS, 0 warnings, 0 errors.
- T085 `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release` at `1.13.10.44` -> PASS, 89 passed, 0 failed, 0 skipped.
- T086 `dotnet test --configuration Release` at `1.13.10.45` -> PASS, 494 passed, 0 failed, 0 skipped (`Core` 44, `Serialization` 18, `Drivers` 37, `Compatibility` 18, `Controls` 288, `Examples` 89).
- T087 `dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings` at `1.13.10.46` -> PASS, 494 passed, 0 failed, 0 skipped. Gate-scoped Cobertura package line rates: `TuiVision.Core` 89.78 %, `TuiVision.Controls` 84.84 %, `TuiVision.Serialization` 87.95 %, `TuiVision.Compatibility` 80.55 %, `TuiVision.Drivers.Console` 81.70 %. All are above the 70 % gate. The Example-Smoke project emitted a collector warning, but Example-Smokes are not one of the five gate-scoped assemblies and the required coverage files were produced for the gate modules.
- T088 `dotnet format --verify-no-changes` -> PASS, exit code 0 with no required formatting changes.
- T089 `docfx docfx.json` -> PASS, build succeeded with 0 warnings and 0 errors. Generated-output hygiene: `git status --short _site api` produced no tracked or untracked Git changes, and `git ls-files _site api` is empty.
- T090 `npm run test:docfx` from `tests/web-a11y/` -> PASS. The command rebuilt DocFX with 0 warnings and 0 errors, then Playwright/axe reported 2 passed Chromium smoke tests.
- T091 manual startup checks -> PASS. Each scoped command started in a PTY with `--no-build`, rendered a visible first screen with menu/status text including `Help -> Description`, and exited with `Ctrl+Q` code 0: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`. Primary visible operation paths and `Help -> Description` content remain proven by the corresponding app-loop smoke methods listed in section 6.
- T092 `git diff --check` -> PASS, exit code 0 with no whitespace errors.

## 9. Final Feature Evidence

Changed examples:

- `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB` now expose visible main components or stable visual runtime states, real `TStatusLine` feedback, and keyboard-reachable `Help -> Description`.
- `examples/Shared/Wave2Runtime.cs` provides the shared Wave-2 status/menu/layout helpers linked into the eleven scoped projects.
- Primary smoke proof is app-loop based and includes concrete state assertions, view-tree proof, and buffer/cell rendered-visibility proof. `VisibleText`, `VisibleHistory`, and direct helpers are supplemental only.

Representative visual snippets from PTY/manual and buffer proof:

- `Clipboard`: `Clipboard: copy, cut, paste, and fallback feedback`; status `Clipboard: ready | Help -> Description | ^Q Quit`.
- `Demo`: `Demo: Wave-2 controls/dialogs showcase`; status `Demo: ready | Help -> Description | ^Q Quit`.
- `DlgDsn`: framed `Dialog description` view and source-controlled fixture/rejection text.
- `ProgBa`/`TProgB`: visible progress-bar regions with deterministic completion, partial, abort, and cancelled states.
- `Sdlg`/`Sdlg2`: visible scroll-group cells/controls plus vertical and two-axis scroll proof.

Security and scope statement:

- Security risk remains unchanged: no database, external service, network dependency, authentication/authorization path, persistent user history, arbitrary user-file content read, or new runtime NuGet dependency was added.
- AI-SBOM remains `N/A` because AI is used only as development/agent tooling, not as runtime/product AI.
- No Wave-3/Wave-4 behavior, mouse-only path, broad framework redesign, or unrelated framework feature was added.

T094 Lastenheft archival:

- User approved the script commit after the initial no-commit blocker was recorded.
- Before the commit, `Directory.Build.props` was aligned for the eleventh 013 branch commit to `1.13.11.46`.
- Command: `bash scripts/rename-lastenheft.sh Lastenheft_Wave2-Visual-Component-Remediation.md 013-wave2-visual-component-remediation`.
- Result: PASS, commit `cafcca2` (`chore: rename Lastenheft to Lastenheft_Wave2-Visual-Component-Remediation.013-wave2-visual-component-remediation.md`).
- Resulting Lastenheft path: `Lastenheft_Wave2-Visual-Component-Remediation.013-wave2-visual-component-remediation.md`.
- Post-T094 regular feature commit preparation: `Directory.Build.props` was aligned for the twelfth 013 branch commit to `1.13.12.46` before staging and committing the remaining feature files.

## 10. PR Review Remediation

Copilot review comments `discussion_r3329102655` and `discussion_r3329102658`
reported that `scripts/check-homogeneity.sh` printed extra JSON objects in
`--json` mode for `GIT-SCOPE-001` and `GIT-SCOPE-002`, while the script contract
requires one final summary JSON object. The direct `printf` calls were removed.
Both warnings now flow only through `emit_result` and the final summary JSON.
`Directory.Build.props` was aligned for the thirteenth 013 branch commit to
`1.13.13.46`.

Review-fix validation:

- Command: `./scripts/check-homogeneity.sh --json --dry-run .`
- Result: PASS for the review-specific JSON contract on stdout. The command
  writes exactly one final summary JSON object and no longer emits separate
  `GIT-SCOPE-001` or `GIT-SCOPE-002` JSON records.
- Existing caveat outside this review fix: stderr still reports missing
  `hg_scan` helper functions because `scripts/lib/hg-*.sh` is not present in
  this repository snapshot. No homogeneity-framework repair was made in this
  Copilot-comment remediation.
