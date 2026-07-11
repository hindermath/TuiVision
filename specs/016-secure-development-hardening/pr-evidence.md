# PR-Evidenz: Secure Development Hardening / PR Evidence: Secure Development Hardening

**Run-ID**: `016-secure-development-hardening`
**Datum / Date**: 2026-07-11
**Bindende Eingabe / Binding input**: `Lastenheft_Secure-Development-Hardening.md`
**Status**: In Bearbeitung / In progress

## Zweck und Scope-Grenze / Purpose and Scope Guard

Diese Evidenz verbindet jede Sicherheitsentscheidung mit einer Quelle, einem
Ergebnis und einer überprüfbaren Grenze. Kleine und mittlere, reversible
Repository-Korrekturen sind erlaubt. Rechtsentscheidungen, Credentials,
irreversible Provider-Änderungen und breite Architekturänderungen bleiben
Human-only oder Follow-up.

*This evidence links every security decision to a source, result, and
reviewable boundary. Small and medium reversible repository fixes are allowed.
Legal decisions, credentials, irreversible provider changes, and broad
architecture changes remain human-only or follow-up.*

## Erlaubte Status / Allowed Statuses

| Status | Bedeutung / Meaning | Pflichtgrenze / Required boundary |
|---|---|---|
| `Applicable` | Gilt und benötigt Umsetzung oder Evidenz. / Applies and needs implementation or evidence. | Direkte Evidenz und Ergebnis / Direct evidence and result |
| `AlreadySatisfied` | Aktuelle Evidenz erfüllt den Punkt bereits. / Current evidence already satisfies the control. | Aktueller Evidenzpfad / Current evidence path |
| `N/A` | Gilt unter den dokumentierten Fakten nicht. / Does not apply under documented facts. | Begründung und Neubewertungstrigger / Rationale and re-evaluation trigger |
| `Open` | Benötigt eine noch offene Human-/Provider-/Rechtsentscheidung. / Needs an unresolved human/provider/legal decision. | Owner, Priorität, Risiko, Aktion, Trigger / Owner, priority, risk, action, trigger |
| `FollowUp` | Fachlich relevant, aber außerhalb von 016. / Relevant but outside 016. | Benannter Folgeumfang / Named follow-up boundary |

## Befehlsprotokoll / Command Ledger

| Run | Befehl / Command | Scope | Plattform / Platform | Ergebnis / Result | Evidenz / Evidence | Fehlergrenze / Failure boundary |
|---|---|---|---|---|---|---|
| V-001 | `git branch --show-current`; ancestry; feature JSON | Active feature | macOS | PASS | Branch `016-secure-development-hardening`, HEAD `4dd32a3`, `e28ce6e` ancestor, feature path correct | A mismatch would stop implementation |
| V-002 | `specify check` | Spec-Kit toolchain | macOS | PASS | Specify CLI 0.8.3 ready; Git, Claude, Codex, Copilot IDE, Junie, opencode and Qwen surfaces detected as applicable/available | Optional unavailable agents do not block repository workflow |
| V-003 | `check-prerequisites.sh --json --require-tasks --include-tasks` | Feature artifacts | macOS | PASS | Feature directory and research/data-model/contracts/quickstart/tasks resolved | Missing task/artifact would stop implementation |
| V-004 | Checklist count scan | Five feature checklists | macOS | PASS | 91/91 items complete; 0 open | Any open item would stop implementation |
| V-005 | Required-input read review | Repo guidance, Constitution, Lastenheft, feature and GSDB artifacts | macOS | PASS | All required paths readable; 12 checklist files present | Material governance conflict would stop implementation |
| V-006 | Tool/version baseline | Local execution boundary | macOS 26.5 arm64 | PASS | .NET 10.0.301, Bash 3.2.57, PowerShell 7.6.3, Git 2.50.1, gh 2.96.0, DocFX 2.78.5 | Later validation records tool-specific failure separately |
| V-007 | Control ID/schema comparison | 12 source checklists vs. `control-assessment.md` | macOS | PASS | 157 source IDs, 157 rows, 0 missing, 0 duplicate, 0 unknown; every row has 15 fields | Status evidence is refreshed by later implementation tasks |
| V-008 | Targeted negative-count tests before remediation | Serialization boundary | macOS | EXPECTED FAIL | 0/2 passed; both loaders accepted negative counts | Red proof only; version `1.16.4.61` |
| V-009 | Targeted negative-count tests after remediation | Serialization boundary | macOS | PASS | 2/2 passed | Targeted proof; full suite/coverage follows; version `1.16.4.62` |
| V-010 | Local CycloneDX restore/generation/JSON assertion | `TuiVision.sln` | macOS | PASS | CycloneDX 6.2.0; spec 1.7; 21 components; 22 dependency nodes; output deleted | Local/advisory freshness only; no provenance claim |
| V-011 | Vulnerable/deprecated/outdated package review | 26 solution projects | macOS | PASS with documented updates | 0 vulnerable, 0 deprecated; production/examples current; newer MSTest/Coverlet test tooling available but not required | Configured source URLs/credentials are deliberately omitted |
| V-012 | Ruby YAML parse, immutable `uses:` scan, placeholder/generated-output scan | Workflows, Dependabot, security docs | macOS | PASS | 9 workflows + Dependabot parse; 0 mutable `uses:`; 0 accepted placeholders; 0 tracked BOM | Provider execution remains CI proof |
| V-013 | Rename contract before implementation | Bash/PowerShell scripts | macOS | EXPECTED FAIL | Missing Bash help contract stopped first assertion; version `1.16.4.63` | Red test-first proof |
| V-014 | Rename contract after implementation | 2 implementations x 9 contract outcomes | macOS | PASS | 18 assertions passed; version `1.16.4.65` | Remote Linux/macOS/Windows matrix follows in CI |
| V-015 | Bash syntax, PowerShell parser, workflow YAML, man source review | Repository tooling | macOS | PASS | Syntax/parser/YAML clean; `mandoc` unavailable, semantic source review used | CI executes the behavioral contract |

