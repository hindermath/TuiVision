# Autonomous Run Evidence: Mouse Support and Interaction Hardening

**Branch**: `020-mouse-support-interaction`
**Feature directory**: `specs/020-mouse-support-interaction`
**Binding intake**: `Lastenheft_04_MouseSupportAndInteraction.020-mouse-support-interaction.md`
**Delivery mode**: `MergeAndSync`
**Authority source**: User instruction to implement Features 018-023 autonomously, including authorized non-empty PRs, merge, main sync, and narrowly bounded admin bypass

## Scope

### Included

- Bounded SGR-1006 host ingress into the existing canonical `TEvent` contract.
- Click focus, exactly-once activation, deterministic double click, one `TWindow` title drag, keyboard fallback, host evidence, docs, governance, and validation.

### Excluded

- Native Windows Console mouse backend, X10/full terminal parity, wheel, hover,
  touch, extra buttons, second drag target, TP7 demo, Wave-4 terminal/charset
  behavior, new dependencies, broad redesign, cloud/network/runtime AI, and `tv203s/` edits.

## Run Gates

| Phase | Attempt | Result | Evidence | Remaining action |
|---|---:|---|---|---|
| Preflight | 1 | Pass | Branch and `origin/main` baseline `c7964ab88b73184347d30e34813fcb6680d2307a`; `specify check`; prerequisites; presets; 56/56 checklists | None |
| Specify | 1 | Pass | `spec.md`, requirements 18/18 | None |
| Clarify | 2 | Pass | Four decisions in `spec.md`; second pass found no material ambiguity | None |
| Checklists | 3 | Pass | `checklists/`, 56/56 complete | None |
| Plan | 1 | Pass | `plan.md`, research, model, contract, quickstart | None |
| Tasks | 1 | Pass | `tasks.md`, T001-T126 sequential | None |
| Analyze | 3 | Pass | Final pass: 54/54 FR/CR/SC present and mapped, 126 tasks, 56/56 checklist items, no unmapped task, Critical/High/Medium 0 | None |
| Implement | 1 | Pass | T001-T115 local tasks complete; runtime, tests, docs, governance, statistics, archive, and final consistency complete | Authorized delivery T116-T126 |
| Validate | 1 | Pass | 120 targeted, 584 full, five coverage gates, DocFX/axe/lynx, format/diff/secrets | None |
| Deliver | 1 | Closeout in progress | PR #48 merged and `main` synchronized; Remote Delivery table | Record causally post-merge facts in the authorized non-empty closeout PR |

## Artifact Convergence

| Artifact or pass | Result | Evidence |
|---|---|---|
| Requirements quality | Pass | `checklists/requirements.md` 18/18 |
| Domain acceptance | Pass | `checklists/domain-acceptance.md` 13/13 |
| Plan quality | Pass | `checklists/plan-quality.md` 15/15 |
| Plan execution review | Pass | `checklists/plan-review.md` 10/10 |
| Task IDs | Pass | T001-T126, no duplicates or gaps |
| Requirement coverage | Pass | `tasks.md` maps FR-001..FR-026, CR-001..CR-015, SC-001..SC-013 |

## Preflight Results

| Check | Result | Evidence |
|---|---|---|
| Branch and ancestry | Pass | `020-mouse-support-interaction`; `origin/main` is ancestor; branch and origin baseline both `c7964ab88b73184347d30e34813fcb6680d2307a` before feature edits |
| Feature metadata | Pass | `.specify/feature.json` -> `specs/020-mouse-support-interaction` |
| `specify check` | Pass | CLI ready; required local agent/tool integration available |
| PowerShell prerequisites | Pass | Feature directory resolved; research, model, contracts, quickstart, and tasks available |
| Checklists | Pass | requirements 18/18, domain 13/13, plan quality 15/15, plan review 10/10 |
| Presets | Pass | security 0.6.0/10; architecture 0.5.0/20; isaqb 0.2.0/30; a11y 0.4.0/40; cross-platform 0.2.0/50; agent-parity 0.3.0/60 |
| Governance conflict | None | Accepted artifacts align with current Constitution and installed preset matrix |

