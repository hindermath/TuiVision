# Implementierungsplan: GSDB-Spec-Kit-Intensivprüfung / Implementation Plan: GSDB Spec Kit Intensive Review

**Branch**: `046-gsdb-spec-kit-intensive-review` | **Datum / Date**: 2026-08-30 | **Spec**: [spec.md](spec.md)
**Eingabe / Input**: Akzeptierte Feature-046-Spezifikation, bindendes Intake, Klärungsbericht, beide abgeschlossenen Checklisten, beide Verfassungen, aktive Preset-Registry, vollständiger GSDB-Snapshot und relevante Evidenz aus Features 016, 044 und 045.

## Zusammenfassung / Summary

Feature 046 liefert ausschließlich einen reproduzierbaren Sicherheits- und Governance-Review-Snapshot unter `docs/security/secure-development/2026-08-30-gsdb-spec-kit-intensive-review/`. Eine kanonische JSON-Datei erfasst den vollständigen GSDB-Quellenbestand, exakt 157 Kontrollen, abgeleitete Sprachprofile, alle aktuell aktivierten Presets, Governance-Prüfpunkte, Evidenzfamilien, menschliche Nachweisgrenzen und berechnete Summen. Deterministische maschinenlesbare Projektionen für Quellen, Kontrollen, Sprachen, Preset/Governance, Evidenzfamilien und Summary sowie deutsch-englische Markdown-Projektionen werden ausschließlich daraus erzeugt und durch einen neuen test-only MSTest-Validator im bestehenden Projekt `tests/TuiVision.Drivers.Tests` geprüft.

Feature 046 delivers only a reproducible security and governance review snapshot under `docs/security/secure-development/2026-08-30-gsdb-spec-kit-intensive-review/`. One canonical JSON file captures the complete GSDB source set, exactly 157 controls, derived language profiles, every currently enabled preset, governance checkpoints, evidence families, human proof boundaries, and calculated summaries. Deterministic machine-readable projections for sources, controls, languages, preset/governance, evidence families, and summary, plus German-English Markdown projections, are generated only from it and validated by a new test-only MSTest validator in the existing `tests/TuiVision.Drivers.Tests` project.

Der Plan übernimmt Strukturmuster aus Features 016, 044 und 045, aber keine positive Aussage. Jede positive Disposition wird gegen den aktuellen Feature-046-Snapshot neu belegt. Feststellungen erzeugen nur dokumentierte Folgehinweise; es gibt in diesem Feature keine Reparatur.

The plan reuses structural patterns from Features 016, 044, and 045 but no positive conclusion. Every positive disposition is newly evidenced against the current Feature 046 snapshot. Findings create documented follow-up notes only; this feature performs no remediation.

## Technischer Kontext / Technical context

**Sprache/Version / Language/version**: C# 14 über .NET 10 für den test-only Validator; JSON 1.0 und Markdown für Evidenz

**Primäre Abhängigkeiten / Primary dependencies**: vorhandenes MSTest 4.3.2, `System.Text.Json`, Coverlet Collector 10.0.1; keine neue Abhängigkeit

**Speicherung / Storage**: versionierte JSON-/Markdown-Dateien; temporäre Gate-Belege nur als `/private/tmp/046-gsdb-spec-kit-intensive-review.premerge-gate-evidence.json` und `/private/tmp/046-gsdb-spec-kit-intensive-review.postmerge-gate-evidence.json`

**Tests**: bestehendes `tests/TuiVision.Drivers.Tests`; vollständiger Release-Test- und Coverlet-Lauf

**Zielplattform / Target platform**: Repository-Validierung auf .NET-10-fähigen Linux-, macOS- und Windows/WSL-Umgebungen; keine neue Laufzeitplattform

**Projekttyp / Project type**: evidenz-only Repository-Review; kein Produkt- oder neues Projekt

**Leistungsziel / Performance goal**: deterministischer lokaler Validatorlauf ohne Netzwerk; Laufzeit proportional zur versionierten Repository-Größe

**Einschränkungen / Constraints**: exakt 157 Kontrollen mit der bindenden Kapitelverteilung `12/13/15/10/13/11/12/13/17/17/12/12`; alle Nicht-Kontroll-Inventare und ihre Zahlen werden aus dem revalidierten Snapshot abgeleitet; UTF-8/LF-normalisierte Texthashes; Raw-Hashes für Binärdaten; ordinal sortierte Pfade; Fail-closed bei Drift

**Umfang / Scale**: aktuell beobachtet 37 GSDB-Dateien und 12 aktivierte Presets; beide Werte werden bei Umsetzung neu abgeleitet und nicht fest codiert
**Delivery-Modus**: `MergeAndSync`; in dieser Planphase keine Liefer- oder Provider-Aktion

## Verfassungsprüfung / Constitution check

### Vor Forschung und Design / Before research and design

| Gate | Status | Planbeleg / Planning evidence |
|---|---|---|
| Spezifikation und akzeptiertes Intake binden denselben Scope | PASS | Spezifikation, Klärungsbericht, Checklisten und autonomer Laufzustand wurden vollständig gelesen; keine offene materielle Frage. |
| Preset-Priorität und Versionen stammen aus aktiver Registry | PASS | 12 aktivierte Einträge wurden aus `.specify/presets/.registry` mit ihren installierten Versionen abgeleitet. |
| Keine Produkt-, Runtime-, API-, Abhängigkeits-, Projekt-, Beispiel- oder Workflowänderung | PASS | Schreibumfang ist auf Evidenz, test-only Validator, Feature-Artefakte und notwendige Indizes/Statistik begrenzt. |
| Security-by-Design und Least Privilege | PASS | Offline-fähige lokale Auswertung; keine Secrets, Provider-Schreibrechte oder beliebige Nutzerdateien. |
| TDD und 70-%-Coverage-Gate | PASS | Red/Green/Refactor für Validator; vollständiger Coverlet-Lauf für alle fünf Gate-Assemblies. |
| Bilinguale, inklusive Dokumentation | PASS | Deutsch zuerst, Englisch danach, CEFR-B2, semantische Texttabellen, WCAG-2.2-AA-Prüfweg. |
| Historische Quellenpolicy | PASS (`N/A`) | GSDB-Review betrifft keine Turbo-Vision-Portierungsfrage. Historische Bäume bleiben read-only; nur konkrete GSDB-Frage darf begrenzte Einsicht auslösen. |
| Agenten-Parität | PASS | Relevante Agentenflächen werden als Evidenz geprüft. Keine neue gemeinsame Anleitung geplant; Context-Update nur bei nachgewiesener Plan-Skill-Pflicht. |
| Cross-Platform | PASS | Keine neue Script- oder Produktlogik; Validator benutzt portable .NET-/Repository-Pfade und normalisierte Zeilenenden. |
| Autonome Lieferautorität | PASS | Laufzustand ist aktiv; Planungsphase führt keine nicht autorisierte Lieferaktion aus. |

