# Autonomous Run Evidence: Terminal and Charset Hardening

**Branch**: `021-terminal-charset-hardening`
**Feature directory**: `specs/021-terminal-charset-hardening`
**Binding intake**: `Lastenheft_05_TerminalCharsetAndEmulation.021-terminal-charset-hardening.md`
**Delivery mode**: `MergeAndSync`
**Authority source**: User instruction to implement Features 018-023 autonomously, including authorized non-empty PRs, merge, main sync, and narrowly bounded admin bypass

## Scope

### Included

- Driver-owned in-process session, bounded terminal subset, Unicode/KOI8-R
  mapping, raw 8x16 font-fixture validation, closed profiles, one Controls
  projection, app-loop/cell proof, host evidence, docs, governance, and validation.

### Excluded

- Host process, shell, PTY, host font/codepage/keyboard-map/profile/audio changes,
  full ANSI/VT/XTerm/Eterm emulation, visible Wave-4 examples, new dependencies,
  broad redesign, network/cloud/runtime AI, arbitrary user paths, and `tv203s/` edits.

## Run Gates

| Phase | Attempt | Result | Evidence | Remaining action |
|---|---:|---|---|---|
| Preflight | 1 | Pass | Branch/origin baseline `26cf842118b97b133bfe99e4c82759db9c2b40f2`; `specify check`; prerequisites; presets; 60/60 checklist items | None |
| Specify | 1 | Pass | `spec.md`; 30 FR, 16 CR, 13 SC | None |
| Clarify | 3 | Pass | Ten accepted decisions; third pass found no material ambiguity | None |
| Checklists | 4 | Pass | `checklists/`; 60/60 complete | None |
| Plan | 1 | Pass | `plan.md`, research, model, contract, quickstart | None |
| Tasks | 1 | Pass | `tasks.md`; T001-T166 sequential with no duplicate or gap | None |
| Analyze | 2 | Pass | Three first-pass artifact gaps remediated; final pass maps 59/59 FR/CR/SC, 166 tasks, no unmapped task, Critical/High/Medium 0 | None |
| Implement | 1 | Pass | T001-T155 complete; runtime, mapping, profile, host, app-loop proof, governance, docs, routing, statistics, archive, and local validation delivered | None |
| Validate | 1 | Pass | Trigger matrix below; targeted/full/coverage/DocFX/A11Y/text/secret/hygiene gates green | None |
| Deliver | 1 | Open | Authorized Phase 11 | Complete local gates first |

## Artifact Convergence

| Artifact or pass | Result | Evidence |
|---|---|---|
| Requirements quality | Pass | `checklists/requirements.md` 16/16 |
| Domain acceptance | Pass | `checklists/domain-acceptance.md` 16/16 |
| Plan quality | Pass | `checklists/plan-quality.md` 16/16 |
| Plan execution review | Pass | `checklists/plan-review.md` 12/12 |
| Task IDs | Pass | T001-T166; no duplicates or gaps |
| Requirement coverage | Pass | `tasks.md` maps FR-001..FR-030, CR-001..CR-016, SC-001..SC-013 |
| Final Analyze | Pass | 59/59 requirements mapped, 166/166 unique sequential tasks, no unmapped task, no Constitution issue, and Critical/High/Medium 0 |

## Preflight Results

| Check | Result | Evidence |
|---|---|---|
| Branch and ancestry | Pass | Branch `021-terminal-charset-hardening`; `HEAD`, merge-base, and `origin/main` were `26cf842118b97b133bfe99e4c82759db9c2b40f2` before feature edits |
| Feature metadata | Pass | `.specify/feature.json` references `specs/021-terminal-charset-hardening` |
| `specify check` | Pass | CLI ready; required local agent/tool integrations detected |
| PowerShell prerequisites | Pass | Feature directory and research/model/contracts/quickstart/tasks resolved |
| Checklists | Pass | requirements 16/16, domain 16/16, plan quality 16/16, plan review 12/12 |
| Presets | Pass | security 0.6.0/10; architecture 0.5.0/20; isaqb 0.2.0/30; a11y 0.4.0/40; cross-platform 0.2.0/50; agent-parity 0.3.0/60 |
| Governance conflict | None | Accepted 021 artifacts align with Constitution, current preset matrix, and Feature-020 baseline |
| Initial scope diff | Pass | `git diff -- tv203s/ examples/` empty before implementation |

