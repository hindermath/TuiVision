# maintain-agentic-workspace(1)

## NAME

`maintain-agentic-workspace` - orchestriert Repository- und Toolchain-Wartung

*Orchestrates repository and toolchain maintenance.*

## SYNOPSIS

```bash
bash scripts/maintain-agentic-workspace.sh [OPTIONEN]
```

```powershell
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 [OPTIONEN]
```

## DESCRIPTION

Ohne Optionen öffnet ein vollständig interaktives Terminal zuerst die
Wartungs-TUI. TUI bedeutet Terminal User Interface, also eine
textbasierte Benutzungsoberfläche im Terminal. Die Vorauswahl ist
`Dry-run`. Bei umgeleiteter Ein- oder Ausgabe bleibt der bisherige
unbeaufsichtigte Vollwartungsvertrag erhalten. Jeder vorhandene
Wartungsparameter bleibt headless.

Die TUI zeigt die typisierte Auswahl und den entsprechenden Shell-Befehl,
bevor sie genau einen Engine-Prozess startet. Eine echte Mutation benötigt
eine Bestätigung mit Standard `Nein`. Die Oberfläche erteilt keine
Repository-, Provider-, Secret- oder Administratorrechte.

*With no options, a fully interactive terminal first opens the maintenance
TUI with Dry-run selected. Redirected input or output preserves the previous
unattended full-maintenance contract. Every existing maintenance option
remains headless. The UI displays the typed selection and equivalent command
before starting exactly one engine process. Mutation confirmation defaults to
No, and the UI grants no repository, provider, secret, or administrator
authority.*

Nach dem Engine-Start gilt folgende Reihenfolge:

1. Lock, Log und atomarer Bericht werden als Kontroll-Evidence angelegt.
2. Die **Remote-Freshness-Barriere** prueft Level 0 und jedes aktive
   Git-Ziel. Sie fuehrt alle begrenzten `fetch --prune`-Versuche aus, bevor
   Home-Sync, Registry, Propagation, Preset-Reparatur oder Toolchain beginnen.
3. Nur ein sauberer kanonischer Default-Branch mit eindeutigem Upstream,
   `ahead=0` und `behind>0` wird per `pull --ff-only` aktualisiert.
4. Die kanonische Baseline wird nach `~/` synchronisiert.
5. Das versionierte Desired-State-Manifest wird validiert. Fehlende aktive
   Repositories werden ueber einen geprueften temporaeren Geschwisterklon
   bereitgestellt; bestehende sichere Repositories werden nur per Fast-forward
   aktualisiert.
6. Fehlende Registry-Eintraege werden ueber `register-level2-repository.*`
   nachgezogen.
7. Das kanonische Wartungspaket wird mit
   `propagate-agentic-toolchain-maintenance.*` geprueft.
8. Das Registry-Profil jedes Repositories wird gegen die im Profilkatalog
   referenzierte Matrix geprueft. Anzahl und IDs werden aus den Daten gelesen;
   elf Presets sind der aktuelle Flottennachweis, keine Code-Obergrenze. Liegt
   der aktive Arbeitsbaum nicht exakt
   auf `origin/HEAD`, erfolgt die schreibfreie Profilpruefung in einem
   kurzlebigen detached Worktree des kanonischen Default-Branches. Drift dort
   erfordert einen eigenen Branch beziehungsweise PR.
9. Homebrew/apt oder WinGet, Required-CLI-Tools, VS-Code-Extensions und
   Required-Agenten-CLIs werden gepflegt.
10. Repository-Paritaet und Wartungspaket werden abschliessend erneut geprueft.

*Control evidence is created first. The Remote Freshness Barrier then attempts
bounded fetches for Level 0 and every active Git target before any domain
mutation. Only a clean canonical default branch with an unambiguous upstream,
zero ahead commits, and a purely behind state may use `pull --ff-only`.
Profiles and preset counts are resolved from the catalog and referenced
matrices; the current count is evidence, not a coded maximum.*

Die unterstuetzten Profilnamen und ihre Matrixdateien stehen zentral in
`scripts/config/spec-kit-preset-profiles.json`. Lokale Registry-Eintraege mit
unbekannten Profilen brechen weiterhin fail-closed ab.

Die portable Sollquelle steht in
`scripts/config/agentic-workspace-fleet.json`. Sie unterscheidet kanonische
Flottenziele, Preset-Repositories und reine Collections. Der gemeinsame
Python-Standardbibliothekskern validiert Pfade, Remotes, Branches und
Ahead-/Behind-Zustaende fuer beide Einstiegspunkte identisch. Registry-Aufbau
und Wartungspaket-Propagation werden auf aktive Git-Ziele der Klasse
`canonical-fleet` begrenzt. Eine Dateisystemsuche darf keine nicht
deklarierten Legacy-Repositories erneut registrieren oder propagieren.

