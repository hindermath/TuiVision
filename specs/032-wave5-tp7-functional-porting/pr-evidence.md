# Autonomous Run Evidence: Wave-5 TP7 Functional Porting

**Branch**: `032-wave5-tp7-functional-porting`
**Feature directory**: `specs/032-wave5-tp7-functional-porting`
**Binding intake**: `Lastenheft_17_Wave5-TP7-Functional-Porting.032-wave5-tp7-functional-porting.md`
**Delivery mode**: `MergeAndSync`
**Authority source**: Current user instruction

## Scope

### Included

- 15 read-only TP7 source roles, six Wave-5 consumers and ten managed examples.
- Real app-loop state/view/cell proof, controlled fixtures, guides and
  showcase-delta evidence.
- Required project, test, documentation, status, governance and delivery
  integration.

### Excluded

- Full showcase remediation, Feature 033, Wave 6, TVFM and post-Wave-6 audit.
- Broad framework/API redesign, packages, services, processes, native bridges,
  arbitrary user files and host configuration mutation.

## Preflight and Foundation Evidence

- Baseline and branch: `032-wave5-tp7-functional-porting` at
  `269c54f5f882c69e21f46f97d3e89a938bfb568f`; `.specify/feature.json`
  references this feature and all dirty paths are feature-owned.
- Predecessors: Feature PR #90 and causal closeout PR #91 are ancestors; Wave-5
  intake PR #92 supplied Lastenheft 17.
- Tooling: `specify check` and Spec-Kit prerequisite discovery passed with
  reviewed error output. The Bash state validator accepts the current run.
  Local `pwsh` is unavailable; the matching validator remains a Windows
  acceptance-gate responsibility.
- Presets: Security 0.6.0 at priority 10, Architecture 0.5.0 at 20, iSAQB
  0.2.0 at 30, A11Y 0.4.0 at 40, Cross-Platform 0.2.0 at 50, Agent Parity
  0.3.0 at 60 and Autonomous Run 0.2.2 at 70.
- Review readiness: seven checklist files contain 97 completed and zero
  incomplete items. Repeated Analyze maps all 73 requirement/success/
  governance keys to 180 tasks.
- Gate requirements: valid UTF-8 JSON, SHA-256
  `2fd078889248640813ed4c2cf135ce839cbcb01c518fedc2ef13ae0dca01470c`.
- Foundation: one compiled Wave-5 assembly plus ten unique executable projects
  and ten normal/`--smoke` CLI paths are present in `TuiVision.sln`; the smoke
  project references the shared assembly exactly once.
- Identity boundary: no shared source is linked into multiple Wave-5
  assemblies. Public XML documentation is required by the existing Release
  build and the new public surface was reviewed before the first test.
- Scope: no `src/`, package, dependency, `TVDEMOS/`, `TVFM/`, `tv203s/` or
  external-source file changed during foundation. `.gitignore` already covers
  .NET, Node, DocFX and test output.
- External context: `github/spec-kit#3569` is a non-blocking v0.2.2 catalog
  submission; no v0.2.3 release is planned.
- Stage-2 derivation: all ten non-empty delta rows are materialized as
  `Lastenheft_18_Wave5-TP7-Showcase-Remediation.md`. No `specs/033-*` path,
  Feature-033 branch or Wave-6 implementation was created.
