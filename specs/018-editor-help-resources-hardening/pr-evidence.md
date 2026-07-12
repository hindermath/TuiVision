# Autonomous Run Evidence: Editor, Help, and Resources Hardening

**Branch**: `018-editor-help-resources-hardening`
**Feature directory**: `specs/018-editor-help-resources-hardening`
**Binding intake**: `Lastenheft_03_EditorHelpAndResourcesHardening.md`
**Foundation**: `specs/004-editor-file-help-streams/`
**Delivery mode**: `MergeAndSync`
**Authority**: The user's 2026-07-12 instruction authorizes features 018-023,
own-repository PRs and merges, and a narrow human-approval bypass only with
green required checks and zero actionable threads.

## Scope

Included: coherent editor/file and runtime-help proof, a bounded reusable help-
source compiler, exact language-aware resource lookup, malformed persistence
proof, XML/docs, governance, statistics, archive, and agent-context updates.

Excluded: Wave-3 example ports, mouse, terminal/charset/font work, TP7,
dependencies, broad redesign, external/cloud services, generated output, and
all edits under `tv203s/`.

## Run Gates

| Phase | Attempt | Result | Evidence | Remaining action |
|---|---:|---|---|---|
| Preflight | 1 | Pass | `specify check`; six presets; clean synchronized `main` at `3882297`; exact branch | None |
| Specify | 1 | Pass | `spec.md`; requirements checklist | None |
| Clarify | 2 | Pass | No material question | None |
| Checklists | 2 | Pass | Three checklists; zero incomplete | Re-run after implementation |
| Plan | 2 | Pass | Plan, research, model, contract, quickstart | None |
| Tasks | 2 | Pass | 99 stable sequential tasks; format valid | Execute |
| Analyze | 1 | Accepted | No Critical/High; title, UTF-8, four task-path Medium findings | Remediated |
| Analyze | 2 | Pass | 26/26 FR, 13/13 CR, 10/10 SC; zero Critical/High/Medium; zero unmapped | None |
| Implement | 1 | Pass | T001-T091 | Local implementation complete; delivery tasks remain |
| Validate | 1 | Pass | Validation and Success Criteria tables | None |
| Deliver | 1 | Open | Remote table | PR, review, merge, sync |

`speckit-taskstoissues` is `N/A`: a single dependency-ordered PR is the accepted
delivery unit; creating 99 remote issues would add state without proof value.

## Framework Decisions

| Area | Existing surface | Decision | Rationale/change | Proof | Follow-up trigger |
|---|---|---|---|---|---|
| Editor | `TEditor`, `TMemo` | UseExistingFramework | Coherent edit/search/replace/command proof passes | T048-T056 | Gap exceeds narrow fix |
| File | `TFileEditor`, `TEditWindow` | UseExistingFramework | Safe close/conflict/failure state remains intact; no code change | T048-T056 | Data-loss/platform defect |
| Help | `THelpFile`, viewer/window | SmallFrameworkFix | Controls navigation was sufficient; persisted graph now rejects invalid counts/targets | T026, T057-T064 | Broad graph redesign needed |
| Compiler | No reusable source compiler | SmallFrameworkFix | Shared source-to-runtime contract required | T016-T036 | New grammar beyond accepted subset |
| Resources | Exact `TResourceFile` | SmallFrameworkFix | Reused storage plus duplicate-key/negative-length rejection | T037-T047, T065-T071 | Storage cannot express contract |
| i18n | No deterministic selector | SmallFrameworkFix | Shared fallback contract required | T037-T047 | Gettext/charset/ambient locale required |

Final decision vocabulary is exactly `UseExistingFramework`,
`SmallFrameworkFix`, `IntentionalDeviation`, or `FollowUpHardening`.

## Malformed-State Matrix

| Boundary | Expected | Baseline | Feature action/result |
|---|---|---|---|
| Truncation | Explicit failure; no partial graph | Existing stream/record tests | Mapped/rerun; Pass |
| Trailing data | Explicit failure | Existing stream/resource tests | Mapped/rerun; Pass |
| Unknown type | Explicit failure | Existing `PStreamTests` | Mapped/rerun; Pass |
| Cycle | Explicit rejection | Existing coverage test | Mapped/rerun; Pass |
| Invalid count | Explicit rejection | Existing help/resource tests | Extended; Pass |
| Duplicate persisted key | Explicit rejection | New end-to-end test | SmallFrameworkFix; Pass |
| Invalid help reference | Reject before presentation | New end-to-end test | SmallFrameworkFix; Pass |
| Malformed help source | Diagnostics; no model | New compiler tests | SmallFrameworkFix; Pass |
| Missing resource/language | Distinct missing result | New lookup tests | SmallFrameworkFix; Pass |

