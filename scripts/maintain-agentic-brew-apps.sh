#!/usr/bin/env bash
# Maintain Homebrew packages for agentic development on macOS/Linux.
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
REGISTRY="$REPO_ROOT/scripts/config/brew-apps-registry.json"
VSCODE_REGISTRY="$REPO_ROOT/scripts/config/vscode-extensions-registry.json"
CLI_REGISTRY="$REPO_ROOT/scripts/config/required-cli-tools-registry.json"
NPM_AGENT_REGISTRY="$REPO_ROOT/scripts/config/npm-agent-cli-registry.json"
POWERSHELL_MODULE_REGISTRY="$REPO_ROOT/scripts/config/powershell-modules-registry.json"
LINUX_HARDENING="$REPO_ROOT/scripts/lib/linux-maintenance-hardening.py"
DRY_RUN=0
COMPARE_ONLY=0
SKIP_UPGRADE=0
INCLUDE_OPTIONAL=0
SKIP_VSCODE_EXTENSIONS=0
ALLOW_ADMIN_PROMPTS=0
RESULT_FILE=""
WORK_DIR=""
RESULT_ROWS=""
ATTEMPTS_FILE=""
FAILED_ATTEMPTS_FILE=""
RESULT_SEQUENCE=0
CURRENT_PROBE_JSON=""
CURRENT_PROBE_STATUS=""

usage() {
  cat <<'USAGE'
Verwendung / Usage: maintain-agentic-brew-apps.sh [OPTIONEN / OPTIONS]

Optionen / Options:
  --dry-run             Paketaktionen nur anzeigen / preview package actions
  --compare-only        Nur Registry vergleichen / compare registry only
  --registry PATH       Alternative Paket-Registry / alternative package registry
  --cli-registry PATH   Alternative Required-CLI-Registry
  --vscode-registry PATH
                        Alternative VS-Code-Extension-Registry
  --npm-agent-registry PATH
                        Alternative npm-Agent-CLI-Registry
  --powershell-module-registry PATH
                        Alternative PowerShell-Modul-Registry
  --skip-upgrade        brew/apt update+upgrade ueberspringen / skip
  --skip-vscode-extensions
                        VS-Code-Extensions ueberspringen / skip
  --include-optional    Optionale Eintraege installieren / install optional items
  --allow-admin-prompts
                        Sichtbare Admin-Prompts nur fuer diesen Lauf erlauben
                        Allow visible administrator prompts for this run only
  --result-file PATH    Atomaren JSON-Abschluss schreiben / write atomic result
  -h, --help            Hilfe anzeigen / show this help

Exit-Codes / Exit codes:
  0  Required-Sollzustand erreicht / required desired state reached
  1  Required-Drift oder Installationsfehler / required drift or install failure
  2  Ungueltiger Aufruf oder Betriebsvertrag / invalid invocation or contract
USAGE
}

while [ "$#" -gt 0 ]; do
  case "$1" in
    --dry-run)
      DRY_RUN=1
      ;;
    --compare-only)
      COMPARE_ONLY=1
      ;;
    --registry)
      if [ "$#" -lt 2 ] || [ -z "${2:-}" ]; then
        echo "Fehler: --registry benoetigt einen Pfad." >&2
        usage >&2
        exit 1
      fi
      REGISTRY="${2:-}"
      shift
      ;;
    --cli-registry)
      if [ "$#" -lt 2 ] || [ -z "${2:-}" ]; then
        echo "Fehler: --cli-registry benoetigt einen Pfad." >&2
        usage >&2
        exit 2
      fi
      CLI_REGISTRY="${2:-}"
      shift
      ;;
    --vscode-registry)
      if [ "$#" -lt 2 ] || [ -z "${2:-}" ]; then
        echo "Fehler: --vscode-registry benoetigt einen Pfad." >&2
        usage >&2
        exit 1
      fi
      VSCODE_REGISTRY="${2:-}"
      shift
      ;;
    --npm-agent-registry)
      if [ "$#" -lt 2 ] || [ -z "${2:-}" ]; then
        echo "Fehler: --npm-agent-registry benoetigt einen Pfad." >&2
        usage >&2
        exit 1
      fi
      NPM_AGENT_REGISTRY="${2:-}"
      shift
      ;;
    --powershell-module-registry)
      if [ "$#" -lt 2 ] || [ -z "${2:-}" ]; then
        echo "Fehler: --powershell-module-registry benoetigt einen Pfad." >&2
        usage >&2
        exit 1
      fi
      POWERSHELL_MODULE_REGISTRY="${2:-}"
      shift
      ;;
    --skip-upgrade)
      SKIP_UPGRADE=1
      ;;
    --skip-vscode-extensions)
      SKIP_VSCODE_EXTENSIONS=1
      ;;
    --include-optional)
      INCLUDE_OPTIONAL=1
      ;;
    --allow-admin-prompts)
      ALLOW_ADMIN_PROMPTS=1
      ;;
    --result-file)
      if [ "$#" -lt 2 ] || [ -z "${2:-}" ]; then
        echo "Fehler: --result-file benoetigt einen Pfad." >&2
        usage >&2
        exit 2
      fi
      RESULT_FILE="${2:-}"
      shift
      ;;
    -h|--help)
      usage
      exit 0
      ;;
    *)
      echo "Fehler: Unbekannte Option: $1" >&2
      usage >&2
      exit 1
      ;;
  esac
  shift
done

if [ ! -f "$REGISTRY" ]; then
  echo "Fehler: Registry nicht gefunden: $REGISTRY" >&2
  exit 1
fi

if [ "$SKIP_VSCODE_EXTENSIONS" -eq 0 ] && [ ! -f "$VSCODE_REGISTRY" ]; then
  echo "Fehler: VS-Code-Extension-Registry nicht gefunden: $VSCODE_REGISTRY" >&2
  exit 1
fi

if [ ! -f "$CLI_REGISTRY" ]; then
  echo "Fehler: Required-CLI-Registry nicht gefunden: $CLI_REGISTRY" >&2
  exit 1
fi

if [ ! -f "$NPM_AGENT_REGISTRY" ]; then
  echo "Fehler: npm-Agent-CLI-Registry nicht gefunden: $NPM_AGENT_REGISTRY" >&2
  exit 1
fi

if [ ! -f "$POWERSHELL_MODULE_REGISTRY" ]; then
  echo "Fehler: PowerShell-Modul-Registry nicht gefunden: $POWERSHELL_MODULE_REGISTRY" >&2
  exit 1
fi

if ! command -v python3 >/dev/null 2>&1; then
  echo "Fehler: python3 wird fuer die JSON-Registry benoetigt." >&2
  exit 1
fi

[ -f "$LINUX_HARDENING" ] || {
  echo "Fehler: Linux-Haertungsvertrag nicht gefunden: $LINUX_HARDENING" >&2
  exit 2
}