## Optional Command Disposition

| Command | Result | Rationale |
|---|---|---|
| `speckit-constitution` | N/A | Current Constitution and six-preset matrix contain no material conflict requiring a constitutional edit |
| `speckit-taskstoissues` | N/A | The accepted delivery unit is one dependency-ordered feature PR; splitting 126 single-writer tasks into remote issues would add no execution proof |

## Ingress Observation Matrix

| Case | Capability | Raw/condition | Previous state | Expected publication | Rejection/recovery boundary | Result |
|---|---|---|---|---|---|---|
| Valid left press | Enabled | Complete SGR 1006 | Idle | One `MouseDown` | None | Pass |
| Valid pressed move | Enabled | Complete SGR 1006 move | Left pressed | One `MouseMove` | None | Pass |
| Valid release | Enabled | Complete SGR 1006 release | Left pressed | One `MouseUp` | Clear press | Pass |
| Invalid matrix | Any | Truncated/oversized/non-numeric/range/button/phase | Any | Zero | Next independent observation preserved | Pass |
| Qualifying double | Enabled | Same cell/target/left within 500 ms | Prior click | Second down has double flag | Monotonic only | Pass |
| Non-qualifying double | Enabled | 501 ms/cell/target/clock/reset mismatch | Any | Single click | Reset stale state | Pass |

### Driver Red/Green Evidence

| Attempt | Version | Result | Boundary |
|---:|---|---|---|
| Red 1 | `1.20.0.125` | Expected compile failure | Only missing `ConsoleMouseIngress`, capability, and rejection contracts |
| Green attempt 1 | `1.20.0.126` | Compile failure | Definite assignment in bounded number parser; corrected explicitly |
| Green attempt 2 | `1.20.0.127` | 52/53 | Existing capability-map test required the durable phrase `not reproduced` |
| Green 1 | `1.20.0.128` | 53/53 | Initial parser/state matrix passed without analyzer warnings |
| Host red | `1.20.0.129` | Expected compile failure | Missing deterministic host detector only |
| Host green attempt | `1.20.0.130` | Compile failure | Namespace collision required `System.Console` qualification |
| Driver final | `1.20.0.131` | 54/54 | Parser, state, recovery, double click, host classification, and existing Driver regressions passed |

The grouped red matrix retained an explicit expected result for every malformed,
range, phase, capability, recovery, and click-boundary case in one Driver-owned
test file. No cross-project ownership or hidden aggregate assertion was used.

## Interaction Matrix

| Area | Route | Target/focus | Command/drag outcome | Keyboard equivalent | Status/view/cell proof | Result |
|---|---|---|---|---|---|---|
| Click focus | `app.Run()` | Topmost second button | One focus transfer | Existing focus/key routes retained | `TButton` identity and status/cells | Pass |
| Activation | `app.Run()` | Focused second button | Command exactly once | Enter activates first button | Command count, identity, text/cells | Pass |
| Double click | `app.Run()` | Same concrete proof view | Two downs, exactly one double flag | Existing command route remains separate | Status/cells and concrete counters | Pass |
| Title drag | `app.Run()` | Movable `TWindow` | Clamped move/release plus focused cancellation tests | Ctrl+F5/arrows/Enter/Escape | Bounds, `TWindow`, final `┌` cell | Pass |
| Fallback | Disabled/unsupported app loops | First button | One keyboard activation | Enter | `Keyboard activated First` cells | Pass |

### Runtime/App-Loop Red/Green Evidence

| Attempt | Version | Result | Boundary |
|---:|---|---|---|
| Runtime red | `1.20.0.138` | Expected compile failure | Missing `ConfigureMouseCapability` and queue-status contracts only |
| Runtime green | `1.20.0.139` | 5/5 | Real GetEvent, focus/activation, double click, drag/cell, and fallback passed |
| Runtime final | `1.20.0.140` | 5/5 | Added explicit enabled-to-disabled shutdown assertion; all primary proof passed |

## Host Evidence

