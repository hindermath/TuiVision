#!/usr/bin/env bash
# hg-hook.sh — SHA-256-Vergleich pre-push Hook (FR-002, R-03)
# Gibt WARN aus wenn Hook fehlt oder Hash abweicht

# Cross-platform SHA-256 helper
sha256_file() {
  local file="$1"
  if command -v sha256sum &>/dev/null; then
    sha256sum "$file" | awk '{print $1}'
  elif command -v shasum &>/dev/null; then
    shasum -a 256 "$file" | awk '{print $1}'
  else
    echo "ERROR: no sha256 utility found" >&2
    return 1
  fi
}

hg_check_hook() {
  local dir="$1"
  local canonical_hook="${HOME}/scripts/hooks/pre-push"
  local installed_hook="${dir}/.git/hooks/pre-push"

  # No canonical hook to compare against
  if ! [ -f "$canonical_hook" ]; then
    echo "WARN|${dir}|hook-canonical-missing"
    return 1
  fi

  # Hook not installed
  if ! [ -f "$installed_hook" ]; then
    echo "WARN|${dir}|hook-missing"
    return 1
  fi

  local canonical_hash installed_hash
  canonical_hash=$(sha256_file "$canonical_hook") || return 1
  installed_hash=$(sha256_file "$installed_hook") || return 1

  if [ "$canonical_hash" = "$installed_hash" ]; then
    echo "PASS|${dir}|hook-sha256-match"
  else
    echo "WARN|${dir}|hook-outdated"
  fi
}