OS_NAME="$(uname -s)"
WORK_DIR="$(mktemp -d)"
RESULT_ROWS="$WORK_DIR/registry-item-results.tsv"
ATTEMPTS_FILE="$WORK_DIR/attempts.tsv"
FAILED_ATTEMPTS_FILE="$WORK_DIR/failed-attempts.tsv"
: > "$RESULT_ROWS"
: > "$ATTEMPTS_FILE"
: > "$FAILED_ATTEMPTS_FILE"

cleanup() {
  if [ -n "$WORK_DIR" ] && [ -d "$WORK_DIR" ]; then
    rm -rf -- "$WORK_DIR"
  fi
}
handle_signal() {
  local exit_code="$1"
  trap - EXIT INT TERM
  cleanup
  exit "$exit_code"
}
trap cleanup EXIT
trap 'handle_signal 130' INT
trap 'handle_signal 143' TERM

if [ -z "$RESULT_FILE" ]; then
  RESULT_FILE="$WORK_DIR/toolchain-result.json"
fi

log() { printf '%s\n' "$*"; }
run_cmd() {
  if [ "$DRY_RUN" -eq 1 ]; then
    printf 'DRY-RUN:'
    printf ' %q' "$@"
    printf '\n'
  else
    "$@"
  fi
}

snapshot_registry() {
  local name="$1"
  shift
  local destination="$WORK_DIR/$name"
  "$@" > "$destination"
  printf '%s\n' "$destination"
}

mark_attempt() {
  printf '%s\t%s\n' "$1" "$2" >> "$ATTEMPTS_FILE"
}

mark_failed_attempt() {
  printf '%s\t%s\n' "$1" "$2" >> "$FAILED_ATTEMPTS_FILE"
}

was_attempted() {
  grep -Fqx -- "$1"$'\t'"$2" "$ATTEMPTS_FILE"
}

attempt_failed() {
  grep -Fqx -- "$1"$'\t'"$2" "$FAILED_ATTEMPTS_FILE"
}

record_result() {
  local kind="$1" item_id="$2" scope="$3" attempted="$4" final_status="$5"
  local probe_status="$6" evidence="$7" next_action="$8"
  evidence="${evidence//$'\t'/ }"
  evidence="${evidence//$'\n'/ }"
  next_action="${next_action//$'\t'/ }"
  next_action="${next_action//$'\n'/ }"
  RESULT_SEQUENCE=$((RESULT_SEQUENCE + 1))
  printf '%s\t%s\t%s\t%s\t%s\t%s\t%s\t%s\t%s\n' \
    "$RESULT_SEQUENCE" "$kind" "$item_id" "$scope" "$attempted" \
    "$final_status" "$probe_status" "${evidence:-N/A}" "${next_action:-N/A}" \
    >> "$RESULT_ROWS"
}

validate_item_id() {
  local item_id="$1"
  if [[ ! "$item_id" =~ ^[A-Za-z0-9@._/+:-]+$ ]]; then
    echo "Fehler: Ungueltige Registry-ID / invalid registry id: $item_id" >&2
    exit 2
  fi
}

registry_items() {
  local section="$1"
  local scope="${2:-all}"
  python3 - "$REGISTRY" "$section" "$scope" <<'PY'
import json
import sys

registry_path, section, scope = sys.argv[1:4]
with open(registry_path, encoding="utf-8") as handle:
    data = json.load(handle)

if section == "aptFallback":
    items = data.get("aptFallback", {}).get("packages", [])
    key = "name"
else:
    items = data.get(section, [])
    key = "name"

for item in items:
    if scope != "all" and item.get("scope", "required") != scope:
        continue
    value = item.get(key)
    if value:
        print(value)
PY
}

registry_scoped_items() {
  local section="$1"
  python3 - "$REGISTRY" "$section" <<'PY'
import json
import sys

registry_path, section = sys.argv[1:3]
with open(registry_path, encoding="utf-8") as handle:
    data = json.load(handle)
items = (
    data.get("aptFallback", {}).get("packages", [])
    if section == "aptFallback"
    else data.get(section, [])
)
for item in items:
    value = item.get("name")
    scope = item.get("scope", "required")
    if value and scope in {"required", "optional"}:
        print(f"{value}\t{scope}")
PY
}

registry_linked_formulae() {
  python3 - "$REGISTRY" <<'PY'
import json
import sys

with open(sys.argv[1], encoding="utf-8") as handle:
    data = json.load(handle)
for item in data.get("formulae", []):
    if item.get("ensureLinked") is True and item.get("name"):
        print(item["name"])
PY
}

registry_link_commands() {
  local formula="$1"
  python3 - "$REGISTRY" "$formula" <<'PY'
import json
import sys

with open(sys.argv[1], encoding="utf-8") as handle:
    data = json.load(handle)
for item in data.get("formulae", []):
    if item.get("name") == sys.argv[2]:
        for command in item.get("linkCommands", []):
            if command:
                print(command)
        break
PY
}

vscode_registry_items() {
  local section="$1"
  local scope="${2:-all}"
  python3 - "$VSCODE_REGISTRY" "$section" "$scope" <<'PY'
import json
import sys

registry_path, section, scope = sys.argv[1:4]
with open(registry_path, encoding="utf-8") as handle:
    data = json.load(handle)

for item in data.get(section, []):
    if scope != "all" and item.get("scope", "required") != scope:
        continue
    value = item.get("id")
    if value:
        print(value)
PY
}

vscode_registry_scoped_items() {
  python3 - "$VSCODE_REGISTRY" <<'PY'
import json
import sys

with open(sys.argv[1], encoding="utf-8") as handle:
    data = json.load(handle)
for item in data.get("extensions", []):
    value = item.get("id")
    scope = item.get("scope", "required")
    if value and scope in {"required", "optional"}:
        print(f"{value}\t{scope}")
PY
}

cli_registry_items() {
  local scope="${1:-all}"
  python3 - "$CLI_REGISTRY" "$OS_NAME" "$scope" <<'PY'
import json
import sys

registry_path, os_name, scope = sys.argv[1:4]
with open(registry_path, encoding="utf-8") as handle:
    data = json.load(handle)

for item in data.get("tools", []):
    if scope != "all" and item.get("scope", "required") != scope:
        continue
    if os_name not in item.get("platforms", []):
        continue
    install = item.get("install", {})
    print(
        "\t".join(
            [
                item.get("id", ""),
                item.get("command", ""),
                json.dumps(item.get("args", []), separators=(",", ":")),
                install.get("manager") or "-",
                json.dumps(install.get("arguments", []), separators=(",", ":")),
                install.get("url", ""),
                install.get("sha256", ""),
                install.get("interpreter", ""),
            ]
        )
    )
PY
}

npm_agent_registry_items() {
  local scope="${1:-all}"
  python3 - "$NPM_AGENT_REGISTRY" "$OS_NAME" "$scope" <<'PY'
import json
import sys

registry_path, os_name, scope = sys.argv[1:4]
with open(registry_path, encoding="utf-8") as handle:
    data = json.load(handle)

for item in data.get("tools", []):
    if scope != "all" and item.get("scope", "required") != scope:
        continue
    if os_name not in item.get("platforms", []):
        continue
    print(
        "\t".join(
            [
                item.get("id", ""),
                item.get("package", ""),
                item.get("command", ""),
                json.dumps(item.get("args", []), separators=(",", ":")),
            ]
        )
    )
PY
}