| Host | Terminal/condition | Capability | Evidence class | Result | Residual risk | Re-evaluation trigger |
|---|---|---|---|---|---|---|
| macOS | Interactive SGR terminal | Disabled until runtime enables | DeterministicInjection; physical NotRun | Contract Pass; physical NotRun because session stdin/stdout are not TTY and `TERM=dumb` | Physical terminal diversity | Interactive macOS spot-check |
| Linux | Interactive SGR terminal | Disabled until runtime enables | DeterministicInjection/RemoteCI pending | Contract Pass | No local physical host | Linux CI or manual host available |
| WSL | Windows Terminal plus WSL | Disabled until runtime enables | DeterministicInjection/RemoteCI pending | Contract Pass | Backend configuration varies | WSL CI/manual host available |
| Native Windows Console | No SGR contract in 020 | Unsupported | Contract review | Unsupported | No native mouse events | Dedicated native backend feature |
| Headless/redirected | Non-interactive I/O | Unsupported | DeterministicInjection | Pass | None after fail-safe proof | I/O model changes |

## Framework Decisions

| Area | Decision | Existing component | Local logic | Rationale | Evidence | Follow-up boundary |
|---|---|---|---|---|---|---|
| Host ingress | SmallFrameworkFix | `TConsoleDriver` | Bounded SGR parser/state | Missing real host-to-event path | Driver tests | Other protocols/backends |
| Canonical event | UseExistingFramework | `TEvent`/`TMouseEvent` | None | Existing contract is sufficient | Core tests | None |
| Focus routing | SmallFrameworkFix | `TGroup.SetFocus`/dispatch | Topmost hit selection | Current all-child mouse dispatch lacks ownership | Controls tests | Complex capture/bubbling |
| Activation | UseExistingFramework | Control handlers/commands | None | Existing command route must remain exactly once | App-loop proof | None |
| Double click | SmallFrameworkFix | `TMouseEvent.DoubleClick` | Driver classifier | Existing payload lacks runtime classifier | Driver/app proof | Configurable tolerance |
| Title drag | SmallFrameworkFix | `TWindow` keyboard move | One bounded mouse session | Reuses existing move capability | Window tests | Any second drag target |
| Keyboard fallback | UseExistingFramework | Current key/command routes | Status only | Mouse augments keyboard | App-loop proof | None |

## Didactic Comment Decisions

| Area | Decision | Rationale | Change or boundary | Result |
|---|---|---|---|---|
| SGR parser | CommentNeeded | Atomic publication and protocol limit are non-trivial | Structure plus XML contract make complete-before-publication boundary explicit; no trivial inline narration | Pass |
| Double-click state | CommentNeeded | Monotonic time and target key prevent false combining | Two-line bilingual clock-regression reason before reset | Pass |
| Topmost focus routing | CommentNeeded | Z-order and ownership are not obvious | Two bilingual blocks explain container FirstClick and shared Topmost target | Pass |
| Title drag cancellation | CommentNeeded | Multiple end paths protect state | Two-line bilingual release-versus-cancel boundary | Pass |
| Obvious enum/property declarations | NoCommentNeeded | XML docs are sufficient | No inline restatement | Pass |

## Historical Intent

| Modern area | Historical source | Intent retained | Intentional deviation | Proof or rationale |
|---|---|---|---|---|
| Event/double click | `tv203s/contrib/tvision/classes/tevent.cc`, `tmouse.cc`, `include/tv/event.h` | State changes produce down/up/move; repeated same-button/same-position presses inside `doubleDelay` carry double-click | Managed monotonic milliseconds, stable target key, no polling auto-repeat | Reviewed read-only; Driver/Core tests own proof |
| Host ingress | `tv203s/contrib/tvision/classes/unix/xtermmouse.cc`, Unix mouse headers | Explicit terminal reporting enable/disable and backend isolation | SGR 1006 only instead of historical 1000/1002/native matrix | Reviewed read-only; capability/lifecycle tests own proof |
| Hit/focus | `tv203s/contrib/tvision/classes/tview.cc`, `include/tv/view.h` | Global hit testing and one selected receiver | Managed recursive owner-chain coordinates and one topmost target | Reviewed read-only; Controls tests own proof |
| Window move | `tv203s/contrib/tvision/classes/twindow.cc`, `include/tv/window.h` | `wfMove`, owner bounds, resize/drag command, keyboard selection | Event-driven title drag plus retained managed keyboard mode; no grow/zoom | Reviewed read-only; window tests own proof |

