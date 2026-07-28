#!/usr/bin/env bash
# Validate language-aware requirements intake governance.
set -euo pipefail

script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
exec python3 "$script_dir/validate-intake-governance-config.py" "$@"