*The portable desired state lives in
`scripts/config/agentic-workspace-fleet.json`. It distinguishes canonical
fleet targets, preset repositories, and directory-only collections. The
shared Python standard-library core validates paths, remotes, branches, and
ahead/behind states identically for both entry points. Registry maintenance
and maintenance-package propagation are restricted to active Git targets in
the `canonical-fleet` class. Filesystem discovery cannot re-register or
propagate undeclared legacy repositories.*

Im Check-only-Modus wird der manifestgesteuerte Home-Sync jetzt ebenfalls
schreibfrei ausgefuehrt. Nach einem echten Sync wiederholt die
Abschlusspruefung diesen Check, damit SHA-256-, Dateimodus- oder
Konfliktabweichungen nicht unbemerkt bleiben.

*Check-only now also runs the manifest-based Home sync check without writing.
After a real sync, final verification repeats this check so SHA-256, file-mode,
or conflict drift cannot remain unnoticed.*

*Preset validation resolves the canonical default branch through
validated `refs/remotes/origin/HEAD` evidence or
`git ls-remote --symref origin HEAD`; branch names are never guessed. If the
active worktree is on another or an older
commit, an isolated temporary worktree validates the exact preset matrix
without switching branches or touching untracked files. Drift on that
canonical ref requires a dedicated branch or pull request.*

Jeder temporaere Preset-Worktree besitzt vor seiner Erzeugung einen atomaren
**Lease**, also einen zeitlich begrenzten Eigentumsnachweis. Der Lease bindet
Lauf, Prozessstart, Repository, Commit und reservierte State-Pfade. Normaler
Abschluss und der naechste Start entfernen nur einen weiterhin sauberen,
Git-registrierten und eindeutig eigenen Worktree. Aktive, manipulierte,
fremde oder durch PID-Wiederverwendung mehrdeutige Evidence bleibt erhalten
und blockiert weitere mutierende Phasen. Es gibt kein globales `git clean`,
`git worktree prune`, Reset oder Stash.

*Every temporary preset worktree receives an atomic lease before creation. It
binds the run, process start, repository, commit, and reserved state paths.
Normal release and startup recovery remove only a still-clean, Git-registered,
unambiguously owned worktree. Active, tampered, foreign, or PID-reuse-ambiguous
evidence is retained and blocks later mutations. No global clean, prune,
reset, or stash is used.*

Das Skript wechselt vorhandene Branches nicht, fuehrt keinen Reset aus und
commitet oder pusht keine Level-1-/Level-2-Aenderungen. Clone-on-missing ist
nur fuer aktive, vollstaendig deklarierte Git-Ziele erlaubt. Zunaechst wird
ein temporaerer Geschwisterklon geprueft; erst danach wird er atomar an den
freien Zielpfad verschoben. Bei lokalen Aenderungen, fehlendem Upstream,
Ahead-/Diverged-Zustand oder detached HEAD stoppt es fuer das betroffene
Repository. Unabhaengige Ziele werden weiter geprueft.

Pro Home-Verzeichnis verhindert ein Lock parallele Wartungslaeufe. Pro Lauf
entstehen ein vollstaendiges lokales Log unter `~/.home-baseline/logs/` und ein
JSON-Bericht unter `~/.home-baseline/reports/`. Beide verwenden dieselbe Run-ID.
Der Toolchain-Kindprozess liefert seine geordneten Einzelresultate an denselben
Bericht. Normaler Abschluss, ein spaeter Fehler sowie `INT`/`TERM` ersetzen
einen Zwischenstatus genau einmal atomar. Terminal, Log, Reportstatus,
letzte Stufe und Prozess-Exitcode bleiben dadurch konsistent. Eigene reparierte
Dirty-Zwischenstaende werden nur mit
atomarer Resume-Evidence unter `~/.home-baseline/` fortgesetzt, wenn Pfade und
Nachher-Hashes exakt passen. Fremde oder teilweise passende Aenderungen
blockieren.

*Without options, the script performs full maintenance: it fast-forwards
Level-0, synchronizes the local home baseline, resolves declared active
Level-1/Level-2 repositories from the fleet manifest, maintains the local registry,
checks the canonical maintenance package and registry-selected preset profile,
maintains the platform toolchain, and verifies the final state. It never
switches an existing branch, resets worktrees, or commits/pushes target
changes. Missing declared repositories use a verified sibling clone. A
per-home lock prevents parallel runs; correlated local logs and JSON reports
are written below `~/.home-baseline/`. Ordered child toolchain results flow
into that report. Normal completion, a late failure, and `INT`/`TERM` finalize
exactly once through atomic replacement, keeping terminal, log, last stage,
report status, and process exit code consistent. Self-created dirty intermediate
state resumes only from atomically written evidence with exact paths and
after-hashes; unknown or partial changes block.*

