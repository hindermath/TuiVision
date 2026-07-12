# Autonomous Run Evidence: A11Y Framework

**Branch**: `023-a11y-framework`  
**Feature directory**: `specs/023-a11y-framework`  
**Binding intake**: `Lastenheft_06_A11Y_Framework.md`  
**Delivery mode**: `MergeAndSync`  
**Authority source**: User-approved autonomous Features 018-023 plan

## Scope

### Included

- Opt-in accessible widget text, typed focus payload, structured shortcuts,
  keyboard inventory, High Contrast, reference app and DocFX/Axe proof.

### Excluded

- Native AT bridges, full control migration, terminal-wide WCAG claims, new
  dependencies, Wave 1-4 changes, historical-source edits and Feature 024.

## Run Gates

| Phase | Attempt | Result | Evidence | Remaining action |
|---|---:|---|---|---|
| Preflight | 1 | Pass | clean branch from `3329e59`, `HEAD == origin/main`, `specify check` | None |
| Specify | 1 | Pass | `spec.md`, no clarification markers | None |
| Clarify | 2 | Pass | five fixed decisions; second pass found no material ambiguity | None |
| Checklists | 1 | Pass | four checklists, zero incomplete | None |
| Plan | 1 | Pass | `plan.md` plus research/model/contract/quickstart | None |
| Tasks | 1 | Pass | 148 dependency-ordered sequential tasks | None |
| Analyze | 2 | Pass | 33 FR/SC keys, 148 tasks, no CRITICAL/HIGH/unresolved MEDIUM | None |
| Implement | 1 | Pass | T001-T128 complete; Core, Controls and reference app delivered | Remote delivery |
| Validate | 1 | Pass | 24 targeted, 140 examples, 686 full; coverage/docs/A11Y green | Remote checks |
| Deliver | 1 | Open | pending | PR/review/merge/sync |

`speckit-constitution` is unchanged because the accepted constitution already
covers this feature. `speckit-taskstoissues` is `N/A`: one dependency-ordered
feature PR is the authorized delivery unit.

## Requirement and Proof Coverage

| Area | Requirement / SC | Planned proof | Result |
|---|---|---|---|
| Widget contract | FR-001..002, SC-008 | Core API tests + Release XML build | Pass: 7/7 Core contract tests |
| Focus announcement | FR-003..005, SC-001 | Controls unit/integration tests | Pass: direct, nested, legacy and no-op proofs |
| Shortcuts | FR-006..008, SC-002 | menu/status provider tests | Pass: executable-only immutable snapshots |
| Keyboard inventory | FR-009..010, SC-003 | explicit Controls matrix | Pass: 7/7 families, every cell Proof/N/A |
| High Contrast | FR-011..013, SC-004 | scheme and cell tests | Pass: explicit propagation, text and cells |
| Reference app | FR-014..015, SC-005 | real-loop state/view/cell smoke | Pass: 3/3 standard/description/narrow smokes |
| Docs CI | FR-016..019, SC-007..008 | workflow review + DocFX/Axe/lynx | Pass: existing `pages.yml`, DocFX 0/0, Axe 2/2, lynx |
| Governance | FR-020..024, SC-009 | decision tables and scans | Pass: six presets and all triggers recorded |

## API and Architecture Decisions

| Area | Decision | Rationale | Evidence | Residual risk | Owner | Follow-up or re-evaluation trigger |
|---|---|---|---|---|---|---|
| Widget | `SmallFrameworkFix` | truthful opt-in semantic contract | `research.md#r2---widget-adoption` | partial adoption | Maintainer | migrate controls only with stable labels |
| Focus | `SmallFrameworkFix` | one existing broadcast, typed payload | `research.md#r1---focus-transport` | legacy raw payload consumers | Maintainer | remove compatibility only in breaking release |
| Shortcuts | `SmallFrameworkFix` | immutable provider-owned query | `research.md#r3---shortcut-ownership` | conflicting keys remain contextual | Maintainer | add conflict policy if global aggregation appears |
| High Contrast | `SmallFrameworkFix` | semantic explicit opt-in roles | `research.md#r4---high-contrast` | terminal colour support varies | Maintainer | driver capability API changes |
| Native AT | `FollowUpHardening` | outside managed text-first layer | `spec.md#out-of-scope` | no OS assistive bridge | Maintainer | dedicated platform integration feature |
| PowerShell Homogeneity helper | `FollowUpHardening` | Existing `check-homogeneity.ps1` cannot resolve `Invoke-HgScan`, still exits 0 and targets `~/STATS.md`; unrelated to 023 runtime | observed command output; no TuiVision diff from helper | false-positive automation result | Maintainer | autonomous retrospective/Home-Baseline workitem; fix in script-governance scope |

