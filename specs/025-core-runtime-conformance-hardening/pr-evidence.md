# Autonomous Run Evidence: Core Runtime Conformance Hardening

**Branch**: `025-core-runtime-conformance-hardening`
**Feature directory**: `specs/025-core-runtime-conformance-hardening`
**Binding intake**: `Lastenheft_10_Core-Runtime-Conformance-Hardening.025-core-runtime-conformance-hardening.md` (archived after implementation)
**Delivery mode**: `MergeAndSync`
**Authority source**: User instruction dated 2026-07-13 to close PR #68 and execute Feature 025 autonomously through merge and local main synchronization.

## Scope

### Included

- Findings `F001` through `F009`: concrete event kinds, focus veto, Group state matrix, pending/idle lifecycle, Desktop stack, close/modal lifecycle, shared command context, canonical real keyboard ingress, and bounded generic drag.
- Additive framework contracts, red-first tests, Feature-024 resolution metadata, public XML documentation, learner guide, governance evidence, project statistics, agent context, and intake/archive markers.

### Excluded

- Findings `F010` through `F013`, Feature 026/028 implementation, Wave 5/6 application code, examples, broad framework rewrite, new package/runtime dependency, full desktop drag-and-drop, pointer-only interaction, and any write under `TVDEMOS/`, `TVFM/`, `tv203s/`, or external Free Vision.

## Run Gates