Das harte Coverage-Minimum bleibt 70 % pro Gate-Assembly; 80 % ist das dokumentierte Qualitätsziel. Ein Wert zwischen 70 % und 80 % ist kein verschwiegenes Scheitern, wird aber im Evidence-Bericht als Abstand zum Ziel ausgewiesen.

The hard coverage minimum remains 70 percent per gate assembly; 80 percent is the documented quality target. A result between 70 and 80 percent is not a hidden failure, but its gap to the target is stated in the evidence report.

### Nach Design / After design

| Gate | Status | Designentscheidung / Design decision |
|---|---|---|
| Eine kanonische Quelle | PASS | `gsdb-spec-kit-intensive-review.json` ist alleinige fachliche Quelle; Projektionen werden bytegenau validiert. |
| Bestandsabschluss | PASS | Quellen aus physischem Baum und Manifestabschluss; eine Zeile je physischer Datei. |
| Kontrollabschluss | PASS | Exakt 157 IDs aus den zwölf Checklisten; die daraus berechnete Kapitelverteilung muss zusätzlich exakt `12/13/15/10/13/11/12/13/17/17/12/12` entsprechen. Dispositionszahlen bleiben abgeleitet. |
| Keine vorweggenommenen Befunde | PASS | Dispositionen entstehen erst in der Umsetzung; `observations` darf leer sein. |
| Positive Aussagen neu validiert | PASS | Validator fordert aktuelle Feature-046-Evidenz; Feature 016/044/045 sind nur referenzierte Evidenz. |
| Geeignete Validatoroberfläche | PASS | Neues test-only C#-Testfile im vorhandenen Drivers-Testprojekt; keine Projekt-/Paket-/Scriptänderung. |
| Nachweisgrenzen | PASS | `LocalDirect`, `RemoteObserved`, `HumanApproval`, `ProviderBoundary`, `LegalOrganizational` sind getrennt. |
| Determinismus | PASS | Sortierung, Normalisierung, Hashing, Ableitungen und Renderer sind im Datenmodell festgelegt. |
| Liefer- und Closeout-Kausalität | PASS | Exact-head Pre-/Post-Merge-Gates; mergeabhängige Fakten erst im serialisierten Closeout. |

## Preset-Verpflichtungen / Preset obligations

### Security Governance 0.6.2

- Vollständiger 157er Kontrollreview, aktuelle Evidenz, Fail-closed-Validator, Supply-Chain- und Secret-Checks.
- Keine Geheimnisrotation, Provider-Einstellung oder unautorisierte Reparatur.
- SBOM/AI-SBOM wird als Kontrollfrage bewertet; Feature 046 führt kein Runtime-/Produkt-AI ein.
- NuGet-Währung wird über vulnerable, deprecated und outdated getrennt geprüft. Bestehende Pinning-Ausnahmen benötigen aktuelle Begründung; Pakete werden in diesem Feature nicht geändert.
- Das Evidenzschema verwendet ausschließlich vorhandenes `System.Text.Json`, striktes UTF-8, case-sensitive IDs, referenzielle Integrität und Fail-closed-Ablehnung; keine neue Produktserialisierung entsteht.

### Architecture Governance 0.5.2 und iSAQB 0.2.2

- Änderungstyp ist evidenz-only und test-only. Es entsteht keine neue Produktkomponente, Runtime-Grenze oder API.
- Architektur-, Security- und Governance-Evidenzfamilien werden geprüft; positive Architekturaussagen werden nicht übernommen.
- ADR, Threat Model, Security Review oder Architekturdiagramm werden nur bei tatsächlicher Trigger-Änderung verlangt. Eine solche Änderung liegt im erlaubten Scope nicht vor und würde den Lauf stoppen.
- Qualitätsziele: Reproduzierbarkeit, Nachvollziehbarkeit und Barrierefreiheit; bewusster Trade-off ist ein größerer Evidenzdatensatz zugunsten unabhängiger Prüfbarkeit.

### Accessibility Governance 0.4.3

- Deutsch zuerst, Englisch danach, CEFR-B2 und text-first.
- Semantische Überschriften und Tabellen; keine Farbcodierung als alleiniger Informationsträger.
- DocFX plus Playwright/axe und Textbrowser-Smoke sind bei tatsächlicher DocFX-Eingabe anwendbar; die geplante Sicherheitsdokumentation macht dies voraussichtlich erforderlich.
- CLI-Accessibility ist für neue produktive Terminalausgabe `N/A`, weil nur testinterne Diagnostik entsteht; stabile zweisprachige Fehlercodes bleiben dennoch textlesbar. Neue User-CLI-Ausgabe würde das Gate reaktivieren und den Scope stoppen.

### Cross-Platform Governance 0.2.2

- Validator verwendet `Path`, UTF-8, explizite LF-Normalisierung und ordinalen Vergleich.
- Keine Shell- oder PowerShell-Neuentwicklung. Unerwartete Scriptänderungen lösen den vollständigen Paritätsnachweis oder einen Scope-Stopp aus.
- Remote-OS-Evidenz wird von lokaler direkter Evidenz getrennt.

### Agent Parity Governance 0.4.2 und Model Routing Governance 0.1.4

- Projektgeführte Agentenflächen werden aus dem TuiVision-Level-2-Eintrag, den tatsächlich versionierten Guidance-/Command-/Prompt-/Skill-Flächen und den Agentenschlüsseln der aktivierten Preset-Registry vereinigt. Beim Planen beobachtete Namen sind keine feste Liste; fehlende oder zusätzliche aktuelle Flächen müssen sichtbar werden.
- Aktive Presets und Modell-Routing werden aus ihren aktuellen Registern abgeleitet.
- Kein Agenten-Kontext wird allein für dieses Feature neu erzeugt. Nur eine ausdrückliche projektgeführte Generatorpflicht darf `update-agent-context` auslösen; andernfalls wird `NoUpdateRequired` dokumentiert.
- Eine atomare Synchronisierung aller Agentenflächen ist für die Implementierung `N/A`, weil das Feature keine gemeinsame Regel ändert und die Spezifikation Synchronisierung verbietet. Festgestellte Drift wird vollständig bewertet und nur als Folgehinweis dokumentiert.

