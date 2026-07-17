# Autonomous Run Evidence: Wave-6 TVFM Showcase Remediation

**Branch**: `036-wave6-tvfm-showcase-remediation`
**Feature directory**: `specs/036-wave6-tvfm-showcase-remediation`
**Binding intake**: `Lastenheft_21_Wave6-TVFM-Showcase-Remediation.036-wave6-tvfm-showcase-remediation.md`
**Delivery mode**: `MergeAndSync`
**Authority source**: Current user instruction
**Owner**: Thorsten Hindermann
**Reviewer**: Codex
**Review date**: 2026-07-17

## Scope and Preflight

- Baseline: clean synchronized `main` at
  `42a842fb63a0695a618a0f87ffec543e9bc3b6c8`; Feature-035 PR #101 and
  causal closeout PR #102 are merged. Intake PR #103 is merged through the
  baseline commit and introduced Lastenheft 21 plus the Feature-036 markers.
- Feature 035: `Retrospective`, `Completed`, `163/163`,
  `nextExactAction: N/A`.
- Identity: branch `036-wave6-tvfm-showcase-remediation`;
  `.specify/feature.json` is updated by this Specify stage.
- Included: one `Tp7FileManager` entry point, ten `W6S` showcase areas,
  visible access, safe dialogs, constrained layout, optional mouse intent,
  status, Description, proof, guide, governance, and delivery integration.
- Excluded: functional re-porting, wider filesystem authority, arbitrary user
  data, dependencies, another entry point, broad framework revision,
  Feature 037, independent closure, and portfolio audit.
- Read-only roots: `TVFM/`, `TVDEMOS/`, `tv203s/`, and external comparison
  checkouts.
- Tooling: macOS detected; PowerShell 7 is available and preferred for
  matching repository automation; `specify check` passed.
- Presets: Security 0.6.0 at priority 10, Architecture 0.5.0 at 20, iSAQB
  0.2.0 at 30, A11Y 0.4.0 at 40, Cross-Platform 0.2.0 at 50, Agent Parity
  0.3.0 at 60, and Autonomous Run 0.2.2 at 70.
- Remote authority permits feature commit, push, PR, review convergence,
  narrow Human-Approval-only bypass, merge, branch cleanup, and main sync.
- No intentional interruption is scheduled. Unexpected interruption requires
  read-only Status and explicit Resume.
- Shared single writers are `pr-evidence.md`, `tasks.md`, run state, gate
  requirements, `Tp7FileManagerApp.cs`, the showcase view and test files,
  guide, agent contexts, `Directory.Build.props`, status documents,
  statistics, and the archived intake. They are changed sequentially.
- Versioning is `1.36.<patch>.<build>`. Exactly one manual build-counter
  increment authorizes one explicit `dotnet build` or `dotnet test`; commit
  and push realignment does not increment without another such invocation.
- Intended implementation paths are the existing Wave-6 application and
  smoke-test project, one new example-local view file, the existing Wave-6
  guide and navigation, feature evidence, status/statistics, agent contexts,
  version fields, and the archived intake. No project or package file is added.
- Post-merge facts use at most
  `specs/036-wave6-tvfm-showcase-remediation/delivery-closeout.md`; a closeout
  remains evidence-only and does not claim its own PR or merge recursively.

## Feature-035 Historical Baseline

All 24 read-only files match the accepted Feature-035 hashes.