## Optional Command Disposition

| Command | Result | Rationale |
|---|---|---|
| `speckit-constitution` | N/A | Constitution and six-preset matrix contain no material conflict requiring amendment |
| `speckit-taskstoissues` | N/A | One dependency-ordered, single-writer feature PR is the delivery unit; remote issue fan-out would add no proof |

## Compile-Surface Review

| Surface | Result | Decision |
|---|---|---|
| Project graph | Pass | Drivers.Console -> Core; Controls -> Drivers.Console/Core; Compatibility -> Core; no cycle or new reference required |
| Cells and snapshots | Pass | Existing `TConsoleCell`, `TConsoleBuffer`, Driver resize/presentation, and Controls draw-buffer contracts are sufficient |
| Imports and XML docs | Pass | New public Driver/Controls contracts require complete bilingual XML docs; no undocumented public API is accepted |
| Harness helpers | Pass | Existing Controls app-loop and buffer/cell proof patterns can host `TTerminalView` without an example project |
| Ownership assertions | Pass | Tests will assert exact session/view identity, lifecycle, cursor, status, profile metadata, and cells rather than text alone |
| Assembly identity | Pass | Session types compile once in Drivers.Console and are referenced by Controls/tests; no linked-source identity assumption |
| Compatibility boundary | Pass | `TConsoleInputAdapter` remains in Compatibility; no duplication, move, or Drivers reference is planned |
| Parser ownership | Pass | No reusable session/parser currently exists; Drivers.Console is the smallest non-visual owner |
| Placeholder and ID scan | Pass | No unresolved clarification, TODO/TBD, temporary requirement suffix, duplicate task, or task gap |
| Historical/example diff | Pass | `git diff -- tv203s/ examples/` empty before and after read-only review |

## Session and Emulation Matrix

| Area | Required accepted proof | Required boundary/negative proof | State publication rule | Result |
|---|---|---|---|---|
| Text and cells | Printable text, wrap, scroll, clipping | Empty, one-cell, one-row, disposed | Complete observation once | Pass: `TerminalSessionTests` |
| C0 controls | BEL, BS, TAB, CR, LF | Edge positions, mixed CR/LF, no host BEL | Complete control once | Pass: expanded `TerminalSessionTests` |
| Cursor | CSI A/B/C/D, H/f | Defaults, zero, 9,999, clamp, 10,000 reject | Atomic | Pass: relative/absolute matrix |
| Erase | CSI J/K documented modes | Unsupported mode, malformed/truncated | Atomic | Pass: modes 0/1/2 and negative matrix |
| Attributes | CSI m reset and 16 colors | Unsupported code, parameter overflow | Atomic | Pass: all 16 foreground/background colors |
| Reset/lifecycle | Reset, close, capability loss, dispose | Repeated cleanup, input after close | Idempotent | Pass: reset/close/dispose and shared capability cleanup contract |
| Recovery | Next valid text/sequence | Unknown, 63/64/65 chars, 4/5 params | No next-input loss | Pass: exact size/parameter/value and recovery matrix |
| History/resize | FIFO 4,096, top-left preservation | 4,095/4,096/4,097, shrink/grow | Bounded | Pass: exact history and resize tests |

### Session Red/Green Evidence

| Attempt | Version | Result | Boundary |
|---:|---|---|---|
| Red 1 | `1.21.0.144` | Expected compile failure | Missing `TerminalSession`, lifecycle, and result contracts only |
| Green attempt 1 | `1.21.0.145` | 9/11 | Test precision corrected: CR/LF row placement and evicted-cell count semantics |
| Green attempt 2 | `1.21.0.146` | 10/11 | Final boundary assertion used write count instead of evicted-cell count |
| Green 1 | `1.21.0.147` | 11/11 | Text, cursor, snapshots, delayed wrap, scroll, 4,095/4,096/4,097 history, resize, atomic rejection/recovery, reset/close/dispose passed |

The initial red matrix stayed in one Driver-owned file and named every expected
state boundary. A static source scan found no process, shell, PTY, host audio,
font, codepage, or terminal-setting API in `TerminalSession.cs`.

### Expanded Emulation Evidence

