## Cross-Platform Parity & Documentation

### Mandate

Every script-shaped tool MUST work natively on macOS, Linux, and
Windows. Bash-only or PowerShell-only tooling creates a second-class
experience that silently excludes contributors and operators.

### Required pairing

Each critical script MUST exist in two variants:

- Bash 3.x+ as `*.sh` for macOS and Linux (target `bash` so that
  default macOS Bash 3.2 still works unless a higher version is
  documented)
- PowerShell Core 7+ as `*.ps1` for Windows (and as a portable
  alternative on macOS/Linux)

Both variants MUST provide identical functionality and produce
equivalent output.

### Definition of done

A script is not complete until all of the following are in place — and
all artefacts MUST be committed in the same change:

1. Both variants exist and pass manual verification on at least one
   target OS each.
2. A corresponding Unix man-page is provided for the Bash variant
   (default location `docs/man/<name>.1`).
3. Complete bilingual (DE first, EN second) comment-based help is
   provided for the PowerShell variant
   (`<#  .SYNOPSIS … #>` block).
4. The PowerShell script is also exposed as a Cmdlet (Advanced
   Function) using the `Verb-Noun` naming convention with an approved
   PowerShell verb (e.g. `New-HBWorkspace`, not
   `Create-HBWorkspace`).
5. Help switches `-h` / `--help` (Bash) and `-Help` / `Get-Help`
   (PowerShell) point to the man-page or internal help.

### Implementation discipline

- Bash scripts SHOULD use `set -euo pipefail` (or document a
  justification).
- Bash scripts MUST quote all variable expansions (`"$var"`,
  `"${arr[@]}"`) and avoid `eval` on untrusted input.
- Bash scripts MUST NOT depend on Bash 4+ features unless the project
  documents Bash 4 as a hard requirement (default macOS Bash is 3.2).
- PowerShell scripts MUST set `Set-StrictMode -Version Latest`.
- PowerShell subprocess calls SHOULD use `-NoProfile` to avoid loading
  user profiles that interfere with non-interactive runs.
- File paths SHOULD use forward slashes where possible; PowerShell
  scripts MUST handle the empty-string `$env:HOME` on Windows
  explicitly (use `if ($env:HOME) { $env:HOME } else { $env:USERPROFILE }`).
- Both variants MUST behave identically in dry-run mode (`--dry-run`
  for Bash, `-WhatIf` for PowerShell).

### Evidence locations

- Default man-page location: `docs/man/`.
- Default parity-checklist location: `docs/cross-platform/` or alongside
  the script.
- Document every `N/A` decision with rationale.
