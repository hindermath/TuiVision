#!/usr/bin/env bash
# hg-stats.sh — STATS.md Append-Only Schreiber (FR-007/008, R-04)
# Verwendet mkdir-Lock (atomares POSIX-Lock, 5s Timeout)

# Acquire atomic mkdir lock
_hg_acquire_lock() {
  local lockdir="$1"
  local timeout="${2:-5}"
  local elapsed=0
  while ! mkdir "$lockdir" 2>/dev/null; do
    sleep 1
    elapsed=$((elapsed + 1))
    if [ "$elapsed" -ge "$timeout" ]; then
      echo "WARN: stats file locked — try again later" >&2
      return 1
    fi
  done
  return 0
}

_hg_release_lock() {
  rmdir "$1" 2>/dev/null || true
}

# Build ASCII bar (20 chars)
_hg_bar() {
  local score="$1" width=20
  local filled=$(( score * width / 100 ))
  local empty=$(( width - filled ))
  local bar=""
  local i=0
  while [ $i -lt $filled ]; do bar="${bar}#"; i=$((i+1)); done
  i=0
  while [ $i -lt $empty ]; do bar="${bar}."; i=$((i+1)); done
  echo "$bar"
}

# Count entries in STATS.md (## Run headings)
_hg_count_entries() {
  local stats_file="$1"
  if ! [ -f "$stats_file" ]; then
    echo 0
    return 0
  fi
  grep -c '^## Run ' "$stats_file" 2>/dev/null || echo 0
}

# Archive STATS.md if >= 500 entries
_hg_maybe_archive() {
  local stats_file="$1"
  local count
  count=$(_hg_count_entries "$stats_file")
  if [ "$count" -ge 500 ]; then
    local year
    year=$(date +%Y)
    local archive_file
    archive_file="${stats_file%STATS.md}STATS-archive-${year}.md"
    cp "$stats_file" "$archive_file"
    # Keep header + last 50 entries
    local first_entry_line keep_entry_line
    first_entry_line=$(grep -n '^## Run ' "$stats_file" | head -1 | cut -d: -f1)
    keep_entry_line=$(grep -n '^## Run ' "$stats_file" | tail -50 | head -1 | cut -d: -f1)
    if [ -n "$first_entry_line" ] && [ -n "$keep_entry_line" ]; then
      {
        head -n "$((first_entry_line - 1))" "$stats_file"
        tail -n "+${keep_entry_line}" "$stats_file"
      } > "${stats_file}.new"
      mv "${stats_file}.new" "$stats_file"
    fi
    echo "STATS.md archived to: ${archive_file}" >&2
  fi
}

# Main: Write a STATS run entry
# Usage: hg_write_stats <stats_file> <score> [dir1 dir2 ...]
hg_write_stats() (
  local stats_file="$1"
  local score="$2"
  shift 2
  local dirs=("$@")

  local lockdir="${stats_file}.lock"

  # Acquire lock
  _hg_acquire_lock "$lockdir" 5 || return 1
  trap '_hg_release_lock "$lockdir"' EXIT INT TERM

  # Collision-safe timestamp
  local ts
  ts=$(date '+%Y-%m-%d %H:%M')
  local suffix=""
  local collision=2
  while grep -q "^## Run ${ts}${suffix}" "$stats_file" 2>/dev/null; do
    suffix=":${collision}"
    collision=$((collision + 1))
  done

  # Ensure file exists with header
  if ! [ -f "$stats_file" ]; then
    printf '# STATS.md\n\n## Überblick / Overview\n\nCompliance-Historie — Compliance History\n\n## Verwendung / Usage\n\nJeder `check-homogeneity.sh`-Aufruf fügt hier einen Eintrag hinzu.\n\nEach `check-homogeneity.sh` run appends an entry here.\n\n' > "$stats_file"
  fi

  # Append entry
  {
    printf '\n## Run %s%s\n\n' "$ts" "$suffix"
    printf 'Overall Score: **%d %%**\n\n' "$score"
    printf '| Level | Directory | Score %% |\n'
    printf '|-------|-----------|----------|\n'

    local i=0
    for dir in "${dirs[@]+"${dirs[@]}"}"; do
      local short_dir="$dir"
      if [[ "$dir" == "$HOME"* ]]; then
        short_dir="~${dir#"$HOME"}"
      fi
      printf '| %s | `%s` | — |\n' "$i" "$short_dir"
      i=$((i + 1))
    done

    echo ""
    printf 'ASCII Bar: [%s] %d %%\n' "$(_hg_bar "$score")" "$score"
    echo ""
  } >> "$stats_file"

  # Archive if needed
  _hg_maybe_archive "$stats_file"

  _hg_release_lock "$lockdir"
  trap - EXIT INT TERM
)