| Attempt | Version | Result | Boundary |
|---:|---|---|---|
| Expanded run 1 | `1.21.0.148` | 27/28 | C# variable-length `\x` literal in the reset test consumed `c`; changed to explicit `\u001b` |
| Expanded green | `1.21.0.149` | 28/28 | Complete C0/CSI/reset/color/atomicity/63-65/4-5/9,999-10,000 matrix passed |

The deterministic malformed-input sweep uses explicit MSTest rows and fixed
sizes. It is not a broad fuzzing claim and adds no dependency.

## Charset, Font, and Profile Matrix

| Area | Positive contract | Negative/fallback contract | Host boundary | Result |
|---|---|---|---|---|
| Unicode | Valid scalar identity | Invalid unit -> U+FFFD/Replaced | Locale independent | Pass |
| KOI8-R | Known byte map | U+FFFD is the sole replacement boundary | Fixed table, no host codepage | Pass |
| Other charset | None | Unsupported, no implicit conversion | No host fallback | Pass |
| Raw font | 8x16, 256 glyphs, 16 bytes/glyph, 4,096 bytes | Geometry/count/stride/truncated/oversized/format/path reject | No install/generator | Pass |
| Minimal profile | ProfileId + Charset | Missing/invalid required reject | In-process JSON | Pass |
| Optional profile | Font/colors/defaults | Unknown/duplicate/malformed reject; unavailable request falls back visibly | No host mutation | Pass |

### Charset, Font, and Profile Red/Green Evidence

| Slice | Red version/result | Green version/result | Proof |
|---|---|---|---|
| Charset/font | `1.21.0.150`, expected missing contracts | `1.21.0.151`, 6/6 | KOI8-R, Unicode/U+FFFD, Culture independence, exact fixture, nine negative fixture boundaries |
| Profile/host | `1.21.0.152`, expected missing contracts | `1.21.0.153`, 17/17 | Minimal/full/default profiles, malformed/unknown/duplicate/type/color/required rejection, unavailable capability fallback, five host classes |

## Controls Proof Matrix

| Route | Required state | Visible proof | Fallback/proof limit | Result |
|---|---|---|---|---|
| `app.Run()` controlled text/key input | Session, cursor, effective profile/charset/font metadata | Concrete `TTerminalView`, status line, cell positions | No Wave-4 example or host process | Pass |
| Disabled/unsupported capability | Session remains controlled; quit works | Text-readable fallback and visible cells | Physical terminal is separate evidence | Pass |
| Existing key translation | Compatibility contract unchanged | Existing exact xterm key tests plus targeted batch | No duplicate parser | Pass |

### Controls Red/Green Evidence

| Attempt | Version | Result | Boundary |
|---:|---|---|---|
| Red | `1.21.0.154` | Expected compile failure | Missing `TTerminalView` only |
| Green attempt | `1.21.0.155` | 4/5 | Narrow status clipped `Unsupported`; capability moved directly after profile identity |
| Green | `1.21.0.156` | 5/5 | Draw, cursor, status, key input, resize, app-loop state/view/cells/quit, and unsupported fallback passed |
| Targeted regression | `1.21.0.157` | Driver 63, Controls 5, matching smokes 3 | Session/profile/font/capability map, app-loop, and existing Compatibility boundary passed |

## Framework Decisions

| Area | Decision | Existing component | Planned local logic | Rationale | Evidence | Follow-up boundary |
|---|---|---|---|---|---|---|
| Session/emulation | SmallFrameworkFix | Drivers.Console and Core buffer | Bounded in-process session/parser | Reusable Wave-4 foundation is missing | `TerminalSessionTests` 11/11 at `1.21.0.147` | Full ANSI/VT/XTerm, shell/PTY |
| Buffer/cells | UseExistingFramework | `TConsoleBuffer`/`TConsoleCell` | None | Existing snapshot/cell contract is sufficient | Snapshot, resize, wrap, scroll, history tests | Alternate raster renderer |
| Charset mapping | SmallFrameworkFix | Unicode cells | KOI8-R mapper/results | Historical byte intent needs deterministic mapping | Charset tests 6/6 at `1.21.0.151` | Additional codepages |
| Font fixture | SmallFrameworkFix | Core cell geometry | Raw 8x16 metadata validator | Wave-4 needs reproducible fixture proof | Font tests at `1.21.0.151` | SFT/PSF/compression/install |
| Profile/fallback | SmallFrameworkFix | `System.Text.Json` policy | Closed schema and effective metadata | Eterm/XTerm config intent needs a safe contract | Profile tests 17/17 at `1.21.0.153` | Native resource syntax |
| Controls projection | SmallFrameworkFix | `TView`, app loop, draw buffer | One `TTerminalView` | Primary view/cell proof is required before Wave 4 | Controls tests 5/5 at `1.21.0.156` | Visible example composition |

