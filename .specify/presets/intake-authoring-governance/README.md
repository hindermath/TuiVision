# Intake Authoring Governance Preset

<!-- current-package: 0.3.13 -->

Paketkennung: **v0.3.13**. [Vorbereitung und Veröffentlichungsgrenze](docs/release-0.3.13-validation.md). Die Release-Seite und der externe Archivnachweis belegen die Veröffentlichung, nicht dieser Vorbereitungsstand. Frühere Versionsnachweise unten bleiben historisch.

<span lang="en">Package identity: v0.3.13. The release page and external archive evidence prove publication, not this preparation snapshot. Earlier version records below remain historical.</span>

[Release-Abschluss und Verbesserungsprofil / Publication closeout and improvement profile](docs/release-0.3.9-validation.md).

Der unten verlinkte Basispaket-Nachweis bleibt historisch; dieses Paket nimmt die Quellenkorrekturen als neue Version auf.

<span lang="en">The linked base-package evidence remains historical; this package includes the source corrections as a new version.</span>

**Leserpfad / Reader path:** [Deutsch: Einstieg, Begriffe und Fehlerhilfe](docs/user-guide.md) · [English: start, terms and troubleshooting](docs/user-guide.EN.md).

Historisches Basispaket **v0.3.9**. Der [Release-Stand](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/releases) wird getrennt geprüft; ein Commit auf main ist noch keine Veröffentlichung oder Installation.

*Historical base package v0.3.9. The release page records actual publication separately; a main-branch commit is not publication or installation.*


Patch **0.3.9** liefert die verbesserte zweisprachige Einstiegs- und
Kompositionsdokumentation. Verhalten und Berechtigungsgrenzen bleiben erhalten.
[Release-Nachweis / Release record](docs/release-0.3.9-validation.md).

*Patch 0.3.9 packages the improved bilingual getting-started and composition
guide. Behavior and authority boundaries remain unchanged.*

Paketversion / Package version: **0.3.13**. Der Installationsbefehl steht
auf einer Zeile und bindet das genaue Release-ZIP, damit die automatische
Community-Prüfung ihn direkt auswerten kann. Die Generatorvorlage bindet
0.3.13; bekannte Schema-2-Receipts einschließlich 0.3.6, 0.3.7, 0.3.8, 0.3.9, 0.3.10, 0.3.11 und 0.3.12 bleiben
gültig und unverändert. Dieser Patch gleicht die DE-/EN-Anleitung aneinander an.
Keine neue Command-, Schema-, Prioritäts- oder Ausführungsberechtigung.

The installation command is a single line bound to the exact release ZIP for
automatic community validation. The generator template binds 0.3.13 and keeps
known schema-2 receipts, including 0.3.6 through 0.3.12, valid without rewriting
them. This patch aligns the German/English instructions. No new command,
schema, priority or execution authority is introduced.
See [patch preparation and documentation](docs/release-0.3.9-validation.md).

Version **0.3.6**: Laufende `Active`-Serien mit
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

Version **0.3.5** veroeffentlicht die
Korrektur fuer Receipts der eigenen Generatorversion. Beide Validatoren
akzeptieren die aktuelle Vorlage und bekannte Schema-2-Generatoren, darunter
0.3.4; unbekannte Versionen und unzulaessige Schema-Kombinationen bleiben gesperrt.

This patch publishes current-generator receipt compatibility. Both validators
accept the shipped template and known schema-2 generators, including 0.3.4,
while rejecting unknown versions and invalid schema/version combinations.
Earlier physical-boundary and lifecycle hardening remains unchanged.
See [generator compatibility](docs/generator-version-compatibility.md).
See [boundary hardening](docs/lifecycle-boundary-hardening.md).

Optional, stackable intake-authoring governance for GitHub Spec Kit. Version
`0.3.1` publishes the agent-neutral `model-routing.json` contract. Version
`0.3.0` governs traceable intake Create, Read, Update, logical Delete, bounded
public HTTPS sources, and explicitly approved intake series.

