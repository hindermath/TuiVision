#!/usr/bin/env bash
# Validate one intake-series manifest without changing files.
set -euo pipefail

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
exec python3 "$script_dir/validate-intake-series.py" --kind manifest "$@"
