# PR Evidence: Wave-3 Visual Component Porting

**Branch**: `019-wave3-visual-component-porting`
**Feature directory**: `specs/019-wave3-visual-component-porting`
**Binding intake**: `Lastenheft_Wave3-Visual-Component-Porting.md`
**Delivery mode**: `MergeAndSync`
**Authority source**: User instruction authorizes feature, retrospective,
Home-Baseline package, adoption PRs, merge/sync, and a narrowly bounded admin bypass

## Scope

### Included

- Five visible .NET examples: `BHelp`, `HelpDemo`, `I18n`, `TvEdit`, `TvHc`.
- Three-layer runtime, real app-loop proof, controlled I/O, five guides, and evidence.
- Proportional six-preset governance and authorized GitHub delivery.

### Excluded

- Wave-4 terminal/charset/emulation work and mandatory mouse interaction.
- TP7 follow-on examples, proprietary Borland `.tch` decoding, broad framework revision.
- Arbitrary user-data access, new dependencies, generated output, and `tv203s/` edits.

## Run Gates

| Phase | Attempt | Result | Evidence | Remaining action |
|---|---:|---|---|---|
| Preflight | 1 | Pass | `specify check`; prerequisite JSON; 70/70 checklist items | None |
| Specify | 1 | Pass | `spec.md`; requirements checklist 18/18 | None |
| Clarify | 2 | Pass | `spec.md` Clarifications | None |
| Checklists | 3 | Pass | domain 19/19; plan quality 13/13; plan review 20/20 | None |
| Plan | 1 | Pass | `plan.md`, research, model, quickstart, contract | None |
| Tasks | 1 | Pass | 109 unique sequential tasks | None |
| Analyze | 1 | Remediated | CLI-start, push-version, and causal commit/merge evidence wording | Re-run complete |
| Analyze | 2 | Pass | FR 32/32; CR 14/14; SC 13/13; tasks 109/109; remote paths 10/10; C/H/M 0 | None |
| Implement | 1 | Closed | T001-T088 complete | Five examples, proof matrix, guides, governance, routing, and statistics complete |
| Validate | 1 | Closed | T089-T098 complete | Diff, format, tests, coverage, DocFX/A11Y, lynx, secrets, hygiene, and archive pass |
| Deliver | 1 | Open | Remote Delivery table | Run after local completion |

`speckit-taskstoissues` is `N/A`: one dependency-ordered feature PR is the
accepted delivery unit; 109 remote issues would add state without proof value.

## Preflight Evidence

| Check | Result | Evidence |
|---|---|---|
| Branch | Pass | `019-wave3-visual-component-porting`; `origin/main` is an ancestor |
| Feature selector | Pass | `.specify/feature.json` points to this feature directory |
| Toolchain | Pass | `specify check`: CLI ready |
| Prerequisites | Pass | Feature directory and research/model/contracts/quickstart/tasks returned |
| Checklists | Pass | requirements 18, domain 19, plan quality 13, plan review 20; incomplete 0 |
| Constitution | Pass | v1.14.0; no material post-Analyze conflict |
| Presets | Pass | security 0.6.0 p10; architecture 0.5.0 p20; iSAQB 0.2.0 p30; A11Y 0.4.0 p40; cross-platform 0.2.0 p50; agent parity 0.3.0 p60 |
| Historical tree | Pass | Reviewed read-only; `git diff -- tv203s/` empty |

## Feature 018 Reuse

| Contract area | Existing component | 019 use | Reopening boundary |
|---|---|---|---|
| Editor/file | `TFileEditor`, `TEditWindow` | Visible editor composition and controlled file proof | Focused failing framework test only |
| Help | `THelpFile`, `THelpTopic`, `THelpWindow` | Topic, context, navigation, fallback | No proprietary decoder |
| Compiler | `THelpSourceCompiler` | Controlled source, diagnostics, topic model | No local parser |
| Localization | `TLocalizedResourceLookup` | Explicit language/fallback state | No ambient locale/gettext |
| Persistence rejection | 018 hardened serializers | Negative supplemental proof | No duplicate hardening |