- Agent parity: the `032-wave5-tp7-functional-porting` section is byte-equal
  across `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
  `.github/copilot-instructions.md` and
  `.github/agents/copilot-instructions.md`; all five sections have SHA-256
  `c2afb802672898c366e11923c20a91f36f1527b3b83fa4304dd8559d187e805b`.
  The legacy `scripts/check-homogeneity.sh` exits 2 before scanning because
  repository-local `scripts/lib/hg-*.sh` helpers are absent. Current CI does
  not invoke that scanner; it runs the agent secret and Lastenheft rename
  contracts. The missing helper is recorded as a pre-existing tooling limit,
  not represented as a passed scan.

## Run Gates

| Phase | Attempt | Result | Evidence | Remaining action |
|---|---:|---|---|---|
| Preflight | 1 | Pass | clean synchronized `269c54f`, exact branch, `specify check`, seven presets | None |
| Specify | 1 | Pass | `spec.md`, requirements checklist 25/25 | None |
| Clarify | 2 | Pass | two sessions in `spec.md`; no material ambiguity | None |
| Checklists | 1 | Pass | five files, 73/73 items | None |
| Plan | 1 | Pass | `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, contract | Generate tasks |
| Tasks | 1 | Pass | `tasks.md`, 180 dependency-ordered tasks, no unsafe parallel writers | Run Analyze |
| Analyze | 2 | Pass | 73/73 requirement and success/governance keys mapped; 180/180 tasks valid; first pass medium entry-point, Demo/Editor/Mouse gaps remediated; repeated pass has no Critical, High or Medium finding | Implement |
| Implement | 1 | Open | task and diff evidence | Execute after Analyze |
| Validate | 1 | Open | commands below | Run triggered gates |
| Deliver | 1 | Open | PR and exact-head evidence | MergeAndSync |

Allowed results are `Pass`, `Fail`, `Accepted`, `Deferred`, and `Open`.

## Historical Source Matrix

| Source | Role | Modern target | Intentional modernization | Proof |
|---|---|---|---|---|
| `TVDEMOS/TVDEMO.PAS` | EntryPoint | `Tp7Demo` | Modern typed commands and bounded gadget state | `Tp7ApplicationSmokeTests` |
| `TVDEMOS/DEMOCMDS.PAS` | SupportUnit | shared command IDs | No Pascal global constants | exactly-once command proof |
| `TVDEMOS/DEMOSTRS.PAS` | FixtureOrContent | embedded UTF-8 strings | No legacy codepage resource | rendered Demo cells |
| `TVDEMOS/GADGETS.PAS` | SupportUnit | `Tp7Demo` gadget state | No host heap semantics | two bounded idle cycles |
| `TVDEMOS/TVEDIT.PAS` | EntryPoint | `Tp7Edit` | Existing safe editor/file contracts | modify, safe-close, conflict and traversal smokes |
| `TVDEMOS/TVHC.PAS` | EntryPoint | `Tp7Help` | Existing bounded `.topic` compiler | valid/invalid compiler smokes |
| `TVDEMOS/HELPFILE.PAS` | SupportUnit | Help model | No proprietary binary parity | real `THelpWindow` known/fallback proof |
| `TVDEMOS/DEMOHELP.PAS` | FixtureOrContent | embedded topics | Typed controlled contexts | contexts 101 and 999 |
| `TVDEMOS/TVRDEMO.PAS` | EntryPoint | `Tp7ResourceDemo` | Existing closed resource registry and atomic exact-key publication | `Tp7ResourceSmokeTests` |
| `TVDEMOS/GENRDEMO.PAS` | GeneratorIntent | `Tp7ResourceGenerator` | Allowlist and controlled test-owned target only | generator/load roundtrip and traversal rejection |
| `TVDEMOS/ASCIITAB.PAS` | EntryPoint | `Tp7AsciiTable` | Deterministic Unicode/control labels over bounded byte values | `Tp7DomainSmokeTests` |
| `TVDEMOS/CALC.PAS` | EntryPoint | `Tp7Calculator` | Invariant decimal state; no Pascal floating-point/global-state copy | `Tp7CalculatorSmokeTests` 3/3 |
| `TVDEMOS/CALENDAR.PAS` | EntryPoint | `Tp7Calendar` | Fixed month fixture independent of host date/locale | `Tp7DomainSmokeTests` |
| `TVDEMOS/PUZZLE.PAS` | EntryPoint | `Tp7Puzzle` | Fixed board, adjacent moves and atomic rejection | `Tp7DomainSmokeTests` |
| `TVDEMOS/MOUSEDLG.PAS` | EntryPoint | `Tp7MouseDialog` | Local state and honest capability, no host mutation | `Tp7DomainSmokeTests` |