registry_excluded_casks() {
  printf '%s\n' "xquartz"
  python3 - "$REGISTRY" <<'PY'
import json
import sys

with open(sys.argv[1], encoding="utf-8") as handle:
    data = json.load(handle)
for item in data.get("policy", {}).get("excludedCasks", []):
    name = item.get("name")
    if name:
        print(name)
PY
}

installed_registry_formulae() {
  local formula snapshot
  snapshot="$(snapshot_registry installed-registry-formulae.tsv registry_items formulae all)"
  while IFS= read -r -u 3 formula; do
    [ -n "$formula" ] || continue
    if brew list --formula --versions "$formula" >/dev/null 2>&1; then
      printf '%s\n' "$formula"
    fi
  done 3< "$snapshot"
}

installed_requested_formulae() {
  brew info --json=v2 --installed 2>/dev/null \
    | python3 -c '
import json
import sys

data = json.load(sys.stdin)
for formula in data.get("formulae", []):
    if any(item.get("installed_on_request") for item in formula.get("installed", [])):
        print(formula["full_name"])
' \
    | sort -u
}

installed_casks() {
  {
    brew list --cask 2>/dev/null || true
    if visual_studio_code_app_present; then
      printf '%s\n' "visual-studio-code"
    fi
  } | sort -u
}

visual_studio_code_app_present() {
  [ -d "/Applications/Visual Studio Code.app" ] \
    || [ -d "$HOME/Applications/Visual Studio Code.app" ]
}

cask_present() {
  local cask="$1"
  if brew list --cask --versions "$cask" >/dev/null 2>&1; then
    return 0
  fi
  case "$cask" in
    visual-studio-code)
      visual_studio_code_app_present
      ;;
    *)
      return 1
      ;;
  esac
}

code_candidates() {
  if [ -n "${VSCODE_CLI:-}" ]; then
    printf '%s\n' "$VSCODE_CLI"
  fi
  if command -v code >/dev/null 2>&1; then
    command -v code
  fi
  printf '%s\n' \
    "/Applications/Visual Studio Code.app/Contents/Resources/app/bin/code" \
    "$HOME/Applications/Visual Studio Code.app/Contents/Resources/app/bin/code" \
    "/opt/homebrew/bin/code" \
    "/usr/local/bin/code"
}

find_vscode_cli() {
  local candidate seen candidate_snapshot
  seen=""
  candidate_snapshot="$(snapshot_registry vscode-candidates.tsv code_candidates)"
  while IFS= read -r -u 3 candidate; do
    [ -n "$candidate" ] || continue
    case "$seen" in
      *"|$candidate|"*) continue ;;
    esac
    seen="$seen|$candidate|"
    [ -x "$candidate" ] || continue
    if "$candidate" --version </dev/null >/dev/null 2>&1 \
        && "$candidate" --list-extensions </dev/null >/dev/null 2>&1; then
      printf '%s\n' "$candidate"
      return 0
    fi
    printf 'WARN: VS-Code-CLI nicht nutzbar: %s\n' "$candidate" >&2
  done 3< "$candidate_snapshot"
  return 1
}

installed_vscode_extensions() {
  local code_cli="$1"
  "$code_cli" --list-extensions 2>/dev/null | tr '[:upper:]' '[:lower:]' | sort -u
}

print_missing() {
  local label="$1"
  local installed_file="$2"
  local registry_file="$3"
  local output
  output="$(comm -13 "$installed_file" "$registry_file" || true)"
  if [ -n "$output" ]; then
    log "$label"
    printf '%s\n' "$output" | sed 's/^/  - /'
  else
    log "$label: none"
  fi
}

json_array_items() {
  local json="$1"
  python3 - "$json" <<'PY'
import json
import sys

for value in json.loads(sys.argv[1] or "[]"):
    print(value)
PY
}

probe_cli_tool() {
  local tool_id="$1" command_name="$2" args_json="$3"
  local probe_output probe_exit expected_swift arg
  local -a args=()

  while IFS= read -r arg; do
    args+=("$arg")
  done < <(json_array_items "$args_json")
  probe_exit=0
  probe_output="$(
    python3 "$LINUX_HARDENING" probe \
      --tool-id "$tool_id" \
      --timeout-seconds 5 \
      --redact "$HOME" \
      -- "$command_name" "${args[@]}"
  )" || probe_exit=$?
  CURRENT_PROBE_JSON="$probe_output"
  CURRENT_PROBE_STATUS="$(
    python3 -c 'import json,sys; print(json.load(sys.stdin).get("status", "Unusable"))' \
      <<< "$probe_output"
  )"
  CURRENT_PROBE_EVIDENCE="$(
    python3 -c 'import json,sys; print(json.load(sys.stdin).get("sanitizedEvidence", "N/A"))' \
      <<< "$probe_output"
  )"
  CURRENT_PROBE_NEXT_ACTION="$(
    python3 -c 'import json,sys; print(json.load(sys.stdin).get("nextAction", "N/A"))' \
      <<< "$probe_output"
  )"
  if [ "$tool_id" = "swift" ] && [ "$OS_NAME" = "Linux" ] \
      && [ "$CURRENT_PROBE_STATUS" = "Available" ]; then
    expected_swift="$(
      python3 - "$CLI_REGISTRY" <<'PY'
import json
import sys

with open(sys.argv[1], encoding="utf-8") as handle:
    registry = json.load(handle)
for item in registry.get("tools", []):
    if item.get("id") == "swift":
        print(item.get("install", {}).get("linux", {}).get("swiftVersion", ""))
        break
PY
    )"
    if [ -n "$expected_swift" ] \
        && [[ "$CURRENT_PROBE_EVIDENCE" != *"Swift version ${expected_swift}"* ]] \
        && [[ "$CURRENT_PROBE_EVIDENCE" != *"Swift ${expected_swift}"* ]]; then
      CURRENT_PROBE_STATUS="Unusable"
      CURRENT_PROBE_NEXT_ACTION="Swift ${expected_swift} aktivieren / activate the pinned Swift version."
      return 1
    fi
  fi
  [ "$probe_exit" -eq 0 ] && [ "$CURRENT_PROBE_STATUS" = "Available" ]
}

cli_tool_available() {
  probe_cli_tool "$1" "$2" "$3"
}

