#!/usr/bin/env bash
# init-stats.sh — STATS.md Baseline-Generator v1.0
# FR-REV-B04; Contract: init-stats-cli.md
#
# Usage: init-stats.sh [workspace-name-or-path]
# Exit codes: 0=success, 1=error
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# ─── Argument Parsing ────────────────────────────────────────────────────────

WORKSPACE_NAME=""
TARGET_DIR=""
while [ $# -gt 0 ]; do
  case "$1" in
    -h|--help) echo "USAGE: init-stats.sh [workspace-name-or-path]" >&2; exit 0 ;;
    --)
      shift
      if [ $# -gt 0 ] && [ -z "$WORKSPACE_NAME" ]; then WORKSPACE_NAME="$1"; shift; fi
      continue ;;
    --*) echo "ERROR: unknown option $1" >&2; exit 1 ;;
    *) WORKSPACE_NAME="$1" ;;
  esac
  shift
done

# ─── Prerequisites ───────────────────────────────────────────────────────────

check_script="${SCRIPT_DIR}/check-homogeneity.sh"
if ! [ -f "$check_script" ] || ! [ -x "$check_script" ]; then
  if [ -f "$check_script" ]; then
    chmod +x "$check_script"
  else
    echo "ERROR: check-homogeneity.sh nicht gefunden — zuerst FR-REV-B01 implementieren" >&2
    exit 1
  fi
fi

# ─── ASCII Bar Helper ─────────────────────────────────────────────────────────

make_bar() {
  local score="$1"
  # Round to nearest 5%
  local rounded=$(( (score + 2) / 5 * 5 ))
  local filled=$(( rounded / 5 ))
  [ "$filled" -gt 20 ] && filled=20
  local empty=$(( 20 - filled ))
  local bar=""
  local i=0
  while [ $i -lt $filled ]; do bar="${bar}#"; i=$((i+1)); done
  i=0
  while [ $i -lt $empty ]; do bar="${bar}."; i=$((i+1)); done
  printf '%s %d%%' "$bar" "$score"
}

# ─── STATS.md Header ─────────────────────────────────────────────────────────

stats_header() {
  cat <<'EOF'
# Statistiken / Statistics

| Datum / Date | Compliance-Score | Fortschritt / Progress |
|---|---|---|
EOF
}

