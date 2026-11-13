# Intake Review Governance Preset

<!-- current-package: 0.2.9 -->

Paketkennung: **v0.2.9**. [Vorbereitung und Veröffentlichungsgrenze](docs/release-0.2.9-validation.md). Die Release-Seite und der externe Archivnachweis belegen die Veröffentlichung, nicht dieser Vorbereitungsstand. Frühere Versionsnachweise unten bleiben historisch.

<span lang="en">Package identity: v0.2.9. The release page and external archive evidence prove publication, not this preparation snapshot. Earlier version records below remain historical.</span>

[Release-Abschluss und Verbesserungsprofil / Publication closeout and improvement profile](docs/release-0.2.6-validation.md).

Der unten verlinkte Basispaket-Nachweis bleibt historisch; dieses Paket nimmt die Quellenkorrekturen als neue Version auf.

<span lang="en">The linked base-package evidence remains historical; this package includes the source corrections as a new version.</span>

**Leserpfad / Reader path:** [Deutsch: Einstieg, Begriffe und Fehlerhilfe](docs/user-guide.md) · [English: start, terms and troubleshooting](docs/user-guide.EN.md).

Historisches Basispaket **v0.2.6**. Der [Release-Stand](https://github.com/hindermath/spec-kit-preset-intake-review-governance/releases) wird getrennt geprüft; ein Commit auf main ist noch keine Veröffentlichung oder Installation.

*Historical base package v0.2.6. The release page records actual publication separately; a main-branch commit is not publication or installation.*


Patch **0.2.6** liefert die verbesserte zweisprachige Einstiegs- und
Kompositionsdokumentation. Verhalten und Berechtigungsgrenzen bleiben erhalten.
[Release-Nachweis / Release record](docs/release-0.2.6-validation.md).

*Patch 0.2.6 packages the improved bilingual getting-started and composition
guide. Behavior and authority boundaries remain unchanged.*

Paketversion / Package version: **0.2.8**. Laufende `Active`-Serien mit
mindestens einem `Active`-Mitglied bleiben ohne weiteren `Eligible`-Kandidaten
gueltig; `eligibleCandidate` ist dann `N/A`. `Ready` verlangt weiterhin genau
einen Kandidaten. Mehrfachkandidaten, Hash-, Pfad- und Abhaengigkeitsfehler
bleiben gesperrt. Schema, Commands und Prioritaet bleiben gleich.

Running Active series with an Active member may have no additional Eligible
candidate. The candidate remains N/A; this grants no execution authority.
Ready still requires exactly one candidate, and all integrity checks remain.
Documentation target combination (not a new field-test claim): Authoring 0.3.10, Review 0.2.7, Sequencing 0.2.11.

Fuer die Python-basierten Pruefungen eine gepflegte Python-Version verwenden.
macOS-System-Python 3.9 kann gueltige UTC-Zeitstempel mit sieben Nachkommastellen
abweisen. Die Release-Pruefung verwendet lokal Python 3.14 und in Intake-CI 3.12.
*Use a maintained Python runtime for Python-backed validation. macOS system
Python 3.9 can reject valid UTC timestamps with seven fractional digits.
Release validation uses Python 3.14 locally and 3.12 in Intake CI.*
Each preset remains independently installable; historical evidence is preserved.

Gemeinsame Konfigurationsvalidatoren akzeptieren auch die bereits etablierte,
leere `Idle`-Serie. `Idle` darf keine Ziele, Wurzeln oder Abhaengigkeiten
enthalten; DirectoryStrict verlangt zusaetzlich ein leeres aktives Inventar.
Indizes eigenstaendiger verschachtelter Git-Repositories sind keine Duplikate
des uebergeordneten Projekts. Gewoehnliche doppelte Indizes bleiben gesperrt.

*The shared validators also accept established empty Idle series, without
targets, roots or dependencies. DirectoryStrict additionally requires an
empty active inventory. Nested Git checkouts own their index; ordinary
duplicate indexes still fail. This aligns all three Intake presets.*

Version **0.2.3** schliesst physische
Collection-Aliase und unbekannte Lifecycle-Zustaende aus. Authoring prueft auch
bestehende Receipt-Ziele und Quellen vor dem Lesen gegen die Repository-Grenze.

This patch rejects physical collection aliases and unknown lifecycle states.
Authoring also checks existing receipt targets and sources for repository
containment before reading. Earlier feature versions below describe history.
See [boundary hardening](docs/lifecycle-boundary-hardening.md).

Optional, stackable intake-quality governance for GitHub Spec Kit. Version
`0.2.1` publishes the agent-neutral `model-routing.json` contract. Version
`0.2.0` provides three commands, eight templates, and read-only
Bash/PowerShell validators. Series reviews use a schema-1.1 request binding
with normalized SHA-256, explicit roots, complete target ordering, and
validated acyclic dependency edges. Single and Campaign schema 1.0 results
remain compatible. Recommended priority: `65`, between Agent Parity (`60`) and
Autonomous Run Governance (`70`). Spec Kit `>=0.8.3` is required.

Version `0.2.6` also reviews a project-declared learner contract. It verifies
audience and prior knowledge, first-use explanations, language and readability,
and text-first dependencies, status, decisions, and next actions without
making those project choices itself.

## Commands

### Einstieg und Ergebnisdateien / Getting Started and Result Files

Voraussetzung: initialisiertes Zielprojekt, Spec Kit `>=0.8.3`, eindeutige
Intake-/Serien-/Kampagnenziele und relevante Policy. Das [Manifest](preset.yml)
liefert eigene Commands und Policy-, Request-, Result-, Report-, Checklisten-
und Runbook-Starter. Installation erstellt keinen fertigen Review und
aktiviert keinen projektweiten Pflicht-Gate ohne dessen Policy.

*Prerequisites: initialized target project, Spec Kit >=0.8.3, explicit
single/series/campaign targets and policy. The [manifest](preset.yml) supplies
standalone commands and policy, request, result, report, checklist/runbook
starters. Installation creates no completed review or policy activation.*

„Read-only Review“ schützt die Intake-Zieldateien: Die Review-Anweisungen
verlangen Result und Report außerhalb dieser Ziele. Status und Validatoren
prüfen lesend. Repair benötigt Änderungsautorität und vollständigen Re-Review.
Ein Starter ist noch kein Review-Ergebnis.

*Read-only review protects intake targets: review instructions require result
and report outside those targets. Status/validators inspect without writes.
Repair needs mutation authority and full re-review. A starter is not a result.*

Beispiel in der Agentenoberfläche / Example agent invocation:

```text
DE: $speckit-intake-review Pruefe intakes/order-import-validation.md im
Modus Single. Veraendere den Intake nicht; benenne Result-/Report-Pfade.
EN: $speckit-intake-review Review intakes/order-import-validation.md in
Single mode. Preserve the intake; report result and report paths.
```

Findings behandeln, notwendige Reparatur separat beauftragen, danach mit
`$speckit-intake-review-status` die Frische prüfen. `ReadyWithAcceptedRisks`
braucht menschliche Risikoannahme; auch `Ready` startet keine Folgephase.
Drift klären, statt Result-Hashes zum Bestehen umzuschreiben.

*Address findings, separately authorize repair, then check freshness with
intake-review-status. ReadyWithAcceptedRisks needs human risk acceptance;
even Ready starts no next phase. Resolve drift, never forge result hashes.*

Priorität 65 startet keinen Review nach Authoring. Kleinere Zahlen haben
höhere Auflösungspriorität; Presets/Overrides können gleichnamige Starter
ersetzen. Eigene Reihenfolgen separat prüfen; Hashbindung, Frische und
Policy-Gates bleiben fachliche Bedingungen.

*Priority 65 does not execute review after authoring. Lower numbers have higher
precedence; presets/overrides can replace same-name starters. Check custom
orderings separately; hashes, freshness and policy gates remain requirements.*

Leserpfad / Reader path: [Install](#install) → [Commands](#commands) →
[Safety](#safety) → [Completed Series](#abgeschlossene-serien--completed-series).

- `$speckit-intake-review`: read-only semantic review for a single intake,
  ordered series, or parallel campaign.
- `$speckit-intake-repair`: explicitly authorized repair followed by mandatory
  full re-review.
- `$speckit-intake-review-status`: read-only freshness and consumer-gate check.

## Install

```bash
specify preset add --from https://github.com/hindermath/spec-kit-preset-intake-review-governance/archive/refs/tags/v0.2.9.zip --priority 65
specify preset list
specify preset resolve intake-review-policy-template
```

Installing the preset does not silently change the standard eight-preset
profile. Projects activate the gate through the policy template; campaigns may
activate it through their schema-1.2 `intakeReview` object.

## Safety

Review and status never modify intake targets. Repair needs explicit mutation
authority. An agent cannot accept residual risk or invent an operator
exception. The validators are read-only and grant no delivery authority.

An accepted Series result must use schema 1.1 and bind its repository-relative
request through `requestEvidence`. The validators reject request drift,
identity or role mismatch, incomplete ordering, unknown or duplicate edges,
cycles, and roots that differ from the graph's zero-indegree targets.
## Requirements Collections / Requirements-Sammlungen

Version 0.2.1 reviews `requirements/intake-governance-config.json` schema 2.0
together with the selected intake or Series. It validates BCP-47 documentation
language, naming profile, portable artifact roles, resolved paths, computed
inventory, canonical index, hashes, receipts, references, and eligibility.
Implementation language and locale are not language evidence. `Ready` and
`Eligible` grant no implementation or remote permission.

*Version 0.2.1 prüft sprachbewusste Requirements-Sammlungen zusammen mit Intake
oder Series. Dokumentationssprache, Rollen, Pfade, Hashes, Referenzen und
Eligibility bleiben nachvollziehbar; Review-Erfolg ist keine Lieferfreigabe.*

## Abgeschlossene Serien / Completed Series

`Completed`-Mitglieder liegen in der konfigurierten Archivsammlung. Noch
nicht abgeschlossene Serienmitglieder liegen in der aktiven Sammlung;
Backlog und History sind keine ausfuehrbaren Serienquellen. Eine laufende
Serie darf archivierte Vorgaenger enthalten. Eine abgeschlossene Serie
behaelt ihre Mitglieder und hat null `Eligible`-Ziele (`eligibleCandidate: N/A`).

`activeIntakeCount` zaehlt die physischen passenden Dateien direkt in der
aktiven Sammlung; das zusaetzliche Feld `activeSeriesTargetCount` zaehlt nur
aktive Serienmitglieder. `seriesTargetCount` umfasst auch archivierte Mitglieder.
`SeriesManifest` erlaubt eigenstaendige aktive Intakes ausserhalb der Serie.
Ein fehlendes leeres Aktivverzeichnis zaehlt dort als null; ein Dateipfad statt
eines Verzeichnisses bleibt ungueltig. `DirectoryStrict` verlangt das aktive
Verzeichnis und gleicht dessen Bestand mit den aktiven Serienmitgliedern ab.

Die Pruefung meldet Status-/Ablagewidersprueche als `RIG017`, verschiebt aber
keine Dateien. Eine Korrektur benoetigt einen eigenen Aenderungsauftrag.

*Completed members belong to the configured archive. Non-completed members
belong to the active collection; backlog and history are not executable
sources. Active series may retain archived predecessors. Completed series
retain all members and expose no eligible candidate. Physical active files,
active series members, and all series members have separate counts.
SeriesManifest permits standalone active intakes and an absent empty active
directory. DirectoryStrict requires that directory and compares its contents
with active series members. RIG017 reports lifecycle mismatches without moving
files or granting repair authority.*

Pruefnachweis und Release-Grenzen / Validation and release boundaries: [Lifecycle evidence](docs/completed-series-lifecycle.md).
