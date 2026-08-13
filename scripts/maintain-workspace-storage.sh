#!/usr/bin/env bash
# Safely inventory and reclaim generated Level-2 workspace storage.
set -euo pipefail

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd -P)"
SOURCE_ROOT="$(cd -- "${SCRIPT_DIR}/.." && pwd -P)"
ENGINE="${SCRIPT_DIR}/lib/workspace_storage_maintenance.py"
POLICY="${SCRIPT_DIR}/config/workspace-storage-maintenance.json"
HOME_DIR="${HOME}"
REGISTRY=""
RESULT_FILE=""
MODE="update"
PROFILE="safe"
CONFIRM_DEEP=0
RUN_ID=""

usage() {
  cat <<'USAGE'
Verwendung / Usage: maintain-workspace-storage.sh [OPTIONEN]

Inventarisiert und bereinigt ausschließlich wiederherstellbare, geprüfte
Build-Ausgaben und Caches. Das Safe-Profil schützt Git-Dateien, Symlinks,
laufende Builds sowie cc65-Sample-/Targettest-Nachweise.

Inventories and reclaims only recoverable, verified build outputs and caches.
The safe profile protects Git files, symlinks, running builds, and cc65
sample/target-test evidence.

  --check-only             Nur inventarisieren / inventory only
  --dry-run                Löschplan zeigen / show the cleanup plan
  --profile safe|deep|none Bereinigungsprofil; Standard safe
                           Cleanup profile; default safe
  --confirm-deep-cleanup   Deep-Löschung für echten Lauf bestätigen
                           Confirm deep deletion for an update run
  --home-dir PFAD          Alternatives Home-Verzeichnis / alternative home
  --registry PFAD          Alternatives Level-2-Register / alternative registry
  --policy PFAD            Alternative Storage-Policy / alternative policy
  --result-file PFAD       Privater JSON-Bericht / private JSON report
  --run-id UUID            Korrelation mit dem Orchestrator / run correlation
  -h, --help               Diese Hilfe anzeigen / show this help

Exit-Codes: 0 = erfolgreich oder mit Provider-Warnungen, 2 = unsicherer
Vertrag, ungültiger Aufruf oder Betriebsfehler. Provider-Warnungen stehen im
JSON-Bericht und blockieren andere Wartungsstufen nicht.
USAGE
}

die() {
  printf 'Fehler / Error: %s\n' "$*" >&2
  exit 2
}

mode_selector_count=0
while [ $# -gt 0 ]; do
  case "$1" in
    --check-only) MODE="check-only"; mode_selector_count=$((mode_selector_count + 1)) ;;
    --dry-run) MODE="dry-run"; mode_selector_count=$((mode_selector_count + 1)) ;;
    --profile)
      [ $# -ge 2 ] || die "--profile benötigt einen Wert / requires a value"
      PROFILE="$2"
      shift
      ;;
    --confirm-deep-cleanup) CONFIRM_DEEP=1 ;;
    --home-dir)
      [ $# -ge 2 ] || die "--home-dir benötigt einen Pfad / requires a path"
      HOME_DIR="$2"
      shift
      ;;
    --registry)
      [ $# -ge 2 ] || die "--registry benötigt einen Pfad / requires a path"
      REGISTRY="$2"
      shift
      ;;
    --policy)
      [ $# -ge 2 ] || die "--policy benötigt einen Pfad / requires a path"
      POLICY="$2"
      shift
      ;;
    --result-file)
      [ $# -ge 2 ] || die "--result-file benötigt einen Pfad / requires a path"
      RESULT_FILE="$2"
      shift
      ;;
    --run-id)
      [ $# -ge 2 ] || die "--run-id benötigt eine UUID / requires a UUID"
      RUN_ID="$2"
      shift
      ;;
    -h|--help) usage; exit 0 ;;
    --) shift; break ;;
    --*) die "Unbekannte Option / unknown option: $1" ;;
    *) die "Unerwartetes Argument / unexpected argument: $1" ;;
  esac
  shift
done

[ "$mode_selector_count" -le 1 ] \
  || die "--check-only und --dry-run sind nicht kombinierbar / cannot be combined"
case "$PROFILE" in
  safe|deep|none) ;;
  *) die "Unbekanntes Profil / unknown profile: $PROFILE" ;;
esac
[ -d "$HOME_DIR" ] || die "Home-Verzeichnis fehlt / home directory is missing: $HOME_DIR"
HOME_DIR="$(cd -- "$HOME_DIR" && pwd -P)"
REGISTRY="${REGISTRY:-${HOME_DIR}/.home-baseline/level2-repository-registry.json}"
[ -f "$REGISTRY" ] || die "Level-2-Register fehlt / registry is missing: $REGISTRY"
[ -f "$POLICY" ] || die "Storage-Policy fehlt / policy is missing: $POLICY"
[ -f "$ENGINE" ] || die "Storage-Engine fehlt / engine is missing: $ENGINE"
command -v python3 >/dev/null 2>&1 || die "Python 3 ist erforderlich / is required"

if [ -z "$RUN_ID" ]; then
  RUN_ID="$(python3 -c 'import uuid; print(uuid.uuid4())')"
fi
python3 -c 'import sys, uuid; uuid.UUID(sys.argv[1])' "$RUN_ID" \
  || die "Run-ID ist keine gültige UUID / run ID is not a valid UUID"
RESULT_FILE="${RESULT_FILE:-${HOME_DIR}/.home-baseline/reports/workspace-storage-${RUN_ID}.json}"

arguments=(
  python3 "$ENGINE"
  --home-dir "$HOME_DIR"
  --registry "$REGISTRY"
  --policy "$POLICY"
  --report "$RESULT_FILE"
  --mode "$MODE"
  --profile "$PROFILE"
  --run-id "$RUN_ID"
)
[ "$CONFIRM_DEEP" -eq 1 ] && arguments+=(--confirm-deep-cleanup)

status=0
"${arguments[@]}" || status=$?
printf 'Storage-Bericht / storage report: %s\n' "$RESULT_FILE"
exit "$status"