## Findings

| FindingId | Controls | Pfade / Paths | Schwere / Severity | Beschreibung / Description | Auswirkungsgrenze / Impact boundary | Disposition | Owner | Reviewer | ReviewDate | Akzeptanz / Acceptance | Restrisiko / Residual risk | Follow-up | Trigger |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| F-001 | CL-01, CL-02, CL-04, CL-05 | `docs/security/` | Medium | Neun Indexeinträge bzw. mehrere Dokumente sind noch als unbefüllte Stubs markiert. / Nine index entries or documents still claim unpopulated stub status. | Positive Sicherheitsclaims sind nicht projektweit belastbar. / Positive claims are not project-wide evidence. | Remediate | Maintainer | Codex | 2026-07-11 | Keine akzeptierte Stub-Markierung bleibt. / No accepted stub marker remains. | Low after consolidation | T036-T048, T064 | Neue projektweite Evidenzvorlage / New project-wide evidence template |
| F-002 | CL-05-08, CL-10-08 | `.github/workflows/` | Medium | Verwendete Actions tragen veränderliche Major-Tags. / Used Actions have mutable major tags. | CI-Lieferkette kann ohne Repository-Diff wechseln. / CI supply chain may change without repository diff. | Remediate | Maintainer | Codex | 2026-07-11 | Alle `uses:` sind immutable gepinnt. / All `uses:` are immutable-pinned. | Low | T076-T080 | Neue/aktualisierte Workflow-Action |
| F-003 | CL-08-10, CL-10-17 | `scripts/rename-lastenheft.*` | Medium | Rename erzwingt einen Commit und kann fremde staged Änderungen aufnehmen. / Rename forces a commit and may include unrelated staged changes. | Repository-Historie und Index-Isolation. / Repository history and index isolation. | Remediate | Maintainer | Codex | 2026-07-11 | Test-first NoCommit/preview/path-isolated commit parity. | Low | T089-T101 | Änderung des Archivierungsvertrags |
| F-004 | CL-06-01..CL-06-06 | `SECURITY.md` | Medium | Keine auffindbare Disclosure-Richtlinie im Root. / No discoverable root disclosure policy. | Meldende kennen keinen privaten, koordinierten Pfad. / Reporters lack a private coordinated path. | Remediate | Maintainer | Codex | 2026-07-11 | Bilinguale `SECURITY.md` mit privatem Advisory-Pfad. | Low; organizational ownership remains Open | T065-T066 | Änderung des Reporting-Kanals |
| F-005 | CL-01-03, CL-05-01, CL-05-08 | `.config/`, `.github/dependabot.yml` | Medium | Kein gepinnter lokaler SBOM-Generator und keine Update-Automation. / No pinned local SBOM generator or update automation. | Release-Transparenz und Dependency-Freshness. | Remediate | Maintainer | Codex | 2026-07-11 | Clean-checkout CycloneDX und Dependabot-Konfiguration. | Low | T071-T080 | Tool-/Paketoberfläche ändert sich |
| F-006 | CL-08-01, CL-08-08, CL-08-12 | `TResourceFile.Load`, `THelpIndex.LoadFrom` | Medium | Negative persistierte Counts wurden als leere Collections akzeptiert. / Negative persisted counts were accepted as empty collections. | Malformed resource/help input could bypass explicit rejection. | Remediate | Maintainer | Codex | 2026-07-11 | Two red tests then explicit `InvalidDataException`; targeted rerun PASS. | Low | None | New persisted count reader is added |
| F-007 | CL-08-04, CL-10-11 | `docfx.json`, `docs/secure-development/README.md` | Low | Vorhandene Secure-Development-Ressourcen und die Constitution lagen außerhalb des DocFX-Publishing-Satzes; zwei Verzeichnislinks hatten kein konkretes HTML-Ziel. / Existing secure-development resources and the constitution were outside the DocFX publishing set; two directory links lacked concrete HTML targets. | Generated documentation emitted 51 link warnings. | Remediate | Maintainer | Codex | 2026-07-11 | Include existing resources/content and use concrete index targets; repeat DocFX with zero warnings. | Low | None | DocFX content/resource topology changes |
| F-008 | CL-08-10, CL-10-17 | `tests/scripts/rename-lastenheft-tests.sh` | Medium | Git Bash übergab den MSYS-Pfad `/d/...` unverändert an Windows PowerShell. / Git Bash passed the MSYS `/d/...` path unchanged to Windows PowerShell. | Windows contract CI could not resolve `Get-Help` or later `-File` targets. | Remediate | Maintainer | Codex | 2026-07-11 | Convert only the PowerShell script path with `cygpath -w` on Windows; preserve Unix paths elsewhere. | Low | None | Bash-hosted PowerShell invocation changes |

## Remediation