install_swift_tool() {
  local id="$1" contract_json contract_status archive extracted post_install
  local swift_env swiftly_command swift_version swiftly_platform url digest
  local contract_exit=0

  if [ "$OS_NAME" != "Linux" ]; then
    return 0
  fi
  mark_attempt cli "$id"
  if [ "$ALLOW_ADMIN_PROMPTS" -ne 1 ]; then
    log "DEFERRED_ADMIN_REQUIRED: Swift-Bereitstellung benoetigt aktuelle Admin-Prompt-Autoritaet."
    log "DEFERRED_ADMIN_REQUIRED: Swift provisioning requires current admin-prompt authority."
    mark_failed_attempt cli "$id"
    return 0
  fi

  contract_json="$(
    python3 "$LINUX_HARDENING" swift-contract \
      --registry "$CLI_REGISTRY" \
      --os-release /etc/os-release \
      --architecture "$(uname -m)"
  )" || contract_exit=$?
  contract_status="$(
    python3 -c 'import json,sys; print(json.load(sys.stdin).get("status", "InvalidContract"))' \
      <<< "$contract_json"
  )"
  if [ "$contract_exit" -ne 0 ] || [ "$contract_status" != "Supported" ]; then
    log "SWIFT CONTRACT: $contract_json"
    mark_failed_attempt cli "$id"
    return 0
  fi
  IFS=$'\t' read -r swift_version swiftly_platform url digest <<< "$(
    python3 -c '
import json, sys
value = json.load(sys.stdin)
print("\t".join(value[key] for key in ("swiftVersion", "swiftlyPlatform", "url", "sha256")))
' <<< "$contract_json"
  )"

  if [ "$DRY_RUN" -eq 1 ]; then
    log "DRY-RUN: Swiftly aus gepinntem offiziellen Archiv installieren / install from pinned official archive"
    log "DRY-RUN: Swift ${swift_version} fuer ${swiftly_platform} installieren / install Swift for platform"
    return 0
  fi
  if ! command -v curl >/dev/null 2>&1; then
    log "SWIFT FAILED: curl fehlt / curl is missing"
    mark_failed_attempt cli "$id"
    return 0
  fi

  archive="$WORK_DIR/swiftly.tar.gz"
  extracted="$WORK_DIR/swiftly"
  post_install="$WORK_DIR/swift-post-install.sh"
  if ! curl --proto '=https' --tlsv1.2 -fsSL "$url" -o "$archive" </dev/null; then
    log "SWIFT FAILED: offizieller Download fehlgeschlagen / official download failed"
    mark_failed_attempt cli "$id"
    return 0
  fi
  if ! python3 "$LINUX_HARDENING" verify-sha256 \
      --path "$archive" --expected "$digest" >/dev/null; then
    log "SWIFT FAILED: SHA-256-Integritaet nicht bestaetigt / integrity not verified"
    mark_failed_attempt cli "$id"
    return 0
  fi
  if ! python3 "$LINUX_HARDENING" extract-swiftly \
      --archive "$archive" --output "$extracted" >/dev/null; then
    log "SWIFT FAILED: Archiv konnte nicht sicher extrahiert werden / safe extraction failed"
    mark_failed_attempt cli "$id"
    return 0
  fi
  if ! "$extracted" init --verbose --assume-yes --skip-install \
      --no-modify-profile --platform "$swiftly_platform" </dev/null; then
    log "SWIFT FAILED: Swiftly-Initialisierung fehlgeschlagen / initialization failed"
    mark_failed_attempt cli "$id"
    return 0
  fi

  swift_env="${SWIFTLY_HOME_DIR:-$HOME/.local/share/swiftly}/env.sh"
  if [ ! -f "$swift_env" ] || [ -L "$swift_env" ]; then
    log "SWIFT FAILED: erwartete Swiftly-Umgebung fehlt / expected environment is missing"
    mark_failed_attempt cli "$id"
    return 0
  fi
  # Swiftly erzeugt diese begrenzte Datei aus dem zuvor verifizierten Binary.
  # shellcheck disable=SC1090
  source "$swift_env"
  swiftly_command="$(command -v swiftly 2>/dev/null || true)"
  if [ -z "$swiftly_command" ]; then
    log "SWIFT FAILED: Swiftly ist im aktuellen Prozess nicht aktiv / not active in this process"
    mark_failed_attempt cli "$id"
    return 0
  fi
  if ! "$swiftly_command" use "$swift_version" --assume-yes </dev/null; then
    if ! "$swiftly_command" install "$swift_version" --use --assume-yes \
        --post-install-file "$post_install" </dev/null; then
      log "SWIFT FAILED: Toolchain-Installation fehlgeschlagen / toolchain installation failed"
      mark_failed_attempt cli "$id"
      return 0
    fi
  fi
  if [ -s "$post_install" ]; then
    if [ ! -f "$post_install" ] || [ -L "$post_install" ]; then
      log "SWIFT FAILED: unzulaessiges Post-Install-Artefakt / invalid post-install artifact"
      mark_failed_attempt cli "$id"
      return 0
    fi
    if ! sudo -- bash "$post_install" </dev/null; then
      log "SWIFT FAILED: autorisierte Systemnacharbeit fehlgeschlagen / authorized post-install failed"
      mark_failed_attempt cli "$id"
      return 0
    fi
  fi
  hash -r
  if ! probe_cli_tool "$id" swift '["--version"]'; then
    log "SWIFT FAILED: Swift ist nach Installation nicht nutzbar / unavailable after install"
    log "  $CURRENT_PROBE_EVIDENCE"
    mark_failed_attempt cli "$id"
  fi
}

install_cli_tool() {
  local id="$1"
  local manager="$2"
  local install_args_json="$3"
  local install_url="${4:-}"
  local install_sha256="${5:-}"
  local interpreter="${6:-}"
  local -a install_args
  local arg

  case "$manager" in
    uv)
      while IFS= read -r arg; do
        install_args+=("$arg")
      done < <(json_array_items "$install_args_json")
      log "INSTALL cli tool: $id"
      mark_attempt cli "$id"
      if [ "$DRY_RUN" -eq 1 ] || command -v uv >/dev/null 2>&1; then
        if ! run_cmd uv "${install_args[@]}" </dev/null; then
          mark_failed_attempt cli "$id"
        fi
      else
        log "SKIP cli tool install: $id (uv fehlt)"
        mark_failed_attempt cli "$id"
      fi
      ;;
    vendor-script)
      if [ "$OS_NAME" != "Linux" ]; then
        log "MISSING cli tool: $id (vendor installer is Linux-only)"
        return 0
      fi
      if [ -z "$install_url" ] || [ -z "$install_sha256" ] || [ "$interpreter" != "bash" ]; then
        log "SKIP cli tool install: $id (ungueltige vendor-script Registrydaten)"
        return 0
      fi
      log "INSTALL cli tool: $id (verified vendor script)"
      mark_attempt cli "$id"
      if [ "$DRY_RUN" -eq 1 ]; then
        log "DRY-RUN: download $install_url, verify sha256=$install_sha256, execute with bash"
        return 0
      fi
      command -v curl >/dev/null 2>&1 || {
        log "SKIP cli tool install: $id (curl fehlt)"
        mark_failed_attempt cli "$id"
        return 0
      }
      local installer actual_sha256
      installer="$(mktemp "$WORK_DIR/vendor-installer.XXXXXX")"
      if ! curl --proto '=https' --tlsv1.2 -fsSL "$install_url" -o "$installer" </dev/null; then
        rm -f -- "$installer"
        log "SKIP cli tool install: $id (Download fehlgeschlagen)"
        mark_failed_attempt cli "$id"
        return 0
      fi
      if command -v sha256sum >/dev/null 2>&1; then
        actual_sha256="$(sha256sum "$installer" | awk '{print $1}')"
      elif command -v shasum >/dev/null 2>&1; then
        actual_sha256="$(shasum -a 256 "$installer" | awk '{print $1}')"
      else
        rm -f -- "$installer"
        log "SKIP cli tool install: $id (SHA-256-Werkzeug fehlt)"
        mark_failed_attempt cli "$id"
        return 0
      fi
      if [ "$actual_sha256" != "$install_sha256" ]; then
        rm -f -- "$installer"
        log "SKIP cli tool install: $id (Installer-Pruefsumme geaendert; Registry zuerst pruefen)"
        mark_failed_attempt cli "$id"
        return 0
      fi
      if ! bash "$installer" </dev/null; then
        mark_failed_attempt cli "$id"
      fi
      rm -f -- "$installer"
      ;;
    swiftly)
      install_swift_tool "$id"
      ;;
    *)
      log "MISSING cli tool: $id"
      ;;
  esac
}

