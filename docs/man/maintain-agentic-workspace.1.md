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

Secure-Trader-Ziele werden anhand von
`scripts/config/maintenance-execution-contexts.json` einer freigegebenen
Podman-Sandbox zugeordnet. Podman ist die hier verwendete Container-Laufzeit.
Lokale Git- und Wartungsoperationen dieser Ziele haben keinen Host-Fallback.
Host-Git-Netzwerkoperationen fuer diese beschreibbar eingebundenen Ziele sind
gesperrt, damit deren Repository-Konfiguration keine Host-Prozesse auswaehlen
kann. Der begrenzte Bundle-Transport verwendet stattdessen frische Bare-Repos
auf dem Host und die 21 kanonischen HTTPS-Adressen. Er liest keine lokale
Secure-Trader-Konfiguration. Alle Snapshots werden vor Fast-forwards geladen;
Hash und Ref-Menge werden in der Sandbox geprueft. Nur origin-Tracking-Refs
werden importiert; lokale Branches und Tags bleiben erhalten. Ohne gueltigen
Preflight und vorbereiteten Transport bleibt `HostGitTransportBlocked` aktiv.
Zugangsdaten werden nicht
in den Container kopiert. Vor spaeteren Fetches und Änderungen müssen Mounts,
Owner-Freigabe, Ablaufdatum und Hashbindung des eingebauten Quellpakets
übereinstimmen. Ein laufender Container allein genügt nicht.
Schema 3 verwendet eine ausdruecklich dokumentierte Owner-Entscheidung und
Repository-Review statt eines zusaetzlichen Owner-Schluessels. Der Status
`PendingOwnerApproval` bleibt gesperrt; nur eine ausdrueckliche Freigabe darf
ihn auf `OwnerApproved` setzen. Dokument-, Vertrags- und Paket-Hashes erkennen
Abweichungen, authentifizieren aber nicht den Owner. Image, Paket, Mounts und
Ablaufdatum bleiben gebunden. Die Git-Commit-ID ist Herkunftsevidence, keine
selbstreferenzielle Freigabebedingung. Schema 2 erfordert explizite Migration.
Bei `SandboxPreflightBlocked` zuerst den Bericht prüfen und das geprüfte
Quellpaket ausdrücklich veröffentlichen sowie im Sandbox-Image pinnen.
Das Wartungsskript baut oder ersetzt kein Image automatisch.
Arbeits- und Freigabestand:
[Container-Delegation](../maintenance/container-delegation-2026-09-20.md).

*Secure Trader targets use the approved Podman sandbox declared in
`scripts/config/maintenance-execution-contexts.json`. Podman is the container
runtime used here. Local Git and maintenance operations never fall back to
the host. Host Git network operations are blocked for these writable mounted
targets so repository-controlled configuration cannot select host processes.
The bounded bundle transport instead uses fresh host bare repositories and
the 21 canonical HTTPS URLs, never local Secure Trader configuration. All
snapshots are downloaded before fast-forwards; hashes and refs are verified
inside the sandbox. Imports update only origin tracking refs, preserving local
branches and tags. Without valid preflight and prepared transport, targets
still report `HostGitTransportBlocked`. Credentials are not copied into the
container. Before later fetches or changes, mounts, Owner approval,
expiry and embedded package hashes must match. A running container alone
is insufficient. Schema 3 uses an explicit documented owner decision and
repository review instead of an additional owner key. `PendingOwnerApproval`
blocks execution; only explicit approval may set `OwnerApproved`. Document,
contract and package hashes detect drift, but do not authenticate the owner.
Image, package, mounts and expiry remain bound. Git commit IDs are provenance,
not a self-referential approval requirement. Schema 2 needs explicit migration.
An unapproved newer image remains fail-closed
until the owner explicitly renews approval. Every active Secure Trader root must be present in the
contract, and the package hash covers every helper executed by the leaf
worker. An incomplete mount or helper closure blocks before Git. For
`SandboxPreflightBlocked`, inspect the report, explicitly
publish the reviewed package and pin it in the sandbox image. Maintenance
never rebuilds or replaces the image automatically.*

