# Implementierungsplan: RL-SE-/Checklist-Selbstprüfung / Implementation Plan: RL-SE and Checklist Self-Review

**Branch**: `045-rl-se-checklist-self-review` | **Datum / Date**: 2026-08-30 | **Spezifikation / Spec**: [spec.md](spec.md)

**Bindende Eingabe / Binding input**: `requirements/intakes/active/Lastenheft_RL-SE-Checklist-Selbstpruefung.md`

**Planungsbasis / Planning baseline**: Git-HEAD `6bf24ca6d18f83e0c54e9e00f50aba36fff2739c`; der vorhandene Template-Entwurf wurde vollständig neu gebunden.

**Liefermodus / Delivery mode**: `MergeAndSync` ist der im akzeptierten
Run-State gespeicherte spätere Liefermodus, aber keine fortdauernde
Berechtigung. Commit, Push, PR, Merge, Bypass und Branch-Bereinigung setzen vor
der jeweiligen Operation eine aktuelle ausdrückliche Autorisierung voraus. Der
fachliche Scope bleibt ein Audit ohne automatische Härtung oder
Governance-Reparatur.

**Phasengrenze / Phase boundary**: Dieser Lauf erstellt nur die Planungsartefakte und die maschinenlesbare Gate-Anforderung. `tasks.md`, Auditdaten, Validator, Build, Test, Commit, Push, PR, Merge und Run-State-Änderungen bleiben ungestartet.

## Zusammenfassung / Summary

Feature 045 aktualisiert die TuiVision-Selbstprüfung gegen die Richtlinie
Sichere Entwicklung. Ein kanonischer JSON-Datensatz enthält genau 157
Kontrollentscheidungen mit den Kapitelzahlen
`12/13/15/10/13/11/12/13/17/17/12/12`. Jede Zeile verwendet genau einen der
Statuswerte `Applicable`, `AlreadySatisfied`, `N/A`, `Open` oder `FollowUp`
und führt Identität, Quelle, Begründung, aktuelle Evidenz oder Beweislücke,
Owner, Reviewer, Reviewdatum, Follow-up, Priorität, Restrisiko,
Re-Evaluation-Trigger und Human-only-Grenze. Markdown-Projektionen machen
dieselben Fakten Deutsch zuerst, Englisch danach und text-first lesbar.

