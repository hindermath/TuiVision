# Autonomous Run Evidence: Component and Data Conformance Hardening

**Branch**: `026-component-data-conformance-hardening`
**Feature directory**: `specs/026-component-data-conformance-hardening`
**Binding intake**: `Lastenheft_11_Component-Data-Conformance-Hardening.md`
**Delivery mode**: `MergeAndSync`
**Authority source**: User instruction dated 2026-07-13 to execute the recommended preset sequence and then deliver Feature 026 autonomously.

## Scope

### Included

- Close `F010`/`C019`, `F011`/`C021`, `F012`/`C023`, and `F013`/`C026` with new real-path Red/Green proof.
- Additive dialog validation, validator integration, typed file outcomes, safe allowlisted UI-resource descriptions, XML documentation, tests, audit reconciliation, and completion evidence.
- Reuse Feature-025 focus, modal, command, lifecycle, and keyboard contracts.

### Excluded

- Wave-5 or Wave-6 applications, destructive file-manager operations, broad framework or serialization redesign, new dependencies, hidden file I/O, arbitrary runtime type activation, and breaking APIs.
- Changes under `tv203s/`, `TVDEMOS/`, `TVFM/`, external Free Vision, or `examples/`.
- Mechanical C++ or Pascal translation and historical binary-resource compatibility.

## Run Gates

| Phase | Attempt | Result | Evidence | Remaining action |
|---|---:|---|---|---|
| Preflight | 1 | Pass | Branch `026-component-data-conformance-hardening`; `.specify/feature.json`; `specify check`; prerequisite JSON | None |
| Specify | 1 | Accepted | `spec.md`; 28 FR and 10 SC | None |
| Clarify | 2 | Accepted | Four material decisions encoded in `spec.md`; second pass found no critical ambiguity | None |
| Checklists | 2 | Pass | Five checklists; 92 completed and zero incomplete items | None |
| Plan | 2 | Pass | `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, contract, two plan-review checklists | None |
| Tasks | 1 | Pass | `tasks.md`; 153 sequential tasks | Execute T025-T153 |
| Analyze | 3 | Pass | 38/38 normative items covered; no Critical, High, or Medium issue after two remediations | None |
| Implement | 1 | Pass | F010-F013 real-path Red/Green slices and four closed audit resolutions | None |
| Validate | 1 | Pass | 748/748 Release and coverage tests, five assembly gates, DocFX/Axe/Lynx, parity, secrets, and scope firewall | Exact staged-candidate check remains before commit |
| Deliver | 1 | Open | Remote Delivery table | Run after exact-candidate validation |

## Baseline and Protected Sources

| Surface | Baseline | Result | Boundary |
|---|---|---|---|
| Repository head | `91fef0f` | Pass | Feature branch starts from merged preset adoption on synchronized `main` |
| `tv203s/` | 987 tracked files; index hash `457efd4ebd5058faeb0cc589448bffd08d24aacd` | Pass | Read-only primary historical source |
| `TVDEMOS/` | 18 tracked files; index hash `605362ad690c257c5c5a491b389e3ae9e06fa20e` | Pass | Read-only consumer evidence |
| `TVFM/` | 24 tracked files; index hash `f396a584bf5580e65f14b1f4af2c1867d3206945` | Pass | Read-only consumer evidence |
| Free Vision | commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` in `/tmp/tuivision-freevision-026` | Pass | External untracked second opinion; never vendored |
| Planned production diff | Empty before T025 | Pass | `git diff --name-only -- src tests examples tv203s TVDEMOS TVFM` returned no path |

Free Vision SHA-256 evidence: `dialogs.inc` `7ce3dbf42e478ee220689204fadecbe7c6407bbe739ad77e71292db75e05a208`; `validate.inc` `865a9342390618c535ac88a39258bec53aed536f870fb2ad8fb92baf152ef6ba`; `stddlg.inc` `3626e70d310831430f4f8f45c283c19f11d30047103f2d4b81b432de2993f96c`; `resource.pas` `c447361213ca73e9559f2fce598534f3ede7798b1b90b8499ad55f0dfc1f9d3b`.