## Consumer Decisions

| Consumer | Decision | Framework contracts | Real-path proof | Residual risk | Follow-up |
|---|---|---|---|---|---|
| W5-001 | UseExistingFramework | Application/Desktop/Menu/Status/Dialog/Help/Idle/Commands | two idle cycles, one command, Help and rendered windows pass | None | Stage-2 showcase delta |
| W5-002 | UseExistingFramework | `TFileEditor`, `TEditWindow` | modified, safe-close, conflict and controlled-root tests pass | None | Stage-2 showcase delta |
| W5-003 | UseExistingFramework | `TResourceFile`, registered records | exact `Dialog`, `Menu`, `Status` roundtrip and malformed matrix pass | None | Stage-2 showcase delta |
| W5-004 | UseExistingFramework | closed registry and controlled stream/path | allowlisted bytes, controlled-root write and traversal rejection pass | None | Stage-2 showcase delta |
| W5-005 | UseExistingFramework | existing views, commands and app loop | Calculator 3/3 plus ASCII/Calendar/Puzzle 3/3 pass with deterministic fixtures | Local deterministic example domain code | Stage-2 visual delta |
| W5-006 | UseExistingFramework | existing mouse events/capability/focus | supported, Unsupported, capability-loss and keyboard-parity 3/3 pass | Host-specific support may be Unsupported | Stage-2 controls and layout |

## Primary Proof Matrix

| Example | State proof | View proof | Cell proof | App-loop test | Result |
|---|---|---|---|---|---|
| Tp7Demo | idle=2, command=1 | `TWindow` plus real menu/status | command/help cells | `Tp7ApplicationSmokeTests` | Pass |
| Tp7Edit | modified, close rejected, conflict/traversal decisions | `TEditWindow` | edited `X` and status cells | `Tp7ApplicationSmokeTests` | Pass |
| Tp7Help | invalid no-partial, contexts 101/999 | `THelpWindow` | Welcome and fallback cells | `Tp7ApplicationSmokeTests` | Pass |
| Tp7ResourceDemo | exact records loaded; malformed input has no partial model | `TWindow` | dialog/menu/status and rejection cells | `Tp7ResourceSmokeTests` | Pass |
| Tp7ResourceGenerator | bytes/path accepted or traversal rejected | `TWindow` | generated file and rejection status | `Tp7ResourceSmokeTests` | Pass |
| Tp7AsciiTable | code `255`, hex `FF`, invalid `256` preserved | `TWindow` | decimal/hex/control-label cells | `Tp7DomainSmokeTests` | Pass |
| Tp7Calculator | value `15`; division keeps `8` | `TWindow` | visible `15`, `8`, rejection status | `Tp7CalculatorSmokeTests` | Pass |
| Tp7Calendar | fixed `2026-12` to `2027-01` rollover | `TWindow` | invariant month cells | `Tp7DomainSmokeTests` | Pass |
| Tp7Puzzle | one accepted move; invalid move preserves board | `TWindow` | `13 14 15 __` cells | `Tp7DomainSmokeTests` | Pass |
| Tp7MouseDialog | local settings, capability and activation; no host mutation | `TWindow` | activation/capability cells | `Tp7DomainSmokeTests` | Pass |

## Showcase Delta Matrix

