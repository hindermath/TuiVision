# Autonomous Run Evidence: Wave-5 TP7 Showcase Remediation

**Branch**: `033-wave5-tp7-showcase-remediation`
**Feature directory**: `specs/033-wave5-tp7-showcase-remediation`
**Binding intake**: `Lastenheft_18_Wave5-TP7-Showcase-Remediation.033-wave5-tp7-showcase-remediation.md`
**Delivery mode**: `MergeAndSync`
**Authority source**: Current user instruction to implement the approved plan

## Scope

### Included

- Ten evidence-derived TP7 showcase compositions and their real app-loop,
  keyboard, focus, status, description, constrained-layout, and cell proofs.
- Guides, feature evidence, deterministic evidence validation, archived intake,
  project statistics, and synchronized next-step markers.

### Excluded

- Functional re-porting, broad framework revision, new dependencies, arbitrary
  user files, host mutation, historical-source edits, Wave 6, Feature 034, and
  the post-Wave-6 portfolio audit.

## Preflight and Ownership

| Check | Result | Evidence |
|---|---|---|
| Branch and feature metadata | Pass | `033-wave5-tp7-showcase-remediation`; `.specify/feature.json` names this feature |
| Baseline ancestry | Pass | `5df2ec3cef5dd0e534abe630a8d472e0e5bc0236` equals pre-branch `origin/main` |
| Feature-032 delivery | Pass | PR #93 merged as `e74c33d`; closeout PR #94 merged as `355f8ec` |
| Intake prompt standardization | Pass | PR #95 merged as `5df2ec3` |
| Working-tree ownership | Pass | Initial writes are Feature-033 metadata, artifacts, and synchronized active-feature guidance |
| `specify check` | Pass | Exit 0; tool inventory completed |
| Prerequisites | Pass | Feature directory, spec, plan, tasks, research, model, contract, and quickstart resolved |
| Checklists | Pass | 51/51 checkbox items plus 14/14 plan-review rows; zero incomplete |
| Presets | Pass | 0.6.0/0.5.0/0.2.0/0.4.0/0.2.0/0.3.0/0.2.2 at priorities 10-70 |
| State validators | Pass | Bash and PowerShell accept run `401d4b96-8db6-4bae-a018-260036838892` |
| Gate JSON | Pass | UTF-8 JSON, schema 1.0, twelve declared gates |
| Ignore boundaries | Pass | `_site/`, generated `api/`, TestResults, `obj/`, and `node_modules/` are ignored |
| External preset context | Non-blocking | `github/spec-kit#3569` does not gate local Feature-033 delivery |

## Cardinality and Serialization

- Exactly ten examples, ten framework decisions, ten primary showcase proofs,
  ten normal entry points, ten shortcut inventories, and ten constrained
  layouts are required.
- Shared single writers are the Wave-5 source files, the smoke-test project,
  `pr-evidence.md`, `tasks.md`, run state, gate requirements,
  `Directory.Build.props`, statistics, status/order files, and agent guidance.
- Before every explicit `dotnet build` or `dotnet test`, all three version
  fields use `1.33.<patch>.<build>` and the manual build component increments
  exactly once.
- No intentional interruption is scheduled. A real interruption requires
  read-only status followed by explicit authority-revalidated resume.
- `TVDEMOS/`, `TVFM/`, `tv203s/`, external comparison sources, generated
  output, credentials, caches, logs, and test results remain read-only or
  untracked.
- The optional non-recursive closeout path is
  `specs/033-wave5-tp7-showcase-remediation/delivery-closeout.md`; it is
  created only when terminal facts cannot truthfully exist on the feature
  head.

## Run Gates

