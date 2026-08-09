#!/usr/bin/env bash
# Run Documentation Impact fixtures through the Bash entry point.
set -euo pipefail

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
REPO="$(cd -- "${SCRIPT_DIR}/.." && pwd)"
FIXTURES="${REPO}/scripts/tests/documentation-impact/fixtures"

run_case() {
  local file="$1"
  local expected="$2"
  local marker="${3:-}"
  local actual=0
  local output
  output="$(bash "${SCRIPT_DIR}/validate-documentation-impact.sh" \
    --evidence "${FIXTURES}/${file}" 2>&1)" || actual=$?
  if [ "$actual" -ne "$expected" ]; then
    printf 'Fixture %s expected %s, got %s.\\n' "$file" "$expected" "$actual" >&2
    exit 1
  fi
  if [ -n "$marker" ] && ! grep -Fq "$marker" <<<"$output"; then
    printf 'Fixture %s did not report expected class %s.\n' "$file" "$marker" >&2
    exit 1
  fi
}

run_case valid.json 0
run_case legacy-valid.json 0
run_case missing-architecture-fields.json 1 ARCHITECTURE
run_case invalid-distribution-class.json 1 DISTRIBUTION
run_case invalid-home-sync-type.json 1 DISTRIBUTION
run_case invalid-language-partners-type.json 1 LANGUAGE
run_case missing-decision.json 1 DECISION
run_case duplicate-id.json 1 DUPLICATE
run_case invalid-followup.json 1 FOLLOWUP
run_case unsafe-defer.json 1 RISK
printf '%s\n' 'PASS: Documentation Impact Bash fixtures (10 cases).'