## Keyboard Inventory

| Control family | Tab | Shift+Tab | Arrows | Enter | Direct shortcut | Evidence / N/A rationale | Result |
|---|---|---|---|---|---|---|---|
| `TButton` | Proof | Proof | N/A: group navigation owns arrows | Proof | Proof | matrix plus existing button regressions | Pass |
| `TInputLine` | Proof | Proof | Proof | N/A: text control does not activate | N/A: no command shortcut | matrix plus input regressions | Pass |
| `TListBox` | Proof | Proof | Proof | N/A: selection confirmation is pointer-specific | N/A: list navigation | matrix plus list regressions | Pass |
| `TMenuBar` | N/A: F10 activation | N/A: F10 activation | Proof | Proof | Proof | F10/arrows/Enter/mnemonic matrix | Pass |
| `TStatusLine` | N/A: passive provider | N/A: passive provider | N/A: passive provider | N/A: passive provider | Proof | explicit-key provider tests | Pass |
| `TDialog` / `TGroup` | Proof | Proof | N/A: child-specific | Proof | N/A: container | matrix plus dialog regressions | Pass |
| Reference accessible widget | Proof | Proof | N/A: group navigation | Proof | Proof | 3/3 real-loop app smokes | Pass |

## Historical Intent

| Modern area | Historical source | Intent retained | Intentional deviation | Proof or N/A rationale |
|---|---|---|---|---|
| Focus ownership | `tv203s/contrib/tvision/classes/tgroup.cc`, `include/tv/group.h` | one current child and ordered transitions | semantic text snapshot and descendant shell propagation are new | read-only review complete; tests are modern proof |
| Menu/status shortcuts | `tv203s/contrib/tvision/classes/tmenubar.cc`, `tstatusl.cc`, relevant headers | keyboard-command identity | structured query API is new | read-only review complete; provider tests are modern proof |
| High Contrast / AT | no direct historical equivalent | N/A | modern inclusion contract | historical code cannot prove WCAG |

## Governance Applicability

| Preset | Version | Checkpoint | Applicability | Rationale | Evidence path | Owner | Reviewer | Result | Residual risk | Follow-up | Re-evaluation trigger |
|---|---|---|---|---|---|---|---|---|---|---|---|
| security-governance | 0.6.0 | NIST SSDF, CWE, bounded text/events | Applicable | public runtime contracts and validation | Core/Controls tests + this ledger | Maintainer | Codex | Pass | custom controls remain semantic trust boundary | suppress blank text; document opt-in | contract change |
| security-governance | 0.6.0 | ASVS, SBOM, VEX, SLSA, OpenSSF, AI-SBOM, NIS2, CRA, EU AI Act, DORA | N/A | no web auth, package, release, AI or regulated service change | final package/diff scans | Maintainer | Codex | Pass | None in accepted scope | re-screen | dependency/distribution/AI/service trigger |
| architecture-governance | 0.5.0 | STRIDE/CIA/CAPEC | Applicable | focus payload and shortcut truth boundary | plan + typed/negative tests | Maintainer | Codex | Pass | spoofed semantic text in custom controls | documented opt-in trust boundary | provider model changes |
| architecture-governance | 0.5.0 | S-ADR, arc42, Zero Trust, SAMM, BSI C3A, BSI C5 | N/A | no deployment, cloud, provider or distributed flow | plan/final diff | Maintainer | Codex | Pass | None in accepted scope | re-screen | topology/cloud/security-program trigger |
| isaqb-architecture-governance | 0.2.0 | cohesion, coupling, reuse | Applicable | existing event and control structures reused | plan/research/final API | Maintainer | Codex | Pass | partial opt-in adoption | inspect public surface | new abstraction |
| a11y-governance | 0.4.0 | keyboard, focus, contrast, text, docs, comments | Applicable | primary feature scope | tests/guide/DocFX/Axe/evidence | Maintainer | Codex | Pass | no native AT | `FollowUpHardening` | platform bridge scope |
| cross-platform-governance | 0.2.0 | managed runtime and script parity | Applicable | colour/keyboard behavior crosses hosts; no new scripts | managed tests/CI and visible fallback | Maintainer | Codex | Pass | physical host colour rendering | remote matrix; no new script | script or host-specific API added |
| agent-parity-governance | 0.3.0 | five agent surfaces and templates | Applicable | active context changes; templates N/A | five equal block hashes; no template diff | Maintainer | Codex | Pass | None | recheck on shared guidance | shared guidance changes |