## Example Acceptance Matrix

| Example | Historical intent | Main surface | Status/description | Framework decision | Decision rationale | Current result |
|---|---|---|---|---|---|---|
| `TvEdit` | Minimal app built on reusable editor application behavior | Real `TFileEditor` in `TEditWindow` | Buffer/modified state; Help/Description | `UseExistingFramework` | Feature 018 editor/file components are sufficient | Pass: 4/4 focused |
| `BHelp` | Topic viewer, search/context selection, navigation, unavailable help | Modern `THelpWindow` and controlled topics | Context/topic; Help/Description | `IntentionalDeviation` | Learner-visible viewer intent retained; proprietary unchecked `.tch` decoder omitted | Pass: 2/2 focused |
| `HelpDemo` | Focus contexts, status hints, help commands, visible dialog/topic | Focusable controls plus modern help view/result | Context/hint; Help/Description | `UseExistingFramework` | Current view, status, command, and help APIs are sufficient | Pass: 2/2 focused |
| `I18n` | Visible Spanish translation of framework/menu text | Localized resource panel/dialog | Requested/matched language; Help/Description | `UseExistingFramework` | Feature 018 deterministic lookup replaces ambient gettext safely | Pass: 2/2 focused |
| `TvHc` | `.topic` compiler, contexts, references, diagnostics, help output | Source/compiler/result window | Source/result; Help/Description | `UseExistingFramework` | Feature 018 compiler and help model carry the contract | Pass: 2/2 focused |

## Primary Visual Proof

| ProofId | Example | Test method | App-loop route | Concrete state | View-tree kind | Rendered region | Status proof | Description proof | Helper usage | Result | Proof boundary |
|---|---|---|---|---|---|---|---|---|---|---|---|
| W3-P01 | `TvEdit` | `TvEdit_AppLoop_Edits_Visible_Buffer_And_Status` plus safe-close/owned-save tests | Key edit, close, save, and description commands through `Run()` | Text changed; modified; close rejected/accepted; traversal rejected | `TEditWindow`, editor, and description `TWindow` | `X` in editor plus description cells | Buffer identity, modified, close/save result | Help command renders description | `SetupOnly` for test-temp root | Pass | Embedded content and test-owned root only; normal shutdown is outside close-dispatch assertion |
| W3-P02 | `BHelp` | `BHelp_AppLoop_Navigates_Visible_Topics` plus fallback test | Topic/context commands through `Run()` | Navigation topic and context-999 fallback | `THelpWindow` plus description `TWindow` | Topic title/body and description cells | Context/topic | Help command opens description | `None` | Pass | No proprietary `.tch` decoding |
| W3-P03 | `HelpDemo` | `HelpDemo_AppLoop_Changes_Focus_Context_And_Shows_Help` plus fallback test | Focus/help commands through `Run()` | Context 102, Cancel hint/topic, context-999 fallback | Focus controls, `THelpWindow`, description `TWindow` | Control/topic and description cells | Context hint | Help command opens description | `None` | Pass | Keyboard focus only; mouse is Feature 020 |
| W3-P04 | `I18n` | `I18n_AppLoop_Shows_Neutral_Spanish_And_Fallback_States` plus description test | Language and description commands through `Run()` | Neutral, Spanish, missing-key, and missing-language fallback | Localized `TWindow` plus description `TWindow` | `Window`, `Ventana`, fallback, and description cells | Requested/matched language, key, and fallback | Help command renders host-independent description | `None` | Pass | Managed explicit lookup, not gettext or ambient host locale |
| W3-P05 | `TvHc` | `TvHc_AppLoop_Compiles_Visible_Topic_And_Rejects_Invalid_Source` plus persist/description test | Compile, persist, rejection, and description commands through `Run()` | Success topic, stable diagnostic, no partial result, owned output, traversal rejection | Compiler/result `TWindow` plus description `TWindow` | Success/rejected result and description cells | Source/result/rejection | Help command renders description | `SetupOnly` for test-temp output root | Pass | Embedded source and test-temp-only persisted proof; no arbitrary user path |