Final malformed-state results: truncation, trailing data, unknown type, cycle,
invalid count, duplicate persisted key, negative payload length, invalid or
unresolved Help reference, malformed source, strict encoding failure, and
missing language/resource all have explicit tests. The 44/44 Serialization
run proves atomic rejection or a distinguishable missing result; no partial
catalog, Help graph, compiler model, or symbol map is returned.

## Historical Intent

| Area | Read-only historical sources | Intent retained | Intentional deviation | Proof |
|---|---|---|---|---|
| Editor/file | `classes/teditor.cc`, `teditorf.cc`, `teditwin.cc`; `include/tv/editors.h` | Reusable editor, modified/save/close state | Managed file snapshots and strings | T048-T056 |
| Runtime help | `classes/help.cc`, `helpbase.cc`; `include/tv/help*.h` | Context topics, paragraphs, references, fallback | Managed model, no binary HLP compatibility | T026, T057-T064 |
| Compiler | `examples/tvhc/tvhc.cc`, `tvhc.h`, `demohelp.txt` | `.topic`, symbols, paragraphs, inline and forward references | Unresolved target rejects complete result instead of warning/output | T016-T036 |
| Resources | `include/tv/resource.h` | Named persisted objects | Managed registry/archive | T065-T071 |
| i18n | `doc/I18n.txt`, `examples/i18n/` | Explicit language/domain fallback intent | Exact resource candidates; no gettext, host `LANG`, codepage cache, or `.mo` | T037-T047 |

## Governance Applicability

All rows have review date 2026-07-12, owner TuiVision maintainer, and reviewer
feature PR reviewer or available remote automation.

| Preset | Version | Checkpoint | Applicability | Rationale/evidence | Result | Residual risk / re-evaluation trigger |
|---|---|---|---|---|---|---|
| security-governance | 0.6.0 | NIST SSDF/CWE Top 25 | Applicable | Parser, persistence, path, limits, atomic output; this file/tests | Pass | Malformed-input defect; test failure |
| security-governance | 0.6.0 | ASVS | N/A | No web/API/HTTP/auth | N/A | Re-evaluate if such surface appears |
| security-governance | 0.6.0 | SBOM/VEX/SLSA/OpenSSF | N/A feature-specific | No dependency/package/provenance change; `docs/security/supply-chain-evidence.md` remains | N/A | Re-evaluate on dependency/release change |
| security-governance | 0.6.0 | AI-SBOM | N/A | AI is development tooling only | N/A | Re-evaluate for product/runtime AI |
| security-governance | 0.6.0 | NIS2/CRA/EU AI Act/DORA | N/A feature-specific | No new operated, AI, financial, or regulated boundary; existing regulatory file remains | N/A | Re-evaluate on product/regulatory change |
| architecture-governance | 0.5.0 | STRIDE/CIA/CAPEC | Applicable | Untrusted source/persisted input boundaries; plan/tests | Pass | Parser/resource exhaustion; new trust boundary |
| architecture-governance | 0.5.0 | S-ADR/arc42/SAMM | N/A update | Existing module ownership; bounded quality scenario in plan | N/A | Re-evaluate on architecture boundary change |
| architecture-governance | 0.5.0 | Zero Trust | N/A | No identity/distributed service | N/A | Identity/service boundary |
| architecture-governance | 0.5.0 | BSI C3A/C5 | N/A | No cloud/provider/autonomy/assurance boundary | N/A | Cloud/provider scope |
| isaqb-architecture-governance | 0.2.0 | Goals/views/quality/risks | Applicable | `plan.md`, `research.md` | Pass | Re-evaluate for new project/service |
| a11y-governance | 0.4.0 | Text/keyboard/WCAG | Applicable | Runtime help, diagnostics, XML/docs; DocFX/A11Y | Pass | User-facing path changes |
| a11y-governance | 0.4.0 | DE/EN CEFR-B2 | Applicable | Learner docs and XML | Pass | Learner-facing text changes |
| a11y-governance | 0.4.0 | Didactic comments | Applicable | Non-trivial parser/fallback logic | Pass | Logic changes |
| cross-platform-governance | 0.2.0 | Script parity | N/A | No script change | N/A | Any script diff |
| agent-parity-governance | 0.3.0 | Maintained surfaces | Applicable | Active/completed and next-intake context | Pass | Shared guidance changes |
| agent-parity-governance | 0.3.0 | `.specify/templates/` | N/A | No template change planned | N/A | Intentional template change |