| Example | Disposition | Delivered function | Visual delta | Interaction delta | Layout delta | A11Y delta | Priority | Evidence |
|---|---|---|---|---|---|---|---|---|
| Tp7Demo | Stage2Required | Menu, commands, windows, Help and bounded gadgets | Add complete historical demo window set and arrangement | Add tile/cascade/next/close shortcuts | Prove constrained multi-window layout | Add full focus/shortcut text and Description | P1 | app smokes, guide |
| Tp7Edit | Stage2Required | Real editor, safe close, conflict and controlled save | Add complete editor chrome and file dialogs | Add full edit/search/file menu paths | Prove constrained editor/dialog layouts | Add focus text, shortcut inventory and Description | P1 | app smokes, guide |
| Tp7Help | Stage2Required | Compiler, known topic and fallback | Add visible compiler diagnostics plus viewer composition | Add cross-reference/back navigation | Prove constrained topic/diagnostic layouts | Add complete Help shortcut/focus text and Description | P1 | app smokes, guide |
| Tp7ResourceDemo | Stage2Required | Atomic exact-key reconstruction and malformed-input rejection | Add visible reconstructed dialog, menu and status composition | Add selectable resources and rejection dialog | Prove constrained resource layouts | Add shortcut/focus text and Description | P1 | `Tp7ResourceSmokeTests`, guide |
| Tp7ResourceGenerator | Stage2Required | Allowlisted deterministic generation and controlled-root rejection | Add visible generator controls and progress/error state | Add controlled target selection and generation command | Prove constrained generator/dialog layouts | Add keyboard labels, focus order and Description | P1 | `Tp7ResourceSmokeTests`, guide |
| Tp7AsciiTable | Stage2Required | Bounded navigation/direct selection and decimal/hex/control labels | Add visible 16x16 table and selection highlight | Add arrows, paging and direct keyboard selection | Prove constrained table layout | Add focused-cell text and Description | P2 | `Tp7DomainSmokeTests`, guide |
| Tp7Calculator | Stage2Required | Invariant arithmetic, clear/backspace/sign and atomic division rejection | Add visible calculator display and key grid | Add direct keyboard shortcuts and Help -> Description | Prove 40x12 control composition | Add explicit widget text/focus order | P1 | `Tp7CalculatorSmokeTests`, guide |
| Tp7Calendar | Stage2Required | Fixed deterministic month navigation and year rollover | Add visible month/day grid | Add day/month keyboard navigation | Prove constrained calendar layout | Add selected-date text and Description | P2 | `Tp7DomainSmokeTests`, guide |
| Tp7Puzzle | Stage2Required | Fixed board, legal move and invalid preservation | Add selectable tile grid | Add arrow/direct tile keyboard paths | Prove 4x4 constrained board | Add blank/tile focus text and Description | P2 | `Tp7DomainSmokeTests`, guide |
| Tp7MouseDialog | Stage2Required | Local settings, supported/Unsupported activation, loss cancellation and keyboard parity | Add visible settings controls and activation target | Add focused controls and complete shortcuts | Prove constrained dialog layout | Add capability/fallback descriptions and focus order | P1 | `Tp7DomainSmokeTests`, guide |

## Governance Applicability