install_cli_tools() {
  local scope id command_name args_json manager install_args_json install_url install_sha256 interpreter snapshot
  scope="required"
  [ "$INCLUDE_OPTIONAL" -eq 1 ] && scope="all"
  snapshot="$(snapshot_registry cli-install.tsv cli_registry_items "$scope")"

  while IFS=$'\t' read -r -u 3 id command_name args_json manager install_args_json install_url install_sha256 interpreter; do
    [ -n "$id" ] || continue
    validate_item_id "$id"
    if cli_tool_available "$id" "$command_name" "$args_json"; then
      log "OK cli tool: $id"
    else
      install_cli_tool "$id" "$manager" "$install_args_json" "$install_url" "$install_sha256" "$interpreter"
    fi
  done 3< "$snapshot"
}

install_npm_agent_tools() {
  local scope id package_name command_name args_json snapshot
  scope="required"
  [ "$INCLUDE_OPTIONAL" -eq 1 ] && scope="all"
  snapshot="$(snapshot_registry npm-install.tsv npm_agent_registry_items "$scope")"

  while IFS=$'\t' read -r -u 3 id package_name command_name args_json; do
    [ -n "$id" ] || continue
    validate_item_id "$id"
    validate_item_id "$package_name"
    if cli_tool_available "$id" "$command_name" "$args_json"; then
      log "OK npm agent cli: $id"
    elif [ "$DRY_RUN" -eq 1 ] || command -v npm >/dev/null 2>&1; then
      log "INSTALL npm agent cli: $id ($package_name)"
      mark_attempt npm-cli "$id"
      if ! run_cmd npm install -g "$package_name" </dev/null; then
        mark_failed_attempt npm-cli "$id"
      fi
    else
      log "MISSING npm agent cli: $id (npm fehlt)"
    fi
  done 3< "$snapshot"
}

compare_cli_scope() {
  local scope="$1"
  local label="$2"
  local id command_name args_json manager install_args_json install_url install_sha256 interpreter
  local missing snapshot attempted final_status
  missing=""
  snapshot="$(snapshot_registry "cli-compare-${scope}.tsv" cli_registry_items "$scope")"

  while IFS=$'\t' read -r -u 3 id command_name args_json manager install_args_json install_url install_sha256 interpreter; do
    [ -n "$id" ] || continue
    attempted=false
    was_attempted cli "$id" && attempted=true
    if cli_tool_available "$id" "$command_name" "$args_json"; then
      final_status="Present"
      [ "$attempted" = true ] && final_status="Installed"
    else
      missing="${missing}${id}"$'\n'
      final_status="StillMissing"
      [ "$DRY_RUN" -eq 1 ] && [ "$attempted" = true ] && final_status="Planned"
      attempt_failed cli "$id" && final_status="Failed"
    fi
    record_result cli "$id" "$scope" "$attempted" "$final_status" \
      "$CURRENT_PROBE_STATUS" "$CURRENT_PROBE_EVIDENCE" "$CURRENT_PROBE_NEXT_ACTION"
  done 3< "$snapshot"

  if [ -n "$missing" ]; then
    log "$label"
    printf '%s' "$missing" | sed '/^$/d; s/^/  - /'
  else
    log "$label: none"
  fi
}

compare_cli_registry() {
  log "Required CLI tool registry: $CLI_REGISTRY"
  compare_cli_scope required "missing_on_machine.required.cli_tools"
  compare_cli_scope optional "missing_on_machine.optional.cli_tools"
}

compare_npm_agent_scope() {
  local scope="$1"
  local label="$2"
  local id package_name command_name args_json
  local missing snapshot attempted final_status
  missing=""
  snapshot="$(snapshot_registry "npm-compare-${scope}.tsv" npm_agent_registry_items "$scope")"

  while IFS=$'\t' read -r -u 3 id package_name command_name args_json; do
    [ -n "$id" ] || continue
    attempted=false
    was_attempted npm-cli "$id" && attempted=true
    if cli_tool_available "$id" "$command_name" "$args_json"; then
      final_status="Present"
      [ "$attempted" = true ] && final_status="Installed"
    else
      missing="${missing}${id}"$'\n'
      final_status="StillMissing"
      [ "$DRY_RUN" -eq 1 ] && [ "$attempted" = true ] && final_status="Planned"
      attempt_failed npm-cli "$id" && final_status="Failed"
    fi
    record_result npm-cli "$id" "$scope" "$attempted" "$final_status" \
      "$CURRENT_PROBE_STATUS" "$CURRENT_PROBE_EVIDENCE" "$CURRENT_PROBE_NEXT_ACTION"
  done 3< "$snapshot"

  if [ -n "$missing" ]; then
    log "$label"
    printf '%s' "$missing" | sed '/^$/d; s/^/  - /'
  else
    log "$label: none"
  fi
}

compare_npm_agent_registry() {
  log "npm agent CLI registry: $NPM_AGENT_REGISTRY"
  compare_npm_agent_scope required "missing_on_machine.required.npm_agent_cli_tools"
  compare_npm_agent_scope optional "missing_on_machine.optional.npm_agent_cli_tools"
}