## Requirement Coverage

| Requirements | Primary task coverage | Result |
|---|---|---|
| `FR-001`-`FR-004`, `SC-002`-`SC-003` | T025-T042 | Covered by F010 Red/Green real dialog path |
| `FR-005`-`FR-007`, `SC-003`-`SC-004` | T043-T060 | Covered by F011 edit/focus/acceptance path |
| `FR-008`-`FR-010`, `SC-005` | T061-T076 | Covered by F012 mode/outcome matrix |
| `FR-011`-`FR-014`, `SC-006` | T077-T102 | Covered by F013 persisted-record and Controls reconstruction paths |
| `FR-015`-`FR-020`, `SC-001`, `SC-009` | T006-T010, T023-T024, T103-T118, T133 | Covered by Red-first, historical, consumer, closure, and scope-firewall evidence |
| `FR-021`-`FR-023`, `SC-008` | T038, T057, T073, T100, T113-T115, T127-T129 | Covered by XML, didactic-comment, A11Y, DocFX, and text-first review |
| `FR-024`, `SC-007` | T119-T126 | Covered by format, full Release, and canonical coverage gates |
| `FR-025`-`FR-027` | T106-T110, T133, T135-T139 | Covered by audit, next-intake, agent, statistics, archive, and protected-path reconciliation |
| `FR-028`, `SC-010` | T018-T022, T116-T118, T130-T140 | Covered by complete governance rows and final trigger review |

## Finding Decisions and Follow-ups

| FindingId | ContractId | Decision | RedProof | Change | RealPathProof | HistoricalIntent | FreeVisionRelation | ConsumerBoundary | ApiA11YImpact | ResidualBoundary | Result |
|---|---|---|---|---|---|---|---|---|---|---|---|
| `F010` | `C019` | Implemented | Build 246 compile failure on missing contracts | Additive validation result/phase, ordered group walk, descendant focus, and completion classifier | Build 247; 19/19 filtered dialog tests | Explicit completion and ordered group validity | `FV006` corroborates dialog/group validity | Dialog framework only | Additive XML API and keyboard/focus proof | Safe-close policy remains separate | Pass |
| `F011` | `C021` | Implemented | Build 248 compile failure on missing phase method | Optional validator, three phases, explicit selection, and candidate-before-mutation edits | Build 249; 12/12 filtered input tests plus dialog integration in full suite | Validator ownership and state-preserving rejection | `FV007` corroborates input/validator lifecycle | Input framework only | Additive XML API and text-first rejection | Transfer protocol remains intentionally modernized | Pass |
| `F012` | `C023` | Implemented | Build 250 compile failure on missing outcome contract | Closed typed outcome, additive rejection projection, metadata-only classifier, and atomic history/close publication | Build 251; 8/8 filtered file-dialog tests | Operation-aware path decisions | `FV010` corroborates standard file-dialog modes | Metadata classification only | Additive XML API and rejection text | Caller owns later I/O and TOCTOU | Pass |
| `F013` | `C026` | Implemented | Builds 252-253 compile failures on absent models, records, factories, and registrations | Closed primitive records, built-in allowlist, bounded resource loader, validated Controls models/adapters/factories | Builds 255 and 258; Controls 12/12 and Serialization 22/22 | Named resources reconstruct controlled UI | `FV012` corroborates resource responsibility | Framework records/factories only | Additive XML API, shortcut/cell proof, no runtime activation | No historical binary compatibility | Pass |

`Implemented` and `AlreadySatisfied` are the only closure decisions. `FollowUpHardening` cannot close an accepted finding. `ProductDecision` stops any breaking, destructive, arbitrary-activation, or unresolved format-compatibility change.

### F010 Dialog Completion and Validation Matrix