## Compile-Surface Review

| Surface | Result | Decision |
|---|---|---|
| Project graph | Pass | Driver references Core; Controls references Driver/Core; no reverse reference or new project required |
| Imports and public XML docs | Pass | New public Driver capability/ingress APIs require complete bilingual XML documentation; no undocumented public type is accepted |
| Harness helpers | Pass | Existing queued `TProgram` patterns and buffer capture can be reused; new integration harness remains in Controls.Tests |
| Focus and ownership assertions | Pass | Tests assert `TGroup.Current`, `TViewState.Focused`, exact target identity, and command count rather than text alone |
| Linked-source identity | N/A | No new linked source file or cross-assembly type identity is planned |
| Target resolver boundary | Pass | Controls supplies a `TPoint -> string?` delegate; Driver stores only a stable key and never references a Controls type |
| Example-local parser scan | Pass | No current example contains SGR/1006/raw mouse parsing; future reusable logic remains prohibited |
| Placeholder scan | Pass | No unresolved marker; checklist/task references to marker names are normative instructions only |
| Historical diff | Pass | `git diff -- tv203s/` empty before and after read-only review |

## Controls and Drag Red/Green Evidence

| Slice | Version | Result | Boundary |
|---|---|---|---|
| Focus red | `1.20.0.132` | 1/5 pass, 4 expected failures | Nested coordinates and group focus ownership missing; outside negative already passed |
| Focus attempt | `1.20.0.133` | 2/6 pass | Container's inherited FirstClick cleared MouseDown before child routing |
| Focus green | `1.20.0.134` | 6/6 | Recursive coordinates, topmost hit, selectable focus, non-selectable no-focus passed |
| Focus regressions | `1.20.0.135` | 81/81 | Button, dialog, list, group, program, and new focus tests passed |
| Drag red | `1.20.0.136` | Expected compile failure | Missing mouse-drag state and capability-change contract only |
| Drag green | `1.20.0.137` | 6/6 | Press/multiple move/release, four-edge clamp, invalid starts, Escape/capability/disable/removal/shutdown, keyboard fallback passed |

## Governance Applicability

| Preset | Version | Checkpoint | Applicability | Rationale | Evidence path | Owner | Reviewer | Result | Residual risk | Follow-up | Re-evaluation trigger |
|---|---|---|---|---|---|---|---|---|---|---|---|
| security-governance | 0.6.0 | NIST SSDF/CWE/input validation | Applicable | Untrusted host input and state transitions change | This file, Driver/Controls tests, `docs/security/threat-model.md` | Feature owner | Codex | Pass 2026-07-12 | Low after bounded tests | None | Parser or protocol scope changes |
| security-governance | 0.6.0 | ASVS/supply chain/AI/regulation | N/A | No web/auth, dependency, distribution, product AI, or regulated operation | Existing ledgers plus this file | Feature owner | Codex | N/A reviewed 2026-07-12 | Trigger drift | None | Named trigger enters scope |
| architecture-governance | 0.5.0 | STRIDE/CIA/CAPEC | Applicable | Host spoofing, malformed input, duplicate dispatch, stale state, availability | `docs/security/threat-model.md`, this file | Feature owner | Codex | Pass 2026-07-12 | Physical terminal variance | Host evidence follow-up | Trust boundary changes |
| architecture-governance | 0.5.0 | S-ADR/arc42/Zero Trust/SAMM/C3A/C5 | N/A | Existing local component boundary; no cloud/distributed/provider boundary | Existing architecture/cloud ledgers | Feature owner | Codex | N/A reviewed 2026-07-12 | Architecture drift | None | New deployment/provider boundary |
| isaqb-architecture-governance | 0.2.0 | Component ownership/quality goals | Applicable | Driver/Core/Controls ownership and fallbacks are central | `plan.md`, `docs/architecture/runtime-view.md`, this file | Feature owner | Codex | Pass 2026-07-12 | Low boundary leakage risk | None | Component dependency changes |
| a11y-governance | 0.4.0 | Keyboard/text/WCAG/comments | Applicable | Mouse must not gate required operation; learner docs change | Tests, guide, DocFX/axe/lynx | Feature owner | Codex | Pass 2026-07-12 | Physical terminal variance | None | UI/docs change |
| cross-platform-governance | 0.2.0 | Host matrix | Applicable | macOS/Linux/WSL/native Windows differ | Host table and guide | Feature owner | Codex | Contract Pass 2026-07-12 | Linux/WSL physical checks unavailable locally | Remote CI/manual evidence | Backend support changes |
| cross-platform-governance | 0.2.0 | Script parity | N/A | No script changed | Diff review | Feature owner | Codex | N/A reviewed 2026-07-12 | Scope drift | None | Script enters diff |
| agent-parity-governance | 0.3.0 | Five agent surfaces | Applicable | Active feature context changes | Five agent files | Feature owner | Codex | Pass 2026-07-12; block hash `c585bf142913e875fab53c770a715009cef9005dc742e9a7513b9526e06240e4` | None | None | Shared guidance changes |
| agent-parity-governance | 0.3.0 | `.specify/templates/` | N/A | No generic workflow rule changed in feature implementation | Diff review | Feature owner | Codex | N/A reviewed 2026-07-12 | Later retrospective finding | Separate retro PR | Generic correction proven |