maintain_powershell_modules() {
  local maintainer="$REPO_ROOT/scripts/maintain-powershell-modules.ps1"
  local probe_output probe_exit=0 final_status="Present"
  local -a arguments

  if [ ! -f "$maintainer" ]; then
    log "MISSING powershell module maintainer: $maintainer"
    return 0
  fi

  arguments=(-NoProfile -File "$maintainer" -Registry "$POWERSHELL_MODULE_REGISTRY")
  [ "$COMPARE_ONLY" -eq 1 ] && arguments+=(-CompareOnly)
  [ "$DRY_RUN" -eq 1 ] && arguments+=(-WhatIf)
  [ "$INCLUDE_OPTIONAL" -eq 1 ] && arguments+=(-IncludeOptional)

  if [ "$DRY_RUN" -eq 1 ]; then
    run_cmd pwsh "${arguments[@]}"
    return 0
  fi
  probe_output="$(
    python3 "$LINUX_HARDENING" probe \
      --tool-id powershell-modules \
      --timeout-seconds 300 \
      --redact "$HOME" \
      -- pwsh "${arguments[@]}"
  )" || probe_exit=$?
  CURRENT_PROBE_STATUS="$(
    python3 -c 'import json,sys; print(json.load(sys.stdin).get("status", "Unusable"))' \
      <<< "$probe_output"
  )"
  CURRENT_PROBE_EVIDENCE="$(
    python3 -c 'import json,sys; print(json.load(sys.stdin).get("sanitizedEvidence", "N/A"))' \
      <<< "$probe_output"
  )"
  CURRENT_PROBE_NEXT_ACTION="$(
    python3 -c 'import json,sys; print(json.load(sys.stdin).get("nextAction", "N/A"))' \
      <<< "$probe_output"
  )"
  if [ "$probe_exit" -ne 0 ]; then
    final_status="$([ "$CURRENT_PROBE_STATUS" = "Missing" ] && printf 'StillMissing' || printf 'Failed')"
    log "WARN powershell module follow-up: $CURRENT_PROBE_STATUS"
    log "  $CURRENT_PROBE_EVIDENCE"
  fi
  [ "$final_status" = "Present" ] \
    || log "WARN: PowerShell-Modul-Follow-up bleibt optional und nicht fatal / remains optional and non-fatal."
}

compare_brew_registry() {
  local tmp_dir installed_f installed_requested_f registry_f registry_required_f registry_optional_f
  local installed_c registry_c registry_required_c registry_optional_c excluded_c
  tmp_dir="$(mktemp -d)"
  installed_f="$tmp_dir/installed-formulae"
  installed_requested_f="$tmp_dir/installed-requested-formulae"
  registry_f="$tmp_dir/registry-formulae"
  registry_required_f="$tmp_dir/registry-formulae-required"
  registry_optional_f="$tmp_dir/registry-formulae-optional"
  installed_c="$tmp_dir/installed-casks"
  registry_c="$tmp_dir/registry-casks"
  registry_required_c="$tmp_dir/registry-casks-required"
  registry_optional_c="$tmp_dir/registry-casks-optional"
  excluded_c="$tmp_dir/excluded-casks"

  installed_registry_formulae | sort -u > "$installed_f"
  installed_requested_formulae > "$installed_requested_f"
  registry_items formulae all | sort -u > "$registry_f"
  registry_items formulae required | sort -u > "$registry_required_f"
  registry_items formulae optional | sort -u > "$registry_optional_f"
  print_missing "missing_on_machine.required.formulae" "$installed_f" "$registry_required_f"
  print_missing "missing_on_machine.optional.formulae" "$installed_f" "$registry_optional_f"
  print_missing "missing_from_registry.formulae" "$registry_f" "$installed_requested_f"

  if [ "$OS_NAME" = "Darwin" ]; then
    installed_casks > "$installed_c"
    registry_items casks all | sort -u > "$registry_c"
    registry_items casks required | sort -u > "$registry_required_c"
    registry_items casks optional | sort -u > "$registry_optional_c"
    registry_excluded_casks | sort -u > "$excluded_c"
    if [ -s "$excluded_c" ]; then
      grep -Fvx -f "$excluded_c" "$installed_c" > "$installed_c.filtered" || true
      mv "$installed_c.filtered" "$installed_c"
    fi
    print_missing "missing_on_machine.required.casks" "$installed_c" "$registry_required_c"
    print_missing "missing_on_machine.optional.casks" "$installed_c" "$registry_optional_c"
    print_missing "missing_from_registry.casks" "$registry_c" "$installed_c"
  else
    log "casks: skipped on non-macOS"
  fi

  rm -rf "$tmp_dir"
}

collect_brew_results() {
  local formula scope attempted final_status snapshot cask
  snapshot="$(snapshot_registry brew-result.tsv registry_scoped_items formulae)"
  while IFS=$'\t' read -r -u 3 formula scope; do
    [ -n "$formula" ] || continue
    attempted=false
    was_attempted formula "$formula" && attempted=true
    if brew list --formula --versions "$formula" >/dev/null 2>&1; then
      final_status="Present"
      [ "$attempted" = true ] && final_status="Installed"
    else
      final_status="StillMissing"
      [ "$DRY_RUN" -eq 1 ] && [ "$attempted" = true ] && final_status="Planned"
      attempt_failed formula "$formula" && final_status="Failed"
    fi
    record_result formula "$formula" "$scope" "$attempted" "$final_status" \
      "N/A" "N/A" "$([ "$final_status" = "StillMissing" ] && printf 'Formel installieren / install formula.' || printf 'N/A')"
  done 3< "$snapshot"

  [ "$OS_NAME" = "Darwin" ] || return 0
  snapshot="$(snapshot_registry cask-result.tsv registry_scoped_items casks)"
  while IFS=$'\t' read -r -u 3 cask scope; do
    [ -n "$cask" ] || continue
    attempted=false
    was_attempted cask "$cask" && attempted=true
    if cask_present "$cask"; then
      final_status="Present"
      [ "$attempted" = true ] && final_status="Installed"
    else
      final_status="StillMissing"
      [ "$DRY_RUN" -eq 1 ] && [ "$attempted" = true ] && final_status="Planned"
      attempt_failed cask "$cask" && final_status="Failed"
    fi
    record_result cask "$cask" "$scope" "$attempted" "$final_status" \
      "N/A" "N/A" "$([ "$final_status" = "StillMissing" ] && printf 'Cask installieren / install cask.' || printf 'N/A')"
  done 3< "$snapshot"
}