| Path | Expected boundary | Proof |
|---|---|---|
| Help, application, unknown command | Remains open and unconsumed | `TDialog_F010_NonCompletionCommandsRemainOpenAndUnconsumed` |
| `cmOK`, `cmYes`, `cmNo` | Stable child validation before completion | Existing completion matrix plus `TDialog_F010_AcceptanceStopsAtFirstRejectionAndFocusesTarget` |
| First rejected child | Later children skipped; target focused; text result retained | `TDialog_F010_AcceptanceStopsAtFirstRejectionAndFocusesTarget` |
| `cmCancel` | Completes without content validation | `TDialog_F010_CancelBypassesContentValidation` |
| Derived bounded completion | Protected classifier extends the set without replacing `HandleEvent` | `TDialog_F010_DerivedClassifierExtendsCompletionSet` |

### F011 Validator Lifecycle and Rejection Matrix

| Path | Expected boundary | Proof |
|---|---|---|
| No validator | Existing editing remains compatible | `TInputLine_F011_ValidatorIsOptionalAndNoValidatorRemainsCompatible` |
| Range edit | Meaningful intermediate candidate allowed; final phases strict | `TInputLine_F011_EditFocusAndAcceptanceUseDistinctValidatorPhases` |
| Filter edit | Invalid syntax rejected before mutation | Same phase test |
| Insert, paste, cut, delete, backspace | Text, cursor, offset, selection, insert mode, and event state preserved | `TInputLine_F011_RejectedEditsPreserveNonEmptySelectionAndState` |
| Collapsed selection | Same atomic state preservation | `TInputLine_F011_RejectedEditsPreserveCollapsedSelectionAndState` |
| Focus loss and dialog acceptance | Focus/input preserved; phase and text-first result observable | `TInputLine_F011_FocusAndDialogAcceptancePreserveInvalidInput`; `TDialog_F011_InputValidationRejectsAcceptanceWithAccessibleEvidence` |

### F012 File Outcome and Fixture Matrix

| Path | Expected outcome | Proof boundary |
|---|---|---|
| Directory navigation / filter | `Navigation` / `Filter` | Test-owned temporary directory; metadata only |
| Existing open target | `OpenAccepted` | Existing fixture content remains unchanged |
| New save target | `SaveAccepted` | Parent exists; no file is created |
| Existing save target | `OverwriteDecisionRequired` | Caller owns later content decision |
| File or directory selection | `SelectionAccepted` for the requested target kind | Mode-aware metadata classification |
| Cancel | `Canceled` | No history or file mutation |
| Missing open, missing parent, wrong type | `Rejected` with stable code/message | No stale accepted result and no history commit |
| Invalid path or wildcard syntax | `Rejected`, not an escaped exception | Platform-neutral NUL fixture; text-first result |

### F013 Resource and Reconstruction Matrix

| Path | Expected boundary | Proof |
|---|---|---|
| Dialog/menu/status built-ins | Exact-key roundtrip through explicit registry | `TUiDescriptionRecord_F013_BuiltInsRoundtripDialogMenuAndStatusWithExactKeys` |
| Menu model | Stable ID/order/command/mnemonic plus visible cells | `MenuDescription_F013_ValidModelReconstructsIdentityOrderCommandAndCells` |
| StatusLine model | First-match order, command, key, accessible shortcut | `StatusLineDescription_F013_ValidModelReconstructsContextOrderAndShortcut` |
| Graph/range/command/version | Rejected before Runtime reconstruction | Controls and Serialization negative tests |
| Truncation/trailing/unknown type/duplicate key | Whole load rejected; candidate not published | `TResourceFile_F013_TruncationTrailingUnknownTypeAndDuplicateKeysAreRejected` |
| Entry/payload/item/depth limits | Hard bounded rejection | `TResourceFile_F013_EntryAndPayloadLimitsAreRejectedWithoutPublication` and menu/status limits |
| Persisted fields | Primitive values only; no CLR activation metadata | Record property review and adapter tests |

