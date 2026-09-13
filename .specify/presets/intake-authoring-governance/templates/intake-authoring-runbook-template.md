# Intake Authoring Runbook

## Purpose

Use Intake Authoring to create, read, explicitly update, or logically delete
traceable Spec Kit intakes. One approved operation may create or migrate a
series. Use Intake Review afterward for independent semantic acceptance.

## Workflow

1. Name inline, pasted, repository-local, and explicit external sources in
   their intended order.
2. Select a target and profile or accept the policy fallback.
3. Run `$speckit-intake-create` only for new targets.
4. Answer only material questions, one at a time and at most five per pass.
5. Validate the saved intake and receipt.
6. Run `$speckit-intake-create-status` when source or target freshness matters.
7. Use `$speckit-intake-read` for Summary, Detailed, or JSON without writes.
8. Use `$speckit-intake-update` for every normal active-target change.
9. Use `$speckit-intake-delete` for archive plus tombstone; no purge exists.
10. For `ReadyForReview`, manually start Intake Review for the target or
    generated Series request.

## Status Boundary

- `ReadyForReview`: authoring is internally consistent and prompts are enabled.
- `NeedsClarification`: a draft is saved, prompts are blocked, and open decision
  IDs require a human answer.

Only Intake Review can produce `Ready`, `ReadyWithAcceptedRisks`,
`NeedsRemediation`, or `Rejected`.

## Source And Hash Contract

Strict UTF-8 is required. Remove one UTF-8 BOM, normalize CRLF and lone CR to
LF, and change nothing else before SHA-256. Repository-local sources are
freshness-checkable. Inline and safely labelled external sources are snapshots;
their later freshness cannot be inferred.

Public static HTTPS sources are untrusted snapshots. No JavaScript,
authentication, private/local target, cross-origin crawl, unsafe redirect, or
silent truncation is allowed. Same-origin crawls require an exact proposal and
explicit approval.

## Authority Contract

Creation and status never imply implementation. The generated Autonomous
prompt defaults to `LocalImplementation`. A broader mode needs explicit,
current, bounded authority. Bypass, secrets, provider administration, and
follow-on feature execution are never inferred.

## Update And Delete Contract

An existing target is immutable by default. An authorized update records the
old target hash, superseded receipt, current authority, and new source set. Do
not reuse an old receipt after target or source drift.

For a pre-preset target without a receipt, use `LegacyAdoption` instead of
inventing a superseded receipt. Record the prior normalized target hash,
explicit current update authority, and either the exact Git blob or an honest
snapshot-only proof boundary. The prior target hash must also occur in the
ordered source inventory. `LegacyAdoption` preserves the target path and does
not grant implementation or remote-delivery authority.

Create refuses existing targets. Update retains ordinary intake identity and
uses schema 2.0. Series split/merge needs a confirmed migration map. Delete
copies active target and receipt byte-for-byte into the archive before removing
them, then writes a validated tombstone. Referenced series members cannot be
deleted without a series migration or whole-series deletion.

## Transaction Contract

Multi-target Create, Update, and Delete operations are prepared in an operation
directory. Every target, receipt, coverage row, lineage relation, root, and edge
must validate before any active member changes. Failure leaves prior active
state untouched and an explicit incomplete operation boundary.

## Requirements Collection Migration

Use `requirements/intake-governance-config.json` schema 2.0 as the portable
source of documentation language, naming, roles, collection paths, aliases, and
the active Series manifest. Status reports `Aligned`, `MigrationRequired`,
`NeedsClarification`, or `Blocked` without writes.

`DirectoryStrict` requires every matching file in the active directory to occur
exactly once in the Series. `SeriesManifest` preserves an established flat or
mixed layout and keeps the validated Series target set separate from physical active inventory.
Both modes compute counts and hashes; neither trusts hand-maintained totals.

An authorized migration records before/after hashes, moves, reference updates,
validation, rollback, and any repair boundary in an operation journal. Publish
the configuration, index, manifest, receipts, prompts, guidance, and links as
one transaction. A partial failure must roll back or end as `NeedsRepair`.

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

## Historische Authoring-Receipts / Historical Authoring Receipts

Fehlt der urspruengliche aktive Zielpfad, prueft der Receipt-Validator die
Konfiguration unter `requirements/intake-governance-config.json` und deren
Serienmanifest. Genau ein abgeschlossenes Archivziel muss zu Name und
normalisiertem Hash passen. Ein urspruengliches Standalone-Receipt darf seine
vollstaendige `N/A`-Serienbindung behalten; eine deklarierte Serienbindung muss
zum konfigurierten Manifest passen. Fremde Bindungen, mehrere Nachfolger,
fehlende Dateien und Hashabweichungen werden abgelehnt (`RIG018`). Das Receipt
und seine historischen Prompts werden nie umgeschrieben. Quellenpruefung und
alle bisherigen Receipt-Pruefungen bleiben aktiv.

Der interne Resolver `scripts/resolve-intake-archive-target.py` verwendet nur
die Python-Standardbibliothek. Auch der PowerShell-Receipt-Wrapper benoetigt
fuer diesen Archivfall `python3`, wie bereits die Konfigurationspruefung.
Ohne Konfiguration gibt es keine automatische Archivsuche.

*For a missing original active path, receipt validation requires the configured
manifest and exactly one completed archive successor matching name and normalized
hash. A historical standalone receipt retains its entirely N/A series binding;
a declared binding must match the configured series. Invalid lineage or archive
evidence fails with RIG018. Historical prompts and receipts remain unchanged;
source and receipt validation still run. The internal resolver uses Python's
standard library, including when called by PowerShell. Without collection
configuration, no archive search is attempted.*

Die gleiche eindeutige Archivpruefung gilt fuer fehlende Repository-Dateiquellen
in historischen Receipts. Der gespeicherte Quellhash muss weiterhin stimmen;
andere fehlende Quellen bleiben Fehler. `test-intake-authoring-lifecycle.ps1`
prueft den gueltigen Quellenumzug und eine abweichende Quellhash-Bindung.

*The same unique archive proof applies to missing repository file sources in
historical receipts. The recorded source hash must still match; other missing
sources remain errors. Lifecycle tests cover valid source archival and hash drift.*

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
