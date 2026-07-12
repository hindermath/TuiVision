# Tasks: A11Y Framework

**Input**: Accepted artifacts under `specs/023-a11y-framework/`  
**Delivery mode**: `MergeAndSync`  
**Acceptance ledger**: `specs/023-a11y-framework/pr-evidence.md`

Tasks are sequential because the slices share public contracts, focus dispatch,
menu/status structures, the smoke project, evidence, version, docs, statistics,
agent guidance and delivery state.

## Phase 1: Preflight and Evidence Foundation

- [X] T001 Verify branch ancestry, clean ownership, feature pointer and intake order; record exact state in `pr-evidence.md`
- [X] T002 Run `specify check` and PowerShell prerequisite checks with tasks; record results
- [X] T003 Verify all four checklists have zero incomplete items
- [X] T004 Read AGENTS, Constitution, Lastenheft and all 023 artifacts; record conflicts or `None`
- [X] T005 Verify six preset IDs/versions and record delivery authority plus narrow bypass boundary
- [X] T006 Complete requirement/proof, API, keyboard, historical, governance, validation and remote table shapes
- [X] T007 Record `speckit-constitution` unchanged and `speckit-taskstoissues` N/A rationale
- [X] T008 Record Specify, two Clarify passes, checklists, Plan, Plan Review, Tasks and Analyze state
- [X] T009 Scan artifacts for placeholders, duplicate requirements/tasks and unresolved markers
- [X] T010 Prove initial `git diff -- tv203s/` is empty

## Phase 2: Compile Surface and Historical Review

- [X] T011 Review Core/Controls dependency direction and public API/XML/nullability surface
- [X] T012 Review all `cmFocusChanged` producers/consumers and same-target no-op flow
- [X] T013 Review `TMenuItem`, `TStatusItem`, `TMenuBar`, `TStatusLine` item state and key representation
- [X] T014 Review palette, draw-buffer and console-colour ownership for semantic scheme integration
- [X] T015 Inventory selectable/focusable public Control families and existing keyboard tests
- [X] T016 Review smoke harness, app-loop queue, focus/view/cell/status/description helpers
- [X] T017 Review `.github/workflows/pages.yml` triggers, DocFX, npm, Chrome and Axe failure semantics
- [X] T018 Review relevant historical group/focus/menu/status sources and headers read-only
- [X] T019 Record modern A11Y no-direct-equivalent and intentional deviations
- [X] T020 Record compile-surface findings and re-prove no `tv203s/` change

## Phase 3: Core Contracts Test First

- [X] T021 Add failing Core tests for valid opt-in widget semantics
- [X] T022 Add failing Core tests for immutable accessible shortcut values and validation
- [X] T023 Add failing Core tests for nonblank label/text/source and nonzero key/command rejection
- [X] T024 Review grouped Core red matrix and expected compile/validation boundaries
- [X] T025 Align `Directory.Build.props` to `1.23.0.<next-build>` and increment Build
- [X] T026 Run targeted Core tests expecting documented red compile boundary; record result
- [X] T027 Add XML-documented `IAccessibleWidget`
- [X] T028 Add XML-documented `TAccessibleShortcut`
- [X] T029 Add XML-documented `IAccessibleShortcutProvider`
- [X] T030 Run static compile-surface review for XML, namespaces and API scope
- [X] T031 Increment Build and run targeted Core tests green; record count/version

## Phase 4: Focus Announcement Test First

- [X] T032 Add failing Controls tests for typed focus target/label/description/capability payload
- [X] T033 Add failing tests for non-widget target without fabricated label
- [X] T034 Add failing tests for blank custom label rejection/suppression boundary
- [X] T035 Add failing tests for exactly one event per actual transition and none for same target
- [X] T036 Add failing `TStatusLine` compatibility tests for typed and legacy payloads
- [X] T037 Review focus red matrix and event ownership
- [X] T038 Increment Build and run focus tests expecting red boundary; record result
- [X] T039 Add XML-documented Controls focus announcement payload
- [X] T040 Update `TProgram.CurrentChanged` to emit typed payload on existing command
- [X] T041 Update `TStatusLine` to consume typed payload while retaining legacy view handling
- [X] T042 Add concise didactic comments for single-event and compatibility boundaries
- [X] T043 Increment Build and run focus tests green; record count/version
- [X] T044 Record SC-001 proof and residual compatibility risk

## Phase 5: Structured Shortcuts Test First

