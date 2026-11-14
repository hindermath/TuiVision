#!/usr/bin/env bash
# DE: Nur synthetische Fixture pruefen. EN: Validate synthetic fixture only.
set -euo pipefail
fixture_script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd -P)"
fixture_args=()
for fixture_arg in "$@"; do
  case "$fixture_arg" in
    --json) fixture_args+=('-Json') ;;
    --require-tasks) fixture_args+=('-RequireTasks') ;;
    --include-tasks) fixture_args+=('-IncludeTasks') ;;
    *) printf 'Unsupported fixture option: %s\n' "$fixture_arg" >&2; exit 2 ;;
  esac
done
exec pwsh -NoProfile -File "$fixture_script_dir/../powershell/check-prerequisites.ps1" "${fixture_args[@]}"