## OPTIONS

| Bash | PowerShell | Wirkung / Effect |
|---|---|---|
| `--tui` | `-Tui` | Erweiterte TUI; sichtbarer linearer Fallback nur vor Engine-Start / Enhanced TUI; visible plain fallback only before engine start |
| `--plain-ui` | `-PlainUi` | Lineare, textorientierte Auswahl / Line-oriented text assistant |
| `--no-tui` | `-NoTui` | Headless Engine und interner Rekursionsschutz / Headless engine and internal recursion guard |
| `--check-only` | `-CheckOnly` | Nur fetchen und pruefen; keine Pulls, Datei- oder Paketupdates / Fetch and check only |
| `--dry-run` | `-WhatIf` | Schreibende Schritte als Vorschau / Preview mutating steps |
| `--scripts-only` | `-ScriptsOnly` | Maschinenpakete ueberspringen / Skip machine packages |
| `--repair-drift` | `-RepairDrift` | Wartungspaket lokal reparieren; nie committen/pushen / Repair package locally; never commit/push |
| `--include-optional` | `-IncludeOptional` | Auch optionale Maschinenpakete installieren / Install optional machine packages too |
| `--allow-admin-prompts` | `-AllowAdminPrompts` | Administratorabfragen nur fuer diesen Lauf erlauben / Allow administrator prompts for this run only |
| `--manifest PATH` | `-ManifestPath PATH` | Alternatives Fleet-Manifest / Alternative fleet manifest |
| `--home-dir PATH` | `-HomeDir PATH` | Alternatives Home fuer Tests/Profile / Alternative home for tests/profiles |
| — | `-GitRetryAttempts N` | Begrenzte Versuche nur fuer transiente Git-Netzwerkfehler / Bounded attempts for transient Git network failures only |
| — | `-GitTimeoutSeconds N` | Harte Grenze je Fetch-/Pull-Versuch / Hard limit per fetch/pull attempt |
| — | `-WinGetTimeoutSeconds N` | Harte Grenze je WinGet-Unterprozess / Hard limit per WinGet subprocess |

`--check-only` / `-CheckOnly` und Vorschau sind gegenseitig exklusiv.
Drift-Reparatur ist nur in einem echten Lauf erlaubt. Optionale Pakete sind im
`scripts-only`-Modus nicht anwendbar. Administratorinteraktion ist
standardmaessig gesperrt. Die Freigabe gilt nur fuer den aktuellen Prozess und
speichert keine Zugangsdaten.

*Check-only and preview are mutually exclusive. Drift repair is only allowed
in an actual run. Optional packages do not apply to scripts-only mode.
Administrator interaction is denied by default; the opt-in applies only to
the current process and stores no credentials. It never bypasses UAC, process
timeouts, repository safety checks, tests, or review gates.*

Die drei UI-Schalter sind gegenseitig ausgeschlossen. Enhanced und Plain
dürfen außer dem Home-Verzeichnis keine Wartungsoption vorwegnehmen. Ein
ungeeignetes Terminal, fehlendes .NET-10-SDK, Locked-Restore-/Buildfehler oder
ein nicht sicher nutzbarer Cache führt vor Engine-Start zum linearen
Assistenten. Nach Engine-Start wird nie ein zweiter Prozess gestartet.

Der TUI-Build liegt in einem inhaltsadressierten, plattformgebundenen Cache
unter `~/.home-baseline/cache/maintenance-tui/`. Quellfingerabdruck,
Plattform und Metadaten müssen exakt stimmen. Temporäre Builds werden erst
nach erfolgreicher Prüfung atomar veröffentlicht.

Ein interner JSONL-Kanal unter `~/.home-baseline/events/` enthält vollständige
UTF-8-Ereigniszeilen mit Schema, Run-ID und streng steigender Sequenz. Diese
Ereignisse unterstützen nur die Live-Anzeige. Bei Drift oder Beschädigung
zeigt die Anzeige dauerhaft `EVENT_STREAM_DEGRADED` und wechselt in den
linearen Modus. Bericht und Exitcode bleiben die Abschlusswahrheit. Die
Schlussansicht nennt Mutationsbarriere, Repository-Zählung, Preset-Phase,
Bericht, Log und nächste Aktion als kopierbaren Text.

Der erwartete Berichtspfad wird vor dem Engine-Start aus Home-Verzeichnis und
Run-ID gebildet. Deshalb bleibt ein finalisierter, laufzugehöriger Bericht auch
ohne nutzbares `run-completed`-Ereignis auffindbar. Ein vorhandenes
Abschlussereignis muss mit Bericht und Prozess-Exitcode übereinstimmen.