- [X] T045 Add failing menu shortcut query tests for key/text/command/source
- [X] T046 Add failing menu exclusion tests for separators, disabled and zero-command entries
- [X] T047 Add failing status shortcut query tests for linked definitions/items
- [X] T048 Add failing duplicate-key source-preservation and query-no-mutation tests
- [X] T049 Review shortcut red matrix and ownership
- [X] T050 Increment Build and run shortcut tests expecting red boundary; record result
- [X] T051 Implement `IAccessibleShortcutProvider` on `TMenuBar`
- [X] T052 Implement provider on `TStatusLine` using current/defined items without execution
- [X] T053 Add exact key/source normalization and exclusion rules
- [X] T054 Add concise comments for truth/exclusion and duplicate-source boundaries
- [X] T055 Increment Build and run shortcut tests green; record count/version
- [X] T056 Record SC-002 proof and key-conflict follow-up trigger

## Phase 6: High Contrast Test First

- [X] T057 Add failing tests for immutable named semantic colour roles
- [X] T058 Add failing tests for `HighContrast` role differences and readable foreground/background pairs
- [X] T059 Add failing tests for explicit application and unchanged default behavior
- [X] T060 Add failing tests for application to bufferless or narrow views without failure
- [X] T061 Review contrast red matrix and text-not-colour-only boundary
- [X] T062 Increment Build and run contrast tests expecting red boundary; record result
- [X] T063 Add XML-documented immutable `TColorScheme` and `HighContrast`
- [X] T064 Add bounded scheme ownership/application to participating shell views
- [X] T065 Make menu/status rendering consume active semantic roles while preserving defaults
- [X] T066 Add concise comments for opt-in and terminal-capability boundary
- [X] T067 Increment Build and run contrast tests green; record count/version
- [X] T068 Record SC-004 proof and host-colour residual risk

## Phase 7: Keyboard Accessibility Matrix

- [X] T069 Finalize explicit selectable Control-family inventory in evidence
- [X] T070 Add failing matrix tests for `TButton` Tab/ShiftTab/Enter/direct shortcut
- [X] T071 Add failing matrix tests for `TInputLine` Tab/ShiftTab/arrows and N/A columns
- [X] T072 Add failing matrix tests for `TListBox` Tab/ShiftTab/arrows/Enter and N/A shortcut
- [X] T073 Add failing matrix tests for `TMenuBar` F10/arrows/Enter/mnemonic and Tab N/A
- [X] T074 Add failing matrix tests for `TStatusLine` provider shortcut and passive-navigation N/A
- [X] T075 Add failing matrix tests for `TDialog`/`TGroup` Tab/ShiftTab/default Enter
- [X] T076 Add failing disabled/invisible rejection and wrap-around cases
- [X] T077 Review every matrix row for Proof or concrete N/A
- [X] T078 Increment Build and run keyboard matrix expecting any documented gaps
- [X] T079 Implement only bounded framework fixes required by accepted matrix
- [X] T080 Re-run static focus/event review after matrix fixes
- [X] T081 Increment Build and run keyboard matrix green; record count/version
- [X] T082 Complete keyboard evidence and SC-003 coverage at 100 %

## Phase 8: Reference Application Test First

- [X] T083 Add `A11yFramework` project skeleton, solution/coverage/smoke references without behavior
- [X] T084 Add failing construction/first-frame accessible widget and visible purpose tests
- [X] T085 Add failing real-loop Tab/ShiftTab focus announcement and status tests
- [X] T086 Add failing menu/status shortcut query and keyboard execution tests
- [X] T087 Add failing High-Contrast toggle identity/text/cell before-after tests
- [X] T088 Add failing Help -> Description and deterministic quit tests
- [X] T089 Add failing exact view-tree and standard/narrow rendered-region tests
- [X] T090 Add failing unsupported native-AT/full-migration honest fallback proof
- [X] T091 Review grouped reference red matrix, app ownership and proof limits
- [X] T092 Increment Build and run reference smokes expecting red boundary; record result
- [X] T093 Implement labeled opt-in reference widgets
- [X] T094 Implement app shell, event queue, focus status and structured shortcut display
- [X] T095 Implement explicit High-Contrast toggle and text state
- [X] T096 Implement keyboard-reachable bilingual Description and honest fallback
- [X] T097 Implement standard/narrow composition and stable proof-region accessors
- [X] T098 Add concise didactic comments for app-loop, A11Y and cell-proof boundaries
- [X] T099 Increment Build and run reference smokes green; record count/version
- [X] T100 Record SC-005 state/view/cell proof and framework decisions