Finding closure cardinality is exactly four `Implemented`, zero `AlreadySatisfied`, zero closing `FollowUpHardening`, and zero `ProductDecision`. No additional runtime, design, parity, format, application, or proof issue was discovered that requires a Feature-026 follow-up. Historical binary-resource compatibility, destructive file operations, Wave applications, and arbitrary activation remain accepted non-goals rather than newly discovered defects.

## Historical Intent

| Modern area | Historical source | Intent retained | Intentional deviation | Proof boundary |
|---|---|---|---|---|
| Dialog completion and child validity | `tv203s/contrib/tvision/classes/tdialog.cc`; `include/tv/dialog.h`, `group.h`, `view.h` | Four completion commands and hierarchical validity | Additive immutable C# result and protected classifier hook | F010 real `HandleEvent` tests |
| Input validation | `tinputli.cc`; `include/tv/inputln.h`, `validate.h` | Input owns an optional validator and preserves rejected state | Three explicit managed phases; no pointer transfer protocol | F011 candidate/focus/dialog tests |
| File dialog decisions | `tfiledia.cc`; `include/tv/filedlg.h`, `stddlg.h` | Mode-aware navigation, filter, open/select/save checks | Typed metadata-only outcome; caller owns I/O | F012 temp-directory matrix |
| Named UI resources | Turbo Vision resource responsibility plus consumer programs | Exact named lookup and reconstructable UI ownership | Closed primitive records and allowlisted factories; no object-graph activation | F013 roundtrip/malformed tests |
| Pascal second opinion | Free Vision `FV006`, `FV007`, `FV010`, `FV012` at pinned commit | Corroborates responsibility and lifecycle boundaries | Secondary only; no copied or vendored code | Hashes in Baseline section |
| Consumer evidence | Selected `TVDEMOS/*.PAS` and `TVFM/*.PAS` | Applications need framework dialogs, validation, files, and resources | No consumer application work in 026 | Protected path hashes and final diff |

## Governance Applicability