| RemediationId | FindingId | Controls | Pfade / Paths | Klasse / Class | Scope-Begründung / Scope rationale | Verhaltenswirkung / Behaviour impact | Kommentarprüfung / Comment review | ValidationIds | Ergebnis / Result | Restrisiko / Residual risk |
|---|---|---|---|---|---|---|---|---|---|---|
| R-006 | F-006 | CL-08-01, CL-08-08, CL-08-12 | `src/TuiVision.Serialization/TResourceFile.cs`, `THelpIndex.cs`, `tests/.../SerializationCoverageSweepTests.cs` | Code/Test | Two local guards, reversible, persistence-compatible for valid data | Malformed negative counts now fail explicitly; valid behavior unchanged | CommentNeeded: two 2-line German-first/English-second why blocks | V-008, V-009 | PASS | Low |
| R-002 | F-002 | CL-05-08, CL-10-08 | `.github/workflows/*.yml` | CI | Pin current major-alias commits without changing workflow semantics | CI dependency resolution becomes immutable | CommentAdequate: readable alias comments identify intended channel | V-012 | PASS | Low |
| R-005 | F-005 | CL-01-03, CL-05-01, CL-05-08 | `.config/dotnet-tools.json`, `.github/dependabot.yml`, `security-supply-chain.yml` | Tool/CI | Pinned local tool and read-only repository workflow; no runtime package | Adds reproducible SBOM and update review only | CommentNeeded: two-line temporary-output rationale in workflow | V-010, V-011, V-012 | PASS | Low |
| R-003 | F-003 | CL-08-10, CL-10-17 | `scripts/rename-lastenheft.*`, `tests/scripts/`, `docs/man/`, homogeneity workflow | Script/Test/CI/Docs | Repository-local, reversible contract hardening | Default commit preserved; preview/NoCommit/path isolation added | CommentNeeded: short bilingual commit-isolation rationale | V-013, V-014, V-015 | PASS | Low |
| R-007 | F-007 | CL-08-04, CL-10-11 | `docfx.json`, `docs/secure-development/README.md` | Docs/Config | Existing tracked resources and constitution only; no content or runtime expansion | Generated links/resources become complete | NoCommentNeeded: declarative file lists and concrete links are self-explanatory | V-028 | PASS | Low |
| R-008 | F-008 | CL-08-10, CL-10-17 | `tests/scripts/rename-lastenheft-tests.sh` | Test/CI | Host-path adaptation only; production scripts and contract semantics stay unchanged | Windows PowerShell receives a native path while macOS/Linux keep POSIX paths | CommentNeeded: two-line German-first/English-second platform constraint | V-034 and renewed Windows CI | PASS locally | Low until remote Windows rerun |

## Evidenzartefakte / Evidence Artifacts

| EvidenceId | Pfad oder Befehl / Path or command | Typ / Type | Controls | FreshnessDate | Owner | Reviewer | Generated | Retention | Ergebnis / Result | Grenze / Limit |
|---|---|---|---|---|---|---|---|---|---|---|
| E-001 | `docs/secure-development/checklisten/CL_*.md` | Policy source | CL-01..CL-12 | 2026-07-11 | Project | Codex | No | Tracked | 157 unique headings; 0 duplicates | Explanatory text is not an extra control |
| E-002 | `docs/security/control-assessment.md` | Assessment | CL-01..CL-12 | 2026-07-11 | Maintainer | Codex | No | Tracked | Schema and 12-checklist baseline created | Complete rows follow in T023-T034 |
| E-003 | `.github/workflows/*.yml` inventory | CI | CL-05, CL-10 | 2026-07-11 | Maintainer | Codex | No | Tracked | Eight workflows; mutable action tags identified | Provider settings are not proven by YAML |
| E-004 | `src/`, `tests/` inventory | Review | CL-02, CL-04, CL-08 | 2026-07-11 | Maintainer | Codex | No | Tracked | 141 production and 140 test project/source files in named boundaries | Inventory is not a source-review result |
| E-005 | `scripts/rename-lastenheft.sh`, `.ps1` | Review | CL-08, CL-10 | 2026-07-11 | Maintainer | Codex | No | Tracked | Both use `git mv` then unconditional `git commit` | Behavior proof follows in disposable repos |
| E-006 | `.gitignore` and tracked-file scan | Configuration | CL-05, CL-10 | 2026-07-11 | Maintainer | Codex | No | Tracked | artifacts, TestResults, coverage, `_site`, generated `api` ignored; no prohibited tracked output found | Explicit BOM naming is rechecked after generation |

## Foundation-Inventar / Foundation Inventory

- Control source: 157 unique `CL-XX-NN` headings; per-list counts are
  12/13/15/10/13/11/12/13/17/17/12/12; duplicates: 0.
- Security evidence: 12 existing project files including ADR index; nine index
  entries still say `Stub`; C3A, C5, regulatory, control matrix, and root
  disclosure surfaces were missing at baseline.
- Source boundaries: Core 21 files/1,561 C# lines; Controls 76/10,001;
  Serialization 24/1,921; Drivers 11/383; Compatibility 9/355; tests
  140 project/source files/14,429 C# lines.
- Workflows: eight YAML files; `actions/*`, Gitleaks, and Anthropic actions use
  mutable major tags except the already SHA-pinned release-please action.
- Agents: all five maintained surfaces contain 016 context; the four manual
  surfaces have one start/end marker pair; 28 templates and six installed
  presets were inventoried.
- Historical boundary: `tv203s/` is read-only and `N/A` for implementation
  because 016 does not port or change Turbo Vision behavior. Re-evaluate if a
  concrete source finding requires historical intent.
- Bounded-remediation test: a change must be repository-local, reversible,
  testable, architecture-compatible, and free of unsupported external claims.

## Source-/Test-Review / Source and Test Review

| Boundary | Review result | Evidence/proof boundary |
|---|---|---|
| `TuiVision.Core` | No new finding | Managed buffers/events validate dimensions and kinds; full tests remain final proof |
| `TuiVision.Controls` | No new finding | File/dialog paths expose metadata and controlled state; no arbitrary content write was found |
| `TuiVision.Serialization` | F-006 found and remediated | Existing malformed-input tests plus new negative-count tests |
| `TuiVision.Drivers.Console` | No new finding | Managed frame snapshots and bounded dimensions; platform fallbacks remain test scope |
| `TuiVision.Compatibility` | No new finding | Small managed fallback surface; no native/unsafe/process/network boundary |
| `examples/` | No new finding | I/O is controlled metadata/fixtures or commented illustration; no new example work |
| Six test projects | One negative-count proof gap closed | Deterministic fixtures/temp directories and negative paths reviewed |
| Scripts/workflows | F-002/F-003 confirmed | Immutable pins and rename test-first remediation follow in US3/US4 |

## Finding-Abschluss / Finding Closure

| Severity | Found | Remediated | Accepted human/follow-up boundary | Unresolved merge-blocking |
|---|---:|---:|---:|---:|
| Critical | 0 | 0 | 0 | 0 |
| High | 0 | 0 | 0 | 0 |
| Medium | 7 | 7 | 0 | 0 |
| Low | 1 | 1 | 0 | 0 |

