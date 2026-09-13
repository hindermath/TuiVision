# Intake Review Checklist

## Single Intake

- [ ] Identity, audience, goal, scope, and non-goals are explicit.
- [ ] Assumed prior knowledge is explicit; Spec Kit experience is not silently
      assumed for first-time learner audiences.
- [ ] Technical and workflow terms are explained on first use at the declared
      readability level and in the project language order.
- [ ] Dependencies, status, decisions, and next actions are understandable as
      ordered text and do not rely only on colour, diagrams, or visual position.
- [ ] Requirements are atomic, testable, and free of conflicting modal terms.
- [ ] Acceptance criteria and validation evidence are measurable.
- [ ] Dependencies, ordering, authority, delivery, risks, and follow-ups are explicit.
- [ ] Security, privacy, accessibility, platform, and supply-chain applicability is decided.
- [ ] References and embedded Specify/Autonomous prompts match the normative text.
- [ ] No credentials, secrets, unnecessary personal data, or binary content is present.

## Series

- [ ] Schema-2.0 documentation language is explicit and independent of
      implementation language and locale.
- [ ] Naming profile, four portable roles, six collection paths, canonical
      index, bounded aliases, and computed inventory agree.
- [ ] Every active intake occurs once in the configured Series with a current
      normalized SHA-256; exactly one candidate is `Eligible` in an active delivery series, none in a `Completed` series.
- [ ] IDs, order, dependency graph, handoffs, and future-scope boundaries are consistent.
- [ ] Schema 1.1 binds the repository-relative request path and normalized SHA-256.
- [ ] Every target occurs exactly once in `orderedTargetPaths`.
- [ ] Declared roots equal the graph's zero-indegree targets.
- [ ] Every non-root has an incoming edge; edges are unique, known, ordered, and acyclic.
- [ ] No unexplained gap, overlap, duplicate ownership, or terminology drift remains.
- [ ] Shared invariants are stated once and consumed consistently.

## Campaign

- [ ] Every `featureInput` exists and has one current target review.
- [ ] Every worker has an applicability row; duplicate intake use reuses semantic review.
- [ ] Manifest DAG, base refs, repositories, runner constraints, and intake ordering agree.
- [ ] Operator exceptions are separate, explicitly owned, dated, justified, and expiring.

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
