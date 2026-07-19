#!/usr/bin/env bash
set -euo pipefail

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"

case "${1:-}" in
  -h|--help)
    cat <<'EOF'
orchestrate-parallel-autonomous-runs.sh

DE: Validiert und koordiniert isolierte autonome Spec-Kit-Kampagnen.
EN: Validates and coordinates isolated autonomous Spec Kit campaigns.

Usage:
  orchestrate-parallel-autonomous-runs.sh -Action ACTION -Manifest PATH [OPTIONS]

Actions:
  Validate, Start, Status, Stop, Resume, Consolidate

Status output:
  -OutputFormat Json|Text

Documentation:
  docs/man/orchestrate-parallel-autonomous-runs.1
EOF
    exit 0
    ;;
esac

if ! command -v pwsh >/dev/null 2>&1; then
  printf 'Fehler: PowerShell 7 (pwsh) ist fuer den strukturierten Campaign-Kern erforderlich.\n' >&2
  exit 2
fi

exec pwsh -NoLogo -NoProfile -File \
  "$script_dir/orchestrate-parallel-autonomous-runs.ps1" "$@"