| Phase | Attempt | Result | Evidence | Remaining action |
|---|---:|---|---|---|
| Preflight | 1 | Pass | Branch `025-core-runtime-conformance-hardening`; base `f22865f`; `specify check`; prerequisite JSON; zero incomplete checklist items | None |
| Specify | 1 | Accepted | `spec.md`; 37 FR, 13 CR, 12 SC | None |
| Clarify | 2 | Accepted | Five decisions in `spec.md`: focus result, one pending slot, owner-scoped modality, command refresh, one-cell shared drag | None |
| Checklists | 2 | Pass | `checklists/`; 118 completed, zero incomplete items | None |
| Plan | 2 | Pass | `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, `contracts/` | None |
| Tasks | 1 | Pass | `tasks.md`; 154 sequential tasks | Execute T001-T154 |
| Analyze | 3 | Pass | Final pass: 62/62 normative items covered; 9x6 Finding traceability; no Critical, High, or Medium issue | None |
| Implement | 1 | Pass | T001-T107; all nine runtime findings and Feature-024 resolution validators passed | Complete documentation, governance, archive, and final validation |
| Validate | 1 | Open | Validation table below | Run triggered gates after implementation |
| Deliver | 1 | Open | Remote Delivery table below | Commit, PR, review, merge, sync |

Allowed results are `Pass`, `Fail`, `Accepted`, `Deferred`, and `Open`.

## Validator Trigger Inventory

| Validator or owned surface | Trigger recorded before edit | Planned acceptance |
|---|---|---|
| Feature-024 audit JSON and readable gate | Resolution metadata, new source/public contracts, gate marker | Update validator test first; preserve Revision-2 observations and reciprocal links |
| Public XML/API inventory | Additive public focus/command/close/drag/Desktop contracts | Complete DE-first/EN-second XML; update compiler-backed audit inventory |
| Runtime regression tests | Shared Core/Controls/Compatibility behavior | Red-first filtered tests, targeted Release, full Release, canonical coverage |
| DocFX and A11Y | XML API and learner guide/navigation | `docfx`, Playwright/Axe, UTF-8 Lynx/text review |
| Agent parity | Active 025 context and completed-next-intake transition | Five maintained instruction files in one logical change |
| Pflichtenheft and archive markers | Feature completion | Feature 026 next; Wave 5/6 blocked through 028; safe Lastenheft rename parity |
| Versioning | Every `dotnet build`/`dotnet test`, then commit/push | One manual counter increment per invocation; `1.25.<patch>.<build>` |
| Coverage configuration | Shared runtime change | XML validity and >=70 percent for five canonical assemblies |
| Generated/secret/scope scans | Always before push | No generated output, credentials, packages, Wave code, examples, or historical/external edits |

## Finding Decisions and Follow-ups

| FindingId | ContractId | Decision | RedProof | Change | RealPathProof | HistoricalIntent | FreeVisionRelation | ModernRationale | ApiImpact | A11YImpact | PlatformBoundary | ResidualBoundary | Result |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| `F001` | `C004` | Implemented | Build 215; filtered Core test failed because `TEventKind.Mouse` created an event | Exact allow-list and XML contract in `TEvent.cs` | Build 216; filtered public-factory test passed 1/1 | One concrete event payload channel | Corroborates one active event kind | Exact allow-list retains masks only for filters | XML clarified; no signature change | N/A | Managed invariant | Filter masks remain valid outside factories | Pass |
| `F002` | `C008` | Implemented | Build 219; test project failed because no overridable `CanReleaseFocus` contract existed | Additive result enum, view veto hook, strict `TrySetFocus`, compatible `SetFocus` wrapper | Build 220; focus matrix passed 6/6 | Current view may refuse focus loss before mutation | Corroborates validation before release | Typed managed result avoids hidden flag reassertion | Additive | Focus announcement remains single and coherent | Managed invariant | InputLine validator integration stays in 026 | Pass |
| `F003` | `C009` | Implemented | Build 221; four tests failed for local Disabled, one Current, missing Dragging, and Insert inheritance | Bitwise responsibility matrix in `TGroup.SetState` and `Insert` | Build 222; state matrix passed 7/7 | State propagation follows owner responsibility | Corroborates state-specific propagation | Explicit matrix avoids fabricated local disabled/focus state | None planned | One focused child remains observable | Managed invariant | No new hierarchy abstraction | Pass |
| `F004` | `C013` | Implemented | Build 223 proved the missing lifecycle seams; Build 224 isolated an incorrect test expectation | One pending slot, pending-first `GetEvent`, nonblocking host poll, one Idle and conditional CPU release in `Run` | Build 225: 4/4 filtered lifecycle and raw-ingress tests passed through actual `Run()`/`GetEvent()` paths | One pending event and idle only after no input | Corroborates PutEvent/GetEvent/Idle ordering | Bounded seam avoids queue, thread, and busy loop | Additive | Deterministic status/focus refresh opportunity | Host wait differs by platform; deterministic seam is shared | No timer/message-pump recreation | Pass |
| `F005` | `C014` | Implemented | Build 228 failed to compile because Desktop had no coherent stack, geometry, close-all, or result API | Owner-local snapshot/reorder primitives plus immutable insertion, top/next, tile, cascade, and Close-All results | Build 229: 4/4 filtered Desktop tests passed for empty and mixed stacks, focus, Z-order, bounds, geometry, veto, and skip counts | Desktop owns window stack and tile/cascade | Corroborates Desktop responsibility | Owner-local snapshots and managed geometry | Additive | Keyboard-selectable stack remains text-first | Managed geometry | General close interface replaces the temporary framed-host adapter in F006; no application registry | Pass |
| `F006` | `C015` | Implemented | Build 230 failed to compile because `TWindow` lacked an overridable close decision and no close/modal result contracts existed | Additive close decision/interface, visible Window/host removal, generic Desktop Close-All, and owner-scoped modal execution with `finally` cleanup | Builds 232-233: 6/6 focused and 10/10 integrated Desktop/Window/Dialog/Application tests passed | Close validates then removes; execView restores lifecycle | Corroborates close/modal ownership and cleanup | Explicit result plus `finally` protects managed ownership | Additive | Focus restoration and keyboard close paths | Managed event loop | Dialog child validation stays in 026 | Pass |
| `F007` | `C017` | Implemented | Build 226 failed to compile because no opt-in `ICommandStateProvider` or shared snapshot existed | Immutable per-refresh context, opt-in active-view chain, separate menu/status overlays, and pre-dispatch refresh | Build 227: 4/4 focused CommandContext tests passed, including actual `Run()` idle and canonical F008 ingress dependency | One command set drives presentation and dispatch | Corroborates shared command availability | Immutable snapshots preserve static/manual constraints | Additive | Menu, StatusLine, active view, keyboard agree | Managed invariant | No global application command catalog | Pass |
| `F008` | `C034` | Implemented | Build 217; raw `a` produced enum KeyCode 65 instead of character KeyCode 97; canonical Ctrl did not start move mode | Controls references existing Compatibility; `GetEvent` uses `TConsoleInputAdapter`; Window uses `TShiftState.Ctrl` | Build 218; production-ingress/window matrix passed 5/5 | Real driver key translation owns scan/modifier semantics | Corroborates canonical driver translation | Reuse existing Compatibility translator instead of copied table | Internal project reference and protected test seam; no package | Keyboard behavior remains complete | Local macOS/Linux; Windows/WSL by CI | Terminal-specific unsupported input remains explicit | Pass |
| `F009` | `C036` | Implemented | Build 234 failed to compile because no generic `IDragTarget`, session, state, or result contracts existed | Shared bounded state/result/session/target contracts; Window title pointer and Ctrl+F5 keyboard paths use one session | Build 236: 9/9 tests passed without warnings, including two actual `Run()` loops, target accept/reject, one-cell capture, bounds, cells, and lifecycle cancellation | DragView shares pointer and keyboard move lifecycle | Corroborates tracked mouse/keyboard responsibility | Source-owned state object is deterministic C# | Additive | Arrow/Enter/Escape parity is mandatory | Character-cell coordinates; host capability loss cancels | No full desktop/file drag protocol | Pass |

`FollowUpHardening` cannot close an accepted row. A required breaking change or accepted-semantic conflict is `ProductDecision` and stops autonomous implementation.

## Historical Intent

| Modern area | Historical source | Intent retained | Intentional deviation | Proof or N/A rationale |
|---|---|---|---|---|
| Focus and state | `tv203s/tvision/classes/tgroup.cc` plus matching headers | Focus loss may be rejected; child state follows owner responsibility | Typed additive result and explicit C# matrix | F002/F003 tests |
| Event loop | `tv203s/tvision/classes/tprogram.cc` plus matching headers | One pending event; idle after no physical input | Replaceable managed CPU-release seam | F004 tests |
| Desktop | `tv203s/tvision/classes/tdesktop.cc` plus matching headers | Desktop owns selection, tile, and cascade | Managed snapshots instead of static translation | F005 tests |
| Modal and close | `tv203s/tvision/classes/tdialog.cc`, `twindow.cc`, `tview.cc` plus matching headers | Validation, result, removal, modal cleanup, focus restoration | Explicit managed close/result interfaces and `finally` | F006 tests |
| Command/input/drag | Matching `tprogram.cc`, `tview.cc`, menu/status and driver headers | Shared command truth, canonical input, pointer/keyboard drag | Immutable command snapshot and managed drag state | F007-F009 tests |
| Free Vision second opinion | External `/tmp/tuivision-fv-025-ffc03b34`, commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2`; `views.inc`, `app.inc`, `menus.inc`, `statuses.inc`, `dialogs.inc`, `drivers.inc` | Corroborates responsibility and lifecycle boundaries | Secondary only; no copied or vendored code | Commit and Feature-024 manifest hashes verified 2026-07-13 |