*Feature 045 refreshes the TuiVision self-review against the Secure
Development Guideline. One canonical JSON dataset contains exactly 157 control
decisions with chapter counts `12/13/15/10/13/11/12/13/17/17/12/12`. Every
row uses exactly one of `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, or
`FollowUp` and records identity, source, rationale, current evidence or an
evidence gap, owner, reviewer, review date, follow-up, priority, residual risk,
re-evaluation trigger, and human-only boundary. Markdown projections expose
the same facts German first, English second, and text-first.*

Die Umsetzung beginnt mit `pr-evidence.md` und einem parsefähigen, noch nicht
akzeptierten Evidence-Skelett. Danach beweist `CL-01-01` als repräsentativer
Vertikalschnitt die vollständige Kette von kanonischer Quelle über Freshness,
Status und Preset-Bezug bis zur Markdown-Projektion. Erst nach positivem und
kontrolliert negativem Validatornachweis werden die übrigen 156 Kontrollen
kapitelweise bewertet. Bekannte Baseline-, Constitution-, Preset-,
Feature-016- und Feature-044-Abweichungen bleiben dokumentierte Beobachtungen; keine
Quellfläche wird dabei repariert.

*Implementation starts with `pr-evidence.md` and a parseable evidence skeleton
that is not yet accepted. `CL-01-01` then proves the complete chain from the
canonical source through freshness, status, and preset relation to the Markdown
projection. The other 156 controls are assessed chapter by chapter only after
positive and controlled negative validator proof. Known baseline,
constitution, preset, Feature 016, and Feature 044 differences remain documented
observations; no source surface is repaired.*

## Technischer Kontext / Technical Context

| Aspekt / Concern | Festlegung / Decision |
|---|---|
| Sprache und Laufzeit / Language and runtime | .NET 10, C# 14 beziehungsweise `LangVersion=latest`; C# wird nur für einen test-only Evidence-Validator verwendet. |
| Primäre Abhängigkeiten / Primary dependencies | Vorhandenes MSTest 4.3.2 und `System.Text.Json`; keine neue NuGet-, Runtime-, Tool- oder Paketabhängigkeit. |
| Speicherung / Storage | Repository-lokales UTF-8-JSON mit stabiler Property-Reihenfolge sowie semantisches Markdown; keine Datenbank, kein Dienst und keine produktiven Daten. |
| Testoberfläche / Test surface | `tests/TuiVision.Drivers.Tests/` erhält einen Evidence-Integritätstest und kontrollierte Fixtures; `.csproj` und `.sln` bleiben unverändert. |
| Zielplattform / Target platform | Plattformneutrale Auditdaten; lokaler macOS-Nachweis plus bestehende CI-/Runner-Evidence. Externe oder menschliche Plattformfakten werden nicht erfunden. |
| Projekttyp / Project type | Audit- und Governance-Dokumentation für ein .NET-Terminal-UI-Framework; keine Produktfunktion. |
| Leistungsziel / Performance goal | Deterministische, netzwerkfreie lineare Validierung von 157 Kontrollen, zwölf Presets und den zugehörigen Relationen. |
| Grenzen / Constraints | Null Änderungen an `src/`, `examples/`, Public API, XML-Kommentaren, Dependencies, Paketen, Projektdateien, Produktverhalten, Governance-Quellen oder automatischen Folge-Intakes. |
| Umfang / Scale | 157 Kontrollzeilen, exakt zwölf Preset-Datensätze, mindestens vier bekannte Drift-Beobachtungen, explizite Human-only-Grenzen und triggerbasierte Gate-Evidence. |
| Quellenbasis / Source baseline | Zwölf `CL_*.md`-Dateien mit 157 eindeutigen Überschriften; Feature-016-Matrix mit historischer Verteilung 65/13/38/36/5 und Feature-044-Sandbox-Evidence mit der begrenzten Entscheidung `ConditionallyUsable` werden nur als neu zu prüfende Eingangsevidenz verwendet. |

## Bindende Artefakte und Drift-Basis / Binding Artifacts and Drift Baseline

Die akzeptierten Feature-Eingaben bleiben während der Umsetzung hashgebunden:

| Artefakt / Artifact | SHA-256 |
|---|---|
| Intake | `62fadb9f571f6c6e5fb81badd103f5ca5087c7219698fdec7be708196d6d6863` |
| `spec.md` | `726238b81de860075cdce75b24a06cefa1193d9d5c86e8e583a2e8d5cfe908e2` |
| `clarification-report.md` | `ad444310fbdd8527e8896bae25032298a5d27b69d4a78f3aa8497cfc73a99cde` |
| `checklists/requirements.md` | `e2f143ad3d58c46bb6d0cb9633b6f79827ccc93c172e0b72f37a515666070a74` |
| `checklists/audit-readiness.md` | `64e127f89ae8c960a39511911cf8c19963b055e2ea9ac729e3161d3caa7108ce` |
| Ready-Series-Review | `795f0e781e6526ff9f00b54efaddb5878ce3e4bcc213646aadc15b2ad2dfb5e9` |

Die historischen Evidence-Eingänge sind am Planungsstand ebenfalls exakt
gebunden: Feature 016 `docs/security/control-assessment.md`
`b311c5b40d09b91cfa688469aaa38d3f8eca89545a7cec83add4a581dbbb5f13`
und `pr-evidence.md`
`58ff4736639c8de8deec0b3f0e2995487d68db8d2c4c80bed4ad7e5de6bb3a6c`;
Feature 044 `assessment.json`
`221def400d03a84383e7d91d24e178f58c31e6eeeb9e1c29fc3c79043ebfc31d`
und `pr-evidence.md`
`ce57b2c41b9c13744aa142f0154947490b9f92950d114aa4c4b78eeb1f227887`.

Die aktuelle fachliche Basis wird bei Implementierungsbeginn erneut gehasht.
Planungsbeobachtungen sind: Manifest-Baseline 3.1.0 gegen Richtlinie und
Sammelband 3.2.0; ältere Einzelchecklisten gegen CL-09/CL-12 3.2.0;
`constitution.md` 1.17.0 gegen `.specify/memory/constitution.md` 1.18.1;
sechs/sieben/historisch acht Presets gegen zwölf aktivierte Registry-Einträge;
die vor späteren Änderungen entstandene Feature-016-Matrix sowie die
Feature-044-Sandbox-Bewertung mit weiterhin offenen Freigabe-, Provider-,
Netzwerk- und Plattformgrenzen. Diese
Unterschiede werden bestätigt, verworfen oder präzisiert, aber nicht behoben.

*The current domain baseline is hashed again at implementation start. Planning
observations cover baseline, constitution, preset-count, Feature 016
freshness, and Feature 044 sandbox-boundary differences. They are confirmed,
rejected, or refined, never
repaired by this audit.*

## Verfassungsprüfung vor Phase 0 / Constitution Check before Phase 0

| Gate | Planentscheidung / Plan decision | Status |
|---|---|---|
| Branch und PR-Fluss | Der vorhandene nummerierte Branch bleibt bindend. Die Planphase führt keine Git- oder Remote-Mutation aus. Spätere `MergeAndSync`-Schritte sind Delivery-Orchestrierung und erweitern weder Auditinhalt noch Änderungsrechte. | Pass |
| Level-2-Umgebung | Die TuiVision-Registerzeile bindet .NET 10/C#, MSTest, Coverlet, DocFX, Playwright/Axe, text-first A11Y, Statistikprofil 2 und die gepflegten Agentenflächen. | Pass |
| Toolchain | .NET 10 und C# 14 bleiben unverändert. Der SDK-Stil nimmt die neue Testdatei ohne Projektdateiänderung auf. | Pass |
| Memory-safe language | C# steht auf der MSL-Allowlist. MSL ersetzt keine Schema-, Pfad-, Status-, Evidenz- oder Freshness-Prüfung. | Pass |
| Secure Code Generation | Der test-only Validator verwendet `System.Text.Json`, geschlossene Enums, Obergrenzen, Ordinalsortierung, Pfadkontrolle und fail-closed Fehlercodes. Relevante Linsen sind CWE-20, CWE-22, CWE-400, CWE-502 und CWE-703. | Pass |
| Architekturgrenzen | Produktarchitektur, Schichten, Schnittstellen, Trust Boundaries, Deployment und Laufzeitflüsse bleiben unverändert. Auditdataset, Projektionen und Validator sind getrennte Evidence-Verantwortungen. | Pass |
| iSAQB/arc42 | Bestehende Architektur-, Risiko- und Technical-Debt-Evidence wird auditiert. Neue ADRs oder Architekturansichten sind `N/A`, solange der Audit kein Produkt- oder Grenzdelta erzeugt. | Pass |
| Sichere Architektur | STRIDE/CIA/CAPEC, Least Privilege, Fail-Safe Defaults, Zero Trust, SAMM, C3A und C5 erhalten Auditentscheidungen. Neue S-ADR-, Threat-Model- oder arc42-Reparaturen sind außerhalb des Scopes. | Pass |
| Security-Standards | NIST SSDF und CWE Top 25 sind `Applicable`. ASVS, SBOM, VEX, SLSA, CAPEC, AI-SBOM, Zero Trust, SAMM, OpenSSF, CRA, NIS2, DORA, EU AI Act, C3A und C5 werden einzeln evidenzbasiert klassifiziert. | Pass |
| AI-SBOM | AI ist Entwicklungswerkzeug, nicht Teil des ausgelieferten oder betriebenen Systems. Feature-Ausführung ist `N/A`; Re-Evaluation erfolgt bei Runtime-/Produkt-KI, Modellen, Daten, Inferenz-Infrastruktur oder ausgelieferten KI-Komponenten. | Pass |
| Supply Chain | Bestehende Dependency-, SBOM-, VEX-, SLSA-, Provenance-, Workflow-Pin- und Scorecard-Evidence wird auf Freshness geprüft. Es gibt keine Paket-, Workflow- oder Release-Reparatur. | Pass |
| Security-First | Keine Credentials, privaten absoluten Pfade, Agent-History, Logs, Sessions, SQLite-Zustände oder produktiven Daten werden erfasst. Secret- und Scope-Scans bleiben Abschlussgates. | Pass |
| Red-Green-Refactor | Nach vollständiger Testassembly-Kompilation scheitert der fokussierte Slice ausschließlich an fehlender/ungültiger `CL-01-01`-Evidence, wird grün und erst dann verbreitert. Negativ-Fixtures verletzen jeweils genau eine Invariante. | Pass |
| Coverage | Fokussierte Tests dienen Red/Green. Am finalen Kandidaten beweist genau ein vollständiger Release-Lauf mit kanonischem Coverlet-Setup Positiv-/Negativvalidator, Regression und mindestens 70 % je Pflichtassembly; Ziel bleibt 80 %. | Pass |
| Dependencies und Pinning | Keine NuGet- oder Tooländerung. Bestehende Versionen werden nur als Auditfakten gelesen. | Pass |
| Serialisierung und Daten | Kanonisches UTF-8-JSON, Schema-Version, exakte geschlossene Werte, Ordinalsortierung, normalisierte SHA-256-Werte und atomare Ablehnung eines ungültigen Gesamtdatensatzes. | Pass |
| Inclusion/A11Y | Alle Leserflächen sind semantisch, text-first, tastatur-, Screenreader-, Braille- und Textbrowser-tauglich. Status, Priorität und Risiko sind nie farb- oder layoutabhängig. | Pass |
| Bilinguale Lieferung | Deutsch zuerst, Englisch direkt danach, ungefähr CEFR B2. Fach- und Spec-Kit-Begriffe werden beim ersten Gebrauch kurz erklärt. | Pass |
| XML/DocFX | Public API und XML-Kommentare bleiben unverändert. Neue Security-Markdown-Evidence und Statistik lösen dennoch DocFX sowie Playwright/Axe und einen text-first Spot-Check aus. | Pass |
| Didaktische Kommentare | Produktkommentare sind `N/A`. Nur nicht triviale Validatorgrenzen erhalten bei Bedarf moderate DE-first/EN-second Warum-Kommentare. | Pass |
| Cross-Platform-Skripte | Kein neues Skript ist nötig: der vorhandene plattformneutrale MSTest-Stack deckt Positiv- und Negativvalidierung ab. Bash/PowerShell-Paar, Manpage, Cmdlet und Paritätsartefakt sind `N/A`; Trigger ist ein späterer script-shaped Diff. | Pass |
| Agentenparität | Shared Guidance, Preset-Policy und Constitution werden nicht geändert. Die fünf Agentenflächen sind für Implementierungsänderungen `NoUpdateRequired`; entdeckter Drift wird nur als Finding erfasst. | Pass |
| Statistik | `docs/project-statistics.md` wird erst nach abgeschlossenem Implementierungsmeilenstein mit Profil 2 aktualisiert; 80 Zeilen/Arbeitstag manuell und 125 Thorsten-Solo bleiben die Referenzen. | Pass |
| Dokumentationsauswirkung | Genau eine Entscheidung: `UpdateRequired`. Neue datierte Security-Evidence, Security-Index, Feature-Evidence und Abschlussstatistik werden dokumentiert und validiert. | Pass |
| Quellenpolicy | Genau `N/A`: Es wird weder historisches Verhalten portiert noch Produktsemantik geändert. Lizenzgrenze `MultipartNotRepositoryWideMIT`; Trigger ist ein tatsächlicher Vertrag-, Pin- oder Consumer-Evidence-Scope. | Pass |

Nach Phase 1 bleibt die Prüfung unverändert bestanden: Das Datenmodell und der
Abnahmevertrag führen keine Produkt-, Architektur-, Dependency-, API- oder
Governance-Mutation ein. Es besteht keine Verfassungsverletzung und keine
Ausnahme ist erforderlich.

*The post-design check remains passed. No constitutional violation or
exception is required.*

## Governance-Preset-Plan / Governance Preset Plan

Die zwölf Registry-Datensätze werden jeweils mit ID, Version, Prüfpunkten,
Status, Evidenz, Owner, Reviewer, Restrisiko, Follow-up und Trigger erfasst:

| Preset | Version | Feature-Entscheidung |
|---|---:|---|
| `security-governance` | 0.6.2 | Vollständiger RL-SE-, MSL-, Standards-, Supply-Chain- und Regulatory-Review. |
| `architecture-governance` | 0.5.2 | Bestehende Security-Architektur-, Zero-Trust-, SAMM-, C3A-/C5-Evidence prüfen; keine Reparatur. |
| `isaqb-architecture-governance` | 0.2.2 | Architekturziele, Views, Risiken, ADR-Bedarf und technische Schulden auditieren. |
| `a11y-governance` | 0.4.3 | Bilinguale CEFR-B2- und text-first-Evidence mit WCAG-2.2-AA-Prüfpfad. |
| `cross-platform-governance` | 0.2.2 | Vorhandene Plattform-/Skript-Evidence auditieren; neue Scripts `N/A`. |
| `agent-parity-governance` | 0.4.2 | Agenten-, Template- und Constitution-Drift dokumentieren, nicht synchronisieren. |
| `model-routing-governance` | 0.1.4 | Fail-closed lokale Routing-Evidence lesen; keine Modell-/Providerkonfiguration ändern. |
| `intake-authoring-governance` | 0.3.1 | Intake-Lineage lesen; keine Intake-Erstellung oder -Mutation. |
| `intake-review-governance` | 0.2.1 | Hashgleichen `Ready`-Review als Startgate prüfen. |
| `intake-sequencing-governance` | 0.2.3 | Serienreihenfolge und Eligibility nur als Evidence lesen. |
| `autonomous-run-governance` | 0.4.1 | Evidence-first, Scope-Firewall, Gate-Vertrag und genaue Delivery-Grenzen anwenden. |
| `parallel-autonomous-run-governance` | 0.2.6 | Ausführung `N/A`, da serieller Lauf; Installation und Trigger bleiben dokumentiert. |

## Projektstruktur / Project Structure

### Planungsartefakte und spätere Audit-Evidence / Planning Artifacts and Later Audit Evidence

```text
specs/045-rl-se-checklist-self-review/
├── plan.md
├── plan-review.md
├── research.md
├── data-model.md
├── quickstart.md
├── autonomous-gate-requirements.json
├── contracts/
│   └── rl-se-self-review-acceptance.md
├── checklists/
│   ├── requirements.md
│   └── audit-readiness.md
├── pr-evidence.md                              # später zuerst anzulegen
└── delivery-closeout.md                        # später nur für dauerhafte Delivery-Fakten