### Intake-/Autonomie-Governance

- Bindendes Intake, Review, Manifest, Receipt, Serie und Laufzustand bleiben hashgebunden.
- Shared writers werden serialisiert.
- Liefermodus `MergeAndSync`; enger Human-Approval-only Bypass ausschließlich gemäß Gate-Vertrag und nie in dieser Planphase.
- `parallel-autonomous-run-governance` wird als installiertes Preset geprüft, ist für die Ausführung aber `N/A`, weil Feature 046 seriell läuft und gemeinsame Writer nicht parallel beschrieben werden.

## Projektstruktur / Project structure

### Planungsartefakte / Planning artifacts

```text
specs/046-gsdb-spec-kit-intensive-review/
├── plan.md
├── plan-review.md
├── research.md
├── data-model.md
├── quickstart.md
├── autonomous-gate-requirements.json
├── contracts/
│   └── gsdb-review-acceptance-contract.md
├── spec.md
├── clarification-report.md
├── checklists/
│   ├── requirements.md
│   ├── audit-readiness.md
│   └── plan-quality.md
└── autonomous-run-state.json
```

### Geplanter Implementierungs- und Lieferumfang / Planned implementation and delivery set

```text
docs/security/
├── README.md                                      # Navigationslink / navigation link
└── secure-development/
    └── 2026-08-30-gsdb-spec-kit-intensive-review/
        ├── README.md
        ├── gsdb-spec-kit-intensive-review.json
        ├── source-projection.json
        ├── control-projection.json
        ├── language-projection.json
        ├── preset-governance-projection.json
        ├── evidence-family-projection.json
        ├── summary-projection.json
        ├── source-inventory.md
        ├── control-assessment.md
        ├── language-assessment.md
        ├── preset-governance-assessment.md
        ├── evidence-family-assessment.md
        ├── human-boundaries.md
        ├── summary.md
        └── validation-evidence.md

tests/TuiVision.Drivers.Tests/
├── GsdbSpecKitIntensiveReviewEvidenceTests.cs
└── Fixtures/GsdbSpecKitIntensiveReview/
    └── invalid-*.json                             # Kleine negative Fixtures / small negative fixtures

specs/046-gsdb-spec-kit-intensive-review/
├── pr-evidence.md                                 # Umsetzungsevidenz / implementation evidence
├── delivery-closeout.md                           # Erst nach Merge-Fakten / only after merge facts
└── retrospective.md                               # Kausaler Abschluss / causal closeout

docs/project-statistics.md                         # Serialisierte Abschlussstatistik / serialized final statistics
Directory.Build.props                              # Nur Versionsfelder / version fields only
requirements/intakes/archive/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.046-gsdb-spec-kit-intensive-review.md
requirements/intakes/series/tui-vision-delivery/
├── intake-review-report.md                        # Nur kausaler Serienübergang / causal transition only
├── intake-review-request.json
├── intake-review-result.json
├── manifest.json
├── operation.json
├── order.md
└── receipt.json

specs/intake-series-archive/a73dda7c-163b-4530-97f2-fd9eea5e8986/{new-operation-id}/
├── manifest.json                                  # Durch Governance-Ablauf abgeleitet / governance-derived
└── receipt.json
```

`Directory.Build.props`, Intake-Serie, Security-Index, Statistik, Gate-Evidenz und Retrospektive sind gemeinsame Writer und werden nacheinander geschrieben. Das bindende Intake bleibt bis zur kausal zulässigen Archivierung an seinem aktiven Ort; es wird nicht dupliziert.

`Directory.Build.props`, intake series, security index, statistics, gate evidence, and retrospective are shared writers and are written sequentially. The binding intake remains active until causally eligible for archiving; it is not duplicated.

### Ausdrücklich ausgeschlossene Pfade / Explicitly excluded paths

- `src/**`, `examples/**`, Projektdateien und Paketmanifeste.
- `.github/workflows/**`, Provider-/Repository-Einstellungen und Secret Stores.
- `tv203s/**`, `TVDEMOS/**`, `TVFM/**` und andere historische/Consumer-Quellen als Schreibziel.
- Neue Skripte, neue Testprojekte, neue Runtime-Tools oder neue Abhängigkeiten.
- Reparaturen oder Folgefeatures aus Review-Ergebnissen.

Jede unerwartete Änderung in diesen Flächen ist ein Scope-Firewall-Fehler und muss vor Fortsetzung entfernt oder als materieller Konflikt gemeldet werden.

Any unexpected change in these surfaces is a scope-firewall failure and must be removed before continuing or reported as a material conflict.

## Design / Design

### Kanonischer Datensatz / Canonical dataset

Der Datensatz folgt [data-model.md](data-model.md). Er enthält keine voreingestellten Kontrollurteile. Quellen-, Sprach-, Preset-, Agentenflächen-, Governance- und Evidenzfamilieninventare werden aus dem akzeptierten Snapshot abgeleitet. Summen werden ausschließlich berechnet. Das Feld `controlCount` muss 157 ergeben und `controlCountByChapter` muss die bindende Verteilung `12/13/15/10/13/11/12/13/17/17/12/12` ergeben; alle Nicht-Kontroll-Kardinalitäten müssen dem aktuellen Snapshot entsprechen.

The dataset follows [data-model.md](data-model.md). It contains no pre-seeded control judgments. Source, language, preset, agent-surface, governance, and evidence-family inventories are derived from the accepted snapshot. Summaries are exclusively computed. `controlCount` must equal 157 and `controlCountByChapter` must equal the binding `12/13/15/10/13/11/12/13/17/17/12/12` partition; every non-control cardinality must match the current snapshot.

### Projektionen / Projections