## Governance Applicability

| RunId | Preset | Version | Checkpoint | Applicability | Rationale | Evidence path | Owner | Reviewer | ReviewDate | Result | Residual risk | Follow-up | Re-evaluation trigger |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| `025-G01` | security-governance | 0.6.0 | NIST SSDF / CWE Top 25 | Applicable | Exact event kinds, bounded pending/capture state, explicit rejection, and fail-closed target exceptions reduce state and input confusion | Finding rows; runtime tests; final diff | Maintainer | Codex | 2026-07-13 | Pass | Low: future consumers can misuse additive APIs | Revalidate in Feature 028 | Runtime boundary or public contract changes |
| `025-G02` | security-governance | 0.6.0 | ASVS | N/A | No Web, HTTP, API, auth, or remote session service | This file | Maintainer | Codex | 2026-07-13 | Pass | None | None | Such a service enters scope |
| `025-G03` | security-governance | 0.6.0 | SBOM / VEX / SLSA / OpenSSF | N/A | No package, dependency, distribution, or CI provenance change in 025 | Existing `docs/security/`; this file | Maintainer | Codex | 2026-07-13 | Pass | Existing release baseline remains separate | None | Package, release artifact, or supply-chain flow changes |
| `025-G04` | security-governance | 0.6.0 | AI-SBOM | N/A | AI is development tooling only | This file | Maintainer | Codex | 2026-07-13 | Pass | None | None | Runtime model, dataset, AI service, or delivered AI component appears |
| `025-G05` | security-governance | 0.6.0 | NIS2 / CRA / EU AI Act / DORA | N/A | No regulated service, market placement, runtime AI, or financial ICT boundary changes | Existing regulatory evidence; this file | Maintainer | Codex | 2026-07-13 | Pass | Legal classification can change outside feature | None | Deployment/customer/regulatory scope changes |
| `025-G06` | architecture-governance | 0.5.0 | STRIDE / CIA / CAPEC | Applicable | Spoofed input type, state tampering, stale command dispatch, denial through busy polling, and target exceptions are bounded by exact factories, immutable snapshots, one-slot/session limits, CPU release, and fail-closed rejection | Plan; Finding tests; guide; this file | Maintainer | Codex | 2026-07-13 | Pass | Low: host-specific input remains a CI boundary | Revalidate in Feature 028 | Boundary, trust, or host-adapter changes |
| `025-G07` | architecture-governance | 0.5.0 | S-ADR / arc42 | N/A | The in-repository Controls-to-Compatibility reference reuses the documented canonical input adapter; no trust boundary, external integration, authentication, sensitive data flow, deployment topology, or security architecture decision changes | `docs/security/adr/README.md`; `docs/security/arc42-security.md`; guide; final graph | Maintainer | Codex | 2026-07-13 | Pass | Existing project-level security concepts remain authoritative | None | A trust boundary, external integration, sensitive flow, or security architecture decision changes |
| `025-G08` | architecture-governance | 0.5.0 | Zero Trust / SAMM / BSI C3A / BSI C5 | N/A | No distributed, cloud, provider, deployment, or remotely managed service boundary | This file | Maintainer | Codex | 2026-07-13 | Pass | None | None | Cloud/provider/distributed operation enters scope |
| `025-G09` | isaqb-architecture-governance | 0.2.0 | Quality scenarios / risks / modernization | Applicable | Determinism, usability, compatibility, accessibility, and maintainability are proven by explicit results, immutable snapshots, bounded lifecycle, keyboard parity, and historical deviation records | Spec; plan; Finding rows; guide; tests | Maintainer | Codex | 2026-07-13 | Pass | Additive APIs increase maintenance surface | Feature-028 revalidation | New architecture layer or trade-off appears |
| `025-G10` | a11y-governance | 0.4.0 | Keyboard / focus / text-first / docs | Applicable | Focus, command, close, modal, and drag expose keyboard-equivalent state; guide, XML, Axe, and Lynx prove text-first access | Tests; guide; XML; DocFX/Axe/Lynx rows | Maintainer | Codex | 2026-07-13 | Pass | Low: later controls can omit opt-in command or drag semantics | Revalidate in Feature 028 and Wave consumers | Interaction or learner docs change |
| `025-G11` | cross-platform-governance | 0.2.0 | Keyboard/terminal platform proof | Applicable | Real keyboard and modifier ingress changes | Local tests and remote CI | Maintainer | Codex | 2026-07-13 | Open | Local host cannot prove Windows/WSL | Require CI | Platform adapter changes |
| `025-G12` | cross-platform-governance | 0.2.0 | Script parity | N/A | Product scripts are not edited; archive scripts are invoked under their existing parity contract | T117; this file | Maintainer | Codex | 2026-07-13 | Pass | Invocation mismatch | Dry-run/error-channel comparison | Script source changes |
| `025-G13` | agent-parity-governance | 0.3.0 | Five maintained surfaces | Applicable | Completed 025 state, nine closures, preserved gates, and next 026 intake are synchronized together | Five agent files | Maintainer | Codex | 2026-07-13 | Pass | Generated context metadata can drift in later runs | Final parity scan | Shared guidance or active status changes |
| `025-G14` | agent-parity-governance | 0.3.0 | `.specify/templates/` | N/A | Feature uses templates but does not change repository-owned templates | This file | Maintainer | Codex | 2026-07-13 | Pass | None | None | A reusable template correction is proven |
| `025-G15` | autonomous-run-governance | 0.1.0 | Evidence / convergence / MergeAndSync | Applicable | User authorized full autonomous delivery; validators and causal closeout are explicit | All 025 artifacts; this file | Maintainer | Codex | 2026-07-13 | Open | Remote reviewer availability | Complete T001-T154 | Permission, gate, or reusable workflow rule changes |