All implementation changes map to F-001 through F-007. The only executable
runtime change is F-006's malformed-input rejection; valid persistence and
public API signatures remain unchanged. Package versions and example scope did
not change. Human/provider/legal `Open` controls are governance decisions, not
unresolved technical critical/high findings.

## Kontrollstatus / Control Status

| Status | Count |
|---|---:|
| `Applicable` | 65 |
| `AlreadySatisfied` | 13 |
| `N/A` | 38 |
| `Open` | 36 |
| `FollowUp` | 5 |
| **Total** | **157** |

All `Open` rows are human/platform/legal decisions with owner, medium priority,
residual risk, concrete action, and trigger. The five `FollowUp` rows cover
release provenance, reproducible build/lock maturity, and an RFC 9116 web path;
they do not represent an unresolved critical/high technical risk.

## Supply-Chain-Ergebnis / Supply-Chain Result

- Local tool: CycloneDX 6.2.0 in `.config/dotnet-tools.json`.
- BOM proof: CycloneDX 1.7 JSON, 21 components, 22 dependency nodes, temporary
  output deleted, zero tracked BOM.
- Packages: zero vulnerable/deprecated packages; only non-blocking newer test
  tooling was observed and left unchanged.
- Actions: every `uses:` line is a full SHA with its readable original alias.
- Automation: Dependabot covers NuGet, Actions, and web-A11Y npm;
  `security-supply-chain.yml` uses read-only permissions and no new secret.
- VEX: `N/A` until a known vulnerability exists. SLSA/reproducible build/NuGet
  lock maturity: named `FollowUp`. Scorecard publication/provider posture:
  applicable with Human-only provider boundary. AI-SBOM: `N/A` until product AI.

## Governance