## Phase 9: Documentation, Governance and Parity

- [X] T101 Add DE-first/EN-second CEFR-B2 `docs/guides/a11y-framework.md`
- [X] T102 Update examples README/root README/toc only where navigation requires it
- [X] T103 Add PF-A11Y requirements and completion marker to `Pflichtenheft.md`
- [X] T104 Validate existing Pages workflow statically; change only if a real acceptance gap remains
- [X] T105 Populate security NIST/CWE/STRIDE rows and bounded semantic-text threat boundary
- [X] T106 Populate supply-chain/regulatory/AI trigger-based N/A rows with reevaluation triggers
- [X] T107 Populate architecture/iSAQB and S-ADR/arc42/Zero Trust/SAMM/C3A/C5 N/A rows
- [X] T108 Populate A11Y, cross-platform, script N/A, agent parity and templates N/A rows
- [X] T109 Complete didactic comment review decisions for every changed non-trivial area
- [X] T110 Update all five maintained agent surfaces with Feature-023 completion/next-intake context
- [X] T111 Verify five agent surfaces are synchronized and record hashes
- [X] T112 Review changed Markdown/XML for bilingual CEFR-B2, umlauts/ß, semantics and text-first use
- [X] T113 Update `docs/project-statistics.md` while preserving chronological ledger and final Gesamtstatistik
- [X] T114 Complete FR/SC/API/keyboard/governance/follow-up evidence rows

## Phase 10: Validation and Archive

- [X] T115 Run `git diff --check`, placeholder, scope, package, generated-output and `tv203s/` scans
- [X] T116 Run `dotnet format --verify-no-changes --no-restore`
- [X] T117 Increment Build and run targeted Core/Controls/reference Release tests
- [X] T118 Increment Build and run complete example-smoke suite
- [X] T119 Increment Build and run full Release suite
- [X] T120 Validate Coverlet XML, increment Build and run canonical coverage gate
- [X] T121 Run `docfx docfx.json` and record warnings/errors
- [X] T122 Run matching Playwright/Axe DocFX smoke and record count
- [X] T123 Review guide/generated representative page through UTF-8 `lynx` when available
- [X] T124 Run tracked-secret scan and remove all generated DocFX/test/coverage output
- [X] T125 Confirm final diff has no native bridge, dependency, Wave 1-4, Feature 024 or historical-source change
- [X] T126 Archive binding Lastenheft via repository PowerShell rename workflow
- [X] T127 Re-run `specify check`, prerequisites, checklist counts and final consistency analysis
- [X] T128 Mark local tasks complete only where evidence contains acceptance results

## Phase 11: Authorized GitHub Delivery

- [X] T129 Align version to `1.23.<branch-commit-count>.<build>` without incrementing Build
- [X] T130 Commit intentional implementation scope and record observed hash externally if self-referential
- [X] T131 Re-align version with bounded follow-up commit only if branch commit count requires it
- [ ] T132 Push `023-a11y-framework`
- [ ] T133 Create ready PR from `pr-evidence.md`
- [ ] T134 Monitor PR-context required checks, Claude/Copilot availability and GraphQL threads
- [ ] T135 Remediate every actionable remote finding with focused validation and thread response
- [ ] T136 Use narrow admin bypass only if required checks green, zero actionable threads and sole approval rule remains
- [ ] T137 Merge with merge commit and delete remote feature branch
- [ ] T138 Create one causal evidence-only closeout PR only if post-merge/review facts cannot be recorded earlier
- [ ] T139 Merge any required closeout after green checks and zero actionable threads
- [ ] T140 Switch to local `main`, pull/prune and prove clean `HEAD == origin/main`

## Phase 12: Learning Handoff

- [ ] T141 Add Feature-023 retrospective observation and classify each finding
- [ ] T142 Create non-empty TuiVision retrospective branch/PR only for evidence-backed local improvements
- [ ] T143 Merge retrospective PR and re-synchronize local `main`
- [ ] T144 Rebase Home-Baseline package branch on `origin/main`
- [ ] T145 Add 023 workitem with feature/PR/merge evidence and promotion decisions
- [ ] T146 Snapshot changed reusable skill/runbook/template/evidence/agent inputs plus manifest
- [ ] T147 Run Home-Baseline secret/diff validation, commit and push package branch
- [ ] T148 Record 023 as the sixth field run and hand control to preset productization without starting Feature 024