compare_vscode_registry() {
  [ "$SKIP_VSCODE_EXTENSIONS" -eq 0 ] || return 0

  local code_cli tmp_dir installed_ext registry_ext registry_required_ext registry_optional_ext deprecated_report
  local extension scope attempted final_status snapshot
  log "VS Code extension registry: $VSCODE_REGISTRY"
  if ! code_cli="$(find_vscode_cli)"; then
    log "vscode_cli: unavailable"
    log "missing_on_machine.required.vscode_extensions"
    vscode_registry_items extensions required | sed 's/^/  - /'
    log "missing_on_machine.optional.vscode_extensions"
    vscode_registry_items extensions optional | sed 's/^/  - /'
    snapshot="$(snapshot_registry vscode-result-unavailable.tsv vscode_registry_scoped_items)"
    while IFS=$'\t' read -r -u 3 extension scope; do
      [ -n "$extension" ] || continue
      attempted=false
      was_attempted vscode-extension "$extension" && attempted=true
      final_status="StillMissing"
      [ "$DRY_RUN" -eq 1 ] && [ "$attempted" = true ] && final_status="Planned"
      attempt_failed vscode-extension "$extension" && final_status="Failed"
      record_result vscode-extension "$extension" "$scope" "$attempted" "$final_status" \
        "Missing" "VS-Code-CLI nicht verfügbar / unavailable." \
        "VS-Code-CLI und Extension installieren / install VS Code CLI and extension."
    done 3< "$snapshot"
    return 0
  fi

  tmp_dir="$(mktemp -d)"
  installed_ext="$tmp_dir/installed-vscode-extensions"
  registry_ext="$tmp_dir/registry-vscode-extensions"
  registry_required_ext="$tmp_dir/registry-vscode-extensions-required"
  registry_optional_ext="$tmp_dir/registry-vscode-extensions-optional"
  installed_vscode_extensions "$code_cli" > "$installed_ext"
  vscode_registry_items extensions all | tr '[:upper:]' '[:lower:]' | sort -u > "$registry_ext"
  vscode_registry_items extensions required | tr '[:upper:]' '[:lower:]' | sort -u > "$registry_required_ext"
  vscode_registry_items extensions optional | tr '[:upper:]' '[:lower:]' | sort -u > "$registry_optional_ext"

  print_missing "missing_on_machine.required.vscode_extensions" "$installed_ext" "$registry_required_ext"
  print_missing "missing_on_machine.optional.vscode_extensions" "$installed_ext" "$registry_optional_ext"
  snapshot="$(snapshot_registry vscode-result.tsv vscode_registry_scoped_items)"
  while IFS=$'\t' read -r -u 3 extension scope; do
    [ -n "$extension" ] || continue
    attempted=false
    was_attempted vscode-extension "$extension" && attempted=true
    if grep -Fxiq -- "$extension" "$installed_ext"; then
      final_status="Present"
      [ "$attempted" = true ] && final_status="Installed"
    else
      final_status="StillMissing"
      [ "$DRY_RUN" -eq 1 ] && [ "$attempted" = true ] && final_status="Planned"
      attempt_failed vscode-extension "$extension" && final_status="Failed"
    fi
    record_result vscode-extension "$extension" "$scope" "$attempted" "$final_status" \
      "N/A" "N/A" "$([ "$final_status" = "StillMissing" ] && printf 'Extension installieren / install extension.' || printf 'N/A')"
  done 3< "$snapshot"

  deprecated_report="$(python3 - "$VSCODE_REGISTRY" "$installed_ext" <<'PY'
import json
import sys

registry_path, installed_path = sys.argv[1:3]
with open(registry_path, encoding="utf-8") as handle:
    registry = json.load(handle)
with open(installed_path, encoding="utf-8") as handle:
    installed = {line.strip().lower() for line in handle if line.strip()}
for item in registry.get("deprecatedExtensions", []):
    ext_id = item.get("id", "")
    if ext_id.lower() in installed:
        replacement = item.get("replacement", "")
        if replacement:
            print(f"{ext_id} -> {replacement}")
        else:
            print(ext_id)
PY
)"
  if [ -n "$deprecated_report" ]; then
    log "deprecated_installed.vscode_extensions"
    printf '%s\n' "$deprecated_report" | sed 's/^/  - /'
  else
    log "deprecated_installed.vscode_extensions: none"
  fi

  rm -rf "$tmp_dir"
}

install_brew_items() {
  local scope formula cask snapshot
  scope="required"
  [ "$INCLUDE_OPTIONAL" -eq 1 ] && scope="all"
  snapshot="$(snapshot_registry brew-install.tsv registry_items formulae "$scope")"

  while IFS= read -r -u 3 formula; do
    [ -z "$formula" ] && continue
    validate_item_id "$formula"
    if brew list --formula --versions "$formula" >/dev/null 2>&1; then
      log "OK formula: $formula"
    else
      log "INSTALL formula: $formula"
      mark_attempt formula "$formula"
      if ! run_cmd brew install "$formula" </dev/null; then
        mark_failed_attempt formula "$formula"
      fi
    fi
  done 3< "$snapshot"

  if [ "$OS_NAME" = "Darwin" ]; then
    snapshot="$(snapshot_registry cask-install.tsv registry_items casks "$scope")"
    while IFS= read -r -u 3 cask; do
      [ -z "$cask" ] && continue
      validate_item_id "$cask"
      if cask_present "$cask"; then
        log "OK cask: $cask"
      else
        log "INSTALL cask: $cask"
        mark_attempt cask "$cask"
        if ! run_cmd brew install --cask "$cask" </dev/null; then
          mark_failed_attempt cask "$cask"
        fi
      fi
    done 3< "$snapshot"
  fi
}