| RunId | PresetName | PresetVersion | Checkpoint | Status | Rationale | EvidencePath | Owner | Reviewer | ReviewDate | Result | RiskPriority | ResidualRisk | FollowUp | ReevaluationTrigger | HumanOnly |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| 016 | security-governance | 0.6.0 | NIST SSDF | `Applicable` | Mandatory Level-2 SDLC frame | `control-assessment.md`; `pr-evidence.md` | Maintainer | Codex | 2026-07-11 | Applied to review/remediation | None | Low | None | SDLC/governance change | No |
| 016 | security-governance | 0.6.0 | CWE Top 25 / C# secure coding | `Applicable` | Mandatory Level-2 defect review | Source/test review; F-006 | Maintainer | Codex | 2026-07-11 | One malformed-input gap closed | None | Low | None | New input/I/O/parser code | No |
| 016 | security-governance | 0.6.0 | OWASP ASVS | `N/A` | No web/API/HTTP/auth/session scope | `docs/security/asvs-verification.md` | Maintainer | Codex | 2026-07-11 | N/A with facts | None | Low | None | Web/API/auth scope enters product | No |
| 016 | security-governance | 0.6.0 | Owned cryptography | `N/A` | No project-owned crypto, keys, signing, TLS, or trust anchors | `security-checklist.md`; CL-03 rows | Maintainer | Codex | 2026-07-11 | N/A with facts | None | Low | None | Crypto/key/signing scope enters product | No |
| 016 | security-governance | 0.6.0 | SBOM | `Applicable` | Public release-capable framework | `.config/dotnet-tools.json`; `supply-chain-evidence.md` | Maintainer | Codex | 2026-07-11 | CycloneDX 1.7, 21 components | None | Low | None | Dependency/release/tool change | No |
| 016 | security-governance | 0.6.0 | VEX | `N/A` | No known vulnerable evaluated/shipped component | Package V-011; `supply-chain-evidence.md` | Maintainer | Codex | 2026-07-11 | N/A at evidence date | None | Low | None | Any vulnerability finding | No |
| 016 | security-governance | 0.6.0 | SLSA/reproducible build | `FollowUp` | Release pipeline emits no attestable provenance contract | `supply-chain-evidence.md` | Release maintainer | Codex | 2026-07-11 | Follow-up named | Medium | Provenance maturity incomplete | Release-provenance hardening | Distributable artifact pipeline is hardened | No |
| 016 | security-governance | 0.6.0 | OpenSSF Scorecard | `Applicable` | Public OSS posture is relevant | Immutable workflows; API no-result note | Repository owner | Codex | 2026-07-11 | Local posture improved; publication Open | Medium | Provider visibility incomplete | Human review of publication/settings | Provider policy/release review | Yes |
| 016 | security-governance | 0.6.0 | AI-SBOM | `N/A` | AI is development tooling only | `supply-chain-evidence.md`; CL-05-13 | Maintainer | Codex | 2026-07-11 | N/A with trigger | None | Low | None | Models/services/datasets/inference assets delivered | No |
| 016 | security-governance | 0.6.0 | Regulatory screening | `Applicable` | Constitution requires explicit screening | `regulatory-applicability.md` | Maintainer | Codex | 2026-07-11 | NIS2/AI Act/DORA/DPIA decided; CRA Open | Medium | CRA role human-owned | Human legal decision | Market placement/customer handover | Yes |
| 016 | architecture-governance | 0.5.0 | STRIDE/CIA/CAPEC | `Applicable` | Local runtime/tooling trust boundaries exist | `threat-model.md` | Maintainer | Codex | 2026-07-11 | Current model with mitigations | None | Low | None | New trust boundary/incident | No |
| 016 | architecture-governance | 0.5.0 | arc42 security concepts | `Applicable` | Level-2 architecture evidence | `arc42-security.md` | Maintainer | Codex | 2026-07-11 | Current project baseline | None | Low | None | Material architecture change | No |
| 016 | architecture-governance | 0.5.0 | S-ADR | `AlreadySatisfied` | 016 changes evidence/tooling, not architecture | `docs/security/adr/README.md` | Maintainer | Codex | 2026-07-11 | Trigger recorded; no artificial ADR | None | Low | None | Trust/auth/crypto/cloud/release architecture decision | No |
| 016 | architecture-governance | 0.5.0 | OWASP SAMM | `Applicable` | Long-lived Level-2 repository | `samm-assessment.md` | Maintainer | Codex | 2026-07-11 | Lightweight maturity/priorities recorded | None | Low | None | Periodic review/incident | No |
| 016 | architecture-governance | 0.5.0 | Zero Trust | `N/A` | No distributed service, remote identity, or control plane | `zero-trust-applicability.md` | Maintainer | Codex | 2026-07-11 | N/A with local-boundary distinction | None | Low | None | Distributed/cloud/remote-managed scope | No |
| 016 | architecture-governance | 0.5.0 | BSI C3A | `N/A` | No product cloud service/provider dependency | `cloud-autonomy-applicability.md` | Maintainer | Codex | 2026-07-11 | N/A with trigger | None | Low | None | Cloud service selected/operated | No |
| 016 | architecture-governance | 0.5.0 | BSI C5 | `N/A` | No product cloud assurance/shared responsibility | `cloud-compliance-assurance.md` | Maintainer | Codex | 2026-07-11 | N/A with trigger | None | Low | None | Cloud service/shared responsibility enters scope | No |
| 016 | isaqb-architecture-governance | 0.2.0 | Goals and context | `Applicable` | Security goals/context are project-wide | `arc42-security.md`; `plan.md` | Maintainer | Codex | 2026-07-11 | Goals and boundaries explicit | None | Low | None | Architecture scope change | No |
| 016 | isaqb-architecture-governance | 0.2.0 | Runtime/deployment view | `Applicable` | Local process and CI distinction matters | `arc42-security.md`; `threat-model.md` | Maintainer | Codex | 2026-07-11 | Text-first flow documented | None | Low | None | New deployment/service | No |
| 016 | isaqb-architecture-governance | 0.2.0 | Quality scenarios | `Applicable` | Security acceptance needs measurable stimuli/results | `security-quality-scenarios.md` | Maintainer | Codex | 2026-07-11 | 10 measurable scenarios | None | Low | None | New risk/path | No |
| 016 | isaqb-architecture-governance | 0.2.0 | Decisions, risks, technical debt | `Applicable` | Follow-ups and human-only risk must stay visible | ADR index; finding/control ledgers | Maintainer | Codex | 2026-07-11 | Risks/debt/ADR trigger explicit | Medium | Named release/provider debt remains | Follow-up rows | Decision/risk state changes | No |
| 016 | a11y-governance | 0.4.0 | Bilingual/text-first evidence | `Applicable` | Security evidence is learner-facing | A11Y review section; `docs/security/` | Maintainer | Codex | 2026-07-11 | Source review PASS | None | Low | None | Learner-facing content changes | No |
| 016 | a11y-governance | 0.4.0 | WCAG 2.2 AA generated HTML | `Applicable` | `docs/security/` is DocFX content | T136-T137; V-028/V-029 | Maintainer | Codex | 2026-07-11 | DocFX 0/0; Playwright/axe 2/2; UTF-8 Lynx samples readable | None | Low after generated proof | None | DocFX content/template change | No |
| 016 | a11y-governance | 0.4.0 | Didactic inline comments | `Applicable` | New non-trivial guards/scripts/workflow need why/proof explanation | Source/script/workflow diff | Maintainer | Codex | 2026-07-11 | 5/5 blocks within 1-3 lines | None | Low | None | New/changed non-trivial logic | No |
| 016 | cross-platform-governance | 0.2.0 | Bash/PowerShell behavior | `Applicable` | Critical archive tool has paired scripts | V-013..V-015; script contract table | Maintainer | Codex | 2026-07-11 | 18 assertions PASS locally | None | Low | None | Script contract/platform change | No |
| 016 | cross-platform-governance | 0.2.0 | Help/man/preview/error semantics | `Applicable` | User-facing CLI must be equivalent and safe | Scripts; `docs/man/rename-lastenheft.1` | Maintainer | Codex | 2026-07-11 | Syntax/parser/source review PASS | None | Low | None | CLI option/behavior change | No |
| 016 | cross-platform-governance | 0.2.0 | OS matrix | `Applicable` | Repository CI supports Linux/macOS/Windows | `homogeneity-check.yml` | Maintainer | Codex | 2026-07-11 | Local macOS PASS; remote matrix pending | Medium | Remote OS execution pending | CI validation | Runner/tool availability changes | No |
| 016 | agent-parity-governance | 0.3.0 | Five maintained surfaces | `Applicable` | Shared context must stay aligned | Agent files; T102/T133/T139 | Maintainer | Codex | 2026-07-11 | Existing 016 context reviewed | None | Low | None | Shared guidance/context changes | No |
| 016 | agent-parity-governance | 0.3.0 | Context refresh | `Applicable` | Plan changed technology context | Four update-agent-context runs | Maintainer | Codex | 2026-07-11 | Codex/Claude/Gemini/Copilot refreshed | None | Low | None | Plan technology/structure changes | No |
| 016 | agent-parity-governance | 0.3.0 | `.specify/templates/` impact | `N/A` | No repository template defect or shared rule change found | Template inventory; T103 | Maintainer | Codex | 2026-07-11 | N/A with trigger | None | Low | None | Template defect/shared rule change | No |

## Human-only und Follow-up / Human-only and Follow-up

| Item | Status | Owner | Priorität / Priority | Risiko / Risk | Begründung / Rationale | Konkrete Aktion / Concrete action | Neubewertung / Re-evaluation |
|---|---|---|---|---|---|---|---|
| CRA market placement/product class | `Open` Human-only | Human legal/release owner | Medium | Unsupported legal claim | Repository facts cannot determine legal role or market placement | Decide before commercial/EU conformity claim | Commercial distribution or customer handover |
| Vulnerability response SLA/organizational owner | `Open` Human-only | Human security maintainer | Medium | Response expectations remain non-binding | `SECURITY.md` supplies a private route but cannot appoint an organization | Approve owner and response targets | Formal response process or incident |
| GitHub vulnerability alerts/Scorecard publication | `Open` Human-only | Repository owner | Medium | Provider posture not fully automated | Settings/publication are external provider mutations | Review and enable through repository settings if accepted | Provider policy or release-readiness review |
| Agent sandbox/network/model approval | `Open` Human-only | Agent platform owner | Medium | Session/platform controls vary outside Git | Repository files cannot prove host isolation or network policy | Supply platform evidence and approval | Agent/runtime/model/platform change |

