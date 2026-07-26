#!/usr/bin/env bash
# Run Documentation Impact fixtures through the Bash entry point.
set -euo pipefail

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
REPO="$(cd -- "${SCRIPT_DIR}/.." && pwd)"
FIXTURES="${REPO}/specs/012-documentation-impact-governance/tests/fixtures"

run_case() {
  local file="$1"
  local expected="$2"
  local actual=0
  bash "${SCRIPT_DIR}/validate-documentation-impact.sh" \
    --evidence "${FIXTURES}/${file}" >/dev/null 2>&1 || actual=$?
  if [ "$actual" -ne "$expected" ]; then
    printf 'Fixture %s expected %s, got %s.\\n' "$file" "$expected" "$actual" >&2
    exit 1
  fi
}

run_case valid.json 0
run_case missing-decision.json 1
run_case duplicate-id.json 1
run_case invalid-followup.json 1
run_case unsafe-defer.json 1
printf '%s\n' 'PASS: Documentation Impact Bash fixtures (5 cases).'