## Validation

| Command or review | Trigger | Result | Evidence or failure boundary |
|---|---|---|---|
| `git diff --check` | Always | Pass | Planning artifacts pass on 2026-07-13; repeat after implementation |
| `specify check` | Preflight | Pass | Seven presets resolved; local autonomous preset remains v0.1.0 |
| Prerequisite/checklist scan | Before implementation | Pass | Feature directory correct; 118 complete and zero incomplete checklist items |
| Spec-Kit Analyze | Before implementation | Pass | Final pass: no Critical/High/Medium; 62/62 coverage; 154 valid tasks |
| Build 215: filtered Core `F001` red test | Concrete event factory | Fail | Expected assertion: category `TEventKind.Mouse` was accepted by the old nonzero-mask guard. VSTest reported one failed test even though the wrapper process returned 0; the test result, not that process anomaly, is authoritative. |
| Build 216: filtered Core `F001` green test | Concrete event factory | Pass | 1/1; all four concrete kinds accepted and category/composite/mixed/unknown values rejected. |
| Build 217: filtered Controls `F008` red tests | Real keyboard ingress and Ctrl modifier | Fail | Expected 2/2 failures: production `GetEvent` used enum values instead of canonical key composition; `TWindow` treated Alt `0x0004` as Ctrl instead of canonical `0x0002`. |
| Build 218: filtered Controls `F008` green matrix | Real keyboard ingress and Ctrl modifier | Pass | 5/5; printable, navigation, F5/F10 modifiers, unknown fallback, Alt quit, Ctrl+W/Ctrl+F5, and keyboard move fallback use canonical semantics. |
| Build 219: filtered Controls `F002` red test | Focus veto contract | Fail | Expected compile boundary: `TView.CanReleaseFocus` and typed `TGroup.TrySetFocus` did not exist. |
| Build 220: filtered Controls `F002` green matrix | Focus veto contract | Pass | 6/6; accepted/rejected/no-op, exactly-once veto, eligibility, compatibility wrapper, unchanged data/state, and announcement count passed. |
| Build 221: filtered Controls `F003` red matrix | Group state responsibility | Fail | Expected 4/4 failures: Disabled and Focused propagated uniformly, Dragging did not propagate, and Insert omitted Dragging while inheriting forbidden flags. |
| Build 222: filtered Controls `F003` green matrix | Group state responsibility | Pass | 7/7; Active/Dragging, Current-only Focused, visible-only Exposed, owner-local Disabled, Insert inheritance, nested and empty cases passed. |
| Build 223: filtered Controls `F004` red tests | Pending and idle lifecycle | Fail | Expected compile boundary: `TryReadConsoleKey`, `Idle`, `ReleaseCpu`, `PutEvent`, and `HasPendingEvent` were absent. |
| Build 224: filtered Controls `F004` implementation matrix | Pending and idle lifecycle | Fail | 3/4 passed. The remaining assertion omitted the intentional `quit` handling performed inside Idle; no post-shutdown poll or CPU release occurred. Test expectation corrected before the next run. |
| Build 225: filtered Controls `F004` lifecycle and `F008` raw-ingress matrix | Pending ordering, idle lifecycle, CPU release, shutdown, and adapter ingress | Pass | 4/4 passed through actual `Run()` and production `GetEvent()` paths. One pending slot is bounded; shutdown causes neither another poll nor CPU release. |
| Build 226: filtered Controls `F007` red matrix | Shared command context | Fail | Expected compile boundary: `ICommandStateProvider` and the immutable context/overlay API did not exist, confirming that program, menu, StatusLine, and editor still had split command truth. |
| Build 227: filtered Controls `F007` matrix | Shared command context | Pass | 4/4 passed. Focus, handled-event, Idle, and pre-dispatch generations agree across active View, menu, StatusLine, editor state, and keyboard-command dispatch; manual disablement remains authoritative. |
| Build 228: filtered Controls `F005` red matrix | Desktop stack and geometry | Fail | Expected compile boundary: `TDesktop` lacked insertion, top/next, tile, cascade, close-all, and immutable operation-result APIs. |
| Build 229: filtered Controls `F005` matrix | Desktop stack and geometry | Pass | 4/4 passed. Empty/mixed operations, focused insertion, top/next Z-order, bounded tile/cascade, and explicit closed/vetoed/skipped counts are deterministic. |
| Build 230: filtered Controls `F006` red matrix | Close and modal lifecycle | Fail | Expected compile boundary: `TWindow` had no overridable close decision, and `TCloseResult`, `ICloseableView`, and owner-scoped `ExecuteModal` were absent. |
| Build 231: filtered Controls `F006` implementation matrix | Close and modal lifecycle | Fail | Test-harness compile boundary only: two new cell assertions used `Character` instead of the existing `TConsoleCell.Glyph` property. Production code compiled; assertions corrected. |
| Build 232: filtered Controls `F006` matrix | Close and modal lifecycle | Pass | 6/6 passed. Explicit close decisions, Command/Ctrl+W/Escape removal, rendered-cell clearing, actual `TApplication.Run`, modal isolation/nesting/reentry rejection, exception/shutdown cleanup, result, ownership, and focus restoration passed. |
| Build 233: integrated Controls `F005`/`F006` matrix | Desktop, close, and modal lifecycle | Pass | 10/10 passed. Generic `ICloseableView` Close-All, Window completion, modal ownership, focus, geometry, View-tree, and rendered-cell boundaries agree. |
| Build 234: filtered Controls `F009` red matrix | Generic bounded drag | Fail | Expected compile boundary: title drag had only specialized Window fields; `IDragTarget`, generic session/state, and immutable terminal result contracts were absent. |
| Build 235: filtered Controls `F009` implementation matrix | Generic bounded drag | Pass with warning | 9/9 tests passed, including real Pointer/keyboard loops and lifecycle cancellation. Compiler warning CS1570 exposed one missing `</summary>` on `TWindow.CanClose`; documentation marker corrected before final green proof. |
| Build 236: filtered Controls `F009` matrix | Generic bounded drag | Pass | 9/9 passed without warnings. Pointer and keyboard reached identical rendered bounds; target opt-in, one-cell capture, clamping, drop/reject, Escape, capability, disable, removal, and shutdown results passed. |
| Build 237: Feature-024 resolution validator red proof | Exact 025 closure schema and readable gate | Fail | Expected `JsonException`: the immutable Revision-2 dataset had no `resolutions` array. Original `findings` observations and contract decisions remain unchanged; closure is appended only after all nine runtime proofs passed. |
| Build 238: Feature-024 full validator first green attempt | Resolution, inventory, reciprocal links, readable gate | Fail | 11/12 passed. The validator correctly rejected two stale Feature-024 proof method names after the F003/F009 tests were strengthened; no runtime defect or documentation-only closure was accepted. |
| Build 239: Feature-024 full validator | Resolution, inventory, reciprocal links, readable gate | Pass | 12/12. Exactly `F001`-`F009` are `Closed` by Feature 025, all nine have `documentationOnly=false`, eight new source files and 16 new public types reconcile exactly, and `F010`-`F013` plus Feature 026/028 and Wave gates remain open. |
| Lastenheft archive Bash `--dry-run` / PowerShell `-WhatIf` | Script parity and error-channel review | Pass | Both returned exit 0 and the same exact target `Lastenheft_10_Core-Runtime-Conformance-Hardening.025-core-runtime-conformance-hardening.md`; PowerShell emitted only its expected WhatIf operation record and no ErrorRecord/fatal signature. |
| Bash archive `--no-commit` | Traceable completion rename | Pass | Source is absent and the target exists exactly once; script returned `OK: Umbenannt ohne Commit / Renamed without commit`. |
| Agent and next-intake parity | Five maintained contexts plus `Pflichtenheft.md` | Pass | The 025/026 blocks have identical SHA-256 `5e883225f0a68819f25ec5b2ecd332418b20dfeb0dcc073d78b95c0746d37826`; the marker names Feature 026 and keeps Wave 5/6 blocked through 028. |
| Project statistics | Implementation delta and final text-first block | Pass | Pre-statistics snapshot: production `+1641/-150`, tests `+1237/-33`, docs/evidence `+2598/-46`, metadata `+5/-4`; final `## Gesamtstatistik` remains the last top-level section. |
| `git diff --check`; `dotnet format --verify-no-changes --no-restore`; Markdown/UTF-8 scan | Final static quality | Pass | Diff and formatter are clean; 23 changed Markdown files are valid UTF-8 with balanced language-tagged fences. The first scan had a zsh newline-iteration harness defect before any file check; the corrected line-safe scan passed. |
| Build 240: targeted Core Release tests | Core event and baseline contracts | Pass | 52/52 tests passed. |
| Build 241: targeted Compatibility Release tests | Canonical key translator | Pass | 18/18 tests passed. |
| Build 242: targeted Controls Release tests | Focus, state, loop, command, Desktop, close/modal, drag, and regression contracts | Pass | 354/354 tests passed. |
| Build 243: targeted Drivers Release tests | Feature-024 audit resolution plus driver regressions | Pass | 117/117 tests passed, including 12/12 conformance-audit validators. |
| Build 244: full repository Release tests | Shared runtime regression gate | Pass | 725/725 passed: Core 52, Compatibility 18, Serialization 44, Controls 354, Drivers 117, Examples 140. |
| `xmllint --noout coverlet.runsettings` | Canonical coverage configuration | Pass | XML is well formed before the coverage run. |
| Build 245: canonical Coverlet Release gate | Five required assemblies at >=70% line coverage | Pass | 725/725 tests passed. Cobertura: Core 92.96%, Controls 85.70%, Serialization 89.50%, Compatibility 80.55%, Drivers.Console 89.18%. The Examples project has no collector and emitted a non-gate warning; all five canonical reports were generated and passed. |
| `docfx docfx.json` | Public XML, API inventory, guide, and navigation | Pass | Build succeeded with 0 warnings and 0 errors; 305 models and 198 managed-reference files processed. |
| `npm ci`; `npx playwright install chromium` | Reproducible local A11Y dependencies | Pass with environment note | 7 packages audited with 0 vulnerabilities and Chromium available. Local Node 26.5.0 is newer than the declared 20/22/24 range and emitted `EBADENGINE`; no lockfile or package changed, and the full suite passed. CI remains on a supported Node line. |
| `npm run test:docfx` | WCAG 2.2 AA smoke path | Pass | DocFX repeated with 0 warnings/errors; Playwright/Axe passed 2/2. Node 26 emitted only deprecation/color warnings. |
| UTF-8 Lynx review | Guide and representative public API pages | Pass | Guide, `TDragSession`, `TCommandContext`, and `TDesktop` expose skip links first, one clear main heading, German-first/English-second summaries, parameters/exceptions, and readable table/property order. |
| Local and remote platform boundary | Real keyboard/modifier semantics | Open for remote | Darwin arm64 with .NET 10.0.301 proves managed/raw `ConsoleKeyInfo` mapping locally. Windows/WSL must preserve the same printable, navigation, function, Alt/Ctrl/Shift, unknown-fallback, Ctrl+W, and Ctrl+F5 results; final PR CI is the authoritative unavailable-host proof. |
| Scope and secret scan | Packages, generated output, examples, consumers, historical/external sources, credentials | Pass | 61 tracked/untracked change paths contained no `_site`, API YAML, TestResults, cache, examples, `TVDEMOS/`, `TVFM/`, `tv203s/`, or external worktree content. The only project-file change is an in-repo project reference with no PackageReference. Gitleaks current-diff scan reports high=0; local `.claude/settings.local.json` remains an unchanged medium configuration note. |
| Filtered red/green tests | Each finding | Pass | Builds 215-239 record a red-first proof and final green result for every accepted finding plus the Feature-024 closure validator. |
| Targeted/full/coverage | Shared runtime | Pass | Builds 240-245 passed targeted, full, and canonical five-assembly coverage validation. |
| DocFX/Axe/Lynx | Public XML and guide | Pass | DocFX completed with zero warnings/errors, Playwright/Axe passed 2/2, and representative UTF-8 Lynx output preserved semantic reading order. |
| Scope/secret/generated scan | Always | Pass | No prohibited scope or generated output is present; the current-diff secret scan reports zero high findings. |