| RunId | Preset | Version | Checkpoint | Applicability | Rationale | Evidence path | Owner | Reviewer | ReviewDate | Result | Residual risk | Follow-up | Re-evaluation trigger |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| `026-G01` | security-governance | 0.6.0 | NIST SSDF / CWE Top 25 / OWASP input validation | Applicable | Untrusted resource and path metadata require bounded, fail-closed parsing and state-preserving rejection | Plan, F011-F013 tests, final diff | Maintainer | Codex | 2026-07-13 | Pass | Future callers may bypass additive contracts | Revalidate in Feature 028 | Input, parser, or trust boundary changes |
| `026-G02` | security-governance | 0.6.0 | ASVS | N/A | No Web, HTTP, authentication, or remote API surface | This file | Maintainer | Codex | 2026-07-13 | Pass | None | None | Such a service enters scope |
| `026-G03` | security-governance | 0.6.0 | SBOM / VEX / SLSA / OpenSSF / AI-SBOM | N/A | No dependency, package, release, provenance, runtime AI, model, or dataset change | Existing supply-chain evidence; final diff | Maintainer | Codex | 2026-07-13 | Pass | Existing release evidence remains separate | None | Dependency, release, or runtime AI scope changes |
| `026-G04` | security-governance | 0.6.0 | NIS2 / CRA / EU AI Act / DORA | N/A | No regulated service, product-market, runtime AI, or financial ICT boundary changes | Existing regulatory evidence; this file | Maintainer | Codex | 2026-07-13 | Pass | Legal classification can change outside 026 | None | Deployment, customer, or regulatory scope changes |
| `026-G05` | architecture-governance | 0.5.0 | STRIDE / CIA / CAPEC | Applicable | Tampered input, partial state, parser exhaustion, unsafe activation, and stale result confusion are explicit threats | Plan, contract, F011-F013 tests | Maintainer | Codex | 2026-07-13 | Pass | Additive APIs depend on correct consumer use | Revalidate in Feature 028 | Trust boundary or public contract changes |
| `026-G06` | architecture-governance | 0.5.0 | S-ADR / arc42 | N/A | Bounded additive contracts do not change deployment, external integration, sensitive data flow, authentication, or security architecture | Existing security architecture; plan | Maintainer | Codex | 2026-07-13 | Pass | Existing concepts remain authoritative | None | Architecture or trust boundary changes |
| `026-G07` | architecture-governance | 0.5.0 | Zero Trust / SAMM / BSI C3A / BSI C5 | N/A | No cloud, provider, distributed service, deployment topology, or organizational maturity scope | This file | Maintainer | Codex | 2026-07-13 | Pass | None | None | Cloud/provider/distributed operation enters scope |
| `026-G08` | isaqb-architecture-governance | 0.2.0 | Quality scenarios / modernization / risk | Applicable | Determinism, compatibility, security, accessibility, and maintainability are explicit quality scenarios | Spec, plan, contract, tests | Maintainer | Codex | 2026-07-13 | Pass | Additive API surface increases maintenance cost | Revalidate in Feature 028 | New layer or trade-off appears |
| `026-G09` | a11y-governance | 0.4.0 | Keyboard / focus / text-first / XML / comments | Applicable | Rejection must retain focus and expose text; public APIs and non-trivial logic are learner-facing | XML, tests, DocFX/Axe/Lynx rows | Maintainer | Codex | 2026-07-13 | Pass | Later controls may omit equivalent evidence | Revalidate in Feature 028 | Interaction or learner documentation changes |
| `026-G10` | cross-platform-governance | 0.2.0 | Path behavior | Applicable | File classification must use platform-neutral paths and CI evidence | F012 tests and remote matrix | Maintainer | Codex | 2026-07-13 | Open | Host filesystem edge cases vary | Revalidate through CI | Path or filesystem behavior changes |
| `026-G11` | cross-platform-governance | 0.2.0 | Script parity | N/A | Product scripts are not changed; existing archive scripts are invoked under their established parity contract | Final diff; archive evidence | Maintainer | Codex | 2026-07-13 | Pass | Invocation drift | Compare both dry-run/error paths | Script source changes |
| `026-G12` | agent-parity-governance | 0.3.0 | Maintained guidance surfaces | Applicable | Active feature and next-intake markers must remain synchronized | Agent files and parity scan | Maintainer | Codex | 2026-07-13 | Pass | Generated context can drift | Revalidate after later status changes | Shared guidance or status changes |
| `026-G13` | agent-parity-governance | 0.3.0 | `.specify/templates/` | N/A | Repository templates are consumed but not changed | Final diff | Maintainer | Codex | 2026-07-13 | Pass | None | None | A reusable template correction is proven |
| `026-G14` | autonomous-run-governance | 0.1.2 | Evidence / convergence / exact candidate / MergeAndSync | Applicable | Current user instruction authorizes full autonomous delivery with causal closeout | All 026 artifacts and remote table | Maintainer | Codex | 2026-07-13 | Open | Provider review may be unavailable | Retrospective after merge | Permission, gate, or reusable workflow issue appears |

## Preset and Agent Parity

