# PR Evidence: Interactive Wave 2 Demos

**Feature**: `012-interactive-wave2-demos`
**PR**: [#27](https://github.com/hindermath/TuiVision/pull/27)
**Status**: Interactive runtime paths implemented and validated locally; PR body updated for final review.

This file is the repository-local proof ledger required by the specification. It is intentionally present before implementation so later tasks can append evidence without inventing the proof surface mid-feature.

## Current Planning Evidence

| Evidence Area | Current State | Reference |
|---|---|---|
| Specification | Created and clarified | `spec.md` |
| Plan | Created and reviewed | `plan.md` |
| Task plan | Created with 89 executable tasks after analysis remediation | `tasks.md` |
| Plan-quality checklist | Completed with 36 checks | `checklists/plan-quality.md` |
| Requirements checklist | Review-cleaned wording for governance/validation details | `checklists/requirements.md` |

## Setup Evidence

| Task | Evidence |
|---|---|
| T001 prerequisites | `.specify/scripts/bash/check-prerequisites.sh --json --paths-only` passed on branch `012-interactive-wave2-demos`; feature dir `/Users/thorstenhindermann/RiderProjects/TuiVision/specs/012-interactive-wave2-demos`; tasks file `/Users/thorstenhindermann/RiderProjects/TuiVision/specs/012-interactive-wave2-demos/tasks.md`. Binding Level-2 registry row: `RiderProjects/TuiVision` = `.NET 10 / C# terminal UI framework and Turbo Vision port`, build/test via `dotnet restore/build/test`, Coverlet gates, `dotnet format`, DocFX plus Playwright/axe A11Y, statistics baselines 80 and 125 lines/workday. |
| T002 restore | `dotnet restore` passed locally on 2026-05-10. No new runtime NuGet dependency was added. |
| T003 011 reusable helpers | Reused the 011 functional helpers as setup/supplemental proof: `CopyInput`, `CutInput`, `PasteIntoInput`, `InspectStandardFileDialog`, `RenderDescription`, `UpdateText`, `LoadItems`, `MoveSelection`, `RunToCompletion`, `ScrollToControl`, `ScrollToCell`, `SelectIndex`, `RunTo`, and `Abort`. |
| T004 direct-helper baseline | Existing Wave-2 smoke classes used direct helpers after a headless `app.Run()` quit path. 012 converts primary proof to queued command events and classifies remaining direct helper calls through `DirectHelperUsage.SupplementalAssertion` or `SetupOnly`. |
| T005 evidence matrix fields | The implementation matrix below now tracks historical source review, visible runtime path, app-loop smoke, guide update, and notes for all eleven examples. |

## Implementation Evidence Matrix

| Example | Historical Source Review | Visible Runtime Path | App-Loop Smoke | Guide Update | Notes |
|---|---|---|---|---|---|
| Clipboard | Complete | `Clipboard` menu: Copy, Cut, Paste, Unavailable | `Clipboard_AppLoop_Dispatches_Copy_Cut_Paste_And_Unavailable_Feedback` | Updated | Uses `ManagedClipboard`; unavailable state is visible fallback. |
| Demo | Complete | `Demo` menu: Broad, Metadata, Manual path, Cancel, Invalid path, Color/display, Omissions | `Demo_AppLoop_Dispatches_Three_Visible_Command_States` and file/path app-loop smoke | Updated | MVP vertical slice; file metadata only, no file-content proof. |
| DlgDsn | Complete | `DlgDsn` menu: Load/render, Change, Reject malformed, Reject invalid | `DlgDsn_AppLoop_Loads_Renders_Changes_And_Rejects_Descriptions` | Updated | Read-only source-controlled fixtures only. |
| DynTxt | Complete | `DynTxt` menu: Short, Long, Constrained | `DynTxt_AppLoop_Dispatches_Short_Long_And_Constrained_Text` | Updated | Short, long, constrained text states. |
| InpLis | Complete | `InpLis` menu: Load, Next, Commit, Recall, Boundary, Empty | `InpLis_AppLoop_Dispatches_Input_Selection_History_Boundary_And_Empty_Feedback` | Updated | History remains session-only. |
| ListVi | Complete | `ListVi` menu: Load, First, Last, Empty | `ListVi_AppLoop_Dispatches_Selection_Boundaries_And_Empty_Feedback` | Updated | Selection and boundary states. |
| ProgBa | Complete | `ProgBa` menu: Complete | `ProgBa_AppLoop_Dispatches_Progress_Completion` | Updated | Completion state. |
| Sdlg | Complete | `Sdlg` menu: Scroll, Focus, Boundary | `Sdlg_AppLoop_Dispatches_Vertical_Scroll_Focus_And_Boundary_Feedback` | Updated | Vertical scroll/focus state. |
| Sdlg2 | Complete | `Sdlg2` menu: Scroll both, Focus far, Boundary | `Sdlg2_AppLoop_Dispatches_TwoAxis_Scroll_Focus_And_Boundary_Feedback` | Updated | Horizontal and vertical scroll/focus state. |
| TCombo | Complete | `TCombo` menu: Load, Select, Boundary, Empty | `TCombo_AppLoop_Dispatches_Selection_Boundary_And_Empty_Feedback` | Updated | Selection, value, boundary states. |
| TProgB | Complete | `TProgB` menu: Partial, Abort, Cancelled | `TProgB_AppLoop_Dispatches_Partial_Abort_And_Cancelled_Feedback` | Updated | Partial, abort, cancelled states. |

## Historical Source Review

| Example | Source Files Reviewed | Historical Intent | 012 Interactive Outcome |
|---|---|---|---|
| Clipboard | `tv203s/contrib/tvision/examples/clipboard/test.cc`; `tv203s/contrib/tvision/include/tv/osclipboard.h` | OS-independent clipboard menu with copy/paste commands and explicit unavailable-clipboard message boxes. | Managed menu exposes copy, cut, paste, and unavailable fallback as text-first states. |
| Demo | `tvdemo1.cc`, `tvdemo2.cc`, `tvdemo3.cc`, `tvdemo.h`, `tvcmds.h`, `gadgets.cc`, `fileview.cc`, `ascii.cc`, `calendar.cc` | Broad Turbo Vision showcase with menus for about/calendar/ascii/file/color/window/gadget behavior. | Wave-2 scope exposes controls/dialog/gadget, metadata, cancel/invalid, color/display, and omission states; editor/help/stream/terminal/mouse/charset parity remains out of scope. |
| DlgDsn | `freedsgn.cc`, `dsgobjs.cc`, `propdlgs.cc`, `propedit.cc`, `strmoper.cc`, `dsgdata.h`, `dsgobjs.h` | Dialog designer loads, edits, saves, validates, and renders dialog/object descriptions. | Managed path loads/renders source-controlled fixtures, applies one visible change, and rejects malformed/invalid descriptions without user-data writes. |
| DynTxt | `dyntext.cpp`, `testdyn.cpp`, `dyntext.h` | Dynamic text control updates slave text from input and clips/right-justifies to visible bounds. | Commands show short, long clipped, and constrained-width text states. |
| InpLis | `inplist.cpp`, `test.cpp`, `inplist.h` | List item editing through focused input lines with keyboard navigation. | Commands show list load, next selection, input commit, session-only recall, boundary, and empty feedback. |
| ListVi | `lst_view.cpp`, `listbox2.cpp`, `lst_view.h`, `classes/tlistvie.cc` | TListViewer navigation, item selection broadcasts, inline editing, first/last bounds, and empty range handling. | Commands show load, first/last selection, and empty feedback through visible app state. |
| ProgBa | `example.cpp`, `tprogbar.cpp`, `tprogbar.h`, `makerez.cpp`, `readrez.cpp` | Progress dialog updates a visible bar through completion and checks cancel state. | Command reaches deterministic completion without sleeps. |
| Sdlg | `main.cpp`, `scrldlg.cpp`, `scrlgrp.cpp`, `dlg.h` | Vertical ScrollDialog/ScrollGroup keeps controls reachable outside the initial viewport. | Commands scroll, focus a lower control, and show boundary state. |
| Sdlg2 | `main.cpp`, `scrldlg.cpp`, `scrlgrp.cpp`, `dlg.h` | Two-axis ScrollDialog/ScrollGroup with horizontal and vertical limits. | Commands scroll both axes, focus a far cell, and show boundary state. |
| TCombo | `test.cpp`, `tcombobx.cpp`, `tcombobx.h`, `tcmbovwr.cpp`, `tcmbowin.cpp`, `tsinputl.cpp`, `tsinputl.h` | Combo dialog opens a selection window, syncs chosen item to an input line, and handles list boundaries/history-like input. | Commands load choices, select a visible value, retain value on invalid boundary index, and show empty choices. |
| TProgB | `calc.cpp`, `tprogbar.cpp`, `tprogbar.h` | Progress bar reacts to OK/start and cancel/abort commands. | Commands show partial progress, abort request, and separate cancelled state. |

## Command ID Plan

| Example | Reserved Command IDs |
|---|---|
| Clipboard | `12100` Copy, `12101` Cut, `12102` Paste, `12103` Unavailable |
| Demo | `12010` Broad, `12011` Metadata, `12012` Manual path, `12013` Cancel, `12014` Invalid path, `12015` Color/display, `12016` Omissions |
| DlgDsn | `12200` Load/render, `12201` Change, `12202` Reject malformed, `12203` Reject invalid |
| DynTxt | `12300` Short, `12301` Long, `12302` Constrained |
| InpLis | `12400` Load, `12401` Next, `12402` Commit, `12403` Recall, `12404` Boundary, `12405` Empty |
| ListVi | `12500` Load, `12501` Last, `12502` First, `12503` Empty |
| ProgBa | `12600` Complete |
| Sdlg | `12700` Scroll, `12701` Focus, `12702` Boundary |
| Sdlg2 | `12800` Scroll both, `12801` Focus far, `12802` Boundary |
| TCombo | `12900` Load, `12901` Select, `12902` Boundary, `12903` Empty |
| TProgB | `13000` Partial, `13001` Abort, `13002` Cancelled |

## Safety And Fixture Audit

- `examples/Demo/DemoApp.cs` uses directory metadata and wildcard file names only; `FileContentIoPerformed` remains false in the file/path app-loop smoke.
- `examples/DlgDsn/DlgDsnApp.cs` limits fixture names to a fixed allow-list under `examples/DlgDsn/Fixtures/` and rejects traversal.
- `examples/DlgDsn/Fixtures/valid.tvdialog` remains source-controlled proof data.
- `examples/InpLis/InpLisApp.cs` keeps history in memory (`HistoryText`) and does not persist user history.
- Normal startup review: all Wave-2 `Program.cs` files use console bounds with 80x25 fallback and construct the matching app normally.

## Validation Evidence

| Command | Status | Notes |
|---|---|---|
| `.specify/scripts/bash/check-prerequisites.sh --json --paths-only` | Passed for implementation | Points to `specs/012-interactive-wave2-demos` |
| `dotnet restore` | Passed | No new runtime dependency added |
| `dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Demo"` | Passed | 8 Demo tests after vertical-slice wiring |
| `dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Clipboard\|...~TProgB"` | Passed | 30 targeted Wave-2 tests after remaining runtime wiring |
| `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release` | Passed | 73 tests, 0 failed, 0 skipped |
| `git diff --check` | Passed for planning | Used for spec, plan, checklist, and task artifacts |
| `dotnet build --configuration Release` | Passed | `Directory.Build.props` incremented to `1.12.9.31`; 0 warnings, 0 errors. Final pre-commit version-alignment build also passed at `1.12.10.35` with 0 warnings and 0 errors. |
| `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release` | Passed final evidence | `Directory.Build.props` incremented to `1.12.9.32`; 73 passed, 0 failed, 0 skipped |
| `dotnet test --configuration Release` | Passed | `Directory.Build.props` incremented to `1.12.9.33`; 478 passed, 0 failed, 0 skipped across Core, Controls, Drivers, Compatibility, Serialization, and Example smoke projects |
| `dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings` | Passed | `Directory.Build.props` incremented to `1.12.9.34`; tests passed and five gate-relevant Cobertura files were attached |
| Coverage gate evidence | Passed | `TuiVision.Core` 89.78 %, `TuiVision.Controls` 84.84 %, `TuiVision.Serialization` 87.95 %, `TuiVision.Compatibility` 80.55 %, `TuiVision.Drivers.Console` 81.70 % |
| `dotnet format --verify-no-changes` | Passed | No formatting changes required |
| `docfx docfx.json` | Passed | 185 models, 180 HTML files, 0 warnings, 0 errors; generated `_site/` and generated `api/*.yml` remain uncommitted/ignored |
| `npm run test:docfx` in `tests/web-a11y/` | Passed | DocFX rebuild passed and Playwright/Axe smoke passed 2/2 |
| Manual startup checks | Passed | `dotnet run --project examples/<Name> --configuration Release --no-build` started and quit cleanly for `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB` |
| `git diff --check` | Passed final evidence | Clean after implementation, docs, statistics, and Lastenheft rename |
| PR description | Passed | Created `specs/012-interactive-wave2-demos/pr-description.md` and updated <https://github.com/hindermath/TuiVision/pull/27> with `gh pr edit 27 --body-file ...` |
| Lastenheft rename | Passed with commit-safe execution | Renamed to `Lastenheft_Interactive-Wave2-Demos.012-interactive-wave2-demos.md` via `git mv`; the repository script was not invoked because it performs an immediate `git commit`, while this implementation turn keeps all changes uncommitted for review |

## Governance Evidence

- `Pflichtenheft.md` now marks Welle 2 Controls/Dialoge plus the interactive Showcase stage complete and moves `>>> NAECHSTER SCHRITT <<<` to Welle 3 editor/file/help/stream examples.
- `docs/project-statistics.md` records the 012 implementation scope, validation set, coverage values, manual baseline, and blended repository speedup; the final ASCII summary now includes `14 012i`.
- `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md` were reviewed and synchronized to the implemented 012 status and next Wave-3 scope.
- Architecture and security proof surfaces were updated with the unchanged-risk rationale: no new runtime dependency, database, external service, user-data write path, or persisted user history.

## Review Cleanup Notes

- 2026-05-10: Added this evidence ledger before implementation so all references to `pr-evidence.md` resolve in the PR.
- 2026-05-10: Replaced local absolute links in planning artifacts with repository-relative links.
- 2026-05-10: Reworded the requirements checklist to distinguish user-facing behavioural requirements from required governance and validation-evidence details.
- 2026-05-10: Remediated `/speckit-analyze` findings by adding explicit Release build/test evidence, pre-test version/build-counter ordering, PR-description completion evidence, and the scripted Lastenheft rename as the final Polish step.
- 2026-05-10: The `.specify/extensions.yml` `after_implement` hook remains optional (`speckit.git.commit`) and was not executed automatically; implementation commit was performed manually after explicit user approval.