Der test-only Renderer erzeugt zuerst die geforderten maschinenlesbaren JSON-Projektionen für Quellen, Kontrollen, Sprachen, Preset/Governance einschließlich Agentenflächen, Evidenzfamilien und Summary. Jede trägt Projektionstyp, kanonischen normalisierten Hash und nur die zugehörige sortierte Teilmenge; sie ist keine zweite Pflegequelle. Danach erzeugt er die feste, text-first Markdown-Reihenfolge. Jede lesbare Projektion enthält Scope, Snapshotbindung, ausführliche Dispositionsbezeichnungen, Evidenzpfade, Grenzen und deutsch-englischen Inhalt. Der Validator erzeugt alle erwarteten Bytes im Speicher und vergleicht sie mit den versionierten Dateien.

The test-only renderer first produces the required machine-readable JSON projections for sources, controls, languages, preset/governance including agent surfaces, evidence families, and summary. Each carries its projection type, canonical normalized hash, and only its related sorted subset; it is not a second maintenance source. It then produces the fixed text-first Markdown order. Every readable projection contains scope, snapshot binding, expanded disposition labels, evidence paths, boundaries, and German-English content. The validator renders all expected bytes in memory and compares them with the versioned files.

### Kontrollentscheidungen / Control decisions

Jede der 157 Kontrollen erhält genau eine Entscheidung aus dem akzeptierten Katalog. `Applicable` und `AlreadySatisfied` sind positive Entscheidungen und benötigen aktuelle, pfadgenaue Evidenz. `N/A` benötigt eine konkrete Nichtanwendbarkeitsbegründung und einen Revalidierungstrigger. `Open` und `FollowUp` benennen Risiko, Eigentümerrolle und nächsten dokumentierten Schritt, führen ihn aber nicht aus.

Every one of the 157 controls receives exactly one decision from the accepted catalog. `Applicable` and `AlreadySatisfied` are positive decisions and require current path-specific evidence. `N/A` requires a concrete non-applicability rationale and revalidation trigger. `Open` and `FollowUp` name risk, owner role, and next documented step but do not execute it.

Die zweiachsigen GSDB-Quellbegriffe bleiben zusätzlich als unveränderte Quellkontexte sichtbar: Anwendbarkeit mit `Applicable`, `N/A`, `Open` sowie Erfüllung mit `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled`, `Not Assessed`. Sie ersetzen nie die eine Feature-Disposition und erzeugen keine positive Aussage.

The two-axis GSDB source terms also remain visible as unchanged source context: applicability with `Applicable`, `N/A`, `Open`, and fulfillment with `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled`, `Not Assessed`. They never replace the one feature disposition and create no positive claim.

### Verbindliche Review-Domänen / Mandatory review domains

- **Sprachen**: .NET 10/C# als MSL mit weiterhin erforderlicher Prüfung von Datenzugriff, Ausgabe, Autorisierung, Validierung, Deserialisierung, HTTP/SSRF, Datei-/Prozessgrenzen und Secrets; Bash und PowerShell mit Quoting, End-of-options, Strict Mode, dynamischer Ausführung, temporären Dateien, Fehler- und Hilfeparität; TypeScript/JavaScript für den Web-A11Y-Pfad; C/C++, SQL und alle weiteren abgeleiteten Profile sichtbar entscheiden.
- **Standards**: NIST SSDF und CWE Top 25 immer; OWASP ASVS, SBOM, VEX, AI-SBOM, SLSA, SAMM, CAPEC, Zero Trust, OWASP Cheat Sheets/Proactive Controls und OpenSSF Scorecard jeweils einzeln disponieren.
- **Regulatorik und Assurance**: CRA, NIS2, DORA, EU AI Act, Datenschutz/DPIA, BSI C3A und BSI C5 getrennt behandeln; externe Rechts-, Organisations- und Providerfragen nie lokal positiv behaupten.
- **Architektur und Grenzen**: Trust Boundaries, CIA, STRIDE, CAPEC, Defense in Depth, Least Privilege, sichere Defaults, Angriffsoberfläche, Konfiguration, Daten-, Datei-, UI-, CLI-, Prozess- und Deploymentgrenzen, technische Schulden und Restrisiken prüfen, ohne Architektur zu ändern.
- **Supply Chain**: Dependency-Inventar, Restore-/Lock-Reproduzierbarkeit, immutable Workflow-Referenzen, SBOM, VEX, Provenance/SLSA, Malware-/Secret-Scans, Disclosure und Scorecard-Evidenz prüfen.
- **Agenten-Parität**: Projektgeführte Guidance-Dateien sowie tatsächlich vorhandene Command-, Prompt-, Skill- und Agent-Definitionen aus versionierten Pfaden inventarisieren; diese Menge mit Level-2-Verfassung und aktivierter Preset-Registry schließen. Jede Fläche erhält Pfad, Hash, Agentenfamilie, Flächentyp und Assessment. Nicht vorhandene Junie- oder andere persönliche Agentenzustände werden nicht erfunden oder gelesen.
- **KI und Sandbox**: Entwicklungs-KI von Runtime-/Produkt-KI trennen. AI-SBOM für reines Development-Tooling mit Systemgrenze bewerten. Mounts, Schreibrechte, Hostdaten, Agentenzustand, Secrets, Netzwerk, Toolchain, Modell-Routing, Prompt-/Log-Redaction, praktische Plattform-Evidenz, Freigabe und Lebenszyklus getrennt prüfen.

- **Languages**: assess .NET 10/C# as an MSL while still reviewing data access, output, authorization, validation, deserialization, HTTP/SSRF, file/process boundaries, and secrets; Bash and PowerShell for quoting, end-of-options, strict mode, dynamic execution, temporary files, errors, and help parity; TypeScript/JavaScript for the web-A11Y path; visibly decide C/C++, SQL, and every other derived profile.
- **Standards**: always cover NIST SSDF and CWE Top 25; separately disposition OWASP ASVS, SBOM, VEX, AI-SBOM, SLSA, SAMM, CAPEC, Zero Trust, OWASP Cheat Sheets/Proactive Controls, and OpenSSF Scorecard.
- **Regulation and assurance**: separately treat CRA, NIS2, DORA, EU AI Act, privacy/DPIA, BSI C3A, and BSI C5; never claim external legal, organizational, or provider questions as locally satisfied.
- **Architecture and boundaries**: assess trust boundaries, CIA, STRIDE, CAPEC, defense in depth, least privilege, secure defaults, attack surface, configuration, data, file, UI, CLI, process and deployment boundaries, technical debt, and residual risk without changing architecture.
- **Supply chain**: assess dependency inventory, restore/lock reproducibility, immutable workflow references, SBOM, VEX, provenance/SLSA, malware/secret scans, disclosure, and Scorecard evidence.
- **Agent parity**: inventory project-owned guidance files and the actually present command, prompt, skill, and agent definitions from tracked paths; close that set against the Level-2 constitution and enabled preset registry. Every surface records path, hash, agent family, surface type, and assessment. Absent Junie or other personal agent state is neither invented nor read.
- **AI and sandbox**: separate development AI from runtime/product AI. Assess AI-SBOM for development-only tooling with system-boundary evidence. Separately assess mounts, writes, host data, agent state, secrets, network, toolchain, model routing, prompt/log redaction, practical platform evidence, approval, and lifecycle.

