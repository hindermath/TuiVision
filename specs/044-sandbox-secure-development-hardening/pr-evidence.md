# Autonomer Laufnachweis: Sandbox-Secure-Development-Hardening / Autonomous Run Evidence: Sandbox Secure Development Hardening

**Branch**: `044-sandbox-secure-development-hardening`
**Feature-Verzeichnis / Feature directory**: `specs/044-sandbox-secure-development-hardening`
**Verbindlicher Intake / Binding intake**: `requirements/intakes/active/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md`
**Liefermodus / Delivery mode**: `MergeAndSync`
**Autorität / Authority source**: Aktuelle Benutzeranweisung vom 30. August 2026 einschließlich eng begrenztem Admin-Bypass. / Current user instruction of 30 August 2026 including narrowly scoped admin bypass.

## Umfang / Scope

### Enthalten / Included

- Nachweisbare Entscheidung, wie TuiVision mit `absdd-image-sandbox` sicher und ausbildungsgeeignet bearbeitet werden kann. / Evidence-backed decision on how TuiVision can be worked on safely and accessibly with `absdd-image-sandbox`.
- Dokumentation von Mounts, Schreibgrenzen, Toolchain-Fähigkeit, Secret-Grenzen sowie lokaler, Sandbox- und CI-Verantwortung. / Documentation of mounts, write boundaries, toolchain feasibility, secret boundaries, and local, sandbox, and CI responsibilities.
- Nur findings-basierte, nicht leere Folge-Intakes. / Finding-derived, non-empty follow-up intakes only.

### Ausgeschlossen / Excluded

- Keine Runtime-, API-, Paket-, Beispiel- oder Sandbox-Image-Änderung. / No runtime, API, package, example, or sandbox-image change.
- Keine Secrets, privaten Hostpfade, Benutzerprofile oder unbelegte Sicherheitsbehauptungen. / No secrets, private host paths, user profiles, or unsupported security claims.
- Kein Start eines Folgefeatures. / No start of a follow-up feature.

## Laufgates / Run Gates

