# maintain-agentic-winget-apps(1)

## Name

`maintain-agentic-winget-apps` - pflegt die WinGet-Toolchain fuer agentische Entwicklung

*maintains the WinGet toolchain for agentic development*

## Synopsis

```powershell
pwsh -NoProfile -File scripts/maintain-agentic-winget-apps.ps1 [-WhatIf] [-CompareOnly] [-SkipUpgrade] [-SkipVSCodeExtensions] [-IncludeOptional] [-NpmAgentRegistry PATH] [-PowerShellModuleRegistry PATH]
```

## Beschreibung / Description

`-MslOnly` begrenzt die Wartung auf sechs stabile MSL-Toolchains samt
Abhaengigkeiten. `-CompareOnly` und `-WhatIf` bleiben ohne Installation oder
Benutzer-PATH-Aenderung. `-ResultFile` schreibt optional atomare JSON-Evidence
mit sechs `mslToolchains`-Eintraegen. Versionsgebundene WinGet-ID-Vorlagen
werden aus stabilen Releases aufgeloest. Admin-Gates bleiben verbindlich.

Vergleich und Vorschau führen nur die im Code fest freigegebenen
Versionsproben aus. Registry-Daten können weder Programm noch Argumente einer
primären oder begleitenden MSL-Probe autorisieren.

