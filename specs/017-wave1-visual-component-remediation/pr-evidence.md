# PR Evidence: Wave-1 Visual Component Remediation

**Feature**: `017-wave1-visual-component-remediation`  
**Binding input**: `Lastenheft_Wave1-Visual-Component-Remediation.md`  
**Archived input**: `Lastenheft_Wave1-Visual-Component-Remediation.017-wave1-visual-component-remediation.md`  
**Started**: 2026-07-11  
**Owner**: Thorsten Hindermann / TuiVision maintainers  
**Reviewer**: GitHub PR reviewer  
**Baseline HEAD**: `2f7faa0f6e1eca35359a9ea377f6545ca549ce34`

## Scope Summary

Feature 017 is the visible second stage for Desklogo, MsgCls, Tutorial
`tvguid01` through `tvguid16`, and Videomode. Feature 014 remains the accepted
functional baseline. The required runtime model is visible main state, real
`TStatusLine`, and keyboard-reachable `Help -> Description`, with primary proof
through the real application loop plus state, view-tree, and rendered cells.

Excluded: functional re-porting, Wave-2/3/4 delivery, broad framework redesign,
new dependencies, mouse-only operation, persistence, arbitrary user-file proof,
external services, runtime/product AI, generated output, and historical edits.

## Preflight

| Check | Result | Evidence |
|---|---|---|
| Branch | Pass | `017-wave1-visual-component-remediation` |
| Feature marker | Pass | `.specify/feature.json` points to the 017 directory |
| `specify check` | Pass | Specify CLI 0.8.3 ready; required Git/Codex/Claude tools detected |
| Prerequisites | Pass | Feature directory and research/data-model/contracts/quickstart/tasks resolved |
| Feature checklists | Pass | 6 files, 120 complete items, 0 incomplete |
| Constitution/AGENTS | Pass | Read before implementation; TuiVision Level-2 and project rules apply |

## Review Area Evidence

| AreaId | PathOrFlow | HistoricalSource | MainSurface | StatusSurface | DescriptionPath | Operation | PrimarySmoke | ConcreteState | ViewTreeProof | RenderedProof | HelperUsage | FrameworkDecision | Deviation | ProofBoundary | FollowUp |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| W1-DESK | `examples/Desklogo/` | `desklogo.cc`; generator context `set-logo.cc`, `tv_logo.cc` | Logo desktop | Real `TStatusLine` | `Help -> Description` | Startup, description, quit | `Desklogo_AppLoop_Renders_Logo_And_StatusLine` | Full or clipped logo metrics | `DesklogoDesktop` | Block glyphs, status row, description region | `None` primary | `UseExistingFramework` | Embedded logo replaces generators | Proves visible intersection, not generator execution | None |
| W1-MSG | `examples/MsgCls/` | `testdyn.cpp`, `tlnmsg.cpp`, `tlnmsg.h` | Message window | Real `TStatusLine` | `Help -> Description` | Command, broadcast, repeat, quit | `MsgCls_AppLoop_Renders_RoutedMessage_Status_And_Description` | Ordered message list | `TWindow` | Description region and status row | `None` primary | `UseExistingFramework` | One managed window replaces historical split | Proves routed result, not direct helper alone | Historical info-window split bounded |
| W1-TUT | `examples/Tutorial/` | `tvguid01.cc` through `tvguid16.cc` | 16 representative lesson states | Real `TStatusLine` | `Help -> Description` | Token, action, description, quit | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | 16 unique signatures | Step-specific types | Token cells in stable regions | `None` primary | `IntentionalDeviation` | Modern representative cumulative controls | Proves 16 states, not token text alone | Full line-by-line recreation excluded |
| W1-VID | `examples/Videomode/` | `test.cc` | Capability result view | Real `TStatusLine` | `Help -> Description` | Probe, retry, description, quit | `Videomode_AppLoop_Renders_ProbeResult_Status_And_RemainsUsable` | Four canonical state classes | `VideomodeView` | State/result region and status row | `None` primary | `IntentionalDeviation` | Host capability differs | Proves honest state, not uniform resize success | Historical mode/shell matrix bounded |
| W1-SHARED | `examples/Shared/Wave1Runtime.cs` | N/A: presentation composition | Shared status/help/region policy | Drawable final row | Shared Help menu | Called by four apps | Four app smokes | Session-only message | Real `TStatusLine` | Stable rows/regions | None | `UseExistingFramework` | Separate Wave-1 composition | No framework replacement or duplication | Re-evaluate on framework defect |
| W1-PROOF | `tests/TuiVision.Examples.SmokeTests/` | Historical links per matrix row | App-loop proof targets | Status assertions | Description assertions | Event/command/key scripts | 20-row matrix | Concrete state | Required and passed | Required and passed | `None` primary | `UseExistingFramework` | Managed in-process proof instead of screenshots | Rejects startup/static/helper-only proof | None |

## Tutorial Visual Matrix

