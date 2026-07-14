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
DRY_RUN=0
COMPARE_ONLY=0
SKIP_UPGRADE=0
INCLUDE_OPTIONAL=0
SKIP_VSCODE_EXTENSIONS=0

usage() {
  cat <<'USAGE'
Usage: maintain-agentic-brew-apps.sh [OPTIONS]

Options:
  --dry-run             Show package-manager actions without executing them
  --compare-only        Only compare installed packages with the registry
  --registry PATH       Use an alternative registry JSON
  --vscode-registry PATH
                        Use an alternative VS Code extensions registry JSON
  --npm-agent-registry PATH
                        Use an alternative npm agent CLI registry JSON
  --powershell-module-registry PATH
                        Use an alternative PowerShell module registry JSON
  --skip-upgrade        Skip brew/apt update+upgrade
  --skip-vscode-extensions
                        Skip VS Code extension install and comparison
  --include-optional    Also install optional registry entries
  -h, --help            Show this help
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

OS_NAME="$(uname -s)"

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

installed_formulae() {
  brew list --formula --full-name 2>/dev/null | sort -u
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
  local candidate seen
  seen=""
  while IFS= read -r candidate; do
    [ -n "$candidate" ] || continue
    case "$seen" in
      *"|$candidate|"*) continue ;;
    esac
    seen="$seen|$candidate|"
    [ -x "$candidate" ] || continue
    if "$candidate" --version >/dev/null 2>&1 && "$candidate" --list-extensions >/dev/null 2>&1; then
      printf '%s\n' "$candidate"
      return 0
    fi
    printf 'WARN: VS-Code-CLI nicht nutzbar: %s\n' "$candidate" >&2
  done < <(code_candidates)
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

run_probe_with_timeout() {
  python3 - "$@" <<'PY'
import os
import signal
import subprocess
import sys

try:
    process = subprocess.Popen(
        sys.argv[1:],
        stdout=subprocess.DEVNULL,
        stderr=subprocess.DEVNULL,
        start_new_session=True,
    )
except OSError:
    raise SystemExit(127)

try:
    raise SystemExit(process.wait(timeout=5))
except subprocess.TimeoutExpired:
    os.killpg(process.pid, signal.SIGTERM)
    try:
        process.wait(timeout=0.5)
    except subprocess.TimeoutExpired:
        os.killpg(process.pid, signal.SIGKILL)
        process.wait()
    raise SystemExit(124)
PY
}

cli_tool_available() {
  local command_name="$1"
  local args_json="$2"
  local -a args
  local arg

  command -v "$command_name" >/dev/null 2>&1 || return 1
  while IFS= read -r arg; do
    args+=("$arg")
  done < <(json_array_items "$args_json")
  run_probe_with_timeout "$command_name" "${args[@]}"
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
      if [ "$DRY_RUN" -eq 1 ] || command -v uv >/dev/null 2>&1; then
        run_cmd uv "${install_args[@]}"
      else
        log "SKIP cli tool install: $id (uv fehlt)"
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
      if [ "$DRY_RUN" -eq 1 ]; then
        log "DRY-RUN: download $install_url, verify sha256=$install_sha256, execute with bash"
        return 0
      fi
      command -v curl >/dev/null 2>&1 || {
        log "SKIP cli tool install: $id (curl fehlt)"
        return 0
      }
      local installer actual_sha256
      installer="$(mktemp)"
      if ! curl -fsSL "$install_url" -o "$installer"; then
        rm -f -- "$installer"
        log "SKIP cli tool install: $id (Download fehlgeschlagen)"
        return 0
      fi
      if command -v sha256sum >/dev/null 2>&1; then
        actual_sha256="$(sha256sum "$installer" | awk '{print $1}')"
      elif command -v shasum >/dev/null 2>&1; then
        actual_sha256="$(shasum -a 256 "$installer" | awk '{print $1}')"
      else
        rm -f -- "$installer"
        log "SKIP cli tool install: $id (SHA-256-Werkzeug fehlt)"
        return 0
      fi
      if [ "$actual_sha256" != "$install_sha256" ]; then
        rm -f -- "$installer"
        log "SKIP cli tool install: $id (Installer-Pruefsumme geaendert; Registry zuerst pruefen)"
        return 0
      fi
      bash "$installer"
      rm -f -- "$installer"
      ;;
    *)
      log "MISSING cli tool: $id"
      ;;
  esac
}

install_cli_tools() {
  local scope id command_name args_json manager install_args_json install_url install_sha256 interpreter
  scope="required"
  [ "$INCLUDE_OPTIONAL" -eq 1 ] && scope="all"

  while IFS=$'\t' read -r id command_name args_json manager install_args_json install_url install_sha256 interpreter; do
    [ -n "$id" ] || continue
    if cli_tool_available "$command_name" "$args_json"; then
      log "OK cli tool: $id"
    else
      install_cli_tool "$id" "$manager" "$install_args_json" "$install_url" "$install_sha256" "$interpreter"
    fi
  done < <(cli_registry_items "$scope")
}

