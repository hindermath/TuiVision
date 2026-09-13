# Intake Sequencing Runbook

## Create

Name every existing target, proposed order, root, edge, type, and lifecycle
state. Publish only after explicit approval and both validators pass.

## Read

Summarize identity, target count, roots, dependencies, statuses, blockers, and
eligible targets. Do not modify files.

## Update

Require current authority, archive the accepted predecessor, prepare all files,
validate, and publish atomically with `supersedes` evidence.

## Delete

Archive manifest and receipt, create a tombstone, and keep intake documents.

## Status And Next

Validate hashes and graph read-only. `next` reports candidates but never starts
Review, Specify, Autonomous, or Parallel Autonomous.

When schema 2.0 is configured, resolve the Series manifest and target paths
through `requirements/intake-governance-config.json`. Treat
`RequirementsGovernanceGate` as a binding predecessor edge. Require one evidenced `Eligible` target for an active delivery series and none for a `Completed` series, but never interpret eligibility as implementation,
remote-delivery, bypass, or follow-on authority.

If the series has no members, use `status: "Idle"` with
empty `orderedTargets`, `roots`, and `dependencies`. Status and next must report
this state without inventing a placeholder target. Any target or edge makes an
idle series invalid.

*Hat die Serie keine Mitglieder, wird `Idle` mit drei leeren
Listen verwendet. Status und Next erklären diesen Zustand textorientiert und
erfinden keinen Platzhalter.*

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

## Physische Pfadgrenzen / Physical path boundaries

DE: Collection-Pfade muessen sich auf unterschiedliche Orte innerhalb des
Repositories aufloesen. Ein aktives Ziel darf auch ueber Symlinks nicht im Archiv
liegen. Unbekannte Serien- oder Zielzustaende sind Fehler. Bestehende Receipt-Ziele
und Repository-Dateiquellen werden vor dem Lesen einschliesslich ihrer
Elternverzeichnisse auf Containment geprueft. Ein Fehler erteilt keine Reparatur-
oder Ausfuehrungsbefugnis und aendert keine historische Evidence.

EN: Collection paths must resolve to distinct in-repository locations. An active
target cannot reside in the archive through a symlink. Unknown series or target
states are errors. Existing receipt targets and repository file sources undergo
containment checks, including parent directories, before reading. A failure
grants no repair/execution authority and changes no historical evidence.

In SeriesManifest mode, an Idle series may coexist with active standalone intakes;
physical active inventory and series membership remain separate. DirectoryStrict
requires the active inventory to be empty for an Idle series.

Im Modus SeriesManifest darf eine Idle-Serie neben aktiven Standalone-Intakes
bestehen. Physischer Aktivbestand und Serienmitgliedschaft bleiben getrennt.
DirectoryStrict verlangt fuer Idle einen leeren aktiven Bestand.