## Validation

| Command or review | Trigger | Result | Evidence or failure boundary |
|---|---|---|---|
| `git diff --check` | Always | Pass | no whitespace errors |
| `dotnet format --verify-no-changes --no-restore` | C# changes | Pass | exit 0 |
| targeted Core/Controls/example tests | touched projects | Pass | 7 Core + 14 Controls + 3 reference = 24/24 at `1.23.0.186` |
| complete example smokes | reference project added | Pass | 140/140 at `1.23.0.187` |
| full Release suite | shared runtime | Pass | final 687/687 at `1.23.0.194`; earlier 686/686 before popup-role regression |
| canonical Coverlet gate | shared runtime | Pass | Core 90.45, Controls 83.89, Serialization 89.50, Compatibility 80.55, Drivers 89.18 at `1.23.0.195` |
| `docfx docfx.json` | public XML/docs | Pass | 286 models, 0 warnings, 0 errors |
| Playwright/Axe | every DocFX run | Pass | 2/2 Chromium tests |
| `lynx` guide review | learner-facing guide | Pass | UTF-8 text contains A11Y, High Contrast and native boundary |
| secret/generated/tv203s scans | Always | Pass | no packages, generated tracked output or historical diff; final secret scan pending immediately before push |
| repeated final Analyze | before implementation closeout | Pass | 33 FR/SC keys, 148 dependency-ordered tasks, 128/128 local tasks complete; no CRITICAL/HIGH/unresolved MEDIUM finding |
| PowerShell homogeneity helper | optional repository helper | FollowUpHardening | missing `Invoke-HgScan`, misleading exit 0 and wrong default target; not accepted as validation evidence |

## Remote Delivery

| Item | Result | Evidence |
|---|---|---|
| Push | Open | pending |
| Pull request | Open | pending |
| Required checks | Open | pending |
| Review threads | Open | pending |
| Unavailable reviews | None observed | pending |
| Reviewed head | Open | pending |
| Merge | Open | pending |
| Local `main` sync | Open | pending |
| Causal closeout | Open | pre-name `specs/023-a11y-framework/closeout-evidence.md` only if reviewed-head/post-merge facts cannot be causal in feature commit |
| Duplicate workflow events | N/A | classify PR-context checks as gate if observed |

## Retrospective

- **Effective**: One test command per Build increment made later validation causal; the vertical reference slice combined API, keyboard and rendering proof.
- **Waste**: The first targeted command chained a second `dotnet test` under one counter and had to be corrected; solution-wide filters also emitted harmless no-match messages in unrelated test projects.
- **Recurring blocker**: MSTest 4 strict exception helpers differ from older examples; the optional PowerShell homogeneity helper also failed internally while returning success.
- **Recommended refinement**: Promote a runbook/task rule that one version increment owns exactly one explicit `dotnet build` or `dotnet test` invocation. Validation orchestration must also reject error records even when a helper returns exit 0, and must pass an explicit repository root to workspace-aware scripts.