Eine lokale Home-Runtime delegiert den argumentlosen Aufruf unter
macOS-System-Bash 3.2 ohne unsichere leere Array-Expansion an genau einen
Prozess der versionierten Level-0-Quelle.

Ein erstes `Ctrl+C` wird genau einmal an den laufenden Engine-Prozess
weitergegeben. Weitere Signale starten keinen zweiten Prozess und lösen keine
automatische Bereinigung aus. Der kontrollierte Abbruch endet mit Exitcode
`130`, sobald die Engine ihren Abschlusszustand geschrieben hat.

*UI selectors are mutually exclusive. Enhanced and plain UI cannot preselect
maintenance options. Unsupported terminal capability, missing .NET 10, locked
restore or build failure, or an unsafe cache falls back before engine start.
The platform-bound content-addressed cache accepts only exact source and
metadata. Internal JSONL events support live display only. Event drift causes
permanent linear degradation with `EVENT_STREAM_DEGRADED`, while report and
process exit remain canonical. The final view keeps the mutation barrier,
repository counts, preset phase, report, log, and next action copyable. The
first `Ctrl+C` is forwarded exactly once; later signals cannot start another
process or trigger automatic cleanup. The expected report path is bound from
the home directory and run ID before engine start, so a finalized run-owned
report remains available without a usable `run-completed` event. A present
completion event must match the report and process exit. Under macOS system
Bash 3.2, the Home Runtime delegates an argument-free invocation without an
unsafe empty-array expansion.*

## EXIT STATUS

| Code | Bedeutung / Meaning |
|---|---|
| `0` | Aktuell oder erfolgreich abgeschlossen / Current or completed successfully |
| `1` | Drift oder nicht synchroner Zustand gefunden / Drift or unsynchronized state found |
| `2` | Betriebs-, Parameter- oder Sicherheitsfehler / Operational, parameter, or safety error |
| `3` | Drift lokal repariert; separate Pruefung, Commit und Push erforderlich / Drift repaired locally; separate review, commit, and push required |
| `130` | Vor oder während des Laufs durch den Benutzer abgebrochen / Cancelled by the user before or during the run |

Ein nicht sicher abschliessbarer WinGet-Adminvorgang wird intern als
`DEFERRED_ADMIN_REQUIRED` klassifiziert und am Orchestrator als blockierter
Teilabschluss mit Exitcode `1` berichtet.

*A WinGet administrator operation that cannot complete safely is classified
internally as `DEFERRED_ADMIN_REQUIRED` and reported by the orchestrator as a
blocked partial result with exit code `1`.*

Dasselbe gilt auf Linux: Ohne aktuelle `--allow-admin-prompts`-Autoritaet wird
die Toolchain schreibfrei verglichen. Bei installierbarem Required-Drift erfolgt
kein `sudo`; Bericht und Exitcode `1` nennen
`DEFERRED_ADMIN_REQUIRED` und die vollständige Restmenge. Signale enden
kanonisch mit `130` (`INT`) beziehungsweise `143` (`TERM`).

*The same applies on Linux: without current `--allow-admin-prompts` authority,
the toolchain is compared without mutation. Installable required drift never
starts `sudo`; the report and exit `1` preserve `DEFERRED_ADMIN_REQUIRED` and
the complete remaining set. Signals use canonical exit `130` (`INT`) or `143`
(`TERM`).*

## EXAMPLES

```bash
bash scripts/maintain-agentic-workspace.sh --tui
bash scripts/maintain-agentic-workspace.sh --plain-ui
bash scripts/maintain-agentic-workspace.sh --check-only
bash scripts/maintain-agentic-workspace.sh --dry-run
bash scripts/maintain-agentic-workspace.sh --manifest /tmp/fleet.json --home-dir /tmp/test-home --dry-run
bash scripts/maintain-agentic-workspace.sh
bash scripts/maintain-agentic-workspace.sh --scripts-only --repair-drift
```

```powershell
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -Tui
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -PlainUi
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -CheckOnly
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -WhatIf
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -WhatIf -GitRetryAttempts 3 -GitTimeoutSeconds 300 -WinGetTimeoutSeconds 1800
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -ManifestPath C:\Temp\fleet.json -HomeDir C:\Temp\TestHome -WhatIf
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -ScriptsOnly -RepairDrift
```

## SEE ALSO

`maintain-agentic-brew-apps(1)`, `maintain-agentic-winget-apps(1)`,
`propagate-agentic-toolchain-maintenance(1)`, `register-level2-repository(1)`,
`sync-home(1)`