| Preset | Version | Checkpoint | Applicability | Rationale | Evidence path | Owner | Reviewer | Result | Residual risk | Follow-up | Re-evaluation trigger |
|---|---|---|---|---|---|---|---|---|---|---|---|
| security-governance | 0.6.0 | NIST SSDF, CWE, parser/file boundaries, supply chain | Applicable | Executable examples and controlled data boundaries change | `plan.md`, tests, this file | Thorsten | Codex | Open | Input/evidence defects | Complete validation | Scope change |
| security-governance | 0.6.0 | ASVS, AI-SBOM, NIS2, CRA, EU AI Act, DORA | N/A | No web/runtime AI/regulated service trigger | this file | Thorsten | Codex | Accepted | Trigger drift | Re-evaluate | Any web, AI or regulated role |
| architecture-governance | 0.5.0 | STRIDE/CIA/CAPEC | Applicable | Paths, parsers, commands, capability and evidence are trust boundaries | `plan.md` | Thorsten | Codex | Open | Incorrect boundary claim | Review tests | Boundary change |
| architecture-governance | 0.5.0 | Zero Trust, BSI C3A, BSI C5 | N/A | No identity, network, cloud or provider scope | this file | Thorsten | Codex | Accepted | Trigger drift | Re-evaluate | Cloud/service change |
| isaqb-architecture-governance | 0.2.0 | Quality, runtime and building-block views | Applicable | Shared example assembly and ten app composition need traceability | `plan.md`, `data-model.md` | Thorsten | Codex | Pass | None | None | Architecture change |
| a11y-governance | 0.4.0 | Keyboard, focus, text-first, guides, comments | Applicable | Ten learner-facing TUI examples and guides | tests, guides, DocFX/Axe | Thorsten | Codex | Open | Missing showcase polish | Stage-2 delta | Visible scope change |
| cross-platform-governance | 0.2.0 | Linux/macOS/Windows runtime | Applicable | Ten runnable examples | CI evidence | Thorsten | Codex | Open | Platform variance | Remote gates | Project change |
| cross-platform-governance | 0.2.0 | Script parity | N/A | No script is planned | gate requirements | Thorsten | Codex | Accepted | Trigger drift | Re-evaluate | Script change |
| agent-parity-governance | 0.3.0 | Five maintained surfaces | Applicable | Active feature and next intake markers change | five agent files | Thorsten | Codex | Open | Stale fifth surface | Homogeneity check | Shared guidance change |
| autonomous-run-governance | 0.2.2 | State, authority, exact head, review, closeout | Applicable | MergeAndSync autonomous delivery | run state, gates, this file | Thorsten | Codex | Open | Provider timing | Validate exact head | Delivery mode change |

## Validation

| Command or review | Trigger | Result | Evidence or failure boundary |
|---|---|---|---|
| `specify check` | Preflight | Pass | Exit 0; tool availability reviewed |
| Bash state validator | Logical checkpoints | Pass | Checklists checkpoint accepted |
| `git diff --check` | Every candidate | Pass | `git diff --check HEAD`, exit 0 |
| `git diff --cached --check` plus candidate inventory | Before commit | Open | Final staged candidate |
| `dotnet format TuiVision.sln --verify-no-changes` | C# changes | Pass | Exit 0 at version `1.32.0.322` |
| targeted Tp7 Release tests | Ten apps | Pass | 21/21 at version `1.32.0.322` |
| full Release tests | Shared/project changes | Pass | 803/803 at version `1.32.0.323` |
| canonical coverage | Merge policy | Pass | Core 92.96%, Controls 86.66%, Serialization 90.01%, Compatibility 80.55%, Drivers.Console 89.18% |
| `docfx docfx.json` | Guides/navigation | Pass | explicit tool path, 0 warnings and 0 errors |
| Playwright/Axe | DocFX change | Pass | 2/2 |
| secret/supply-chain/scope/parity | Delivery | Pass locally | high secrets 0; no protected/source/package drift; five agent sections byte-equal |

### Static Candidate Review

- `git diff --check HEAD`: exit 0.
- Placeholder scan over new Feature-032 artifacts, code, tests, guides and
  Lastenheft 18, excluding the completed checklist assertion that names the
  forbidden markers: no unresolved marker.
- Protected roots: no tracked or untracked path under `src/`, `tv203s/`,
  `TVDEMOS/` or `TVFM/`.
- Dependency and package review: no package or dependency version changed.
  New projects reference only the five existing TuiVision assemblies and the
  smoke project references the shared Wave-5 assembly once.
- `bash scripts/scan-agent-secrets.sh --fail-on-high .`: exit 0, high 0,
  pre-existing local `.claude` configuration classified medium.
- `specify check`: exit 0.
- `/Users/thorstenhindermann/.dotnet/dotnet format TuiVision.sln --verify-no-changes`:
  exit 0.