| Phase | Versuch / Attempt | Ergebnis / Result | Evidence | Restaktion / Remaining action |
|---|---:|---|---|---|
| Preflight | 1 | Pass | sauberer `main`; aktueller Review `847bce5c-98b0-4461-b2a7-c1b5bc9d83dc`; Routing `Aligned` | None |
| Specify | 1 | Pass | `spec.md` | None |
| Clarify | 1 | Pass | `spec.md` | keine planungsrelevante Unklarheit / no planning-material ambiguity |
| Checklists | 1 | Pass | `checklists/` | 22/22 Anforderungen und 20/20 Planpunkte / requirements and plan items |
| Plan | 1 | Pass | `plan.md`; `research.md`; `data-model.md`; `quickstart.md`; `contracts/` | None |
| Tasks | 1 | Pass | `tasks.md` | 75 eindeutige sequenzielle Aufgaben / unique sequential tasks |
| Analyze | 1 | Pass | `analysis.md` | null Critical, High oder Medium / zero Critical, High, or Medium |
| Implement | 1 | Pass | Dokumentation und deterministische Nachweise / documentation and deterministic proof | nur Delivery- und Abschlussaufgaben offen / only delivery and closeout tasks remain |
| Validate | 1 | Pass | Befehlsledger unten / command ledger below | None |
| Deliver | 1 | In progress | [PR #159](https://github.com/hindermath/TuiVision/pull/159) | Remote-Nachweise konvergieren / converge remote evidence |

## Entscheidungen und Folgearbeiten / Decisions and Follow-ups

| Bereich / Area | Entscheidung / Decision | Begründung / Rationale | Evidence | Restrisiko / Residual risk | Owner | Follow-up oder Neubewertung / Follow-up or re-evaluation trigger |
|---|---|---|---|---|---|---|
| Sandbox-Anwendbarkeit | `ConditionallyUsable` | Technische Isolation und Toolchain sind geeignet, wenn nur der TuiVision-Checkout schreibbar ist; formelle Freigabe bleibt offen. / Technical isolation and toolchain are usable with a narrow writable checkout; formal approval remains open. | `docs/security/secure-development/2026-08-29-sandbox-applicability/assessment.json` | Offene Freigabe, Egress-, Provider- und Plattformgrenzen. / Open approval, egress, provider, and platform boundaries. | TuiVision Maintainer | Image, Mounts, Datenklasse, Provider, Netzwerk oder Plattform-Evidence ändern sich. |
| Mount-Policy | Narrow project mount | Private Roots und fremde Repositories bleiben ausgeschlossen. / Private roots and unrelated repositories remain excluded. | `mount-policy.md` | Lokale Environment-Werte können technisch zu breit gesetzt werden. / Local environment values can still be too broad. | Session operator | Vor jeder Sitzung lokale Compose-Konfiguration prüfen. |
| Execution-Policy | Split proof levels | Statische, praktische und Plattform-Evidence werden nicht vermischt. / Static, practical, and platform evidence remain separate. | `execution-matrix.md` | Kein frischer Image-Lauf in Feature 044. / No fresh image run in Feature 044. | Maintainer | Image oder Toolchain ändern sich. |
| Agent-Guidance | `NoUpdateRequired` | Bestehende Secret-, Scope-, Authority- und Agent-Paritätsregeln decken die Entscheidung ab. / Existing rules already cover the decision. | `AGENTS.md`; `.specify/templates/` | None | Maintainer | Eine neue gemeinsame Regel entsteht. |
| Folge-Intake | None | Die offenen Punkte gehören zur externen Sandbox-/Human-Governance und sind kein neuer TuiVision-Produktbefund. / Open items belong to external sandbox or human governance, not a new TuiVision product finding. | `assessment.json` | Offene Entscheidungen bleiben sichtbar. / Open decisions remain visible. | Named owners in assessment | Erst bei reproduzierbarem, nicht leerem TuiVision-Finding. |

## Governance-Anwendbarkeit / Governance Applicability

| Preset | Checkpoint | Anwendbarkeit / Applicability | Begründung / Rationale | Evidence | Owner | Ergebnis / Result | Restrisiko / Residual risk | Neubewertung / Re-evaluation |
|---|---|---|---|---|---|---|---|---|
| Security Governance v0.6.2 | SSDF, CWE, secrets, dependency/SBOM | Applicable | Secure workspace and evidence boundaries are the feature purpose. | `assessment.json`; `docs/security/` | Maintainer | Pass/Open split | Human and image claims remain open. | Security, package, image, or release scope changes |
| Security Governance v0.6.2 | ASVS | N/A | No Web, API, HTTP, or authentication contract changes. | `spec.md` | Maintainer | N/A | None | Such a contract enters scope |
| Security Governance v0.6.2 | VEX, SLSA, Scorecard, AI-SBOM, NIS2, CRA, EU AI Act, DORA | N/A | No release, dependency, product-AI, regulated service, or deployment change is delivered. Temporary SBOM and dependency review remain applicable evidence. | `plan.md`; `.github/workflows/security-supply-chain.yml` | Maintainer | N/A | Existing project evidence remains authoritative. | Triggering product or release scope changes |
| Architecture Governance v0.5.2 | Host/container/mount/network trust boundaries | Applicable | The assessment names the relevant isolation and write boundaries. | `mount-policy.md`; `execution-matrix.md` | Maintainer | Pass/Open split | Free egress and formal protection level remain open. | Boundary or image changes |
| Architecture Governance v0.5.2 | STRIDE/CIA/CAPEC | Applicable | Threat review is bounded to credential disclosure, broad mounts, tampering, egress, and evidence spoofing. | `assessment.json` | Maintainer | Pass | Residual risks remain named. | New flow or threat appears |
| Architecture Governance v0.5.2 | S-ADR, Zero Trust, SAMM, BSI C3A/C5 | N/A | No product architecture, cloud, provider deployment, or organizational maturity contract changes. | `spec.md` | Maintainer | N/A | None | Cloud or deployment scope enters |
| iSAQB Architecture Governance v0.2.2 | Quality scenarios and architecture evidence | Applicable | Isolation, resumability, proof honesty, and partial evidence are recorded without product architecture changes. | `plan.md`; `assessment.json` | Maintainer | Pass | No new product ADR. | Product structure or interface changes |
| A11Y Governance v0.4.3 | DE/EN, CEFR-B2, text-first, WCAG | Applicable | New learner-facing security guidance and DocFX navigation are delivered. | assessment guides; DocFX/Axe/Lynx proof | Maintainer | Pass | Browser-based proof covers the generated local site, not every assistive technology. | Documentation changes |
| Cross-Platform Governance v0.2.2 | Python core, Bash/PowerShell, man page, Cmdlet | Applicable | A new read-only validator is delivered with paired entry points. | `scripts/validate-sandbox-applicability.*`; man page | Maintainer | Pass locally | Remote Windows proof pending. | Script changes |
| Agent Parity Governance v0.4.2 | Maintained agent and generated surfaces | Applicable | Existing guidance is reviewed; no shared rule changes. | parity test; Git diff | Maintainer | `NoUpdateRequired` | Provider availability is separate. | Agent or preset surfaces change |
| Model Routing Governance v0.1.4 | Codex role binding | Applicable | Local routing was refreshed and is `Aligned`; concrete models remain untracked. | `scripts/resolve-model-routing.sh` | Maintainer | Pass | Local machine configuration only. | Catalog or harness changes |
| Intake Authoring v0.3.1 | Intake provenance | Applicable | Current authoring receipt and normalized hash are bound. | authoring receipt; run state | Maintainer | Pass | None | Intake changes |
| Intake Review v0.2.1 | Current `Ready` review | Applicable | Review `847bce5c-98b0-4461-b2a7-c1b5bc9d83dc` covers the exact hash. | series review result | Maintainer | Pass | None | Intake or review changes |
| Intake Sequencing v0.2.3 | Single `Eligible` target | Applicable | The series selected only the sandbox intake. | series manifest | Maintainer | Pass | Next intake remains unstarted. | Series transition |
| Autonomous Run v0.4.1 | State, authority, exact-head delivery | Applicable | MergeAndSync is explicitly authorized and state is validated. | run state; gate requirements | Maintainer | Active | Remote proof pending. | Authority or run state changes |
| Parallel Autonomous Run v0.2.6 | Campaign orchestration | N/A | One serial feature is executed. | `spec.md` | Maintainer | N/A | None | A campaign is requested |
| Historical source policy | `tv203s/` and external design sources | N/A | No historically derived product behavior changes. | `spec.md` | Maintainer | N/A | None | Historical behavior enters scope |

## Validierung / Validation

| Befehl oder Review / Command or review | Auslöser / Trigger | Ergebnis / Result | Evidence oder Fehlergrenze / Evidence or failure boundary |
|---|---|---|---|
| Intake-Review-Validator mit Repository-Root | Pflicht-Preflight | Pass | Exit 0; Series `Ready`, 10 Targets |
| `scripts/resolve-model-routing.sh -Action Status -Harness Codex -RoutingRoot .specify/presets` | Pflicht-Preflight | Pass | `Aligned`, 7 Modelle / models |
| Initialer unittest-Lauf / Initial unittest run | Test-first red proof | Pass | Exit 1 because the validator did not exist yet; no test executed falsely green |
| `python3 -m unittest scripts.tests.test_sandbox_applicability -v` | Validator contract | Pass | 8/8 positive and negative tests |
| Bash assessment validator | Canonical evidence | Pass | 12 controls; `ConditionallyUsable`; JSON output |
| PowerShell assessment validator | Cross-platform parity | Pass | Same 12 controls, recommendation, JSON output, and exit 0 |
| Bash/PowerShell/Python syntax | New script family | Pass | Bash and PowerShell parser pass; Python compiles; PSScriptAnalyzer 1.25.0 reports no finding |
| Bash/PowerShell help and Unix manual review | New public script entry points | Pass | Both wrappers expose bilingual help; the man page documents equivalent inputs, outputs, and exit codes |
| Sandbox `podman-compose --env-file .env.example config --quiet` | Static external reference | Pass | Exit 0 without printing local environment values |
| External sandbox commit, source hashes, clean diff and remote pin | Read-only provenance | Pass | Commit `7adaeac18ca259726468a2fe1d1fd028b895e09c` and all eight accepted hashes match; external worktree is clean |
| `git diff --check` | jede Änderung / every change | Pass | Exit 0; final staged check follows before commit |
| `dotnet format --verify-no-changes` | Repository formatting | Pass | Exit 0; no product source was changed |
| `scripts/scan-agent-secrets.sh --fail-on-high .` | Security and agent evidence | Pass | Exit 0, no High finding; matched values are not copied into Evidence |
| Agent-surface parity unittests | Maintained agent surfaces | Pass | 3/3 tests; no agent or template update required |
| `dotnet list TuiVision.sln package --vulnerable --include-transitive` | Dependency review | Pass | Exit 0; no vulnerable package reported. Sensitive local source connection metadata seen only in process output was deliberately not persisted. |
| `docfx docfx.json` | New guides and navigation | Pass | Exit 0 with 0 warnings and 0 errors; generated output remains ignored |
| `npm test` in `tests/web-a11y` | Documentation A11Y trigger | Pass | Playwright/Axe 2/2 passed |
| UTF-8 `lynx` text dump | Text-first review | Pass | German umlauts and semantic reading order render correctly with explicit UTF-8 locale and charset |
| Product build, runtime tests and coverage | Final delivery-set trigger review | N/A | No product source, API, project, package, example or executable behavior changed; remote baseline CI remains supplemental |
| `scripts/render-project-statistics.sh --repo .` | Shared repository evidence | Pass | Canonical Profile 2 updated from committed implementation source `1eb79e955566` |
| `scripts/render-project-statistics.sh --repo . --check-only --json` | Statistics drift | Pass | `CURRENT`, methodology v2 |
| `scripts/check-homogeneity.sh --dry-run --no-patch .` | Repository homogeneity | Pass | 100%, 29/29 checks |

## Remote-Lieferung / Remote Delivery

| Element / Item | Ergebnis / Result | Evidence |
|---|---|---|
| Push | Pass | Branch `044-sandbox-secure-development-hardening`; veröffentlichter Head / published head `db3ffa6efc21818232ea59bacd0889b2b6e6c4b0` |
| Pull Request | Pass | [hindermath/TuiVision#159](https://github.com/hindermath/TuiVision/pull/159) |
| Pflichtchecks / Required checks | Open | noch nicht ausgeführt / not run |
| Review-Threads | Open | noch nicht geprüft / not inspected |
| Merge | Open | nur nach grünen Gates / only after green gates |
| Lokaler `main`-Sync | Open | nach Merge / after merge |

## Retrospektive / Retrospective

- **Wirksam / Effective**: offen / open
- **Leerlauf / Waste**: offen / open
- **Wiederkehrender Blocker / Recurring blocker**: keiner im Preflight / none in preflight
- **Empfohlene Verbesserung / Recommended refinement**: offen / open