docs/security/secure-development/2026-08-30-rl-se-checklist-self-review/
├── README.md                                   # Leserpfad und Begriffe
├── rl-se-self-review.json                      # kanonischer 157-Zeilen-Datensatz
├── control-assessment.md                       # text-first Projektion
├── preset-assessment.md                        # exakt zwölf Preset-Zeilen
├── governance-observations.md                  # dokumentierter, unreparierter Drift
├── human-boundaries.md                         # Human-/External-only Entscheidungen
└── validation-evidence.md                      # Positiv-/Negativ- und Freshness-Ergebnisse

docs/security/README.md                         # später: Link auf datierte Evidence
docs/project-statistics.md                      # später: Abschlussmeilenstein

tests/TuiVision.Drivers.Tests/
├── RlSeSelfReviewEvidenceTests.cs              # später: test-only Validator
└── Fixtures/RlSeSelfReview/
    ├── valid-vertical-slice.json
    └── invalid-*.json                          # später: je eine verletzte Invariante
```

`tasks.md` gehört der nächsten `/speckit.tasks`-Phase und wird durch diesen
Planlauf nicht erstellt.

*`tasks.md` belongs to the next `/speckit.tasks` phase and is not created by
this planning run.*

### Read-only- und geschützte Flächen / Read-Only and Protected Surfaces

```text
src/
examples/
tv203s/
TVDEMOS/
TVFM/
*.sln
*.csproj
Directory.Packages.props
.config/dotnet-tools.json
.github/workflows/
.specify/presets/
constitution.md
.specify/memory/constitution.md
requirements/intakes/                     # außer späterer Standardarchivierung des bindenden Intake
docs/secure-development/
docs/security/control-assessment.md        # Feature-016-Ausgangsevidenz bleibt historisch nachvollziehbar
docs/security/secure-development/2026-08-29-sandbox-applicability/
specs/016-secure-development-hardening/pr-evidence.md
specs/044-sandbox-secure-development-hardening/pr-evidence.md
```

Der spätere Lieferweg besitzt zwei geschlossene, kausal getrennte
Pfadmengen. Der primäre PreMerge-Kandidat darf nur diese Flächen enthalten:

```text
specs/045-rl-se-checklist-self-review/spec.md
specs/045-rl-se-checklist-self-review/clarification-report.md
specs/045-rl-se-checklist-self-review/checklists/requirements.md
specs/045-rl-se-checklist-self-review/checklists/audit-readiness.md
specs/045-rl-se-checklist-self-review/plan.md
specs/045-rl-se-checklist-self-review/research.md
specs/045-rl-se-checklist-self-review/data-model.md
specs/045-rl-se-checklist-self-review/quickstart.md
specs/045-rl-se-checklist-self-review/contracts/rl-se-self-review-acceptance.md
specs/045-rl-se-checklist-self-review/autonomous-gate-requirements.json
specs/045-rl-se-checklist-self-review/plan-review.md
specs/045-rl-se-checklist-self-review/tasks.md
specs/045-rl-se-checklist-self-review/analysis-report.md
specs/045-rl-se-checklist-self-review/analysis-report-2.md
specs/045-rl-se-checklist-self-review/pr-evidence.md
specs/045-rl-se-checklist-self-review/autonomous-run-state.json  # ausschließlich runner-owned
.specify/feature.json                       # vorhandener runner-erzeugter Feature-Zeiger, kein manueller Edit
docs/security/secure-development/2026-08-30-rl-se-checklist-self-review/
docs/security/README.md
docs/project-statistics.md
tests/TuiVision.Drivers.Tests/RlSeSelfReviewEvidenceTests.cs
tests/TuiVision.Drivers.Tests/Fixtures/RlSeSelfReview/
Directory.Build.props
```

Der nach dem tatsächlichen Feature-Merge erforderliche Evidence-only-Closeout-
Kandidat darf zusätzlich nur diese kausal neuen Flächen enthalten:

```text
specs/045-rl-se-checklist-self-review/delivery-closeout.md
specs/045-rl-se-checklist-self-review/retrospective.md
specs/045-rl-se-checklist-self-review/tasks.md
specs/045-rl-se-checklist-self-review/autonomous-run-state.json  # ausschließlich runner-owned Projektion
docs/project-statistics.md
requirements/intakes/active/Lastenheft_RL-SE-Checklist-Selbstpruefung.md
requirements/intakes/archive/Lastenheft_RL-SE-Checklist-Selbstpruefung.045-rl-se-checklist-self-review.md
requirements/intakes/series/tui-vision-delivery/intake-review-report.md
requirements/intakes/series/tui-vision-delivery/intake-review-request.json
requirements/intakes/series/tui-vision-delivery/intake-review-result.json
requirements/intakes/series/tui-vision-delivery/manifest.json
requirements/intakes/series/tui-vision-delivery/operation.json
requirements/intakes/series/tui-vision-delivery/order.md
requirements/intakes/series/tui-vision-delivery/receipt.json
specs/intake-series-archive/a73dda7c-163b-4530-97f2-fd9eea5e8986/
```

Die konkrete neue Operations-ID wird vor dem ersten Serienwrite aus dem
transaktionalen Sequencing-Plan übernommen und gegen genau ein neues
Manifest-/Receipt-Paar validiert. Die beiden Intake-Pfade dürfen erst nach dem
tatsächlichen Feature-Merge gemeinsam durch die vorhandenen gepaarten
Archivierungswerkzeuge als Rename erscheinen. `autonomous-run-state.json`
bleibt eine Orchestratorfläche und ist kein Implementierungs- oder manueller
Delivery-Edit. `.specify/runtime/` bleibt runner-eigen, untracked und außerhalb
jedes Delivery-Commits; sein Vorhandensein wird im Status klassifiziert und
nicht als Kandidatenpfad akzeptiert.

**Strukturentscheidung / Structure decision**: JSON ist die kanonische
maschinenlesbare Wahrheit. Markdown ist eine vollständig validierte
Leserprojektion, keine zweite Statusquelle. Der Validator liegt im vorhandenen
Evidence-orientierten Testprojekt; es entsteht weder ein neues Projekt noch
eine neue Abhängigkeit. Die Feature-016-Matrix wird nicht überschrieben.

## Daten- und Evidence-Strategie / Data and Evidence Strategy

1. Die zwölf Checklistenüberschriften werden ordinal nach Datei und
   `CL-XX-NN` extrahiert. Das Inventar muss 157 eindeutige IDs und die exakten
   Kapitelzahlen liefern.
2. Jede Quell- und Evidenzdatei erhält repository-relativen Pfad, SHA-256,
   Beobachtungszeit, direkte oder begrenzte Beweisrolle und Freshness-Trigger.
3. Jede Kontrollzeile wird neu bewertet. Feature-016-Status und Resultat sowie
   Feature-044-Sandbox-Fakten sind Vergleichsevidence, nie automatische
   Übernahmen oder formale Freigaben.
4. `AlreadySatisfied` verlangt mindestens eine aktuelle direkte Evidence-
   Referenz. Eine Vorlage, ein alter Claim oder bloße Dateiexistenz genügt
   nicht.
5. `N/A`, `Open` und `FollowUp` werden statusgerecht geprüft. Fehlende
   Autorität oder Evidenz ist niemals allein eine `N/A`-Begründung.
6. Presets, Governance-Beobachtungen und Human-only-Grenzen sind eigene
   Relationen und erhöhen die 157 Kontrollzeilen nicht.
7. JSON-Eigenschaften und Arrays werden deterministisch ordinal geschrieben;
   Zeitstempel und Hashes beziehen sich auf den dokumentierten Review-Snapshot.

## Implementierungsstrategie / Implementation Strategy

### Phase A – Evidence-first und Eingangsgates

1. Lege `pr-evidence.md` vor jeder Implementierungsänderung an. Es enthält
   Authority, Accepted Hashes, Baseline-HEAD, Scope-Firewall, Gate-Status
   `Not Run`, Validierungsledger und Human-only-Grenzen.
2. Revalidiere Intake, Ready-Review, Accepted Artifacts, Branch, HEAD,
   Registry, Baseline, 157 Quellüberschriften, Feature-016-Evidence,
   Feature-044-Sandbox-Evidence und geschützte Wurzeln. Nutze die vorhandenen
   read-only Series-, Receipt-, Review- und Run-State-Validatoren mit
   explizitem Repository-Root. Der mutierende Alignment-Wrapper ist für die
   akzeptierte `Binding Input`-Formulierung nicht der geplante Gate-Befehl.
3. Erzeuge das parsefähige Audit-Skelett und die sieben Leserflächen, ohne
   einen bestandenen Audit-, Build-, Test-, Remote- oder Compliance-Claim.
4. Kompiliere die vollständige spätere Testoberfläche, bevor ein erwartetes
   Red ausgeführt wird. Jeder `dotnet build`/`dotnet test`-Aufruf erhält genau
   eine vorherige Build-Counter-Erhöhung.

### Phase B – Repräsentativer Vertikalschnitt

1. Eine fokussierte Prüfung erwartet für `CL-01-01` zunächst den stabilen
   Fehler für die fehlende oder unvollständige Zeile.
2. Ergänze Quellidentität, alle Pflichtfelder, Evidence-Freshness,
   Feature-016-Vergleich, den verknüpften `security-governance`-Preset-Datensatz
   und die Markdown-Projektion.
3. Der grüne Lauf beweist JSON-Schema, Statusregel, Evidence-Relation,
   Projektion und Scope-Schutz für diesen Slice.
4. Kontrollierte Negativ-Fixtures beweisen mindestens falsche Kardinalität,
   doppelte/unbekannte ID, ungültigen Status, leeres Pflichtfeld, schwache
   `AlreadySatisfied`-Evidence, fehlenden N/A-Trigger, unbefugten Human-Claim,
   Preset-Unterdeckung, Drift-Reparaturpfad und absoluten privaten Pfad.

### Phase C – Kapitelweise 157-Zeilen-Prüfung

Nach dem grünen Slice werden `CL-01` bis `CL-12` in fester Reihenfolge
bewertet. Nach jedem Kapitel müssen Quellmenge, JSON, Markdown, Statussummen,
Evidence-Referenzen und offene Grenzen konsistent sein. Der Lauf stoppt bei
Baseline-Drift, unklarer Quellidentität, nicht reproduzierbarer positiver
Evidence oder einer Scope-Mutation.

### Phase D – Presets, Drift, Human-only und Abschlussbild

1. Erzeuge exakt zwölf Preset-Datensätze aus der Registry.
2. Dokumentiere bestätigte Drift mit beiden Quellen, Unterschied, Auswirkung,
   Owner, Priorität, Restrisiko, Folgeaktion und Trigger; repariere keine
   Quelle.
3. Trenne rechtliche, organisatorische, Provider-, Secret-, Plattform- und
   Freigabeentscheidungen von technischer Repository-Evidence.
4. Erzeuge Statussummen erst aus dem validierten Datensatz. Die historischen
   65/13/38/36/5 werden nur als Vergleich gezeigt.
5. Führe den text-first, bilingualen CEFR-B2- und Begriffserklärungsreview aus.

### Phase E – Triggerbasierte Validierung und Delivery

1. Führe Struktur-, Scope-, Secret-, Validator-, Format-, Supply-Chain-,
   DocFX-, Playwright/Axe- und text-first-Gates nur gemäß den ausgelösten
   Pfaden aus. Ein finaler Release-Coverage-Lauf ersetzt getrennte Volltest-
   und Coverage-Wiederholungen und enthält Positiv- wie Negativvalidator.
2. Aktualisiere Statistik und getrackte Kandidaten-Evidence seriell und lasse
   den Orchestrator den runner-owned Candidate-Freeze vor dem letzten exakten
   Release-Coverage-Lauf erfassen. Richte unmittelbar vor diesem Lauf den
   Build-Counter aus, ändere danach keinen getrackten PreMerge-Pfad mehr und
   erzeuge zunächst den lokalen Delivery-Head. Die temporäre PreMerge-Evidence
   entsteht weiterhin erst nach aktuellen Remote-Checks für genau diesen Head.
3. Erst nach grünen lokalen Delivery-Eintrittsgates und aktueller
   ausdrücklicher `PublishPR`- oder `MergeAndSync`-Autorisierung darf dieser
   unveränderte Head gepusht und der PR erstellt werden. Aktuelle Remote-Checks
   und Review-Threads werden anschließend genau diesem Head zugeordnet.
4. Erzeuge erst danach die temporäre PreMerge-Gate-Evidence für denselben
   Feature-HEAD und validiere sie außerhalb der Gate-Liste gegen
   `autonomous-gate-requirements.json`. Der Validator seiner eigenen Evidence
   und echte PostMerge-Fakten sind keine PreMerge-Gates.
5. Erst nach akzeptierter PreMerge-Evidence und erneuter aktueller
   ausdrücklicher `MergeAndSync`-Autorisierung darf gemergt, der Feature-Branch
   bereinigt und `main == origin/main` synchronisiert werden. Danach erfolgen
   Lastenheft-Rename und Serienübergang. Dauerhafte PostMerge-Fakten,
   Retrospektive und runner-owned Terminalprojektion werden über genau einen
   triggerproportional validierten Evidence-only-Closeout-PR geliefert; dieser
   Closeout behauptet keine eigene PR-, Head- oder Merge-Identität in sich.

## Validierungsleiter und Trigger / Validation Ladder and Triggers

| Trigger | Spätere Prüfung / Later proof | Build-Counter |
|---|---|---|
| Plan-, JSON- oder Markdown-Änderung | `git diff --check`; Marker-, Fence-, Link-, UTF-8- und Scope-Prüfung | Nein |
| Binding-Input- oder Intake-Evidence | vorhandene Intake-/Series-/Review-Validatoren mit explizitem Repository-Root | Nein |
| Auditdataset oder Projektionen in Red/Green-Entwicklung | fokussierter positiver Validator und alle kontrollierten Negativ-Fixtures; Entwicklungsnachweis, kein zusätzliches finales Gate | Je `dotnet test` genau einmal vorher erhöhen |
| Finaler Test-only-C#-/Coverage-Kandidat | zuerst `xmllint --noout coverlet.runsettings`, dann genau ein `dotnet test TuiVision.sln --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings --logger "console;verbosity=detailed"`; Log beweist Vollsuite, `Test_CompleteAuditIsValid`, `Test_InvalidFixturesFailClosed` und mindestens 70 % je Pflichtassembly, Ziel 80 % | Ja, genau einmal unmittelbar vor diesem Testaufruf |
| Security-Markdown oder Security-Index | `docfx docfx.json`, danach `tests/web-a11y` Playwright/Axe und text-first Spot-Check | Nur wenn der konkrete Befehl `dotnet build`/`dotnet test` ausführt |
| Script-shaped Diff | Bash/PowerShell-Paar, Help, Manpage, Cmdlet, Parität und OS-Nachweis | In diesem Plan `N/A`; tritt nur bei Scope-Drift ein |
| Public API/XML/Package/Project/Runtime | Hard stop als Scope-Verletzung | Keine Fortsetzung |
| Delivery-Kandidat vor Commit | `git diff --cached --check` und staged-path/status-Abgleich gegen die geschlossene Positivliste | Nein |
| Committer exakter Head | `git diff --check "$(git merge-base origin/main HEAD)" HEAD`, `git diff --name-only "$(git merge-base origin/main HEAD)" HEAD` und `git status --short`; danach PreMerge-Evidence für denselben Head | Nein |
| Kausaler PostMerge-Closeout | PostMerge-Schema-2.0-Evidence mit leerem `changedPaths`, Lastenheft-Rename, atomarer Serienübergang, Retrospektive und runner-owned Terminalprojektion; proportional betroffene Validatoren, dann genau ein Evidence-only-Closeout-PR und erneuter sauberer Default-Branch-Sync | Nur wenn der proportionale Closeout-Validator tatsächlich `dotnet build` oder `dotnet test` ausführt |

Nicht ausgeführte Remote-, Plattform- oder Human-Gates werden nie als
bestanden berichtet. Ein Pass erfordert erwarteten Exitcode und einen
Fehlerkanal ohne fatale Signatur.

## Build-, Versions- und Shared-Write-Grenzen / Build, Version, and Shared-Write Boundaries

- `Directory.Build.props` bleibt in der Planphase unverändert.
- Vor jedem einzelnen späteren `dotnet build` oder `dotnet test` werden
  `Version`, `AssemblyVersion` und `FileVersion` gemeinsam auf
  `1.45.<FeatureCommitCount>.<incrementedBuild>` ausgerichtet. Ein impliziter
  Build innerhalb `dotnet test` erhält keine zweite Erhöhung.
- Restore, Format, reine Validatoren, DocFX, NPM und Scans erhöhen den Counter
  nur dann, wenn sie tatsächlich einen expliziten `dotnet build` oder
  `dotnet test` ausführen.
- Vor Commit oder Push wird der Patch-Anteil auf den nach diesem Commit
  geltenden Feature-Commit-Count ausgerichtet. Der Build-Anteil wird genau
  einmal unmittelbar vor dem letzten exakten `dotnet test` erhöht; danach
  bleibt der Kandidat getrackt unverändert. Erfordert eine Korrektur einen
  neuen Commit oder Test, beginnt diese Sequenz erneut.
- `rl-se-self-review.json`, seine Markdown-Projektionen, `pr-evidence.md`,
  `autonomous-gate-requirements.json`, Gate-Evidence, Version, Statistik,
  Security-Index, Lastenheft-Archivierung und Delivery-Closeout sind
  Single-writer-Flächen. Sie werden nicht parallel bearbeitet.

## Autonomous Execution Contract

- **Authority**: `MergeAndSync` ist der gespeicherte Delivery-Modus, keine
  geerbte Berechtigung. Jede Remote-, Merge-, Bypass- und Cleanup-Operation
  verlangt aktuelle ausdrückliche Autorisierung. Der Audit selbst darf
  ausschließlich Evidence und test-only Validierung ändern.
- **Evidence first**: `pr-evidence.md` und der nicht akzeptierte Datensatz
  existieren vor Validator- oder breiter Auditbearbeitung.
- **Vertical slice**: `CL-01-01` muss mit Red-/Green- und Negativnachweis
  vollständig funktionieren, bevor weitere Kontrollzeilen folgen.
- **Scope firewall**: Geschützte Wurzeln, Governance-Quellen, Produkt- und
  Dependency-Flächen müssen im Diff leer bleiben.
- **Convergence**: Clarify ist abgeschlossen; Tasks/Analyze müssen später ohne
  umsetzbaren Finding konvergieren. Implementierung konvergiert bei 157/157,
  zwölf Presets, vollständigen Pflichtfeldern, grünen anwendbaren Gates und
  null unbefugten Claims oder Scope-Deltas.
- **Shared writers**: Evidence, JSON, Markdown, Version, Statistik, Index,
  Gate- und Delivery-Dateien werden serialisiert.
- **Safe stops**: Nach Plan, Evidence-Skelett, Vertikalschnitt, jedem Kapitel,
  Preset-/Drift-Freeze, lokaler Validierung und PreMerge-Evidence.
- **Resume**: Eine Unterbrechung setzt Revalidierung von State, Authority,
  Accepted Hashes, HEAD, Diff, Scope, Counter und letztem Gate voraus. Der
  State wird nur durch den Orchestrator an einer Phasengrenze geändert.
- **Exact-head evidence**: Geplant sind
  `/private/tmp/045-rl-se-checklist-self-review.premerge-gate-evidence.json`
  und bei erforderlichen PostMerge-Fakten
  `/private/tmp/045-rl-se-checklist-self-review.postmerge-gate-evidence.json`.
  Die PreMerge-Anforderung enthält weder eine Selbstvalidierung noch ein
  PostMerge-Gate; der vorhandene Validator prüft die fertige temporäre Datei
  als äußerer Abschluss. Staged-Prüfung ist ein Vor-Commit-Prozessgate,
  während der Gate-Datensatz den committed Candidate scannt.
  Dauerhafte Delivery-Fakten landen erst nach ihrer Entstehung in
  `delivery-closeout.md`; diese Planphase erklärt keinen davon für vollständig.
- **Remote closeout**: Commit, Push, PR, Review, Merge und Sync sind getrennte
  spätere Orchestrierungsaktionen unter jeweils aktueller Autorität. Kausal
  neue PostMerge-Dateien werden nur über den einen benannten Evidence-only-
  Closeout-PR geliefert. Provider-Einstellungen, Secrets, Bypass von
  technischen Gates und formale Rechts-/Compliance-Freigaben bleiben verboten.
- **Retrospective handoff**: Nach sauberem MergeAndSync darf die vorhandene
  autonome Retrospektivphase Workflow-Lernen erfassen; sie startet keine
  Folgearbeit.

## Komplexitätsnachweis / Complexity Tracking

Es gibt keine Constitution-Verletzung. Die zusätzliche kanonische JSON-Datei
und der test-only Validator sind die kleinste belastbare Lösung: Markdown
allein kann Kardinalität, geschlossene Werte, Freshness-Relationen und
Negativfälle nicht ausreichend deterministisch beweisen. Ein neues Projekt,
eine neue Dependency oder ein neues Cross-Platform-Skript wären größer und
werden deshalb nicht eingeführt.

*There is no constitution violation. Canonical JSON plus a test-only validator
is the smallest reliable solution; no new project, dependency, or script is
introduced.*

## Plan-Abnahmekriterien / Plan Acceptance Criteria

- Alle sechs verlangten Planungsdokumente und die Gate-Anforderung existieren
  ohne offene Marker.
- Der Plan bindet 157 Kontrollen, die exakten Kapitelzahlen, fünf Statuswerte,
  alle Pflichtfelder, Freshness, zwölf Presets, Drift und Human-only-Grenzen.
- Positive und negative Validierung, Build-Counter, Single-writer-Flächen,
  triggerbasierte Gates, Scope-Firewall und exakter HEAD sind festgelegt.
- `pr-evidence.md` ist vor späteren Implementierungsänderungen Pflicht.
- `CL-01-01` ist der verpflichtende Vertikalschnitt vor breiter Wiederholung.
- Source-reference, neue Architekturartefakte, neue Scripts und Produktänderung
  sind jeweils nachvollziehbar `N/A` oder verboten.
- `MergeAndSync` ist klar von der fachlichen Selbstprüfung getrennt.
- Diese Planung enthält keinen Claim über einen bereits bestandenen
  Implementierungs-, Delivery-, Remote- oder Compliance-Gate.
