# maintain-agentic-brew-apps(1)

## Name

`maintain-agentic-brew-apps` - pflegt die Homebrew-/apt-Toolchain fuer agentische Entwicklung

*maintains the Homebrew/apt toolchain for agentic development*

## Synopsis

```bash
bash scripts/maintain-agentic-brew-apps.sh [--dry-run] [--compare-only] [--allow-admin-prompts] [--result-file PATH] [--skip-upgrade] [--skip-vscode-extensions] [--include-optional] [--cli-registry PATH] [--npm-agent-registry PATH] [--powershell-module-registry PATH]
```

## Beschreibung / Description

`--msl-only` begrenzt die Wartung auf die sechs stabilen MSL-Toolchains und
deren Manager-/Paketabhaengigkeiten. Vergleich (`--compare-only`) und Vorschau
(`--dry-run`) bleiben ohne Installations- und Profilveraenderungen. Hersteller-,
Paket- und aktive CLI-Versionen werden getrennt geprueft; offene MSL-Befunde
verhindern einen vollstaendigen Erfolg auch im normalen Lauf.

Registry-Vergleich und Vorschau dürfen ausschließlich die im Code fest
freigegebenen Versionsproben ausführen. Eine alternative CLI-Registry kann
weder Programm noch Argumente einer Probe ändern; dies gilt auch für primäre
MSL- und Begleitproben.

