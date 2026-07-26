#!/usr/bin/env bash
# Validate Documentation Impact evidence through the portable PowerShell core.
set -euo pipefail

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
EVIDENCE=""

usage() {
  cat <<'USAGE'
Verwendung / Usage:
  bash scripts/validate-documentation-impact.sh --evidence DATEI

Prueft den strukturellen Documentation-Impact-Vertrag.
Validates the structural Documentation Impact contract.
USAGE
}

while [ $# -gt 0 ]; do
  case "$1" in
    --evidence) EVIDENCE="${2:?--evidence requires a file}"; shift ;;
    -h|--help) usage; exit 0 ;;
    *) printf 'Unknown argument: %s\n' "$1" >&2; exit 2 ;;
  esac
  shift
done

[ -n "$EVIDENCE" ] || { usage >&2; exit 2; }
command -v pwsh >/dev/null 2>&1 || {
  printf '%s\n' 'PowerShell 7 (pwsh) is required.' >&2
  exit 2
}
exec pwsh -NoProfile -File "${SCRIPT_DIR}/validate-documentation-impact.ps1" \
  -Evidence "$EVIDENCE"