| Source | SHA-256 |
|---|---|
| `TVFM/ASSOC.PAS` | `6f063ce05767a5759d8e010f439ada682db9855108e09abe0657a79769b330d6` |
| `TVFM/COLORS.PAS` | `70a5304e75fdff06bb5042b65b3e9126796235c05f7027c9b0482af6c80f724f` |
| `TVFM/CYAN.PAL` | `7e76ad41b91f79bbd94271e0918b35c1d110a2dee0a2c50c4396ae9305b06b22` |
| `TVFM/DEFAULT.PAL` | `ea7964e4f88681a12009326e90cc4c75ac35a89bbcd35c61b400e1791988941f` |
| `TVFM/DIRVIEW.PAS` | `dd443d44d4d414607f857d570aaa186671e55eafc24f14f536ab12af5a55f0ba` |
| `TVFM/DRAGDROP.PAS` | `48d2303a068d023ab1ed4d33a408500a14e98124b1d4748b128827ea1dc7ded7` |
| `TVFM/EDITPAL.PAS` | `0cd9ae275d4bcc53a348be511892b65755df28fed535f0c58696eef74b753cbf` |
| `TVFM/EQU.PAS` | `0d576743ffd297cfeee5181b07ad2958c9028bf270893be669ceb48c75c7dc71` |
| `TVFM/FILECOPY.PAS` | `7811657673bd12839a227e490ee98525c61371f292713c221f281f8d84ba308d` |
| `TVFM/FILEFIND.PAS` | `224a627877b40b54673a103721b7728dd8eb122fc570760d461e016772a95b75` |
| `TVFM/FILEVIEW.PAS` | `343dee0bddbc869eff7e4729f6f3decbafeb0ba50ee1da137d67ebe8d72fd479` |
| `TVFM/GAUGES.PAS` | `7478ac17cd21f65245f00b2a5d53ea8d6207fd6192e12f644c10e39609eefc39` |
| `TVFM/GLOBALS.PAS` | `f593e95784b1c38b5aa899f43fa299c9297c1bf7fe33d3c0edcf3c3462d107f2` |
| `TVFM/INFOVIEW.PAS` | `f644b0e7b0369a18b49f5d70cb1c07935911c0cd87b4076fb44920a6d7c2067d` |
| `TVFM/MAKERES.PAS` | `7543c7a0f471893b208e5ab46dcacbff5f4a175d267ae55519386f3d228e2197` |
| `TVFM/MAKETVFM.BAT` | `4231a8f3b888ad3ca8f1b23a1f4d1810cee4565cef30a45d94f4566eda568285` |
| `TVFM/ROSE.PAL` | `d557882668638308c2fe1e1157acaf43da87bbb2ae55c59d6c5e812772beb0d6` |
| `TVFM/TOOLS.PAS` | `a8800797bca0ed45a780086601a8ba04a3ff7f9846376477f7851cf0bf72807e` |
| `TVFM/TRASH.PAS` | `115b64620483df2b54641f41352819caf80f9c8fcef67c8f1bbb527a225b78e0` |
| `TVFM/TREEWIN.PAS` | `54b7ef2d03655040e24c0b75843d4b679b658aba04785f13355241a89c6fff40` |
| `TVFM/TVFM.PAS` | `8e8da1b47d1d43e8835c98d82a9bc4cfc461d56d934080d75b60985703b15d0a` |
| `TVFM/TVFM.TVR` | `0e0f359e852a5e640355ca2748605f3565254c814e4b61b0a910aaf59821122c` |
| `TVFM/VIEWHEX.PAS` | `e478bc15a0b6dd663e9da4e976bf3656b86e78e2c6237f6f90c4bd7148b80cb2` |
| `TVFM/VIEWTEXT.PAS` | `15f1a7335d68b0e71536b9da1a36039e7231100932d4023b46c1098710920c64` |

## Run Gates

| Phase | Attempt | Result | Evidence | Remaining action |
|---|---:|---|---|---|
| Preflight | 1 | Pass | synchronized baseline, predecessor, branch, toolchain, seven presets | None |
| Specify | 1 | Pass | `spec.md`, requirements checklist, early state and gates | Repeated Clarify |
| Clarify | 2 | Pass | two focused passes found no material question | None |
| Checklists | 1 | Pass | eight checklists, 139/139 items complete | None |
| Plan | 2 | Pass | plan plus independent plan-review remediation and design artifacts | None |
| Tasks | 1 | Pass | 187 dependency-ordered sequential tasks with test-first vertical slice | Analyze |
| Analyze | 2 | Pass | 70/70 requirements and 187/187 tasks mapped; no Critical, High, Medium, Constitution, or unmapped finding remains | None |
| Implement | 5 | Pass | T031-T151; all five stories, 10/1 closure, documentation, governance, guidance, status, statistics, and archived intake are complete | Final local validation |
| Validate | 1 | Pass | T152-T167 local static, behavioral, coverage, documentation, A11Y, supply-chain, parity, text, scope, and state gates | Refresh exact candidate hashes, stage, then obtain remote and exact-head evidence |
| MergeAndSync | 2 | Pass | PR #104, final head `a0d5062`, 22 successful checks, 12/12 exact-head gates, zero actionable threads, authorized Human-Approval-only bypass, merge `559bffb` | None |
| Retrospective | 1 | Pass | `NoPromotion`; one repository test-design correction and no provider-neutral preset defect | None |