## Didactic Comment Decisions

| Area | Decision | Rationale | Planned boundary | Result |
|---|---|---|---|---|
| Parser publication | CommentNeeded | Complete-before-mutation protects recovery | Two-line bilingual validation-before-apply block | Pass for initial slice |
| Scroll/history/resize | CommentNeeded | Cell eviction and top-left preservation are non-trivial | XML contracts plus focused implementation names; no trivial narration added | Pass for initial slice |
| KOI8/replacement | CommentNeeded | Host-independent historical mapping is not obvious | Two-line fixed-table versus host-codec reason | Pass |
| Font fixture | CommentNeeded | Metadata proof deliberately avoids host installation | XML contract and exact validator names; no trivial inline narration | Pass |
| Profile fallback | CommentNeeded | Invalid schema rejection differs from unavailable-capability fallback | Closed-schema flow plus distinct result vocabulary | Pass |
| Controls proof | CommentNeeded | Profile/font metadata is visible without custom host raster | Two-line cursor proof boundary and status-priority regression | Pass |
| Obvious declarations/accessors | NoCommentNeeded | XML docs and names are sufficient | No inline restatement | Accepted |

## Historical Intent

| Modern area | Historical source | Intent retained | Intentional deviation | Proof or rationale |
|---|---|---|---|---|
| Terminal buffer | `examples/terminal/terminal.cc`, `include/tv/terminal.h` | A bounded terminal text view, scrolling history, and visible dialog proof | Managed cells/session instead of byte ring, streambuf, shell/process, or mechanical `TTerminal` port | Reviewed read-only; Driver/Controls tests own proof |
| KOI8-R | Cyrillic Linux/X11 `test.cc`, Linux README and setup scripts | KOI8-R display intent, explicit capability, and fallback awareness | Unicode canonical output; no root access, `/dev/vcsa`, `consolechars`, `loadkeys`, host codepage, or auto-detection claim | Reviewed read-only; mapper tests own proof |
| Font fixture | fonts `test.cc`, `font.016`, `genraw.cc`, `ocr.sft`, `fontcoll.cc/.h` | 8x16/256-glyph fixture, geometry checks, and unavailable-font status | Validate raw bytes as metadata only; no host set/restore, shell, SFT generation, or compressed-format parsing | Reviewed read-only; fixture tests own proof |
| Profiles | Eterm configs/docs and XTerm resources | Named terminal capabilities, key/profile variance, and explicit setup requirements | Closed JSON with safe defaults; no terminfo/theme/resource installation | Reviewed read-only; profile tests own proof |
| XTerm screen/key | Unix `xtermkey.cc`, `xtermdis.cc`, `xtermscr.cc` | Bounded escape handling, cursor/state, terminal capability and cleanup intent | Existing Compatibility input remains separate; no termios, tty opening, signals, palette/font/window mutation | Reviewed read-only; session/host tests own proof |

## Host Evidence

| Host | Terminal condition | Evidence class | Capability/result | Residual risk | Re-evaluation trigger |
|---|---|---|---|---|---|
| macOS | Darwin arm64, `TERM=dumb`, stdin/stdout non-interactive | DeterministicInProcess Pass; PhysicalObservation NotRun | Host family contract Pass; physical NotRun | Physical terminal diversity | Safe interactive macOS spot-check available |
| Linux | No local physical host | DeterministicInProcess Pass; RemoteCI pending; PhysicalObservation NotRun | Contract Pass | Terminal/backend variation | Linux CI or manual host available |
| Windows/WSL | No local physical host | DeterministicInProcess Pass; RemoteCI pending; PhysicalObservation NotRun | Contract Pass | Native/WSL split | Windows/WSL CI or manual host available |
| Headless/redirected | No physical terminal | DeterministicInProcess | Unsupported classification Pass; in-process session remains deterministic | None after fail-safe proof | I/O model changes |

