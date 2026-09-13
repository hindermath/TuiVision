# Intake Sequencing Governance

Optional Spec Kit preset for managing the order and lifecycle of existing
intakes. Version `0.2.3` uses priority `66`: after Intake Review at `65` and
before Autonomous Run at `70`.

## Why This Preset Exists

An order table alone cannot distinguish a binding predecessor from a preferred
delivery order. This preset stores both a learner-readable order and a
machine-checkable typed graph. It never writes intake content and never starts
the work it selects.

Version `0.2.3` publishes the agent-neutral `model-routing.json` contract for
composition with Model Routing Governance. Sequencing behavior is unchanged.

Version `0.2.2` preserves the project-declared learner contract in the readable
order: audience, prior knowledge, language and readability, first-use terms,
and a normative text representation of dependencies, blockers, status,
decisions, and next actions.

Version `0.2.2` adds the explicit state `Idle` for a repository that currently
has no active intake of its own. An idle series has no targets, roots, or
dependencies. This state prevents tools from inventing a placeholder intake
only to satisfy a non-empty graph rule.

*Version `0.2.2` ergänzt den ausdrücklichen Zustand `Idle` für ein Repository
ohne eigenen aktiven Intake. Eine solche Serie enthält keine Ziele, Roots oder
Abhängigkeiten. Werkzeuge dürfen dafür keinen künstlichen Platzhalter erzeugen.*

Version `0.2.2` keeps canonical-index validation inside the current Git
repository boundary. An index owned by a nested repository is not a duplicate
of its parent index, while an ordinary second index in the same repository
still blocks validation.

*Version `0.2.2` begrenzt die Indexprüfung auf das aktuelle Git-Repository. Der
Index eines verschachtelten Repositories ist kein Duplikat des Eltern-Indexes.
Ein gewöhnlicher zweiter Index im selben Repository bleibt ein Fehler.*

## Installation

```bash
specify preset add \
  --from https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/archive/refs/tags/v0.2.4.zip \
  --priority 66
```

## Commands

| Command | Writes | Purpose |
|---|---:|---|
| `$speckit-intake-series-create` | Yes | Create one new series |
| `$speckit-intake-series-read` | No | Summarize order and graph |
| `$speckit-intake-series-update` | Yes | Supersede one series |
| `$speckit-intake-series-delete` | Yes | Archive and tombstone a series |
| `$speckit-intake-series-status` | No | Validate current state |
| `$speckit-intake-series-next` | No | List eligible targets or blockers |

## Example

```text
A --> B --> C
```

If `A` is completed, `B` may be eligible. `next` reports that fact, but does
not invoke Intake Review, Specify, Autonomous, or Parallel Autonomous.

For an `Idle` series, `next` reports that no active intake exists and returns no
candidate. `Idle` is invalid as soon as a target, root, or dependency is
present.

## Edge Types

Binding types model real prerequisites. `PreferredSerialOrder` and
`SharedWriterSerialization` coordinate delivery without pretending that one
feature is functionally required by another.

## Safety

- strict UTF-8 and normalized SHA-256 evidence;
- repository-relative paths only;
- no source execution;
- fail-closed ambiguity and drift;
- explicit authority for create, update, and delete;
- archive plus tombstone instead of physical purge;
- read-only status and next commands.

## Accessibility

Order, roots, dependencies, blockers, and next actions are always available as
text. Color or a graphical diagram is never the only information carrier.

## Composition

The preset composes with Intake Authoring `64`, Intake Review `65`, Autonomous
Run `70`, and Parallel Autonomous `80`. Priority controls merge order only; it
does not grant execution or remote authority.
`RequirementsGovernanceGate` is a binding predecessor used when one shared
requirements migration must finish before existing roots are released. Under
schema 2.0, target paths are resolved from portable roles and collection paths.
At most one target may explicitly declare `Eligible`; that state selects order
only and grants no implementation or remote authority. A valid `Idle` series
has no eligible target.

*`RequirementsGovernanceGate` sperrt bestehende Roots bis zum gemeinsamen
Requirements-Abschluss. Schema 2.0 löst Pfade über Rollen auf. Höchstens ein
Ziel darf `Eligible` sein; daraus entstehen keine Lieferrechte.*

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