## Showcase Area Evidence

| Area | Scope | Feature-035 proof | Visible access | Normal / constrained proof | Focus / status / Description / keyboard | Framework contracts | Local showcase composition | Historical intent / deviation | Filesystem / A11Y / platform / security boundary | Decision | Residual risk / re-evaluation |
|---|---|---|---|---|---|---|---|---|---|---|---|
| W6S-001 | Navigation and list | Feature-035 controlled `List` and navigation | Persistent list plus Navigate command | Normal and `48x16` app-loop cells pass | `TListBox` focus, path status, F1, Ctrl+Q | `TWindow`, `TListBox`, `TStringList`, `ControlledFileWorkspace` | `Wave6ShowcaseWindow` maps immutable snapshots only | TVFM directory intent retained; modern split/stack layout | Controlled root; keyboard-first; no host path discovery | `UseExistingFramework` | Re-evaluate if reusable tree/navigation behavior is needed by another example |
| W6S-002 | Text and hex preview | Feature-035 bounded preview results | View menu and focused-list dispatch | Text, invalid UTF-8, truncation, offset, bytes, and printable cells | Selection and status retain the relative path | Existing preview result and controlled workspace | Detail panel wraps long proof lines without changing domain output | Internal text/hex intent retained; no external viewer | Byte/line limits and replacement remain visible | `UseExistingFramework` | Re-evaluate if scrolling viewer becomes a shared requirement |
| W6S-003 | Filter, sort, and tags | Feature-035 list/filter/sort contracts | Search and Options commands | Filter, sort, empty result, tag marker, and cells pass | Selection is preserved where the path remains | Existing immutable snapshots and example-local tag set | Header/list render state only | TVFM organisation intent retained with deterministic ordering | No ambient locale or arbitrary pattern path | `UseExistingFramework` | Re-evaluate if tags require persistence |
| W6S-004 | Search and cancellation | Feature-035 bounded breadth-first search | Search menu and command payload | Match, pre-cancel, 100-result limit, partial status, and cells pass | Status states cancel/complete/limit explicitly | Existing cancellation token and search limits | App supplies only the requested cancellation token | Search intent retained without background worker or host index | Controlled root, depth/file/result limits | `UseExistingFramework` | Re-evaluate if interactive progressive cancellation is introduced |
| W6S-005 | Association and internal viewer | Feature-035 extension decision | View Associated command | Text, hex, and unknown-extension fallback cells pass | Focused file drives the decision | Existing `DecideViewer` and preview methods | No process launch or shell association | TVFM association intent retained as internal safe choice | Unsupported extension requires visible manual choice | `IntentionalDeviation` | Re-evaluate only if a sandboxed shared viewer contract exists |
| W6S-006 | Copy, rename, delete, and read-only dialogs | Feature-035 typed intents and one-shot execution | File menu plus real modal dialog requests | Preview, Confirm, Cancel, rejection, result, and recovery cells pass | Stable input/button focus; Tab, Shift+Tab, Enter, Escape, F1 | Existing `TDialog`, `TInputLine`, `TButton`, validators, workspace | Example-local dialog state creates no filesystem authority | TVFM operation intent retained with explicit modern confirmation | Root-relative validation; no overwrite; TOCTOU revalidation; NoMutation | `UseExistingFramework` | Re-evaluate on directory mutation or shared file-operation dialog requirement |
| W6S-007 | Drag-and-drop intent | Feature-035 typed copy intent and confirmation | Left-button down/move/up inside the visible main region | Mouse and keyboard prepare equivalent intents; release never executes; invalid source/target/button cancels | Status exposes release or cancellation; keyboard path remains complete | Existing app/group event dispatch and `ControlledFileWorkspace` intent | Example-local drag state owns no write authority | Historical drag intent retained without direct filesystem drop | Escape, capability loss, view removal, shutdown, outside target, and `48x16` are bounded | `UseExistingFramework` | Re-evaluate if another example requires a shared drag-target contract |
| W6S-008 | Palette and resources | Feature-035 closed `Wave6Palette` | Options Palette command | Default, Cyan, Rose, HighContrast, and unknown fallback pass | Text labels expose state without color-only meaning | Existing enum and deterministic event payload | Unknown resource maps visibly to Default | Historical palette intent retained without host resources | Four-value closed set; no ambient theme lookup | `UseExistingFramework` | Re-evaluate if shared runtime palette application enters scope |
| W6S-009 | Help and Description | Feature-035 F1 dispatch | F1 and Help Description command | Persistent header and bounded detail in both layouts | DE-first/EN-second purpose, safety, modernization, platform, proof, and quit paths | Existing key/command dispatch and text-first views | Detail panel updates without replacing the main window | Historical learning purpose stated; no Pascal source copy | No personal files, shell, external viewer, or color-only meaning | `UseExistingFramework` | Re-evaluate when public help-topic integration enters example scope |
| W6S-010 | Status, focus, and layout | Feature-035 app-loop/status proof | Persistent first frame, list selection, focus-aware status, modal dialogs, and bounded mouse region | 80-column split and `48x16` stacked layouts pass with dialogs and mouse release/cancellation | Stable list/dialog focus; narrow status prioritizes F1 and Ctrl+Q | Existing view tree, focus routing, `TStatusLine`, modal dispatch, and render buffer | Bounded example-local panels and drag state own presentation only | TVFM desktop intent retained with responsive C# composition | Text-first state; shutdown and capability-loss proofs preserve NoMutation | `UseExistingFramework` | Re-evaluate only if future Wave-6 deltas change layout, focus, or shared input contracts |