Version `0.3.9` carries a project-profile learner contract into new or updated
intakes: declared audience, assumed prior knowledge, first-use terminology, and
text-first dependencies, status, decisions, and next actions. It does not
hard-code a country, occupation, or language into the portable preset.

*Optional, stackable intake-authoring governance for GitHub Spec Kit. Version
`0.3.1` adds the agent-neutral model-routing contract; the complete traceable
Create, Read, Update, logical Delete, HTTPS, and series contract remains intact.*

Recommended priority: `64`, after Agent Parity (`60`) and before Intake Review
(`65`), Autonomous Run (`70`), and Parallel Autonomous Run (`80`). Spec Kit
`>=0.8.3` is required.

## Why This Preset Exists / Warum dieses Preset existiert

### Einstieg und Phasengrenzen / Getting Started and Phase Boundaries

Ein Intake ist ein begrenzter Arbeitsauftrag; sein Receipt enthält Herkunft
und Hashbindungen. Das [Manifest](preset.yml) registriert eigene Commands und
Starter, keine automatisch gestartete Authoring-Phase vor jedem Specify.
Nach Installation Operation, Quellen, Policy und Ziel ausdrücklich wählen.
Leere JSON-Starter sind keine gültigen Receipts oder abgeschlossenen Vorgänge.

*An intake is a bounded work request; its receipt records provenance and
hashes. The [manifest](preset.yml) registers standalone commands/starters,
not automatic authoring before every Specify. Explicitly choose operation,
sources, policy and target. Empty JSON starters are not valid receipts or
completed operations.*

Beispiel: Genau einen neuen Intake aus `planning/order-import-notes.md`
erstellen lassen, materielle Fragen klären, Intake/Receipt sichten und Status
prüfen. Erforderlichen Intake-Review gesondert beauftragen. `ReadyForReview`
ist weder `Ready` noch Start-/Lieferfreigabe. Bestehende Ziele benötigen
autorisiertes Update; historische Receipts nicht passend umschreiben.

*Example: create exactly one new intake from planning/order-import-notes.md,
resolve material questions, inspect intake/receipt and check status. Separately
request any required Intake Review. ReadyForReview is not Ready or execution
authority. Existing targets need authorized Update; preserve old receipts.*

Priorität steuert die Auflösung, nicht die Abfolge Authoring → Review →
freigegebener Feature-Lauf. Kleinere Zahlen haben höhere Priorität;
gleichnamige Starter können durch andere Presets oder Overrides ersetzt
werden. Eigene Kombinationen prüfen. Status/Read starten keine Folgephase;
Installation migriert keine Sammlung.

*Priority controls resolution, not authoring -> review -> authorized execution.
Lower numbers have higher precedence; presets/overrides can replace same-name
starters. Check custom combinations. Status/Read start no next phase;
installation migrates no collection.*