## Governance Applicability

| Preset | Version | Checkpoint | Applicability | Rationale | Evidence path | Owner | Reviewer | Result | Residual risk | Follow-up | Re-evaluation trigger |
|---|---|---|---|---|---|---|---|---|---|---|---|
| security-governance | 0.6.0 | NIST SSDF/CWE/input/resource validation | Applicable | Untrusted sequences, JSON, bytes, lifecycle, and bounds change | This file, tests, `docs/security/threat-model.md` | Feature owner | Codex | Pass 2026-07-12 | Low after bounded matrices | None | Contract changes |
| security-governance | 0.6.0 | ASVS/supply chain/AI/regulation | N/A | No web/auth, dependency, distribution, product AI, or regulated operation | Existing ledgers plus this file | Feature owner | Codex | N/A reviewed 2026-07-12 | Trigger drift | None | Named trigger enters scope |
| architecture-governance | 0.5.0 | STRIDE/CIA/CAPEC | Applicable | Spoofed sequences, state corruption, resource exhaustion, and false capability claims | This file, threat model, runtime view | Feature owner | Codex | Pass 2026-07-12 | Physical variance | Host evidence remains separated | Trust boundary changes |
| architecture-governance | 0.5.0 | S-ADR/arc42/Zero Trust/SAMM/C3A/C5 | N/A | Existing local boundary; no cloud/distributed/provider/operations boundary | Existing architecture/cloud ledgers | Feature owner | Codex | N/A reviewed 2026-07-12 | Architecture drift | None | New deployment/provider boundary |
| isaqb-architecture-governance | 0.2.0 | Component ownership/quality goals | Applicable | Driver/Core/Controls/Compatibility ownership is central | `plan.md`, research, this file | Feature owner | Codex | Pass 2026-07-12 | Boundary leakage | None | Component graph changes |
| a11y-governance | 0.4.0 | Keyboard/text/WCAG/comments | Applicable | Visible status, keyboard quit, learner docs, and non-trivial logic change | Controls tests, guide, DocFX, axe, and lynx evidence below | Feature owner | Codex | Pass 2026-07-12 | Physical assistive-technology diversity remains outside local proof | None | UI/docs or generated-doc structure changes |
| cross-platform-governance | 0.2.0 | Host/charset/font matrix | Applicable | Host terminal, locale, font, and capability differ | Host table, mapping/profile tests, guide | Feature owner | Codex | Deterministic Pass 2026-07-12 | Physical Linux/Windows/WSL unavailable locally | Remote/manual evidence | Host contract changes |
| cross-platform-governance | 0.2.0 | Script parity | N/A | No script is planned or changed | Diff review | Feature owner | Codex | N/A reviewed 2026-07-12 | Scope drift | None | Script enters diff |
| agent-parity-governance | 0.3.0 | Five agent surfaces | Applicable | Active feature context changes | Five agent files | Feature owner | Codex | Pass 2026-07-12; 021 block hash `47d8dac92d681cad5e1c07d23dfae4c659e58dc1fa529165dfafd9b102d0b364` | None | None | Shared context changes |
| agent-parity-governance | 0.3.0 | `.specify/templates/` | N/A | No generic workflow rule changed in feature implementation | Diff review | Feature owner | Codex | N/A reviewed 2026-07-12 | Retrospective may find generic issue | Separate retro PR | Generic correction proven |

## Validation

| Command or review | Trigger | Result | Evidence or failure boundary |
|---|---|---|---|
| `git diff --check` | Always | Pass | Worktree and staged diff contain no whitespace errors |
| Placeholder/task/scope scans | Always | Pass | 166 sequential tasks; no unresolved requirement marker; no `tv203s/` or `examples/` implementation diff; no duplicate parser |
| `dotnet format --verify-no-changes --no-restore` | C# changes | Pass | Final formatting verification completed without changes |
| Targeted Drivers/Controls/Compatibility Release tests | Touched projects | Pass | `1.21.0.158`: Drivers 51/51, Controls 314/314, Compatibility 18/18 |
| Full Release tests | Shared runtime changes | Pass | `1.21.0.159`: Core 44, Serialization 44, Controls 314, Compatibility 18, Drivers 105, example smokes 115; total 640/640 |
| Canonical Coverlet gate | Shared runtime changes | Pass | `xmllint` passed; `1.21.0.160`: Core 89.78%, Controls 83.42%, Serialization 89.50%, Compatibility 80.55%, Drivers.Console 89.18%; all required assemblies >=70% |
| `docfx docfx.json` plus Playwright/axe | Public XML/guide/toc | Pass | DocFX generated 275 models with 0 warnings/errors; Playwright/axe 2/2 passed |
| UTF-8 text-browser review | Learner guide | Pass | `lynx -dump -display_charset=UTF-8` exposed semantic headings, tables, bilingual text, and skip links without layout-only meaning |
| Secret/generated-output hygiene | Always | Pass | PowerShell secret scan found no diff or tracked secret; `_site/`, `api/`, TestResults, Playwright reports, and test results removed after validation |

