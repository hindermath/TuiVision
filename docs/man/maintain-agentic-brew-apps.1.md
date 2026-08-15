# maintain-agentic-brew-apps(1)

## Name

`maintain-agentic-brew-apps` - pflegt die Homebrew-/apt-Toolchain fuer agentische Entwicklung

*maintains the Homebrew/apt toolchain for agentic development*

## Synopsis

```bash
bash scripts/maintain-agentic-brew-apps.sh [--dry-run] [--compare-only] [--allow-admin-prompts] [--result-file PATH] [--skip-upgrade] [--skip-vscode-extensions] [--include-optional] [--cli-registry PATH] [--npm-agent-registry PATH] [--powershell-module-registry PATH]
```

## Beschreibung / Description

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
Fehlt `agy` auf Linux, wird der offizielle Installer nur nach erfolgreicher
SHA-256-Pruefung gegen die Required-CLI-Registry ausgefuehrt.
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

Auf Ubuntu 22.04/24.04 (`x86_64`/`aarch64`) kann fehlendes oder abweichendes
Swift ueber den gepinnten offiziellen Vertrag Swiftly `1.1.2` / Swift `6.3.3`
installiert werden. URL und SHA-256 werden vor dem sicheren Extrahieren
validiert. Swiftly veraendert mit `--no-modify-profile` kein Shell-Profil; seine
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
When `agy` is missing on Linux, the official installer runs only after its
SHA-256 digest matches the required CLI registry.
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

On Ubuntu 22.04/24.04 (`x86_64`/`aarch64`), missing or mismatched Swift can be
installed through the pinned official Swiftly `1.1.2` / Swift `6.3.3`
contract. URL and SHA-256 are validated before safe extraction. Swiftly runs
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

## Optionen / Options

| Option | Bedeutung / Meaning |
|---|---|
| `--dry-run` | Paketmanager-Aktionen anzeigen, nicht ausfuehren |
| `--compare-only` | Registry-Drift nach Required/Optional getrennt melden, nichts installieren oder upgraden |
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
