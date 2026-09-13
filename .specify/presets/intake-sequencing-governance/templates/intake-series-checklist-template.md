# Intake Series Checklist

- [ ] A schema-2.0 requirements configuration resolves all target paths through
      the declared portable roles and collection paths.
- [ ] Every target path is explicit, unique, repository-relative, and hashed.
- [ ] Visible order contains every target exactly once.
- [ ] Roots equal the zero-indegree target set.
- [ ] Every edge uses an accepted type and correct binding flag.
- [ ] `RequirementsGovernanceGate` is used only for a binding predecessor that
      must complete before the dependent intake becomes eligible.
- [ ] An active delivery series has exactly one current `Eligible` target; a `Completed` series has none; eligibility
      grants no delivery authority.
- [ ] An `Idle` series has zero targets, roots, dependencies, and eligible
      candidates.
- [ ] The graph is order-consistent and acyclic.
- [ ] Material ambiguity is recorded as `NeedsClarification`.
- [ ] Write authority is current and bounded.
- [ ] Read, status, and next remain read-only.
- [ ] No downstream command is started implicitly.
- [ ] Bash and PowerShell validators agree.

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