## Script-Vertrag / Script Contract

| CaseId | Scenario | Bash | PowerShell | Exit | Dateisystem / Filesystem | Index | CommitDelta | Ausgabebedeutung / Output meaning | Result |
|---|---|---|---:|---:|---|---|---:|---|---|
| C-01 | Help | `--help` | `Get-Help` | 0 | unchanged | unchanged | 0 | usage/syntax | PASS |
| C-02 | Missing input | no args | no parameters | non-zero | unchanged | unchanged | 0 | explicit error | PASS |
| C-03 | Invalid/untracked/unsafe | `--no-commit` | `-NoCommit` | non-zero | unchanged | unchanged | 0 | rejection before mutation | PASS |
| C-04 | Preview | `--dry-run` | `-WhatIf` | 0 | unchanged | unchanged | 0 | target shown | PASS |
| C-05 | Commit-free rename | `--no-commit` | `-NoCommit` | 0 | renamed | rename staged | 0 | no commit | PASS |
| C-06 | Explicit commit | default | default | 0 | renamed | unrelated staging preserved | +1 | isolated rename commit | PASS |
| C-07 | Unrelated staged content | default | default | 0 | unrelated file unchanged | remains staged | +1 rename only | isolation | PASS |
| C-08 | Branch normalization | `codex/demo` | `codex/demo` | 0 | target uses `codex-demo` | rename staged | 0 | normalized target | PASS |
| C-09 | Idempotence | archived source | archived source | 0 | unchanged | unchanged | 0 | informative no-op | PASS |

The same Bash-driven harness executes both tools in disposable repositories.
The homogeneity workflow runs it on Ubuntu, macOS, and Windows runners.

## Agent-/Template-Review / Agent and Template Review

- All five maintained agent surfaces already carry the shared secure-coding,
  didactic-comment, preset, and feature-016 technology context.
- The plan context refresh was executed for Codex, Claude, Gemini, and Copilot
  after planning. Implementation did not change the planned technology set.
- `.specify/templates/` and installed presets are `N/A` for implementation
  changes: no repository-owned template defect was found.
- Final active-context synchronization and parity review remain T133/T139 so
  completion metadata is not asserted early.

## A11Y-/Didaktik-Review / A11Y and Didactic Review

- 16 project security Markdown files use semantic headings, tables/lists, and
  text status values; the reviewed set contains 307 table lines, 55 headings,
  and three balanced fenced-code blocks.
- No pointer-only/color-only instruction, unsupported certification/conformity
  claim, unbalanced fence, or trailing whitespace was found.
- Security evidence is German-first/English-second where learner-facing; dense
  control rows preserve bilingual source titles and rationales.
- Six new non-trivial inline comment blocks were reviewed: two serialization
  guards, Bash/PowerShell commit isolation, temporary workflow output, and the
  Windows PowerShell path boundary. All six are German-first/English-second,
  explain why/proof boundary, and use two lines (100% within the 1-to-3-line target).
- The sample trace for `CL-05-01` remains understandable without layout or
  color and explicitly separates SBOM inventory from vulnerability/provenance.
- Generated samples `_site/docs/security/control-assessment.html` and
  `_site/docs/project-statistics.html` passed semantic UTF-8 Lynx review;
  Playwright/axe passed 2/2 before generated output was deleted.

## Validierung / Validation

