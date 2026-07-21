#!/usr/bin/env bash
# hg-scan.sh — 3-Ebenen-Traversal Engine (FR-001)
# Gibt pro qualifizierendem Verzeichnis aus: level|path|type
# Level 0: TARGET_DIR (Home), Level 1: Workspaces, Level 2: Projekte
# Directories ohne .git/ auf Level 1/2 werden übersprungen (kein WARN)
# Level >=3 wird silently ignoriert

hg_scan() {
  local target_dir="${1:-$HOME}"
  # Normalize trailing slash
  target_dir="${target_dir%/}"
  local skip_gitignored=false
  if [ "$target_dir" != "$HOME" ] && git -C "$target_dir" rev-parse --is-inside-work-tree >/dev/null 2>&1; then
    skip_gitignored=true
  fi

  hg_is_ignored_child() {
    local child="$1"
    $skip_gitignored || return 1
    local rel="${child#${target_dir}/}"
    git -C "$target_dir" check-ignore -q -- "$rel" 2>/dev/null
  }

  # Level 0 — Target dir itself
  echo "0|${target_dir}|home"

  # Level 1 — Workspaces (subdirs of target_dir that have .git/)
  local workspace
  while IFS= read -r workspace; do
    [ -d "${workspace}/.git" ] || continue
    hg_is_ignored_child "$workspace" && continue
    echo "1|${workspace}|workspace"

    # Level 2 — Projects (subdirs of workspace that have .git/)
    local project
    while IFS= read -r project; do
      [ -d "${project}/.git" ] || continue
      hg_is_ignored_child "$project" && continue
      echo "2|${project}|project"
    done < <(find "${workspace}" -mindepth 1 -maxdepth 1 -type d 2>/dev/null | sort)

  done < <(find "${target_dir}" -mindepth 1 -maxdepth 1 -type d 2>/dev/null | sort)
}