- Exact staged candidate after Markdown whitespace remediation: 77 paths,
  0 unstaged paths and 0 untracked paths; no protected or generated path.
  `git diff --cached --check` exits 0. The staged diff is
  `+5618/-86` lines before later delivery bookkeeping.

### Complete Targeted Wave-5 Run

- Version: `1.32.0.322`.
- Command:
  `/Users/thorstenhindermann/.dotnet/dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~Tp7|FullyQualifiedName~Wave5Functional"`.
- Result: exit 0, 21/21 passed. This includes three Calculator, six central
  app, three Resource, six domain/mouse and three exact-matrix tests.

### Full Release Run

- Version: `1.32.0.323`.
- Command:
  `/Users/thorstenhindermann/.dotnet/dotnet test TuiVision.sln --configuration Release`.
- Result: exit 0, 803/803 passed: Core 52, Controls 373, Serialization 48,
  Compatibility 18, Drivers 151 and Example Smokes 161.
- `xmllint --noout coverlet.runsettings`: exit 0.

### Canonical Coverage Gate

- Version: `1.32.0.324`.
- Command:
  `/Users/thorstenhindermann/.dotnet/dotnet test TuiVision.sln --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings`.
- Result: exit 0, 803/803 tests passed and five canonical Cobertura reports
  were produced.
- Package line coverage: `TuiVision.Core` 92.96%,
  `TuiVision.Controls` 86.66%, `TuiVision.Serialization` 90.01%,
  `TuiVision.Compatibility` 80.55%, and
  `TuiVision.Drivers.Console` 89.18%; all exceed 70%.
- The Example-Smoke project emitted `Data collector ... not found` because it
  does not carry the collector and is not a gate assembly. No coverage claim
  is made for that project; the five required test projects each emitted a
  valid report.

### Documentation and Entry-Point Gates

- Initial `docfx docfx.json`: exit 127 because `docfx` was absent from the
  app shell `PATH`.
- `PATH=/Users/thorstenhindermann/.dotnet:$PATH /Users/thorstenhindermann/.dotnet/tools/docfx docfx.json`:
  exit 0, 0 warnings and 0 errors.
- `npm run test:docfx` under `tests/web-a11y`: exit 0; DocFX again reported
  0 warnings/errors and Playwright/Axe passed 2/2.
- All changed text files decode as UTF-8. `lynx` is unavailable locally;
  semantic Markdown plus generated-page Playwright/Axe is the local text-first
  proof, with remote documentation gates still required.
- First `dotnet run --no-build` attempt for `Tp7Demo` failed before process
  start because the full test invocation had not emitted executable apphosts.
  A separate Release build is required before the ten no-build entry-point
  checks; no runtime result was claimed from this attempt.
- Release build version `1.32.0.325`:
  `/Users/thorstenhindermann/.dotnet/dotnet build TuiVision.sln --configuration Release`,
  exit 0 with 0 warnings and 0 errors.
- Ten commands of the form
  `/Users/thorstenhindermann/.dotnet/dotnet run --no-build --configuration Release --project examples/Tp7<Name> -- --smoke`
  passed for Demo, Edit, Help, ResourceDemo, ResourceGenerator, AsciiTable,
  Calculator, Calendar, Puzzle and MouseDialog.
- Local supply-chain scope passed: no package, SDK, lockfile or workflow
  drift, and no new Wave-5 project contains a `PackageReference`.

### Test-first Reference Slice

- Red version: `1.32.0.311`.
- Red command:
  `/Users/thorstenhindermann/.dotnet/dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~Tp7CalculatorSmokeTests"`.
- Red result: exit 1, 1 passed and 2 failed. Both failures were the intended
  missing command behavior: expected `15`, observed `0`; expected preserved
  `8`, observed `0`. The complete foundation compiled and no unexpected
  exception or unrelated test failure occurred.