### Nachweisgrenzen / Proof boundaries

Lokale Datei-, Hash-, Test-, Coverage-, Formatierungs- und Dokumentationsbelege sind `LocalDirect`. Remote-Checks, Merge und Branch-Synchronisierung sind `RemoteObserved`. Organisations-, Provider-, Rechts- und menschliche Freigaben bleiben eigene Grenzen. Ein lokaler Beleg darf keine externe Aussage stützen.

Local file, hash, test, coverage, formatting, and documentation evidence is `LocalDirect`. Remote checks, merge, and branch synchronization are `RemoteObserved`. Organizational, provider, legal, and human approvals remain separate boundaries. Local evidence cannot support an external claim.

## Autonomer Ausführungsvertrag / Autonomous execution contract

| Vertragspunkt / Contract point | Bindende Planung / Binding plan |
|---|---|
| Aktuelle Autorität / Current authority | Der akzeptierte Modus ist `MergeAndSync`; jede spätere Remote-Aktion setzt erneute Lauf- und Exact-head-Prüfung voraus. Die Planphase führt keine Delivery-Aktion aus. |
| Evidence-first Setup | Hashbindungen, Registry, GSDB-Abschluss, Branch, Arbeitsbaum und Scope-Firewall werden vor dem ersten Implementierungsschreiben geprüft. |
| Repräsentativer Schnitt / Representative slice | Eine Quelle, `CL-01-01`, ein Sprachprofil, ein aktuelles Preset, eine Evidenzfamilie, eine Grenze, Summary und Projektion durchlaufen Red/Green vor dem vollständigen Datensatz. |
| Konvergenz / Convergence | Jede Phase besitzt eine messbare Stop-Bedingung: Input grün, Schnitt grün, 157er Abschluss, Projektionsparität, lokale Gates, committed candidate, Remote exact-head, MergeAndSync, Closeout. |
| Gemeinsame Writer / Shared writers | `Directory.Build.props`, Security-Index, kanonisches JSON/Projektionen, Gate-/PR-Evidence, Statistik, Intake-Serie, Lauf-/Closeout- und Retrospektivflächen werden serialisiert und vor dem Schreiben neu gelesen. |
| Triggerbasierte Validierung / Trigger-based validation | DocFX/Axe/Textbrowser, Script-Parität, Architektur/API/XML und historische Quellen folgen expliziten Triggern; `N/A` benötigt aktuelle Begründung. |
| Scope-Firewall | Geschlossene Positivliste; Produkt-, Runtime-, API-, Dependency-, Projekt-, Beispiel-, Workflow-, Provider-, Secret- und Finding-Abhilfeänderungen sind verboten. |
| Remote-Closeout | Pre-Merge und Post-Merge binden exakte Commits. Intake-Archiv, Serienübergang, finale Statistik und Retrospektive folgen kausal nach dem wirklichen Merge. |
| Ausnahme / Exception | Nur ein einzeln benanntes, nachweislich nicht verfügbares Remote-Gate darf nach vollständigem lokalem technischem Grün, null umsetzbaren technischen Befunden, null actionable Review-Threads, null Scope-Verstößen und Human Approval als einziger offener Regel begrenzt ersetzt werden. Der Nachweis nennt Gate, autorisierte Person, Zeitpunkt, Begründung, Evidence-Grenze und Ablaufzeitpunkt. |

Der Vertrag erweitert keine Autorität. Fehlt eine notwendige aktuelle Berechtigung, bleibt der Lauf an der betreffenden Grenze stehen und dokumentiert den Blocker.

The contract expands no authority. If required current authorization is absent, the run stops at that boundary and records the blocker.

## Implementierungsphasen / Implementation phases

### Phase 0 – Drift- und Autoritätsprüfung / Drift and authority audit

1. Laufzustand, Branch, Feature-ID, Delivery-Modus und akzeptierte Serie erneut lesen. Intake, Review, Manifest und Receipt werden gegen `acceptedArtifacts` geprüft. Feature-Artefakte werden über die im Run-State hashgebundenen neuesten abgeschlossenen Routing-Ergebnisdateien und deren `payloadPath`-/`payloadSha256`-Kette geprüft. Der selbst als `plan-review-1`-Payload gebundene Reviewbericht attestiert zusätzlich die post-remediation Hashes der dort ausdrücklich gelisteten Planungsartefakte und hat für diese Pfade Vorrang vor dem älteren `plan-1`-Payload. Ein älterer `specify-1`-Payload darf ebenso die neuere akzeptierte `clarify-1`-Spezifikation nicht überschreiben. Artefakte ohne Routing-Payload oder Review-Attestation werden mit aktuellem Hash inventarisiert und spätestens durch den exakten Kandidaten-Commit gebunden.
2. Aktive Registry samt installierter Versionen neu ableiten; beim Planen beobachtete 12 Einträge nur als Vergleich verwenden.
3. Arbeitsbaum und Delivery-Set erfassen; fremde Änderungen unverändert lassen.
4. `Directory.Build.props` gemäß Version `1.46.<Feature-Branch-Commitzahl>.<inkrementierter Buildzähler>` vorbereiten. `Version`, `AssemblyVersion` und `FileVersion` bleiben identisch. Der Buildzähler wird vor jedem `dotnet build` oder `dotnet test` erhöht. Vor einem Commit ist der Patchwert die prospektive Commitzahl (`git rev-list --count HEAD` plus eins); unmittelbar nach dem Commit muss er der tatsächlichen HEAD-Commitzahl entsprechen.
5. Bei Hash-, Scope- oder Autoritätskonflikt stoppen; keine Annahme repariert einen akzeptierten Input.

