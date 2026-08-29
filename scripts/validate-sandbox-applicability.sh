#!/usr/bin/env bash
# Validiert die Sandbox-Anwendbarkeit ohne Schreibzugriff.
# Validates sandbox applicability without writing files.
set -euo pipefail

script_dir=$(CDPATH='' cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)

usage() {
  cat <<'EOF'
Usage: validate-sandbox-applicability.sh --evidence FILE [--repo-root PATH] [--json]

DE: Prüft Struktur und innere Konsistenz der TuiVision-Sandbox-Evidence.
EN: Validates structure and internal consistency of TuiVision sandbox evidence.

See docs/man/validate-sandbox-applicability.1.md for the complete contract.
EOF
}

for argument in "$@"; do
  case "$argument" in
    -h|--help) usage; exit 0 ;;
  esac
done

if command -v python3 >/dev/null 2>&1; then
  python_command=python3
elif command -v python >/dev/null 2>&1; then
  python_command=python
else
  printf '%s\n' 'ERROR: Python 3 is required.' >&2
  exit 3
fi

exec "$python_command" "$script_dir/validate-sandbox-applicability.py" "$@"