For every repository validation helper, the explicit repository root, exit status, and error-channel review are recorded. Exit code zero with PowerShell ErrorRecords, command-not-found text, or a fatal signature is a failure.

For every explicit `dotnet build` or `dotnet test`, the immediately preceding manual build-counter value is recorded. One counter increment covers exactly one invocation.

## Remote Delivery

| Item | Result | Evidence |
|---|---|---|
| Push | Open | Branch `025-core-runtime-conformance-hardening` |
| Pull request | Open | Prepared from this evidence; final URL is terminal external fact |
| Required checks | Open | Pull-request-context gate on final reviewed head |
| Review threads | Open | Copilot, Claude, and GraphQL query |
| Unavailable reviews | None yet | Record provider/quota limitation honestly |
| Reviewed head | Open | Verified externally to avoid self-invalidating commit |
| Merge | Open | Merge commit and branch deletion verified externally |
| Local `main` sync | Open | `HEAD == origin/main`, clean tree, verified externally |
| Causal closeout | Required | This file and `tasks.md`; one post-merge Evidence-only PR may record Feature-025 terminal facts and task/retrospective completion. Its own URL/head/merge stay external. |
| Duplicate workflow events | Open | PR-context checks are delivery gate; equivalent push runs are noise unless safe concurrency exists |