## Entry-Point Decision

| Entry point | State proof | View/focus proof | Cell/status proof | App-loop proof | Final decision | Residual risk / re-evaluation |
|---|---|---|---|---|---|---|
| `Tp7FileManager` | Feature-035 bounded state plus visible read, dialog, palette, and mouse outcomes | Persistent `TWindow`, `TListBox`, modal controls, stable focus, normal and `48x16` layouts | Rendered first frame, previews, operation results, cancellation, StatusLine, F1, and Ctrl+Q | Real `app.Run()` dispatch for keyboard, command, modal, and mouse events | `ShowcaseComplete` | Re-evaluate if Wave-6 closure finds a new functional, interaction, platform, A11Y, or framework-reuse gap |

## Governance Applicability

| Preset | Version | Checkpoint | Applicability | Rationale | Evidence | Result | Residual risk / re-evaluation |
|---|---|---|---|---|---|---|---|
| security-governance | 0.6.0 | NIST SSDF and CWE Top 25 | Applicable | New example input, dialog, mouse, and proof logic requires bounded design, tests, review, and traceability | spec, plan, tasks, showcase tests, this file | Pass | Re-evaluate if authority, parser, input, or filesystem scope changes |
| security-governance | 0.6.0 | Secure filesystem and evidence integrity | Applicable | File-operation UI must preserve controlled-root, no-overwrite, one-shot, TOCTOU, hash, and fail-closed evidence boundaries | `ControlledFileWorkspace`, showcase tests, exact 10/1/24 validator | Pass | Residual platform attribute variance is explicit; re-evaluate on directory mutation or arbitrary-user paths |
| security-governance | 0.6.0 | OWASP ASVS | N/A | No web application, HTTP endpoint, browser authentication, or session contract is changed | scope and dependency scan | Accepted | Re-evaluate if a web-facing surface enters scope |
| security-governance | 0.6.0 | SBOM, VEX, SLSA, OpenSSF Scorecard | N/A | No package, dependency, release artifact, build provenance, or distribution contract changes | project/package diff and supply-chain workflow | Accepted | Re-evaluate on package, release, build, or provenance change |
| security-governance | 0.6.0 | AI-SBOM | N/A | AI is development tooling only; no model, dataset, runtime AI, or delivered AI component exists | scope review | Accepted | Re-evaluate if runtime or product AI enters scope |
| security-governance | 0.6.0 | NIS2, CRA, EU AI Act, DORA | N/A | No regulated service, critical-infrastructure role, commercial product release, runtime AI, financial service, or provider obligation changes | regulatory trigger review | Accepted | Re-evaluate when product, deployment, customer, AI, or regulated-service scope changes |
| architecture-governance | 0.5.0 | STRIDE, CIA, and CAPEC | Applicable | Dialogs and mouse intent must not obscure trust, integrity, authorization, cancellation, or TOCTOU boundaries | threat review, tests, W6S-006/W6S-007 | Pass | Re-evaluate if another write authority or external process is introduced |
| architecture-governance | 0.5.0 | S-ADR and arc42 security concepts | N/A | No new architecture decision, runtime boundary, deployment topology, or cross-component security concept is introduced | plan and framework decisions | Accepted | Re-evaluate on framework/API/runtime-boundary change |
| architecture-governance | 0.5.0 | Zero Trust and SAMM | N/A | No identity, network, service authorization, or organizational maturity-program scope exists | architecture trigger review | Accepted | Re-evaluate on identity, network, service, or maturity-program scope |
| architecture-governance | 0.5.0 | BSI C3A and BSI C5 | N/A | No cloud service, provider dependency, cloud autonomy decision, control environment, or assurance engagement changes | cloud trigger review | Accepted | Re-evaluate on cloud provider, hosted service, or cloud-compliance scope |
| isaqb-architecture-governance | 0.2.0 | Runtime, building-block, quality, and decision views | Applicable | Persistent composition, modal decisions, bounded mouse state, and app-loop proof require explicit ownership and quality evidence | plan, data model, W6S matrix, tests | Pass | Example-local composition remains bounded; re-evaluate if reused by another example |
| a11y-governance | 0.4.0 | Keyboard, focus, text-first, guide, contrast, and comments | Applicable | The learner-facing TUI and guide change | normal/`48x16` tests, F1/StatusLine/dialog proofs, guide, DocFX/Axe | Pass | Re-evaluate on new control, navigation, color, pointer-only action, or learner documentation |
| cross-platform-governance | 0.2.0 | Filesystem, input, terminal, and layout | Applicable | Paths, attributes, keyboard, optional mouse, and terminal dimensions vary | platform-aware tests, local/remote gates, W6S boundaries | Pass | Physical terminal behavior remains provider evidence; re-evaluate on platform-specific code |
| cross-platform-governance | 0.2.0 | Bash and PowerShell script parity | N/A | No repository script is added or changed | final path inventory | Accepted | Re-evaluate on `.sh`, `.ps1`, or workflow-script change |
| agent-parity-governance | 0.3.0 | Maintained and generated surfaces | Applicable | Active feature context is synchronized across five maintained files and generated Antigravity context | agent files, `.agent/rules/specify-rules.md`, homogeneity checks | Pass | `.specify/templates/` is unchanged; re-evaluate on shared guidance or template change |
| autonomous-run-governance | 0.2.2 | State, authority, exact head, review, and MergeAndSync | Applicable | The feature uses autonomous remote delivery | run state, gate requirements, this file, provider evidence, `delivery-closeout.md` | Pass | Re-evaluate on interruption, authority drift, changed head, review finding, or delivery failure |