## Baseline Review

Feature 004 already proves isolated editing/search/replace, modified close,
external file conflicts, help lookup/navigation/fallback, exact resource keys,
shared references, truncation, unknown types, cycles, invalid counts, and
trailing data. No current help-source compiler or resource-language selector
exists. Existing contracts are preserved unless an 018 test proves a gap.

Checklist state: requirements 21/21, domain 17/17, plan review 13/13; zero
incomplete.

## Validation

### Test-first baseline

The three new test files define the compiler, localized lookup, editor/file,
and runtime-help contracts before production implementation. The first focused
Serialization run is expected to fail at compilation because
`THelpSourceCompiler` and `TLocalizedResourceLookup` do not yet exist; this is
the observable missing-contract proof, not an accepted product failure.

| Command/review | Trigger | Result | Evidence/boundary |
|---|---|---|---|
| `specify check` and prerequisites | Preflight | Pass | Feature/tasks resolved; six presets present |
| `git diff --check` and scope scans | Always | Open | T082 |
| `dotnet format --verify-no-changes` | C# | Open | T083 |
| Focused Serialization/Controls Release | Changed modules | Open | T019 onward |
| Full Release tests | Shared runtime | Open | T085 |
| Canonical Coverlet >=70% per required assembly | Shared runtime | Open | T086 |
| `docfx docfx.json` | Public API/XML | Open | T087 |
| `tests/web-a11y` | Every DocFX run | Open | T088 |
| Script parity | No script diff | N/A | Re-evaluate on script change |
| Visible example app-loop/buffer | No example port | N/A | Feature 019 trigger |

### Validation log

| Version | Command | Result | Proof boundary |
|---|---|---|---|
| `1.18.0.95` | `dotnet test tests/TuiVision.Serialization.Tests/TuiVision.Serialization.Tests.csproj --configuration Release --no-restore` | Expected Fail | Existing Serialization library built; test compilation failed only because the planned `THelpSourceCompiler` and `TLocalizedResourceLookup` public contracts were absent. This is the red test-first baseline. |
| `1.18.0.96` | Same focused Serialization command | Fail | Production compilation found four char/string overload mismatches in localized-key validation; no tests executed. Corrected before retry. |
| `1.18.0.97` | Same focused Serialization command | Fail (37/38 pass) | Lookup result reported all candidates rather than only keys actually attempted before a match. Corrected the proof boundary to stop at the matched key. |
| `1.18.0.98` | Same focused Serialization command | Pass (38/38) | Compiler vertical slice, malformed source baseline, localized lookup, and resource round-trip all pass. |
| `1.18.0.99` | `dotnet test tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj --configuration Release --no-restore` | Pass (291/291) | Coherent editor/file flow and compiled/persisted help navigation pass using existing Controls runtime code. |
| `1.18.0.100` | Focused Serialization Release command | Expected Fail (38/44 pass) | Six new negative proofs exposed unknown directives, non-UTF-8 BOM acceptance, duplicate keys, negative payload length, negative Help counts, and unresolved persisted references. Narrow fixes applied before retry. |
| `1.18.0.101` | Same focused Serialization command | Pass (44/44) | All compiler, lookup, resource, stream, Help graph, and malformed-state tests pass. |
| `1.18.0.102` | Focused Controls Release command | Pass (292/292) | Editor/file coherence, cancellation/conflict/failure preservation, and compiled/persisted Help navigation remain green after graph validation. |
| N/A | `git diff --check`; scope/placeholder/dependency/generated-output/`tv203s` review | Pass | No forbidden path, dependency, script, generated artifact, placeholder, or historical-source diff. Ignored build outputs remain untracked. |
| N/A | `git diff | gitleaks stdin --config .gitleaks.toml --redact --no-banner` | Pass | 41.98 KB diff scanned; no leaks found. |
| N/A | `dotnet format --verify-no-changes --no-restore` | Pass | No formatting changes required. |
| `1.18.0.103` | Final focused Serialization Release command | Pass (44/44) | Final compiler, lookup, resource, Help, and malformed-state regression set. |
| `1.18.0.104` | Final focused Controls Release command | Pass (292/292) | Final editor/file and runtime-help integration set, including cross-platform save-failure boundary. |
| `1.18.0.105` | `dotnet test --configuration Release --no-restore` | Pass (536/536) | Core 44, Compatibility 18, Drivers 37, Serialization 44, Controls 292, Example smokes 101; zero failed/skipped. |
| `1.18.0.106` | `xmllint --noout coverlet.runsettings`; canonical Coverlet Release command | Pass | Core 89.78%, Controls 84.84%, Serialization 89.50%, Compatibility 80.55%, Drivers.Console 81.70%. Example project lacks collector but is outside the five-assembly gate; all required Cobertura files exist. |
| N/A | `docfx docfx.json` | Pass | 239 models, 0 warnings, 0 errors. Public compiler/lookup XML pages generated. |
| N/A | `cd tests/web-a11y && npm run test:docfx` | Pass (2/2) | DocFX rebuilt with 0 warnings/errors; landing and representative API/statistics pages have no serious axe findings. |
| N/A | `lynx -dump -nolist _site/api/TuiVision.Serialization.THelpSourceCompiler.html` | Pass | Text view exposes bilingual skip links, class heading, summary, constructors, methods, parameters, and returns. |
| N/A | `git clean -fdX -- _site api` | Pass | Generated DocFX output removed; none will be tracked. |
| `1.18.1.106` | Implementation commit | Pass | `5b4e533` (`[Spec Kit] Implement editor help resources hardening`), 30 files, no forbidden staged path. |

