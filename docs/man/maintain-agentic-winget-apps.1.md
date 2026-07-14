# maintain-agentic-winget-apps(1)

## Name

`maintain-agentic-winget-apps` - pflegt die WinGet-Toolchain fuer agentische Entwicklung

*maintains the WinGet toolchain for agentic development*

## Synopsis

```powershell
pwsh -NoProfile -File scripts/maintain-agentic-winget-apps.ps1 [-WhatIf] [-CompareOnly] [-SkipUpgrade] [-SkipVSCodeExtensions] [-IncludeOptional] [-NpmAgentRegistry PATH] [-PowerShellModuleRegistry PATH]
```

## Beschreibung / Description

Das Werkzeug liest
`scripts/config/winget-apps-registry.json` und gleicht die Windows-Toolchain fuer
agentische Entwicklung ab. Zusaetzlich liest es
`scripts/config/vscode-extensions-registry.json` und pflegt die Required-VS-
Code-Extensions fuer die sechs MSL-Pfade C#, Go, Java, Python, Rust und Swift
sowie Microsoft Container Tools fuer Podman-Workflows. Danach liest es
`scripts/config/required-cli-tools-registry.json` und prueft die Required-CLI-
Tools fuer die sechs MSL-Pfade, `syft`, GitHub Spec Kit (`specify`) und die
Agenten-CLIs `codex`, `claude` und `copilot` sowie das Required-Paket
`Google.AntigravityCLI`. npm-basierte Fallbacks fuer Codex, Claude und Copilot werden ueber
`scripts/config/npm-agent-cli-registry.json` installiert, wenn ihre CLI fehlt.
Erforderliche PowerShell-Module werden aus
`scripts/config/powershell-modules-registry.json` durch den gemeinsamen
PowerShell-Modulpfleger installiert und geprueft.
Ein normaler Lauf aktualisiert WinGet-Quellen, fuehrt `winget upgrade --all`
aus und installiert fehlende Required-Pakete per `winget install --id <Id>
--exact`.
CLI-Versionsproben werden nach fuenf Sekunden beendet, damit interaktive oder
festhaengende Agenten-CLIs den Wartungslauf nicht blockieren.

*The tool reads `scripts/config/winget-apps-registry.json` and reconciles the
Windows toolchain for agentic development. It additionally reads
`scripts/config/vscode-extensions-registry.json` and maintains the required VS
Code extensions for the six MSL paths C#, Go, Java, Python, Rust, and Swift
plus Microsoft Container Tools for Podman workflows. It then reads
`scripts/config/required-cli-tools-registry.json` and checks the required CLI
tools for the six MSL paths, `syft`, GitHub Spec Kit (`specify`), and the
agent CLIs `codex`, `claude`, and `copilot` plus required package `Google.AntigravityCLI`. npm-based fallbacks for Codex, Claude, and Copilot are installed from
`scripts/config/npm-agent-cli-registry.json` when their CLI is missing.
Required PowerShell modules are installed and checked from
`scripts/config/powershell-modules-registry.json` by the shared PowerShell
module maintainer. A normal run refreshes WinGet sources, runs `winget upgrade --all`, and installs
missing required packages via `winget install --id <Id> --exact`. CLI version
probes stop after five seconds so interactive or stuck agent CLIs cannot block
maintenance.*

Das Skript versucht zuerst `winget update`. Wenn diese Unterfunktion auf der
installierten WinGet-Version nicht verfuegbar ist, nutzt es
`winget source update` als kompatiblen Fallback. Paket-IDs werden vor der
Installation mit `winget search --id <Id> --exact` geprueft.

*The script tries `winget update` first. If that subcommand is not available on
the installed WinGet version, it uses `winget source update` as a compatible
fallback. Package IDs are checked with `winget search --id <Id> --exact` before
installation.*

## Optionen / Options

| Option | Bedeutung / Meaning |
|---|---|
| `-WhatIf` | Paketmanager-Aktionen anzeigen, nicht ausfuehren |
| `-CompareOnly` | Nur Registry-Drift melden, nichts installieren oder upgraden |
| `-Registry PATH` | Alternative Registry-Datei verwenden |
| `-VSCodeRegistry PATH` | Alternative VS-Code-Extension-Registry verwenden |
| `-NpmAgentRegistry PATH` | Alternative npm-Agent-CLI-Registry verwenden |
| `-PowerShellModuleRegistry PATH` | Alternative PowerShell-Modul-Registry verwenden |
| `-SkipUpgrade` | WinGet-Update und `winget upgrade --all` ueberspringen |
| `-SkipVSCodeExtensions` | VS-Code-Extensions weder installieren noch vergleichen |
| `-IncludeOptional` | Auch optionale Registry-Eintraege installieren |

## Beispiele / Examples

```powershell
pwsh -NoProfile -File scripts/maintain-agentic-winget-apps.ps1 -WhatIf
pwsh -NoProfile -File scripts/maintain-agentic-winget-apps.ps1 -CompareOnly
pwsh -NoProfile -File scripts/maintain-agentic-winget-apps.ps1
```

## Abschlusskriterien / Closeout Criteria

- `gitleaks version` funktioniert.
- `syft version` und `specify --version` funktionieren.
- `codex --version`, `claude --version`, `agy --version` und
  `copilot --help` funktionieren.
- `.NET`, Go, Java/Javac, Python, Rust/Cargo und Swift sind per CLI pruefbar.
- `code --version` und `hx --version` funktionieren.
- `-CompareOnly` meldet keine fehlenden Required-Tools.
- `python3 -m json.tool scripts/config/winget-apps-registry.json` ist erfolgreich.
- `python3 -m json.tool scripts/config/vscode-extensions-registry.json` ist erfolgreich.
- `python3 -m json.tool scripts/config/required-cli-tools-registry.json` ist erfolgreich.
- `python3 -m json.tool scripts/config/npm-agent-cli-registry.json` ist erfolgreich.
- `python3 -m json.tool scripts/config/powershell-modules-registry.json` ist erfolgreich.
- PSScriptAnalyzer `1.25.0` ist vorhanden und der repositoryweite Analyselauf ist gruen.
- Neue bewusst installierte WinGet-Top-Level-Tools werden in der Registry
  nachgetragen.

*`gitleaks version`, `syft version`, `specify --version`, `codex --version`,
`claude --version`, `agy --version`, `copilot --help`, .NET, Go,
Java/Javac, Python, Rust/Cargo, Swift, `code --version`, and `hx --version`
work; `-CompareOnly` reports no missing required tools; the registries are valid
JSON; and intentional new WinGet top-level tools are added to the registry.*

## Sicherheit / Security

Die Registry enthaelt nur Paket-IDs und Zwecke, keine Secrets, lokalen Tokens
oder privaten Pfade. WinGet-Laeufe schreiben ausserhalb des Repositories und
sollen vorab mit `-WhatIf` geprueft werden.

*The registry contains only package IDs and purposes, not secrets, local tokens,
or private paths. WinGet runs write outside the repository and should be
previewed with `-WhatIf` first.*
