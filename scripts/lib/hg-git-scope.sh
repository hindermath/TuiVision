#!/usr/bin/env bash
# hg-git-scope.sh — Git Scope Isolation Check (FR-009, SC-005)
# GIT-SCOPE-001: ~/.gitconfig.d/ exists
# GIT-SCOPE-002: [includeIf "gitdir:~/home-baseline-source/"] present in ~/.gitconfig
# Severity: WARN (non-fatal — compliance score NOT reduced)

hg_check_git_scope() {
  local gitconfig_d="${HOME}/.gitconfig.d"
  local gitconfig="${HOME}/.gitconfig"

  # GIT-SCOPE-001: ~/.gitconfig.d/ vorhanden / present
  if [ ! -d "$gitconfig_d" ]; then
    if [ "${OPT_JSON:-false}" = "true" ] || [ "${1:-}" = "--json" ]; then
      printf '{"check":"GIT-SCOPE-001","status":"WARN","message":"~/.gitconfig.d/ fehlt — Scope-Isolierung nicht konfiguriert / missing — scope isolation not configured"}\n'
    else
      printf "  WARN %-40s WARN: %s\n" \
        "~/.gitconfig.d/" \
        "~/.gitconfig.d/ fehlt — Scope-Isolierung nicht konfiguriert / missing — scope isolation not configured"
    fi
    return 0
  fi

  # GIT-SCOPE-002: includeIf für home-baseline-source in ~/.gitconfig
  if ! grep -qF 'gitdir:~/home-baseline-source/' "$gitconfig" 2>/dev/null; then
    if [ "${OPT_JSON:-false}" = "true" ] || [ "${1:-}" = "--json" ]; then
      printf '{"check":"GIT-SCOPE-002","status":"WARN","message":"includeIf für home-baseline-source nicht gefunden / not found for home-baseline-source"}\n'
    else
      printf "  WARN %-40s WARN: %s\n" \
        "~/.gitconfig" \
        "includeIf für home-baseline-source nicht gefunden / not found for home-baseline-source"
    fi
    return 0
  fi
}