assert_no_link_components() {
  local path="$1" current remaining component owner
  if [[ "$path" = /* ]]; then
    current="/"
    remaining="${path#/}"
  else
    current="$PWD"
    remaining="$path"
  fi
  while IFS= read -r component; do
    case "$component" in
      ""|.) continue ;;
      ..) return 1 ;;
    esac
    current="${current%/}/$component"
    if [ -L "$current" ]; then
      owner="$(stat -f '%u' "$current" 2>/dev/null || stat -c '%u' "$current" 2>/dev/null || true)"
      [ "$owner" = "0" ] || return 1
    fi
  done < <(printf '%s\n' "$remaining" | tr '/' '\n')
}

# ─── Write Stats Entry ────────────────────────────────────────────────────────

write_stats_entry() {
  local stats_file="$1"
  local score="$2"
  local timestamp parent temporary existing_mode=""
  parent="$(dirname -- "$stats_file")"
  if ! assert_no_link_components "$parent" \
      || [ ! -d "$parent" ] || [ -L "$parent" ] || [ -L "$stats_file" ]; then
    echo "ERROR: STATS.md target must be a regular non-linked path: $stats_file" >&2
    return 1
  fi
  if [ -e "$stats_file" ] && [ ! -f "$stats_file" ]; then
    echo "ERROR: STATS.md target is not a regular file: $stats_file" >&2
    return 1
  fi
  timestamp=$(date '+%Y-%m-%d %H:%M')
  local bar
  bar=$(make_bar "$score")
  local entry="| ${timestamp} | ${score}% | ${bar} |"

  temporary="$(mktemp "${parent}/.STATS.md.XXXXXX")" || return 1
  trap 'rm -f -- "$temporary"' RETURN
  if [ -f "$stats_file" ]; then
    cat -- "$stats_file" > "$temporary"
    case "$(uname -s)" in
      Darwin|FreeBSD) existing_mode="$(stat -f '%Lp' "$stats_file" 2>/dev/null || true)" ;;
      *) existing_mode="$(stat -c '%a' "$stats_file" 2>/dev/null || true)" ;;
    esac
  else
    stats_header > "$temporary"
  fi
  printf '%s\n' "$entry" >> "$temporary"
  [ -z "$existing_mode" ] || chmod "$existing_mode" "$temporary"
  if [ -L "$parent" ] || [ -L "$stats_file" ]; then
    echo "ERROR: STATS.md target changed to a linked path: $stats_file" >&2
    return 1
  fi
  mv -f -- "$temporary" "$stats_file"
  trap - RETURN
  echo "✓ STATS.md updated at ${stats_file}"
}

# ─── Get Score for a Directory ────────────────────────────────────────────────

get_score_for() {
  local target_dir="$1"
  local json_output
  json_output=$(bash "$check_script" "$target_dir" --json --dry-run 2>/dev/null) || true
  local score
  score=$(printf '%s' "$json_output" | grep -o '"score":[0-9]*' | grep -o '[0-9]*' | head -1)
  printf '%s' "${score:-0}"
}

resolve_target_dir() {
  local input="$1"

  if assert_no_link_components "$input" && [ -d "$input" ] && [ ! -L "$input" ]; then
    printf '%s' "${input%/}"
    return 0
  fi

  if assert_no_link_components "${HOME}/${input}" \
      && [ -d "${HOME}/${input}" ] && [ ! -L "${HOME}/${input}" ]; then
    printf '%s' "${HOME}/${input}"
    return 0
  fi

  return 1
}

# ─── Main ────────────────────────────────────────────────────────────────────

if [ -n "$WORKSPACE_NAME" ]; then
  # Scoped mode: support either a workspace name or an absolute path
  TARGET_DIR="$(resolve_target_dir "$WORKSPACE_NAME" || true)"
  if ! [ -d "$TARGET_DIR" ]; then
    echo "ERROR: Workspace/Pfad nicht gefunden: ${WORKSPACE_NAME}" >&2
    exit 1
  fi

  ws_score=$(get_score_for "$TARGET_DIR")
  write_stats_entry "${TARGET_DIR}/STATS.md" "$ws_score"

  # Level-2 projects within the target directory
  while IFS= read -r proj_dir; do
    [ -d "${proj_dir}/.git" ] || continue
    proj_score=$(get_score_for "$proj_dir")
    write_stats_entry "${proj_dir}/STATS.md" "$proj_score"
  done < <(find "$TARGET_DIR" -mindepth 1 -maxdepth 1 -type d 2>/dev/null | sort)

else
  # Full mode: Level 0, all Level-1 workspaces, all Level-2 projects

  # Level 0 — root
  l0_score=$(get_score_for "$HOME")
  write_stats_entry "${HOME}/STATS.md" "$l0_score"

  # Level 1 + Level 2
  while IFS= read -r ws_dir; do
    [ -d "${ws_dir}/.git" ] || continue
    ws_score=$(get_score_for "$ws_dir")
    write_stats_entry "${ws_dir}/STATS.md" "$ws_score"

    # Level-2 projects
    while IFS= read -r proj_dir; do
      [ -d "${proj_dir}/.git" ] || continue
      proj_score=$(get_score_for "$proj_dir")
      write_stats_entry "${proj_dir}/STATS.md" "$proj_score"
    done < <(find "$ws_dir" -mindepth 1 -maxdepth 1 -type d 2>/dev/null | sort)

  done < <(find "$HOME" -mindepth 1 -maxdepth 1 -type d 2>/dev/null | sort)
fi

exit 0