*Compare and preview execute only version probes fixed in code policy.
Registry data cannot authorize an executable or arguments for primary or
companion MSL probes.*
[Quellen und Nachweisgrenzen](https://github.com/hindermath/home-baseline/blob/main/docs/maintenance/msl-toolchains.md).

*Use `-MslOnly` for the six stable MSL toolchains and dependencies. Comparison
and WhatIf do not change installations or the user PATH. Optional `-ResultFile`
writes atomic JSON evidence including six MSL results. Versioned WinGet ID
templates are resolved from stable releases. Administrator gates still apply.*

Das Werkzeug liest
`scripts/config/winget-apps-registry.json` und gleicht die Windows-Toolchain fuer
agentische Entwicklung ab. Zusaetzlich liest es
`scripts/config/vscode-extensions-registry.json` und pflegt die Required-VS-
Code-Extensions fuer die sechs MSL-Pfade C#, Go, Java, Python, Rust und Swift,
die offizielle PowerShell-Erweiterung `ms-vscode.powershell` fuer das Schreiben
und Debuggen von PowerShell-Skripten und Cmdlets sowie Microsoft Container Tools
fuer Podman-Workflows. Danach liest es
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
Eine fehlende `specify`-CLI wird ausschließlich aus dem in der Code-Policy
freigegebenen vollständigen Spec-Kit-Commit installiert. Abweichende Registry-
Argumente werden vor einem `uv`-Aufruf abgelehnt.
Jeder WinGet-Unterprozess besitzt ebenfalls eine harte Zeitgrenze. Leseproben
verwenden hoechstens 30 Sekunden. Bei einem Timeout wird der vollstaendige
Prozessbaum beendet und begrenzt abgewartet. Eine feste Ausgabegrenze meldet
einen Überlauf als Fehler; abgeschnittene Ausgabe wird nie als vollständiger
Paketstatus ausgewertet. Upgrade- und Installationsschritte
ohne aktuelle Admin-Prompt-Autoritaet werden nicht gestartet und als
`DEFERRED_ADMIN_REQUIRED` gemeldet; UAC wird nicht umgangen.

*The tool reads `scripts/config/winget-apps-registry.json` and reconciles the
Windows toolchain for agentic development. It additionally reads
`scripts/config/vscode-extensions-registry.json` and maintains the required VS
Code extensions for the six MSL paths C#, Go, Java, Python, Rust, and Swift,
the official PowerShell extension `ms-vscode.powershell` for authoring and
debugging PowerShell scripts and cmdlets, plus Microsoft Container Tools for
Podman workflows. It then reads
`scripts/config/required-cli-tools-registry.json` and checks the required CLI
tools for the six MSL paths, `syft`, GitHub Spec Kit (`specify`), and the
agent CLIs `codex`, `claude`, and `copilot` plus required package `Google.AntigravityCLI`. npm-based fallbacks for Codex, Claude, and Copilot are installed from
`scripts/config/npm-agent-cli-registry.json` when their CLI is missing.
Required PowerShell modules are installed and checked from
`scripts/config/powershell-modules-registry.json` by the shared PowerShell
module maintainer. A normal run refreshes WinGet sources, runs `winget upgrade --all`, and installs
missing required packages via `winget install --id <Id> --exact`. CLI version
probes stop after five seconds so interactive or stuck agent CLIs cannot block
maintenance. A missing `specify` CLI is installed only from the full Spec Kit
commit approved in code policy; differing registry arguments are rejected
before invoking `uv`. Every WinGet subprocess also has a hard timeout, with read probes
capped at 30 seconds. On timeout, the complete process tree is terminated and
awaited for a bounded interval. A fixed output limit also fails closed, so
truncated output is never treated as a complete package status. Upgrade and
install steps without current
administrator-prompt authority are not started and are reported as
`DEFERRED_ADMIN_REQUIRED`; UAC is never bypassed.*

Das Skript versucht zuerst `winget update`. Wenn diese Unterfunktion auf der
installierten WinGet-Version nicht verfuegbar ist, nutzt es
`winget source update` als kompatiblen Fallback. Paket-IDs werden vor der
Installation mit `winget search --id <Id> --exact` geprueft.

*The script tries `winget update` first. If that subcommand is not available on
the installed WinGet version, it uses `winget source update` as a compatible
fallback. Package IDs are checked with `winget search --id <Id> --exact` before
installation.*

## Dokumentwerkzeuge (Document tools)

Pandoc (`JohnMacFarlane.Pandoc`) und Typst CLI (`Typst.Typst`) sind
Pflichtpakete. Nach der Paketpflege werden `pandoc --version` und
`typst --version` geprueft. Die Community-Erweiterung Tinymist Typst
(`myriad-dreamin.tinymist`) ist in VS Code Pflicht und verwaltet ihren
Language Server selbst. Das separate Paket `Myriad-Dreamin.Tinymist` bleibt
optional und wird nur mit `-IncludeOptional` nachinstalliert. Eine fehlende
optionale `tinymist`-CLI blockiert den Pflichtstatus nicht.
`-WhatIf` und `-CompareOnly` installieren keine Dokumentwerkzeuge.

*Pandoc (`JohnMacFarlane.Pandoc`) and Typst CLI (`Typst.Typst`) are required
packages, checked with `pandoc --version` and `typst --version` after package
maintenance. The community-maintained Tinymist Typst extension
(`myriad-dreamin.tinymist`) is required in VS Code and manages its own
language server. Standalone `Myriad-Dreamin.Tinymist` remains optional and
is installed only with `-IncludeOptional`. A missing optional `tinymist`
CLI does not block required status. `-WhatIf` and `-CompareOnly` do not
install document tools.*

PDF-Smoke-Test in einem temporaeren Verzeichnis / PDF smoke test in a temporary
directory:

```powershell
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
| `-WhatIf` | Paketmanager-Aktionen anzeigen, nicht ausfuehren |
| `-CompareOnly` | Nur Registry-Drift melden, nichts installieren oder upgraden |
| `-Registry PATH` | Alternative Registry-Datei verwenden |
| `-VSCodeRegistry PATH` | Alternative VS-Code-Extension-Registry verwenden |
| `-NpmAgentRegistry PATH` | Alternative npm-Agent-CLI-Registry verwenden |
| `-PowerShellModuleRegistry PATH` | Alternative PowerShell-Modul-Registry verwenden |
| `-SkipUpgrade` | WinGet-Update und `winget upgrade --all` ueberspringen |
| `-SkipVSCodeExtensions` | VS-Code-Extensions weder installieren noch vergleichen |
| `-IncludeOptional` | Auch optionale Registry-Eintraege installieren |
| `-ProcessTimeoutSeconds N` | Harte Zeitgrenze je WinGet-Unterprozess; Standard 1800 Sekunden / Hard timeout per WinGet subprocess; default 1800 seconds |
| `-ProcessCleanupSeconds N` | Frist zum Beenden und Abwarten des Prozessbaums; Standard 10 Sekunden / Process-tree cleanup interval; default 10 seconds |

## Beispiele / Examples

```powershell
pwsh -NoProfile -File scripts/maintain-agentic-winget-apps.ps1 -WhatIf
pwsh -NoProfile -File scripts/maintain-agentic-winget-apps.ps1 -CompareOnly
pwsh -NoProfile -File scripts/maintain-agentic-winget-apps.ps1 -WhatIf -ProcessTimeoutSeconds 1800 -ProcessCleanupSeconds 10
pwsh -NoProfile -File scripts/maintain-agentic-winget-apps.ps1
```

## Exitstatus / Exit Status

| Code | Bedeutung / Meaning |
|---:|---|
| `0` | Aktuell oder erfolgreich / Current or successful |
| `1` | Fehlende oder unbrauchbare Pflichtpakete, CLIs oder Extensions, auch in der Vorschau / Missing or unusable required packages, CLIs, or extensions, including preview |
| `2` | Betriebsfehler oder widerspruechlicher Paketstatus / Operational error or contradictory package status |
| `75` | `DEFERRED_ADMIN_REQUIRED`; sicher erneut mit aktueller Autoritaet ausfuehren / safely rerun with current authority |

## Abschlusskriterien / Closeout Criteria

- `gitleaks version` funktioniert.
- `pandoc --version` und `typst --version` funktionieren; `myriad-dreamin.tinymist` ist installiert. / Both document CLIs work and the required Typst extension is installed.
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
sollen vorab mit `-WhatIf` geprueft werden. Administratorautoritaet gilt nur
fuer den aktuellen Elternprozess und ersetzt kein UAC-, Timeout-, Sicherheits-
oder Test-Gate.

*The registry contains only package IDs and purposes, not secrets, local tokens,
or private paths. WinGet runs write outside the repository and should be
previewed with `-WhatIf` first. Administrator authority is scoped to the
current parent process and does not replace UAC, timeout, security, or test
gates.*