## Controlled Artifacts

| ArtifactId | Example | Ownership | Path or identity | Access | Cleanup | Proof boundary | Result |
|---|---|---|---|---|---|---|---|
| W3-A01 | `TvEdit` | Embedded | Initial learner buffer | Read/write in process | App disposal | Normal startup reads no user file | Pass |
| W3-A02 | `TvEdit` | SourceControlled | External fixture not required | N/A | None | Embedded learner buffer is sufficient; no repository or user file read | N/A |
| W3-A03 | `TvEdit` | TestTemp | Unique test directory | WriteNew/explicit overwrite decision | Test finally block | Prefix check rejects `../escape.txt`; no write outside owned root | Pass |
| W3-A04 | `TvHc` | Embedded | `.topic` learning source | ReadOnly | None | Bounded known input; invalid source yields stable diagnostics and no partial result | Pass |
| W3-A05 | `TvHc` | TestTemp | Unique test output | WriteNew only within normalized owned root | Test finally block | Prefix check rejects `../escape.hlp`; rejected compile has no accepted partial output | Pass |

## Historical Intent

| Modern area | Historical source | Intent retained | Intentional deviation | Proof boundary |
|---|---|---|---|---|
| BHelp file/viewer | `tv203s/contrib/tvision/examples/bhelp/bhelp.cc`, `bhelp.h`, `thelp.cc` | Topic viewer, context/search selection, navigation, unavailable help | Modern safe `THelpFile`; no proprietary binary decoder | Viewer intent only |
| HelpDemo | `tv203s/contrib/tvision/examples/helpdemo/helpdemo.cc` | Focusable controls, help contexts, hints, command dispatch | Current control/help APIs and bilingual description | Keyboard flow; no mouse obligation |
| I18n | `tv203s/contrib/tvision/examples/i18n/test.cc`, `test.po`, `es.po`, `README`, `extract.sh` | Visible alternate translation and fallback | Explicit managed lookup; no `LANG`, gettext, or extraction script runtime | Exact dictionaries only |
| TvEdit | `tv203s/contrib/tvision/examples/tvedit/tvedit.cc` | Minimal app demonstrates reusable editor framework | Controlled learner buffer and explicit safe-close proof | No arbitrary user file discovery |
| TvHc | `tv203s/contrib/tvision/examples/tvhc/tvhc.cc`, `tvhc.h`, `demohelp.txt` | Topic syntax, symbols, references, diagnostics, help result | Feature 018 bounded compiler; no global native buffers | Accepted syntax subset only |

## Governance Applicability

