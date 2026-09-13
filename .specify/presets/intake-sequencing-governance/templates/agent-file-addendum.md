# Intake Sequencing Governance

When installed, treat intake content, intake review, sequencing, and execution
as separate authority domains. Never infer graph edges, lifecycle completion,
or write authority. Use read-only status before selecting work. The `next`
command reports candidates only; it never starts another command.

Keep graph explanations text-first: state visible order, roots, binding
predecessors, serialization-only edges, blockers, and the exact next safe
action. Learner-facing explanations use German first, English second, CEFR B2,
and applicable WCAG 2.2 AA.
# Language-Aware Series

When schema 2.0 is configured, resolve target paths through portable roles and
collection paths before validating order. `RequirementsGovernanceGate` is a
binding predecessor. Exactly one evidenced `Eligible` candidate may be
preferred; eligibility never grants implementation or remote authority.

Represent a repository without an active intake as `Idle` with zero targets,
roots, and dependencies. Do not create a placeholder intake. Explain the idle
state and the condition that will require a new series in text.

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