- Green candidate: command processing now updates invariant calculator state,
  visible window and status; division by zero preserves the last valid left
  operand and publishes a text-first rejection.
- Green version/result: `1.32.0.312`, exit 0, 3/3 passed. State `15` and
  preserved `8`, `TWindow` identity, constrained first frame and rendered
  result/rejection cells all passed.

### Central App Red Matrix

- Red version: `1.32.0.313`.
- Red command:
  `/Users/thorstenhindermann/.dotnet/dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~Tp7ApplicationSmokeTests"`.
- Red result: exit 1, 0/6 passed. Expected missing effects covered Demo
  command/help/idle, Editor close/conflict/traversal and Help invalid/known/
  unknown context behavior. The test and public compile surfaces succeeded.
- First Green attempt at `1.32.0.314`: 4/6 passed. Remaining assertions were
  corrected to respect intentional shell-reference cleanup after `Run()` and
  the established fallback text `No help topic is available`; no runtime
  implementation change was needed for those two failures.
- Final Green version/result: `1.32.0.315`, exit 0, 6/6 passed.

### Resource Red Matrix

- Red version: `1.32.0.316`.
- Red command:
  `/Users/thorstenhindermann/.dotnet/dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~Tp7ResourceSmokeTests"`.
- Red result: exit 1, 0/3 passed. Generator output, traversal rejection and
  malformed-load proof were all absent as expected. The malformed setup could
  not yet mutate bytes because the missing generator returned no payload; this
  is the same intended Red boundary.
- First Green attempt at `1.32.0.317`: 2/3 passed. The only failure was in the
  test-owned unknown-type fixture, which searched for a shortened type ID
  instead of the registered `tuivision.dialog-description.v1` identifier.
  Generator, controlled-root, exact-key load and the other rejection paths
  passed; no product implementation change was required.
- Final Green version/result: `1.32.0.318`, exit 0, 3/3 passed. The generator,
  exact-key load, traversal boundary and duplicate/unknown/negative-length
  rejection matrix all passed through real application loops.

### Domain and Mouse Red Matrix

- Red version: `1.32.0.319`.
- Red command:
  `/Users/thorstenhindermann/.dotnet/dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~Tp7DomainSmokeTests"`.
- Red result: exit 1, 0/6 passed. The expected missing behavior covered ASCII
  navigation/boundary preservation, fixed Calendar rollover, accepted/rejected
  Puzzle moves, supported double-click/local settings, Unsupported keyboard
  fallback and mid-interaction capability loss. The stable public compile
  surface and all unrelated projects compiled successfully.
- First Green attempt at `1.32.0.320`: compilation stopped before test
  execution because the new capability-loss handler referenced
  `ShellCommandIds` without importing `TuiVision.Controls`. This was an
  implementation-local compile-surface omission; no behavioral result was
  claimed.
- Final Green version/result: `1.32.0.321`, exit 0, 6/6 passed. ASCII,
  Calendar, Puzzle and all three Mouse capability/fallback paths passed through
  real application loops with state, view and cell assertions.

## Remote Delivery

| Item | Result | Evidence |
|---|---|---|
| Push | Open | `032-wave5-tp7-functional-porting` |
| Pull request | Open | URL after publication |
| Required checks | Open | PR-context exact head |
| Acceptance-gate mapping | Open | temporary provider evidence |
| Review threads | Open | GraphQL query |
| Unavailable reviews | Open | Provider/quota evidence |
| Reviewed head | Open | Exact commit hash |
| Merge | Open | Merge commit |
| Local `main` sync | Open | `HEAD == origin/main`, clean tree |
| Causal closeout | Open | `specs/032-wave5-tp7-functional-porting/delivery-closeout.md` only if required |
| Duplicate workflow events | Open | PR-context gates are primary |

## Retrospective

- **Effective**: Pending.
- **Waste**: Pending.
- **Recurring blocker**: Pending.
- **Recommended refinement**: Pending.
