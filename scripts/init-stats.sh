#!/usr/bin/env bash
# init-stats.sh — STATS.md Baseline-Generator v1.0
# FR-REV-B04; Contract: init-stats-cli.md
#
# Usage: init-stats.sh [workspace-name]
# Exit codes: 0=success, 1=error
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# ─── Argument Parsing ────────────────────────────────────────────────────────

WORKSPACE_NAME=""
while [ $# -gt 0 ]; do
  case "$1" in
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
  while [ $i -lt $filled ]; do bar="${bar}█"; i=$((i+1)); done
  i=0
  while [ $i -lt $empty ]; do bar="${bar}░"; i=$((i+1)); done
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

# ─── Write Stats Entry ────────────────────────────────────────────────────────

write_stats_entry() {
  local stats_file="$1"
  local score="$2"
  local timestamp
  timestamp=$(date '+%Y-%m-%d %H:%M')
  local bar
  bar=$(make_bar "$score")
  local entry="| ${timestamp} | ${score}% | ${bar} |"

  if [ -f "$stats_file" ]; then
    printf '%s\n' "$entry" >> "$stats_file"
  else
    mkdir -p "$(dirname "$stats_file")"
    stats_header > "$stats_file"
    printf '%s\n' "$entry" >> "$stats_file"
  fi
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

# ─── Main ────────────────────────────────────────────────────────────────────

if [ -n "$WORKSPACE_NAME" ]; then
  # Scoped mode: only the specified Level-1 workspace and its Level-2 projects
  ws_dir="${HOME}/${WORKSPACE_NAME}"
  if ! [ -d "$ws_dir" ]; then
    echo "ERROR: Workspace nicht gefunden: ${ws_dir}" >&2
    exit 1
  fi

  ws_score=$(get_score_for "$ws_dir")
  write_stats_entry "${ws_dir}/STATS.md" "$ws_score"

  # Level-2 projects within workspace
  while IFS= read -r proj_dir; do
    [ -d "${proj_dir}/.git" ] || continue
    proj_score=$(get_score_for "$proj_dir")
    write_stats_entry "${proj_dir}/STATS.md" "$proj_score"
  done < <(find "$ws_dir" -mindepth 1 -maxdepth 1 -type d 2>/dev/null | sort)

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
