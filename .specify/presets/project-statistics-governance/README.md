# Project Statistics Governance

<!-- current-package: 0.1.4 -->

Paketkennung: **v0.1.4**. [Vorbereitung und Veröffentlichungsgrenze](docs/release-0.1.4-validation.md). Die Release-Seite und der externe Archivnachweis belegen die Veröffentlichung, nicht dieser Vorbereitungsstand. Frühere Versionsnachweise unten bleiben historisch.

<span lang="en">Package identity: v0.1.4. The release page and external archive evidence prove publication, not this preparation snapshot. Earlier version records below remain historical.</span>

[Release-Abschluss und Verbesserungsprofil / Publication closeout and improvement profile](docs/release-0.1.2-validation.md).

Der unten verlinkte Basispaket-Nachweis bleibt historisch; dieses Paket nimmt die Quellenkorrekturen als neue Version auf.

<span lang="en">The linked base-package evidence remains historical; this package includes the source corrections as a new version.</span>

**Leserpfad / Reader path:** [Deutsch: Einstieg, Begriffe und Fehlerhilfe](docs/user-guide.md) · [English: start, terms and troubleshooting](docs/user-guide.EN.md).

Historisches Basispaket **v0.1.2**. Der [Release-Stand](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/releases) wird getrennt geprüft; ein Commit auf main ist noch keine Veröffentlichung oder Installation.

*Historical base package v0.1.2. The release page records actual publication separately; a main-branch commit is not publication or installation.*


Patch **0.1.2** liefert die verbesserte zweisprachige Einstiegs- und
Kompositionsdokumentation. Verhalten und Berechtigungsgrenzen bleiben erhalten.
[Release-Nachweis / Release record](docs/release-0.1.2-validation.md).

*Patch 0.1.2 packages the improved bilingual getting-started and composition
guide. Behavior and authority boundaries remain unchanged.*

**Reproduzierbare Projekttransparenz fuer Git und Spec Kit.**
**Reproducible project transparency for Git and Spec Kit.**

Das optionale MIT-Preset macht Textbestand, Artefaktmix und sichtbare
Git-Aktivitaet nachvollziehbar. Ergebnisse sind an Git-Quellen, Konfiguration
und Werkzeugversion gebunden. Es misst weder Softwarequalitaet noch
Ausbildungsleistung, Personenleistung oder tatsaechliche KI-Zeitersparnis.

This optional MIT preset makes text inventory, artifact mix and visible Git
activity traceable. Results bind Git sources, configuration and tool version.
It does not measure quality, learner or individual performance, or AI time savings.

**Paketstand / Package version:** `v0.1.2`. Dieser Dokumentationspatch berichtigt
Download- und Prüfsummenanleitung. Der Feldtest von v0.1.0 bleibt historische
Evidence, keine Abnahme jedes späteren ZIPs. Den Veröffentlichungsstand und
den zum genauen Quellarchiv gehörenden SHA-256 zeigt nach Veröffentlichung die
[Release-Seite / release page](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/releases/tag/v0.1.2).

*This documentation patch corrects download/checksum guidance. The v0.1.0 field
test remains historical evidence, not acceptance of each later ZIP. After
publication, the linked release notes identify this exact source archive and
its SHA-256. While a release is not published, no new archive/hash is claimed.*

## Inhalt und Leserpfade / Contents and reader paths