### Phase 1 – Roter vertikaler Schnitt / Red vertical slice

1. Neue test-only Validator-Datei und kleine negative Fixtures im bestehenden Drivers-Testprojekt anlegen; `.csproj` bleibt unverändert.
2. Einen repräsentativen Schnitt implementieren: eine physische GSDB-Quelle, `CL-01-01`, ein abgeleitetes Sprachprofil, der erste aktuelle Registry-Eintrag, eine Evidenzfamilie, eine Governance-Grenze und die zugehörigen Summary-/Projection-Regeln.
3. Zuerst fehlschlagende Tests für fehlende Evidenz, falsche Kontrollanzahl, Registry-Drift und Projektionsdrift ausführen und Red-Evidenz protokollieren.
4. Minimalen kanonischen Testdatensatz im Speicher validieren; noch keinen dauerhaften Review-Datensatz schreiben.

### Phase 2 – Vollständiges Inventar und Bewertung / Complete inventory and assessment

1. Mit der zentralen Verzahnungsdatei beginnen, danach den vollständigen physischen GSDB-Baum und Manifestabschluss erfassen und eindeutige Quellenzeilen und Hashes bilden.
2. Exakt 157 Kontrollen aus den zwölf Checklisten ableiten und die berechnete Kapitelverteilung zusätzlich gegen `12/13/15/10/13/11/12/13/17/17/12/12` prüfen; keine Statusübernahme aus Feature 016.
3. Sprachprofile deterministisch aus expliziten Regelprofilen, Verfassungs-/Preset-Pflichten und `git ls-files`-basierten, im Datensatz deklarierten Dateityp-/Shebang-Detektoren bilden; unbekannte codeartige Treffer schlagen geschlossen fehl. Historische C/C++-Wurzeln standardmäßig `N/A` und read-only halten.
4. Presets aus der aktuellen Registry ableiten. Projektgeführte Agentenflächen werden separat aus Verfassungsangaben, Registry-Agentenschlüsseln und tatsächlich versionierten Guidance-/Command-/Prompt-/Skill-/Agent-Pfaden geschlossen. Governance-Checkpoints entstehen aus beiden Verfassungen, Presets, Agentenflächen, Modell-Routing, Intake und Laufzustand.
5. Evidenzfamilien aus dem akzeptierten Pflichtdomänenkatalog und allen zusätzlichen aktiven Preset-/Governance-Pflichten ableiten; jede Familie verwendet deklarative Selektoren und sortierte Trefferlisten. Fehlende Pflichtfamilien oder nicht zugeordnete aktive Pflichten schlagen geschlossen fehl.
6. Features 016/044/045 pfadgenau als Evidenz lesen; jede positive Aussage unabhängig neu prüfen.
7. Jede Kontrolle und jedes weitere prüfpflichtige Element vollständig disponieren. Keine Ergebnisse vorerfinden; tatsächliche Beobachtungen erst jetzt anlegen.
8. Feststellungen nur als `DocumentationOnly`, `FollowUpCandidate`, `HumanDecision` oder `ProviderDecision` dokumentieren; `implementedInFeature046` bleibt `false`.

### Phase 3 – Kanonische Ausgabe und Projektionen / Canonical output and projections

1. Datierte Ausgabestruktur anlegen und kanonisches JSON schreiben.
2. Alle Zahlen aus Arrays berechnen; `157` und die Kapitelverteilung `12/13/15/10/13/11/12/13/17/17/12/12` als Invarianten desselben Kontrollinventars validieren. Alle Nicht-Kontroll-Zahlen bleiben abgeleitet.
3. Maschinenlesbare Quellen-, Kontroll-, Sprach-, Preset/Governance-/Agentenflächen-, Evidenzfamilien- und Summary-Projektionen deterministisch erzeugen.
4. Neun reader-orientierte Markdown-Dateien deterministisch projizieren.
5. `docs/security/README.md` um die deutsche und englische Reader-Route ergänzen.
6. Kanonisches JSON, maschinenlesbare und lesbare Projektionen, Navigation und Hashes mit dem Validator prüfen.

### Phase 4 – Lokale technische Gates / Local technical gates

1. Targeted Red/Green-/Negativtests und vollständigen Validatorlauf ausführen.
2. Vollständigen Release-Testlauf mit kanonischer Coverlet-Konfiguration ausführen; `xmllint --noout coverlet.runsettings` soweit verfügbar. Jede der fünf Gate-Assemblies muss mindestens 70 % Line Coverage erreichen.
3. `dotnet format --verify-no-changes` nach Restore ausführen.
4. Abhängigkeits- und Supply-Chain-Prüfungen ausführen: vulnerabel, deprecated und outdated als getrennte Belege; unveränderliche Workflow-Referenzen prüfen.
5. Secret-/Credential-Scans für vollständigen Delivery-Diff und Repository-Evidenz ausführen; keine Geheimnisse ausgeben.
6. DocFX ohne Warnung/Fehler, Playwright/axe und Textbrowser-Smoke ausführen, wenn der Diff eine DocFX-Eingabe berührt. Öffentliche API/XML-Diffs sind ein harter Scope-Fehler.
7. Agenten- und Preset-Parität sowie Security-/Architecture-/A11Y-/Cross-Platform-Nachweise aktualisieren. Für die Statistik werden nur vorläufige, reproduzierbare Messwerte vorbereitet; der mergeabhängige finale Statistikstand wird erst in Phase 6 geschrieben.
8. `pr-evidence.md` mit Befehlen, Exitcodes, Commit, Belegpfaden und klaren lokalen/externen Grenzen schreiben.

### Phase 5 – Committed-candidate und Remote-Exact-Head / Committed candidate and remote exact head

Diese Phase ist im späteren autonomen Implementierungs-/Lieferlauf anwendbar, nicht in der aktuellen Planphase.

This phase applies in the later autonomous implementation/delivery run, not in the current planning phase.