## Requirement and Success Coverage

| Range | Primary evidence | Current result |
|---|---|---|
| FR-001, FR-002, FR-003, FR-004, FR-005 | `Session and Emulation Matrix`, `Session Red/Green Evidence`, `TerminalSessionTests.cs` | Pass: controlled in-process session, cells, cursor, lifecycle, resize, scroll, and host isolation |
| FR-006, FR-007, FR-008, FR-009 | `Expanded Emulation Evidence`, `TerminalSessionTests.cs` | Pass: complete supported subset, validation-before-mutation, exact malformed bounds, stable outcomes, and recovery |
| FR-010, FR-011, FR-012 | `Charset, Font, and Profile Matrix`, `TerminalCharsetAndFontTests.cs` | Pass: Unicode, KOI8-R, U+FFFD, explicit outcomes, and host independence |
| FR-013, FR-014, FR-015 | `Charset, Font, and Profile Matrix`, fixture and font tests | Pass: exact controlled raw fixture and rejection boundaries; no generator, install, or arbitrary path |
| FR-016, FR-017 | `Charset, Font, and Profile Matrix`, `TerminalProfileTests.cs` | Pass: closed profile schema, atomic rejection, defaults, requested/effective values, and visible fallback reason |
| FR-018, FR-019, FR-020 | `Controls Proof Matrix`, `TTerminalViewTests.cs` | Pass: app-loop state, view identity, status, keyboard, and cell proof; helpers remain supplemental |
| FR-021, FR-022 | `Host Evidence`, `TerminalProfileTests.cs` | Pass locally: deterministic host classifications separated from remote and physical observation |
| FR-023, FR-024 | `Framework Decisions` | Pass: exactly six decisions, reusable logic in Drivers/Core/Controls, no example-local subsystem |
| FR-025 | `Historical Intent` | Pass: terminal, Cyrillic, font, Eterm, and XTerm sources reviewed read-only with deliberate deviations |
| FR-026, FR-027 | `Didactic Comment Decisions`, `docs/guides/terminal-charset-hardening.md` | Pass: selective bilingual reason-focused comments and DE-first/EN-second CEFR-B2 guide |
| FR-028, FR-029, FR-030 | This ledger, agent hash, `Pflichtenheft.md`, statistics, archive, and scope scans | Pass: complete evidence/routing/archive and zero Wave-4 example implementation |
| CR-001, CR-002, CR-003 | `Governance Applicability`, threat model, validation matrices | Pass: C# registry, NIST/CWE/input/resource controls, and STRIDE/CIA/CAPEC evidence |
| CR-004, CR-005, CR-006, CR-007, CR-008 | `Governance Applicability` | Pass: explicit trigger-based N/A decisions and re-evaluation boundaries |
| CR-009, CR-010, CR-011, CR-012 | A11Y/host/agent/preset rows, guide, app-loop tests, agent hash | Pass: six-preset applicability, keyboard/text proof, host matrix, and five-surface parity |
| CR-013, CR-014 | `Compile-Surface Review`, red/green tables | Pass: compile surface preceded red commands and negative cases stayed project-local and explicit |
| CR-015, CR-016 | `Remote Delivery`, pre-named closeout path | Local portion Pass; remote reviewed-head and operational facts remain T156-T166 |
| SC-001, SC-002, SC-003 | `Session and Emulation Matrix`, complete Driver matrix | Pass: every action and supported sequence has exact positive, boundary, atomicity, and recovery proof |
| SC-004, SC-005, SC-006 | Charset/font/profile matrix and Driver tests | Pass: required mapping classes, one exact fixture plus nine negative boundaries, and profile/fallback matrix |
| SC-007, SC-008 | Controls and host matrices | Pass locally: app-loop state/view/status/cells; all host rows classify evidence honestly |
| SC-009, SC-010 | Scope scans and `Framework Decisions` | Pass: zero example-local subsystem and exactly six admissible framework decisions |
| SC-011, SC-012 | `Validation` and `Didactic Comment Decisions` | Pass: all triggered local gates green; every non-trivial changed flow reviewed for comment need |
| SC-013 | Archive, routing/statistics/agent changes, `Remote Delivery` | Local portion Pass; merge and synchronized-main evidence remain T159-T166 |