## Retrospective

- **Effective**: Evidence-first planning found task-order and closeout recursion defects before implementation.
- **Waste**: Open until delivery.
- **Recurring blocker**: Open until remote review.
- **Recommended refinement**: Classify after Feature-025 and closeout merge; promote only repeated or correctness-relevant evidence.

## Prepared Pull Request Description

### Summary

- close the nine accepted Core-025 runtime conformance findings with modern, additive C# contracts
- preserve historical Turbo Vision intent while using Free Vision only as a pinned secondary review source
- append exact real-path resolutions to the immutable Feature-024 audit and keep F010-F013, Feature 026/028, and Wave 5/6 gated

### Validation

- targeted Release tests: Core 52/52, Compatibility 18/18, Controls 354/354, Drivers 117/117
- full Release suite: 725/725
- canonical line coverage: Core 92.96%, Controls 85.70%, Serialization 89.50%, Compatibility 80.55%, Drivers.Console 89.18%
- DocFX: 0 warnings/errors; Playwright/Axe: 2/2; representative UTF-8 Lynx review passed
- formatting, Markdown/UTF-8, scope, generated-output, and current-diff secret scans passed

### Delivery Boundary

The feature commit intentionally contains no recursive claims about its own push, pull request, reviewed head, merge, or local-main synchronization. Those terminal facts are verified externally and may be recorded once in the authorized causal evidence-only closeout.