1. Delivery-Set gegen Scope-Firewall und gemeinsame Writer prüfen.
2. Vor dem Kandidaten-Commit den Patchwert auf die prospektive Commitzahl und den Buildwert auf den nächsten manuellen Zähler setzen; alle drei Versionsfelder identisch committen. Unmittelbar danach muss der Patchwert `git rev-list --count HEAD` entsprechen.
3. Den vollständigen kanonischen Release-/Coverlet-Lauf auf genau diesem sauberen committed-candidate-HEAD ausführen. Die dafür nötige Build-Erhöhung muss bereits vor dem Kandidaten-Commit erfolgt sein. Jede weitere `dotnet build`-/`dotnet test`-Ausführung macht den Kandidaten veraltet und verlangt einen neuen Buildzähler, prospektiven Patchwert, Commit und vollständigen finalen Lauf.
4. Nach dem finalen `dotnet`-Lauf keine getrackte Datei mehr ändern. Committed-candidate-HEAD erfassen; temporären Pre-Merge-Gate-Beleg exakt als `/private/tmp/046-gsdb-spec-kit-intensive-review.premerge-gate-evidence.json` erzeugen und mit dem Gate-Validator prüfen. Dauerhafte Evidence darf Ergebnisse früherer Läufe enthalten; der temporäre Beleg bindet den letzten exakten Kandidatenlauf, ohne den Kandidaten zu verändern.
5. Nur nach vollständigem lokalem Grün pushen und Pull Request erstellen oder aktualisieren.
6. Erforderliche Remote-Checks genau am Kandidaten-HEAD beobachten. Kein beweglicher Branch oder älterer Lauf gilt als Beleg.
7. Human-Approval-only Bypass nur für genau ein nachweislich nicht verfügbares Remote-Gate bei vollständigem technischem Grün, null umsetzbaren technischen Befunden, null actionable Review-Threads, null Scope-Verstößen und Human Approval als einziger offener Regel. Gate, autorisierte Person, Zeitstempel, Begründung, Evidence-Grenze und Ablaufzeitpunkt sind Pflicht; die Ausnahme ersetzt keine technische oder fachliche Prüfung.

### Phase 6 – MergeAndSync und kausaler Closeout / MergeAndSync and causal closeout

1. Erst nach exact-head Remote-Grün den autorisierten Merge ausführen und Hauptbranch synchronisieren.
2. Post-Merge-HEAD und Remote-Zustand exakt in `/private/tmp/046-gsdb-spec-kit-intensive-review.postmerge-gate-evidence.json` prüfen.
3. Erst danach mergeabhängige Fakten schreiben: `delivery-closeout.md`, finaler Statistikstand nach Statistikprofil 2, Intake-Archivname, Serienübergang und Retrospektive.
4. Das Intake mit dem vorhandenen paarigen Rename-Ablauf nach `requirements/intakes/archive/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.046-gsdb-spec-kit-intensive-review.md` verschieben. Danach die aktuell akzeptierten Serienartefakte und ihre Governance-Archive serialisiert über den bestehenden Intake-Sequencing-Ablauf aktualisieren und Manifest, Receipt sowie Review-Freshness erneut validieren; keine ad-hoc Teilmutation ist zulässig.
5. Falls ein Closeout-Commit/PR nötig ist, bleibt er ausschließlich Evidenz und darf keine Feststellung beheben.
6. Finalen Hauptbranch-HEAD, Delivery-Set und Runnerzustand synchron bestätigen und die Feature-Branch-Bereinigung nur im akzeptierten Repository und unter aktueller Autorität durchführen.

## Validator- und Teststrategie / Validator and test strategy

### Geplante Testklassen / Planned test classes

- `Test_RepresentativeVerticalSlice_ValidatesCurrentEvidence`
- `Test_CanonicalSnapshot_ContainsExactly157UniqueControls`
- `Test_CanonicalSnapshot_MatchesExactChapterCardinalities`
- `Test_SourceInventory_EqualsPhysicalAndManifestClosure`
- `Test_RegistryInventory_EqualsAllEnabledCurrentPresets`
- `Test_AgentSurfaceInventory_EqualsCurrentProjectOwnedClosure`
- `Test_LanguageGovernanceAndEvidenceInventories_AreComplete`
- `Test_Summaries_AreDerivedFromCanonicalArrays`
- `Test_Projections_AreByteExactAndTextFirst`
- `Test_PositiveDispositions_RequireCurrentEvidence`
- `Test_PreviousFeatureConclusion_CannotServeAsSolePositiveEvidence`
- `Test_TextHashes_AreLineEndingNeutral`
- `Test_BinaryHashes_UseRawBytesAndManagedChecksumMatches`
- `Test_HistoricalSourceConsultation_IsBoundedOrNA`
- `Test_InvalidFixtures_FailClosedWithStableDiagnostics`

### Negative Fixtures / Negative fixtures

Kleine Fixtures decken mindestens ab: doppelte oder fehlende Kontrolle, Kontrollzahl ungleich 157, falsche Kapitelverteilung bei weiterhin 157 Kontrollen, unbekannte Disposition, fehlendes Pflichtfeld, fehlende positive Evidenz, fremde/fehlende Evidenzreferenz, unzulässige Proof-Boundary-Kombination, doppelte Quelle, Manifest-/Physikdrift, manipulierte Prüfsumme, fehlendes Registry-Preset, fehlende oder fremde Agentenfläche, ausgelassenes Sprachprofil, ausgelassener Governance-Checkpoint, ausgelassene Evidence-Familie, manuell verfälschte Summary, unsortierte Pfade, Projektionsdrift sowie veraltete oder widersprüchliche Routing-Payload-Bindung.

Small fixtures cover at minimum: duplicate or missing control, control count other than 157, a wrong chapter partition that still totals 157, unknown disposition, missing required field, missing positive evidence, dangling/missing evidence reference, an invalid proof-boundary combination, duplicate source, manifest/physical drift, tampered checksum, missing registry preset, missing or foreign agent surface, omitted language profile, omitted governance checkpoint, omitted evidence family, manually falsified summary, unsorted paths, projection drift, and a stale or conflicting routing-payload binding.

### Gate-Reihenfolge / Gate order

Targeted Red → representative Green → vollständige Evidenzvalidierung → negativer Fail-closed-Satz → vorläufige lokale Gates → dauerhafte Evidence → Versions-/Kandidaten-Commit → vollständiger kanonischer Release-/Coverlet-Lauf am sauberen committed candidate → nicht mutierende Restgates → Pre-Merge-Evidence → exact-head remote → MergeAndSync → kausaler Closeout.