## Didactic Comment Review

Five new non-trivial blocks receive bilingual two-line comments: compiler
atomic publication, explicit language policy, missing-versus-empty lookup,
duplicate persisted keys, and deferred Help-reference validation. All five are
within the normal one-to-three-line intensity. Parser mechanics that are clear
from names and tests intentionally remain uncommented; public explanation stays
in XML comments. Decision counts: five `CommentNeeded`, remaining reviewed
clear blocks `NoCommentNeeded`, and no `FollowUpHardening`.

## Success Criteria

| Criterion | Result | Evidence |
|---|---|---|
| SC-001 | Pass | Six framework rows, exactly one final decision each: two `UseExistingFramework`, four `SmallFrameworkFix` |
| SC-002 | Pass | Controls 292/292 covers coherent editor, cancel, conflict, and failed-save preservation |
| SC-003 | Pass | Compiler/resource round-trip plus viewer open/reference/back/fallback proof |
| SC-004 | Pass | Deterministic recompile and all named invalid source diagnostics with no model/symbol map |
| SC-005 | Pass | Exact, ordered fallback, neutral, missing, empty, and case-sensitive lookup tests |
| SC-006 | Pass | Complete malformed-state matrix; Serialization 44/44 |
| SC-007 | Pass | Named reusable editor/help/compiler/resource/i18n contracts; no example-local replacement required |
| SC-008 | Pass | 536/536 full Release; all five assemblies above 70% coverage |
| SC-009 | Pass | Bilingual XML/evidence/guidance, keyboard Help proof, DocFX/axe/lynx |
| SC-010 | Pass | Scope diff has no example, mouse, terminal, charset, dependency, generated, or `tv203s/` change |

## Remote Delivery

| Item | Result | Evidence |
|---|---|---|
| Push | Pass | `origin/018-editor-help-resources-hardening` |
| Pull request | Pass | PR #42: `https://github.com/hindermath/TuiVision/pull/42` |
| Required checks | Open | Await PR |
| Review threads | Open | Await GraphQL review |
| Unavailable reviews | Open | Record quota/provider limitation as missing review |
| Merge | Open | Merge commit required |
| Local `main` sync | Open | Must prove clean `HEAD == origin/main` |

## Retrospective

- **Effective**: Evidence-first plus one compiler-to-runtime vertical slice made
  historical intent, modern deviation, parser boundary, and downstream viewer
  proof reviewable before broad negative hardening.
- **Waste**: The task sequence split closely related Serialization negative
  cases across two focused cycles. Both cycles found real defects, but a future
  generator can group one project's complete red boundary matrix before the
  first green implementation when dependencies permit.
- **Recurring blocker**: None locally. Remote quota/review behavior remains to
  be measured.
- **Recommended refinement**: `PresetFollowUp` with `ObserveAgain`: teach task
  generation to batch project-local red boundaries and require every delivery
  task to name its evidence path. The latter already prevented four malformed
  remote tasks during Analyze remediation.
- **Resume state**: Local implementation and all triggered validation gates are
  complete. Implementation commit `5b4e533` and evidence commit `1b723fd`
  exist; PR #42 is open. Next step is remote review convergence, merge/sync,
  then the 018 Home-Baseline workitem.