install_npm_agent_tools() {
  local scope id package_name command_name args_json
  scope="required"
  [ "$INCLUDE_OPTIONAL" -eq 1 ] && scope="all"

  while IFS=$'\t' read -r id package_name command_name args_json; do
    [ -n "$id" ] || continue
    if cli_tool_available "$command_name" "$args_json"; then
      log "OK npm agent cli: $id"
    elif [ "$DRY_RUN" -eq 1 ] || command -v npm >/dev/null 2>&1; then
      log "INSTALL npm agent cli: $id ($package_name)"
      run_cmd npm install -g "$package_name"
    else
      log "MISSING npm agent cli: $id (npm fehlt)"
    fi
  done < <(npm_agent_registry_items "$scope")
}

compare_cli_scope() {
  local scope="$1"
  local label="$2"
  local id command_name args_json manager install_args_json install_url install_sha256 interpreter
  local missing
  missing=""

  while IFS=$'\t' read -r id command_name args_json manager install_args_json install_url install_sha256 interpreter; do
    [ -n "$id" ] || continue
    if ! cli_tool_available "$command_name" "$args_json"; then
      missing="${missing}${id}"$'\n'
    fi
  done < <(cli_registry_items "$scope")

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
  local missing
  missing=""

  while IFS=$'\t' read -r id package_name command_name args_json; do
    [ -n "$id" ] || continue
    if ! cli_tool_available "$command_name" "$args_json"; then
      missing="${missing}${id}"$'\n'
    fi
  done < <(npm_agent_registry_items "$scope")

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
  elif command -v pwsh >/dev/null 2>&1; then
    pwsh "${arguments[@]}"
  else
    log "MISSING powershell module runtime: pwsh"
  fi
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

  installed_formulae > "$installed_f"
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

compare_vscode_registry() {
  [ "$SKIP_VSCODE_EXTENSIONS" -eq 0 ] || return 0

  local code_cli tmp_dir installed_ext registry_ext registry_required_ext registry_optional_ext deprecated_report
  log "VS Code extension registry: $VSCODE_REGISTRY"
  if ! code_cli="$(find_vscode_cli)"; then
    log "vscode_cli: unavailable"
    log "missing_on_machine.required.vscode_extensions"
    vscode_registry_items extensions required | sed 's/^/  - /'
    log "missing_on_machine.optional.vscode_extensions"
    vscode_registry_items extensions optional | sed 's/^/  - /'
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
  local scope formula cask
  scope="required"
  [ "$INCLUDE_OPTIONAL" -eq 1 ] && scope="all"

  while IFS= read -r formula; do
    [ -z "$formula" ] && continue
    if brew list --formula --versions "$formula" >/dev/null 2>&1; then
      log "OK formula: $formula"
    else
      log "INSTALL formula: $formula"
      run_cmd brew install "$formula"
    fi
  done < <(registry_items formulae "$scope")

  if [ "$OS_NAME" = "Darwin" ]; then
    while IFS= read -r cask; do
      [ -z "$cask" ] && continue
      if cask_present "$cask"; then
        log "OK cask: $cask"
      else
        log "INSTALL cask: $cask"
        run_cmd brew install --cask "$cask"
      fi
    done < <(registry_items casks "$scope")
  fi
}

install_vscode_extensions() {
  [ "$SKIP_VSCODE_EXTENSIONS" -eq 0 ] || return 0

  local scope code_cli extension installed_lc extension_lc
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

  while IFS= read -r extension; do
    [ -z "$extension" ] && continue
    extension_lc="$(printf '%s\n' "$extension" | tr '[:upper:]' '[:lower:]')"
    if [ -n "$installed_lc" ] && printf '%s\n' "$installed_lc" | grep -Fxq "$extension_lc"; then
      log "OK vscode extension: $extension"
    else
      log "INSTALL vscode extension: $extension"
      run_cmd "$code_cli" --install-extension "$extension"
    fi
  done < <(vscode_registry_items extensions "$scope")
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
    run_cmd sudo apt update
    run_cmd sudo apt upgrade
  fi

  local scope pkg
  scope="required"
  [ "$INCLUDE_OPTIONAL" -eq 1 ] && scope="all"
  while IFS= read -r pkg; do
    [ -z "$pkg" ] && continue
    if dpkg-query -W -f='${Status}' "$pkg" 2>/dev/null | grep -q 'install ok installed'; then
      log "OK apt: $pkg"
    elif apt_package_available "$pkg"; then
      log "INSTALL apt: $pkg"
      run_cmd sudo apt install -y "$pkg"
    else
      log "SKIP apt unavailable: $pkg"
    fi
  done < <(registry_items aptFallback "$scope")
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

  compare_brew_registry
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
fi

maintain_powershell_modules
