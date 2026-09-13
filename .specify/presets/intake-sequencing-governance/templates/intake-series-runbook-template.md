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

If the repository has no active intake of its own, use `status: "Idle"` with
empty `orderedTargets`, `roots`, and `dependencies`. Status and next must report
this state without inventing a placeholder target. Any target or edge makes an
idle series invalid.

*Hat das Repository keinen eigenen aktiven Intake, wird `Idle` mit drei leeren
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