## Generated and Sensitive Output Hygiene

| Surface | Required result | Current result |
|---|---|---|
| `_site/` and generated `api/` | Untracked/absent from commit | Pass: removed after DocFX/A11Y validation |
| `TestResults/`, coverage, caches, logs | Untracked/absent from commit | Pass: validation result/report directories removed; ignored build intermediates remain untracked |
| Credentials/secrets | No tracked or diff secret | Pass: repository PowerShell scanner and diff review |
| `tv203s/` | No diff | Pass before and after read-only historical review |
| `examples/` | No 021 implementation diff | Pass: no Wave-4 port or example-local terminal subsystem |

## Local Completion Summary

| Item | Result |
|---|---|
| Local tasks | T001-T155 complete; 155/166 complete before authorized remote delivery |
| Changed files | 40 paths: 6 source, 7 test/fixture, 12 feature artifacts, 7 docs/routing/statistics, 5 agent surfaces, and 3 version/metadata/intake paths |
| Runtime proof | Targeted 383/383 across affected projects; full Release 640/640 |
| Coverage | All five required assemblies exceed 70%; lowest is Compatibility at 80.55% |
| Documentation/A11Y | DocFX 0 warnings/errors, Playwright/axe 2/2, UTF-8 lynx text-first review passed |
| Conditional evidence | Physical Linux/Windows/WSL and interactive macOS observations remain `NotRun`; deterministic contracts passed and no host mutation was attempted |
| Scope | No host process/shell/PTY, dependency, Wave-4 example, generated output, `tv203s/`, or broad redesign change |
| Follow-up | Wave-4 visual examples consume these contracts; full terminal emulation, native resource syntax, additional codepages, host raster installation, and process integration remain out of 021 |
| Archive | `Lastenheft_05_TerminalCharsetAndEmulation.021-terminal-charset-hardening.md` |
| Commit preparation | Pass: version `1.21.1.160`; 40 intentional paths staged; no generated, `tv203s/`, `examples/`, dependency, or secret path in the index |
| Commit evidence boundary | The planned implementation commit is the first 021 branch commit; its observed hash and later reviewed-head facts belong only in `closeout-evidence.md` |

## Remote Delivery

| Item | Result | Evidence |
|---|---|---|
| Push | Open | Authorized after local completion |
| Pull request | Open | Ready feature PR required |
| Required checks | Open | Verify on final reviewed head |
| Review threads | Open | GraphQL-aware zero-actionable requirement |
| Unavailable reviews | None observed yet | Quota/provider limitations will be recorded as missing review |
| Reviewed head | Open | Recorded only after remote verification |
| Merge | Open | Merge commit after convergence |
| Local `main` sync | Open | Require clean `HEAD == origin/main` |
| Causal closeout | Pre-named | `specs/021-terminal-charset-hardening/closeout-evidence.md`; used only for self-invalidating reviewed-head and post-merge facts |

## Retrospective

- **Effective**: Evidence-first ownership and a Driver vertical slice kept the parser, mapping, profile, and Controls projection independently testable.
- **Waste**: One focused test rerun corrected a C# variable-length `\x` test literal, and one Controls rerun exposed status clipping; both added concrete proof rather than repeating an unchanged gate.
- **Recurring blocker**: None. Physical host diversity remained an explicitly classified evidence limit, not a local blocker.
- **Recommended refinement**: Treat text-status priority under narrow rendering as feature-specific A11Y evidence. Observe targeted solution-level test filtering before promoting any batching rule; no workflow correction is yet proven by two runs.
