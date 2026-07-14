# maintain-powershell-modules(1)

## Name

`maintain-powershell-modules` - pflegt erforderliche PowerShell-Module

*maintains required PowerShell modules*

## Synopsis

```powershell
pwsh -NoProfile -File scripts/maintain-powershell-modules.ps1 [-Registry PATH] [-CompareOnly] [-IncludeOptional] [-WhatIf]
```

## Beschreibung / Description

Das Skript liest `scripts/config/powershell-modules-registry.json`, prueft die
fuer das aktuelle Betriebssystem festgelegten exakten Modulversionen und
installiert fehlende Required-Module im `CurrentUser`-Bereich. Es bevorzugt
`Install-PSResource` und verwendet `Install-Module` nur als
Kompatibilitaets-Fallback. Die Registry legt PSScriptAnalyzer `1.25.0` fuer
macOS, Linux und Windows fest.

*The script reads `scripts/config/powershell-modules-registry.json`, checks the
exact module versions selected for the current operating system, and installs
missing required modules in `CurrentUser` scope. It prefers
`Install-PSResource` and uses `Install-Module` only as a compatibility fallback.
The registry pins PSScriptAnalyzer `1.25.0` for macOS, Linux, and Windows.*

## Optionen / Options

| Option | Bedeutung / Meaning |
|---|---|
| `-Registry PATH` | Alternative Modul-Registry verwenden / Use an alternative module registry |
| `-CompareOnly` | Nur vergleichen, nichts installieren / Compare only, install nothing |
| `-IncludeOptional` | Auch optionale Module installieren / Also install optional modules |
| `-WhatIf` | Installationsschritte nur anzeigen / Preview installation steps |

## Beispiele / Examples

```powershell
pwsh -NoProfile -File scripts/maintain-powershell-modules.ps1 -CompareOnly
pwsh -NoProfile -File scripts/maintain-powershell-modules.ps1 -WhatIf
pwsh -NoProfile -File scripts/maintain-powershell-modules.ps1
```

## Abschlusskriterien / Closeout Criteria

- `missing_on_machine.required.powershell_modules: none` wird ausgegeben.
- `Get-Module -ListAvailable PSScriptAnalyzer` enthaelt Version `1.25.0`.
- Ein zweiter Lauf ist idempotent und meldet das Modul als `OK`.

*The required-module comparison reports none missing, PSScriptAnalyzer 1.25.0
is available, and a second run is idempotent.*