## Validation

| Command or review | Trigger | Result | Evidence or failure boundary |
|---|---|---|---|
| `specify check` | Preflight | Pass | Exit 0; Spec Kit 0.12.11 and agent toolchain reviewed |
| prerequisite discovery | Before Clarify/Plan/Tasks/Analyze/Implement | Planned | Run at each required boundary |
| checklist completeness | Before Implement | Planned | All checklist files require zero incomplete items |
| state validators | Logical checkpoints | Planned | Bash and PowerShell must accept the same state |
| PowerShell state validator with unsupported `-RepositoryRoot` | US1 checkpoint | Failed invocation, not accepted | Parameter binding failed; corrected command uses repository working directory plus `-State` |
| `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~Wave6ShowcaseSmokeMatrixTests"` | US1 Red, `1.36.0.371` | NeedsRevalidation | The original invocation left no trustworthy terminal result, running process, TRX, or log; no result was inferred |
| `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~Wave6ShowcaseSmokeMatrixTests"` | US1 Red revalidation, `1.36.0.372` | Expected fail, exit 1 | Build and harness passed; 0/3 tests passed. Failures were limited to transient window identity, absent focused `TListBox`, and Description replacing rather than preserving TP7 TVFM first-frame context |
| targeted Wave-6 showcase plus preserved functional tests, `1.36.0.373` | US1 Green attempt 1 | Expected fail, exit 1 | 8/10 passed; proof incorrectly inspected the intentionally cleared post-shutdown `Desktop` |
| targeted Wave-6 showcase plus preserved functional tests, `1.36.0.374` | US1 Green attempt 2 | Expected fail, exit 1 | 9/10 passed; the `48x16` StatusLine clipped the required Ctrl+Q hint |
| targeted Wave-6 showcase plus preserved functional tests, `1.36.0.375` | US1 Green attempt 3 | Pass | 10/10 passed after narrow StatusLine hint prioritization |
| targeted Wave-6 showcase plus preserved functional tests, `1.36.0.376` | US1 final Green | Pass | 10/10 passed with explicit Description modernization, platform, and app-loop proof boundaries |
| targeted Wave-6 showcase tests, `1.36.0.377` | US2 Red | Expected fail, exit 1 | 4/8 passed; expected failures isolate the missing six-group menu, selected-entry dispatch, canceled-search presentation, and unknown-palette fallback |
| targeted Wave-6 showcase plus preserved functional tests, `1.36.0.378` | US2 Green attempt 1 | Expected fail, exit 1 | 14/15 passed; the hex printable region was clipped from the visible detail panel |
| targeted Wave-6 showcase plus preserved functional tests, `1.36.0.379` | US2 Green attempt 2 | Failed invocation, exit 1 | Compile failed on local `List<string>.Length`; no test result accepted |
| targeted Wave-6 showcase plus preserved functional tests, `1.36.0.380` | US2 Green attempt 3 | Expected fail, exit 1 | 14/15 passed; wrapping moved the long Description proof below the visible region |
| targeted Wave-6 showcase plus preserved functional tests, `1.36.0.381` | US2 final Green | Pass | 15/15 passed after concise Description text preserved all required boundaries |
| targeted Wave-6 showcase tests, `1.36.0.382` | US3 Red | Expected fail, exit 1 | 8/12 passed; four dialog tests fail because the existing Copy command still ignores modal Copy, Rename, Delete, Read-only, validation, and focus payloads |
| targeted showcase, functional, and operation tests, `1.36.0.383` | US3 Green attempt 1 | Failed invocation, exit 1 | Compile stopped on a local null-coalescing common-base type error; no test result accepted |
| targeted showcase, functional, and operation tests, `1.36.0.384` | US3 final Green | Pass | 26/26 passed; real modal decisions and all preserved one-shot, stale, conflict, link, cancel, and unsupported boundaries remain green |
| targeted showcase and functional tests, `1.36.0.385` | US4 Red compile check | Fail | Test-only namespace import for `ConsoleMouseCapabilityState` was missing; no product result accepted |
| targeted showcase and functional tests, `1.36.0.386` | US4 behavioral Red | Expected fail | 20/23 passed; only release-status, outside-target, and Escape/capability/view cancellation behavior remained missing; no fixture was mutated |
| targeted showcase and functional tests, `1.36.0.387` | US4 first Green compile check | Fail | Example-local namespace import for the existing capability enum was missing; no behavioral result accepted |
| targeted showcase and functional tests, `1.36.0.388` | US4 Green fixture refinement | Fail | 22/23 passed; the empty-source click intersected list selection instead of the intended visible detail-region boundary |
| targeted showcase and functional tests, `1.36.0.389` | US4 invalid-source refinement | Fail | 22/23 passed; a fully empty fixture exercised a pre-existing list assumption, so the source-loss case was narrowed to post-snapshot removal |
| targeted showcase and functional tests, `1.36.0.390` | US4 final Green | Pass | 23/23 passed; complete drag, keyboard parity, NoMutation, invalid source/target/button, Escape, capability loss, view removal, shutdown, and `48x16` boundaries are green |
| evidence validator tests, `1.36.0.391` | US5 closure Red | Expected fail | 3/4 passed; only the intentionally `Planned` entry-point closure was rejected, while all malformed synthetic fixtures failed closed |
| evidence validator tests, `1.36.0.392` | US5 exact closure | Pass | 4/4 passed; exact ordered 10/1 cardinality, allowed decisions, complete fields, negative fixtures, and all 24 current TVFM SHA-256 hashes pass |
| Bash and PowerShell `check-homogeneity` wrappers | Agent parity local wrapper check | Failed invocation, not accepted | Both fail closed with exit 2 because baseline `scripts/lib/hg-*` helpers are not tracked; Feature 036 does not alter those wrappers or broaden scope |
| `SPECKIT` block SHA-256 across AGENTS, CLAUDE, GEMINI, and primary Copilot | Maintained guidance parity | Pass | All four hashes are `e2397e24b44568e60fc87d1e2197c9f645d58dbb2964b745311c766c028eb4f4`; Agent-Copilot and Antigravity remain intentionally compact generated surfaces with matching Feature-036 metadata |
| `bash tests/scripts/rename-lastenheft-tests.sh` | Actual homogeneity workflow contract | Pass | 18/18 assertions passed |
| `bash scripts/scan-agent-secrets.sh --fail-on-high .` | Actual homogeneity workflow secret gate | Pass | high=0; one pre-existing local `.claude` permissions/config classification remains medium |
| `specify check` | Post-integration toolchain check | Pass | Exit 0; Antigravity active, Gemini CLI absent as documented |
| `git diff --check`, scope, placeholder, generated-path, protected-root, dependency/project, and secret inventory | Exact candidate static scope | Pass | No whitespace, generated, protected, package, project, high-secret, or unresolved-placeholder finding; two checklist lines only assert that markers are absent |
| `dotnet format TuiVision.sln --verify-no-changes --no-restore` | Exact candidate format | Pass | Exit 0 |
| first normal PTY `--no-build` attempt | Runtime candidate check | NeedsRevalidation | Exit 0 but the existing `Tp7FileManager` output predated Feature 036 and exposed only three menus; result not accepted, bounded example rebuild required |
| targeted Wave-6 showcase, functional, workspace, and operation tests, `1.36.0.393` | Complete targeted candidate | Pass | 42/42 passed |
| `dotnet build examples/Tp7FileManager/Tp7FileManager.csproj --configuration Release --no-restore`, `1.36.0.394` | Refresh entry-point output for runtime checks | Pass | 0 warnings, 0 errors |
| normal `expect` PTY with Down, F1, and Ctrl+Q against current `--no-build` output | Interactive runtime acceptance | Pass | Exit 0; six menus, selected path, Description, and controlled quit were visible |
| `dotnet run --no-build --configuration Release --project examples/Tp7FileManager -- --smoke` | Deterministic runtime acceptance | Pass | Exit 0; navigation and bounded text preview ran through the real app loop |
| `dotnet test TuiVision.sln --configuration Release --no-restore`, `1.36.0.395` | Full Release solution | Pass | 879/879 passed: Core 52, Compatibility 18, Serialization 48, Controls 373, Drivers 151, Examples 237 |
| `xmllint --noout coverlet.runsettings` | Coverage configuration integrity | Pass | Exit 0 |
| canonical Coverlet collection, `1.36.0.396` | Repository coverage gate | Pass | Required package line rates: Core 92.96%, Controls 86.66%, Serialization 90.01%, Compatibility 80.55%, Drivers.Console 89.18%; all exceed 70%. The example project has no collector, while all five required reports were generated |
| `docfx docfx.json` | Guide and status documentation | Pass | Build succeeded with 0 warnings and 0 errors; generated `_site/` and `api/` output remains ignored |
| `npm test` in `tests/web-a11y` | Playwright/Axe documentation acceptance | Pass | 2/2 passed; landing semantics and representative pages have no serious Axe violation |
| `dotnet list TuiVision.sln package --vulnerable --include-transitive` | Local supply-chain review | Pass | No vulnerable package reported in any of 50 projects |
| `dotnet list TuiVision.sln package --deprecated --include-transitive` | Local dependency status review | Pass with existing observation | Exit 0; MSTest 4.0.1 is classified `Legacy` in the six existing test projects. Feature 036 changes no package or project and does not remediate this baseline observation |
| temporary CycloneDX 1.7 generation and `jq` validation | Local SBOM workflow parity | Pass | 50 projects, 54 packages, valid temporary BOM with non-empty components; no generated BOM is tracked |
| changed-file MIME, Markdown, keyboard, text-first, and constrained-layout review | UTF-8 and learner-facing A11Y | Pass | Changed text is UTF-8 or ASCII; guide is German-first/English-second, semantically structured, keyboard complete, text-first, and documents fallback boundaries |
| agent context block hashes and generated context review | Agent parity | Pass | Four maintained full `SPECKIT` blocks share SHA-256 `e2397e24b44568e60fc87d1e2197c9f645d58dbb2964b745311c766c028eb4f4`; compact Copilot-agent and Antigravity surfaces contain matching Feature-036 identity and scope |
| Bash and PowerShell autonomous state validators | Local state schema parity | Pass | Both exit 0 against run `4f84dab9-293f-45f9-b491-a5fe4693270b`, stage `Validate`, status `Active`; final hashes are revalidated at T168 |
| exact evidence and scope inventory | 1/10/24 closure and scope firewall | Pass | Exactly 10 ordered `W6S` rows, one entry decision, and 24 TVFM hashes; no `CandidateFinding`, `ProductDecision`, framework follow-up, protected-root, dependency/project, Feature-037, or portfolio-audit path |
| Windows CI full test on `c34a65848dd1e06af7b998d43b92945aec6fb469` | First provider acceptance | Fail, actionable | 236/237 example smokes passed; the showcase source-hash validator compared a CRLF checkout of `TVFM/ASSOC.PAS` with the accepted LF content hash. Product behavior and the other five test assemblies passed |
| corrected Wave-6 showcase, functional, workspace, and operation filter, `1.36.1.397` | Platform-neutral historical-hash correction | Pass | 35/35 passed, including a new proof that `.PAS`/`.BAT` hashes are LF-canonical while `.PAL`/`.TVR` hashes remain byte-exact |
| `dotnet format TuiVision.sln --verify-no-changes --no-restore` after Windows correction | Corrected candidate format | Pass | Exit 0 |
| `dotnet test TuiVision.sln --configuration Release --no-restore`, `1.36.1.398` | Corrected full Release solution | Pass | 880/880 passed: Core 52, Compatibility 18, Serialization 48, Controls 373, Drivers 151, Examples 238 |
| Ubuntu/macOS/Windows | Remote acceptance | Pass | CI run `29606533761`: Ubuntu job `87971004497`, macOS job `87971004520`, Windows job `87971004496`; corrected exact head passed |
| DocFX Pages build | Remote documentation acceptance | Pass | Run `29606533887`, job `87971005330`; PR Pages deployment skipped as expected |
| supply-chain workflow | Remote dependency/SBOM acceptance | Pass | Run `29606534116`, job `87971005518` |
| homogeneity and agent parity | Remote maintained-surface acceptance | Pass | Run `29606533859`; all required platform jobs succeeded |
| Claude review | Remote review acceptance | Pass | Run `29606534614`, job `87971007187` |
| exact-head gate evidence | Before merge | Pass | Both validators accepted 12/12 primary requirements for `a0d5062` |
| reviewer/thread convergence | Before merge | Pass with missing Copilot review | Claude passed; Copilot quota exhausted; zero issue comments, PR review comments, or GraphQL threads |