## Gate-Anforderungen / Gate requirements

Die maschinenlesbaren Anforderungen stehen in [autonomous-gate-requirements.json](autonomous-gate-requirements.json). Der Implementierungslauf muss daraus temporäre Pre-/Post-Merge-Evidenz erzeugen. Ein Gate ist nur erfüllt, wenn Befehl, Exitcode, Commitbindung, Zeit, relevante Zählungen und Belegpfade vorhanden sind. `N/A` benötigt den vorab definierten Trigger und eine aktuelle Begründung.

The machine-readable requirements are in [autonomous-gate-requirements.json](autonomous-gate-requirements.json). The implementation run must use them to produce temporary pre-/post-merge evidence. A gate is satisfied only when command, exit code, commit binding, time, relevant counts, and evidence paths are present. `N/A` requires the predefined trigger and a current rationale.

## Dokumentations- und Statistikplan / Documentation and statistics plan

- Reader-Route: `docs/security/README.md` → datiertes `README.md` → Fachprojektionen → kanonisches JSON.
- Jede Projektion ist Deutsch zuerst, Englisch danach, CEFR-B2 und text-first.
- `docs/project-statistics.md` folgt Statistikprofil 2: reproduzierbare JSON-Konfiguration, chronologisches Fortschreibungsprotokoll, exakte Werte, ASCII-only-Diagramme mit deutscher und englischer Textalternative sowie ein weiterhin letzter Top-Level-Gesamtstatistikblock. Zahlen stammen aus dem finalen post-merge Delivery-Set, nicht aus Planungsschätzungen; die Referenzen 80/125 Zeilen pro Arbeitstag bleiben unverändert.
- `pr-evidence.md` trennt lokale direkte, Remote-, menschliche und Provider-Evidenz.
- `retrospective.md` wird nach technischem und kausalem Abschluss geschrieben und enthält wiederverwendbare Erkenntnisse, keine nachträgliche Statuskosmetik.

## Versions- und Lieferregeln / Version and delivery rules

- Major bleibt `1`; Minor ist `46`.
- Patch ist die tatsächliche `git rev-list --count HEAD`-Commitzahl des Feature-/PR-Branches nach dem jeweiligen Commit. Vor dem Commit wird deshalb der prospektive Wert `aktuelle HEAD-Commitzahl + 1` eingetragen und danach gegen HEAD geprüft.
- Build wird vor jedem `dotnet build` oder `dotnet test` manuell erhöht.
- `Version`, `AssemblyVersion` und `FileVersion` sind stets identisch.
- Für den finalen Exact-Head-Lauf werden Patch und nächster Buildzähler vor dem Kandidaten-Commit gesetzt. Nach diesem Commit läuft genau der kanonische finale `dotnet test TuiVision.sln --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; ein weiterer Build/Test erzwingt einen neuen Kandidatenzyklus.
- Die aktuelle Planungsbeobachtung `1.45.816.479` ist nur der Vorgängerstand, nicht der Zielwert.
- Kein Commit, Push, PR, Merge, Bypass, Provider-Write, Intake-Übergang oder Secret-Write ist durch diese Planphase autorisiert.

## Risiken und Gegenmaßnahmen / Risks and mitigations

| Risiko / Risk | Gegenmaßnahme / Mitigation |
|---|---|
| Frühere positive Aussage wird unkritisch übernommen | Aktuelle Evidenzpflicht; früheres Feature als alleiniger positiver Beleg validatorseitig ablehnen. |
| Zähler driften zwischen JSON und Markdown | Alle Summen berechnen; Projektionen bytegenau rendern und prüfen. |
| Presetanzahl oder Version wird veraltet | Registry zur Laufzeit lesen; beim Planen beobachtete 12 nur als Vergleich. |
| GSDB-Datei wird doppelt oder gar nicht gezählt | Physik-/Manifestabschluss, Pfadeindeutigkeit und Rollenaggregation prüfen. |
| Menschliche/Provider-Grenze wird lokal behauptet | Proof-boundary-Typen und positive Evidenzregeln erzwingen. |
| Review führt versehentlich zu Reparatur | Scope-Firewall und `implementedInFeature046=false`; nur dokumentierte Folgehinweise. |
| Gemeinsame Evidenzdateien kollidieren | Alle shared writers serialisieren und vor jedem Schreibschritt neu lesen. |
| Merge-Fakten werden vorweggenommen | Pre-/Post-Merge-Trennung und kausaler Closeout. |
| Zeilenenden erzeugen plattformabhängige Hashes | LF-Normalisierung für Text; Raw-Bytes nur für Binärdaten. |

## Komplexitätsnachweis / Complexity tracking

Keine Verfassungsverletzung ist erforderlich. Die einzige bewusst zusätzliche Komponente ist ein test-only Validatorfile im vorhandenen Testprojekt. Das ist kleiner und besser wartbar als ein neues Tool, Skript oder Projekt. Die umfangreiche JSON-Struktur ist durch die unabhängige Reproduzierbarkeit des 157er Reviews gerechtfertigt und bleibt außerhalb des Produkts.

No constitution violation is required. The only intentionally added component is one test-only validator file in an existing test project. This is smaller and more maintainable than a new tool, script, or project. The larger JSON structure is justified by independent reproducibility of the 157-control review and remains outside the product.

## Phase-1-Entwurfsabschluss / Phase 1 design completion

- Forschung: [research.md](research.md)
- Datenmodell: [data-model.md](data-model.md)
- Akzeptanzvertrag: [contracts/gsdb-review-acceptance-contract.md](contracts/gsdb-review-acceptance-contract.md)
- Quickstart: [quickstart.md](quickstart.md)
- Autonome Gates: [autonomous-gate-requirements.json](autonomous-gate-requirements.json)

Kein Agentenkontext wird in dieser Planphase aktualisiert. Der Plan führt keine neue projektweite Technologie oder Anleitung ein; die aktive Agenten-/Preset-Parität wird später als Review-Evidenz geprüft. Das entspricht der Plan-Skill-Regel, Kontext nur bei tatsächlicher projektgeführter Generatorpflicht zu ändern.

No agent context is updated in this planning phase. The plan introduces no new project-wide technology or instruction; active agent/preset parity is assessed later as review evidence. This follows the Plan skill rule to change context only when a project-owned generator is actually required.