| ValidationId | Befehl oder Review / Command or review | Trigger | Scope | Version | Platform | Result | Zusammenfassung / Summary | Retention | Fehlergrenze / Failure boundary |
|---|---|---|---|---|---|---|---|---|---|
| V-016 | Control-ID/schema/status/evidence-path validator | Final control baseline | 12 source checklists and 157 assessment rows | N/A | macOS | PASS | 157/157 unique IDs; 15/15 fields per row; 0 missing, duplicate, unknown, empty, or invalid-status rows; counts 65/13/38/36/5; 15 unique repository evidence paths resolve | Command summary only | A missing path or malformed row blocks acceptance |
| V-017 | Placeholder, stale-status, and unsupported-claim scan | Accepted security evidence | `SECURITY.md`, `docs/security/` | N/A | macOS | PASS | No actionable stub, placeholder, empty starter row, stale feature-only status, or unsupported positive assurance claim; negative disclaimers and checklist labels are intentional | Command summary only | Any unsupported positive claim blocks acceptance |
| V-018 | Git tracking/generated-output/historical-source scan | Retention boundary | Worktree and tracked files | N/A | macOS | PASS | No `_site`, generated API YAML, TestResults, coverage, BOM, package report, cache, log, credential, or `tv203s/` change detected | No generated output retained | Prohibited tracked output or historical-source edit blocks acceptance |
| V-019 | Version alignment review | Pre-validation commit boundary | `Directory.Build.props` | `1.16.4.65` | macOS | PASS | Three version fields aligned; next feature commit patch is 4 and build counter was not incremented | Tracked version metadata | A later build/test must increment only the build counter first |
| V-020 | `dotnet format --verify-no-changes` | Final formatting | Solution | `1.16.4.65` | macOS | PASS | Exit 0; no formatter changes required | Console summary only | Any formatter diff blocks acceptance |
| V-021 | `git diff --check`; Markdown marker/fence scan; Ruby YAML parse; `bash -n`; PowerShell parser | Final structural validation | Changed files, 9 workflows, Dependabot, paired rename scripts and test harness | `1.16.4.65` | macOS | PASS | Diff, Markdown, YAML, Bash, and PowerShell syntax clean; initial Ruby helper call was unsupported locally, corrected parser invocation passed all files | Console summary only | A structural or parser error blocks acceptance |
| V-022 | NuGet vulnerable/deprecated/outdated checks and temporary CycloneDX generation | Final supply-chain refresh | `TuiVision.sln` against nuget.org | `1.16.4.65` | macOS | PASS with documented updates | 0 vulnerable, 0 deprecated; 125 repeated outdated rows across 21 unique test-tool/transitive packages; CycloneDX 1.7 with 21 components and 22 dependency nodes; temporary output deleted | No package report or BOM retained | Security/compatibility-triggered update would require bounded remediation; configured source credentials are omitted |
| V-023 | `dotnet test TuiVision.sln --configuration Release` | Full regression | Six test projects | `1.16.4.66` | macOS | PASS | 498 passed, 0 failed, 0 skipped: Serialization 20, Core 44, Compatibility 18, Drivers 37, Controls 288, Examples Smoke 91 | Build output ignored | Any failure blocks acceptance |
| V-024 | `xmllint --noout coverlet.runsettings`; canonical Coverlet collector test | 70% coverage gate | Five required production assemblies | `1.16.4.67` | macOS | PASS | 498 tests passed; Core 89.78%, Controls 84.84%, Serialization 88.44%, Compatibility 80.55%, Drivers.Console 81.70%; all exceed 70% | `TestResults` deleted after extraction | Example smoke project lacks collector but is outside the five-assembly gate; any required assembly below 70% blocks merge |
| V-025 | Active-context and next-step parity scan | Accepted implementation | Five maintained agent surfaces and `Pflichtenheft.md` | N/A | macOS | PASS | All five surfaces record completed 016 and next intake; exactly one next-step marker points to Wave-1 visual remediation; 015/016 checkboxes complete | Tracked guidance | Final post-generation parity is repeated in T139 |
| V-026 | `rename-lastenheft.sh --no-commit` with HEAD/commit-count comparison | Completion archive | Binding 016 Lastenheft | N/A | macOS | PASS | Renamed to `Lastenheft_Secure-Development-Hardening.016-secure-development-hardening.md`; HEAD and total commit count remained unchanged at `4dd32a3`/406 | Tracked staged rename | Any implicit commit or unrelated staged path blocks acceptance |
| V-027 | Repository statistics recount and structural review | Completed implementation phase | `docs/project-statistics.md` | N/A | macOS | PASS | Snapshot: 20,766 production lines, 14,047 C# test lines, 176,872 documentation lines, 211,685 total; 80/125-line baselines and phase-18 diagrams refreshed; chronological ledger preserved and `Gesamtstatistik` remains final top-level section | Tracked statistics | Recount after later material scope change |
| V-028 | `docfx docfx.json` initial and remediated rerun | Changed DocFX Markdown | 221 generated HTML pages plus existing PDF/SHA/JSON resources | `1.16.4.67` | macOS | PASS | Initial build exposed 51 missing-publishing-target warnings; bounded F-007 remediation added existing resources/constitution and concrete index links; rerun succeeded with 0 warnings and 0 errors | `_site/` and generated API YAML remain ignored and are deleted after A11Y proof | Any remaining warning/error blocks generated-doc acceptance |
| V-029 | `npm run test:docfx`; explicit Ruby HTTP fallback plus Playwright/axe and UTF-8 Lynx | Changed generated documentation | Landing, TView API, statistics, and security control matrix | `1.16.4.68` | macOS | PASS with documented local fallback | `npm run test:docfx` rebuilt DocFX 0/0 but Python 3.14 webServer timed out in `getfqdn`; explicit loopback Ruby server then ran Playwright/axe 2/2 PASS; Lynx samples preserved headings, skip text, bilingual control/status content, and statistics | `_site`, generated API YAML, Playwright reports, and test output deleted | A test/axe failure blocks acceptance; local server-launch limitation does not alter generated content |
| V-030 | Bash and PowerShell agent-secret scans; Gitleaks staged-diff and full-directory scans | Final secret boundary | Tracked/untracked worktree and agent directories | `1.16.4.68` | macOS | PASS | 0 High; PowerShell and both Gitleaks modes clean; Bash classified local untracked `.claude/settings.local.json` as the expected Medium platform-permission file and six prompt/template/empty directories as Low | No scan report retained; values redacted | CI repeats history/provider boundary; local platform settings remain untracked and human-owned |
| V-031 | Five-surface policy/context/marker parity scan | Final agent synchronization | AGENTS, Claude, Gemini, Copilot instruction and Copilot agent files | N/A | macOS | PASS | All five contain 157-control, 498-test, human-only, evidence, and next-intake facts; four Spec-Kit marker pairs and one manual marker pair are balanced; all referenced paths exist | Tracked guidance only | Generator changes require a repeated parity check |
| V-032 | Post-implementation Analyze plus final version/status/generated-output/historical-source/scope scan | PR evidence completion | 66 requirements, 148 tasks, 47 changed paths | `1.16.4.68` | macOS | PASS | Analyze found no new issue; T001-T142 complete except delivery action T143 onward; patch 4 and all three version fields align; diff check clean; no generated report/output, credential, cache/log, dependency/example, or `tv203s/` change remains | Only source-controlled feature scope retained | Remote CI/review may require bounded follow-up commits |
| V-033 | `git push --set-upstream` and `gh pr create` | Remote delivery | Branch and PR #33 | `1.16.4.68` | GitHub | PASS | Initial remote HEAD matched local implementation commit; PR #33 created from the final evidence summary with `main` as base | Remote Git/PR metadata | Delivery metadata commit is pushed next and CI/review convergence remains T146 |
| V-034 | Windows Homogeneity CI log review; `bash -n`; local rename contract rerun | Actionable CI remediation | Bash-hosted PowerShell path boundary | `1.16.5.69` | GitHub Windows log plus macOS local | PASS | Both Windows jobs failed because `Get-Help` received an MSYS `/d/...` path; bounded `cygpath -w` normalization added; syntax clean and 18/18 local contract assertions pass; both renewed Windows jobs pass | CI logs remain remote; no local report retained | Re-evaluate on Git Bash/PowerShell path behavior changes |
| V-035 | `gh pr checks --watch` plus thread-aware PR review reads | CI/review convergence | PR #33 at `b35fe94` | `1.16.6.69` | GitHub | PASS with external review boundary | Ubuntu/macOS build-test, DocFX, Supply Chain, Linux/macOS/Windows tooling, Gitleaks, secrets, and Claude review all pass; 0 review threads/comments; Copilot reported user quota exhaustion rather than a code finding | Remote check/review metadata | Human approval rule remains the only merge-state block and may require authorized admin bypass |
| V-036 | Admin merge of PR #33; `git fetch --prune`; local `main`/`origin/main` equality and clean-status checks | Final delivery | PR #33 and merge commit `5f22dbc` | `1.16.7.69` | GitHub and macOS | PASS | Admin bypass applied only to unavailable Human approval after all checks passed and no actionable review remained; remote feature ref deleted; local clean `main` exactly matched `origin/main` | Durable Git/PR history | None for feature 016 delivery |