| Preset | Version | Checkpoint | Applicability | Rationale | Evidence path | Owner | Reviewer | Review date | Result | Residual risk | Follow-up | Re-evaluation trigger |
|---|---|---|---|---|---|---|---|---|---|---|---|---|
| security-governance | 0.6.0 | NIST SSDF / CWE Top 25 / secure coding | Applicable | Changed C#, controlled paths, parser/resource use | This file; changed code/tests | Codex | Codex self-review plus remote review | 2026-07-12 | Local pass; remote review pending | Residual platform/reviewer risk remains until remote checks | Complete remote proof | Any file/parser change |
| security-governance | 0.6.0 | OWASP ASVS | N/A | No web/API/auth surface | `docs/security/asvs-verification.md`; this file | Codex | Codex | 2026-07-12 | N/A | None identified | None | Web/API/auth enters scope |
| security-governance | 0.6.0 | SBOM / VEX / SLSA / OpenSSF | N/A for new feature artifact | No dependency, package, provenance, or distribution change | `docs/security/supply-chain-evidence.md`; this file | Codex | Codex | 2026-07-12 | Existing baseline | Repository baseline remains external to feature | CI supply-chain check | Package/distribution changes |
| security-governance | 0.6.0 | AI-SBOM | N/A | AI is development tooling only | This file | Codex | Codex | 2026-07-12 | N/A | None identified | None | Runtime model/service/data enters product |
| security-governance | 0.6.0 | NIS2 / CRA / EU AI Act / DORA | N/A | Local training examples, no regulated operated service or product AI | `docs/security/regulatory-applicability.md`; this file | Codex | Codex | 2026-07-12 | N/A | Legal classification remains human-owned | None | Distribution/operation/regulatory status changes |
| architecture-governance | 0.5.0 | STRIDE / CIA / CAPEC | Applicable proportionally | Local untrusted file/source/key boundaries | This file; tests | Codex | Codex self-review plus remote review | 2026-07-12 | Local pass; remote review pending | Only embedded or test-owned inputs are accepted; remote review remains | Complete remote proof | Trust boundary changes |
| architecture-governance | 0.5.0 | S-ADR / arc42 security concepts | N/A | Existing component architecture reused | plan/research/this file | Codex | Codex | 2026-07-12 | N/A | Local helper could drift | Framework gate | Material architecture decision |
| architecture-governance | 0.5.0 | Zero Trust / SAMM | N/A for new artifact | No distributed service or program change | Existing security ledgers; this file | Codex | Codex | 2026-07-12 | N/A | Existing repository maturity unchanged | None | Remote service or program scope changes |
| architecture-governance | 0.5.0 | BSI C3A / BSI C5 | N/A | No cloud provider/service/deployment/audit boundary | Cloud applicability ledgers; this file | Codex | Codex | 2026-07-12 | N/A | None identified | None | Cloud/provider boundary enters scope |
| isaqb-architecture-governance | 0.2.0 | Goals/views/quality/risks/debt | Applicable | Reuse and quality boundary need explicit proof | plan, research, framework matrix | Codex | Codex self-review plus remote review | 2026-07-12 | Pass locally | Linked presentation shell remains example-local by design | Re-evaluate after remote review | Reusable domain logic discovered |
| a11y-governance | 0.4.0 | WCAG 2.2 AA / text-first / bilingual | Applicable | Runtime and five guides are learner-facing | Smokes, guides, DocFX/axe | Codex | Codex plus remote checks | 2026-07-12 | Runtime/text-first pass; DocFX/axe pending | Generated presentation remains until validation | Complete docs/A11Y gates | User-facing surface changes |
| a11y-governance | 0.4.0 | Didactic inline comments | Applicable | New dispatch, fallback, controlled-I/O, proof logic | Changed source/test review | Codex | Codex | 2026-07-12 | Pass | No material residual risk identified | None | Non-trivial logic changes |
| cross-platform-governance | 0.2.0 | Runtime/path portability | Applicable | Temp paths and console rendering vary by OS | Tests and remote CI | Codex | Remote CI | 2026-07-12 | Local pass; remote OS checks pending | Platform differences until CI | Run required OS checks | Runtime/path changes |
| cross-platform-governance | 0.2.0 | Script parity/manpages/help | N/A | No script is added or changed | Final diff; this file | Codex | Codex | 2026-07-12 | N/A | None identified | None | Script enters diff |
| agent-parity-governance | 0.3.0 | Maintained agent surfaces | Applicable | Active feature context advances | Five agent files; hash review | Codex | Codex plus remote scan | 2026-07-12 | Pass locally | Remote scan remains | Recheck in CI | Shared context changes |
| agent-parity-governance | 0.3.0 | `.specify/templates/` | N/A | Generic workflow changes are isolated to later retrospective PR | Final diff; retrospective boundary | Codex | Codex | 2026-07-12 | N/A | Field learning may remain local temporarily | Post-merge retrospective | Generic correction is proven |

