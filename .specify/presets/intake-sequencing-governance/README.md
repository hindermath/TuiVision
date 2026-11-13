# Intake Sequencing Governance

<!-- current-package: 0.2.14 -->

Paketkennung: **v0.2.14**. [Vorbereitung und Veröffentlichungsgrenze](docs/release-0.2.14-validation.md). Die Release-Seite und der externe Archivnachweis belegen die Veröffentlichung, nicht dieser Vorbereitungsstand. Frühere Versionsnachweise unten bleiben historisch.

<span lang="en">Package identity: v0.2.14. The release page and external archive evidence prove publication, not this preparation snapshot. Earlier version records below remain historical.</span>

[Release-Abschluss und Verbesserungsprofil / Publication closeout and improvement profile](docs/release-0.2.10-validation.md).

Der unten verlinkte Basispaket-Nachweis bleibt historisch; dieses Paket nimmt die Quellenkorrekturen als neue Version auf.

<span lang="en">The linked base-package evidence remains historical; this package includes the source corrections as a new version.</span>

**Leserpfad / Reader path:** [Deutsch: Einstieg, Begriffe und Fehlerhilfe](docs/user-guide.md) · [English: start, terms and troubleshooting](docs/user-guide.EN.md).

Historisches Basispaket **v0.2.10**. Der [Release-Stand](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/releases) wird getrennt geprüft; ein Commit auf main ist noch keine Veröffentlichung oder Installation.

*Historical base package v0.2.10. The release page records actual publication separately; a main-branch commit is not publication or installation.*


Patch **0.2.10** liefert die verbesserte zweisprachige Einstiegs- und
Kompositionsdokumentation. Verhalten und Berechtigungsgrenzen bleiben erhalten.
[Release-Nachweis / Release record](docs/release-0.2.10-validation.md).

*Patch 0.2.10 packages the improved bilingual getting-started and composition
guide. Behavior and authority boundaries remain unchanged.*

Paketversion / Package version: **0.2.13**. Der Installationsbefehl bindet
das genaue Release-ZIP. Dieses Dokumentationspatch beschreibt die in 0.2.12
eingeführten Sicherheitsprüfungen: sichere repository-relative Pfade,
Symlink-Grenzen, konsistente Abschlussstatus und gebundene Archivketten.
Der Patch 0.2.13 ändert diese Validatorlogik nicht. Bestehende Tags und
Archive bleiben erhalten. Siehe [Patch-Nachweis](docs/release-0.2.13-validation.md).

Package **0.2.13** corrects the description of the security checks introduced
in 0.2.12: safe repository-relative paths, symlink boundaries, consistent
completion states and bound archive chains. This documentation patch does
not change that validator logic. The installation command pins the exact
release ZIP; historical tags and archives remain unchanged.
See [patch evidence](docs/release-0.2.13-validation.md).

Version **0.2.7**: Laufende `Active`-Serien mit
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

Version **0.2.6** schliesst physische
Collection-Aliase und unbekannte Lifecycle-Zustaende aus. Authoring prueft auch
bestehende Receipt-Ziele und Quellen vor dem Lesen gegen die Repository-Grenze.

This patch rejects physical collection aliases and unknown lifecycle states.
Authoring also checks existing receipt targets and sources for repository
containment before reading. Earlier feature versions below describe history.
See [boundary hardening](docs/lifecycle-boundary-hardening.md).

Optional Spec Kit preset for managing the order and lifecycle of existing
intakes. Version `0.2.3` uses priority `66`: after Intake Review at `65` and
before Autonomous Run at `70`.

## Why This Preset Exists

### Einstieg und Übergabe / Getting Started and Handoff

Voraussetzung: initialisiertes Zielprojekt, Spec Kit `>=0.8.3`, vorhandene
Intakes und geklärte Abhängigkeiten. Ein Serienmanifest beschreibt Ziele,
Reihenfolge und Kanten; ein DAG ist ein gerichteter Graph ohne Zyklen.
Das [Manifest](preset.yml) liefert eigene Serien-Commands und Starter,
keine nach Installation automatisch gestartete Reihenfolgeplanung.

*Prerequisites: initialized target project, Spec Kit >=0.8.3, existing intakes
and understood dependencies. A series manifest records targets, order and
edges; a DAG is a directed graph without cycles. The [manifest](preset.yml)
supplies standalone series commands/starters, not automatic scheduling.*

Beispiel: A ist Voraussetzung für B. Genau diese Serie mit begründeter Kante
vorschlagen lassen und Create ausdrücklich genehmigen. Danach
`$speckit-intake-series-status` und `$speckit-intake-series-next` prüfen.
Nach belegtem Abschluss von A kann B startfähig sein; `next` berichtet nur.
Review/Feature-Start separat beauftragen. Fehlende Abschlussbelege nicht erfinden.

*Example: A is required by B. Request that exact series proposal with a
justified edge and explicitly approve Create. Check series-status and
series-next. After proven completion of A, B may be eligible; next only reports.
Review/feature execution needs separate authority. Never invent completion.*

Kleinere Prioritätszahlen haben höhere Auflösungspriorität; Presets und lokale
Overrides können gleichnamige Starter ersetzen. Dies ersetzt keine bindenden
Kanten, Review-Frische oder Freigaben. Eigene Kombinationen separat prüfen.
Bei `RIG017` Status und Ablage unter Update-Autorität klären, nicht Dateien
nur zum Bestehen umbenennen.

*Lower numbers mean higher precedence; presets/overrides can replace same-name
starters. This replaces neither binding edges, review freshness nor authority.
Check custom combinations. Resolve RIG017 lifecycle/storage mismatches under
Update authority, not by renaming files just to pass.*

Leserpfad / Reader path: [Installation](#installation) → [Commands](#commands) →
[Example](#example) → [Composition](#composition) →
[Completed Series](#abgeschlossene-serien--completed-series).

Read-only Installationsprüfung / Read-only installation checks:

```bash
specify preset info intake-sequencing-governance
specify preset resolve intake-series-manifest-template
```

An order table alone cannot distinguish a binding predecessor from a preferred
delivery order. This preset stores both a learner-readable order and a
machine-checkable typed graph. It never writes intake content and never starts
the work it selects.

Version `0.2.10` publishes the agent-neutral `model-routing.json` contract for
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
specify preset add --from https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/archive/refs/tags/v0.2.14.zip --priority 66
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
