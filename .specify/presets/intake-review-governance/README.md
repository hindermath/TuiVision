# Intake Review Governance Preset

Aktuelle Version / Current version: **0.2.3**. Dieser Patch schliesst physische
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

Version `0.2.0` also reviews a project-declared learner contract. It verifies
audience and prior knowledge, first-use explanations, language and readability,
and text-first dependencies, status, decisions, and next actions without
making those project choices itself.

## Commands

- `$speckit-intake-review`: read-only semantic review for a single intake,
  ordered series, or parallel campaign.
- `$speckit-intake-repair`: explicitly authorized repair followed by mandatory
  full re-review.
- `$speckit-intake-review-status`: read-only freshness and consumer-gate check.

## Install

```bash
specify preset add --from https://github.com/hindermath/spec-kit-preset-intake-review-governance/archive/refs/tags/v0.2.3.zip --priority 65
specify preset list
specify preset resolve
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
