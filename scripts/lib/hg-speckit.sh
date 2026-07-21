#!/usr/bin/env bash
# hg-speckit.sh — Spec-kit Template-Versionserkennung (FR-018, R-05)
# Vergleicht speckit_version aus ~/.specify/init-options.json mit spec.md-Header

hg_check_speckit() {
  local spec_file="$1"

  # Graceful skip if no .specify/ directory
  if ! [ -d "${HOME}/.specify" ]; then
    return 0
  fi

  if ! [ -f "$spec_file" ]; then
    return 0
  fi

  local speckit_ver
  speckit_ver=$(python3 -c "import json,sys; d=json.load(open('${HOME}/.specify/init-options.json')); print(d.get('speckit_version','unknown'))" 2>/dev/null \
    || grep -o '"speckit_version"[[:space:]]*:[[:space:]]*"[^"]*"' "${HOME}/.specify/init-options.json" 2>/dev/null | grep -o '"[^"]*"$' | tr -d '"' \
    || echo "unknown")

  # Check for explicit version header in spec.md
  local spec_ver
  spec_ver=$(grep '^\*\*Spec-kit Template Version\*\*:' "$spec_file" 2>/dev/null | grep -o '[0-9][0-9.]*' | head -1)

  if [ -n "$spec_ver" ]; then
    if [ "$spec_ver" = "$speckit_ver" ]; then
      echo "PASS|${spec_file}|speckit-version-current"
    else
      echo "WARN|${spec_file}|spec-template-version-outdated (spec: ${spec_ver}, installed: ${speckit_ver})"
    fi
    return
  fi

  # Fallback: check created date — if older than 90 days, warn
  local created_date today_epoch created_epoch days_old
  created_date=$(grep '^\*\*Created\*\*:' "$spec_file" 2>/dev/null | grep -o '[0-9]\{4\}-[0-9]\{2\}-[0-9]\{2\}' | head -1)
  if [ -n "$created_date" ]; then
    if command -v date &>/dev/null; then
      today_epoch=$(date +%s 2>/dev/null || echo 0)
      created_epoch=$(date -d "$created_date" +%s 2>/dev/null \
        || date -j -f "%Y-%m-%d" "$created_date" +%s 2>/dev/null \
        || echo 0)
      if [ "$created_epoch" -gt 0 ] && [ "$today_epoch" -gt 0 ]; then
        days_old=$(( (today_epoch - created_epoch) / 86400 ))
        if [ "$days_old" -gt 90 ]; then
          echo "WARN|${spec_file}|spec-may-be-outdated (created ${created_date}, ${days_old} days ago — verify template version)"
          return
        fi
      fi
    fi
  fi

  echo "PASS|${spec_file}|speckit-version-ok"
}