## Validation

| Command or review | Trigger | Result | Evidence or failure boundary |
|---|---|---|---|
| `git diff --check` | Always | Pass | No whitespace errors before and after generated cleanup/archive |
| Placeholder/scope/generated/tv203s scans | Always | Pass | No unresolved markers, example parser, dependency change, generated tracked output, or historical diff |
| `dotnet format --verify-no-changes --no-restore` | C# changes | Pass | Exit 0 at version `1.20.0.140` |
| Targeted Driver/Core/Controls Release tests | Touched projects | Pass | Version `1.20.0.141`; Driver 17 plus Controls 103 = 120/120 |
| Full Release tests | Shared runtime changes | Pass | Version `1.20.0.142`; Core 44, Serialization 44, Compatibility 18, Drivers 54, Controls 309, examples 115 = 584/584 |
| Canonical Coverlet gate | Shared runtime changes | Pass | Version `1.20.0.143`; Core 89.78%, Controls 83.29%, Serialization 89.50%, Compatibility 80.55%, Drivers.Console 81.36% |
| `docfx docfx.json` | Public XML/guide/toc changes | Pass | 254 models, 0 warnings, 0 errors |
| Playwright/axe DocFX smoke | DocFX run | Pass | 2/2 Chromium tests; no serious representative-page violations |
| UTF-8 lynx guide review | Learner guide | Pass | Generated mouse guide preserves headings, tables, lists, German/English text and skip links |
| Secret scan | Always | Pass | PowerShell diff/tracked scanner: no secrets; extra directory scan found only six generated `_site` documentation examples, removed with `_site` |

## Requirement and Success Coverage

| Range | Primary evidence | Result |
|---|---|---|
| FR-001..FR-007 | Ingress/host matrices and Driver tests | Pass |
| FR-008..FR-010 | Interaction matrix and Controls/app tests | Pass |
| FR-011..FR-017 | Drag/fallback/app-loop proof | Pass |
| FR-018..FR-026 | Project tests, decisions, history, docs, routing | Pass |
| CR-001..CR-015 | Governance and delivery tables | Local Pass; remote rows pending authorized delivery |
| SC-001..SC-013 | Validation, matrices, archive, remote closeout | Local Pass; SC-013 remote closeout pending |

## Generated and Sensitive Output Hygiene