- [Zweck und Grenzen / Purpose and limits](#purpose)
- [Voraussetzungen / Prerequisites](#prerequisites)
- [Installation und Pruefsumme / Installation and checksum](#installation)
- [Erster vollstaendiger Ablauf / First complete workflow](#workflow)
- [Konfiguration / Configuration](#configuration)
- [Ergebnisse verstehen / Understanding results](#results)
- [Wiederkehrende Pflege / Recurring maintenance](#maintenance)
- [Fehlerhilfe / Troubleshooting](#troubleshooting)
- [Disable, Remove und Neuinstallation / Preset lifecycle](#lifecycle)
- [Entwicklung und Tests / Development and tests](#development)
- [Nachweise und Vertiefung / Evidence and references](#references)

**Lernende:** Zweck -> Voraussetzungen -> Installation -> erster Ablauf ->
Ergebnisse -> Fehlerhilfe. **Entwickler:** zusaetzlich Konfiguration, Methodik,
Wiederholbarkeit und Tests. **Maintainer/Reviewer:**
[Lieferdokumentation](docs/delivery.md) -> Feldtest -> Release-Pruefsummen.

**Learners:** purpose -> prerequisites -> installation -> first workflow ->
results -> troubleshooting. **Developers:** also read configuration, methodology
and tests. **Maintainers/reviewers:** use the delivery guide and release evidence.

<a id="purpose"></a>
## Zweck und Grenzen / Purpose and limits

Die Statistik beantwortet: Welche getrackten Textartefakte enthaelt eine
Quellrevision? Wie verteilt sich der Bestand? An welchen Tagen gab es im
gewaehlten Fenster Textaenderungen? Ein Review kann Messungen damit erklaeren
und wiederholen, statt Zahlen aus unterschiedlichen Quellen zu vermischen.

The report answers which tracked text artifacts belong to a revision, how that
inventory is distributed, and when text changes were visible in the selected
window. Reviewers can explain and repeat measurements instead of mixing sources.

Fuer Ausbildungsprojekte vermittelt dies Versionsverwaltung, Nachweisfuehrung
und verantwortliche Interpretation von Kennzahlen. Mehr Zeilen bedeuten nicht
bessere Software; viele Commits beweisen weder Lernerfolg noch Arbeitszeit.
Referenz-Modellrechnungen sind standardmaessig ausgeschaltet. Auch nach
bewusster Aktivierung bleiben sie Annahmen, keine Messung. Keine Personen-
Ranglisten, Noten, Sicherheits-, Produkt- oder Zertifizierungsfreigaben ableiten.

Training projects can use this to teach version control, evidence handling and
responsible interpretation. More lines do not imply better software; commits
do not prove learning or time spent. Optional reference estimates are off by
default and remain assumptions when enabled. Never derive rankings, grades,
security approval, product acceptance or certification from these statistics.

<a id="prerequisites"></a>
## Voraussetzungen / Prerequisites

| Voraussetzung / requirement | Bedeutung / meaning |
| --- | --- |
| Git mit vollstaendiger Historie / complete Git history | Mindestens ein Commit, kein Shallow-/Partial-/Promisor-Klon / at least one commit, no shallow/partial/promisor clone |
| PowerShell 7 (`pwsh`) | Auf allen Plattformen erforderlich; auch Bash delegiert an diese Engine / required on all platforms, including Bash |
| Spec Kit >=0.12.8 (`specify`) | Installation im bereits initialisierten Zielprojekt mit `.specify` / installation into an already initialized target project |
| Bash auf macOS/Linux / Bash on macOS/Linux | Wrapper mit `bash` starten; ZIPs bewahren Ausfuehrungsrechte nicht immer / invoke through Bash |
| Sauberer Git-Stand / clean Git state | Vor freigegebenen Schreiblaeufen pruefen; keine fremden Aenderungen committen / check before authorized writes, exclude unrelated changes |

Vor dem Einstieg im **Zielprojekt**, nicht im Preset-Quellrepo, pruefen:
Check in the **target project**, not the preset source repository:

```text
git --version
pwsh --version
specify version
git rev-parse --show-toplevel
git rev-parse --is-shallow-repository
git status --short
```

`false` bei der Shallow-Abfrage ist notwendig, aber kein vollstaendiger
Quellennachweis; das Preset prueft weitere Grenzen. Fehlende Werkzeuge und
Spec-Kit-Integration zuerst nach Projektanleitung einrichten. Kein pauschales
`specify init --force` ueber vorhandene Projekte ausfuehren.

`false` for the shallow check is necessary but not sufficient; the preset
checks additional boundaries. Establish missing tools and Spec Kit integration
under project instructions first. Do not force-reinitialize an existing project.

Die Messung benoetigt kein GitHub-Konto, keinen Home-Baseline-Klon und kein
weiteres Governance-Preset. Lernenden-Repos duerfen auf anderen Git-Plattformen
liegen. Nur Paketdownload/Installation benoetigt Netzwerk; die Messung fuehrt
keinen Fetch oder automatischen Tool-Install aus.

Measurement needs no GitHub account, Home Baseline clone or companion preset.
Learners may use other Git hosts. Download/installation needs network access;
measurement performs no fetch or automatic tool installation.

<a id="installation"></a>
## Installation und Pruefsumme / Installation and checksum

### Paket zuerst pruefen / Verify the package first

Lies zuerst die Release-Notes von **v0.1.2**. Kopiere ausschließlich den dort
für die exakte `archive/refs/tags/v0.1.2.zip`-URL angegebenen SHA-256 in das
folgende Beispiel. Fehlt der Nachweis, stoppe und frage die Betreuung.
Ein ZIP kann seine eigene endgültige Prüfsumme nicht in seiner README enthalten:
Eine Änderung der README würde das ZIP und damit dessen Hash wieder verändern.
Die Release-Notes sind deshalb der getrennte Prüfsummennachweis.

*Read the v0.1.2 release notes first. Copy only the SHA-256 bound there to the
exact archive/refs/tags/v0.1.2.zip URL into the example. Missing evidence means
stop and ask the maintainer. Embedding a ZIP's own final hash in its README
would change that ZIP again; release notes provide the separate hash record.*

macOS/Linux, in einem neuen temporaeren Verzeichnis ausserhalb des Projekts:
macOS/Linux, in a new temporary directory outside the project:

```bash
statistics_download_dir=$(mktemp -d)
expected_sha256='REPLACE_WITH_V0_1_2_SOURCE_ARCHIVE_SHA256_FROM_RELEASE_NOTES'
if ! printf '%s' "$expected_sha256" | LC_ALL=C grep -Eq '^[0-9a-fA-F]{64}$'; then
  printf 'STOP: SHA-256 aus den Release-Notes eintragen / enter SHA-256 from release notes\n' >&2
  exit 2
fi
curl --fail --location --output "$statistics_download_dir/v0.1.2.zip" \
  https://github.com/hindermath/spec-kit-preset-project-statistics-governance/archive/refs/tags/v0.1.4.zip
if command -v shasum >/dev/null 2>&1; then
  actual_sha256=$(shasum -a 256 "$statistics_download_dir/v0.1.2.zip" | cut -d ' ' -f 1)
else
  actual_sha256=$(sha256sum "$statistics_download_dir/v0.1.2.zip" | cut -d ' ' -f 1)
fi
if [ "$(printf '%s' "$actual_sha256" | tr 'A-F' 'a-f')" != "$(printf '%s' "$expected_sha256" | tr 'A-F' 'a-f')" ]; then
  printf 'STOP: Prüfsumme abweichend / checksum mismatch\n' >&2
  exit 2
fi
```

Windows/PowerShell 7:

```powershell
$StatisticsDownloadDir = Join-Path ([IO.Path]::GetTempPath()) ('statistics-download-' + [guid]::NewGuid())
$null = New-Item -ItemType Directory -Path $StatisticsDownloadDir
$ExpectedSha256 = 'REPLACE_WITH_V0_1_2_SOURCE_ARCHIVE_SHA256_FROM_RELEASE_NOTES'
if ($ExpectedSha256 -notmatch '^[0-9a-fA-F]{64}$') { throw 'STOP: SHA-256 aus den Release-Notes eintragen / enter SHA-256 from release notes' }
$StatisticsZip = Join-Path $StatisticsDownloadDir 'v0.1.2.zip'
Invoke-WebRequest -Uri 'https://github.com/hindermath/spec-kit-preset-project-statistics-governance/archive/refs/tags/v0.1.4.zip' -OutFile $StatisticsZip
$ActualSha256 = (Get-FileHash -LiteralPath $StatisticsZip -Algorithm SHA256).Hash
if ($ActualSha256 -ine $ExpectedSha256) { throw 'STOP: Prüfsumme abweichend / checksum mismatch' }
```

Nur bei exakt passender Pruefsumme fortfahren; Gross-/Kleinschreibung des
Hexwerts ist unerheblich. Bei Abweichung stoppen und nichts installieren.
GitHub bietet das Quellarchiv automatisch an. Zusätzliche hochgeladene Assets
wie `SHA256SUMS` sind nicht vorausgesetzt. Ein Asset-Hash gilt nicht automatisch
für das Quellarchiv. HTTPS oder ein Tagname allein ersetzt keine Hashprüfung.

Continue only if the checksum matches, ignoring hexadecimal letter case.
If it differs, stop without installing. GitHub provides source archives
automatically; uploaded assets such as SHA256SUMS are not assumed. An asset
hash does not automatically identify the source archive. HTTPS or a tag name
alone does not replace hash verification.

### In das Zielprojekt installieren / Install into the target project

Im bereits initialisierten Spec-Kit-Zielprojekt / in the initialized target:

```text
specify preset add --from https://github.com/hindermath/spec-kit-preset-project-statistics-governance/archive/refs/tags/v0.1.4.zip --priority 90
specify preset list
specify preset info project-statistics-governance
specify preset resolve project-statistics-contract
```

Der Installer laedt die URL erneut; er verwendet nicht die zuvor gepruefte
lokale Datei. Fuer revisionsgebundene Lieferung deshalb auch die installierten
Paketdateien mit dem geprueften entpackten ZIP vergleichen. Prioritaet 90 ist
das vorgesehene Profil; kleinere Zahlen haben hoehere Aufloesungsprioritaet.
Bestehende Projekt-Policy vorher pruefen.

The installer downloads the URL again, not the local file checked above.
For source-bound delivery, compare installed package files against the verified
extracted ZIP too. Priority 90 is the intended profile; lower numbers have
higher precedence. Check existing project policy first.

Installation startet weder Messung noch Migration. Registry, installiertes
Preset und erzeugte Agenten-/Command-Flaechen nach Projekt-Policy sichten und
separat committen; `.specify/presets/.cache/` nicht committen. Erst mit sauberem
Git-Stand fortfahren. Kein `git add .` fuer ungepruefte oder fremde Aenderungen.

Installation starts no measurement or migration. Review and separately commit
only the selected integration files under project policy. Do not commit the
preset cache. Continue with a clean worktree; avoid broad staging of unrelated
or unreviewed files.

### Zusammenspiel mit anderen Presets / Composition with Other Presets

Das [Manifest](preset.yml) ergänzt Constitution, Plan und Tasks mit `append`
und registriert drei eigene Statistik-Commands. Es umhüllt nicht Specify und
startet nach einem Feature-Abschluss keine Messung automatisch. Konfiguration
entsteht erst durch autorisiertes Init; `project-statistics-contract` ist eine
Vertragsvorlage, kein ausgefüllter Bericht oder Mess-Snapshot.

*The [manifest](preset.yml) appends to constitution, plan and tasks and
registers three standalone statistics commands. It does not wrap Specify or
measure automatically after a feature. Authorized Init creates configuration;
project-statistics-contract is a contract template, not a report or snapshot.*

Kleinere Prioritätszahlen haben höhere Auflösungspriorität. `append` ergänzt;
gleichnamige `replace`-Beiträge oder Projekt-Overrides können darunterliegende
Beiträge verdrängen. Eigene Reihenfolgen und Drittanbieter-Kombinationen
separat prüfen, etwa über `specify preset resolve plan-template` und erzeugte
Agentenflächen. Priorität 90 ist kein automatisch späterer Messzeitpunkt und
ersetzt weder sauberen Git-Stand noch Schreibfreigabe.

*Lower numbers mean higher precedence. Append adds; same-name replace layers
or overrides can hide lower contributions. Check custom orderings and
third-party combinations using plan-template resolution and generated agent
surfaces. Priority 90 does not schedule measurement or replace clean Git state
and write authority.*

<a id="workflow"></a>
## Erster vollstaendiger Ablauf / First complete workflow

Alle Beispiele laufen vom Git-Root eines freigegebenen Zielprojekts aus und
verwenden den neuen Kontext `docs/project-statistics/`. Existiert dieser bereits,
zuerst die Fehlerhilfe lesen. Die drei bisherigen Piloten verwenden getrennt
`docs/project-statistics-pilot/config.json`; diese nicht erneut initialisieren.

Run examples from an authorized target repository's Git root using the new
`docs/project-statistics/` context. If it exists, read troubleshooting first.
Existing pilots use `docs/project-statistics-pilot/config.json`; do not initialize
them again.

### 1. Vorschau und Initialisierung / Preview and initialization

macOS/Linux:

```bash
bash .specify/presets/project-statistics-governance/scripts/project-statistics.sh init --dry-run
bash .specify/presets/project-statistics-governance/scripts/project-statistics.sh init
git diff -- docs/project-statistics
git status --short
```

Windows:

```powershell
pwsh -NoProfile -File .specify/presets/project-statistics-governance/scripts/project-statistics.ps1 -Action Init -WhatIf
pwsh -NoProfile -File .specify/presets/project-statistics-governance/scripts/project-statistics.ps1 -Action Init
git diff -- docs/project-statistics
git status --short
```

Den echten Init nur nach gepruefter Vorschau und Schreibfreigabe ausfuehren.
Neue ungetrackte Dateien erscheinen nicht in `git diff`: `config.json` und
`report.md` auch direkt im Editor lesen. Init erzeugt noch keinen Mess-Snapshot.
Status liefert deshalb anfangs erwartungsgemaess `DRIFT` mit Exitcode 1.

Execute real Init only after preview and write authorization. New untracked
files do not appear in ordinary `git diff`; open configuration and report in
your editor too. Init does not create a measured snapshot, so initial Status
returns expected `DRIFT` with exit code 1.

### 2. Konfiguration sichten und committen / Review and commit configuration

Projektname, Zeitzone und Ausschluesse bewusst waehlen; Referenzen beim Einstieg
ausgeschaltet lassen. Manuelle Erlaeuterungen ausserhalb der generierten Marker
im Bericht pflegen. Danach nur die beiden neuen Dateien stagen und pruefen:

Choose project name, timezone and exclusions; initially keep references off.
Put authored explanations outside generated report markers. Then stage and
review only the two new files:

```text
git add -- docs/project-statistics/config.json docs/project-statistics/report.md
git diff --cached --check
git diff --cached
git commit -m "docs: initialize project statistics context"
git status --short
```

Commit erst nach Sichtung und nach Projektregeln. Update blockiert uncommittete
Aenderungen: nicht automatisch stashen, verwerfen oder weitere Dateien
mitcommitten. Diese Git-Befehle erfordern eigene Lieferautoritaet; das Preset
fuehrt sie nicht fuer den Benutzer aus.

Commit only after review under project rules. Update blocks uncommitted changes;
never auto-stash, discard or add more files just to pass. These Git operations
require separate authority; the preset does not execute them for you.

### 3. Messen / Measure

macOS/Linux:

```bash
bash .specify/presets/project-statistics-governance/scripts/project-statistics.sh update --dry-run
bash .specify/presets/project-statistics-governance/scripts/project-statistics.sh update --json
bash .specify/presets/project-statistics-governance/scripts/project-statistics.sh status --json
```

Windows:

```powershell
pwsh -NoProfile -File .specify/presets/project-statistics-governance/scripts/project-statistics.ps1 -Action Update -WhatIf
pwsh -NoProfile -File .specify/presets/project-statistics-governance/scripts/project-statistics.ps1 -Action Update -Json
pwsh -NoProfile -File .specify/presets/project-statistics-governance/scripts/project-statistics.ps1 -Action Status -Json
```

Der erste Update-Lauf erstellt `snapshot.json` und rendert den markierten Teil
von `report.md`. Status schreibt nichts. Erwartet: Status-Exitcode 0,
`status=CURRENT`, `reproducible=true`, `current=true`, `changed=false`.
Quellrevision und Stichtag kommen aus dem eigenen Repository.

The first Update creates the snapshot and renders the report's marked section.
Status writes nothing. Expect exit 0 and the fields above; actual revision and
cutoff come from your repository.

### 4. Ergebnisse getrennt liefern / Deliver outputs separately

```text
git diff -- docs/project-statistics/report.md
git add -- docs/project-statistics/report.md docs/project-statistics/snapshot.json
git diff --cached --check
git diff --cached
git commit -m "docs: record reproducible project statistics"
git status --short
```

Den neuen Snapshot ebenfalls vor Commit lesen. Danach Status wiederholen:
Ein reiner Ausgabecommit soll seine eigene Messung nicht veralten lassen.
Push, PR, Review und Merge bleiben separate Schritte nach Projektregeln.

Read the new snapshot before committing too. Repeat Status after commit:
output-only commits should not make their own measurement stale. Push, PR,
review and merge remain separate project-governed steps.

<a id="configuration"></a>
## Konfiguration / Configuration

Die Initialisierung verwendet diese Struktur. `repositoryName` fuer das eigene
Projekt anpassen. Es werden keine Phasenwerte oder Referenzen erfunden.
Editierte Konfiguration vor Update committen.

Initialization uses this structure. Set your project name; no phase values or
reference estimates are invented. Commit configuration edits before Update.

```json
{
  "schemaVersion": 1,
  "methodology": "project-transparency/1",
  "repositoryName": "Project",
  "timeZone": "UTC",
  "activityWindowWeeks": 52,
  "excludedPaths": [],
  "categoryOverrides": [],
  "phases": [],
  "references": {
    "enabled": false,
    "scenarios": []
  }
}
```

| Feld / field | Wirkung / effect |
| --- | --- |
| `schemaVersion`, `methodology` | Vertragskennungen unveraendert lassen / preserve contract identifiers |
| `repositoryName` | Lesbarer Projektname, keine Geheimnisse / readable project name, no secrets |
| `timeZone` | Committer-Datum bestimmt Aktivtage, Default UTC / activity uses committer dates, default UTC |
| `activityWindowWeeks` | 1 bis 104 Wochen, Default 52 / 1 to 104 weeks, default 52 |
| `excludedPaths` | Begruendete, case-insensitive PowerShell-Wildcards / justified case-insensitive PowerShell wildcards |
| `categoryOverrides` | Explizite `pattern`-/`category`-Zuordnung / explicit pattern/category mapping |
| `phases` | Manuell belegte Phasen, keine automatisch bestaetigten Abschluesse / authored phases, not verified completion |
| `references` | Standardmaessig aus, optionale Modellannahmen / off by default, optional assumptions |

Kategorien: `Production`, `Tests`, `Documentation`, `Scripts`, `Configuration`,
`DataMedia`, `Other`. Ein Override sieht etwa so aus:
`{"pattern":"examples/*","category":"Production"}`. Ein Ausschluss wie
`"vendor/*"` braucht eine Projektbegruendung; er ist kein allgemeiner Default.
Muster sind PowerShell-Wildcards, keine `.gitignore`-Regeln. Unbekannte Felder
blockieren. Verbindliche Details: [Schema](scripts/config/project-statistics.schema.json)
und [Methodik](docs/methodology.md).

Overrides map a pattern to one of the categories above. Exclusions such as
`vendor/*` need project justification, not blind copying. Patterns use
PowerShell wildcard syntax, not `.gitignore` rules. Unknown fields block
validation. The linked schema and methodology define the full contract.

Einen anderen Kontext mit `--config docs/my-statistics/config.json` bzw.
`-Config docs/my-statistics/config.json` bei **jedem** Befehl auswaehlen.
Konfiguration, Bericht und Snapshot liegen gemeinsam dort. Nicht gleichzeitig
in denselben Kontext schreiben; Pilot und kanonische Statistik getrennt halten.

Select another context with those flags on **every** command. Its configuration,
report and snapshot share a directory. Serialize writes and keep pilot and
canonical statistics separate.

<a id="results"></a>
## Ergebnisse verstehen / Understanding results

| Datei / file | Aufgabe / role |
| --- | --- |
| `config.json` | Versionierte Auswahl und Methodikparameter / versioned selection and parameters |
| `report.md` | Lesbarer Markerbereich plus erhaltene manuelle Abschnitte / generated section plus preserved authored text |
| `snapshot.json` | Maschinenlesbare Messung und Quellenbindung, nicht manuell reparieren / machine-readable evidence, never manually repair hashes |

Gemessen werden rohe Git-Blobs der gebundenen Revision, nicht der aktuelle
Editorinhalt. Ungetrackte oder uncommittete Arbeit ist kein gemessener Fortschritt.
Eigene Ausgaben, das bestehende Statistik-Ledger und `STATS.md` sind aus den
betreffenden Zaehlungen ausgeschlossen.

Measurement reads raw Git blobs at the bound revision, not your editor buffer.
Untracked or uncommitted work is not measured progress. Own outputs, the existing
statistics ledger and `STATS.md` are excluded from the relevant counts.

Textbestand ist ein **Bestand** an einer Revision. Bruttovolumen sind
hinzugefuegte plus entfernte Zeilen aus Nicht-Merge-Commits im Fenster.
Aktivtage sind Tage mit positivem Textaenderungsvolumen, keine Arbeitsstunden.
Reine Umbenennungen erzeugen kein Volumen. Symlinks/Submodule werden nicht
verfolgt, Binaerdateien nicht als Text gezaehlt. LFS-Pointer bleiben Pointer-Text;
LFS-Inhalte werden nicht automatisch geladen.

Inventory is a **stock** at a revision. Gross volume is added plus removed
lines from non-merge commits in the window. Active days are not work hours.
Pure renames add no volume; symlinks/submodules are not followed and binaries
are not text. LFS pointers stay pointer text without downloading LFS content.

**Reproduzierbar:** Der gespeicherte Stand laesst sich erneut berechnen.
**Aktuell:** Die relevanten heutigen Quellen passen zusaetzlich zur Messung.
Eine alte Messung kann reproduzierbar und trotzdem veraltet sein. Ein neuer
Kalendertag allein verursacht keine Drift. `changed=false` bei Status bedeutet,
dass der Pruefbefehl nichts geschrieben hat, nicht dass Git sauber ist.

**Reproducible:** the stored result can be recalculated. **Current:** relevant
present inputs still match too. A reproducible result can be stale. A new
calendar day alone does not cause drift. Status's `changed=false` means no
writes by that command, not a clean Git worktree.

Optional bestimmen `--revision COMMIT` / `-Revision COMMIT` und
`--as-of YYYY-MM-DD` / `-AsOf YYYY-MM-DD` eine neue Update-Messung. Platzhalter
durch existierenden Commit und gueltiges Datum ersetzen. Der Stichtag begrenzt
Aktivitaet, rekonstruiert aber **nicht** den damaligen Dateibestand. Status
prueft die im Snapshot gespeicherte Bindung.

Revision/cutoff flags select a new Update measurement. Replace placeholders
with an existing commit and valid date. Cutoff limits activity, **not** inventory
reconstruction at that date. Status replays the stored snapshot binding.

<a id="maintenance"></a>
## Wiederkehrende Pflege / Recurring maintenance

Nach Feature-Abschluss zuerst die freigegebenen Quellaenderungen liefern,
dann Konfiguration pruefen, Update-Vorschau, Update, Status und Ausgabe-Review
ausfuehren. Ausgaben getrennt committen. Neue gemessene Dokumentation oder
CI-Dateien nach einer Messung koennen einen Nachlauf erfordern; reine
Ausgabecommits sollen keine Update-Schleife bilden.

Deliver authorized feature changes first, then review configuration, preview,
Update, Status and outputs. Commit outputs separately. Later measured docs or
CI files may require another refresh; output-only commits should not loop.

Agenten-Commands: `speckit.statistics-init`, `speckit.statistics-update`,
`speckit.statistics-status`. Deren Aufruf in der Agenten-UI haengt von der
Integration ab; die direkten Shell-Aufrufe sind davon unabhaengig. Installation
erteilt keine Commit-, Push-, Merge- oder administrative Autoritaet.

The three agent commands have integration-specific UI invocation. Direct shell
calls do not depend on that UI. Installation grants no commit, push, merge
or administrative authority.

<a id="troubleshooting"></a>
## Fehlerhilfe / Troubleshooting

Exitcode direkt nach dem Prozess lesen: Bash `echo "$?"`, PowerShell
`$LASTEXITCODE`. Automatisierung wertet Exitcode und JSON gemeinsam aus,
nicht nur die Zeichenfolge `CURRENT`. WhatIf kann Vorschautext zusaetzlich
zum JSON ausgeben.

Read the exit code immediately after the process. Automation must check both
exit code and JSON, not just search for `CURRENT`. WhatIf can add preview text.

| Situation / situation | Bedeutung und sicherer Schritt / meaning and safe action |
| --- | --- |
| Exit 0, `CURRENT` | Erfolgreiche Statuspruefung / successful status check |
| Exit 1, noch kein Snapshot / no snapshot | Nach Init erwartbar: Konfiguration committen, dann Update / expected after Init: commit configuration, then Update |
| Exit 1, reproduzierbar, aber veraltet / reproducible but stale | Quellen sichten, dann freigegebenen Update-Lauf / inspect inputs, then authorize Update |
| Exit 1, nicht reproduzierbar / not reproducible | Bericht, Snapshot und Bindung pruefen, niemals Hashes manuell reparieren / inspect report, snapshot and binding, never patch hashes |
| Exit 2 | Ungueltige Eingaben oder blockierte Voraussetzung beheben, kein Bypass / fix invalid input or blocked prerequisite, no bypass |
| Fehlendes/veraltetes PowerShell / missing/old PowerShell | PowerShell 7 auch fuer Bash bereitstellen; fehlt das auszufuehrende Programm ganz, kann bereits das OS abbrechen / provide PowerShell 7; missing executables may fail at OS level |
| Shallow/Partial/Promisor | Vollstaendige Quellen separat beschaffen, kein automatischer Fetch / obtain full sources separately, no automatic fetch |
| Dirty Tree | Eigene Aenderungen geregelt sichten und liefern, kein automatischer Reset/Stash / review and deliver owned changes, no auto-reset/stash |
| Init findet vorhandene Dateien / existing Init context | Nicht ueberschreiben: Kontext pruefen oder neuen freigegebenen Pfad waehlen / inspect context or select an authorized new path |
| Unsicherer Pfad/Symlink / unsafe path/symlink | Sicheren relativen Repository-Pfad verwenden / use a safe relative repository path |
| `Not a Spec Kit project` | Richtiges Zielprojekt und bestehende Integration pruefen / check target project and initialization |
| Abweichung zu Legacy / legacy difference | Revision, Ausschluesse, Zeitzone, Fenster und Methodik vergleichen / compare revision, exclusions, timezone, window and methodology |

Fehlerberichte enthalten Version, OS, PowerShell-/Git-Version, bereinigten
Befehl, Exitcode und Quellenrevision. Keine Tokens, privaten Dateiinhalte
oder geheimen Repository-Pfade veroeffentlichen.

Include versions, OS, sanitized command, exit code and source revision in
reports. Do not publish tokens, private contents or sensitive repository paths.

<a id="lifecycle"></a>
## Disable, Remove und Neuinstallation / Preset lifecycle

Nur mit Projektfreigabe und erhaltenen verfassten Berichten verwenden.
Diese Befehle sind Lifecycle-Alternativen, keine Sequenz fuer jedes Update.
Use only with project authorization and preserved authored reports. These are
lifecycle alternatives, not a routine update sequence.

```text
specify preset disable project-statistics-governance
specify preset enable project-statistics-governance
specify preset remove project-statistics-governance
```

Disable/Enable steuert Komposition; es sperrt keine direkten Skriptaufrufe.
Remove entfernt die Installation, nicht die verfassten Projektberichte.
Danach ist der installierte Skriptpfad nicht mehr verfuegbar. Neuinstallation
mit dem geprueften Installationsbefehl; vorhandene Kontexte nicht erneut
initialisieren. Nach jeder Aktion Registry, Command-Flaechen und Git-Diff sichten.

Disable/Enable controls composition, not permission to invoke scripts directly.
Remove removes installation, not authored reports; installed script paths then
vanish. Reinstall with the verified command without reinitializing existing
contexts. Review registry, command surfaces and Git diff after each action.

<a id="development"></a>
## Entwicklung und Tests / Development and tests

Dieses eigenstaendige Repository ist Produktquelle; installierte Kopien sind
Integrationen. Fuer lokale Entwicklung ein Git-Archiv ohne `.git` entpacken
und in einem isolierten, initialisierten Spec-Kit-Testprojekt verwenden:

This standalone repository is the product source; installed copies are
integrations. For local development, extract a Git archive without `.git`
and use an isolated initialized Spec Kit test project:

```text
specify preset add --dev <package-directory> --priority 90
```

Den Platzhalter durch das entpackte Verzeichnis ersetzen, nicht durch einen
Arbeitsklon mit `.git`. Entwicklungstests im Preset-Quellrepository starten:
Replace the placeholder with the extracted directory, not a clone containing
`.git`. Run development tests in the preset source repository:

```powershell
pwsh -NoProfile -File tests/test-project-statistics.ps1
pwsh -NoProfile -File tests/test-installed-preset.ps1
```

Die Suites verwenden isolierte temporaere Git-Repositories. Native CI prueft
macOS/Linux/Windows, PowerShell-Analyse und Preset-Lifecycle. v0.1.0 erreichte
67 Assertions auf Unix und 61 auf Windows; Unix-spezifische Faelle werden
nicht als Windows-Nachweis ausgegeben. Tests ersetzen keine menschliche Abnahme.

Suites use isolated temporary repositories. Native CI covers all three OSs,
PowerShell analysis and lifecycle. v0.1.0 passed 67 Unix and 61 Windows assertions;
Unix-only cases are not counted as Windows evidence. Tests do not grant human
acceptance.

<a id="references"></a>
## Nachweise und Vertiefung / Evidence and references

- [Messmethodik / Methodology](docs/methodology.md)
- [Portabler Vertrag / Portable contract](templates/project-statistics-contract.md)
- [Konfigurationsschema / Configuration schema](scripts/config/project-statistics.schema.json)
- [CLI-Referenz / CLI reference](docs/man/project-statistics.1.md)
- [Herkunft und Lieferkette / Provenance and supply chain](docs/provenance.md)
- [Lieferung, Freigaben und Historie / Delivery and history](docs/delivery.md)
- [Abgenommener Feldtestbericht / Accepted field report](docs/field-tests/v0.1.0/report.md)
- [Hash-Manifest / Hash manifest](docs/field-tests/v0.1.0/manifest.json)
- [Gemergter Feldtest-PR #3 / Merged field-test PR #3](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/pull/3)

Die aktuelle README auf `main` erklaert die regulaere Freigabe. Das
unveraenderte Tag-ZIP enthaelt seinen historischen Pre-Release-Dokumentationsstand;
das ist keine neue Paketversion. Community-Aufnahme, weitere Installationen
und Legacy-Migration benoetigen eigene Auftraege. Eine Veroeffentlichung rollt
nichts automatisch in Zielprojekte aus.

The current README explains regular release approval. The unchanged tag ZIP
retains historical pre-release documentation, not a new package version.
Catalog submission, more installations and legacy migration require separate
decisions; publication performs no rollout.
