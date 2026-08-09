#!/usr/bin/env bash
set -euo pipefail

script_dir=$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)
command -v pwsh >/dev/null 2>&1 || {
  printf 'BLOCKIERT: PowerShell 7 (pwsh) wird fuer die portable Modellzuordnung benoetigt. / BLOCKED: PowerShell 7 (pwsh) is required for portable model routing.\n' >&2
  exit 3
}
pwsh -NoLogo -NoProfile -Command 'if ($PSVersionTable.PSVersion.Major -lt 7) { exit 1 }' >/dev/null 2>&1 || {
  printf 'BLOCKIERT: pwsh konnte nicht als PowerShell 7 oder neuer ausgefuehrt werden. / BLOCKED: pwsh could not be executed as PowerShell 7 or newer.\n' >&2
  exit 3
}
exec pwsh -NoLogo -NoProfile -File "$script_dir/resolve-model-routing.ps1" "$@"