## PR and Merge Evidence

- Initial candidate commit:
  `c34a65848dd1e06af7b998d43b92945aec6fb469`.
- Feature PR: [#104](https://github.com/hindermath/TuiVision/pull/104).
- First provider pass: 21 successful, one skipped, one actionable Windows
  failure. Ubuntu, macOS, DocFX, supply chain, secret, homogeneity,
  PowerShell, and Claude checks passed. Copilot was unavailable because the
  requesting account had reached its quota and is not counted as a review.
- Windows failure boundary: historical text line-ending conversion in the
  test-only source-hash validator. The correction canonicalizes only
  `.PAS`/`.BAT` text bytes and preserves exact binary hashing.
- Corrected feature head:
  `a0d506297c101104fd0e15911a7d21e1c5a21caa`.
- Final PR result: 22 successful checks and one expected skipped Pages
  deployment. The final Ubuntu, macOS, Windows, DocFX, supply-chain,
  homogeneity, secret, PowerShell and Claude gates passed.
- Temporary provider evidence covered 12/12 primary requirements on the exact
  corrected head. Bash and PowerShell gate validators both passed.
- Review convergence: zero issue comments, zero PR review comments and zero
  GraphQL review threads. Copilot quota exhaustion remained a missing review,
  not Pass.
- Admin bypass: used only after every technical and exact-head gate was green,
  no actionable thread remained and Human Approval was the sole open rule.
- Merge: PR #104 was merged with merge commit
  `559bffbfbb94699a33cfe1ad8b01d5ac9b86641d` at
  `2026-07-17T19:16:21Z`; the remote feature branch was deleted and pruned.
- Post-merge facts are recorded once in
  `specs/036-wave6-tvfm-showcase-remediation/delivery-closeout.md`; the
  closeout does not recursively claim its own delivery identity.