| Phase | Attempt | Result | Evidence | Remaining action |
|---|---:|---|---|---|
| Preflight | 1 | Pass | clean `main` at `5df2ec3`; Feature 032 `Completed`; seven presets verified | Specify |
| Specify | 1 | Pass | `spec.md`, `checklists/requirements.md` | Clarify convergence |
| Clarify | 1 | Pass | `spec.md` clarification session; no material question | Checklists |
| Checklists | 1 | Pass | requirements, showcase, security/A11Y, historical scope | Plan |
| Plan | 1 | Pass | `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, contract | Plan review |
| Plan review | 1 | Pass | `checklists/plan-quality.md`, `checklists/plan-review.md` | Tasks |
| Tasks | 1 | Pass | `tasks.md`, `autonomous-gate-requirements.json` | Analyze convergence |
| Analyze | 1 | Remediated | 66/66 requirements covered; task paths and causal closeout order corrected | Repeat Analyze |
| Analyze | 2 | Pass | 196 unique tasks; zero Critical/High/Medium, Constitution conflict, pathless task, or unmapped requirement | Implement |
| Implement | 1 | Open | code, tests, guides, evidence | Execute accepted tasks |
| Validate | 1 | Open | commands below | Run declared gates |
| Deliver | 1 | Open | PR and reviewed-head evidence | Merge and synchronize |

### Calculator Reference Slice

| Invocation | Version | Result | Accepted boundary |
|---|---|---|---|
| `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter FullyQualifiedName~Tp7CalculatorSmokeTests` | `1.33.0.327` | Expected Red, exit 1 | Compile reached the new tests; only missing `CalculatorButtonCount`, `FocusedControlKind`, `DescriptionOpened`, and `LastDescriptionText` blocked compilation. Existing framework and calculator domain assemblies built successfully. |
| Same Calculator-only command | `1.33.0.328` | Corrective Green attempt, 4/6 pass | Real dialog/buttons rendered. Two proof assumptions needed correction: the legacy test expected `TWindow`, and focus was read after controlled shutdown had dismantled the dialog. No domain or framework failure. |
| Same Calculator-only command | `1.33.0.329` | Corrective Green attempt, 5/6 pass | Focus and dialog proof pass. The remaining assertion read `StatusLine` after controlled shutdown; evidence must retain whether a real line was constructed rather than inspect a dismantled owner tree. |
| Same Calculator-only command | `1.33.0.330` | Pass, 6/6 | Real `TDialog`, 20 `TButton` controls, captured initial button focus, real status construction, keyboard arithmetic, F1 Description, atomic division rejection, and `40x12` mandatory cells all pass. |
| `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter FullyQualifiedName~Tp7ApplicationSmokeTests` | `1.33.0.331` | Expected Red, exit 1 | Only missing Demo desktop commands/proof state, Editor focus proof, and Help cross-reference/Back proof surfaces blocked compilation. Existing shared, framework, editor, and Help assemblies built. |
| Same central-app command | `1.33.0.332` | Corrective Green attempt, exit 1 before tests | One local compile error referenced a nonexistent `TFileEditor.Text` proof convenience property. The redundant assignment was removed; editor rendering remains the evidence surface. |
| Same central-app command | `1.33.0.333` | Corrective Green attempt, 9/10 pass | Only the active-shortcut count differed: `GetAccessibleShortcuts()` correctly omits initially disabled Save/Replace entries, leaving five executable shortcuts. The assertion now follows the existing command-context contract. |
| Same central-app command | `1.33.0.334` | Pass, 10/10 | Demo real window family and Desktop operations, Editor menu/focus/file boundaries, Help diagnostics/viewer/reference/Back/fallback, three F1 Descriptions, and `48x16` proofs pass. |
| `dotnet test ... --filter 'FullyQualifiedName~Tp7CalculatorSmokeTests|FullyQualifiedName~Tp7ApplicationSmokeTests'` | `1.33.0.335` | Pass, 16/16 | Complete Calculator reference plus Demo/Editor/Help checkpoint; no file, Help, framework, package, Wave-6, or Feature-034 scope expansion. |
| `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter FullyQualifiedName~Tp7ResourceSmokeTests` | `1.33.0.336` | Expected Red, exit 1 | Only missing Resource Demo/Generator focus, control-count, and progress proof properties blocked compilation; existing serialization and Resource domain assemblies built. |
| Same Resource command | `1.33.0.337` | Corrective Green attempt, 3/4 pass | Controls, focus, progress, boundaries, and Descriptions pass. One case-sensitive legacy cell assertion expected lowercase `generated`; the result label was aligned without behavior change. |
| Same Resource command | `1.33.0.338` | Pass, 4/4 | Exact/atomic Resource dialog, generator input/button/progress/result, controlled-root rejection, focus, status, two F1 Descriptions, and `48x16` cells pass. |
| `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter FullyQualifiedName~Tp7DomainSmokeTests` | `1.33.0.339` | Test-authoring correction required, exit 1 | The expected missing domain/mouse proof properties were present, but two new test helpers also had local `ushort` and static-call compile errors. No product result was accepted from this invocation. |
| Same domain/mouse command | `1.33.0.340` | Expected Red, exit 1 | Compilation reached all new assertions and failed only on the nine intentionally missing grid/dialog proof properties. Existing domain, capability-loss, and host-mutation contracts compiled unchanged. |
| Same domain/mouse command | `1.33.0.341` | Pass, 14/14 | Three focusable grids, real Mouse settings controls, complete keyboard paths, four F1 Descriptions, deterministic/rejection/capability boundaries, and `52x22`, `42x16`, `38x15`, and `46x16` cells pass. |

The Red result identifies missing showcase composition and proof surfaces, not
a calculator-domain or framework defect. The accepted Green change is limited
to shared Description/status presentation, real Calculator controls, keyboard
mapping to the existing state transitions, and proof-facing state.

## Decisions and Follow-ups

| Area | Decision | Rationale | Evidence | Residual risk | Owner | Follow-up or re-evaluation trigger |
|---|---|---|---|---|---|---|
| Feature scope | UseExistingFramework | Feature 032 proves the accepted functional contracts | `spec.md` | Bounded UI defects may appear | Codex | Re-evaluate per example |
| Wave 6 | FollowUpHardening | Requires separate review of actual Feature-033 delta | `Lastenheft_18_Wave5-TP7-Showcase-Remediation.033-wave5-tp7-showcase-remediation.md` | Not yet eligible | Thorsten | Completed Feature-033 closeout |

## Analyze Convergence

| Metric | Result |
|---|---:|
| Functional requirements | 47/47 mapped |
| Governance requirements | 7/7 mapped |
| Success criteria | 12/12 mapped |
| Tasks | 196 unique, dependency ordered |
| Unmapped requirements | 0 |
| Unmapped tasks | 0 |
| Critical / High / Medium findings after remediation | 0 / 0 / 0 |
| Constitution alignment issues | 0 |
| Remaining placeholders | 0 |

## Historical Intent

Feature 032 names the 15 accepted paths but does not publish per-file
SHA-256 values. Feature 033 therefore compares the immutable Git blobs at the
Feature-032 merge `e74c33d` with the current head. `git diff --quiet
e74c33d..HEAD -- TVDEMOS` returned zero and all 15 blob IDs match exactly.

| Modern area | Historical source and stable blob | Intent retained | Intentional modern deviation |
|---|---|---|---|
| Demo shell | `TVDEMO.PAS` `9aa23c875edb1fd6c219c26cfe7d4b0b2dfcad89` | Application menus, desktop windows, dialogs, commands | Typed C# commands and deterministic gadgets instead of DOS globals |
| Demo commands | `DEMOCMDS.PAS` `daf053e9ef1072347cab9888fa76bd8df44cee25` | Stable command meaning | Feature-local `ushort` constants |
| Demo strings | `DEMOSTRS.PAS` `e1abf0fb9206f9bb053793f960c0411c6ce05c09` | Named learner-facing purpose | Bilingual UTF-8 text instead of Pascal resources |
| Demo gadgets | `GADGETS.PAS` `26314f38406cfbc5ff3bd0a9cea27f28c43b54ea` | Visible clock/heap-style feedback | Deterministic tick text; no host-memory probe |
| Editor | `TVEDIT.PAS` `56279ad1b73dfc9e9fe7b2fd48f169983531d784` | File/Edit/Search/Windows chrome and editor focus | Controlled fixture roots and explicit save/conflict decisions |
| Help compiler | `TVHC.PAS` `a5bbbf1bd40ebf9790636f2b6f8b0dda2195cfd8` | Compile topics and report diagnostics | Closed UTF-8 source grammar |
| Help file | `HELPFILE.PAS` `de7912376481c34666222e31b67a225eaac37db1` | Topic/context/cross-reference model | Managed typed model with atomic rejection |
| Help demo | `DEMOHELP.PAS` `013dd90709574262ff106484787d9ad671792e40` | Selectable topic viewer, cross-reference, Back, fallback | No TP7 binary help dependency |
| Resource demo | `TVRDEMO.PAS` `a5e2bd30328c22c7595db2b0c2c4002ac99e81ba` | Reconstruct named menu/status/dialog resources | Exact allowlisted managed records |
| Resource generator | `GENRDEMO.PAS` `2ee61b1619ccae47da985b28ada778a22e802a23` | Generate the records consumed by the demo | Test-owned root, no overlays or arbitrary object persistence |
| ASCII table | `ASCIITAB.PAS` `2b22640533e9454212633cead224d6d1e5014109` | Focused byte selection by keyboard or pointer | Stable 16x16 text grid instead of historical 32-column display |
| Calculator | `CALC.PAS` `d4d3565deea4ab2b77a2f5467744b6f624e9d51b` | Display plus focusable calculator buttons | Decimal typed state and atomic division rejection |
| Calendar | `CALENDAR.PAS` `3667ae873c6ce3a99699ecf03922d48ecfb0b0ca` | Month grid and keyboard month navigation | Fixed fixture plus explicit day focus |
| Puzzle | `PUZZLE.PAS` `30da629232e72575a8213c4c8e5b3ef718ea920a` | Selectable 4x4 board and adjacent movement | Deterministic starting board instead of random scrambling |
| Mouse dialog | `MOUSEDLG.PAS` `5929db027c8a7de1213ab9f07b626ece9af5f393` | Settings controls and click activation | Local capability-aware state; no host DoubleDelay mutation |

## Foundational Showcase Contract

The stable Description command is `Wave5Application.CmDescription` (`32990`).
Every app-specific Description must cover, in this order, historical purpose,
complete keyboard operation, modern deviation, security/file/parser/capability
boundary, and primary proof boundary. The common shell may provide the
command and presentation host, but each application owns its content.

| Example | Normal viewport | Constrained viewport | Main component | Complete accepted keyboard inventory |
|---|---:|---:|---|---|
| `Tp7Demo` | `80x25` | `48x16` | `TWindow` family on `TDesktop` | `Alt+D`, Open, Tile, Cascade, Next, Close, `F1` Description, `Ctrl+Q` |
| `Tp7Edit` | `80x25` | `48x16` | `TEditWindow` / `TFileEditor` | File/Edit/Search menu mnemonics, text keys, save, close, `F1`, `Ctrl+Q` |
| `Tp7Help` | `80x25` | `48x16` | `THelpWindow` / `THelpViewer` | compile, known/unknown context, reference next/activate, Back, `F1`, `Ctrl+Q` |
| `Tp7ResourceDemo` | `80x25` | `48x16` | reconstructed `TDialog` | load, Tab, Enter/select, rejection close, `F1`, `Ctrl+Q` |
| `Tp7ResourceGenerator` | `80x25` | `48x16` | `TDialog` with target and Generate | Tab, target entry, Alt+G/Enter, rejection close, `F1`, `Ctrl+Q` |
| `Tp7AsciiTable` | `80x25` | `52x22` | focusable 16x16 grid view | arrows, PageUp/PageDown, Home/End, direct select command, `F1`, `Ctrl+Q` |
| `Tp7Calculator` | `80x25` | `40x12` | `TDialog` with display and button grid | digits, decimal, operators, Enter/equal, C, Back, Sign, Tab, `F1`, `Ctrl+Q` |
| `Tp7Calendar` | `80x25` | `42x16` | focusable month/day grid | arrows for day, PageUp/PageDown for month, `F1`, `Ctrl+Q` |
| `Tp7Puzzle` | `80x25` | `38x15` | focusable 4x4 grid view | arrows/tile movement, Enter, `F1`, `Ctrl+Q` |
| `Tp7MouseDialog` | `80x25` | `46x16` | `TDialog` with real settings controls | Tab, Space, arrows, Enter activation, `F1`, `Ctrl+Q`; mouse optional |

Feature-032 domain and boundary tests remain binding. This feature adds
presentation proof without weakening calculator state preservation, editor
and generator roots, Help/Resource atomic rejection, deterministic
calendar/puzzle fixtures, capability honesty, or
`HostMutationPerformed == false`.

No framework project, package, dependency, solution project, launch project,
or second example-local framework layer is planned. Existing `TApplication`,
`TDesktop`, `TWindow`, `TDialog`, `TButton`, `TInputLine`, clusters,
`TScrollBar`, `TEditWindow`, `THelpWindow`, `TMenuBar`, and `TStatusLine`
contracts cover the intended composition.

Selective comment review is `CommentNeeded` only for constrained-layout,
historical-deviation, controlled-root, atomic-publication, and primary
cell-proof boundaries. Obvious control construction remains
`NoCommentNeeded`. Public example APIs and XML comments change, so DocFX plus
Playwright/Axe is mandatory.

## Showcase Evidence Matrix

Alle zehn Zeilen sind durch reale App-Loop-, Zustands-, View- und Cell-Proofs
abgeschlossen. / All ten rows are complete through real app-loop, state,
view, and cell proof.

| ExampleId | FrameworkDecision | MainComponent | StatusProof | DescriptionProof | KeyboardInventory | NormalProof | ConstrainedProof | HistoricalIntent | IntentionalDeviation | FollowUpBoundary |
|---|---|---|---|---|---|---|---|---|---|---|
| `Tp7Demo` | `UseExistingFramework` | real closeable/movable `TWindow` family on `TDesktop` | operation/window/focus plus F1/Quit | F1; all five dimensions | Open, Tile, Cascade, Next, Close, Help, F1, Ctrl+Q | app-loop command/idle/window operation tests | `48x16`; window and Description cells | four Demo Pascal units | deterministic gadgets; no host-memory probe | None |
| `Tp7Edit` | `UseExistingFramework` | focused `TFileEditor` in `TEditWindow` | modified/save/close/rejection plus F1/Quit | F1; controlled-root boundary explicit | File/Edit/Search/Help, text, save, close, find/replace, F1, Ctrl+Q | app-loop editing, conflict, traversal, menu/focus tests | `48x16`; real editor chrome and Description | `TVEDIT.PAS` | controlled root and explicit conflicts | None |
| `Tp7Help` | `UseExistingFramework` | focused `THelpViewer` in `THelpWindow` | context/diagnostic/reference/Back plus F1/Quit | F1; atomic/fallback boundary explicit | known/unknown, reference, Back, compile invalid, F1, Ctrl+Q | app-loop diagnostics, context, reference, Back, fallback tests | `48x16`; viewer/Description cells | `TVHC.PAS`, `HELPFILE.PAS`, `DEMOHELP.PAS` | closed UTF-8 typed model | None |
| `Tp7ResourceDemo` | `UseExistingFramework` | real `TDialog`, focused `TButton`, loaded menu/status text | ready/loaded/rejected plus F1/Quit | F1; exact-key/atomic boundary explicit | load, Tab, Enter, F1, Ctrl+Q | app-loop valid and three malformed cases | `48x16`; dialog and Description cells | `TVRDEMO.PAS` | exact allowlisted records | None |
| `Tp7ResourceGenerator` | `UseExistingFramework` | real `TDialog`, focused `TInputLine`, Generate button, progress/result | ready/generated/rejected plus F1/Quit | F1; root/allowlist boundary explicit | target, Tab, Alt+G/Enter, F1, Ctrl+Q | app-loop generate/load and traversal rejection | `48x16`; target, progress, result, Description | `GENRDEMO.PAS` | controlled root/no overlays | None |
| `Tp7AsciiTable` | `UseExistingFramework` | focused `Wave5GridView`, 256 cells | code/hex/rejection plus F1/Quit | F1; purpose, keyboard, 16x16 deviation, byte boundary, app-loop proof | arrows, PageUp/PageDown, Home/End, direct select, F1, Ctrl+Q | app-loop navigation plus invalid direct selection | `52x22`; identity, `00`, `FF`, Description | `ASCIITAB.PAS` byte-table selection | compact Unicode 16x16 text grid; no host codepage | None |
| `Tp7Calculator` | `UseExistingFramework` | `TDialog`, 20 `TButton`, first button focused | real `Wave5StatusLine`; result/rejection plus F1/Quit | F1 event; all five app-specific dimensions asserted | digits, decimal, operators, Enter/`=`, C, Backspace, S, Tab, F1, Ctrl+Q | `Tp7CalculatorSmokeTests`, 6/6, real app-loop through `app.Run()` | `40x12`; display, `7`, `=`, F1, positive region | `CALC.PAS` display/button flow | typed `decimal`; atomic division rejection | None |
| `Tp7Calendar` | `UseExistingFramework` | focused `Wave5GridView`, month/day matrix | selected date plus F1/Quit | F1; purpose, keyboard, fixed-fixture deviation, host-date boundary, app-loop proof | day arrows, PageUp/PageDown month, F1, Ctrl+Q | app-loop day navigation and fixed 2026-12/2027-01 rollover | `42x16`; identity, month, Description | `CALENDAR.PAS` month calendar | fixed invariant fixture and explicit day focus | None |
| `Tp7Puzzle` | `UseExistingFramework` | focused `Wave5GridView`, 16 board cells | focus/move/rejection plus F1/Quit | F1; purpose, keyboard, deterministic deviation, adjacency boundary, app-loop proof | arrows, Enter, direct tile command, F1, Ctrl+Q | app-loop focus, accepted move, atomic invalid move | `38x15`; identity, board, Description | `PUZZLE.PAS` selectable 4x4 puzzle | deterministic solvable fixture instead of random shuffle | None |
| `Tp7MouseDialog` | `UseExistingFramework` | `TDialog` with `TCheckBoxes`, `TScrollBar`, `TButton` | capability/settings/activation plus F1/Quit | F1; purpose, keyboard, local-state deviation, capability boundary, app-loop proof | Tab, Space, arrows, Enter, F1, Ctrl+Q; mouse optional | app-loop controls/focus, keyboard/mouse activation, loss cancellation, no host mutation | `46x16`; identity, keyboard fallback, Description | `MOUSEDLG.PAS` settings and activation dialog | local capability-aware state; no host setting mutation | None |

US2 proof roles: real `app.Run()` paths are `PrimaryProof`; temporary roots
and fixture creation are `SetupOnly`; direct pre-run shortcut inspection is
`SupplementalProof`. No helper replaces Desktop, editor, Help, status, focus,
Description, or rendered-cell evidence. No framework, package, dependency,
project, arbitrary user file, Help grammar, persistence contract, Wave-6
artifact, or Feature-034 artifact changed.

US3 proof roles: temporary-root creation and malformed-byte mutation are
`SetupOnly`; real generator/demo loops, state, focus, status, Description, and
cells are `PrimaryProof`. No serialization, package, dependency, framework,
project, or path-policy scope changed.

US4 proof roles: real `app.Run()` keyboard, command, mouse, capability,
focus, status, Description, and rendered-cell paths are `PrimaryProof`.
The controlled mouse capability fixture is `SetupOnly`; no direct helper
replaces a grid, dialog, domain transition, rejection, or cell assertion.

US4 scope review: no domain model, framework project, package, dependency,
project, host setting, terminal capability contract, Wave-6 artifact, or
Feature-034 artifact changed.

## Foundational Analyze Preconditions

- Historical and modern sources were reviewed read-only.
- All 15 historical blobs match Feature 032 exactly.
- Ten stable viewports, shortcut inventories, decisions, and starter rows are
  explicit.
- Existing framework APIs are sufficient; there is no pre-authorized
  `SmallFrameworkFix`.
- The Description command contract is stable while its missing behavior
  remains available for the calculator Red test.
- Evidence, tasks, version, run state, and shared guidance remain serialized.

## Governance Applicability

| Preset | Version | Checkpoint | Applicability | Rationale | Evidence path | Owner | Reviewer | Review date | Result | Residual risk | Follow-up | Re-evaluation trigger |
|---|---|---|---|---|---|---|---|---|---|---|---|---|
| security-governance | 0.6.0 | NIST SSDF, CWE Top 25, controlled inputs and existing supply-chain gates; ASVS, feature-owned SBOM/VEX/SLSA/OpenSSF, AI-SBOM, NIS2, CRA, EU AI Act and DORA screening | Applicable with trigger-based N/A | Secure input, file, Help, Resource and mouse boundaries remain applicable. ASVS is N/A without a web/auth service; new supply-chain artifacts are N/A without a package or release component; AI-SBOM and regulation are N/A without runtime AI or a regulated product role. | tests, workflows, this evidence | Codex | Thorsten | 2026-07-17 | Pass (scope/applicability) | Platform and provider gates remain pending until PR validation | Run mandatory local and remote gates | Web/auth, package/release, runtime AI, regulated role, dependency or boundary change |
| architecture-governance | 0.5.0 | STRIDE/CIA/CAPEC; S-ADR, arc42 security concept, Zero Trust, SAMM, BSI C3A and BSI C5 screening | Applicable with trigger-based N/A | Command, focus, controlled-file, Resource, Help and mouse boundaries require threat review. New S-ADR/arc42 security material, Zero Trust/SAMM and BSI C3A/C5 are N/A because no service boundary, cloud provider, deployment topology or assurance model changes. | `plan.md`, tests, this evidence | Codex | Thorsten | 2026-07-17 | Pass (scope/applicability) | Existing runtime boundaries still depend on full tests | Preserve bounded architecture and run gates | Service, cloud, provider, deployment, distributed-flow or security-concept change |
| isaqb-architecture-governance | 0.2.0 | Quality goals, building blocks, runtime views, risks and debt | Applicable | The shared showcase composition and ten application paths change while public framework building blocks remain stable. | `plan.md`, `research.md`, showcase matrix | Codex | Thorsten | 2026-07-17 | Pass (scope/applicability) | Bounded examples may still reveal reusable follow-up needs | Route only proven reusable gaps to `FollowUpHardening` | Shared architecture or public framework contract change |
| a11y-governance | 0.4.0 | Keyboard, focus, status, Description, text-first proof, constrained layout and didactic comments | Applicable | Keyboard-reachable visible composition and learner-facing guidance are primary outcomes; changed non-trivial proof and layout logic was reviewed for moderate bilingual comments. | tests, ten guides, showcase matrix | Codex | Thorsten | 2026-07-17 | Pass (scope/applicability) | Terminal rendering can vary across hosts | Run DocFX/Axe and remote platform gates | Navigation, public docs, interaction, focus or rendering change |
| cross-platform-governance | 0.2.0 | Linux/macOS/Windows runtime proof; script parity screening | Applicable with trigger-based N/A | Ten terminal applications require OS proof. New Bash/PowerShell script parity is N/A because no script-shaped tool changes. | CI workflows, entry-point smoke plan | Codex | Thorsten | 2026-07-17 | Pass (scope/applicability) | Host terminal differences require provider evidence | Run exact-head OS gates | Script, host capability or platform boundary change |
| agent-parity-governance | 0.3.0 | Five maintained guidance surfaces; `.specify/templates/` screening | Applicable with trigger-based N/A | Feature and Wave markers change on all maintained agent surfaces. Repository-owned templates are N/A because the workflow contract is unchanged. | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, Copilot files | Codex | Thorsten | 2026-07-17 | Pass (scope/applicability) | Text drift remains possible until parity check | Run homogeneity/parity validation | Shared guidance or repository-owned template change |
| autonomous-run-governance | 0.2.2 | State, convergence, authority, exact-head gates, review and causal closeout | Applicable | The user authorized a complete `MergeAndSync` delivery with serialized evidence and a non-recursive closeout. | run state, gate requirements, tasks, this evidence | Codex | Thorsten | 2026-07-17 | Pass (scope/applicability) | Provider review or approval can remain unavailable | Validate exact head and record unavailable reviews honestly | Provider-neutral orchestration defect or authority change |

`docs/toc.yml` bleibt unverändert: Alle zehn bestehenden Wave-5-Guides sind
bereits semantisch eingehängt. Alle Guides wurden gegen die tatsächlichen
Shortcuts, DE-first/EN-second CEFR-B2, textorientierte A11Y und
sprachmarkierte Code-Fences geprüft.

`docs/toc.yml` remains unchanged because all ten existing Wave-5 guides are
already present in semantic navigation. All guides were reviewed against the
actual shortcuts, German-first/English-second CEFR-B2, text-first
accessibility, and language-tagged code fences.

## Validation

| Command or review | Trigger | Result | Evidence or failure boundary |
|---|---|---|---|
| Agent parity and homogeneity | Shared guidance changed | Pass with pre-existing scanner limit | Four complete Spec-Kit blocks are byte-equal at SHA-256 `754a77b2c2ffe55a487c93e7d5c7116cb1cc4b22c92c1ed6a594b525f70befc6`; the compact generated Copilot surface carries the same completed-candidate/Wave-6-blocked state. Legacy `scripts/check-homogeneity.sh .` exits 2 before scanning because repository-local `scripts/lib/hg-*.sh` is absent; this is not represented as a passed scanner. |
| Static scope and secret scans | Every local candidate | Pass | `git diff --check` passes; protected `src/`, `TVDEMOS/`, `TVFM/`, `tv203s/`, project, package, dependency and generated-output inventories are empty. Placeholder search finds only completed CHK009 wording. Agent secret scan reports high 0, pre-existing local `.claude` medium 1 and prompt-only low 5; Gitleaks current-diff scan passes. |
| `dotnet format TuiVision.sln --verify-no-changes` | Changed C# and tests | Pass, exit 0 | Final pre-test source candidate requires no formatter changes. |
| `git diff --cached --check` and path inventory | Final candidate | Pass after one whitespace correction | Exact candidate contains 50 staged paths, 0 unstaged and 0 untracked paths. The first run correctly rejected six metadata-line trailing spaces; the corrected candidate passes and contains no protected, generated, package, dependency, project or historical-source path. |
| Targeted Wave-5 showcase tests | Ten UI compositions | Pass, 40/40 at `1.33.0.344` | `Tp7` plus `Wave5Showcase` Release filter covers Calculator, central apps, Resources, domain/mouse and exact/negative/parity matrix validation. |
| `dotnet build TuiVision.sln --configuration Release` | Normal entry-point artifacts | Pass at `1.33.0.345` | 0 warnings, 0 errors; all ten `Tp7*` Release entry points built. |
| Ten `dotnet run --no-build --configuration Release --project examples/Tp7* -- --smoke` commands | Normal entry points | Pass, 10/10 | All ten real executables enter and leave their controlled smoke path without rebuild; visible text/status output is produced. |
| Full Release tests | Shared executable example code | Pass, 826/826 at `1.33.0.346` | Core 52, Compatibility 18, Serialization 48, Controls 373, Drivers 151 and example smokes 184; zero failures or skips. |
| `xmllint --noout coverlet.runsettings` | Coverage configuration | Pass, exit 0 | Canonical coverage settings are well-formed before collection. |
| Canonical Coverlet coverage | Shared executable example code | Pass, 826/826 at `1.33.0.347` | Core 92.96%, Controls 86.66%, Serialization 90.01%, Compatibility 80.55%, Drivers.Console 89.18%; all exceed 70%. The non-gate example-smoke project has no collector, matching the accepted canonical boundary. |
| DocFX and Playwright/Axe | Ten learner-facing guides and XML comments | Pass | DocFX 2.78.5 builds 341 models with 0 warnings/errors; Playwright/Axe passes 2/2. |
| UTF-8, text-first and supply-chain review | Learner-facing Markdown and executable examples | Pass | 20 changed Markdown files round-trip as UTF-8; Lynx text-first samples pass 3/3; no generated output is tracked; NuGet vulnerable-package review and npm production audit report 0 vulnerabilities. |
| Evidence, state, prerequisites and checklists | Final local candidate | Pass | Showcase 10, governance 7, accepted artifacts 14, tasks 174/196; Bash and PowerShell state validators pass, `specify check` passes, prerequisites resolve Feature 033 and all feature checklists have zero incomplete items. |
| Linux, macOS, Windows gates | Executable TUI examples | Open | Pending |

## Remote Delivery

| Item | Result | Evidence |
|---|---|---|
| Push | Open | Feature branch |
| Pull request | Open | Pending |
| Required checks | Open | Pending |
| Acceptance-gate mapping | Open | `autonomous-gate-requirements.json` after tasks |
| Review threads | Open | Pending |
| Unavailable reviews | None observed | Pending |
| Reviewed head | Open | Pending |
| Merge | Open | Pending |
| Local `main` sync | Open | Pending |
| Causal closeout | Open | Pre-name `delivery-closeout.md` only if terminal facts require it |
| Duplicate workflow events | N/A | Re-evaluate after push and PR |

## Retrospective

- **Effective**: Evidence and run state existed before implementation.
- **Waste**: Pending.
- **Recurring blocker**: Pending.
- **Recommended refinement**: Pending.
