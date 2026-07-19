#!/usr/bin/env bash
# Render ASCII Statistics Profile 2 through the canonical PowerShell engine.
set -euo pipefail

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
ENGINE="${SCRIPT_DIR}/render-project-statistics.ps1"
REPO="."
CHECK_ONLY=0
DRY_RUN=0
JSON=0

usage() {
  cat <<'USAGE'
Verwendung / Usage: render-project-statistics.sh [OPTIONEN]

  --repo PFAD     Zielrepository / target repository
  --check-only    Drift pruefen, keine Dateien schreiben / check drift, do not write
  --dry-run       Generierten Block anzeigen / show the generated block
  --json          Maschinenlesbaren Status ausgeben / emit machine-readable status
  --help, -h      Hilfe anzeigen / show help

Exitcodes: 0 = aktuell/erfolgreich, 1 = Drift, 2 = Aufruf- oder Toolingfehler.
Exit codes: 0 = current/success, 1 = drift, 2 = usage or tooling error.
USAGE
}

while [ $# -gt 0 ]; do
  case "${1:-}" in
    --repo)
      REPO="${2:?--repo benoetigt einen Pfad / requires a path}"
      shift
      ;;
    --check-only) CHECK_ONLY=1 ;;
    --dry-run) DRY_RUN=1 ;;
    --json) JSON=1 ;;
    --help|-h)
      usage
      exit 0
      ;;
    --)
      shift
      break
      ;;
    --*)
      echo "Fehler / Error: Unbekannte Option / Unknown option: $1" >&2
      exit 2
      ;;
    *)
      echo "Fehler / Error: Unerwartetes Argument / Unexpected argument: $1" >&2
      exit 2
      ;;
  esac
  shift
done

if [ "$CHECK_ONLY" -eq 1 ] && [ "$DRY_RUN" -eq 1 ]; then
  echo "Fehler / Error: --check-only und / and --dry-run sind nicht kombinierbar." >&2
  exit 2
fi
if ! command -v pwsh >/dev/null 2>&1; then
  echo "Fehler / Error: PowerShell 7 (pwsh) ist erforderlich / is required." >&2
  exit 2
fi
if [ ! -f "$ENGINE" ]; then
  echo "Fehler / Error: Renderer fehlt / renderer missing: $ENGINE" >&2
  exit 2
fi

arguments=(-NoProfile -File "$ENGINE" -Repo "$REPO")
[ "$CHECK_ONLY" -eq 1 ] && arguments+=(-CheckOnly)
[ "$DRY_RUN" -eq 1 ] && arguments+=(-WhatIf)
[ "$JSON" -eq 1 ] && arguments+=(-Json)

exec pwsh "${arguments[@]}"
