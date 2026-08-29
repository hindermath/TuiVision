#!/usr/bin/env bash
set -euo pipefail

script_dir=$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)
policy='requirements/source-reference-policy.json'
repo='.'
skip=0
self_test=''
json=0

while [ "$#" -gt 0 ]; do
  case "$1" in
    --policy) policy="${2:?--policy requires a file}"; shift 2 ;;
    --repo) repo="${2:?--repo requires a path}"; shift 2 ;;
    --skip-surface-checks) skip=1; shift ;;
    --self-test) self_test="${2:?--self-test requires a fixture matrix}"; shift 2 ;;
    --json) json=1; shift ;;
    -h|--help)
      printf '%s\n' 'Usage: validate-source-reference-policy.sh [--policy FILE] [--repo PATH] [--skip-surface-checks] [--self-test FILE] [--json]'
      exit 0 ;;
    *) printf 'ERROR: unknown option: %s\n' "$1" >&2; exit 2 ;;
  esac
done

arguments=("$script_dir/validate-source-reference-policy.py" --policy "$policy" --repo "$repo")
[ "$skip" -eq 1 ] && arguments+=(--skip-surface-checks)
[ -n "$self_test" ] && arguments+=(--self-test "$self_test")
[ "$json" -eq 1 ] && arguments+=(--json)
exec python3 "${arguments[@]}"