- `specify preset list` reports the six baseline presets at priorities 10-60 plus `autonomous-run-governance` v0.1.2 at priority 70.
- `specify preset info` passed for all seven preset IDs.
- `specify preset resolve spec-template`, `plan-template`, and `tasks-template` each produced the expected eight-layer chain; `autonomous-run-evidence-template` resolves uniquely to v0.1.2.
- `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and generated `.github/agents/copilot-instructions.md` share SHA-256 `79a18abe596dd41a1d5cfe6f4f3b3ee9f7253a64e642cfb88fc2db431aa58f98` for the extracted 025/026/028 context and route only to Feature 028.
- Codex and Claude each contain exactly one `speckit-autonomous` and one `speckit-autonomous-retrospective` skill. Copilot contains exactly one agent and one prompt per command; OpenCode contains exactly one command file per command. No duplicate command payload was found.
- `.specify/templates/`, autonomous skill sources, generated autonomous prompts/agents/commands, scripts, and workflows are unchanged. The status-only agent-context update therefore does not claim a shared preset or template correction.

## Current Production Baseline

| Finding | Existing production boundary | New Red proof required |
|---|---|---|
| `F010` | `TDialog.HandleEvent` closes every command; `Valid` defaults to unconditional success | Non-completion command and ordered child rejection through real handler |
| `F011` | `TValidator.IsValid` exists but `TInputLine` has no validator, phase result, or selection contract | Candidate edit, focus veto, dialog acceptance, and exact state preservation |
| `F012` | `TFileDecisionResult` covers accepted/cancel decisions but not all mode/rejection outcomes | Eight-mode matrix, stale-result rejection, no hidden I/O |
| `F013` | Dialog records and exact named resources exist; menu/status reconstructable records are absent | Allowlisted roundtrip/factory and malformed/limit/atomic rejection |

F013 persisted-field review: `TDialogDescriptionRecord`, `TMenuDescriptionRecord`, and `TStatusLineDescriptionRecord` contain only integers, booleans, strings, nullable IDs, and immutable record lists. They contain no CLR type name, runtime owner, `TView`, delegate, pointer, reflection metadata, assembly name, or activation instruction. Runtime reconstruction occurs only after record and Controls validation through explicit factories.

## Validation

| Command or review | Trigger | Result | Evidence or failure boundary |
|---|---|---|---|
| `specify check` | Preflight | Pass | Seven presets available; Gemini CLI absent and Antigravity available as expected |
| Prerequisite JSON | Before implementation | Pass | Absolute feature directory and `tasks.md` available |
| Checklist scan | Before implementation | Pass | 92 complete; zero incomplete |
| Repeated Analyze | Before implementation | Pass | 38/38 normative items, 153 contiguous tasks, no Critical/High/Medium issue |
| `git diff --check` | Planning and every later candidate | Pass | Planning diff passed before production edits |
| `git diff --cached --check` plus candidate inventory | Before commit | Open | Run after implementation and final evidence |
| Build 246: filtered F010 Red test | F010 production-path baseline | Fail | Expected compile boundary: `TValidationPhase`, `TValidationResult`, and overridable completion classification did not exist |
| Build 247: filtered F010 Green test | Dialog completion and child validation | Pass | 19/19; real `HandleEvent`, four defaults, derived classifier, first rejection, focus target, Cancel bypass, and non-completion forwarding |
| Build 248: filtered F011 Red test | Validator integration baseline | Fail | Expected compile boundary: `TValidator` had no phase-aware virtual method and `TInputLine` had no validator/selection/result contract |
| Build 249: filtered F011 Green test | Edit, focus, acceptance, and state preservation | Pass | 12/12; optional validator, permissive range edit, strict filter edit, focus veto, exact selection/state preservation |
| Build 250: filtered F012 Red test | Mode-aware file outcome baseline | Fail | Expected compile boundary: no `TFileDialogOutcome`, outcome methods, observable result, or `FileDecisionKind.Rejected` existed |
| Build 251: filtered F012 Green test | File outcome and rejection matrix | Pass | 8/8; navigation, filter, Open, Save, overwrite, selection, Cancel, stale result, invalid path/filter, missing parent, no content mutation |
| Build 252: filtered F013 Controls Red test | UI-description Controls baseline | Fail | Expected compile boundary: menu/status description models and factories did not exist |
| Build 253: filtered F013 Serialization Red test | UI-description persistence baseline | Fail | Expected compile boundary: menu/status primitive records and built-in registrations did not exist |
| Build 254: filtered F013 Controls Green attempt | Public test documentation gate | Fail | Compile stopped on six missing public test-method XML summaries; no test executed |
| Build 255: filtered F013 Controls Green test | Menu/status validation and reconstruction | Pass | 12/12; identity, order, command, shortcut, context, view-tree/cell proof, malformed graph/range rejection, and adapter parity |
| Build 256: filtered F013 Serialization Green attempt | Public test documentation gate | Fail | Compile stopped on four missing public test-method XML summaries; no test executed |
| Build 257: filtered F013 Serialization Green attempt | Test documentation placement | Fail | Compile stopped because one XML summary was attached to the preceding method; no test executed |
| Build 258: filtered F013 Serialization Green test | Allowlisted resources and malformed-input boundaries | Pass | 22/22; dialog/menu/status exact-key roundtrip, invalid version/graph/range/command, truncation, trailing data, unknown type, duplicate key, entry/payload limits |
| `git diff --check` | Candidate hygiene before full validation | Pass | Exit 0; no whitespace error |
| Initial `git diff --cached --check` | Pre-final staged candidate | Fail | Four generated planning artifacts had one blank line at EOF and the new Evidence header used Markdown hardbreak whitespace; no product code issue |
| `dotnet format --verify-no-changes --no-restore` | Repository formatting | Pass | Exit 0; stdout/stderr empty |
| Build 259: full Release attempt | Repository regression gate | Fail | 631/635 passed; four audit-evidence tests correctly detected missing Feature-026 source/type inventory and pre-026 resolution assumptions; no product test failed |
| Build 260: targeted conformance-audit evidence | Audit remediation | Pass | 12/12; 139-source and 211-public-type inventory, 13 unique resolutions, reciprocal links, and closed 025/026 gate state |
| Build 261: full Release | Repository regression gate | Pass | 748/748; Core 52, Compatibility 18, Serialization 48, Controls 373, Drivers 117, Examples 140; zero skipped/failures |
| `xmllint --noout coverlet.runsettings` | Coverage configuration | Pass | Exit 0; XML well formed |
| Build 262: canonical Coverlet coverage | Five-assembly coverage gate | Pass | 748/748; Cobertura files under each gate test project's `TestResults/`; Examples collector absence is non-gate noise |
| Coverage calculation | >=70 percent per required assembly | Pass | Core 92.96%; Controls 86.66%; Serialization 90.01%; Compatibility 80.55%; Drivers.Console 89.18% |
| Targeted F010-F013 Release tests | Each slice | Pass | Builds 247, 249, 251, 255, and 258 passed; every invocation had one prior manual build-counter increment |
| Full Release and canonical coverage | Shared executable logic | Pass | Builds 261 and 262 passed after complete audit reconciliation |
| `docfx docfx.json` after statistics | Public API, XML, guide, and statistics changes | Pass | Exit 0; 325 models, 0 warnings, 0 errors; stdout/stderr contained no fatal signature |
| `npm run test:docfx` from `tests/web-a11y` | Generated-document A11Y | Pass | DocFX repeated with 0 warnings/errors; Playwright/Axe 2/2 passed |
| Initial Lynx `rg -q` pipeline | Text-first helper attempt | Fail | Exit 141 because `rg -q` closed the pipe early under `pipefail`; no content defect |
| UTF-8 Lynx review | Text-first A11Y | Pass | Full output captured before matching; guide, statistics, and `TValidationResult` API page passed 3/3 with umlauts and bilingual text |
| Agent context parity | Status and next-intake synchronization | Pass | Five extracted context sections have identical SHA-256; all route exclusively to Feature 028 |
| Seven-preset and command uniqueness | Preset/skill parity | Pass | Versions 0.6.0/0.5.0/0.2.0/0.4.0/0.2.0/0.3.0/0.1.2; autonomous Evidence resolves uniquely; generated command counts are exactly one per expected artifact |
| Bash secret scan with explicit repository root | Secret boundary | Pass | Exit 0; high=0, medium=1 local `.claude/settings.local.json`, low=5 expected agent metadata; gitleaks diff clean |
| PowerShell secret scan with explicit repository root | Cross-platform secret boundary | Pass | Exit 0; gitleaks diff and tracked-file scan clean |
| Initial protected-scope shell helper | Scope firewall helper attempt | Fail | zsh special variable `path` replaced `PATH` inside a loop; command was corrected without repository writes |
| Corrected protected-scope scan | Scope firewall | Pass | No diff in `tv203s/`, `TVDEMOS/`, `TVFM/`, examples, dependencies, projects, workflows, templates, scripts, generated output, caches, logs, or credentials |
| Historical subtree hashes | Read-only source integrity | Pass | `tv203s` `a2ec70e...`; `TVDEMOS` `38e3705...`; `TVFM` `e419066...` at `HEAD` and no changed path |
| Lastenheft archive parity | Traceability | Pass | Bash dry-run and PowerShell `-WhatIf` agreed on the branch-suffixed target; Bash `--no-commit` performed only the tracked rename |
| Project statistics | Completion ledger | Pass | Pre-statistics delta: production `+1761/-97`, tests `+802/-26`, docs/evidence `+2539/-60`, metadata `+4/-4`; final `## Gesamtstatistik` remains the last top-level block |

