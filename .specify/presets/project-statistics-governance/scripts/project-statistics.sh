#!/usr/bin/env bash
# DE: Ein Parser, eine PowerShell-Engine. EN: One parser, one PowerShell engine.
set -euo pipefail
SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
ACTION=Status
REPO=.
CONFIG=docs/project-statistics/config.json
REVISION=HEAD
AS_OF=
PREVIEW=0
JSON=0
usage() {
  echo 'Usage: bash project-statistics.sh init|status|update [--repo PATH] [--config PATH]'
  echo '       [--revision REV] [--as-of YYYY-MM-DD] [--dry-run] [--json]'
  echo 'DE: Status/Vorschau schreiben nichts. EN: Status/preview never write.'
  echo 'Exit: 0 success/current; 1 drift; 2 invalid/precondition failure.'
}
die() { echo "ERROR: $*" >&2; exit 2; }
case "${1:-}" in
  init) ACTION=Init; shift ;;
  status) ACTION=Status; shift ;;
  update) ACTION=Update; shift ;;
esac
while [ "$#" -gt 0 ]; do
  case "$1" in
    --repo|--config|--revision|--as-of)
      [ "$#" -ge 2 ] && [ -n "$2" ] || die "Missing value: $1"
      case "$1" in
        --repo) REPO=$2 ;;
        --config) CONFIG=$2 ;;
        --revision) REVISION=$2 ;;
        --as-of) AS_OF=$2 ;;
      esac
      shift 2 ;;
    --dry-run) PREVIEW=1; shift ;;
    --json) JSON=1; shift ;;
    --check-only) ACTION=Status; shift ;;
    --help|-h) usage; exit 0 ;;
    *) die "Unknown argument: $1" ;;
  esac
done
command -v pwsh >/dev/null 2>&1 || die 'PowerShell 7 required / erforderlich.'
major="$(pwsh -NoProfile -Command '$PSVersionTable.PSVersion.Major')" || die 'PowerShell version check failed.'
case "$major" in ''|*[!0-9]*) die 'Invalid PowerShell version.' ;; esac
[ "$major" -ge 7 ] || die 'PowerShell 7 required / erforderlich.'
args=(-NoProfile -File "$SCRIPT_DIR/project-statistics.ps1" -Action "$ACTION" -Repo "$REPO" -Config "$CONFIG" -Revision "$REVISION")
[ -z "$AS_OF" ] || args+=(-AsOf "$AS_OF")
[ "$PREVIEW" -eq 0 ] || args+=(-WhatIf)
[ "$JSON" -eq 0 ] || args+=(-Json)
exec pwsh "${args[@]}"
