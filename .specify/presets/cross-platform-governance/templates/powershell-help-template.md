# PowerShell Comment-Based Help Template

> Place this block at the top of `<name>.ps1` or directly above the
> Advanced Function. Bilingual: German first, English second.

```powershell
<#
.SYNOPSIS
    Kurzbeschreibung auf Deutsch.

    Short English description.

.DESCRIPTION
    Ausführliche deutsche Beschreibung. Erklärt, was das Skript tut,
    wann es verwendet wird und welche Seiteneffekte es hat. Domänen-
    spezifische Begriffe werden bei der ersten Erwähnung definiert.

    Detailed English description. Explains what the script does, when
    to use it, and what side effects it has. Domain-specific terms are
    defined on first use.

.PARAMETER WorkspaceName
    Deutscher Hilfetext für den Parameter.

    English help text for the parameter.

.PARAMETER WhatIf
    Zeigt an, was passieren würde, ohne Änderungen vorzunehmen
    (Spiegelbild von Bash `--dry-run`).

    Shows what would happen without making changes (mirror of Bash
    `--dry-run`).

.EXAMPLE
    PS> New-HBWorkspace -WorkspaceName Beispiel -WhatIf

    Vorschau einer Workspace-Anlage ohne Schreibzugriff.

    Preview a workspace creation without writing anything.

.EXAMPLE
    PS> New-HBWorkspace -WorkspaceName Beispiel

    Legt den Workspace mit Standardwerten an.

    Creates the workspace with default values.

.NOTES
    Autor / Author : <Name>
    Mindestversion / Minimum version : PowerShell 7+
    Verwandt / Related : Bash-Variante `<name>.sh`,
    Manpage `docs/man/<name>.1`.

.LINK
    https://example.invalid/docs/<name>
#>

[CmdletBinding(SupportsShouldProcess = $true)]
param(
    [Parameter(Mandatory = $true)]
    [ValidateNotNullOrEmpty()]
    [string]$WorkspaceName
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

# Function body — keep behaviour aligned with the Bash variant.
```

## Cmdlet Naming Rules

- Use an approved verb (run `Get-Verb` to verify). Common: `New`,
  `Get`, `Set`, `Remove`, `Test`, `Invoke`, `Update`, `Copy`, `Sync`,
  `Install`, `Uninstall`.
- Use a singular noun. Prefix with a project tag where useful
  (e.g. `New-HBWorkspace`).
- Mirror the Bash script name where reasonable. The PowerShell name
  is the canonical name of the Cmdlet; the file name is just the
  loader.

## Parity Reminders

- Every parameter has a Bash-side counterpart with matching defaults.
- `SupportsShouldProcess` enables `-WhatIf` and `-Confirm`; mirror
  them with `--dry-run` on the Bash side.
- Validate parameters with `[ValidateSet]`, `[ValidateRange]`,
  `[ValidatePattern]`, or `[ValidateNotNullOrEmpty]`.
- Never use `Invoke-Expression` on untrusted input.