Nur auf Windows darf ein Podman-Mount fuer `C:\Users\name\Projects` exakt
als `/mnt/c/Users/name/Projects` gemeldet werden. Der Laufwerksbuchstabe und
der vollstaendige Pfad muessen passen; andere Wurzeln, Nachbarverzeichnisse
und `..` werden nicht durch diese Uebersetzung akzeptiert. Die Linux-Schreibweise
wird nicht ohne Beachtung der Gross-/Kleinschreibung verglichen.
macOS- und native Linux-Pfadpruefungen bleiben unveraendert.

*Only on Windows may Podman report `C:\Users\name\Projects` exactly as
`/mnt/c/Users/name/Projects`. The drive and complete path must match; this
translation does not accept other roots, sibling directories or `..`.
Linux spelling is compared case-sensitively. macOS and native Linux path
checks remain unchanged.*

Nachweis / Evidence:
[Windows-Podman-Mountpfade](https://github.com/hindermath/home-baseline/blob/main/docs/maintenance/windows-podman-mount-paths-2026-09-26.md).

Nach bestandener Vorprüfung gilt Git-Vertrauen nur für den jeweiligen
Wartungsprozess und die exakt deklarierten Repository-Pfade. Eine leere
Vertrauensliste setzt geerbte pauschale Freigaben zurück, bevor die geprüften
Pfade ergänzt werden. Es wird keine `.gitconfig` geschrieben. Damit bleibt
`--check-only` schreibfrei und die Wartung benötigt nach einer
Container-Neuerzeugung keine manuelle `safe.directory`-Liste. Interaktive
Git-Aufrufe außerhalb der Wartung erhalten dadurch keine Freigabe.
Auf anderen Systemen müssen deren Mounts und Zielpfade im freigegebenen
Ausführungs- und Flottenvertrag übereinstimmen; es gibt keine Pfadsuche oder
Wildcard-Freigabe. Details und Aktivierungsstand:
[Prozessgebundenes Git-Vertrauen](https://github.com/hindermath/home-baseline/blob/4459e744d126d51b832986b09ac1a73cf88c1607/docs/maintenance/container-git-trust.md).

*After successful preflight, Git trust applies only to the maintenance process
and exact declared repository paths. An empty trust entry resets inherited
broad permissions before validated paths are added. No `.gitconfig` is written;
check-only stays read-only and recreated containers need no manual trust list
for maintenance. Interactive Git outside maintenance receives no additional
trust. Other systems must match their approved execution and fleet contracts;
there is no path discovery or wildcard permission. See the linked operating
note for activation status.*

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
Das Storage-Profil ist sichtbar `Safe` vorausgewählt. `Deep` benötigt bei
einem echten Lauf eine eigene zweite Bestätigung.

*With no options, a fully interactive terminal first opens the maintenance
TUI with Dry-run selected. Redirected input or output preserves the previous
unattended full-maintenance contract. Every existing maintenance option
remains headless. The UI displays the typed selection and equivalent command
before starting exactly one engine process. Mutation confirmation defaults to
No, and the UI grants no repository, provider, secret, or administrator
authority.*

Nach dem Engine-Start gilt folgende Reihenfolge:

1. Lock, Log und atomarer Bericht werden als Kontroll-Evidence angelegt.
   State-, Log- und Reportverzeichnisse sind nur fuer den Benutzer zugaenglich;
   neue Logs werden exklusiv mit Modus `0600` angelegt. Link- oder
   Namenskollisionen blockieren den Lauf.
2. Die **Remote-Freshness-Barriere** prueft Level 0 und jedes aktive
   Git-Ziel. Nicht sandboxgebundene Ziele erhalten begrenzte
   `fetch --prune`-Versuche. Sandboxgebundene Ziele blockieren derzeit vor
   Host-Git-Netzwerkzugriff mit `HostGitTransportBlocked`.
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
   zwoelf Presets sind der aktuelle Flottennachweis, keine Code-Obergrenze. Liegt
   der aktive Arbeitsbaum nicht exakt
   auf `origin/HEAD`, erfolgt die schreibfreie Profilpruefung in einem
   kurzlebigen detached Worktree des kanonischen Default-Branches. Drift dort
   erfordert einen eigenen Branch beziehungsweise PR.
9. Homebrew/apt oder WinGet, Required-CLI-Tools, VS-Code-Extensions und
   Required-Agenten-CLIs werden gepflegt.
10. Lokales Modell-Routing wird schreibfrei geprüft.
11. Die Storage-Stufe inventarisiert und bereinigt nach dem gewählten Profil
    verifizierte Level-2-Buildausgaben, Caches und ausschließlich dangling
    Container-Images. `Safe` ist Standard; `scripts-only` verwendet `None`.
12. Repository-Paritaet und Wartungspaket werden abschliessend erneut geprueft.

*Control evidence is created first. The Remote Freshness Barrier then attempts
bounded fetches for Level 0 and every active Git target before any domain
mutation. Only a clean canonical default branch with an unambiguous upstream,
zero ahead commits, and a purely behind state may use `pull --ff-only`.
Profiles and preset counts are resolved from the catalog and referenced
matrices; the current count is evidence, not a coded maximum.*

Die unterstuetzten Profilnamen und ihre Matrixdateien stehen zentral in
`scripts/config/spec-kit-preset-profiles.json`. Lokale Registry-Eintraege mit
unbekannten Profilen brechen weiterhin fail-closed ab.

Die lokale Registry kann `level0PresetProfile` fuer die ausfuehrende
Level-0-Quelle setzen. Fehlt dieses Feld, gilt wie bisher
`defaultPresetProfile`. Projektbezogene 14er-Freigaben erhoehen dadurch
nicht den Standard fuer andere oder neu registrierte Repositories.
`model-routing` und `storage-cleanup` sind gueltige Live-Ereignisphasen
im unveraenderten Ereignisschema 1.

*The local registry may set `level0PresetProfile` for the executing Level-0
source. When absent, `defaultPresetProfile` remains the fallback. A reviewed
fourteen-preset opt-in does not raise the default for other or newly
registered repositories. Model routing and storage cleanup are valid live
event phases in the unchanged event schema version 1.*

Die portable Sollquelle steht in
`scripts/config/agentic-workspace-fleet.json`. Sie unterscheidet kanonische
Flottenziele, Preset-Repositories und reine Collections. Der gemeinsame
Python-Standardbibliothekskern validiert Pfade, Remotes, Branches und
Ahead-/Behind-Zustaende fuer beide Einstiegspunkte identisch. Registry-Aufbau
und Wartungspaket-Propagation werden auf aktive Git-Ziele der Klasse
`canonical-fleet` begrenzt. Eine Dateisystemsuche darf keine nicht
deklarierten Legacy-Repositories erneut registrieren oder propagieren.
Die reine Collection `SpecKitPresetProjects` besitzt selbst weder Remote noch
Branch. Ihre aktiven, explizit deklarierten Git-Mitglieder entsprechen exakt
den zwoelf Repository-Quellen des verwalteten Profils
`model-routing-twelve-governance-presets`; ein Regressionstest bindet beide
Vertraege aneinander.

*The portable desired state lives in
`scripts/config/agentic-workspace-fleet.json`. It distinguishes canonical
fleet targets, preset repositories, and directory-only collections. The
shared Python standard-library core validates paths, remotes, branches, and
ahead/behind states identically for both entry points. Registry maintenance
and maintenance-package propagation are restricted to active Git targets in
the `canonical-fleet` class. Filesystem discovery cannot re-register or
propagate undeclared legacy repositories. The directory-only
`SpecKitPresetProjects` collection has no remote or branch of its own. Its
active, explicitly declared Git members exactly match the twelve repository
sources of the managed `model-routing-twelve-governance-presets` profile; a
regression test binds both contracts together.*

Collection-Wurzeln und alle manifestierten Zielpfade bleiben physisch unter
`HOME`. Windows-Laufwerkspraefixe sowie verlinkte oder konfliktbehaftete
Collection-Wurzeln werden abgewiesen. Deren deklarierte Kinder werden nur als
blockiert inventarisiert; Clone, Fetch und Fast-forward werden nicht gestartet.

*Collection roots and every declared target remain physically below `HOME`.
Windows drive prefixes and linked or conflicting collection roots are rejected.
Declared children are inventoried as blocked; clone, fetch and fast-forward do
not start.*

Im Check-only-Modus wird der manifestgesteuerte Home-Sync jetzt ebenfalls
schreibfrei ausgefuehrt. Nach einem echten Sync wiederholt die
Abschlusspruefung diesen Check, damit SHA-256-, Dateimodus- oder
Konfliktabweichungen nicht unbemerkt bleiben.

*Check-only now also runs the manifest-based Home sync check without writing.
After a real sync, final verification repeats this check so SHA-256, file-mode,
or conflict drift cannot remain unnoticed.*

### Lokaler CI-Gate / Local CI gate

`--ci-gate` beziehungsweise `-CiGate` startet genau einen gemeinsamen
Python-Engine-Prozess. Die Vorschau `--dry-run` / `-WhatIf` zeigt Profil,
Entscheidung, Status, Blocker, nächste Aktion, Gate-Set-Hash, geordnete Gates
und Evidence-Ziel in stabiler linearer Reihenfolge. Sie schreibt weder Evidence
noch Repository- oder Home-Dateien. Ein echter Gate-Lauf publiziert nur nach
stabilem, sauberem HEAD und vollständigem Erfolg atomare maschinenlokale
Evidence. Fixture-Identitäten sind auf Vorschau und explizite Tests beschränkt.
Der Pre-Push-Hook akzeptiert Evidence nur für den aktuell ausgecheckten,
tatsächlich geprüften ausgehenden Commit; Pfadgates werden aus dessen
Commit-Diff abgeleitet.

*The CI-gate surface starts exactly one shared engine process. Preview is
read-only and text-first. A real run publishes machine-local evidence only
for an exact clean and stable HEAD after all gates and the gate-set hash pass.
Fixture identities are preview/test-only; pre-push binds evidence and path
selection to the checked-out outgoing commit.*

<a id="stage-b-flottenrollout-stage-b-fleet-rollout"></a>

### Stage-B-Flottenrollout / Stage B fleet rollout

Stage B wird ausschließlich aus dem versionierten Level-0-Quellcheckout
ausgeführt. Auch eine Projekt- oder Home-Kopie des Wrappers verwendet den
gemeinsamen Quellresolver: ausführendes Quellrepository, `HOME_BASELINE_SOURCE`,
lokaler Quellnachweis, dann `~/home-baseline-source`. Engine, Konfigurationen
und angenommene Vertragsschemata werden gemeinsam dort gelesen. Fehlt die
Quelle oder der zentrale Kern, bricht der Aufruf ab; es gibt keinen Rückfall
auf Projektkopien. Die Level-0-Spezifikationen werden nicht in Projekte
verteilt. Diese Auflösung erteilt keine zusätzliche Ausführungsautorität und
ändert weder normale Wartung noch das projektbezogene `--ci-gate`.

*Stage B runs exclusively from the versioned Level-0 source checkout. Project
and Home wrapper copies use the shared source resolver: executing source
repository, `HOME_BASELINE_SOURCE`, local source record, then
`~/home-baseline-source`. Engine, configuration and accepted contract schemas
are read together there. Missing source or central engine stops execution;
there is no project-local fallback. Level-0 specifications are not distributed
to projects. Resolution grants no additional execution authority and changes
neither ordinary maintenance nor the project-local `--ci-gate`.*

Die sichere erste Aktion ist eine schreibfreie Vorschau:

```bash
bash scripts/maintain-agentic-workspace.sh --stage-b-action preflight --dry-run
```

```powershell
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -StageBAction Preflight -WhatIf
```

`preflight`, `validate`, `deliver`, `resume` und `verify` starten genau einen
gemeinsamen Engine-Prozess. Vorschauen öffnen das `ExternalWriteGate` nicht.
Derzeit ist ausschließlich `preflight` operativ implementiert. `validate`,
`deliver`, `resume` und `verify` beenden sich bis zu ihrer vollständigen
Implementierung fail-closed mit `Blocked` und Exitcode `1`; sie erzeugen keine
Erfolgs- oder Konvergenzevidence.
Die Preflight-Vorschau berechnet und zeigt den vollständigen Live-Plan, schreibt
aber weder Evidence, Git, Provider, Home, Zielrepositories, Plan noch State.
Ein echter `preflight` darf erst nach separater Lieferung der
Preflight-Implementierung vom sauberen, synchronisierten Level-0-Default-Head
laufen. Er liest Flotte und Provider live und publiziert ausschließlich lokal
zuerst den atomaren Rolloutplan und danach den hashbindenden vorbereiteten State
als Commit-Marker. Der Snapshot bleibt im Speicher; Git, Provider, Home und
Zielrepositories bleiben unverändert. Vorbereitete Authority ist `Pending`, das
Gate ist `Closed` und Admin-Bypass `NotAuthorized`.
Ein echter `deliver`- oder `resume`-Lauf benötigt eine aktuelle, hashgebundene
`MergeAndSync`-Autorität und arbeitet Welle für Welle mit höchstens einem
Writer. Beim ersten nicht behebbaren Fehler stoppt er vor dem nächsten Ziel;
`resume` prüft Plan, Flotte, Providerzustand, Budget und Autorität erneut.

*The five Stage-B actions start exactly one shared engine process. Preview
never opens the ExternalWriteGate. Only `preflight` is operationally
implemented at present. Until their complete implementation, `validate`,
`deliver`, `resume`, and `verify` fail closed with `Blocked` and exit code `1`;
they produce no success or convergence evidence. Preflight preview calculates and prints the
complete live plan with zero evidence, Git, provider, Home, target, plan, or
state writes. After the implementation has been delivered
separately, a real preflight may publish only the local atomic plan followed by
its hash-bound prepared state commit marker; authority remains Pending and no
Git, provider, Home, or target write occurs. A real delivery or resume needs a current,
hash-bound MergeAndSync authority and uses at most one writer. It stops before
the next target on the first non-recoverable failure; resume revalidates the
plan, fleet, provider state, budget, and authority.*

Maschinenlokale Evidence wird atomar unter dem laufgebundenen Evidence-Root
veröffentlicht. Vor Anlage und Austausch werden alle Pfadkomponenten geprüft;
benutzergesteuerte Links sowie Windows-Reparse-Points blockieren die
Veröffentlichung. Exitcode `0` bedeutet Erfolg oder sichere Vorschau, `1` einen
fachlichen Blocker, `2` Vertrags-, Betriebs- oder Sicherheitsfehler und `130`
einen kontrollierten Abbruch. Regulärer Review und Merge sind der Normalweg.
Der Admin-Bypass ist nur nach belegter Schutzregel-Ablehnung erlaubt und ersetzt
keine Acceptance-, Security-, Review- oder Gate-Evidence. G4, Intake-Serie,
Copilot-, Konto- und Abonnementkonfiguration liegen außerhalb von Stage B.

*Evidence is published atomically below the run-bound evidence root. Every path
component is checked before creation and replacement; user-controlled links and
Windows reparse points block publication. Exit codes
are 0 success/preview, 1 business blocker, 2 contract/operational/security
failure, and 130 controlled stop. Regular review and merge are normal. Admin
bypass is restricted to an evidenced protection-only refusal and replaces no
acceptance, security, review, or gate evidence. G4 and account-level settings
remain outside Stage B.*

Stufe A führt keinen Commit, Push, Merge, Home-Sync, Zielrepository-Write oder
GitHub-Schreibzugriff aus und behauptet nie Remote-Konvergenz. Exitcodes:
`0` Erfolg/Vorschau, `1` fachlicher Blocker, `2` Vertrag/Betrieb/Sicherheit,
`130` kontrollierter Abbruch. Code `3` bleibt dem bestehenden
Maintenance-Reparaturvertrag vorbehalten und wird vom CI-Gate nicht erzeugt.

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
Bericht. Die Storage-Stufe bettet ihren privaten atomaren Detailbericht mit
Profil, Pressure Mode, Kandidaten, Bytes, Non-MSL-Begründungen und Warnungen
ebenfalls ein. Normaler Abschluss, ein spaeter Fehler sowie `INT`/`TERM` ersetzen
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
| `--ci-gate` | `-CiGate` | Lokalen profilgebundenen Gate ausführen; mit Vorschau keine Evidence / Run the profile-bound local gate; preview writes no evidence |
| `--stage-b-action preflight\|validate\|deliver\|resume\|verify` | `-StageBAction Preflight\|Validate\|Deliver\|Resume\|Verify` | Genau eine Stage-B-Aktion; zuerst Vorschau / Exactly one Stage-B action; preview first |
| `--scripts-only` | `-ScriptsOnly` | Maschinenpakete ueberspringen / Skip machine packages |
| `--repair-drift` | `-RepairDrift` | Wartungspaket lokal reparieren; nie committen/pushen / Repair package locally; never commit/push |
| `--include-optional` | `-IncludeOptional` | Auch optionale Maschinenpakete installieren / Install optional machine packages too |
| `--allow-admin-prompts` | `-AllowAdminPrompts` | Administratorabfragen nur fuer diesen Lauf erlauben / Allow administrator prompts for this run only |
| `--cleanup-profile safe\|deep\|none` | `-CleanupProfile Safe\|Deep\|None` | Storage-Profil; Standard Safe / Storage profile; default Safe |
| `--confirm-deep-cleanup` | `-ConfirmDeepCleanup` | Echten Deep-Lauf zusätzlich bestätigen / Confirm an update Deep run separately |
| `--manifest PATH` | `-ManifestPath PATH` | Alternatives Fleet-Manifest / Alternative fleet manifest |
| `--home-dir PATH` | `-HomeDir PATH` | Alternatives Home fuer Tests/Profile / Alternative home for tests/profiles |
| — | `-GitRetryAttempts N` | Begrenzte Versuche nur fuer transiente Git-Netzwerkfehler / Bounded attempts for transient Git network failures only |
| — | `-GitTimeoutSeconds N` | Harte Grenze je Fetch-/Pull-Versuch / Hard limit per fetch/pull attempt |
| — | `-WinGetTimeoutSeconds N` | Harte Grenze je WinGet-Unterprozess / Hard limit per WinGet subprocess |

`--check-only` / `-CheckOnly` und Vorschau sind gegenseitig exklusiv.
Drift-Reparatur ist nur in einem echten Lauf erlaubt. Optionale Pakete sind im
`scripts-only`-Modus nicht anwendbar; dieser Modus erzwingt Storage-Profil
`None`. Ein echter Deep-Lauf erfordert eine eigene Bestätigung.
Administratorinteraktion ist
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

Elternkomponenten und Zieldatei des Ereigniskanals werden vor der Anlage
link-sicher geprüft. Ein bereits vorhandenes oder umgeleitetes Ziel blockiert
den Start; die neue Datei wird privat und exklusiv angelegt. Unter Windows
wird der erste Ereignisdatensatz erst nach einer Owner-only-DACL veröffentlicht.
Ein vorhandener Ereigniskanal wird vor jedem Append erneut gehärtet; ein
ACL-Fehler bricht vor neuen Ereignisbytes ab.

Die Frontends begrenzen gespeicherte Kindprozessausgaben, einzelne
Ereignisdatensätze, den Ereigniskanal, die Anzahl vorgehaltener Ereignisse und
den Abschlussbericht. Eine überschrittene PowerShell-Ausgabegrenze ist ein
Fehler und darf nicht als erfolgreicher, nur abgeschnittener Paket- oder
Git-Status weiterverarbeitet werden. Die TUI verwirft überzählige Anzeigedaten
beziehungsweise wechselt in den degradierten Modus; der begrenzte,
laufzugehörige Abschlussbericht und der Prozess-Exitcode bleiben kanonisch.

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
event channel's parent components and target are checked against links before
creation. An existing or redirected target blocks startup; a new file is
created privately and exclusively. On Windows, the first event record is not
published until an owner-only DACL has been applied; an existing stream is
rehardened before every append and ACL failure aborts before new event bytes.
The
frontends place fixed limits on retained child output, individual events, the
event stream, retained event count, and the final report. PowerShell output
overflow fails closed; the TUI discards excess presentation data or degrades
while the bounded run-owned report and process exit remain canonical. The
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
bash scripts/maintain-agentic-workspace.sh --dry-run --cleanup-profile safe
bash scripts/maintain-agentic-workspace.sh --cleanup-profile deep --confirm-deep-cleanup
bash scripts/maintain-agentic-workspace.sh --manifest /tmp/fleet.json --home-dir /tmp/test-home --dry-run
bash scripts/maintain-agentic-workspace.sh
bash scripts/maintain-agentic-workspace.sh --scripts-only --repair-drift
```

```powershell
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -Tui
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -PlainUi
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -CheckOnly
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -WhatIf
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -WhatIf -CleanupProfile Safe
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -CleanupProfile Deep -ConfirmDeepCleanup
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -WhatIf -GitRetryAttempts 3 -GitTimeoutSeconds 300 -WinGetTimeoutSeconds 1800
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -ManifestPath C:\Temp\fleet.json -HomeDir C:\Temp\TestHome -WhatIf
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1
pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -ScriptsOnly -RepairDrift
```

## SEE ALSO

`maintain-workspace-storage(1)`, `maintain-agentic-brew-apps(1)`,
`maintain-agentic-winget-apps(1)`,
`propagate-agentic-toolchain-maintenance(1)`, `register-level2-repository(1)`,
`sync-home(1)`