## Anforderungs-Traceability / Requirement Traceability

| Requirement | Tasks | Evidenzziel / Evidence destination | Result |
|---|---|---|---|
| FR-001..FR-010 | T001-T011, T020-T035, T133-T140 | Feature evidence and control matrix | Mapped |
| FR-011..FR-018 | T014-T022, T036-T070 | `docs/security/`, findings, validation | Mapped |
| FR-019..FR-027 | T065-T088, T115-T138 | Supply chain, disclosure, governance | Mapped |
| FR-028..FR-033 | T089-T114, T119-T120, T133, T139 | Script, agent, A11Y evidence | Mapped |
| FR-034..FR-036 | T019, T087, T124, T133-T148 | Retention, statistics, delivery | Mapped |
| CR-001..CR-009 | T002-T007, T020, T115-T120, T133-T140 | Constitution/preset evidence | Mapped |
| CR-010..CR-016 | T038-T048, T073-T088, T121-T138 | Applicability and validation | Mapped |
| SC-001..SC-005 | T023-T070, T122-T123, T140 | Coverage, finding, no-stub proof | Mapped |
| SC-006..SC-009 | T071-T105, T128, T138-T139 | SBOM, package, script, agents | Mapped |
| SC-010..SC-014 | T004, T106-T114, T122-T148 | Analyze, tests, A11Y, delivery | Mapped |

## Preflight-Ergebnis / Preflight Result

- Ausgangsversion / Initial version: `1.16.3.60` in all three repository fields.
- Branch-Commits vor Implementierung / Branch commits before implementation: `3`.
- Ausgangsstatus / Initial status: only `tasks.md` and the newly created
  `pr-evidence.md` were modified by implementation start.
- Governance-Konflikt / Governance conflict: none found after `4dd32a3`.
- Generated-output rule: SBOM, `_site/`, generated API YAML, TestResults,
  coverage, caches, logs, credentials, and temporary scans remain untracked.
- Stop conditions: credentials, legal decisions, irreversible provider changes,
  scope impossibility, and an unremediated critical risk stop autonomous work.

## Task-Fortschritt / Task Progress

| Phase | Tasks | Completed | Conditional outcome |
|---|---:|---:|---|
| Setup und Foundation / Setup and foundation | T001-T022 | 22 | Complete |
| US1 Audit-Basis / Audit baseline | T023-T050 | 28 | Complete after supply-chain evidence consolidation |
| US2 Bounded Remediation | T051-T070 | 20 | Complete |
| US3 Supply Chain | T071-T088 | 18 | Complete |
| US4 Cross-platform und Agents | T089-T105 | 17 | Complete; final active-context parity is repeated in T133/T139 |
| US5 A11Y und Didaktik | T106-T114 | 9 | Complete |
| Governance und Validierung / Governance and validation | T115-T140 | 26 | Complete |
| Remote Delivery | T141-T148 | 8 | Complete |

## Finale PR-Zusammenfassung / Final PR Summary

Feature 016 establishes the durable TuiVision secure-development baseline and
closes every repository-actionable finding discovered in the bounded review.

- **Control decisions**: 157/157 rows, comprising 65 `Applicable`, 13
  `AlreadySatisfied`, 38 `N/A`, 36 human-only `Open`, and 5 `FollowUp`.
- **Findings**: eight found and eight remediated; seven Medium and one Low; zero
  unresolved Critical, High, Medium, or actionable Low findings.
- **Technical changes**: explicit rejection of negative persisted counts,
  immutable workflow dependencies, pinned CycloneDX plus dependency automation,
  root vulnerability disclosure guidance, safe Bash/PowerShell archive parity,
  and warning-free DocFX resource publication.
- **Validation**: formatting, diff, Markdown, YAML, Bash, PowerShell, package,
  SBOM, script-contract, secret, agent-parity, and generated-document checks
  pass; full Release tests are 498/498 and all five coverage gates exceed 70%.
- **A11Y**: DocFX builds 221 HTML pages with 0 warnings/0 errors; Playwright/axe
  passes 2/2 and UTF-8 Lynx samples preserve semantic text-first content.
- **Governance**: 30 rows across all six presets resolve to 20 `Applicable`, 8
  `N/A`, 1 `AlreadySatisfied`, and 1 `FollowUp`; each has complete evidence,
  ownership, risk, result, and re-evaluation fields.
- **Residual boundaries**: legal, provider, organization, and agent-platform
  decisions remain explicitly human-owned; release provenance, reproducible
  build/lock maturity, and RFC 9116 remain named follow-ups.
- **Retention and scope**: 47 changed paths; no generated DocFX/API/test/coverage/
  SBOM output, credential, cache, log, or `tv203s/` edit is retained.
- **Delivery state**: T001-T148 are complete; PR #33 is merged, the remote
  feature branch is deleted, and local `main` was synchronized cleanly.