## Success Criteria Coverage

| Criterion | Evidence | Result |
|---|---|---|
| SC-001 | [Validation](#validation): five bounded Release CLI starts plus main/status/description proof | Pass |
| SC-002 | [Example Acceptance Matrix](#example-acceptance-matrix) and exact matrix test | Pass |
| SC-003 | [Primary Visual Proof](#primary-visual-proof) plus 14/14 matrix tests at `1.19.0.121` | Pass |
| SC-004 | [TvEdit proof and controlled artifacts](#controlled-artifacts) | Pass |
| SC-005 | [BHelp and HelpDemo app-loop proof](#primary-visual-proof) | Pass |
| SC-006 | [I18n explicit lookup and host-independence proof](#primary-visual-proof) at `1.19.0.118` | Pass |
| SC-007 | [TvHc compiler and controlled-output proof](#controlled-artifacts) at `1.19.0.118` | Pass |
| SC-008 | [Documentation and Governance Review](#documentation-and-governance-review) | Implemented; generated-doc validation pending |
| SC-009 | [Validation](#validation) | Local pass; remote gates tracked under SC-013 |
| SC-010 | [Generated Output Hygiene](#generated-output-hygiene) and final scope diff | Pass |
| SC-011 | [Governance Applicability](#governance-applicability) and unchanged-evidence review | Pass locally; remote rows pending |
| SC-012 | [Run Gates](#run-gates) Analyze pass 2 | Pass |
| SC-013 | [Remote Delivery](#remote-delivery) | Pending |

## Validation

| Command or review | Version/trigger | Result | Evidence or failure boundary |
|---|---|---|---|
| `git diff --check` | Always | Pass | No whitespace errors |
| `dotnet format --verify-no-changes --no-restore` | C# changed | Pass | Exit 0 |
| Focused Wave-3 tests | Every red/green slice | Pass | 14/14 at `1.19.0.122` |
| Full Release tests | New executable projects/shared proof | Pass | `1.19.0.123`: Core 44, Serialization 44, Compatibility 18, Drivers 37, Controls 292, Examples 115; total 550/550 |
| Coverlet coverage | Shared executable additions | Pass | `1.19.0.124`: Core 89.78%, Controls 84.84%, Serialization 89.50%, Compatibility 80.55%, Drivers.Console 81.70% |
| `docfx docfx.json` | Five guides/navigation | Pass | Two builds, each 0 warnings and 0 errors |
| Playwright/axe | After DocFX | Pass | 2/2 Chromium tests |
| UTF-8 `lynx` | New guide | Pass | Generated TvEdit guide preserves skip links, headings, DE/EN text, commands, and proof linearly |
| Secret scans | Always before delivery | Pass | Repository PowerShell scan and cleaned full-directory Gitleaks scan pass |
| Generated-output hygiene | Always | Pass | `_site`, `api`, all `TestResults`, and temporary Gitleaks report removed |

The final local scope scan passed after correcting the scan command itself:
no real clarification/TODO/TBD/placeholder marker, no `src/` or `tv203s/`
diff, and no generated DocFX, API, test, coverage, or log artifact is present.
The first scan attempt was not accepted because its brace expansion named
nonexistent smoke files and matched normative marker wording in the checklist.
The first `lynx` attempt likewise used the wrong generated path
(`_site/guides/...`); the accepted check used
`_site/docs/guides/examples/tvedit.html`.
The first full-directory Gitleaks pass found six generated `_site` matches in
secure-development example text. After the required generated-output cleanup,
the same full-directory command passed with zero findings; no source finding
was suppressed.

## Documentation and Governance Review

- All five guides use German-first/English-second CEFR-B2 sections, semantic
  headings, labelled Bash fences, text equivalents for visible state, and
  keyboard routes. `git diff --check` passes and each guide has balanced fences.
- `docs/security/threat-model.md`, `docs/security/asvs-verification.md`,
  `docs/security/supply-chain-evidence.md`,
  `docs/security/regulatory-applicability.md`, and the cloud/Zero-Trust/SAMM
  ledgers remain unchanged because 019 adds no web, package, cloud, service,
  identity, deployment, or regulated-operation boundary.
- `docs/architecture/architecture-vision.md`, `runtime-view.md`,
  `quality-scenarios.md`, and `architecture-risks.md` remain unchanged because
  the accepted plan and this evidence already classify the linked Wave-3
  presentation shell, exact framework reuse, controlled I/O, and historical
  deviation without changing the framework architecture.
- The byte-identical 019 block in all five maintained agent files has SHA-256
  `6969511696f1229ead6542cc4d63487c1955d2c6c4b5475f6ad6ee36aee9b5d7`.

## Remote Delivery

Pre-commit scope for T100 is limited to Feature 019 artifacts, the five example
projects and shared presentation source, their smoke tests, five guides and
navigation/index updates, the archived Lastenheft, synchronized agent context,
Pflichtenheft/statistics routing, `.specify/feature.json`, and aligned version
`1.19.1.124`. No generated, dependency, `src/`, or `tv203s/` path is staged.

T101 created implementation commit `4606034` (`feat: port Wave 3 visual
components`) with version `1.19.1.124`. T102 recalculated the next branch
commit as `2` and aligned this evidence-only follow-through to `1.19.2.124`
without another build or test.

| Item | Result | Evidence |
|---|---|---|
| Push | Pass | First push observed `origin/019-wave3-visual-component-porting` at `65b1da3`; this T103 evidence commit is aligned to `1.19.3.124` and pushed next |
| Pull request | Open | URL pending |
| Required checks | Open | Remote checks pending |
| Review threads | Open | GraphQL pending |
| Unavailable reviews | None recorded yet | Record quota/provider limitations truthfully |
| Merge | Open | Merge commit pending |
| Remote branch | Open | Cleanup pending |
| Local `main` sync | Open | Clean `HEAD == origin/main` pending |
| Post-merge closeout | Open | Evidence-only if causally required |

## Generated Output Hygiene

| Path/class | Policy | Result |
|---|---|---|
| `_site/`, generated `api/*.yml` | Never track | Pass: removed after DocFX/A11Y review |
| `TestResults/`, coverage, logs, caches | Never track | Pass: removed after coverage evaluation |
| Credentials/agent state | Never track | Pass: PowerShell plus cleaned full-directory Gitleaks |
| `tv203s/` | Read-only | Pass before implementation |

## Versioned Test Runs

| Version | Scope | Expected | Result | Evidence |
|---|---|---|---|---|
| `1.19.0.107` | Focused `TvEditSmokeTests` red boundary | Fail before app implementation | Expected fail | `CS0234`: `TuiVision.Examples.TvEdit` / `TvEditApp` not yet present; all four later project skeletons compiled |
| `1.19.0.108` | Focused `TvEditSmokeTests` with explicit owned-save boundary | Fail before app implementation | Expected fail | Same isolated missing `TvEditApp` boundary; test-temp/traversal contract included before green implementation |
| `1.19.0.109` | Focused `TvEditSmokeTests` first green attempt | Pass | Compile fix required | Test harness used the wrong translator namespace and a nonexistent local assertion wrapper; runtime projects compiled |
| `1.19.0.110` | Focused `TvEditSmokeTests` second green attempt | Pass | Compile fix required | Public MSTest class/methods lacked repository-required XML summaries; runtime and API compiled |
| `1.19.0.111` | Focused `TvEditSmokeTests` first executable attempt | 4 expected green | 2 passed, 2 failed | Visible window existed, but Desktop focus did not target `TEditWindow`; key edit and dependent safe-close proof failed |
| `1.19.0.112` | Focused `TvEditSmokeTests` after focus fix | 4 expected green | 3 passed, 1 failed | Runtime behavior was correct; post-`Run()` Owner assertion observed normal shutdown rather than the close-dispatch boundary |
| `1.19.0.113` | Focused `TvEditSmokeTests` vertical-slice gate | 4 expected green | Pass 4/4 | App-loop state/view/cell/status/description, safe-close, and owned temp-path proof complete |
| `1.19.0.114` | Grouped `BHelpSmokeTests` and `HelpDemoSmokeTests` red boundary | Fail before Help app implementation | Expected fail | `CS0234`: both planned application namespaces/types missing; shared/TvEdit baseline compiled |
| `1.19.0.115` | Grouped `BHelpSmokeTests` and `HelpDemoSmokeTests` green gate | 4 expected green | Pass 4/4 | Topic navigation, focus/context/hint, fallback, view/cell/status, description, and `.tch` deviation proof complete |
| `1.19.0.116` | Grouped `I18nSmokeTests` and `TvHcSmokeTests` red boundary | Fail before app implementation | Expected fail | `CS0234`: both planned application namespaces/types missing; earlier vertical and Help slices compiled |
| `1.19.0.117` | Grouped I18n/TvHc first green attempt | Pass | Compile fix required | `THelpCompilerDiagnostic` is a value type; the diagnostic display incorrectly used the null-conditional operator |
| `1.19.0.118` | Grouped `I18nSmokeTests` and `TvHcSmokeTests` green gate | 4 expected green | Pass 4/4 | Explicit language/key fallback, host independence, compile success, stable rejection, no partial result, owned output, traversal rejection, and description proof complete |
| `1.19.0.119` | First complete Wave-3 matrix attempt | 14 expected green | Compile fix required | Linked `Wave3Runtime.cs` creates an intentional per-example assembly type; the matrix initially treated those copies as one CLR type |
| `1.19.0.120` | Second complete Wave-3 matrix attempt | 14 expected green | Compile fix required | The corrected helper referenced `TApplication` but omitted its `TuiVision.Controls` namespace import |
| `1.19.0.121` | Complete Wave-3 proof matrix | 14 expected green | Pass 14/14 | Five unique framework decisions, app-loop state/view/cell/status/description proof, and five stable `48x16` description layouts passed |
| `1.19.0.122` | Final targeted Wave-3 tests plus bounded Release CLI starts | 14 tests and five starts | Pass | 14/14; BHelp, HelpDemo, I18n, TvEdit, and TvHc each started with `--no-build` and remained active until controlled termination |
| `1.19.0.123` | Full Release test suite | All repository tests | Pass 550/550 | Core 44, Serialization 44, Compatibility 18, Drivers 37, Controls 292, Example Smokes 115 |
| `1.19.0.124` | Canonical Coverlet coverage gate | Five assemblies >=70% | Pass | `xmllint` valid; Core 89.78%, Controls 84.84%, Serialization 89.50%, Compatibility 80.55%, Drivers.Console 81.70%; all 550 tests passed |

## Retrospective

- **Effective**: Grouped red/green boundaries reduced repeated builds for the
  Help and I18n/TvHc slices; exact delivery evidence paths remain unambiguous.
- **Waste**: The TvEdit slice started before its compile-surface checklist was
  complete, and the matrix first assumed one CLR identity for source linked
  into five assemblies. Both caused avoidable build-counter increments.
- **Recurring blocker**: Repository-linked shared example source creates one
  type identity per target assembly. Cross-example proof must use public
  application contracts or state delegates instead of that linked base type.
- **Recommended refinement**: Promote grouped project-local red proof after
  the second successful feature observation, and add a pre-red compile-surface
  check for imports, public XML docs, harness helpers, focus/ownership
  assertions, and linked-source assembly identity.
- **Resume state**: T001-T099 are locally complete with 14/14 targeted and
  550/550 full tests, all five coverage gates, DocFX 0/0, A11Y 2/2, clean
  secret/hygiene scans, and the archived Lastenheft. Delivery starts at T100.