For every repository validation helper, the final record includes repository root, exit status, and error-channel review. A nominal zero exit status with a fatal signature is not accepted.

## Remote Delivery

| Item | Result | Evidence |
|---|---|---|
| Push | Open | Feature branch after exact-candidate commit |
| Pull request | Open | Feature PR after push |
| Required checks | Open | Map actual workflow/job/runner/platform/command |
| Acceptance-gate mapping | Open | Missing platform or command scope blocks merge |
| Review threads | Open | All actionable threads must be resolved |
| Unavailable reviews | None currently observed | Record provider/quota limits as missing, not passed |
| Reviewed head | Open | Final pushed head |
| Merge | Open | Merge commit after technical gates and authorized approval rule |
| Local `main` sync | Open | Prove `HEAD == origin/main` and clean tree |
| Causal closeout | Required | `delivery-closeout.md` after terminal facts exist |
| Duplicate workflow events | N/A | Identify PR-context gate; retain equivalent push runs as noise |

## Retrospective

- **Effective**: The representative F010 slice established validation result, ordered ownership, Red/Green evidence, XML/A11Y proof, and audit-resolution structure before the later slices reused it. Serial evidence and version writes prevented candidate drift.
- **Waste**: Three F013 builds were spent on public test XML documentation placement even though the existing autonomous runbook already requires a compile-surface check. Two later shell failures came from one-off proof-command composition and did not expose repository defects.
- **Recurring blocker**: None. The XML-documentation miss is non-compliance with an existing rule, not evidence of a missing preset rule. The shell mistakes are not repeated repository-helper defects.
- **Recommended refinement**: Provisional `NoPromotion`. Re-evaluate after remote gates and reviews; create no TuiVision or Home-Baseline branch without a reproduced, non-empty correction.

