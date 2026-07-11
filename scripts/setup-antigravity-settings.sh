#!/usr/bin/env bash
# Applies the hardened Antigravity CLI settings baseline on macOS/Linux.
set -euo pipefail

DRY_RUN=false
CHECK_ONLY=false
for arg in "$@"; do
  case "$arg" in
    --dry-run) DRY_RUN=true ;;
    --check-only) CHECK_ONLY=true ;;
    -h|--help)
      printf '%s\n' 'USAGE: setup-antigravity-settings.sh [--dry-run] [--check-only]'
      exit 0
      ;;
    *) printf 'Unbekanntes Argument / Unknown argument: %s\n' "$arg" >&2; exit 1 ;;
  esac
done

command -v python3 >/dev/null 2>&1 || {
  echo 'Fehler / Error: python3 wird benoetigt / is required.' >&2
  exit 1
}

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
TEMPLATE_FILE="${SCRIPT_DIR}/templates/antigravity-statusline.sh"
SETTINGS_DIR="${ANTIGRAVITY_HOME:-${HOME}/.gemini/antigravity-cli}"
SETTINGS_FILE="${SETTINGS_DIR}/settings.json"
STATUSLINE_FILE="${SETTINGS_DIR}/statusline.sh"

[ -f "$TEMPLATE_FILE" ] || {
  printf 'Fehler / Error: template not found: %s\n' "$TEMPLATE_FILE" >&2
  exit 1
}

if $DRY_RUN; then
  printf '[Dry Run] Antigravity settings: %s\n' "$SETTINGS_FILE"
  printf '[Dry Run] Statusline helper: %s\n' "$STATUSLINE_FILE"
  printf '%s\n' '[Dry Run] strict permissions, artifact review, sandbox on, workspace-only access, telemetry off'
  exit 0
fi

if $CHECK_ONLY; then
  python3 - "$SETTINGS_FILE" "$STATUSLINE_FILE" <<'PY'
import json
import sys
from pathlib import Path

settings_path = Path(sys.argv[1])
statusline_path = Path(sys.argv[2])
if not settings_path.is_file() or not statusline_path.is_file():
    print("DRIFT Antigravity settings or statusline helper missing")
    raise SystemExit(2)
try:
    data = json.loads(settings_path.read_text(encoding="utf-8"))
except json.JSONDecodeError as exc:
    print(f"DRIFT invalid Antigravity settings JSON: {exc}")
    raise SystemExit(2)
expected = {
    "toolPermission": "strict",
    "artifactReviewPolicy": "asks-for-review",
    "enableTerminalSandbox": True,
    "allowNonWorkspaceAccess": False,
    "enableTelemetry": False,
}
drift = [key for key, value in expected.items() if data.get(key) != value]
status = data.get("statusLine") or {}
if status.get("type") != "command" or status.get("command") != str(statusline_path):
    drift.append("statusLine")
if drift:
    print("DRIFT Antigravity settings: " + ", ".join(drift))
    raise SystemExit(2)
print("OK Antigravity settings hardened")
PY
  exit $?
fi

mkdir -p "$SETTINGS_DIR"
install -m 0755 "$TEMPLATE_FILE" "$STATUSLINE_FILE"
python3 - "$SETTINGS_FILE" "$STATUSLINE_FILE" <<'PY'
import json
import sys
from pathlib import Path

settings_path = Path(sys.argv[1])
statusline_path = Path(sys.argv[2])
if settings_path.exists():
    try:
        data = json.loads(settings_path.read_text(encoding="utf-8"))
    except json.JSONDecodeError as exc:
        raise SystemExit(f"Fehler / Error: invalid settings JSON: {exc}")
    if not isinstance(data, dict):
        raise SystemExit("Fehler / Error: settings JSON root must be an object")
else:
    data = {}
data.update({
    "toolPermission": "strict",
    "artifactReviewPolicy": "asks-for-review",
    "enableTerminalSandbox": True,
    "allowNonWorkspaceAccess": False,
    "enableTelemetry": False,
    "statusLine": {"type": "command", "command": str(statusline_path)},
})
settings_path.write_text(json.dumps(data, indent=2, ensure_ascii=True) + "\n", encoding="utf-8")
print(f"OK Antigravity settings hardened: {settings_path}")
PY