Leserpfad / Reader path: [Install](#install) →
[Quick Start](#quick-start--schnellstart) → [Output](#output--ausgabe) →
[Status](#status--statuspruefung) → [Review Handoff](#review-handoff--uebergabe-an-review).

Planungen beginnen oft als Chattext, Notiz oder mehrere Dateien. Direktes
Specify aus solchen Quellen kann Scope, Nicht-Ziele, Nachweise oder
Berechtigungen vermischen. Dieses Preset erzeugt zuerst einen lesbaren,
hashgebundenen Intake. Das getrennte Intake-Review-Preset entscheidet danach,
ob der Intake wirklich bereit ist.

*Plans often begin as chat text, notes, or several files. Running Specify
directly can mix scope, non-goals, evidence, and authority. This preset first
creates a readable, hash-bound intake. The separate Intake Review preset then
decides whether that intake is actually ready.*

Creating an intake starts no review, feature, implementation, campaign, commit,
push, pull request, or merge.

## Commands / Befehle

- `$speckit-intake-create`: erzeugt einen neuen Intake oder eine ausdrücklich
  freigegebene Intake-Serie.
- `$speckit-intake-read`: fasst Intake oder Serie read-only als Summary,
  Detailed oder JSON zusammen.
- `$speckit-intake-update`: aktualisiert ausdrücklich einen aktiven Intake oder
  migriert eine freigegebene Serie.
- `$speckit-intake-delete`: archiviert aktive Artefakte und erzeugt einen
  Tombstone; v0.3.0 besitzt keinen Purge.
- `$speckit-intake-create-status`: prueft Receipt, Ziel, lokale Quellen und
  Prompt-Zustand read-only.

*Create writes only new targets. Read summarizes without writes. Update owns
normal changes. Delete uses archive plus tombstone. Status verifies provenance
and freshness without writing.*

## Install

```bash
specify preset add --from https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/archive/refs/tags/v0.3.13.zip --priority 64
specify preset list
specify preset info intake-authoring-governance
specify preset resolve intake-authoring-policy-template
```

The preset is optional. Installation alone does not require authoring or make
Intake Review mandatory. Repository policy chooses when either gate applies.

## Quick Start / Schnellstart

### Direct prompt / Direkter Prompt

```text
$speckit-intake-create Erstelle aus folgendem Text einen Intake mit dem Titel "Audit logging hardening": Die Anwendung soll Audit-Ereignisse nachvollziehbar speichern, aber keine Passwoerter, Tokens oder vollstaendigen Request-Bodies protokollieren.
```

### Pasted agent plan / Eingefuegte Agentenplanung

```text
$speckit-intake-create Wandle die folgende gemeinsam erarbeitete Planung in einen Spec-Kit-Intake um. Speichere das Ergebnis als intakes/order-import-validation.md.

[paste the planning text here]
```

### One source file / Eine Quelldatei

```text
$speckit-intake-create Nutze planning/order-import-notes.md als einzige Quelle und erzeuge genau einen Intake.
```

### Ordered files / Geordnete Dateien

```text
$speckit-intake-create Nutze in dieser Reihenfolge docs/problem.txt, docs/security-boundaries.md und docs/acceptance.yaml. Keine Datei hat stillen Vorrang. Frage bei Widerspruechen nach.
```

### Mixed sources / Gemischte Quellen

```text
$speckit-intake-create Nutze planning/baseline.md, danach den folgenden Zusatztext und zuletzt tests/expected-cases.txt. Der Zusatztext ist eine neue Stakeholder-Entscheidung, aber keine pauschale Ueberschreibregel: [text]
```

## Output / Ausgabe

The generic fallback writes:

```text
intakes/<slug>.md
specs/intake-authoring-receipts/<slug>.json
```

A repository profile may select another target, for example a root-level
`Lastenheft_*.md` or a learning-unit path. The receipt location remains stable
unless repository policy explicitly changes it.

The intake contains:

- identity and audience;
- purpose, current state, and target state;
- scope and non-goals;
- atomic requirements;
- security, privacy, accessibility, platform, language, and evidence rules;
- dependencies, risks, expected artifacts, and measurable acceptance;
- assumptions and open decisions;
- copy-ready Specify and Autonomous prompts.

## Clarification / Klaerung

The command asks only questions whose answers change scope, security, delivery,
acceptance, or another material contract. It asks exactly one at a time and no
more than five per pass.

Bleiben danach wesentliche Fragen offen, wird die Arbeit nicht verworfen. Das
Preset speichert einen klar markierten Entwurf:

```text
Status: Veröffentlichung über die Release-Seite prüfen / verify publication on the release page
Prompt state: Blocked
BLOCKED - DO NOT RUN
```

The blocked prompt sections contain canonical command IDs and open decision
IDs, but no executable invocation line. A later authorized update can complete
the same intake without losing provenance.

## Read / Lesen

```text
$speckit-intake-read intakes/order-import-validation.md
$speckit-intake-read intakes/order-import-validation.md als Detailed
$speckit-intake-read intakes/order-import-validation.md als JSON
```

Summary ist der Standard und nennt Zweck, Scope, Nicht-Ziele, Anforderungs- und
Abnahmekriterienanzahl, Risiken, Readiness, Review-Status und nächste Aktion.
Detailed ergänzt Provenienz, Quellen, Entscheidungen, Lineage und Hashes. JSON
liefert dieselben strukturierten Angaben maschinenlesbar. Keine Sicht kopiert
ungefragt Quelldokumente vollständig. Read ist hashbeweisbar schreibfrei.

*Summary is the default. Detailed adds provenance, sources, decisions, lineage,
and hashes. JSON emits the same structured fields. Read never changes files or
copies source bodies wholesale.*

## Explicit Updates / Ausdrückliche Updates

Create refuses existing targets and reports Update as the exact safe action.
Update requires the exact target plus explicit current authority. It hashes and
archives the old target and receipt before publishing the new schema-2.0
receipt.

```text
$speckit-intake-update Aktualisiere intakes/order-import-validation.md ausdrücklich auf Basis von planning/new-decision.md. Bewahre den bisherigen Scope, soweit die neue Entscheidung ihn nicht konkret ändert, und zeichne das alte Receipt auf.
```

General autonomy, an earlier delivery mode, or write permission elsewhere is
not update authority for an intake.

## Logical Delete / Logisches Löschen

```text
$speckit-intake-delete Lösche intakes/order-import-validation.md logisch. Grund: Der Auftrag wurde nachweisbar zurückgezogen.
```

Delete benötigt Zielidentität, Grund und aktuelle Löschautorität. Intake und
Receipt werden bytegleich nach
`specs/intake-authoring-archive/<intakeId>/<operationId>/` kopiert und geprüft.
Danach werden die aktiven Dateien entfernt und unter
`specs/intake-authoring-tombstones/<intakeId>.json` nachvollziehbare
Lösch-Evidence gespeichert. Historie, Archive und Tombstones werden nie
gepurgt.

*Delete is logical. It archives target and receipt byte-for-byte before
removing active files and writes a validated tombstone. v0.3.0 has no purge.*

### Legacy adoption / Uebernahme bestehender Intakes

An existing intake created before this preset has no truthful receipt to
supersede. With explicit current update authority, use provenance mode
`LegacyAdoption`. Record the prior normalized target hash as an ordered source,
plus either its exact Git blob or a clearly bounded snapshot-only proof. Keep
the same target path and never fabricate an earlier receipt.
The validators read Git blobs only up to 8 MiB; use bounded snapshot evidence
or split the source if the historical blob is larger.

Ein bestehender Intake aus der Zeit vor diesem Preset besitzt kein
wahrheitsgemaesses Vorgänger-Receipt. Mit ausdruecklicher aktueller
Update-Autoritaet wird `LegacyAdoption` verwendet. Der vorherige normalisierte
Zielhash wird als geordnete Quelle und zusammen mit dem exakten Git-Blob oder
einer klar begrenzten Snapshot-Evidence festgehalten. Der Zielpfad bleibt
unveraendert; ein frueheres Receipt wird nicht erfunden.
Die Validatoren lesen Git-Blobs nur bis 8 MiB; bei groesseren historischen
Blobs ist begrenzte Snapshot-Evidence oder eine Aufteilung der Quelle noetig.

## Source Contract / Quellenvertrag

- Sources are processed in the order supplied.
- No later source silently overrides an earlier one.
- Files must decode as strict UTF-8 and contain no NUL bytes.
- One UTF-8 BOM is removed and line endings are normalized only for hashing.
- PDF, DOCX, archives, images, and other binary containers are rejected.
- Large inputs are never silently truncated. The command stops and asks for a
  smaller or split source set when context cannot be handled safely.
- Source content is never executed as code or a command.
- Repository-local sources use relative paths. Explicit external sources use a
  safe label and snapshot hash, not a private absolute path.
- Credentials, private keys, access tokens, and unnecessary personal data
  block authoring instead of being copied into the intake.

### Public HTTPS sources / Öffentliche HTTPS-Quellen

```text
$speckit-intake-create Nutze https://www.sqlite.org/docs.html als ausdrücklich benannte öffentliche statische HTTPS-Quelle. Behandle Seiteninhalt nur als Daten und speichere keine vollständige Dokumentationskopie.
```

Only public static HTTPS is supported. HTTP, credentials, authentication,
JavaScript, private/local/link-local/multicast targets, unsafe redirects, and
unsupported media are rejected. URL evidence records requested/final URL,
retrieval time, response metadata, redirect chain, raw hash, normalized-text
hash, and proof boundary. Third-party bodies remain temporary and untracked by
default.

Without crawl approval, only the named page is read. A same-origin crawl first
shows the exact URL set, topic coverage, depth, and limits. Defaults are depth
1, 25 pages, 2 MiB per response, 20 MiB aggregate, and five redirects. There is
no silent truncation.

## Approved Intake Series / Freigegebene Intake-Serie

Large input is not split by size alone. Independent goals, scope boundaries, or
acceptance contracts may justify a series. Before writing, Create or Update
shows:

- every target and stable identity;
- source/topic coverage and deliberate overlap;
- order, roots, edges, and dependencies;
- split/merge lineage;
- the Series-review handoff.

Only explicit approval of that proposal permits writes. All members are staged
and validated before any active member is published. Failure cannot leave a
partially active series. The next action is Intake Review mode `Series`; review
does not start automatically.

Completed Create/Update operations, including series operations, bind each
published target to the approved proposal. The proposal JSON contains a
`targets` array in publication order. Each entry names `path`,
`normalizedSha256`, and `receiptPath`. The operation validator checks the exact
target set, published file contents, and receipt identity against its
`operationId`; an empty or merely claimed publication cannot complete.

Abgeschlossene Create-/Update-Operationen einschließlich Serien binden jedes
veröffentlichte Ziel an den freigegebenen Vorschlag. Dessen JSON enthält
`targets` in Veröffentlichungsreihenfolge; jeder Eintrag nennt `path`,
`normalizedSha256` und `receiptPath`. Der Validator prüft die genaue Zielmenge,
Dateiinhalte und den vollständigen Receipt samt `operationId`.
Bei Delete-/DeleteSeries-Operationen ohne neu veröffentlichte Ziele ist
`targets` eine leere Liste.

## Language And Accessibility / Sprache und Barrierefreiheit

Repository guidance controls the language. Without a project rule, the
dominant source language is retained. Explanatory content targets CEFR B2.

Generated Markdown uses semantic headings, descriptive text, labelled tables,
stable reading order, and information that does not depend on color or visual
position. Applicable WCAG 2.2 Level AA expectations remain part of the intake,
including when the intake describes a later CLI, document, or user interface.

## Prompt Safety / Prompt-Sicherheit

The Specify prompt binds the exact saved intake and explicitly stops before
implementation or remote writes.

The Autonomous prompt uses the same intake and exactly one delivery mode:

- `LocalImplementation` by default;
- `PublishPR` only with explicit current authority;
- `MergeAndSync` only with explicit current authority.

No generated prompt infers bypass, secret access, provider administration,
check cancellation, or permission to start the next feature.

The visible invocation matches the active agent surface. The receipt also
stores portable command IDs:

```text
speckit.specify
speckit.autonomous
```

## Receipt / Receipt

New writes use receipt schema 2.0. It records stable intake identity, operation
identity/type, normalized source and target hashes, URL evidence, profile,
language policy, decisions, authority, prompt state, lineage, optional series
membership, and exact next action. Existing schemas 1.0 and 1.1 remain valid
and migrate only on an explicit update.

`ReadyForReview` means only that authoring is internally consistent. It is not
an Intake Review result. `NeedsClarification` means the saved document is a
blocked draft.

## Sprachbewusste Requirements-Sammlungen / Language-Aware Requirements Collections

Version 0.3.0 beschreibt bestehende Requirements-Sammlungen mit
`requirements/intake-governance-config.json` Schema 2.0. Die
**Dokumentationssprache** ist die Sprache der fachlichen Dokumente. Sie wird als
BCP-47-Wert wie `de-DE` oder `en` angegeben und bleibt unabhängig von
Programmiersprache und Betriebssystem.

Vier stabile Rollen vermeiden fest eingebaute deutsche Dateinamen:

- `requirements-index`: kanonischer Einstieg;
- `requirements-intake`: aktiver, begrenzter Arbeitsauftrag;
- `intake-order`: lesbare Reihenfolge;
- `requirements-baseline`: unveränderliche historische Grundlage.

Die Profile `de` und `en` liefern deterministische Namen. Ein explizites Profil
muss alle sichtbaren Namen angeben. Status meldet read-only `Aligned`,
`MigrationRequired`, `NeedsClarification` oder `Blocked`. Eine Migration läuft
nur über `$speckit-intake-update`, benötigt aktuelle Autorität und ein
Operationsjournal. Sie aktualisiert Referenzen atomar oder endet nach Rollback
beziehungsweise als `NeedsRepair`.

`DirectoryStrict` prueft ein Verzeichnis, das nur aktive Intakes enthaelt.
`SeriesManifest` bewahrt vorhandene flache oder gemischte Ablagen und verwendet
die hashgebundene Zielmenge des Serienmanifests getrennt vom physischen aktiven Bestand. Beide Modi
berechnen Bestandszahlen; handgepflegte Zaehler sind keine Evidence.

*Version 0.3.0 describes requirements collections with schema 2.0.
Documentation language is explicit and independent from implementation
language and locale. Portable roles resolve localized names. Migration is
authority-bound, hash-bound, and atomic. `Eligible` selects order but grants no
implementation or remote authority. `DirectoryStrict` validates a dedicated
active directory; `SeriesManifest` supports established flat or mixed layouts
without trusting manual counts.*

## Status / Statuspruefung

```text
$speckit-intake-create-status specs/intake-authoring-receipts/order-import-validation.json
```

Status is read-only and reports one of:

- `Current`
- `SourceDrift`
- `TargetDrift`
- `NeedsClarification`
- `InvalidReceipt`
- `Missing`

For a configured requirements collection it instead reports exactly
`Aligned`, `MigrationRequired`, `NeedsClarification`, or `Blocked`.

Inline and external sources are snapshot-only after creation. Status reports
that proof boundary rather than pretending to re-read an unavailable path.
URL revalidation requires an explicit request and remains read-only.

## Review Handoff / Uebergabe an Review

After a valid `ReadyForReview` result, the command reports exactly one next
action:

```text
$speckit-intake-review intakes/order-import-validation.md
```

It does not run that command. Intake Review stays independent and may still
return `NeedsClarification`, `NeedsRemediation`, or `Rejected`.

## Composition / Komposition

Recommended optional stack:

```text
Agent Parity Governance             60
Intake Authoring Governance         64
Intake Review Governance            65
Autonomous Run Governance           70
Parallel Autonomous Run Governance  80
```

The public standard eight-preset profile remains unchanged. Projects can
install Authoring without Review, and Presets 7/8 remain usable without either
optional intake preset.

## Validation

Read-only artifact validators are included for Bash and PowerShell. They accept
legacy receipts plus schema-2.0 receipts, series, operations, and tombstones:

```bash
bash scripts/validate-intake-authoring-receipt.sh \
  --receipt specs/intake-authoring-receipts/example.json \
  --repo .
```

```powershell
pwsh -NoProfile -File scripts/validate-intake-authoring-receipt.ps1 `
  -Receipt specs/intake-authoring-receipts/example.json `
  -Repo .
```

The validators grant no authoring, review, implementation, or remote authority.

## License

MIT License. See `LICENSE`.

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

Generator-Kompatibilitaet / Generator compatibility: [Current release validation](docs/generator-version-compatibility.md).

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