| Token | HistoricalSource | HistoricalIntent | PlannedVisibleTarget | Status | PrimarySmoke | ViewAndRenderProof | Result |
|---|---|---|---|---|---|---|---|
| `tvguid01` | `tutorial/tvguid01.cc` | Minimal application shell | Desktop lesson panel and lifecycle state | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TStaticText`; token cells in stable region | Pass |
| `tvguid02` | `tutorial/tvguid02.cc` | Status-line item | Real status-line lesson and key hint | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TStaticText`; token cells in stable region | Pass |
| `tvguid03` | `tutorial/tvguid03.cc` | Menu and command handling | Menu command with visible result window | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TWindow`; token cells in stable region | Pass |
| `tvguid04` | `tutorial/tvguid04.cc` | Window insertion | `TWindow` on desktop | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TWindow`; token cells in stable region | Pass |
| `tvguid05` | `tutorial/tvguid05.cc` | Drawing inside a window | Custom-drawn lesson view | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TWindow`; token cells in stable region | Pass |
| `tvguid06` | `tutorial/tvguid06.cc` | Scrollable content introduction | Vertical scroll bar and clipped content | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TWindow`; token cells in stable region | Pass |
| `tvguid07` | `tutorial/tvguid07.cc` | Improved two-axis content | Horizontal and vertical scroll bars | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TWindow`; token cells in stable region | Pass |
| `tvguid08` | `tutorial/tvguid08.cc` | Scroller delta | Scrollable view with visible offset | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TScrollGroup`; token cells in stable region | Pass |
| `tvguid09` | `tutorial/tvguid09.cc` | Multiple panes | Two visible content panes | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TWindow`; token cells in stable region | Pass |
| `tvguid10` | `tutorial/tvguid10.cc` | Resize constraints | Window with visible size-limit state | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TWindow`; token cells in stable region | Pass |
| `tvguid11` | `tutorial/tvguid11.cc` | Dialog introduction | Non-modal dialog composition | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TDialog`; token cells in stable region | Pass |
| `tvguid12` | `tutorial/tvguid12.cc` | Modal dialog behavior | Modal-result state | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TDialog`; token cells in stable region | Pass |
| `tvguid13` | `tutorial/tvguid13.cc` | Dialog buttons | Dialog with two command buttons | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TDialog`; token cells in stable region | Pass |
| `tvguid14` | `tutorial/tvguid14.cc` | Labels and choices | Label, check boxes, and radio buttons | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TDialog`; token cells in stable region | Pass |
| `tvguid15` | `tutorial/tvguid15.cc` | Input line | Dialog with visible input state | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TInputLine`; token cells in stable region | Pass |
| `tvguid16` | `tutorial/tvguid16.cc` | Data transfer and validation | Save/restore plus rejection state | Real status | `Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps` | `TDialog`; token cells in stable region | Pass |

## Feature 014 Baseline Inventory

| Area | Accepted functional behavior | Existing proof classification | Visual responsibility in 017 |
|---|---|---|---|
| Desklogo | Embedded logo draws wide and clips safely; app quits | Primary app run plus supplemental render metrics | Real status/description and buffer-region proof |
| MsgCls | Command/broadcast and repeated messages preserve order | Primary command/public route; setup headless message | Visible trigger/result/status/description through app loop |
| Tutorial | 16 exact tokens, catalog identity, fallback, clean runs | Primary app run plus supplemental catalog | 16 representative components/states and render matrix |
| Videomode | Defined coordinator/view outcome and fallback; retry remains usable | Primary state plus setup/supplemental capability paths | Visible probe/retry, canonical user state, status/description |

## Historical Read-Only Boundary

The required source inventory contains 23 files: three Desklogo, three MsgCls,
16 Tutorial, and one Videomode file. SHA-256 values were captured before source
edits; the complete command result is reproducible with `shasum -a 256` over the
paths listed in `tasks.md` T013-T016. Baseline `git diff -- tv203s/` is empty.
Representative digests: `desklogo.cc` starts `e9fbf12b`, `testdyn.cpp` starts
`6b57b4c`, `tvguid01.cc` starts `e6de66a6`, `tvguid16.cc` starts `088391f8`,
and `videomode/test.cc` starts `be369127`.

## Shared Pattern Review

- `Wave2Runtime.cs` proves that a small example-owned drawable status helper and
  Help menu can compose existing framework controls. Its Wave-2 name and scope
  prevent direct reuse in Wave 1.
- `ExampleTestBase.cs` already provides app-loop markers, helper classification,
  view-tree assertions, full-buffer text, and stable-region conversion.
- `InteractiveSmokeEventScript.cs` already queues deterministic command/key
  sequences and quit behavior. It is changed only if a generic Wave-1 gap is
  demonstrated by a failing test.
- Final decision: shared Wave-1 presentation composition, not a framework API;
  all four vertical slices and the acceptance matrix pass.

## Foundational Historical And Design Decisions

### Historical intent reconciliation

- `tvguid01` remains the minimal application shell.
- `tvguid02.cc` explicitly introduces a status-line item; the current managed
  title says menu bar and therefore requires a learner-facing metadata correction.
- `tvguid03.cc` introduces the menu; current command-handling wording is retained
  only together with the visible menu/command result.
- `tvguid04` and `tvguid05` correctly represent window insertion and drawing.
- `tvguid06` and `tvguid07` are cumulative content/drawing/resize steps in the
  source; the managed scrollbar lessons remain an intentional modern
  representation and must name that deviation.
- `tvguid08` correctly represents scrolling delta.
- `tvguid09.cc` introduces multiple panes, not three unrelated Z-order windows;
  the visible 017 target follows the historical pane intent.
- `tvguid10.cc` adds resize limits; the current managed dialog title is corrected.
- `tvguid11.cc` introduces a non-modal dialog and `tvguid12.cc` makes it modal;
  current managed button/input titles are corrected for the learner-facing path.
- `tvguid13` and `tvguid14` correctly add buttons and selection controls.
- `tvguid15.cc` adds an input line; current managed save-data title is corrected.
- `tvguid16` correctly demonstrates data transfer, save/restore, and validation.

The 16 existing `ITutorialStep.CreateApp` implementations remain the feature-014
functional regression surface. Feature 017 adds the normal-launch visual factory
without changing that public metadata/factory contract.

### Commands and stable regions

| Area | Commands | Initial/status state | Stable proof region |
|---|---|---|---|
| Desklogo | `CmDescription=17001`, existing quit | Embedded logo; source/fallback plus Help/Quit hint | Desktop/logo intersection; last terminal row for status |
| MsgCls | existing post commands; `CmDescription=17011` | Routed-message count and last message | Message window interior; last terminal row |
| Tutorial | `CmPrimary=17021`, `CmDescription=17022` | Token, sequence, goal, representative state | Factory main window/panel; last terminal row |
| Videomode | `CmProbe=17031`, `CmDescription=17032` | Canonical capability result and retry hint | Result window interior; last terminal row |

Description windows use the same bounded main region as the current app, contain
German-first/English-second CEFR-B2 text, and are closed by the scripted quit
route. The shared region helper clips proof rectangles to the active desktop.

### Shared ownership decision

`examples/Shared/Wave1Runtime.cs` is `UseExistingFramework`: it composes existing
`TStatusLine`, `TMenuBar`, `TWindow`, `TStaticText`, and geometry primitives.
It does not implement framework event routing or duplicate the helper in four
projects. No `SmallFrameworkFix` is justified by the baseline review.

### Didactic comment candidates

| Area | Initial decision | Reason |
|---|---|---|
| Shared status drawing and clipping | `CommentNeeded` | Explain why explicit cells and clipped regions are proof-stable |
| Tutorial token-to-view factory | `CommentNeeded` | Explain representative cumulative intent and non-1:1 historical mapping |
| Videomode outcome mapping | `UpdateExistingComment` | Explain honest capability classification, not obvious assignment |
| Straight menu/command declarations | `NoCommentNeeded` | Names and framework types are self-explanatory |
| App-loop buffer assertions | `CommentAdequate` | Existing feature-015 helper comments already explain proof boundaries |

## Framework Decision Register

| Area | CandidateDecision | Rationale | EvidencePath | FinalDecision |
|---|---|---|---|---|
| Desklogo | `UseExistingFramework` | Existing desktop, menu, status, and window primitives are sufficient | `examples/Desklogo/`; targeted smoke | Final: full/clipped logo plus description without mutation |
| MsgCls | `UseExistingFramework` | Existing command/broadcast/window primitives are sufficient | `examples/MsgCls/`; vertical-slice smoke | Final: complete visible route without framework change |
| Tutorial | `IntentionalDeviation` | Representative modern controls preserve cumulative lesson intent without full C++ re-port | Tutorial matrix and guide | Final: 16 unique token/view/render signatures |
| Videomode | `IntentionalDeviation` | Host capability must be reported honestly instead of forcing historical modes | coordinator/view proof | Final: four canonical states, retry, status, description, and stable rendered region |
| Shared composition | `UseExistingFramework` | Example-owned composition can reuse existing controls without a new public API | `examples/Shared/Wave1Runtime.cs` | Final: no framework change needed |
| Shared smoke proof | `UseExistingFramework` | Existing generic smoke helpers cover state, view, buffer, and app-loop proof | `ExampleTestBase.cs`; matrix | Final for vertical slice; matrix review remains |

## Governance Evidence

All rows use owner `TuiVision maintainers`, reviewer `GitHub PR reviewer`, and
review date `2026-07-11`. Results are closed or name a concrete validation gate;
no empty starter decision remains.

| RunId | PresetName | PresetVersion | Checkpoint | Applicability | Rationale | EvidencePath | Owner | Reviewer | ReviewDate | Result | ResidualRisk | FollowUp | ReevaluationTrigger |
|---|---|---:|---|---|---|---|---|---|---|---|---|---|---|
| 017-G01 | security-governance | 0.6.0 | NIST SSDF | Applicable | Managed UI/test changes require secure-development traceability | Changed C# files and this evidence | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | Pass: scoped changes and validation are traceable | Input/fallback mistakes possible | Final local and remote gates | Any runtime/source change |
| 017-G02 | security-governance | 0.6.0 | CWE Top 25 / C# profile | Applicable | Generated and edited C# must avoid unsafe input/state handling | Changed C# and smoke files | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | Pass: token, terminal, and state boundaries reviewed | Low local UI exposure | Final analyzer/test gates | Any new input/I/O path |
| 017-G03 | security-governance | 0.6.0 | OWASP ASVS | N/A | No web/API/HTTP/auth surface | `docs/security/asvs-verification.md` | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | N/A confirmed | None identified | None | Web/API/auth enters scope |
| 017-G04 | security-governance | 0.6.0 | SBOM/VEX/SLSA/OpenSSF | Applicable existing baseline | No dependency/package/release change; existing evidence remains authoritative | `docs/security/supply-chain-evidence.md` | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | Pass: baseline unchanged | Existing repository release risk | Preserve baseline | Dependency, packaging, provenance, or release flow changes |
| 017-G05 | security-governance | 0.6.0 | AI-SBOM | N/A | AI is development tooling only | This evidence | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | N/A confirmed | None in product | None | Model, dataset, inference, AI service, or runtime AI enters product |
| 017-G06 | security-governance | 0.6.0 | NIS2/CRA/EU AI Act/DORA | N/A | Local training examples add no market, customer, AI-runtime, critical-sector, or financial ICT scope | `docs/security/regulatory-applicability.md` | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | N/A confirmed; legal classification remains human-owned | Legal classification remains human-owned | None | Release/market/customer/AI/financial scope changes |
| 017-G07 | architecture-governance | 0.5.0 | STRIDE/CIA | Applicable review | Runtime flow changes require confirmation that no trust/data boundary changes | Architecture docs and this evidence | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | Pass: no trust, confidentiality, integrity, or availability boundary changed | UI state correctness | Preserve app-loop proof | Trust, data, or external flow changes |
| 017-G08 | architecture-governance | 0.5.0 | CAPEC | N/A | No externally reachable or adversarial boundary is introduced | `docs/security/threat-model.md` | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | N/A confirmed | None identified | None | External/untrusted flow changes |
| 017-G09 | architecture-governance | 0.5.0 | S-ADR/arc42 security concepts | N/A | No security-significant architecture decision is planned | Architecture/security docs | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | N/A confirmed; example composition does not change architecture | None identified | None | Security architecture decision appears |
| 017-G10 | architecture-governance | 0.5.0 | Zero Trust/SAMM | N/A | No distributed service, remote management, identity, or maturity-program change | Zero Trust/SAMM docs | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | N/A confirmed | Existing program maturity unchanged | None | Distributed/service/identity scope changes |
| 017-G11 | architecture-governance | 0.5.0 | BSI C3A | N/A | No cloud service, provider dependency, portability, or exit strategy changes | `docs/security/cloud-autonomy-applicability.md` | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | N/A confirmed | None identified | None | Cloud/provider dependency enters scope |
| 017-G12 | architecture-governance | 0.5.0 | BSI C5 | N/A | No cloud service, shared responsibility, audit, or operational assurance changes | `docs/security/cloud-compliance-assurance.md` | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | N/A confirmed | None identified | None | Cloud operation or assurance scope changes |
| 017-G13 | isaqb-architecture-governance | 0.2.0 | Runtime view | Applicable | Event/status/description flows change in examples | `docs/architecture/runtime-view.md` | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | Pass: existing example-local boundary remains; feature evidence holds Wave-1 facts | Documentation drift | Re-evaluate architecture docs only for reusable boundary changes | Reusable runtime boundary changes |
| 017-G14 | isaqb-architecture-governance | 0.2.0 | Quality scenarios/risks | Applicable | Keyboard, fallback, clipping, and render proof are quality scenarios | Architecture quality/risk docs | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | Pass: quality scenarios proven in app-loop matrix | Platform rendering variance | Keep honest terminal state classes | New quality risk appears |
| 017-G15 | a11y-governance | 0.4.0 | Terminal keyboard/text-first | Applicable | User-facing terminal interaction changes | Runtime smokes and guides | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | Pass: keyboard, text, status, description, and Braille boundaries reviewed | Focus/layout regressions | Final A11Y gate | User-facing control changes |
| 017-G16 | a11y-governance | 0.4.0 | DE-first/EN-second CEFR-B2 | Applicable | Learner-facing runtime descriptions and guides change | Four guides, README, source descriptions | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | Pass: bilingual learner-facing diff reviewed | Translation drift | PR review | Learner-facing text changes |
| 017-G17 | a11y-governance | 0.4.0 | DocFX/WCAG/axe | Applicable | Guide output changes trigger generated-doc validation | `docfx.json`; `tests/web-a11y/` | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | Pass: DocFX 0/0 and Playwright/axe 2/2 | Browser-tool deprecation warnings only | Monitor CI | Documentation output changes |
| 017-G18 | a11y-governance | 0.4.0 | Didactic inline comments | Applicable | New non-trivial visual factories and proof helpers may need why/proof-boundary comments | Changed C# files | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | Pass: feature-015 decision review recorded | Over/under-commenting | PR review | Non-trivial logic changes |
| 017-G19 | cross-platform-governance | 0.2.0 | macOS/Linux/Windows terminal behavior | Applicable | Videomode and rendering vary by platform | Local tests and GitHub CI | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | Open: local macOS build/tests/PTY passed; remote Linux/Windows required | Host capability variance | Monitor PR matrix | Terminal/runtime behavior changes |
| 017-G20 | cross-platform-governance | 0.2.0 | Script parity/man pages | N/A | No script-shaped tooling is planned | Git diff and this evidence | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | N/A confirmed | None identified | None | Bash/PowerShell/script change enters diff |
| 017-G21 | agent-parity-governance | 0.3.0 | Five maintained agent surfaces | Applicable | Active feature context changes | Five agent guidance files | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | Pass: delivery and next-step context synchronized | Surface drift | Recheck final diff | Active context/shared guidance changes |
| 017-G22 | agent-parity-governance | 0.3.0 | `.specify/templates/` | N/A | No repository-owned template change is planned | Git diff and this evidence | TuiVision maintainers | GitHub PR reviewer | 2026-07-11 | N/A confirmed | None identified | None | Template change enters diff |

## Validation Runs

| Command | Version | Scope | Result | Evidence | GeneratedOutputHygiene |
|---|---|---|---|---|---|
| `specify check` | Tool 0.8.3 | Toolchain preflight | Pass | CLI ready | No output tracked |
| Prerequisite script | N/A | Feature path/task preflight | Pass | All required docs resolved | No output tracked |
| Checklist count | N/A | Six feature checklists | Pass | 120 complete, 0 incomplete | N/A |
| `git diff --check` | `1.17.0.94` | Final feature diff | Pass | Whitespace and prohibited-path audit clean | N/A |
| Release build | `1.17.0.90` | Repository | Pass: 0 warnings, 0 errors | `dotnet build --configuration Release` | Build output ignored |
| Targeted Release tests | `1.17.0.91` | Four Wave-1 apps plus matrix | Pass: 48/48, 0 skipped | Final scoped Release run | Test output ignored |
| Example-smoke suite | `1.17.0.92` | All examples | Pass: 101/101, 0 skipped | Complete example project | Test output ignored |
| Full Release tests | `1.17.0.93` | Repository | Pass: 508/508, 0 skipped | Core 44, Controls 288, Serialization 20, Compatibility 18, Drivers 37, Examples 101 | Test output ignored |
| Coverage gate | `1.17.0.94` | Five required assemblies | Pass: Core 89.78%, Controls 84.84%, Serialization 88.44%, Compatibility 80.55%, Drivers.Console 81.70% | `xmllint` config pass; all above 70% | `TestResults/` ignored |
| Format gate | `1.17.0.94` | Repository | Pass | `dotnet format --verify-no-changes` | No generated output |
| DocFX | `1.17.0.94` | 231 models / 223 HTML files | Pass: 0 warnings, 0 errors | `docfx docfx.json` | `_site/` and generated API YAML ignored |
| Playwright/axe | `1.17.0.94` | Generated DocFX | Pass: 2/2, 0 serious violations | Explicit same-process loopback server after configured-server timeout | Reports/test output ignored |
| Normal CLI starts | `1.17.0.94` | Desklogo, MsgCls, Tutorial default/01/16, Videomode | Pass: visible main/status/menu and clean Ctrl-Q exit | Release `--no-build` PTY runs; deterministic app-loop tests prove operation/description commands | No output tracked |
| Final local audit | `1.17.0.94` | Diff/status/prohibited paths | Pass | Clean whitespace; no `src/`, `tv203s/`, template, generated, or test-output changes | Ignored output only |

The configured Playwright web server timed out in this execution environment.
The documented workaround started Python and Playwright inside one shell/process
context; both tests then passed. Normal PTY starts confirmed the actual initial
views, menus, status rows, canonical Videomode fallback on this host, Tutorial
01/16 identity, and clean `Ctrl-Q` exit. Deterministic app-loop smokes remain the
bounded proof for operation and description commands because PTY escape parsing
is host-dependent.

## Version Baseline

At feature start, branch-specific commit count is `0`; inherited version is
`1.16.7.69`. Before the first build/test, fields move to `1.17.0.70` and the
Build component increments before every later build/test. Before commit/push,
Patch is aligned to the expected feature-branch commit count without a new Build
increment unless a build/test runs.

## Documentation And A11Y Evidence

| Artefact | Trigger | Planned proof | Result |
|---|---|---|---|
| Four example guides | Runtime/operation changes | Bilingual CEFR-B2 and semantic text review | Pass |
| `examples/README.md` | Wave status/commands change | Bilingual traceability review | Pass |
| Runtime status/description | Terminal UI changes | Keyboard, state, view, buffer/cell smoke | Pass |
| Generated DocFX | Guide changes | `docfx docfx.json` | Pass: 0 warnings/errors |
| Generated pages | Guide changes | Playwright/axe WCAG 2.2 AA and text-oriented snapshot | Pass: 2/2, 0 serious violations |

### Test-first observations

| Slice | Version | Expected failure | Observed boundary | Result |
|---|---|---|---|---|
| MsgCls vertical slice | `1.17.0.70` | Missing queued app-loop and visual/status/description proof surfaces | Compile failed on `CmDescription`, `QueueEvents`, `LastVisibleComponentKind`, `LastVisibleRegion`, and `LastStatusMessage` | Expected red boundary confirmed |
| Desklogo visible slice | `1.17.0.73` | Missing retained logo desktop, app-loop queue, status, component kind, and regions | Compile failed on `LogoDesktop`, `CmDescription`, `QueueEvents`, `LastVisibleComponentKind`, `LastVisibleRegion`, and `LastStatusMessage` | Expected red boundary confirmed |
| Tutorial visual matrix | `1.17.0.76` | Missing visual launcher commands, event queue, signatures, view/region/status, and retained render buffer | Compile failed on all planned `TutorialApp` visual-proof surfaces | Expected red boundary confirmed |
| Videomode visible slice | `1.17.0.80` | Missing probe/description commands, queued app-loop events, canonical state, retry count, status, and stable regions | Compile failed on all planned `VideomodeApp` and coordinator proof surfaces | Expected red boundary confirmed |
| Wave-1 acceptance matrix | `1.17.0.87` | One deliberately pending `tvguid16` rendered-proof field | Matrix compiled; 2/3 tests passed and the weak-proof guard rejected `Pending` exactly at `tvguid16` | Expected red boundary confirmed |

### MsgCls vertical-slice result

- Versions `1.17.0.71` and `1.17.0.72`: the new primary method and all seven
  MsgCls Release tests passed with zero failures/skips.
- Route: queued `cmPostLoremIpsum` then `CmDescription` through `app.Run()`;
  the command broadcasts into `MsgClsWindow`, status updates, and the
  description window becomes the last visible `TWindow`.
- Proof: concrete message list, view-tree kind `TWindow`, stable description
  region containing `MsgCls description`, and rendered status row containing
  `Help -> Description`.
- Helper usage: `None` for the new primary visual method. Existing direct
  `PostMessage` tests remain functional regression/supplemental evidence.
- Decision: `UseExistingFramework` for MsgCls, shared composition, and current
  generic smoke helpers. No framework source file changed.

### Desklogo result

- Versions `1.17.0.74` and `1.17.0.75`: two new app-loop methods and all six
  Desklogo Release tests passed with zero failures/skips.
- Main proof retains the constructed `DesklogoDesktop` after shutdown, verifies
  rendered logo rows, view kind, block-glyph cells, and the rendered status hint.
- Description proof queues `CmDescription`, renders a `TWindow` in a stable
  region, and confirms the embedded logo data remains unchanged.
- The existing undersized-terminal tests retain controlled clipping metrics.
  `set-logo.cc` and `tv_logo.cc` remain read-only asset/generator context.
- Decision: `UseExistingFramework`; no artificial logo animation/mutation and no
  framework source change were introduced.

### Tutorial result

- Versions `1.17.0.77` through `1.17.0.79`: both new app-loop visual tests and
  all 22 Tutorial Release tests passed with zero failures/skips.
- The first implementation run exposed a real proof defect at `tvguid08`: the
  deliberate delta clipped the first token characters. Moving the logical child
  origin retained the visible delta while restoring the complete identity marker.
- Exactly 16 unique signatures combine token, expected component kind,
  historical marker, and activated state. Every primary run proves the token in
  its stable rendered region and a status containing `Help -> Description`.
- Representative kinds: two `TStaticText`, seven `TWindow`, one `TScrollGroup`,
  five `TDialog`, and one `TInputLine` target. Shared families remain distinct
  through token, intent marker, state, and rendered content.
- Historically shifted learner titles were corrected for steps 02, 03, 06, 07,
  09-12, 15, and 16. The 16 existing `CreateApp` implementations remain intact
  as feature-014 regression surfaces.
- Decision: `IntentionalDeviation`; the normal-launch visual factory represents
  each cumulative lesson's defining addition without a mechanical C++ re-port.

### Videomode result

- Versions `1.17.0.81` and `1.17.0.82`: both new app-loop methods and all ten
  Videomode Release tests passed with zero failures/skips.
- The constructor performs the accepted initial capability probe; a queued
  `CmProbe` provides the retry, so the primary path proves two real attempts and
  continued usability through `app.Run()`.
- The existing `DisplayModeOutcome` contract remains unchanged. The visible
  layer distinguishes exactly `supported`, `fallback`, `rejected`, and
  `unchanged`; this avoids claiming that a successful probe guarantees a later
  resize or that every host supports the historical mode operation.
- Proof includes the retained coordinator state, `VideomodeView` view-tree kind,
  stable result region, rendered canonical state, real status row, and
  keyboard-reachable description.
- Decision: `IntentionalDeviation`; the managed capability probe preserves the
  historical demonstration goal without recreating the platform mode/shell matrix.

### Three-layer diagnostic

- Version `1.17.0.83`: seven primary app-loop methods across Desklogo, MsgCls,
  Tutorial, and Videomode passed with zero failures/skips.
- Main-component, status-line, description, command/key route, and clean quit
  layers all passed. No failing layer justified source remediation in T059-T064.
- The four app sources and `Wave1Runtime.cs` therefore remain unchanged after
  the diagnostic. The status helper already draws a stable final terminal row;
  all descriptions are German-first/English-second and explain operation,
  historical boundary, visible state, or platform constraint.
- Version `1.17.0.84` repeated the same seven-method set after that decision and
  passed with zero failures/skips. `InteractiveSmokeEventScript` already covers
  queued commands and keys, repeated actions, descriptions, and app-owned quit;
  no generic helper change was needed.
- Version `1.17.0.85`: all 45 Desklogo, MsgCls, Tutorial, and Videomode tests
  passed with zero failures/skips. Every example has one final framework
  decision; bounded historical follow-ups include owner, boundary, residual
  risk, and re-evaluation trigger. `git diff --check` passed and `tv203s/`
  remained unchanged.

### Acceptance matrix implementation

- The matrix contains exactly four app rows and 16 unique Tutorial token rows.
  Each row names a real historical source, primary app-loop method, concrete
  state, view-tree identity, rendered cells/region, status, description,
  evidence path, helper classification `None`, and one allowed decision.
- The weak-proof guard rejects startup/static text, history-only, private-only,
  `PrimaryProof` helper-only, generic Tutorial, and pending render claims.
- Existing `ExampleTestBase` and `InteractiveSmokeEventScript` already cover the
  required generic assertions and command sequencing. No helper modification or
  new proof abstraction was justified.
- App-specific boundaries remain explicit: Desklogo clipping, repeated MsgCls
  routing, 16 unique Tutorial signatures, and host-dependent Videomode outcome.
- Version `1.17.0.88`: all three matrix tests plus the 45 scoped app tests passed,
  for 48/48 with zero failures/skips.

#### Matrix-to-review-area reconciliation

| MatrixId | ReviewAreaId | Primary area | Decision |
|---|---|---|---|
| `Desklogo` | `W1-DESK` | Logo/status/description flow | `UseExistingFramework` |
| `MsgCls` | `W1-MSG` | Command/broadcast/visible result flow | `UseExistingFramework` |
| `Tutorial` | `W1-TUT` | Token launcher and aggregate 16-step flow | `IntentionalDeviation` |
| `Videomode` | `W1-VID` | Probe/retry/result flow | `IntentionalDeviation` |
| `tvguid01` | `W1-TUT-01` | Application shell state | `IntentionalDeviation` |
| `tvguid02` | `W1-TUT-02` | Status-line lesson state | `IntentionalDeviation` |
| `tvguid03` | `W1-TUT-03` | Menu/command lesson state | `IntentionalDeviation` |
| `tvguid04` | `W1-TUT-04` | Window insertion state | `IntentionalDeviation` |
| `tvguid05` | `W1-TUT-05` | Drawing state | `IntentionalDeviation` |
| `tvguid06` | `W1-TUT-06` | Vertical content state | `IntentionalDeviation` |
| `tvguid07` | `W1-TUT-07` | Two-axis content state | `IntentionalDeviation` |
| `tvguid08` | `W1-TUT-08` | Scroll-delta state | `IntentionalDeviation` |
| `tvguid09` | `W1-TUT-09` | Pane composition state | `IntentionalDeviation` |
| `tvguid10` | `W1-TUT-10` | Resize-limit state | `IntentionalDeviation` |
| `tvguid11` | `W1-TUT-11` | Non-modal dialog state | `IntentionalDeviation` |
| `tvguid12` | `W1-TUT-12` | Modal-result state | `IntentionalDeviation` |
| `tvguid13` | `W1-TUT-13` | Button state | `IntentionalDeviation` |
| `tvguid14` | `W1-TUT-14` | Choice-control state | `IntentionalDeviation` |
| `tvguid15` | `W1-TUT-15` | Input-line state | `IntentionalDeviation` |
| `tvguid16` | `W1-TUT-16` | Data/validation state | `IntentionalDeviation` |

The 20 matrix IDs and 20 review-area IDs are unique. Supporting rows
`W1-SHARED` and `W1-PROOF` describe infrastructure and are not matrix targets.
- Version `1.17.0.89`: the complete example-smoke project passed 101/101 with
  zero failures/skips. This includes all feature-014 functional methods, which
  remain supplemental regression evidence beside the new visual primaries.
- The proof checkpoint passed `git diff --check`; `tv203s/`, generated docs,
  test output, and coverage output remain absent from the tracked diff.

### Guide and A11Y traceability

| App/area | Runtime | Primary smoke | Guide/history |
|---|---|---|---|
| Desklogo | Logo, clipping, status, description | Logo/status and description app-loop methods | `desklogo.md`; `desklogo.cc`, generator context |
| MsgCls | Command, broadcast, repeat, status, description | Routed-message/status/description app-loop method | `msgcls.md`; `testdyn.cpp`, `tlnmsg.cpp`, `tlnmsg.h` |
| Tutorial | Token, action, 16 visible states, status, description, fallback | 16-state and description app-loop methods | `tutorial.md`; `tvguid01.cc` through `tvguid16.cc` |
| Videomode | Probe, retry, four canonical states, status, description | Probe/result/usability and description app-loop methods | `videomode.md`; `test.cc` |

All four guides are German-first/English-second at CEFR-B2 level, use semantic
headings/tables and language-tagged fences, and retain correct German umlauts
and `ß` in changed text. Commands, states, status, and descriptions remain
understandable for keyboard users, screen readers, Braille displays, and text
browsers without colour or pointer-only meaning. Existing `docs/toc.yml` links
already cover all four guides, so `docfx.json` and navigation required no edit.

The changed user-facing documentation set is exactly the four guides and
`examples/README.md`; feature evidence is review-facing. These guide changes
trigger both `docfx docfx.json` and the matching Playwright/axe path. `_site/`,
generated `api/*.yml`, caches, and validation output must remain ignored and
untracked. `git diff --check`, fence balance, and guide/navigation reference
checks passed. US4 is complete with no bounded documentation follow-up.

## Comment Review

The feature-015 decision model applies to every new or changed non-trivial block:
`CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`,
`UpdateExistingComment`, or `FollowUpHardening`. Initial candidates are shared
status drawing, Tutorial factory mapping, Videomode outcome mapping, and smoke
proof-boundary helpers. Final rows are added after implementation diff review.

| Area | Decision | Rationale |
|---|---|---|
| `Wave1StatusLine.Draw` | `CommentAdequate` | The bilingual two-line comment explains why explicit cells stabilize cross-driver proof. |
| Shared region/menu helpers | `NoCommentNeeded` | Names, geometry, and framework types make the short composition self-explanatory. |
| Desklogo presentation flow | `NoCommentNeeded` | Existing XML docs and named methods expose the bounded description/status flow without restating code. |
| MsgCls queued command/broadcast flow | `CommentAdequate` | Existing headless and routing comments explain the non-obvious event boundary. |
| Tutorial descriptor/factory mapping | `CommentAdequate` | The bilingual comment explains the representative cumulative mapping rather than only what the switch does. |
| Tutorial visual-app lifecycle | `NoCommentNeeded` | `SetMainState`, `ShowDescription`, and `RemoveMainView` state the short ownership flow directly. |
| Videomode capability mapping | `UpdateExistingComment` | Updated bilingual comments distinguish probe success, later rejection, enum compatibility, and text-state proof. |
| Wave-1 acceptance matrix guards | `NoCommentNeeded` | Test names, field names, and rejected markers express the proof contract directly. |

No changed area requires `CommentNeeded` or `FollowUpHardening`. Comment
intensity remains moderate and German-first/English-second.

## Governance Closure Review

- `specify preset list` confirmed the six accepted versions: security 0.6.0,
  architecture 0.5.0, iSAQB 0.2.0, A11Y 0.4.0, cross-platform 0.2.0, and
  agent parity 0.3.0.
- Constitution Level-2 and the architecture vision/runtime/quality/risk files
  were reviewed. The change adds example-local composition only; no reusable
  framework, deployment, trust, or architecture boundary changed, so feature
  evidence holds the new Wave-1 runtime facts.
- Security, threat, dependency, ASVS, supply-chain, Zero Trust, SAMM, C3A, C5,
  and regulatory evidence were reviewed. No web/auth, package/release, cloud,
  provider, distributed, market/customer, financial, or runtime-AI trigger
  entered scope.
- A11Y runtime and guide checks passed locally; DocFX/axe remains the explicit
  T131-T132 gate. Videomode uses honest host-dependent states; Linux/Windows
  confirmation remains a remote PR gate.
- All five agent surfaces now contain the same delivery and Wave-3-hardening
  next-step context. `.specify/templates/` and script-shaped tooling are `N/A`.
- `Pflichtenheft.md` records 017 complete and routes next to
  `Lastenheft_03_EditorHelpAndResourcesHardening.md`; statistics include the
  prevalidation 017 snapshot and phase-19 trend slot.
- Scope audit: no `src/` framework API, dependency/package, service,
  persistence, arbitrary user-file path, Wave-2/3/4 behavior, generated output,
  or `tv203s/` file changed. Existing example method signatures remain; new
  example-local observability/overloads support deterministic visual proof.
- `git diff --check` and prohibited-path checks passed at governance closure.

## Follow-Up Register

| Item | Boundary | Owner | ResidualRisk | Trigger | Result |
|---|---|---|---|---|---|
| Historical MsgCls info-window split | Not required for routing-visible 017 acceptance | TuiVision maintainers | Historical shape differs | Dedicated parity feature | Existing bounded follow-up |
| Full historical Videomode mode/shell matrix | Broader than honest capability demo | TuiVision maintainers | Some historical commands remain absent | Terminal/mode hardening feature | Existing bounded follow-up |
| Full line-by-line Tutorial recreation | Explicitly excluded; representative visual lessons are required | TuiVision maintainers | Modern lesson differs structurally | New educational porting requirement | Not planned |

## Success-Criteria Audit

| Criterion | Result | Evidence |
|---|---|---|
| SC-001 | Pass | Four normal-start main states observed and app-loop proven |
| SC-002 | Pass | Main area, real status, and description for all four apps |
| SC-003 | Pass | 16/16 unique Tutorial signatures and matrix rows |
| SC-004 | Pass | One or more documented app-loop primary methods per app |
| SC-005 | Pass | Matrix guard rejects startup/static/history/private/helper-only proof |
| SC-006 | Pass | Desklogo clipping, MsgCls repeat, Videomode state/usability proven |
| SC-007 | Pass | One framework decision per matrix/review area |
| SC-008 | Pass | Read-only history reviewed; zero `tv203s/` changes |
| SC-009 | Pass | Bilingual/text-first review, DocFX 0/0, axe 2/2 |
| SC-010 | Pass | Build 0/0, 508 tests, format, diff, and five coverage gates pass |
| SC-011 | Pass | Six presets, 22 complete rows, rationale/trigger for every `N/A` |
| SC-012 | Pass | No cross-wave, framework, dependency, generated, or historical scope |

All four app rows, all 16 Tutorial rows, five final example/shared framework
decisions, 22 governance rows, local validation runs, and bounded follow-ups are
complete. Remote Linux/Windows and review status remain delivery gates rather
than unrecorded acceptance claims.

The binding Lastenheft was archived with `scripts/rename-lastenheft.ps1
-NoCommit`; the final statistics refresh records `+3677/-184` lines before its
own closure row and keeps `## Gesamtstatistik` as the last top-level section.

Pre-commit security closure passed: no secret-like changed filename, Gitleaks
reported no current-diff secret, the agent scan reported high=0 (one known local
`.claude` configuration at medium and five prompt/config directories at low),
`git diff --check` passed, and the pre-push hook completed with no commit range
yet available. The actual push reruns the hook against committed history.

## PR Description

### Summary

- Adds visible main state, real status, and `Help -> Description` to all four
  Wave-1 examples, including 16 distinct Tutorial lesson states.
- Adds app-loop state/view/buffer proof and a 20-row acceptance matrix while
  preserving the feature-014 functional baseline and read-only history.
- Aligns guides, README, project routing, agent context, governance evidence,
  and project statistics with the delivered runtime.

### Validation

- Release build: 0 warnings, 0 errors.
- Targeted Wave-1 and matrix tests: 48/48; all example smokes: 101/101; full
  Release suite: 508/508.
- Coverage: Core 89.78%, Controls 84.84%, Serialization 88.44%, Compatibility
  80.55%, Drivers.Console 81.70%.
- Format and diff gates pass. DocFX: 0 warnings/errors. Playwright/axe: 2/2.
- Remote OS, CI, and review checks remain required before merge.

### Scope And Risk

No framework source, dependency, service, persistence, runtime AI,
historical-source, generated-output, or cross-wave behavior changed. Runtime
risk is limited to example-local presentation and host-dependent Videomode
classification; deterministic app-loop and remote OS checks bound that risk.