*Compare and preview may execute only version probes fixed in code policy. An
alternate CLI registry cannot change a probe executable or its arguments,
including primary and companion MSL probes.*
[Quellen und Benutzerumgebung](https://github.com/hindermath/home-baseline/blob/main/docs/maintenance/msl-toolchains.md).

*Use `--msl-only` for the six stable MSL toolchains and their dependencies.
Comparison and preview do not change installations or profiles. Upstream,
package and active CLI versions are checked separately. MSL findings prevent
complete success in full maintenance as well.*

Bash ab Version 5 ist unter macOS und Linux ein Pflichtprogramm. Homebrew
installiert und aktualisiert die Formel `bash`; ohne Homebrew verwendet Linux
das apt-Paket `bash`. Die CLI-Probe prueft die tatsaechlich ueber PATH aufgeloeste
Bash anhand von `BASH_VERSINFO` und gibt `BASH_VERSION` aus. Bash 3.2 erfuellt
die Voraussetzung nicht. Homebrew-Link-Drift wird separat gemeldet; bei falscher
PATH-Reihenfolge muss das Homebrew-bin-Verzeichnis vor dem Systempfad stehen.
Login-Shell, `/bin/bash` und `/etc/shells` bleiben unveraendert. Windows verwendet
weiterhin PowerShell 7. Die Mindestversion ist 5.0 ohne Major-Obergrenze.

*Bash 5+ is required on macOS and Linux. Homebrew installs and upgrades the
`bash` formula; Linux without Homebrew uses the apt package `bash`. The CLI
probe checks the Bash actually resolved through PATH using `BASH_VERSINFO`
and prints `BASH_VERSION`. Bash 3.2 does not qualify. Homebrew link drift is
reported separately; Homebrew's bin directory must precede system paths.
The login shell, `/bin/bash`, and `/etc/shells` remain unchanged. Windows keeps
PowerShell 7. The minimum version is 5.0 with no upper major-version limit.*

Documentation Impact: `UpdateRequired` — Owner: home-baseline maintainers;
kanonische Quelle / canonical source: Pflichtregister / required registries.
Zielgruppe / audience: Workspace-Betreiber / workspace operators. Leserpfad /
reader path: Wartung / maintenance → diese Manpage / this manpage. DE/EN sind
hier gemeinsam gepflegt / maintained together here. Register sind homeRuntime,
Home-Sync und Wartungspaket-Propagation erforderlich / registries require home
sync and maintenance propagation. Re-Evaluation bei Aenderung der Mindestversion
oder Paketquelle / reevaluate when the minimum version or package source changes.

Das Werkzeug liest
`scripts/config/brew-apps-registry.json` und gleicht die lokale macOS-/Linux-
Toolchain fuer agentische Entwicklung ab. Zusaetzlich liest es
`scripts/config/vscode-extensions-registry.json` und pflegt die Required-VS-
Code-Extensions fuer die sechs MSL-Pfade C#, Go, Java, Python, Rust und Swift,
die offizielle PowerShell-Erweiterung `ms-vscode.powershell` fuer das Schreiben
und Debuggen von PowerShell-Skripten und Cmdlets sowie Microsoft Container Tools
fuer Podman-Workflows. Danach liest es
`scripts/config/required-cli-tools-registry.json` und prueft die Required-CLI-
Tools fuer die sechs MSL-Pfade, `syft`, GitHub Spec Kit (`specify`) und die
Agenten-CLIs `codex`, `claude`, `agy` und `copilot`. npm-basierte
Fallbacks fuer Codex, Claude und Copilot werden ueber
`scripts/config/npm-agent-cli-registry.json` installiert, wenn ihre CLI fehlt.
Erforderliche PowerShell-Module werden aus
`scripts/config/powershell-modules-registry.json` durch den gemeinsamen
PowerShell-Modulpfleger installiert und geprueft.
Auf Systemen mit Homebrew fuehrt ein normaler Lauf `brew update`,
`brew upgrade` und die Installation fehlender Required-Formulae aus. Auf macOS
werden zusaetzlich Required-Casks gepflegt. Formulae mit `ensureLinked: true`
werden nach dem Upgrade idempotent verlinkt; `--compare-only` meldet einen
abweichend aufgeloesten Befehl als `LINK-DRIFT`, ohne ihn zu veraendern.
Fehlt `specify`, akzeptiert die Wartung ausschließlich den in der Code-Policy
freigegebenen vollständigen Spec-Kit-Commit und den daraus konstruierten
`uv`-Aufruf. Fehlt `agy` auf Linux, wird der offizielle Installer nur ausgeführt,
wenn URL, Interpreter und SHA-256 sowohl zur Registry als auch zur unabhängigen
Code-Policy passen. Eine alternative CLI-Registry kann diese Freigaben nicht
ersetzen.
Registry-Schleifen verwenden geordnete Snapshots und eigene Dateideskriptoren.
Ein Unterprozess, der stdin bis EOF liest, kann deshalb keine spaeteren
Eintraege verschlucken. CLI-Funktionsproben laufen in einer eigenen
Prozessgruppe, werden nach fuenf Sekunden beendet und unterscheiden
`Missing`, `Unusable`, `TimedOut` und `CapabilityBlocked`. Der strukturierte
Ergebnisbericht ordnet jedem Eintrag genau einen Endstatus zu. Verbleibender
Required-Drift beendet den Lauf mit Exitcode `1`; rein optionaler Drift bleibt
sichtbar und nicht fatal.
Auch bei einem fruehen Producer-Fehler wird eine atomare, schema-gueltige
Fehlerevidence geschrieben. Consumer unterscheiden stabil zwischen fehlenden,
leeren, unvollstaendigen, syntaktisch fehlerhaften, nicht als UTF-8 lesbaren
und schemafremden Ergebnissen. Sie geben dabei weder Stacktraces noch private
Dateipfade aus.

Auf Ubuntu 22.04/24.04 (`x86_64`/`aarch64`) kann Swiftly ueber den gepinnten
offiziellen Bootstrap-Vertrag `1.1.2` bereitgestellt werden. Die Swift-Version
folgt dem gemeinsamen stabilen Release-Snapshot. URL und SHA-256 werden vor dem sicheren Extrahieren
validiert. Die
Archivextraktion begrenzt unabhängig vom Download-Hash die Mitgliederzahl, die
gesamte deklarierte Entpackgröße sowie die deklarierte und tatsächlich gelesene
Größe des `swiftly`-Programms. Bei Überschreitung bleibt das bisherige Ziel
unverändert und die temporäre Datei wird entfernt. Swiftly veraendert mit `--no-modify-profile` kein Shell-Profil; seine
erzeugte Umgebung wird nur im aktuellen Prozess aktiviert. Eventuelle
Systemnacharbeit laeuft ausschließlich mit aktueller
`--allow-admin-prompts`-Freigabe.
Ein bereits vorhandenes App-Bundle fuer Visual Studio Code unter
`/Applications/Visual Studio Code.app` oder `~/Applications/Visual Studio Code.app`
gilt als erfuellter Required-Cask, auch wenn VS Code nicht von Homebrew
installiert wurde.

*The tool reads `scripts/config/brew-apps-registry.json` and reconciles the
local macOS/Linux toolchain for agentic development. It additionally reads
`scripts/config/vscode-extensions-registry.json` and maintains the required VS
Code extensions for the six MSL paths C#, Go, Java, Python, Rust, and Swift,
the official PowerShell extension `ms-vscode.powershell` for authoring and
debugging PowerShell scripts and cmdlets, plus Microsoft Container Tools for
Podman workflows. It then reads
`scripts/config/required-cli-tools-registry.json` and checks the required CLI
tools for the six MSL paths, `syft`, GitHub Spec Kit (`specify`), and the
agent CLIs `codex`, `claude`, `agy`, and `copilot`. npm-based fallbacks for Codex, Claude, and Copilot are installed from
`scripts/config/npm-agent-cli-registry.json` when their CLI is missing.
Required PowerShell modules are installed and checked from
`scripts/config/powershell-modules-registry.json` by the shared PowerShell
module maintainer. On systems with Homebrew, a normal run executes `brew update`, `brew upgrade`, and
installs missing required formulae. On macOS it also maintains required casks.
Formulae marked with `ensureLinked: true` are linked idempotently after the
upgrade; `--compare-only` reports command-resolution drift without changing it.
When `specify` is missing, maintenance accepts only the full Spec Kit commit
approved in code policy and constructs that exact `uv` invocation. When `agy`
is missing on Linux, URL, interpreter, and SHA-256 must match both the registry
and the independent code policy. An alternate CLI registry cannot replace
these approvals.
Registry loops use ordered snapshots and dedicated file descriptors, so a child
that reads stdin to EOF cannot consume later entries. CLI probes run in their
own process group, stop after five seconds, and distinguish `Missing`,
`Unusable`, `TimedOut`, and `CapabilityBlocked`. Every selected item receives
one structured final status. Remaining required drift exits with `1`; optional
drift remains visible and non-fatal.
An early producer failure still publishes atomic, schema-valid failure
evidence. Consumers distinguish missing, empty, truncated, malformed,
non-UTF-8, and schema-mismatched results without exposing stack traces or
private file paths.

On Ubuntu 22.04/24.04 (`x86_64`/`aarch64`), Swiftly can be provisioned through
the pinned official `1.1.2` bootstrap contract. The Swift language version
follows the shared stable release snapshot. URL and SHA-256 are validated before safe extraction. Extraction independently caps member count, total declared expanded bytes, and both declared and copied `swiftly` bytes; limit failures preserve the prior target and remove temporary output. Swiftly runs
with `--no-modify-profile`; its generated environment is activated only in the
current process. System post-install work requires current
`--allow-admin-prompts` authority.
An existing Visual Studio Code app bundle under `/Applications/Visual Studio Code.app`
or `~/Applications/Visual Studio Code.app` satisfies the required cask even when
VS Code was not installed by Homebrew.*

Wenn Linux kein `brew`, aber `apt` bereitstellt, nutzt das Skript den explizit
dokumentierten apt-Fallback aus der Registry: `sudo apt update`,
`sudo apt upgrade` und nur die dort gemappten Pakete. `code` und `helix` werden
im apt-Fallback nur installiert, wenn sie in den bereits konfigurierten apt-
Quellen verfuegbar sind. Die Homebrew-Registry enthaelt Top-Level-Pakete
(`brew leaves --installed-on-request`) und macOS-Casks, keine transitiven
Abhaengigkeiten. `xquartz` ist bewusst ausgeschlossen.

*When Linux has no `brew` but provides `apt`, the script uses the explicitly
documented apt fallback from the registry: `sudo apt update`,
`sudo apt upgrade`, and only the mapped packages. In the apt fallback, `code`
and `helix` are installed only when available from the already configured apt
sources. The Homebrew registry contains top-level packages
(`brew leaves --installed-on-request`) and macOS casks, not transitive
dependencies. `xquartz` is intentionally excluded.*

## Dokumentwerkzeuge (Document tools)

Pandoc und Typst CLI sind Pflichtwerkzeuge. Nach der Paketpflege werden
`pandoc --version` und `typst --version` geprueft. Homebrew installiert
`pandoc` und `typst`; Linux ohne Homebrew verwendet apt fuer Pandoc und bei
fehlendem Typst den [offiziellen Cargo-Build](https://github.com/typst/typst#installation)
mit `cargo install --locked typst-cli`. Dafuer muss eine kompatible
Rust-Toolchain vorhanden sein (die Releases Typst 0.15.1 und Tinymist 0.15.8
benoetigen Rust 1.92). Der Cargo-Installationspfad wird nur im
Wartungsprozess an PATH angehaengt; Shell-Profile werden nicht geaendert.
Ein Buildfehler oder eine weiterhin fehlende CLI bleibt ein Pflichtbefund.
Vorhandene Cargo-Installationen werden nicht automatisch neu gebaut.

Die Community-Erweiterung Tinymist Typst (`myriad-dreamin.tinymist`) ist in
VS Code Pflicht und verwaltet ihren Language Server selbst. Das separate
Systempaket `tinymist` bleibt optional, etwa fuer andere Editoren. Es wird
nur mit `--include-optional` nachinstalliert: ueber Homebrew oder auf Linux
ohne Homebrew mit `cargo install --locked tinymist-cli`. Eine fehlende optionale
CLI blockiert den Pflichtstatus nicht. Vorschau und Vergleich installieren
keine Dokumentwerkzeuge.

*Pandoc and Typst CLI are required and checked with `--version` after package
maintenance. Homebrew provides both; Linux without Homebrew uses apt for
Pandoc and the official `cargo install --locked typst-cli` build when Typst
is missing. A compatible Rust toolchain is required (Typst 0.15.1 and Tinymist
0.15.8 require Rust 1.92). The Cargo installation
path is appended only within the maintenance process; shell profiles stay
unchanged. Build failures or a still-missing CLI remain required findings.
Existing Cargo installations are not automatically rebuilt. The community
Tinymist Typst extension (`myriad-dreamin.tinymist`) is required in VS Code
and manages its own language server. Standalone `tinymist` remains optional
and is installed only with `--include-optional`, through Homebrew or a locked
Cargo build on Linux without Homebrew. Its absence does not block required
status. Preview and comparison do not install document tools.*

PDF-Smoke-Test in einem temporaeren Verzeichnis / PDF smoke test in a temporary
directory:

```bash
pandoc input.md --standalone --to typst --output input.typ
typst compile input.typ output.pdf
```

Die erzeugte PDF-Datei belegt die Konvertierung und Kompilierung. Die
Barrierefreiheit der Ausgabe benoetigt eine eigene inhaltliche Pruefung.

*The generated PDF proves conversion and compilation. Output accessibility
requires a separate semantic review.*

## Optionen / Options

| Option | Bedeutung / Meaning |
|---|---|
| `--dry-run` | Paketmanager-Aktionen anzeigen, nicht ausfuehren |
| `--compare-only` | Registry-Drift nach Required/Optional getrennt melden, nichts installieren oder upgraden |
| `--msl-only` | Nur sechs stabile MSL-Toolchains samt Abhaengigkeiten pflegen / only six stable MSL toolchains and dependencies |
| `--registry PATH` | Alternative Registry-Datei verwenden |
| `--cli-registry PATH` | Alternative Required-CLI-Registry verwenden |
| `--vscode-registry PATH` | Alternative VS-Code-Extension-Registry verwenden |
| `--npm-agent-registry PATH` | Alternative npm-Agent-CLI-Registry verwenden |
| `--powershell-module-registry PATH` | Alternative PowerShell-Modul-Registry verwenden |
| `--skip-upgrade` | `brew update`/`brew upgrade` bzw. apt-Update/Upgrade ueberspringen |
| `--skip-vscode-extensions` | VS-Code-Extensions weder installieren noch vergleichen |
| `--include-optional` | Auch optionale Registry-Eintraege installieren |
| `--allow-admin-prompts` | Sichtbare Administratorabfragen nur fuer diesen Lauf erlauben; keine technische Umgehung |
| `--result-file PATH` | Atomaren JSON-Ergebnisbericht schreiben |
| `-h`, `--help` | Hilfe anzeigen |

## Beispiele / Examples

```bash
bash scripts/maintain-agentic-brew-apps.sh --dry-run
bash scripts/maintain-agentic-brew-apps.sh --compare-only
bash scripts/maintain-agentic-brew-apps.sh --allow-admin-prompts
```

## Exitstatus / Exit Status

| Code | Bedeutung / Meaning |
|---|---|
| `0` | Required-Sollzustand erreicht; optionaler Drift darf sichtbar bleiben / Required state reached |
| `1` | Required-Drift bleibt oder eine autorisierte Installation schlug fehl / Required drift remains |
| `2` | Parameter-, Registry- oder Betriebsvertrag ist ungueltig / Invalid input or operational contract |

## Abschlusskriterien / Closeout Criteria

- `pandoc --version` und `typst --version` funktionieren; `myriad-dreamin.tinymist` ist installiert. / Both document CLIs work and the required Typst extension is installed.

- `gitleaks version` funktioniert.
- `syft version` und `specify --version` funktionieren.
- `codex --version`, `claude --version`, `agy --version` und
  `copilot --help` funktionieren.
- `.NET`, Go, Java/Javac, Python, Rust/Cargo und Swift sind per CLI pruefbar,
  soweit die Plattform den jeweiligen Pfad unterstuetzt.
- `code --version` und `hx --version` funktionieren, sofern die Plattform die grafische bzw. TUI-Editor-Basis installieren konnte.
- `--compare-only` meldet `missing_on_machine.required.*: none`.
- `--compare-only` meldet fuer erforderliche verlinkte Formulae keinen `LINK-DRIFT`.
- `python3 -m json.tool scripts/config/brew-apps-registry.json` ist erfolgreich.
- `python3 -m json.tool scripts/config/vscode-extensions-registry.json` ist erfolgreich.
- `python3 -m json.tool scripts/config/required-cli-tools-registry.json` ist erfolgreich.
- `python3 -m json.tool scripts/config/npm-agent-cli-registry.json` ist erfolgreich.
- `python3 -m json.tool scripts/config/powershell-modules-registry.json` ist erfolgreich.
- PSScriptAnalyzer `1.25.0` ist vorhanden und der repositoryweite Analyselauf ist gruen.
- Neue bewusst installierte Top-Level-Tools werden in der Registry nachgetragen.

*`gitleaks version`, `syft version`, `specify --version`, `codex --version`,
`claude --version`, `agy --version`, and `copilot --help` work; .NET,
Go, Java/Javac, Python, Rust/Cargo, and Swift are CLI-checkable where the
platform supports the path; `code --version` and `hx --version` work where the
platform could install the graphical/TUI editor baseline; `--compare-only`
reports `missing_on_machine.required.*: none`; the registries are valid JSON;
and intentional new top-level tools are added to the registry.*

## Sicherheit / Security

Die Registry enthaelt keine Secrets oder lokalen Tokens. Berichte begrenzen und
bereinigen Prozessausgaben und private Pfade. Paketmanager-Laeufe schreiben
ausserhalb des Repositories und sollen vorab mit `--dry-run` geprueft werden.
`--allow-admin-prompts` ist nur aktuelle Autoritaet fuer sichtbare Prompts; es
umgeht weder sudo/UAC noch Integritaets-, Test- oder Sicherheitsgates.

*The registry contains no secrets or local tokens. Reports bound and sanitize
process output and private paths. Package-manager runs write outside the
repository and should be previewed with `--dry-run` first.
`--allow-admin-prompts` grants only current authority for visible prompts; it
does not bypass sudo/UAC, integrity, test, or security gates.*