## Pull Request Description

### Summary

- Close audit findings F010-F013 with bounded dialog validation, phase-aware input validation, mode-aware file outcomes, and allowlisted UI-description persistence.
- Reconcile the Feature-024 machine-readable audit to 139 modern sources, 211 public types, and 13 unique finding resolutions while keeping Wave 5 and Wave 6 blocked through Feature 028.
- Add bilingual learner guidance, complete XML documentation, exact evidence, statistics, and next-intake/agent synchronization without changing examples, dependencies, workflows, or historical sources.

### Validation

- 748/748 full Release tests and 748/748 canonical coverage tests.
- Core 92.96%, Controls 86.66%, Serialization 90.01%, Compatibility 80.55%, Drivers.Console 89.18% line coverage.
- `dotnet format`, `git diff --check`, DocFX 0/0, Playwright/Axe 2/2, UTF-8 Lynx 3/3, seven-preset/agent parity, Bash/PowerShell secret scans, and protected-scope scans passed.

### Follow-ups

- Feature 028 is the sole next intake and must revalidate the combined 025/026 closure before either example wave.
- Historical binary-resource compatibility, destructive file operations, arbitrary runtime activation, and consumer application porting remain explicit non-goals, not hidden follow-up defects.
- Remote CI/review/merge facts belong to the causal `delivery-closeout.md` because writing them to this reviewed feature head would invalidate that head.