ensure_brew_formula_links() {
  local formula command resolved brew_prefix link_drift formula_snapshot command_snapshot
  brew_prefix="$(brew --prefix)"
  formula_snapshot="$(snapshot_registry linked-formulae.tsv registry_linked_formulae)"

  while IFS= read -r -u 3 formula; do
    [ -n "$formula" ] || continue
    validate_item_id "$formula"
    if ! brew list --formula --versions "$formula" >/dev/null 2>&1; then
      log "MISSING linked formula: $formula"
      continue
    fi

    link_drift=0
    command_snapshot="$(snapshot_registry "link-commands-${formula//\//_}.tsv" registry_link_commands "$formula")"
    while IFS= read -r -u 4 command; do
      [ -n "$command" ] || continue
      resolved="$(command -v "$command" 2>/dev/null || true)"
      case "$resolved" in
        "$brew_prefix"/bin/*|"$brew_prefix"/sbin/*) ;;
        *) link_drift=1 ;;
      esac
    done 4< "$command_snapshot"

    if [ "$link_drift" -eq 0 ]; then
      log "OK linked formula: $formula"
      continue
    fi
    if [ "$COMPARE_ONLY" -eq 1 ]; then
      log "LINK-DRIFT formula: $formula"
      continue
    fi

    log "LINK formula: $formula"
    run_cmd brew link "$formula" </dev/null
    if [ "$DRY_RUN" -eq 1 ]; then
      continue
    fi
    while IFS= read -r -u 4 command; do
      [ -n "$command" ] || continue
      resolved="$(command -v "$command" 2>/dev/null || true)"
      case "$resolved" in
        "$brew_prefix"/bin/*|"$brew_prefix"/sbin/*) ;;
        *)
          echo "Fehler: $command wird nach dem Linken nicht aus Homebrew aufgeloest: ${resolved:-missing}" >&2
          exit 1
          ;;
      esac
    done 4< "$command_snapshot"
  done 3< "$formula_snapshot"
}

install_vscode_extensions() {
  [ "$SKIP_VSCODE_EXTENSIONS" -eq 0 ] || return 0

  local scope code_cli extension installed_lc extension_lc snapshot
  scope="required"
  [ "$INCLUDE_OPTIONAL" -eq 1 ] && scope="all"

  if ! code_cli="$(find_vscode_cli)"; then
    if [ "$DRY_RUN" -eq 1 ]; then
      code_cli="code"
      log "WARN: Keine nutzbare VS-Code-CLI gefunden; Dry-Run zeigt geplante Extension-Kommandos mit 'code'."
    else
      log "SKIP vscode extensions: Keine nutzbare VS-Code-CLI gefunden. Vergleich meldet fehlende Extensions."
      return 0
    fi
  fi

  installed_lc=""
  if [ "$code_cli" != "code" ] || command -v code >/dev/null 2>&1; then
    installed_lc="$(installed_vscode_extensions "$code_cli" || true)"
  fi

  snapshot="$(snapshot_registry vscode-install.tsv vscode_registry_items extensions "$scope")"
  while IFS= read -r -u 3 extension; do
    [ -z "$extension" ] && continue
    validate_item_id "$extension"
    extension_lc="$(printf '%s\n' "$extension" | tr '[:upper:]' '[:lower:]')"
    if [ -n "$installed_lc" ] && printf '%s\n' "$installed_lc" | grep -Fxq "$extension_lc"; then
      log "OK vscode extension: $extension"
    else
      log "INSTALL vscode extension: $extension"
      mark_attempt vscode-extension "$extension"
      if ! run_cmd "$code_cli" --install-extension "$extension" </dev/null; then
        mark_failed_attempt vscode-extension "$extension"
      fi
    fi
  done 3< "$snapshot"
}

apt_package_available() {
  local pkg="$1"
  apt-cache policy "$pkg" 2>/dev/null | awk '/Candidate:/ { print $2; found=1 } END { if (!found) print "(none)" }' | grep -Fvq '(none)'
}

run_apt_fallback() {
  if [ "$OS_NAME" != "Linux" ] || ! command -v apt >/dev/null 2>&1; then
    echo "Fehler: Homebrew fehlt und apt-Fallback ist auf diesem System nicht verfuegbar." >&2
    exit 1
  fi

  if [ "$COMPARE_ONLY" -eq 1 ]; then
    log "apt fallback packages:"
    registry_items aptFallback all | sed 's/^/  - /'
    return 0
  fi

  if [ "$SKIP_UPGRADE" -eq 0 ]; then
    if [ "$ALLOW_ADMIN_PROMPTS" -eq 1 ]; then
      run_cmd sudo -- apt update </dev/null
      run_cmd sudo -- apt upgrade </dev/null
    else
      log "DEFERRED_ADMIN_REQUIRED: apt update/upgrade wurde nicht gestartet."
      log "DEFERRED_ADMIN_REQUIRED: apt update/upgrade was not started."
    fi
  fi

  local scope pkg snapshot
  scope="required"
  [ "$INCLUDE_OPTIONAL" -eq 1 ] && scope="all"
  snapshot="$(snapshot_registry apt-install.tsv registry_items aptFallback "$scope")"
  while IFS= read -r -u 3 pkg; do
    [ -z "$pkg" ] && continue
    validate_item_id "$pkg"
    if dpkg-query -W -f='${Status}' "$pkg" 2>/dev/null | grep -q 'install ok installed'; then
      log "OK apt: $pkg"
    elif apt_package_available "$pkg"; then
      mark_attempt apt "$pkg"
      if [ "$ALLOW_ADMIN_PROMPTS" -ne 1 ] && [ "$DRY_RUN" -eq 0 ]; then
        log "DEFERRED_ADMIN_REQUIRED apt: $pkg"
        mark_failed_attempt apt "$pkg"
      else
        log "INSTALL apt: $pkg"
        if ! run_cmd sudo -- apt install -y "$pkg" </dev/null; then
          mark_failed_attempt apt "$pkg"
        fi
      fi
    else
      log "SKIP apt unavailable: $pkg"
    fi
  done 3< "$snapshot"
}

collect_apt_results() {
  local pkg scope attempted final_status snapshot
  snapshot="$(snapshot_registry apt-result.tsv registry_scoped_items aptFallback)"
  while IFS=$'\t' read -r -u 3 pkg scope; do
    [ -n "$pkg" ] || continue
    attempted=false
    was_attempted apt "$pkg" && attempted=true
    if dpkg-query -W -f='${Status}' "$pkg" 2>/dev/null | grep -q 'install ok installed'; then
      final_status="Present"
      [ "$attempted" = true ] && final_status="Installed"
    else
      final_status="StillMissing"
      [ "$DRY_RUN" -eq 1 ] && [ "$attempted" = true ] && final_status="Planned"
      attempt_failed apt "$pkg" && final_status="Failed"
    fi
    record_result apt "$pkg" "$scope" "$attempted" "$final_status" \
      "N/A" "N/A" "$(
        if [ "$final_status" = "StillMissing" ] || [ "$final_status" = "Failed" ]; then
          printf 'Mit --allow-admin-prompts erneut ausführen / rerun with current authority.'
        else
          printf 'N/A'
        fi
      )"
  done 3< "$snapshot"
}

log "Agentic Homebrew registry maintenance"
log "Registry: $REGISTRY"
log "OS: $OS_NAME"

if command -v brew >/dev/null 2>&1; then
  if [ "$COMPARE_ONLY" -eq 0 ] && [ "$SKIP_UPGRADE" -eq 0 ]; then
    run_cmd brew update
    run_cmd brew upgrade
  fi

  if [ "$COMPARE_ONLY" -eq 0 ]; then
    install_brew_items
    install_vscode_extensions
    install_cli_tools
    install_npm_agent_tools
  fi

  ensure_brew_formula_links

  compare_brew_registry
  collect_brew_results
  compare_vscode_registry
  compare_cli_registry
  compare_npm_agent_registry
else
  run_apt_fallback
  if [ "$COMPARE_ONLY" -eq 0 ]; then
    install_vscode_extensions
    install_cli_tools
    install_npm_agent_tools
  fi
  compare_vscode_registry
  compare_cli_registry
  compare_npm_agent_registry
  collect_apt_results
fi

maintain_powershell_modules

result_mode="update"
[ "$DRY_RUN" -eq 1 ] && result_mode="dry-run"
[ "$COMPARE_ONLY" -eq 1 ] && result_mode="compare-only"
result_status=0
summary_output="$WORK_DIR/toolchain-summary.json"
python3 "$LINUX_HARDENING" summarize-results \
  --input "$RESULT_ROWS" \
  --output "$RESULT_FILE" \
  --platform "$OS_NAME" \
  --mode "$result_mode" > "$summary_output" || result_status=$?
if [ -f "$RESULT_FILE" ]; then
  python3 - "$RESULT_FILE" <<'PY'
import json
import sys

with open(sys.argv[1], encoding="utf-8") as handle:
    result = json.load(handle)
print(
    "TOOLCHAIN RESULT\t"
    f"{result['overallStatus']}\t{result['exitCode']}\t"
    f"required={len(result['remainingRequired'])}\t"
    f"optional={len(result['optionalDrift'])}"
)
for item in result["remainingRequired"]:
    print(f"REQUIRED DRIFT\t{item}")
for item in result["optionalDrift"]:
    print(f"OPTIONAL DRIFT\t{item}")
PY
  [ "$RESULT_FILE" = "$WORK_DIR/toolchain-result.json" ] \
    || log "Toolchain report / Bericht: $RESULT_FILE"
fi
exit "$result_status"