| Surface | Required result | Result |
|---|---|---|
| `_site/` and generated `api/*.yml` | Untracked/absent from commit | Pass; removed after validation |
| `TestResults/`, coverage, caches, logs | Untracked/absent from commit | Pass; removed after evidence extraction |
| Credentials/secrets | No tracked or diff secret | Pass |
| `tv203s/` | No diff | Pass |

## Local Completion

- Tasks T001-T124 are complete; T125-T126 are completed by the authorized
  non-empty evidence-only closeout PR.
- Binding intake archived by the repository PowerShell workflow as
  `Lastenheft_04_MouseSupportAndInteraction.020-mouse-support-interaction.md` in commit `cfdd0bf`.
- Changed runtime scope is limited to Driver ingress/capability, Program lifecycle,
  recursive coordinates, topmost dispatch/focus, and one window title drag.
- No package, example, script, cloud, network, persistence, runtime AI, Wave-4,
  native Windows backend, generated output, or historical source entered scope.
- Remaining follow-up: physical interactive macOS/Linux/WSL terminal observations
  and any native Windows backend belong to host evidence or a later hardening feature.

## Remote Delivery

| Item | Result | Evidence |
|---|---|---|
| Staged scope | Pass | Intentional 020 source, tests, feature artifacts, docs, governance, metadata, statistics, and version only; `git diff --cached --check` and generated/sensitive path scan pass at `1.20.2.143` |
| Implementation commit | Pass | `f76a0bf` (`feat: harden mouse support and interaction`) at version `1.20.2.143`; 39 files, 3,678 insertions, 69 deletions |
| Evidence/version alignment | Pass | Bounded follow-up required because the implementation hash cannot be known inside its own commit; aligned to prospective third branch commit `1.20.3.143` without running another build/test |
| Push | Pass | Initial and evidence-aligned pushes succeeded; remote branch `020-mouse-support-interaction` first observed at `0d99df0b37ef66fe2af2a077cc24a25e6a7183b4`; pre-push tracked-secret scan passed |
| Pull request | Pass | Ready PR [#48](https://github.com/hindermath/TuiVision/pull/48), created from the evidence-derived scope and validation summary |
| Required checks | Pass on merged head `6944719` | Ubuntu/macOS CI, DocFX Pages, package/SBOM, Claude review, gitleaks, tracked-secret scan, and macOS/Ubuntu/Windows repository tooling passed; DocFX deployment correctly skipped for PR context |
| Review threads | Pass on merged head `6944719` | GraphQL-aware review fetch reported zero threads and zero conversation comments; no actionable remediation existed |
| Unavailable reviews | Missing review recorded | Copilot reported requester quota exhaustion for all three PR-request attempts; this is not counted as a passing review |
| Admin bypass | Pass within authority | Used only after all required checks passed, GraphQL reported zero actionable threads/comments, the PR was mergeable, and `REVIEW_REQUIRED` was the sole remaining rule |
| Merge | Pass | PR #48 merged with merge commit `b52d90f1e6a57ac090e124fa4ba1014a7cddc1dc` at 2026-07-12T02:25:27Z; remote feature branch deleted |
| Local `main` sync | Pass | Fetch/prune plus fast-forward pull completed; clean local `main` and `origin/main` both resolved to `b52d90f1e6a57ac090e124fa4ba1014a7cddc1dc` before creating the closeout branch |
| Closeout rationale | Required | Recording the final review, bypass, merge, deletion, and synchronized-main facts on the feature head was causally impossible: the evidence commit would have invalidated the reviewed head, while merge/sync facts did not yet exist |

## Retrospective

- **Effective**: Evidence-first task order and explicit Driver/Core/Controls ownership reduced ambiguity before red tests.
- **Waste**: A repository commit that records the PR URL and reviewed-head facts retriggers expensive required checks; evaluate a generic causal-evidence checkpoint after delivery without weakening truthful evidence.
- **Recurring blocker**: Copilot requester quota was unavailable for the third consecutive feature, while Claude completed without findings and GraphQL remained empty.
- **Recommended refinement**: Treat evidence whose commit would invalidate its own reviewed-head facts as causally post-merge evidence, and route it to one closeout checkpoint. Evaluate and implement this through the separate 020 retrospective PR.
